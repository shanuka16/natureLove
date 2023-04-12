using AnyStore.BLL;
using AnyStore.DAL;
using DGVPrinterHelper;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;

namespace AnyStore.UI
{
    public partial class frmCheckOut : Form
    {
        public frmCheckOut()
        {
            InitializeComponent();
        }
        ReportDataSource rs = new ReportDataSource();
        roomPaymentDAL dcDal = new roomPaymentDAL();
        roomsDAL dcDAL = new roomsDAL();
        productsDAL pDAL = new productsDAL();
        userDAL uDAL = new userDAL();
        transactionDAL tDAL = new transactionDAL();
        roomPaymentDAL tdDAL = new roomPaymentDAL();
        DataTable transactionDT = new DataTable();

        private void frmCheckOut_Load(object sender, EventArgs e)
        {
            string type = frmRooms.transactionType;
            string roomId = frmRooms.transactionNo;
            txtSearch.Text = roomId;
            txtRoomNumber.Text = roomId;
            int roomNo = int.Parse(roomId);
            
            DataTable dt = dcDal.Select(roomNo);
            dgvAddedProducts.DataSource = dt;

            transactionDT.Columns.Add("Product Name");
            transactionDT.Columns.Add("Rate");
            transactionDT.Columns.Add("Quantity");
            transactionDT.Columns.Add("Total");

            int sum = 0;
            for (int i=0;i< dgvAddedProducts.Rows.Count; i++)
            {
                sum += Convert.ToInt32(dgvAddedProducts.Rows[i].Cells[4].Value);
            }
            txtSubTotal.Text = sum.ToString();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try { 
            string keyword = txtSearch.Text;
            if (keyword == "")
            {
                txtName.Text = "";
                txtEmail.Text = "";
                txtContact.Text = "";
                txtAddress.Text = "";
                return;
            }

            roomsBLL dc = dcDAL.SearchDealerCustomerForTransaction(keyword);
            txtName.Text = dc.Cust_name;
            txtEmail.Text = dc.email;
            txtContact.Text = dc.contact.ToString();
            txtAddress.Text = dc.address;
            txtAddedDate.Text = dc.added_date.ToString();                
            }
            catch
            {
                MessageBox.Show("Add Customer To Room First");
            }

}

        private void txtSearchProduct_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtRoomNumber.Text;
            if (keyword == "")
            {
                txtRoomName.Text = "";
                txtRoomCharge.Text = "";
                txtOtherCharges.Text = "";
                TxtRoomDiscount.Text = "";
                return;
            }

