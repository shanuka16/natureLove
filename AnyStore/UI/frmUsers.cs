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
    public partial class frmUsers : Form
    {
        public frmUsers()
        {
            InitializeComponent();
        }

        userBLL u = new userBLL();
        userDAL dal = new userDAL();
        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try {
                if (txtFirstName.Text == "" || txtLastName.Text == "" || txtEmail.Text == "" || txtUsername.Text == "" || txtPassword.Text == "" ||
                            txtContact.Text == "" || txtAddress.Text == "" || cmbGender.Text == "" || cmbUserType.Text == "")
                {
                    MessageBox.Show("Fields are Empty, Please Try Again");
                }
                else
                {
                    if (txtUserID.Text == "")
                    {
                        u.username = txtUsername.Text;
                        bool successChk = dal.userCheck(u);
                        if (successChk == true)
                        {
                            u.first_name = txtFirstName.Text;
                            u.last_name = txtLastName.Text;
                            u.email = txtEmail.Text;

                            u.password = txtPassword.Text;
                            u.contact = txtContact.Text;
                            u.address = txtAddress.Text;
                            u.gender = cmbGender.Text;
                            u.user_type = cmbUserType.Text;
                            u.added_date = DateTime.Now;

                            string loggedUser = frmLogin.loggedIn;
                            userBLL usr = dal.GetIDFromUsername(loggedUser);
                            u.added_by = usr.id;
                            bool success = dal.Insert(u);
                            if (success == true)
                            {
                                MessageBox.Show("User successfully created.");
                                clear();
                            }
                            else
                            {
                                MessageBox.Show("Failed to add new user");
                            }
                            DataTable dt = dal.Select();
                            dgvUsers.DataSource = dt;
                        }
                        else
                        {
                            MessageBox.Show("User Name Already Taken, Try A Different Username");
                        }
                    }
                    else
                    {
                        MessageBox.Show("User Already In The Database");
                    }
                }
            }
            catch {
                if (txtFirstName.Text == "" || txtLastName.Text == "" || txtEmail.Text == "" || txtUsername.Text == "" ||txtPassword.Text == "" ||
                        txtContact.Text == "" || txtAddress.Text == "" || cmbGender.Text == "" || cmbUserType.Text == "")
                {
                    MessageBox.Show("Fields Empty, Please Try Again");
                }
                else
                {
                    MessageBox.Show("Unexpected Error, Please Try Again");
                }
            }
        }

        private void frmUsers_Load(object sender, EventArgs e)
        {
            DataTable dt = dal.Select();
            dgvUsers.DataSource = dt;
        }
        private void clear()
        {
            txtUserID.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtEmail.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtContact.Text = "";
            txtAddress.Text = "";
            cmbGender.Text = "";
            cmbUserType.Text = "";
        }

        private void dgvUsers_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                int rowIndex = e.RowIndex;
                txtUserID.Text = dgvUsers.Rows[rowIndex].Cells[0].Value.ToString();
                txtFirstName.Text = dgvUsers.Rows[rowIndex].Cells[1].Value.ToString();
                txtLastName.Text = dgvUsers.Rows[rowIndex].Cells[2].Value.ToString();
                txtEmail.Text = dgvUsers.Rows[rowIndex].Cells[3].Value.ToString();
                txtUsername.Text = dgvUsers.Rows[rowIndex].Cells[4].Value.ToString();
                txtPassword.Text = dgvUsers.Rows[rowIndex].Cells[5].Value.ToString();
                txtContact.Text = dgvUsers.Rows[rowIndex].Cells[6].Value.ToString();
                txtAddress.Text = dgvUsers.Rows[rowIndex].Cells[7].Value.ToString();
                cmbGender.Text = dgvUsers.Rows[rowIndex].Cells[8].Value.ToString();
                cmbUserType.Text = dgvUsers.Rows[rowIndex].Cells[9].Value.ToString();
            }
            catch
            {
                MessageBox.Show("Select Row Header");
            }            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try {
                u.id = Convert.ToInt32(txtUserID.Text);
                u.first_name = txtFirstName.Text;
                u.last_name = txtLastName.Text;
                u.email = txtEmail.Text;
                u.username = txtUsername.Text;
                u.password = txtPassword.Text;
                u.contact = txtContact.Text;
                u.address = txtAddress.Text;
                u.gender = cmbGender.Text;
                u.user_type = cmbUserType.Text;
                u.added_date = DateTime.Now;
                u.added_by = 1;

                bool successChk = dal.userUpdateCheck(u);
                if (successChk == true)
                {
                    bool success = dal.Update(u);
                    if (success == true)
                    {
                        MessageBox.Show("User successfully updated");
                        clear();
                    }
                    else
                    {
                        MessageBox.Show("Failed to update user");
                    }
                    DataTable dt = dal.Select();
                    dgvUsers.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("User Name Already Taken, Try A Different Username");
                }
            }
            catch {
                if (txtFirstName.Text == "" || txtLastName.Text == "" || txtEmail.Text == "" || txtUsername.Text == "" || txtPassword.Text == "" ||
                            txtContact.Text == "" || txtAddress.Text == "" || cmbGender.Text == "" || cmbUserType.Text == "")
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
            try {
                u.id = Convert.ToInt32(txtUserID.Text);

                bool success = dal.Delete(u);
                if (success == true)
                {
                    MessageBox.Show("User deleted successfully");
                    clear();
                }
                else
                {
                    MessageBox.Show("Failed to delete user");
                }
                DataTable dt = dal.Select();
                dgvUsers.DataSource = dt;
            }
            catch {                
                 MessageBox.Show("Unexpected Error, Please Try Again");
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keywords = txtSearch.Text;

            if(keywords!=null)
            {
                DataTable dt = dal.Search(keywords);
                dgvUsers.DataSource = dt;
            }
            else
            {
                DataTable dt = dal.Select();
                dgvUsers.DataSource = dt;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtAddress.Clear();
            txtContact.Clear();
            txtEmail.Clear();
            txtFirstName.Clear();
            txtLastName.Clear();
            txtPassword.Clear();
            txtUserID.Clear();
            txtUsername.Clear();
            cmbGender.Text = "";
            cmbUserType.Text = "";
        }
    }
}
