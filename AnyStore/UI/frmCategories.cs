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
    public partial class frmCategories : Form
    {
        public frmCategories()
        {
            InitializeComponent();
        }

        categoriesBLL c = new categoriesBLL();
        categoriesDAL dal = new categoriesDAL();
        userDAL udal = new userDAL();

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        
        private void btnADD_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCategoryID.Text == "")
                {
                    c.title = txtTitle.Text;
                    c.description = txtDescription.Text;
                    c.added_date = DateTime.Now;

                    if (txtTitle.Text == "")
                    {
                        MessageBox.Show("Enter Item Title ");
                    }
                    else
                    {
                        string loggedUser = frmLogin.loggedIn;
                        userBLL usr = udal.GetIDFromUsername(loggedUser);
                        c.added_by = usr.id;
                        bool success = dal.Insert(c);
                        if (success == true)
                        {
                            MessageBox.Show("New Category Inserted Successfully.");
                            Clear();
                            DataTable dt = dal.Select();
                            dgvCategories.DataSource = dt;
                        }
                        else
                        {
                            MessageBox.Show("Failed to Insert New CAtegory.");
                        }
                    }
                }
                else
                {
                    c.id = int.Parse(txtCategoryID.Text);
                    bool successChk = dal.categoryCheck(c);
                    if (successChk == true) {
                        c.title = txtTitle.Text;
                        c.description = txtDescription.Text;
                        c.added_date = DateTime.Now;

                        if (txtTitle.Text == "")
                        {
                            MessageBox.Show("Enter Item Title ");
                        }
                        else
                        {
                            string loggedUser = frmLogin.loggedIn;
                            userBLL usr = udal.GetIDFromUsername(loggedUser);
                            c.added_by = usr.id;
                            bool success = dal.Insert(c);
                            if (success == true)
                            {
                                MessageBox.Show("New Category Inserted Successfully.");
                                Clear();
                                DataTable dt = dal.Select();
                                dgvCategories.DataSource = dt;
                            }
                            else
                            {
                                MessageBox.Show("Failed to Insert New CAtegory.");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Item Already In The Database ");
                    }
                }                
            }
            catch {
                if (txtTitle.Text == "")
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
            txtCategoryID.Text = "";
            txtTitle.Text = "";
            txtDescription.Text = "";
            txtSearch.Text = "";
        }

        private void frmCategories_Load(object sender, EventArgs e)
        {
            DataTable dt = dal.Select();
            dgvCategories.DataSource = dt;
        }

        private void dgvCategories_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try {
                int RowIndex = e.RowIndex;
                txtCategoryID.Text = dgvCategories.Rows[RowIndex].Cells[0].Value.ToString();
                txtTitle.Text = dgvCategories.Rows[RowIndex].Cells[1].Value.ToString();
                txtDescription.Text = dgvCategories.Rows[RowIndex].Cells[2].Value.ToString();
            }
            catch
            {
                MessageBox.Show("Select Row Header");
            }            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try {
                c.id = int.Parse(txtCategoryID.Text);
                c.title = txtTitle.Text;
                c.description = txtDescription.Text;
                c.added_date = DateTime.Now;
                string loggedUser = frmLogin.loggedIn;
                userBLL usr = udal.GetIDFromUsername(loggedUser);
                c.added_by = usr.id;

                bool success = dal.Update(c);
                if (success == true)
                {
                    MessageBox.Show("Category Updated Successfully");
                    Clear();
                    DataTable dt = dal.Select();
                    dgvCategories.DataSource = dt;
                }
                
            }
            catch {
                if (txtTitle.Text == "" )
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
                c.id = int.Parse(txtCategoryID.Text);
                bool success = dal.Delete(c);

                if (success == true)
                {
                    MessageBox.Show("Category Deleted Successfully");
                    Clear();
                    DataTable dt = dal.Select();
                    dgvCategories.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Failed to Delete CAtegory");
                }
            }
            catch {
                if (txtTitle.Text == "" )
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
                DataTable dt = dal.Search(keywords);
                dgvCategories.DataSource = dt;
            }
            else
            {
                DataTable dt = dal.Select();
                dgvCategories.DataSource = dt;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtCategoryID.Clear();
            txtDescription.Clear();
            txtTitle.Clear();
        }
    }
}
