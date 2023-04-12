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
    public partial class frmEditRoomsDetails : Form
    {
        public frmEditRoomsDetails()
        {
            InitializeComponent();
        }

        EditRoomDetailsDAL dcDal = new EditRoomDetailsDAL();
        RoomTypesBLL dc = new RoomTypesBLL();
        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        private void frmEditRoomsDetails_Load(object sender, EventArgs e)
        {
            DataTable dt = dcDal.SelectRoomTypes();
            dgvDeaCust.DataSource = dt;
        }        

        private void dgvDeaCust_RowHeaderMouseClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                int rowIndex = e.RowIndex;

                txtRoomNo.Text = "";
                txtPrice.Text = "";
                txtRoomDescription.Text = "";

                txtRoomNo.Text = dgvDeaCust.Rows[rowIndex].Cells[0].Value.ToString();
                txtPrice.Text = dgvDeaCust.Rows[rowIndex].Cells[2].Value.ToString();
                txtRoomDescription.Text = dgvDeaCust.Rows[rowIndex].Cells[3].Value.ToString();
            }
            catch
            {
                MessageBox.Show("Select Row Header");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try {
                if (txtRoomNo.Text == "" || txtPrice.Text == "")
                {
                    MessageBox.Show("Fields Empty, Please Select Room And Try Again");
                }
                else
                {
                    dc.room_id = int.Parse(txtRoomNo.Text);
                    dc.description = txtRoomDescription.Text;
                    dc.price = decimal.Parse(txtPrice.Text);

                    bool success = dcDal.Update(dc);

                    if (success == true)
                    {
                        MessageBox.Show("Room Details updated Successfully");
                        Clear();
                        DataTable dt = dcDal.SelectRoomTypes();
                        dgvDeaCust.DataSource = dt;
                    }
                }
            }
            catch {
                MessageBox.Show("Failed to Udpate Room Details");
            }
        }
        public void Clear()
        {
            txtRoomNo.Text = "";
            txtPrice.Text = "";
            txtRoomDescription.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtPrice.Clear();
            txtRoomDescription.Clear();
            txtRoomNo.Clear();
        }
    }
}
