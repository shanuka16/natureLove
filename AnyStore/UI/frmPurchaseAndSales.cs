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
    public partial class frmPurchaseAndSales : Form
    {
        public frmPurchaseAndSales()
        {
            InitializeComponent();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        ReportDataSource rs = new ReportDataSource();
        DeaCustDAL dcDAL = new DeaCustDAL();
        productsDAL pDAL = new productsDAL();
        userDAL uDAL = new userDAL();
        transactionDAL tDAL = new transactionDAL();
        transactionDetailDAL tdDAL = new transactionDetailDAL();
        DataTable transactionDT = new DataTable();
        private void frmPurchaseAndSales_Load(object sender, EventArgs e)
        {
            string type = frmUserDashboard.transactionType;
            lblTop.Text = type;

            transactionDT.Columns.Add("Product Name");
            transactionDT.Columns.Add("Rate");
            transactionDT.Columns.Add("Quantity");
            transactionDT.Columns.Add("Total");
        }

        private void txtSearchProduct_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearchProduct.Text;
            productsBLL p = pDAL.GetProductsForTransaction(keyword);

            txtProductName.Text = p.name;
            txtInventory.Text = p.qty.ToString();
            txtRate.Text = p.rate.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try{
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
                        txtGrandTotal.Text = "";
                        txtPaidAmount.Text = "";
                        txtReturnAmount.Text = "";
                        txtSearchProduct.Focus();
                    }
                }
            }
            catch (Exception ex){
                if (TxtQty.Text == "")
                {
                    MessageBox.Show("Please Enter Quantity");
                }
                else {
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
            if (txtPaidAmount.Text != "" || txtPaidAmount.Text != "0")
            {
                try
                {
                    decimal grandTotal = decimal.Parse(txtGrandTotal.Text);
                    decimal paidAmount = decimal.Parse(txtPaidAmount.Text);

                    decimal returnAmount = paidAmount - grandTotal;

                    txtReturnAmount.Text = returnAmount.ToString();
                }
                catch
                {
                    MessageBox.Show("Enter Paid Amount");
                    txtReturnAmount.Text = "0";
                }
            }
            else {
                MessageBox.Show("Paid Amount is 0");
                txtPaidAmount.Text = "0";
                txtReturnAmount.Text = "0";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            /*
            DateTime dt = DateTime.Now;
            
            int a = dt.Day, b = dt.Month, c = dt.Year;
            String str = @"C:\Users\Administrator\Desktop\Nature Love Logs\" + c.ToString() + "/Month " + b.ToString() + "/" + a.ToString();
            Directory.CreateDirectory(str);
            StreamWriter txt = new StreamWriter("C:/Users/Administrator/Desktop/Nature Love Logs/" + c.ToString() + "/Month " + b.ToString() + "/" + a.ToString() +"/H"+DateTime.Now.ToString("hh")+" M" + DateTime.Now.ToString("mm")+" S" + DateTime.Now.ToString("ss") + ".txt");
            //StreamWriter txt = new StreamWriter("C:/Users/Administrator/Desktop/firefox downloads/demo.txt");
            txt.WriteLine("Grand Total         "+txtGrandTotal.Text);
            txt.WriteLine("Service Charge   " + txtVat.Text);
            txt.WriteLine("Discount Amount     " + txtDiscount.Text);
            txt.WriteLine("\n");
            txt.WriteLine("\n");

            txt.WriteLine("                     Price   Quantity    Total");
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
            txt.Close();*/
            
            try {
                if (txtGrandTotal.Text == "")
                {
                    MessageBox.Show("Calculate Grand Total");
                }
                
                else 
                {
                    if (txtPaidAmount.Text == "" || txtPaidAmount.Text == "0")
                    {
                        
                        transactionsBLL transaction = new transactionsBLL();
                        transaction.type = lblTop.Text;

                        transaction.grandTotal = Math.Round(decimal.Parse(txtGrandTotal.Text), 2);
                        transaction.transaction_date = DateTime.Now;
                        transaction.tax = Math.Round(decimal.Parse(txtVat.Text), 2);
                        transaction.discount = Math.Round(decimal.Parse(txtDiscount.Text), 2);

                        string username = frmLogin.loggedIn;
                        userBLL u = uDAL.GetIDFromUsername(username);

                        transaction.added_by = u.id;
                        transaction.transactionDetails = transactionDT;
                        bool success = false;
                        using (TransactionScope scope = new TransactionScope())
                        {
                            int transactionID = -1;
                            bool w = tDAL.Insert_Transaction(transaction, out transactionID);

                            for (int i = 0; i < transactionDT.Rows.Count; i++)
                            {
                                transactionDetailBLL transactionDetail = new transactionDetailBLL();
                                string ProductName = transactionDT.Rows[i][0].ToString();
                                productsBLL p = pDAL.GetProductIDFromName(ProductName);

                                transactionDetail.product_id = p.id;
                                transactionDetail.rate = decimal.Parse(transactionDT.Rows[i][1].ToString());
                                transactionDetail.qty = decimal.Parse(transactionDT.Rows[i][2].ToString());
                                transactionDetail.total = Math.Round(decimal.Parse(transactionDT.Rows[i][3].ToString()), 2);
                                transactionDetail.added_date = DateTime.Now;
                                transactionDetail.added_by = u.id;

                                string transactionType = lblTop.Text;

                                bool y = tdDAL.InsertTransactionDetail(transactionDetail);
                                success = w && y;
                            }

                            if (success == true)
                            {
                                scope.Complete();
                                txtPaidAmount.Text = "0";
                                txtReturnAmount.Text = "0";
                                ReportParameterCollection reportParameters = new ReportParameterCollection();
                                reportParameters.Add(new ReportParameter("pTotal", txtGrandTotal.Text));
                                reportParameters.Add(new ReportParameter("pDiscount", txtDiscount.Text));
                                reportParameters.Add(new ReportParameter("pServiceCharge", txtVat.Text));
                                reportParameters.Add(new ReportParameter("pPaidAmount", txtPaidAmount.Text));
                                reportParameters.Add(new ReportParameter("pBalance", txtReturnAmount.Text));
                                reportParameters.Add(new ReportParameter("pDate", dtpBillDate.Text));
                                List<recieptBll> blldta = new List<recieptBll>();
                                blldta.Clear();
                                for (int i = 0; i < dgvAddedProducts.Rows.Count - 1; i++)
                                {
                                    recieptBll bdt = new recieptBll
                                    {
                                        prdctNme = Convert.ToString(dgvAddedProducts.Rows[i].Cells[0].Value),
                                        rate = Convert.ToDecimal(dgvAddedProducts.Rows[i].Cells[1].Value),
                                        qty = Convert.ToInt32(dgvAddedProducts.Rows[i].Cells[2].Value),
                                        Total = Convert.ToDecimal(dgvAddedProducts.Rows[i].Cells[3].Value)
                                    };
                                    blldta.Add(bdt);
                                }
                                rs.Name = "DataSet1";
                                rs.Value = blldta;
                                frmReceipt ft = new frmReceipt();

                                ft.reportViewer1.LocalReport.SetParameters(reportParameters);
                                ft.reportViewer1.LocalReport.DataSources.Clear();
                                ft.reportViewer1.LocalReport.DataSources.Add(rs);
                                ft.reportViewer1.LocalReport.ReportEmbeddedResource = "AnyStore.UI.Report1.rdlc";
                                ft.ShowDialog();

                                MessageBox.Show("Transaction Completed Sucessfully");
                                dgvAddedProducts.DataSource = null;
                                dgvAddedProducts.Rows.Clear();

                                txtSearchProduct.Text = "";
                                txtProductName.Text = "";
                                txtInventory.Text = "0";
                                txtRate.Text = "0";
                                TxtQty.Text = "0";
                                txtSubTotal.Text = "0";
                                txtDiscount.Text = "0";
                                txtVat.Text = "0";
                                txtGrandTotal.Text = "0";
                                txtPaidAmount.Text = "0";
                                txtReturnAmount.Text = "0";
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Transaction Failed");
                            }
                        }
                    }
                    else {
                        if (decimal.Parse(txtPaidAmount.Text) < decimal.Parse(txtGrandTotal.Text))
                        {
                            MessageBox.Show("Paid Amount is Less Than Grand Total");
                        }
                        else
                        {
                            transactionsBLL transaction = new transactionsBLL();
                            transaction.type = lblTop.Text;

                            transaction.grandTotal = Math.Round(decimal.Parse(txtGrandTotal.Text), 2);
                            transaction.transaction_date = DateTime.Now;
                            transaction.tax = Math.Round(decimal.Parse(txtVat.Text), 2);
                            transaction.discount = Math.Round(decimal.Parse(txtDiscount.Text), 2);

                            string username = frmLogin.loggedIn;
                            userBLL u = uDAL.GetIDFromUsername(username);

                            transaction.added_by = u.id;
                            transaction.transactionDetails = transactionDT;
                            bool success = false;
                            using (TransactionScope scope = new TransactionScope())
                            {
                                int transactionID = -1;
                                bool w = tDAL.Insert_Transaction(transaction, out transactionID);

                                for (int i = 0; i < transactionDT.Rows.Count; i++)
                                {
                                    transactionDetailBLL transactionDetail = new transactionDetailBLL();
                                    string ProductName = transactionDT.Rows[i][0].ToString();
                                    productsBLL p = pDAL.GetProductIDFromName(ProductName);

                                    transactionDetail.product_id = p.id;
                                    transactionDetail.rate = decimal.Parse(transactionDT.Rows[i][1].ToString());
                                    transactionDetail.qty = decimal.Parse(transactionDT.Rows[i][2].ToString());
                                    transactionDetail.total = Math.Round(decimal.Parse(transactionDT.Rows[i][3].ToString()), 2);
                                    transactionDetail.added_date = DateTime.Now;
                                    transactionDetail.added_by = u.id;

                                    string transactionType = lblTop.Text;

                                    bool y = tdDAL.InsertTransactionDetail(transactionDetail);
                                    success = w && y;
                                }

                                if (success == true)
                                {
                                    scope.Complete();

                                    ReportParameterCollection reportParameters = new ReportParameterCollection();
                                    reportParameters.Add(new ReportParameter("pTotal", txtGrandTotal.Text));
                                    reportParameters.Add(new ReportParameter("pDiscount", txtDiscount.Text));
                                    reportParameters.Add(new ReportParameter("pServiceCharge", txtVat.Text));
                                    reportParameters.Add(new ReportParameter("pPaidAmount", txtPaidAmount.Text));
                                    reportParameters.Add(new ReportParameter("pBalance", txtReturnAmount.Text));
                                    reportParameters.Add(new ReportParameter("pDate", dtpBillDate.Text));
                                    List<recieptBll> blldta = new List<recieptBll>();
                                    blldta.Clear();
                                    for (int i = 0; i < dgvAddedProducts.Rows.Count - 1; i++)
                                    {
                                        recieptBll bdt = new recieptBll
                                        {
                                            prdctNme = Convert.ToString(dgvAddedProducts.Rows[i].Cells[0].Value),
                                            rate = Convert.ToDecimal(dgvAddedProducts.Rows[i].Cells[1].Value),
                                            qty = Convert.ToInt32(dgvAddedProducts.Rows[i].Cells[2].Value),
                                            Total = Convert.ToDecimal(dgvAddedProducts.Rows[i].Cells[3].Value)
                                        };
                                        blldta.Add(bdt);
                                    }
                                    rs.Name = "DataSet1";
                                    rs.Value = blldta;
                                    frmReceipt ft = new frmReceipt();

                                    ft.reportViewer1.LocalReport.SetParameters(reportParameters);
                                    ft.reportViewer1.LocalReport.DataSources.Clear();
                                    ft.reportViewer1.LocalReport.DataSources.Add(rs);
                                    ft.reportViewer1.LocalReport.ReportEmbeddedResource = "AnyStore.UI.Report1.rdlc";
                                    ft.ShowDialog();

                                    MessageBox.Show("Transaction Completed Sucessfully");
                                    dgvAddedProducts.DataSource = null;
                                    dgvAddedProducts.Rows.Clear();

                                    txtSearchProduct.Text = "";
                                    txtProductName.Text = "";
                                    txtInventory.Text = "0";
                                    txtRate.Text = "0";
                                    TxtQty.Text = "0";
                                    txtSubTotal.Text = "0";
                                    txtDiscount.Text = "0";
                                    txtVat.Text = "0";
                                    txtGrandTotal.Text = "0";
                                    txtPaidAmount.Text = "0";
                                    txtReturnAmount.Text = "0";
                                    this.Hide();
                                }
                                else
                                {
                                    MessageBox.Show("Transaction Failed");
                                }
                            }
                        }
                    }

                    
                }
            }
            catch {
                MessageBox.Show("Error, Transaction Failed, Please Try Again");
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

        private void btnAdd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtSubTotal_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try {

                if (txtDiscount.Text == "") {
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
            catch {
                MessageBox.Show("Error");
            }
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtInventory.Text == "") {
                MessageBox.Show("Select Item To Remove First");
            }
            else {
                try
                {
                    int rowindex = dgvAddedProducts.CurrentCell.RowIndex;
                    dgvAddedProducts.Rows.RemoveAt(rowindex);
                    txtGrandTotal.Text = "";
                    txtPaidAmount.Text = "";
                    txtReturnAmount.Text = "";

                    decimal sum = decimal.Parse(txtSubTotal.Text) - decimal.Parse(txtInventory.Text);
                    txtProductName.Text = "";
                    txtRate.Text = "";
                    txtInventory.Text = "";
                    TxtQty.Text = "";
                    txtGrandTotal.Text = "";
                    txtPaidAmount.Text = "";
                    txtReturnAmount.Text = "";

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
            try
            {
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
                MessageBox.Show("Please Select Row Header");
            }
            
        }
    }
    public class BillData1
    {
        public string prdctNme { get; set; }
        public double rate { get; set; }
        public int qty { get; set; }
        public double Total { get; set; }
    }
}
