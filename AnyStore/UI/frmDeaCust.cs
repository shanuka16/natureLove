﻿using AnyStore.BLL;
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
    public partial class frmDeaCust : Form
    {
        public frmDeaCust()
        {
            InitializeComponent();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        DeaCustBLL dc = new DeaCustBLL();
        DeaCustDAL dcDal = new DeaCustDAL();

        userDAL uDal = new userDAL();
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try {
                if (txtName.Text == "" || txtEmail.Text == "" || txtContact.Text == "" )
                {
                    MessageBox.Show("Fields Empty, Please Try Again");
                }
                else
                {
                    dc.type = cmbDeaCust.Text;
                    dc.name = txtName.Text;
                    dc.email = txtEmail.Text;
                    dc.contact = txtContact.Text;
                    dc.address = txtAddress.Text;
                    dc.added_date = DateTime.Now;
                    
                    string loggedUsr = frmLogin.loggedIn;
                    userBLL usr = uDal.GetIDFromUsername(loggedUsr);
                    dc.added_by = usr.id;
                    bool success = dcDal.Insert(dc);

                    if (success == true)
                    {
                        MessageBox.Show("Customer Added Successfully");
                        Clear();
                        DataTable dt = dcDal.Select();
                        dgvDeaCust.DataSource = dt;
                    }                    
                }
            }
            catch {
                if (txtName.Text == "" || txtEmail.Text == "" || txtContact.Text == "" )
                {
                    MessageBox.Show("Fields Empty, Please Try Again");
                }
                else
                {
                    MessageBox.Show("Unexpected Error, Please Try Again");
                }
            }            
        }
        public void Clear()
        {
            txtDeaCustID.Text = "";
            txtName.Text = "";
            txtEmail.Text = "";
            txtContact.Text = "";
            txtAddress.Text = "";
            txtSearch.Text = "";
        }

        private void frmDeaCust_Load(object sender, EventArgs e)
        {
            DataTable dt = dcDal.Select();
            dgvDeaCust.DataSource = dt;
        }

        private void dgvDeaCust_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try {
                int rowIndex = e.RowIndex;

                txtDeaCustID.Text = dgvDeaCust.Rows[rowIndex].Cells[0].Value.ToString();
                cmbDeaCust.Text = dgvDeaCust.Rows[rowIndex].Cells[1].Value.ToString();
                txtName.Text = dgvDeaCust.Rows[rowIndex].Cells[2].Value.ToString();
                txtEmail.Text = dgvDeaCust.Rows[rowIndex].Cells[3].Value.ToString();
                txtContact.Text = dgvDeaCust.Rows[rowIndex].Cells[4].Value.ToString();
                txtAddress.Text = dgvDeaCust.Rows[rowIndex].Cells[5].Value.ToString();
            }
            catch
            {
                MessageBox.Show("Select Row Header");
            }            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try {
                dc.id = int.Parse(txtDeaCustID.Text);
                dc.type = cmbDeaCust.Text;
                dc.name = txtName.Text;
                dc.email = txtEmail.Text;
                dc.contact = txtContact.Text;
                dc.address = txtAddress.Text;
                dc.added_date = DateTime.Now;
                
                string loggedUsr = frmLogin.loggedIn;
                userBLL usr = uDal.GetIDFromUsername(loggedUsr);
                dc.added_by = usr.id;

                bool success = dcDal.Update(dc);

                if (success == true)
                {
                    MessageBox.Show("Customer Details updated Successfully");
                    Clear();
                    DataTable dt = dcDal.Select();
                    dgvDeaCust.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Failed to Udpate Customer Details");
                }
            }
            catch {
                if (txtName.Text == "" || txtEmail.Text == "" || txtContact.Text == "" || txtAddress.Text == "" )
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
                dc.id = int.Parse(txtDeaCustID.Text);
                bool success = dcDal.Delete(dc);

                if (success == true)
                {
                    MessageBox.Show("Customer Deleted Successfully");
                    Clear();
                    DataTable dt = dcDal.Select();
                    dgvDeaCust.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Failed to Delete Customer");
                }
            }
            catch {
                MessageBox.Show("Failed to Delete Customer");
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text;

            if(keyword!=null)
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

        private void button1_Click(object sender, EventArgs e)
        {
            txtAddress.Clear();
            txtContact.Clear();
            txtDeaCustID.Clear();
            txtEmail.Clear();
            txtName.Clear();
        }
    }
}
