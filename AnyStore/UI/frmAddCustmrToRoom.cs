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
    public partial class frmAddCustmrToRoom : Form
    {
        public frmAddCustmrToRoom()
        {
            InitializeComponent();
        }
        public static string transactionType;
        public static string transactionNo;

        roomsBLL dc = new roomsBLL();
        roomsDAL dcDal = new roomsDAL();
        userDAL uDal = new userDAL();
        DeaCustDAL r = new DeaCustDAL();
        DeaCustBLL rr = new DeaCustBLL();

        private void frmAddCustmrToRoom_Load(object sender, EventArgs e)
        {
            string type = frmRooms.transactionType;
            string roomId = frmRooms.transactionNo; 
            string roomName = frmRooms.transactionRoomName;
            
            lblTop.Text = type;
            txtRoomId.Text = roomId;
            cmbType.Text = frmRooms.transactionRoomName;
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            frmRooms sales = new frmRooms();
            sales.Show();
            this.Hide();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("Please Enter Name");
            }
            else if (txtEmail.Text == "")
            {
                MessageBox.Show("Please Enter Email");
            }
            else if (txtContact.Text == "")
            {
                MessageBox.Show("Please Enter Contact");
            }
            else if (txtCountry.Text == "")
            {
                MessageBox.Show("Please Enter Country");
            }
            else if (txtAddress.Text == "")
            {
                MessageBox.Show("Please Enter Address");
            }
            else if (txtIdPassport.Text == "")
            {
                MessageBox.Show("Please Enter Passpord Or ID");
            }
            else
            {
                try
                {
                    dc.room_id = int.Parse(txtRoomId.Text);


                    bool success = dcDal.roomCheck(dc);
                    if (success == true)
                    {
                        dc.Id_Passport = txtIdPassport.Text;
                        dc.Cust_name = txtName.Text;
                        dc.added_date = DateTime.Now;
                        dc.room_type = cmbType.Text;
                        dc.no_of_heads = int.Parse(txtNoOfHeads.Text);
                        dc.email = txtEmail.Text;
                        dc.contact = txtContact.Text;
                        dc.country = txtCountry.Text;
                        dc.address = txtAddress.Text;
                        dc.room_status = 1;
                        //Getting the ID to Logged in user and passign its value in dealer or cutomer module
                        string loggedUsr = frmLogin.loggedIn;
                        userBLL usr = uDal.GetIDFromUsername(loggedUsr);
                        dc.added_by = usr.id.ToString();

                        bool Insertsuccess = dcDal.Insert(dc);
                        if (Insertsuccess == true)
                        {
                            MessageBox.Show("Customer added to room");

                            rr.type = lblTop.Text;
                            rr.name = txtName.Text;
                            rr.email = txtEmail.Text;
                            rr.contact = txtContact.Text;
                            rr.address = txtAddress.Text;
                            rr.added_by = usr.id;
                            rr.added_date = DateTime.Now;
                            r.Insert(rr);

                            Clear();
                            this.Hide();
                            frmRooms frm = new frmRooms();
                            frm.Show();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Room Already Full");
                    }

                }
                catch
                {
                    if (txtIdPassport.Text == "" || txtName.Text == "" || txtEmail.Text == "" || txtContact.Text == "" || txtAddress.Text == "" ||
                        txtCountry.Text == "" || cmbType.Text == "" || txtNoOfHeads.Text == "")
                    {
                        MessageBox.Show("Fields Empty, Please Try Again");
                    }
                    else
                    {
                        MessageBox.Show("Unexpected Error, Please Try Again");
                    }
                }
            }
               
        }
        public void Clear()
        {
            txtIdPassport.Text = "";
            txtName.Text = "";
            txtEmail.Text = "";
            txtContact.Text = "";
            txtAddress.Text = "";
            txtCountry.Text = "";
            cmbType.Text = "";
            txtNoOfHeads.Text = "";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try {
                dc.room_id = int.Parse(txtRoomId.Text);
                dc.Id_Passport = txtIdPassport.Text;
                dc.Cust_name = txtName.Text;
                dc.added_date = DateTime.Now;
                dc.room_type = cmbType.Text;
                dc.no_of_heads = int.Parse(txtNoOfHeads.Text);
                dc.email = txtEmail.Text;
                dc.contact = txtContact.Text;
                dc.country = txtCountry.Text;
                dc.address = txtAddress.Text;
                dc.room_status = 1;
                //Getting the ID to Logged in user and passign its value in dealer or cutomer module
                string loggedUsr = frmLogin.loggedIn;
                userBLL usr = uDal.GetIDFromUsername(loggedUsr);
                dc.added_by = usr.id.ToString();

                bool success = dcDal.Update(dc);
                if (success == true)
                {
                    MessageBox.Show("Customer Details updated Successfully");
                    Clear();
                    frmRooms frm = new frmRooms();
                    frm.Show();
                }
            }
            catch {
                if (txtIdPassport.Text == "" || txtName.Text == "" || txtEmail.Text == "" || txtContact.Text == "" || txtAddress.Text == "" ||
                    txtCountry.Text == "" || cmbType.Text == "" || txtNoOfHeads.Text == "")
                {
                    MessageBox.Show("Fields Empty, Please Try Again");
                }
                else
                {
                    MessageBox.Show("Unexpected Error, Please Try Again");
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            /*
            try {
                //add messagebox here
                DialogResult dlgResult = MessageBox.Show("Remove Customer From Room, All Data Will Be Removed", "Delete User", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dlgResult == DialogResult.Yes)
                {
                    dc.room_id = int.Parse(txtRoomId.Text);
                    dcDal.CheckoutUserRemove(dc.room_id);
                }
                else
                {
                    
                }


                
               
                frmRooms frm = new frmRooms();
                frm.Show();
                this.Hide();
            }
            catch {
                if (txtIdPassport.Text == "" || txtName.Text == "" || txtEmail.Text == "" || txtContact.Text == "" || txtAddress.Text == "" ||
                        txtCountry.Text == "" || cmbType.Text == "" || txtNoOfHeads.Text == "")
                {
                    MessageBox.Show("Select Customer and Try Again");
                }
                else
                {
                    MessageBox.Show("Unexpected Error, Please Try Again");
                }
            }
            */
        }
        
        private void addBill_Click(object sender, EventArgs e)
        {
            try {
                dc.room_id = int.Parse(txtRoomId.Text);
                string type = frmRooms.transactionType;
                string roomId = frmRooms.transactionNo;
                transactionType = "Room Order";
                transactionNo = roomId;

                bool success = dcDal.userCheck(dc);
                if (success == true)
                {
                    frmRoomPayment rooms = new frmRoomPayment();
                    rooms.Show();
                }
                else
                {
                    MessageBox.Show("Add Customer To Room First");
                }                
            }
            catch {
                MessageBox.Show("Unexpected Error");
            }
            
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            try
            {
                dc.room_id = int.Parse(txtRoomId.Text);
                string type = frmRooms.transactionType;
                string roomId = frmRooms.transactionNo;
                transactionType = "Room Order";
                transactionNo = roomId;

                bool success = dcDal.userCheck(dc);
                if (success == true)
                {
                    frmCheckOut rooms = new frmCheckOut();
                    rooms.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Add Customer To Room First");                    
                }                
            }
            catch
            {
                MessageBox.Show("Add Customer To Room First");
            }
           
        }

        private void txtContact_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
            (e.KeyChar != '.'))
            {
                e.Handled = true;
            }            
        }

        private void txtNoOfHeads_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
            (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtRoomId_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtRoomId.Text ;
            if (keyword == "")
            {
                txtName.Text = "";
                txtEmail.Text = "";
                txtContact.Text = "";
                txtAddress.Text = "";
                txtIdPassport.Text = "";
                txtCountry.Text = "";
                btnCheckOut.Text = "";
                return;
            }
            roomsBLL dc = dcDal.SearchDealerCustomerForRoom(keyword);
            try {
                txtName.Text = dc.Cust_name;
                txtEmail.Text = dc.email;
                txtContact.Text = dc.contact.ToString();
                txtAddress.Text = dc.address;
                txtIdPassport.Text = dc.Id_Passport;
                txtCountry.Text = dc.country;
                txtNoOfHeads.Text = dc.no_of_heads.ToString();
            }
            catch {
                if (dc.Cust_name == "")
                {
                    MessageBox.Show("Room Not Assigned, Assign New Customer");
                }
            }            
        }
    }
}
