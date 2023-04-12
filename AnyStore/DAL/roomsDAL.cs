using AnyStore.BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnyStore.DAL
{ 
    class roomsDAL
    {
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
        
       
        #region Select Data from Database
        public DataTable Select()
        {
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                String sql = "SELECT * FROM tbl_rooms";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }
        #endregion
        #region Insert Data in Database
        public bool Insert(roomsBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                String sql = "INSERT INTO tbl_rooms " + "(room_id, Id_Passport, Cust_name, added_date, room_type, no_of_heads, contact, email, address, country, room_status, added_by) VALUES " + "(@room_id, @Id_Passport, @Cust_name, @added_date, @room_type, @no_of_heads, @contact, @email, @address, @country, @room_status, @added_by)"; 
                 SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@room_id", u.room_id);
                cmd.Parameters.AddWithValue("@Id_Passport", u.Id_Passport);
                cmd.Parameters.AddWithValue("@Cust_name", u.Cust_name);
                cmd.Parameters.AddWithValue("@added_date", u.added_date);
                cmd.Parameters.AddWithValue("@room_type", u.room_type);
                cmd.Parameters.AddWithValue("@no_of_heads", u.no_of_heads);
                cmd.Parameters.AddWithValue("@contact", u.contact);
                cmd.Parameters.AddWithValue("@address", u.address);
                cmd.Parameters.AddWithValue("@country", u.country);
                cmd.Parameters.AddWithValue("@email", u.email);
                cmd.Parameters.AddWithValue("@room_status", u.room_status);
                cmd.Parameters.AddWithValue("@added_by", u.added_by);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
        #endregion
        #region Check user already in Database
        public bool userCheck(roomsBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                string sql = "Select * from tbl_rooms WHERE room_id=@room_id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@room_id", u.room_id);
                conn.Open();

                int TotalRows = 0;
                TotalRows = Convert.ToInt32(cmd.ExecuteScalar());
                if (TotalRows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
        #endregion
        #region Check product already in Database
        public bool productCheck(productsBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                string sql = "Select * from tbl_products WHERE name=@name";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", u.id);
                conn.Open();

                int TotalRows = 0;
                TotalRows = Convert.ToInt32(cmd.ExecuteScalar());
                if (TotalRows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
        #endregion
        #region Check room already in use
        public bool roomCheck(roomsBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                string sql = "Select * from tbl_rooms WHERE room_id=@room_id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@room_id", u.room_id);
                conn.Open();

                int TotalRows = 0;
                TotalRows = Convert.ToInt32(cmd.ExecuteScalar());
                if (TotalRows > 0)
                {
                    isSuccess = false;
                }
                else
                {
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
        #endregion
        #region Update data in Database
        public bool Update(roomsBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                string sql = "UPDATE tbl_rooms SET room_id=@room_id, Id_Passport=@Id_Passport, Cust_name=@Cust_name, added_date=@added_date, room_type=@room_type, no_of_heads=@no_of_heads, contact=@contact, address=@address, country=@country, email=@email, room_status=@room_status, added_by=@added_by WHERE room_id=@room_id";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@room_id", u.room_id);
                cmd.Parameters.AddWithValue("@Id_Passport", u.Id_Passport);
                cmd.Parameters.AddWithValue("@Cust_name", u.Cust_name);
                cmd.Parameters.AddWithValue("@added_date", u.added_date);
                cmd.Parameters.AddWithValue("@room_type", u.room_type);
                cmd.Parameters.AddWithValue("@no_of_heads", u.no_of_heads);
                cmd.Parameters.AddWithValue("@contact", u.contact);
                cmd.Parameters.AddWithValue("@address", u.address);
                cmd.Parameters.AddWithValue("@country", u.country);
                cmd.Parameters.AddWithValue("@email", u.email);
                cmd.Parameters.AddWithValue("@room_status", u.room_status);
                cmd.Parameters.AddWithValue("@added_by", u.added_by);
                


                conn.Open();

                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
        #endregion
        #region Delete Data from DAtabase
        public bool Delete(roomsBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                string sql = "DELETE FROM tbl_rooms WHERE room_id=@room_id";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@room_id", u.room_id);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
        #endregion
        #region METHOD TO GET ID OF THE DEALER OR CUSTOMER BASED ON NAME
        public roomsBLL GetDeaCustIDFromName(string Name)
        {
            roomsBLL dc = new roomsBLL();
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT room_id FROM tbl_rooms WHERE name='" + Name + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                conn.Open();

                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    dc.room_id = int.Parse(dt.Rows[0]["id"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return dc;
        }
        #endregion
        #region METHOD TO SAERCH DEALER Or CUSTOMER FOR TRANSACTON MODULE
        public roomsBLL SearchDealerCustomerForTransaction(string keyword)
        {
            roomsBLL dc = new roomsBLL();
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT Cust_name, email, contact, address, added_date from tbl_rooms WHERE room_id LIKE '%" + keyword + "%' OR Cust_name LIKE '%" + keyword + "%'";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                conn.Open();
                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    dc.Cust_name = dt.Rows[0]["Cust_name"].ToString();
                    dc.email = dt.Rows[0]["email"].ToString();
                    dc.contact = dt.Rows[0]["contact"].ToString();
                    dc.address = dt.Rows[0]["address"].ToString();
                    dc.added_date = DateTime.Parse(dt.Rows[0]["added_date"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return dc;
        }
        #endregion
        #region DELETE Customer From Room After Checkout
        public void CheckoutUserRemove(int room_no)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);
            int roomNumber = room_no;
            string TableData = "";
            switch (roomNumber)
            {
                case 1:
                    TableData = "tbl_room_onee";
                    break;
                case 2:
                    TableData = "tbl_room_two";
                    break;
                case 3:
                    TableData = "tbl_room_three";
                    break;
                case 4:
                    TableData = "tbl_room_four";
                    break;
                case 5:
                    TableData = "tbl_room_five";
                    break;
                case 6:
                    TableData = "tbl_room_six";
                    break;
                case 7:
                    TableData = "tbl_room_seven";
                    break;
                case 8:
                    TableData = "tbl_room_eight";
                    break;
                case 9:
                    TableData = "tbl_room_nine";
                    break;
                case 10:
                    TableData = "tbl_room_ten";
                    break;

                default:
                    break;
            }            
            try
            {
                string sql = "DELETE FROM " + TableData + "";                
                SqlCommand cmd = new SqlCommand(sql, conn);

                string sql1 = "DELETE FROM tbl_rooms WHERE room_id = " + room_no + "";
                SqlCommand cmd1 = new SqlCommand(sql1, conn);

                conn.Open();
                cmd.ExecuteNonQuery();
                cmd1.ExecuteNonQuery();
                MessageBox.Show("User Deleted Successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }


        }
        #endregion
        #region METHOD TO SAERCH CUSTOMER FOR Add Customer To Room MODULE
        public roomsBLL SearchDealerCustomerForRoom(string keyword)
        {
            roomsBLL dc = new roomsBLL();
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT Cust_name, email, contact, address, no_of_heads, country, Id_Passport from tbl_rooms WHERE room_id LIKE '%" + keyword + "%'";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                conn.Open();
                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    dc.Cust_name = dt.Rows[0]["Cust_name"].ToString();
                    dc.email = dt.Rows[0]["email"].ToString();
                    dc.contact = dt.Rows[0]["contact"].ToString();
                    dc.address = dt.Rows[0]["address"].ToString();
                    dc.no_of_heads = int.Parse(dt.Rows[0]["no_of_heads"].ToString());
                    dc.country = dt.Rows[0]["country"].ToString();
                    dc.Id_Passport = dt.Rows[0]["Id_Passport"].ToString();
                }
            }
            catch (Exception ex)
            {
                if (dt.Rows[0]["Cust_name"].ToString() == "") {
                    MessageBox.Show("Room Not Assigned");
                }
                
            }
            finally
            {
                conn.Close();
            }
            return dc;
        }
        #endregion
        #region SEARCH METHOD for Rooms,  Customer details Module
        public DataTable Search(string keyword)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT * FROM tbl_rooms WHERE Id_Passport LIKE '%" + keyword + "%' OR Cust_name LIKE '%" + keyword + "%' OR room_id LIKE '%" + keyword + "%'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }
        #endregion
    }
}
