using AnyStore.BLL;
using AnyStore.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnyStore.UI
{
    public partial class frmStatistics : Form
    {
        public frmStatistics()
        {
            InitializeComponent();
        }
        transactionDAL tdal = new transactionDAL();
        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void frmStatistics_Load(object sender, EventArgs e)
        {
            DateTime tody = DateTime.Today;
            label1.Text = tody.ToShortDateString();

            DataTable dt = tdal.DisplayAllTransactionsDate();
            dgvTransactions.DataSource = dt;

            
            DataTable dtAll = tdal.DisplayAllTimeTransactions();
            dgvAllSales.DataSource = dtAll;

            decimal sum = 0;
            for (int i = 0; i < dgvTransactions.Rows.Count; i++)
            {
                sum += Convert.ToInt32(dgvTransactions.Rows[i].Cells[3].Value);
            }
            txtTotal.Text = sum.ToString();

            lbl1.Text = DateTime.Today.AddDays(-1).ToShortDateString();            
            transactionsBLL dc = tdal.DisplayAllTransactionsYesterday(-1);
            decimal dayOne= decimal.Parse(dc.grandTotal.ToString()) - decimal.Parse(txtTotal.Text.ToString());
            TxtYestrdyTotal.Text = dayOne.ToString();

            lbl2.Text = DateTime.Today.AddDays(-2).ToShortDateString();
            transactionsBLL dc2 = tdal.DisplayAllTransactionsYesterday(-2);
            decimal dayTwo = decimal.Parse(dc2.grandTotal.ToString()) - decimal.Parse(txtTotal.Text.ToString()) - (dayOne );
            TxtTotalTwo.Text = dayTwo.ToString();

            lbl3.Text = DateTime.Today.AddDays(-3).ToShortDateString();
            transactionsBLL dc3 = tdal.DisplayAllTransactionsYesterday(-3);
            decimal dayThree = decimal.Parse(dc3.grandTotal.ToString()) - decimal.Parse(txtTotal.Text.ToString())- (dayOne+ dayTwo);
            TxtTotalThree.Text = dayThree.ToString();

            lbl4.Text = DateTime.Today.AddDays(-4).ToShortDateString();
            transactionsBLL dc4 = tdal.DisplayAllTransactionsYesterday(-4);
            decimal dayFour = decimal.Parse(dc4.grandTotal.ToString()) - decimal.Parse(txtTotal.Text.ToString())- (dayOne + dayTwo + dayThree);
            TxtTotalFour.Text = dayFour.ToString();

            lbl5.Text = DateTime.Today.AddDays(-5).ToShortDateString();
            transactionsBLL dc5 = tdal.DisplayAllTransactionsYesterday(-5);
            decimal dayFive = decimal.Parse(dc5.grandTotal.ToString()) - decimal.Parse(txtTotal.Text.ToString()) - (dayOne + dayTwo + dayThree + dayFour);
            TxtTotalFive.Text = dayFive.ToString();

            lbl6.Text = DateTime.Today.AddDays(-6).ToShortDateString();
            transactionsBLL dc6 = tdal.DisplayAllTransactionsYesterday(-6);
            decimal daySix = decimal.Parse(dc6.grandTotal.ToString()) - decimal.Parse(txtTotal.Text.ToString()) - (dayOne + dayTwo+dayThree+dayFour+dayFive);
            TxtTotalSix.Text = daySix.ToString();

            lbl6.Text = DateTime.Today.AddDays(-7).ToShortDateString();
            transactionsBLL dc7 = tdal.DisplayAllTransactionsYesterday(-6);
            decimal daySeven = decimal.Parse(dc7.grandTotal.ToString()) - decimal.Parse(txtTotal.Text.ToString()) - (dayOne + dayTwo+dayThree+dayFour+dayFive+ daySix);
            TxtTotalSix.Text = daySeven.ToString();

        }
    }
}
