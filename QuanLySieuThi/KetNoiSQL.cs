using Microsoft.Data.SqlClient;
using System.Data;
using System.Windows;
using System.Windows.Controls;




namespace QuanLySieuThi
{
    public class KetNoiSQL
    {
        public static string sqlCon = @"Server=LAPTOP-OR8R0503\TESTDB;Database=QLSieuThi;User Id=sa;Password=Pasword1234;Trusted_Connection=True;TrustServerCertificate=True;";
        public static SqlConnection myCon = new SqlConnection(sqlCon);

        public static SqlCommand sqlCom;
        public static SqlDataAdapter sqlADT;
        public static DataTable dt;

        public static SqlCommandBuilder sqlCB;
        public static SqlDataReader Showtext(string sql)
        {
            SqlDataReader read = null;
            try
            {
                myCon = new SqlConnection(sqlCon);
                myCon.Open();
                sqlCom = new SqlCommand(sql, myCon);
                read = sqlCom.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối!\n" + ex.Message);
            }
            return read;
        }

        public static void ChuoiKetNoi(string chuoi, DataGrid dtgv)
        {
            try
            {
                sqlADT = new SqlDataAdapter(chuoi, sqlCon);
                dt = new DataTable();
                sqlCB = new SqlCommandBuilder(sqlADT);
                sqlADT.Fill(dt);
                dtgv.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể kết nối " + ex, "Thông báo!");
            }
        }

        public static void LamMoi(string sql, DataGrid dtgv)
        {
            try
            {
                sqlADT = new SqlDataAdapter(sql, myCon);
                sqlCB = new SqlCommandBuilder(sqlADT);
                DataTable dt = new DataTable();
                sqlADT.Fill(dt);
                dtgv.ItemsSource = dt.DefaultView;
                MessageBox.Show("Làm mới thành công! ", "Thông báo ");
                myCon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "" + ex);
            }
        }

        public static void ClickDTGV(string sql, DataGrid dtgv)
        {
            try
            {
                myCon = new SqlConnection(sqlCon);
                myCon.Open();
                sqlCom = new SqlCommand(sql, myCon);
                sqlADT = new SqlDataAdapter(sql, myCon);
                DataTable dt = new DataTable();
                sqlADT.Fill(dt);
                dtgv.ItemsSource = dt.DefaultView;
                myCon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "" + ex);
            }
        }

        public static void Them(string sql, DataGrid dtgv)
        {
            try
            {
                myCon = new SqlConnection(sqlCon);
                myCon.Open();
                sqlCom = new SqlCommand(sql, myCon);
                sqlADT = new SqlDataAdapter(sql, myCon);
                DataTable dt = new DataTable();
                sqlADT.Fill(dt);
                dtgv.ItemsSource = dt.DefaultView;
                MessageBox.Show("Thêm thành công! ", "Thông báo ", MessageBoxButton.OK);
                myCon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "" + ex);
            }
        }

        public static void Xoa(string sql)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                try
                {
                    myCon = new SqlConnection(sqlCon);
                    myCon.Open();
                    sqlCom = new SqlCommand(sql, myCon);
                    MessageBox.Show("Bạn vừa xóa thành công! ", "Thông báo ", MessageBoxButton.OK);
                    sqlCom.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("" + ex);
                }
            }
        }

        public static void Sua(string sql)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn sửa không?", "Thông báo", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                try
                {
                    myCon = new SqlConnection(sqlCon);
                    myCon.Open();
                    sqlCom = new SqlCommand(sql, myCon);
                    sqlCom.ExecuteNonQuery();
                    myCon.Close();
                    MessageBox.Show("Bạn vừa sửa thành công! ", "Thông báo ", MessageBoxButton.OK);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("" + ex);
                }
            }
        }

        public static void CapNhatSoLuongTon(string sql)
        {
            try
            {
                myCon = new SqlConnection(sqlCon);
                myCon.Open();
                sqlCom = new SqlCommand(sql, myCon);
                sqlCom.ExecuteNonQuery();
                myCon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }

        public static void TimKiem(string sql, DataGrid dtgv)
        {
            try
            {
                sqlADT = new SqlDataAdapter(sql, myCon);
                DataTable dt = new DataTable();
                sqlCB = new SqlCommandBuilder(sqlADT);
                sqlADT.Fill(dt);
                dtgv.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }

        public static void XuLyCBX(string sql, ComboBox cbx, string tenCot)
        {
            sqlADT = new SqlDataAdapter(sql, myCon);
            DataTable dt = new DataTable();
            sqlADT.Fill(dt);
            cbx.ItemsSource = dt.DefaultView;
            cbx.DisplayMemberPath = tenCot;
        }

        public static int Dem(string sql)
        {
            int count = 0;
            try
            {
                myCon = new SqlConnection(sqlCon);
                myCon.Open();
                sqlCom = new SqlCommand(sql, myCon);
                count = Convert.ToInt32(sqlCom.ExecuteScalar());
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
            finally
            {
                myCon.Close();
            }
            return count;
        }

        public static bool KiemTraDangNhap(string taiKhoan, string matKhau)
        {
            string querry = "SELECT COUNT(*) FROM TaiKhoan WHERE taikhoan = @TaiKhoan AND matkhau = @MatKhau";

            using (SqlConnection connection = new SqlConnection(sqlCon))
            {
                using (SqlCommand command = new SqlCommand(querry, connection))
                {
                    command.Parameters.AddWithValue("@TaiKhoan", taiKhoan);
                    command.Parameters.AddWithValue("@MatKhau", matKhau);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            int count = Convert.ToInt32(result);
                            return count > 0;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message);
                        return false;
                    }
                }
            }
        }
    }
}
