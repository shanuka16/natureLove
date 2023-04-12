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
    public partial class frmRooms : Form
    {
        public frmRooms()
        {
            InitializeComponent();
        }
        public static string transactionType;
        public static string transactionNo;
        public static string transactionRoomName;
        roomsDAL dcDal = new roomsDAL();
        private void button2_Click(object sender, EventArgs e)
        {
            transactionType = "Room Order";
            transactionNo = "2";
            transactionRoomName = "Cabana Room 2";
            frmAddCustmrToRoom sales = new frmAddCustmrToRoom();
            sales.Show();
            this.Hide();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            transactionType = "Room Order";
            transactionNo = "1";
            transactionRoomName = "Cabana Room 1";

            frmAddCustmrToRoom sales = new frmAddCustmrToRoom();
            sales.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            transactionType = "Room Order";
            transactionNo = "3";
            transactionRoomName = "Cabana Room 3";

            frmAddCustmrToRoom sales = new frmAddCustmrToRoom();
            sales.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            transactionType = "Room Order";
            transactionNo = "4";
            transactionRoomName = "Cabana Room 4";

            frmAddCustmrToRoom sales = new frmAddCustmrToRoom();
            sales.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            transactionType = "Room Order";
            transactionNo = "5";
            transactionRoomName = "Room 5";

            frmAddCustmrToRoom sales = new frmAddCustmrToRoom();
            sales.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            transactionType = "Room Order";
            transactionNo = "6";
            transactionRoomName = "Room 6";

            frmAddCustmrToRoom sales = new frmAddCustmrToRoom();
            sales.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            transactionType = "Room Order";
            transactionNo = "7";
            transactionRoomName = "Room 7";

            frmAddCustmrToRoom sales = new frmAddCustmrToRoom();
            sales.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            transactionType = "Room Order";
            transactionNo = "8";
            transactionRoomName = "Room 8";

            frmAddCustmrToRoom sales = new frmAddCustmrToRoom();
            sales.Show();
            this.Hide();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            transactionType = "Room Order";
            transactionNo = "9";
            transactionRoomName = "Room 9";

            frmAddCustmrToRoom sales = new frmAddCustmrToRoom();
            sales.Show();
            this.Hide();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            transactionType = "Room Order";
            transactionNo = "10";
            transactionRoomName = "Room 10";

            frmAddCustmrToRoom sales = new frmAddCustmrToRoom();
            sales.Show();
            this.Hide();
        }

        private void frmRooms_Load(object sender, EventArgs e)
        {
            DataTable dt = dcDal.Select();
            dgvDeaCust.DataSource = dt;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.ToString();

            if (keyword != null)
            {
                DataTable dt = dcDal.Search(keyword);
                dgvDeaCust.DataSource = dt;
            }
            else
            {
                DataTable dt = dcDal.Select();
                dgvDeaCust.DataSource = dt;
            }
        }
    }
}
