using AnyStore.BLL;
using AnyStore.DAL;
using DGVPrinterHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;

namespace AnyStore.UI
{
    public partial class frmRoomPayment : Form
    {
        public frmRoomPayment()
        {
            InitializeComponent();
        }

        private void frmRoomPayment_Load(object sender, EventArgs e)
        {
            string type = frmRooms.transactionType;
            string roomId = frmRooms.transactionNo;

            lblTop.Text = type;
            txtSearch.Text = roomId;

            transactionDT.Columns.Add("Product Name");
            transactionDT.Columns.Add("Rate");
            transactionDT.Columns.Add("Quantity");
            transactionDT.Columns.Add("Total");
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        roomsDAL dcDAL = new roomsDAL();
        productsDAL pDAL = new productsDAL();
        userDAL uDAL = new userDAL();
        transactionDAL tDAL = new transactionDAL();
        roomPaymentDAL tdDAL = new roomPaymentDAL();
        DataTable transactionDT = new DataTable();
        private void btnSave_Click(object sender, EventArgs e)
        {
            try {
                
                if (txtSubTotal.Text == "")
                {
                    MessageBox.Show("Please Calculate Total");
                }
                else {
                    transactionsBLL transaction = new transactionsBLL();

                    transaction.type = lblTop.Text;
                    string roomIde = frmRooms.transactionNo;
                    string deaCustName = txtName.Text;
                    transaction.id = int.Parse(roomIde);
                    transaction.transaction_date = DateTime.Now;
                    
                    string username = frmLogin.loggedIn;
                    userBLL u = uDAL.GetIDFromUsername(username);

                    transaction.added_by = u.id;
                    transaction.transactionDetails = transactionDT;
                    bool success = false;
                    using (TransactionScope scope = new TransactionScope())
                    {
                        
                        for (int i = 0; i < transactionDT.Rows.Count; i++)
                        {
                            roomPaymentBLL transactionDetail = new roomPaymentBLL();
                            
                            transactionDetail.room_no = int.Parse(roomIde);
                            transactionDetail.item = transactionDT.Rows[i][0].ToString();
                            transactionDetail.rate = decimal.Parse(transactionDT.Rows[i][1].ToString());
                            transactionDetail.quantity = int.Parse(transactionDT.Rows[i][2].ToString());
                            transactionDetail.price = Math.Round(decimal.Parse(transactionDT.Rows[i][3].ToString()), 2);
                            transactionDetail.date = DateTime.Now;

                            string transactionType = lblTop.Text;
                            bool y = tdDAL.InsertTransactionDetail(transactionDetail);
                            success = y;
                        }

                        if (success == true)
                        {
                            scope.Complete();
                            /*
                            DGVPrinter printer = new DGVPrinter();

                            printer.Title = "\r\n\r\n\r\n HOTEL NATURE LOVE \r\n\r\n";
                            printer.SubTitle = "Nuwara Eliya \r\n Phone: 076 5311075 \r\n\r\n";
                            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
                            printer.PageNumbers = true;
                            printer.PageNumberInHeader = false;
                            printer.PorportionalColumns = true;
                            printer.HeaderCellAlignment = StringAlignment.Near;
                            printer.Footer = "\r\n\r\n" + "Thank you for choosing us.";
                            printer.FooterSpacing = 15;
                            printer.PrintDataGridView(dgvAddedProducts);
                            */
                            MessageBox.Show("Transaction Completed Sucessfully");
                            dgvAddedProducts.DataSource = null;
                            dgvAddedProducts.Rows.Clear();
                            
                            this.Hide();

                            txtSearch.Text = "";
                            txtName.Text = "";
                            txtEmail.Text = "";
                            txtContact.Text = "";
                            txtAddress.Text = "";
                            txtSearchProduct.Text = "";
                            txtProductName.Text = "";
                            txtInventory.Text = "0";
                            txtRate.Text = "0";
                            TxtQty.Text = "0";
                            txtSubTotal.Text = "0";
                        }
                        else
                        {
                            MessageBox.Show("Transaction Failed");
                        }
                    }
                }
            }
            catch {
                MessageBox.Show("Unexpected Error, Transaction Failed");
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
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
            txtContact.Text = dc.contact;
            txtAddress.Text = dc.address;
        }

        private void txtSearchProduct_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearchProduct.Text;
            if (keyword == "")
            {
                txtProductName.Text = "";
                txtInventory.Text = "";
                txtRate.Text = "";
                TxtQty.Text = "";
                return;
            }
            productsBLL p = pDAL.GetProductsForTransaction(keyword);
            txtProductName.Text = p.name;
            txtInventory.Text = p.qty.ToString();
            txtRate.Text = p.rate.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try {
                if (txtProductName.Text == "")
                {
                    MessageBox.Show("Select Item First");
                }
                else {
                    string productName = txtProductName.Text;
                    decimal Rate = decimal.Parse(txtRate.Text);
                    decimal Qty = decimal.Parse(TxtQty.Text);
                    decimal Total = Rate * Qty; 
                    decimal subTotal = decimal.Parse(txtSubTotal.Text);
                    subTotal = subTotal + Total;

                    if (productName == "")
                    {
                        MessageBox.Show("Select the product first. Try Again.");
                    }
                    else
                    {
                        transactionDT.Rows.Add(productName, Rate, Qty, Total);
                        dgvAddedProducts.DataSource = transactionDT;
                        txtSubTotal.Text = subTotal.ToString();

                        txtSearchProduct.Text = "";
                        txtProductName.Text = "";
                        txtInventory.Text = "";
                        txtRate.Text = "";
                        TxtQty.Text = "";
                        txtSearchProduct.Focus();
                    }
                }
            }
            catch {
                if (TxtQty.Text == "")
                {
                    MessageBox.Show("Please Enter Quantity");
                }
                else
                {
                    MessageBox.Show("Error");
                }
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
        }

        private void txtRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) )
            {
                e.Handled = true;
            }
        }

        private void TxtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
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

        private void button1_Click(object sender, EventArgs e)
        {            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtInventory.Text == "")
            {
                MessageBox.Show("Select Item To Remove First");
            }
            else
            {
                try
                {
                    int rowindex = dgvAddedProducts.CurrentCell.RowIndex;
                    dgvAddedProducts.Rows.RemoveAt(rowindex);

                    decimal sum = decimal.Parse(txtSubTotal.Text) - decimal.Parse(txtInventory.Text);
                    txtProductName.Text = "";
                    txtRate.Text = "";
                    txtInventory.Text = "";
                    TxtQty.Text = "";
                    txtSubTotal.Text = sum.ToString();
                }
                catch
                {
                    MessageBox.Show("Error In Removing Item");
                }
            }
        }

        private void dgvAddedProducts_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try {
                int rowIndex = e.RowIndex;
                txtSearchProduct.Text = "";
                txtProductName.Text = "";
                txtInventory.Text = "";
                txtRate.Text = "";
                TxtQty.Text = "";

                txtProductName.Text = dgvAddedProducts.Rows[rowIndex].Cells[0].Value.ToString();
                txtRate.Text = dgvAddedProducts.Rows[rowIndex].Cells[1].Value.ToString();
                TxtQty.Text = dgvAddedProducts.Rows[rowIndex].Cells[2].Value.ToString();
                txtInventory.Text = dgvAddedProducts.Rows[rowIndex].Cells[3].Value.ToString();
            }
            catch
            { 
                MessageBox.Show("Select Row Header");
            }
            
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
