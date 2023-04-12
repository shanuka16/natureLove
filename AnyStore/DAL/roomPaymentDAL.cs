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
    class roomPaymentDAL
    {
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region Insert Transaction Method      

        DataTable transactionDT = new DataTable();
        public bool Insert_Transaction(roomPaymentBLL t, out int transactionID)
        {
            bool isSuccess = false;
            //Set the out transactionID value to negative 1 i.e. -1
            transactionID = -1;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                string sql = "INSERT INTO tbl_room_onee (room_no, item,rate, quantity, price, date) VALUES (@room_no, @item,@rate, @quantity, @price, @date,); SELECT @@IDENTITY;";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@room_no", t.room_no);
                cmd.Parameters.AddWithValue("@item", t.item);
                cmd.Parameters.AddWithValue("@rate", t.rate);
                cmd.Parameters.AddWithValue("@quantity", t.quantity);
                cmd.Parameters.AddWithValue("@price", t.price);
                cmd.Parameters.AddWithValue("@date", t.date);

                conn.Open();
                object o = cmd.ExecuteScalar();
                if (o != null)
                {
                    transactionID = int.Parse(o.ToString());
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
        #region Insert Method for Transaction Detail
        public bool InsertTransactionDetail(roomPaymentBLL td)
        {
            bool isSuccess = false;
            int roomNumber = td.room_no;
            string DataTable = "";
            switch (roomNumber)
            {
                case 1:
                    DataTable = "tbl_room_onee";
                break;
                case 2:
                    DataTable = "tbl_room_two";
                    break;
                case 3:
                    DataTable = "tbl_room_three";
                    break;
                case 4:
                    DataTable = "tbl_room_four";
                    break;
                case 5:
                    DataTable = "tbl_room_five";
                    break;
                case 6:
                    DataTable = "tbl_room_six";
                    break;
                case 7:
                    DataTable = "tbl_room_seven";
                    break;
                case 8:
                    DataTable = "tbl_room_eight";
                    break;
                case 9:
                    DataTable = "tbl_room_nine";
                    break;
                case 10:
                    DataTable = "tbl_room_ten";
                    break;

                default:
                break;
            }
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                string sql = "INSERT INTO "+ DataTable + " (room_no, item, rate, quantity, price, date) VALUES (@room_no, @item,@rate, @quantity, @price, @date)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                
                cmd.Parameters.AddWithValue("@room_no", td.room_no);
                cmd.Parameters.AddWithValue("@item", td.item);
                cmd.Parameters.AddWithValue("@rate", td.rate);
                cmd.Parameters.AddWithValue("@quantity", td.quantity);
                cmd.Parameters.AddWithValue("@price", td.price);
                cmd.Parameters.AddWithValue("@date", td.date);

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
        #region SELECT MEthod for Selecting Room Orders To Data Table
        public DataTable Select(int room_no)
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

            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT date, item, rate, quantity, price FROM " + TableData + "";
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
        #region SELECT MEthod for Selecting Room Details and types
        public RoomTypesBLL SearchRoomIdForTransaction(string room_no)
        {
            string roomNumber = room_no;
            RoomTypesBLL rtb = new RoomTypesBLL();
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT * from tbl_room_types WHERE room_id =" + roomNumber + "";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                conn.Open();
                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    rtb.room_id = int.Parse(dt.Rows[0]["room_id"].ToString());
                    rtb.type = dt.Rows[0]["type"].ToString();
                    rtb.price = decimal.Parse(dt.Rows[0]["price"].ToString());
                    rtb.description = dt.Rows[0]["description"].ToString();
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

            return rtb;
        }
        #endregion
    }
}
