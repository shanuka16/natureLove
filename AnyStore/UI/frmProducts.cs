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
    public partial class frmProducts : Form
    {
        public frmProducts()
        {
            InitializeComponent();
        }
        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        categoriesDAL cdal = new categoriesDAL();
        productsBLL p = new productsBLL();
        productsDAL pdal = new productsDAL();
        userDAL udal = new userDAL();
        private void frmProducts_Load(object sender, EventArgs e)
        {
            DataTable categoriesDT = cdal.Select();
            cmbCategory.DataSource = categoriesDT;

            cmbCategory.DisplayMember = "title";
            cmbCategory.ValueMember = "title";

            DataTable dt = pdal.Select();
            dgvProducts.DataSource = dt;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try {
                if (txtName.Text == "" || txtRate.Text == "")
                {
                    MessageBox.Show("Fields Empty, Please Try Again");
                }
                else {
                    if (txtID.Text == "") {
                        p.name = txtName.Text;
                        p.category = cmbCategory.Text;
                        p.description = txtDescription.Text;
                        p.rate = decimal.Parse(txtRate.Text);
                        p.qty = 0;
                        p.added_date = DateTime.Now;

                        String loggedUsr = frmLogin.loggedIn;
                        userBLL usr = udal.GetIDFromUsername(loggedUsr);

                        p.added_by = usr.id;
                        bool success = pdal.Insert(p);
                        if (success == true)
                        {
                            MessageBox.Show("Product Added Successfully");
                            Clear();
                            DataTable dt = pdal.Select();
                            dgvProducts.DataSource = dt;
                        }
                    }
                    else
                    {
                        p.id = int.Parse(txtID.Text);
                        bool successChk = pdal.productCheck(p);
                        if (successChk == true)
                        {
                            p.name = txtName.Text;
                            p.category = cmbCategory.Text;
                            p.description = txtDescription.Text;
                            p.rate = decimal.Parse(txtRate.Text);
                            p.qty = 0;
                            p.added_date = DateTime.Now;

                            String loggedUsr = frmLogin.loggedIn;
                            userBLL usr = udal.GetIDFromUsername(loggedUsr);

                            p.added_by = usr.id;
                            bool success = pdal.Insert(p);
                            if (success == true)
                            {
                                MessageBox.Show("Product Added Successfully");
                                Clear();
                                DataTable dt = pdal.Select();
                                dgvProducts.DataSource = dt;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Product Already In The Database");
                        }
                    }
                }                
            }
            catch {
                MessageBox.Show("Unexpected Error, Please Try Again");
            }            
        }
        public void Clear()
        {
            txtID.Text = "";
            txtName.Text = "";
            txtDescription.Text = "";
            txtRate.Text = "";
            txtSearch.Text = "";
        }

        private void dgvProducts_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                int rowIndex = e.RowIndex;
                txtID.Text = dgvProducts.Rows[rowIndex].Cells[0].Value.ToString();
                txtName.Text = dgvProducts.Rows[rowIndex].Cells[1].Value.ToString();
                cmbCategory.Text = dgvProducts.Rows[rowIndex].Cells[2].Value.ToString();
                txtDescription.Text = dgvProducts.Rows[rowIndex].Cells[3].Value.ToString();
                txtRate.Text = dgvProducts.Rows[rowIndex].Cells[4].Value.ToString();
            }
            catch
            {
                MessageBox.Show("Select Row Header");
            }            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try {
                p.id = int.Parse(txtID.Text);
                p.name = txtName.Text;
                p.category = cmbCategory.Text;
                p.description = txtDescription.Text;
                p.rate = decimal.Parse(txtRate.Text);
                p.added_date = DateTime.Now;
                String loggedUsr = frmLogin.loggedIn;
                userBLL usr = udal.GetIDFromUsername(loggedUsr);
                p.added_by = usr.id;

                bool success = pdal.Update(p);
                if (success == true)
                {
                    MessageBox.Show("Product Successfully Updated");
                    Clear();
                    
                    DataTable dt = pdal.Select();
                    dgvProducts.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Failed to Update Product");
                }
            }
            catch {
                if (txtName.Text == "" || txtRate.Text == "")
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
                p.id = int.Parse(txtID.Text);
                bool success = pdal.Delete(p);

                if (success == true)
                {
                    MessageBox.Show("Product successfully deleted.");
                    Clear();
                    DataTable dt = pdal.Select();
                    dgvProducts.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Failed to Delete Product.");
                }
            }
            catch {
                if (txtName.Text == "" || txtRate.Text == "")
                {
                    MessageBox.Show("Fields Empty, Please Try Again");
                }
                else
                {
                    MessageBox.Show("Unexpected Error, Please Try Again");
                }
            }            
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keywords = txtSearch.Text;
            if(keywords!=null)
            {
                DataTable dt = pdal.Search(keywords);
                dgvProducts.DataSource = dt;
            }
            else
            {
                DataTable dt = pdal.Select();
                dgvProducts.DataSource = dt;
            }
        }
        private void lblSearch_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtDescription.Clear();
            txtID.Clear();
            txtName.Clear();
            txtRate.Clear();

        }
    }
}