            RoomTypesBLL rt = tdDAL.SearchRoomIdForTransaction(keyword);
            txtRoomName.Text = rt.type;
            txtRoomCharge.Text = rt.price.ToString();
            txtOtherCharges.Text = rt.description;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try {
                if (txtNoOfDays.Text == "")
                {
                    MessageBox.Show("Please Enter Number Of Days");
                }
                if (txtOtherCharges.Text == "")
                {
                    txtOtherCharges.Text = "0";
                }
                if (TxtRoomDiscount.Text == "")
                {
                    TxtRoomDiscount.Text = "0";
                }
                else
                {
                    string roomName = txtRoomName.Text;
                    
                    decimal netTotal = (decimal.Parse(txtRoomCharge.Text) * decimal.Parse(txtNoOfDays.Text)) + decimal.Parse(txtOtherCharges.Text);
                    decimal total = netTotal - (netTotal * decimal.Parse(TxtRoomDiscount.Text) / 100);

                    txtRoomtotal.Text = total.ToString();
                }                
            }
            catch {
                MessageBox.Show("Unexpected Error, Please Try Again");
            }
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtVat_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtPaidAmount_TextChanged(object sender, EventArgs e)
        {
            try {
                decimal grandTotal = decimal.Parse(txtbxGrandTotal.Text);
                decimal paidAmount = decimal.Parse(txtPaidAmount.Text);

                decimal returnAmount = paidAmount - grandTotal;
                txtReturnAmount.Text = returnAmount.ToString();
            }
            catch {
                MessageBox.Show("Please Enter Paid Amount");
                txtPaidAmount.Text = "0";
                txtReturnAmount.Text = "0";
            }
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try {
                if (txtGrandTotal.Text == "" )
                {
                    MessageBox.Show("Calculate Food Charges");
                }
                else if (txtRoomtotal.Text == ""|| txtRoomtotal.Text == "0")
                {
                    MessageBox.Show("Calculate Room Charges");
                }
                else if (txtbxGrandTotal.Text == "" || txtbxGrandTotal.Text == "0")
                {
                    MessageBox.Show("Calculate Grand Total");
                }
                else if (txtPaidAmount.Text == "")
                {
                    MessageBox.Show("Enter Paid Amount");
                }
                else {
                    if (decimal.Parse(txtPaidAmount.Text)< decimal.Parse(txtbxGrandTotal.Text))
                    {
                        MessageBox.Show("Paid Amount is Less Than Bill Amount");
                    }
                    else {
                        transactionsBLL transaction = new transactionsBLL();
                        transaction.type = lblTop.Text;
                        string roomIde = frmRooms.transactionNo;
                        string deaCustName = txtName.Text;

                        transaction.id = int.Parse(roomIde);
                        transaction.grandTotal = Math.Round(decimal.Parse(txtbxGrandTotal.Text), 2);
                        transaction.transaction_date = DateTime.Now;
                        transaction.tax = Math.Round(decimal.Parse(txtVat.Text), 2);
                        transaction.discount = Math.Round(decimal.Parse(txtDiscount.Text), 2);
                        
                        //Get the Username of Logged in user
                        string username = frmLogin.loggedIn;
                        userBLL u = uDAL.GetIDFromUsername(username);

                        transaction.added_by = u.id;
                        transaction.transactionDetails = transactionDT;

                        bool success = false;

                        //Actual Code to Insert Transaction And Transaction Details
                        using (TransactionScope scope = new TransactionScope())
                        {
                            int keyword = int.Parse(txtRoomNumber.Text);
                            int transactionID = -1; 
                            bool w = tDAL.Insert_Room_Transaction(transaction, out transactionID);
                            
                            success = w;
                            if (success == true)
                            {
                                //Creating the text file
                                /*DateTime dt = DateTime.Now;

                                int a = dt.Day, b = dt.Month, c = dt.Year;
                                String str = @"C:\Users\Administrator\Desktop\Nature Love Logs\" + c.ToString() + "/Month " + b.ToString() + "/" + a.ToString();
                                Directory.CreateDirectory(str);
                                StreamWriter txt = new StreamWriter("C:/Users/Administrator/Desktop/Nature Love Logs/" + c.ToString() + "/Month " + b.ToString() + "/" + a.ToString() + "/"+txtName.Text.ToString()+ txtContact.Text + ".txt");
                                //StreamWriter txt = new StreamWriter("C:/Users/Administrator/Desktop/firefox downloads/demo.txt");
                                txt.WriteLine("Custome name        " + txtName.Text);
                                txt.WriteLine("Custome Email       " + txtEmail.Text);
                                txt.WriteLine("Custome Contact     " + txtContact.Text);
                                txt.WriteLine("Custome Address     " + txtAddress.Text);
                                txt.WriteLine("Customer Added Date " + txtAddedDate.Text);
                                txt.WriteLine("Bill Date           " + dtpBillDate.Text);
                                txt.WriteLine("\n");
                                txt.WriteLine("Room Name           " + txtRoomName.Text);
                                txt.WriteLine("Room Charge         " + txtRoomCharge.Text);
                                txt.WriteLine("No Of Days          " + txtNoOfDays.Text);
                                txt.WriteLine("Other Chargers      " + txtOtherCharges.Text);
                                txt.WriteLine("Room Discount       " + TxtRoomDiscount.Text);
                                txt.WriteLine("Room Total          " + txtRoomtotal.Text);
                                txt.WriteLine("\n");
                                txt.WriteLine("Ordered Items Total " + txtSubTotal.Text);
                                txt.WriteLine("Items Discount      " + txtDiscount.Text);
                                txt.WriteLine("Items Service Charge" + txtVat.Text);
                                txt.WriteLine("Room Total          " + txtGrandTotal.Text);
                                txt.WriteLine("\n");

                                txt.WriteLine("Bill Total          " + txtbxGrandTotal.Text);
                                txt.WriteLine("Paid Amount         " + txtPaidAmount.Text);
                                txt.WriteLine("Return Amount       " + txtReturnAmount.Text);
                                txt.WriteLine("\n");
                                txt.WriteLine("\n");

                                txt.WriteLine("Item Name        Price       Quantity        Total");
                                txt.WriteLine("\n");
                                for (int i = 0; i < dgvAddedProducts.Rows.Count - 1; i++)
                                {
                                    for (int j = 0; j < dgvAddedProducts.Columns.Count; j++)
                                    {
                                        txt.Write($"{dgvAddedProducts.Rows[i].Cells[j].Value.ToString()}");

                                        if (j != dgvAddedProducts.Columns.Count - 1)
                                        {
                                            txt.Write("     ");
                                        }
                                    }
                                    txt.WriteLine();
                                }

                                txt.Write(dgvAddedProducts.Text);
                                txt.Close();
                                */

                                //Code to Print Bill
                                ReportParameterCollection reportParameters = new ReportParameterCollection();
                                reportParameters.Add(new ReportParameter("pCustName", txtName.Text));
                                reportParameters.Add(new ReportParameter("pAddeDate", txtAddedDate.Text));

                                reportParameters.Add(new ReportParameter("pRoomNo", txtRoomNumber.Text));
                                reportParameters.Add(new ReportParameter("pRoomName", txtRoomName.Text));

                                reportParameters.Add(new ReportParameter("pNoOfDays", txtNoOfDays.Text));
                                reportParameters.Add(new ReportParameter("pRoomCharge", txtRoomCharge.Text));
                                reportParameters.Add(new ReportParameter("pOtherCharges", txtOtherCharges.Text));
                                reportParameters.Add(new ReportParameter("pRoomDiscount", TxtRoomDiscount.Text));
                                reportParameters.Add(new ReportParameter("pRoomTotal", txtRoomtotal.Text));

                                reportParameters.Add(new ReportParameter("pItemDiscount", txtDiscount.Text));
                                reportParameters.Add(new ReportParameter("pItemServiceCharge", txtVat.Text));
                                reportParameters.Add(new ReportParameter("pItemTotal", txtGrandTotal.Text));

                                reportParameters.Add(new ReportParameter("pTotal", txtbxGrandTotal.Text));
                                reportParameters.Add(new ReportParameter("pPaidAmount", txtPaidAmount.Text));
                                reportParameters.Add(new ReportParameter("pBalance", txtReturnAmount.Text));

                                reportParameters.Add(new ReportParameter("pDate", dtpBillDate.Text));
                                List<recieptBll> blldta = new List<recieptBll>();
                                blldta.Clear();
                                for (int i = 0; i < dgvAddedProducts.Rows.Count - 1; i++)
                                {
                                    recieptBll bdt = new recieptBll
                                    {
                                        prdctNme = Convert.ToString(dgvAddedProducts.Rows[i].Cells[1].Value),
                                        rate = Convert.ToDecimal(dgvAddedProducts.Rows[i].Cells[2].Value),
                                        qty = Convert.ToInt32(dgvAddedProducts.Rows[i].Cells[3].Value),
                                        Total = Convert.ToDecimal(dgvAddedProducts.Rows[i].Cells[4].Value)
                                    };
                                    blldta.Add(bdt);
                                }
                                rs.Name = "DataSet1";
                                rs.Value = blldta;
                                frmRecieptCheckout ft = new frmRecieptCheckout();

                                ft.reportViewer1.LocalReport.SetParameters(reportParameters);
                                ft.reportViewer1.LocalReport.DataSources.Clear();
                                ft.reportViewer1.LocalReport.DataSources.Add(rs);
                                ft.reportViewer1.LocalReport.ReportEmbeddedResource = "AnyStore.UI.ReportCheckout.rdlc";
                                ft.ShowDialog();

                                MessageBox.Show("Transaction Completed Sucessfully");
                                //Celar the Data Grid View and Clear all the TExtboxes
                                dgvAddedProducts.DataSource = null;
                                dgvAddedProducts.Rows.Clear();

                                txtSearch.Text = "";
                                txtName.Text = "";
                                txtEmail.Text = "";
                                txtContact.Text = "";
                                txtAddress.Text = "";
                                txtRoomNumber.Text = "";
                                txtRoomName.Text = "";
                                txtRoomCharge.Text = "0";
                                txtOtherCharges.Text = "0";
                                TxtRoomDiscount.Text = "0";
                                txtSubTotal.Text = "0";
                                txtDiscount.Text = "0";
                                txtVat.Text = "0";
                                txtGrandTotal.Text = "0";
                                txtPaidAmount.Text = "0";
                                txtReturnAmount.Text = "0";

                                dcDAL.CheckoutUserRemove(keyword);
                                MessageBox.Show("User Removed From Room");

                                //add messagebox here

                                /*DialogResult dlgResult = MessageBox.Show("Remove customer details","delete user", MessageBoxButtons.YesNo);
                                if (dlgResult == DialogResult.Yes) {
                                    dcDAL.CheckoutUserRemove(keyword);
                                    MessageBox.Show("User removed");
                                }
                                else if (dlgResult == DialogResult.No) {
                                    MessageBox.Show("User data not reoved from room, you can remove it later");
                                }
                                else { }*/
                                scope.Complete();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Transaction Failed, Please Try Again");
                            }
                        }
                    }
                }
            }
            catch {
                MessageBox.Show("Error, Please Check And Fill All Details Correctly");
            }
        }

        private void txtGrandTotal_TextChanged(object sender, EventArgs e)
        {
            decimal txtOne = decimal.Parse(txtRoomtotal.Text) ;
            decimal totone = decimal.Parse(txtGrandTotal.Text) + txtOne;
            txtbxGrandTotal.Text = totone.ToString();
        }

        private void txtRoomtotal_TextChanged(object sender, EventArgs e)
        {
            decimal txtTwo = decimal.Parse(txtRoomtotal.Text) ;
            decimal totTwo = decimal.Parse(txtGrandTotal.Text)  + txtTwo;
            txtbxGrandTotal.Text = totTwo.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSubTotal.Text == "")
                {
                    txtSubTotal.Text = "0";
                }
                if (txtDiscount.Text == "")
                {
                    txtDiscount.Text = "0";
                }
                if (txtVat.Text == "")
                {
                    txtVat.Text = "0";
                }
                double newtot = double.Parse(txtSubTotal.Text);
                double discount = double.Parse(txtDiscount.Text);
                double serviceChrg = double.Parse(txtVat.Text);

                double totDis = ((100 + serviceChrg) / 100) * newtot;
                double grandTot = ((100 - discount) / 100) * totDis;

                txtGrandTotal.Text = grandTot.ToString();
            }
            catch
            {
                MessageBox.Show("Unexpected Error, Please Try Again");
            }
        }

        private void txtNoOfDays_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtOtherCharges_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
            (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void TxtRoomDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
            (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtVat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtPaidAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
            (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
    }
}
