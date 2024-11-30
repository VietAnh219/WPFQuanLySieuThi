using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QuanLySieuThi.GUI
{
    /// <summary>
    /// Interaction logic for Frm_ChucVu.xaml
    /// </summary>
    public partial class Frm_ChucVu : Window
    {
		public string chuoi = "SELECT * FROM ChucVu";
		public Frm_ChucVu()
		{
			InitializeComponent();
			KetNoiSQL.ChuoiKetNoi(chuoi, dtgvChucVu);

			//if (KetNoiSQL.dt != null && KetNoiSQL.dt.Rows.Count > 0)
			//{
			//	TenBang();
			//}
			//else
			//{
			//	MessageBox.Show("Dtgv_TaiKhoan không có dữ liệu!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
			//}
		}

		private void LoadData()
		{
			try
			{
				// Tải dữ liệu lên DataGrid
				KetNoiSQL.ChuoiKetNoi(chuoi, dtgvChucVu);

				// Kiểm tra dữ liệu
				if (KetNoiSQL.dt != null && KetNoiSQL.dt.Rows.Count > 0)
				{
					//TenBang();
				}
				else
				{
					MessageBox.Show("Không có dữ liệu trong bảng ChucVu!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		//private void TenBang()
		//{
		//	dtgvChucVu.Columns[0].Header = "Mã Chức Vụ";
		//	dtgvChucVu.Columns[1].Header = "Tên Chức Vụ";
		//	dtgvChucVu.Columns[2].Header = "Mô Tả";
		//}

		private void Clear()
		{
			txtMaChucVu.Text = "";
			txtTenChucVu.Text = "";
			txtMoTa.Text = "";
		}

		private void btn_LamMoi_Click(object sender, EventArgs e)
		{
			string sql = "SELECT * FROM ChucVu";
			KetNoiSQL.LamMoi(sql, dtgvChucVu);
		}

		private void btn_Them_Click(object sender, RoutedEventArgs e)
		{
			if (KetNoiSQL.dt != null && KetNoiSQL.dt.Rows.Count > 0)
			{
				string maChucVu = txtMaChucVu.Text.Trim();
				if (string.IsNullOrEmpty(maChucVu))
				{
					return;
				}
				else
				{
					foreach (var item in dtgvChucVu.Items)
					{
						DataRowView row = item as DataRowView;
						if (row != null && row[0] != null && row[0].ToString() == maChucVu)
						{
							MessageBox.Show("Mã chức vụ đã tồn tại !", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
							txtMaChucVu.Text = "";
							return;
						}
					}
					if (string.IsNullOrEmpty(txtMaChucVu.Text) || string.IsNullOrEmpty(txtTenChucVu.Text) || string.IsNullOrEmpty(txtMoTa.Text))
					{
						MessageBox.Show("Bạn chưa nhập đầy đủ thông tin", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
					}
					else
					{
						string sql = $"Insert into ChucVu Values(N'{txtMaChucVu.Text}', N'{txtTenChucVu.Text}', N'{txtMoTa.Text}')";
						KetNoiSQL.Them(sql, dtgvChucVu);
						KetNoiSQL.ChuoiKetNoi(chuoi, dtgvChucVu);
						//TenBang();
						LoadData();
						Clear();
					}
				}
			}
		}
		private void btn_Sua_Click(object sender, RoutedEventArgs e)
		{
			if (!string.IsNullOrEmpty(txtMaChucVu.Text) && !string.IsNullOrEmpty(txtTenChucVu.Text) && !string.IsNullOrEmpty(txtMoTa.Text))
			{
				string sql = $"Update ChucVu set tenChucVu = N'{txtTenChucVu.Text}',moTa = N'{txtMoTa.Text}' WHERE maChucVu = N'{txtMaChucVu.Text}'";
				KetNoiSQL.Sua(sql);
				KetNoiSQL.ChuoiKetNoi(chuoi, dtgvChucVu);
				//TenBang();
				LoadData() ;
				Clear();
			}
			else
			{
				MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void btn_Xoa_Click(object sender, RoutedEventArgs e)
		{
			string chucvu = txtMaChucVu.Text;
			string sql = $"delete from ChucVu where maChucVu = '{chucvu}'";
			KetNoiSQL.Xoa(sql);
			LoadData();
			Clear();
		}

		private void dtgvChucVu_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (dtgvChucVu.SelectedItem is DataRowView row)
			{
				txtMaChucVu.Text = row["maChucVu"].ToString();
				txtTenChucVu.Text = row["tenChucVu"].ToString();
				txtMoTa.Text = row["moTa"].ToString();
				//TenBang();
				LoadData() ;
			}
		}
		private void btn_TimKiem_Click(object sender, RoutedEventArgs e)
		{
			string sqlSearch = $"Select * from ChucVu where maChucVu = '{txtMaCVTimKiem.Text}'";
			KetNoiSQL.TimKiem(sqlSearch, dtgvChucVu);
			//LoadData();
		}

		private void btnLamMoi_Click(object sender, RoutedEventArgs e)
		{

		}
		private void Menu_TrangChu_Click(object sender, RoutedEventArgs e)
		{
			Frm_TrangChu frm_TrangChu = new Frm_TrangChu();
			frm_TrangChu.Show();

			this.Close();
		}
		private void Menu_BaoCao_Click(object sender, RoutedEventArgs e)
		{
			Frm_BaoCao frm_BaoCao = new Frm_BaoCao();
			frm_BaoCao.Show();

			this.Close();
		}

		private void Menu_LapHoaDon_Click(object sender, RoutedEventArgs e)
		{
			Frm_LapHoaDon frm_LapHoaDon = new Frm_LapHoaDon();
			frm_LapHoaDon.Show();

			this.Close();
		}

		private void Menu_TaiKhoan_Click(object sender, RoutedEventArgs e)
		{
			Frm_TaiKhoan frm_TaiKhoan = new Frm_TaiKhoan();
			frm_TaiKhoan.Show();

			this.Close();
		}

		private void Menu_KhachHang_Click(object sender, RoutedEventArgs e)
		{
			Frm_KhachHang frm_KhachHang = new Frm_KhachHang();
			frm_KhachHang.Show();

			this.Close();
		}

		private void Menu_NhaCungCap_Click(object sender, RoutedEventArgs e)
		{
			Frm_NhaCungCap frm_NhaCungCap = new Frm_NhaCungCap();
			frm_NhaCungCap.Show();

			this.Close();
		}

		private void Menu_KhuyenMai_Click(object sender, RoutedEventArgs e)
		{
			Frm_KhuyenMai frm_KhuyenMai = new Frm_KhuyenMai();
			frm_KhuyenMai.Show();

			this.Close();
		}

		private void Menu_SanPham_Click(object sender, RoutedEventArgs e)
		{
			Frm_SanPham frm_SanPham = new Frm_SanPham();
			frm_SanPham.Show();

			this.Close();
		}

		private void Menu_LoaiSanPham_Click(object sender, RoutedEventArgs e)
		{
			Frm_LoaiSanPham frm_LoaiSanPham = new Frm_LoaiSanPham();
			frm_LoaiSanPham.Show();

			this.Close();
		}

		private void Menu_NhanVien_Click(object sender, RoutedEventArgs e)
		{
			Frm_NhanVien frm_NhanVien = new Frm_NhanVien();
			frm_NhanVien.Show();

			this.Close();
		}

		private void Menu_Thoat_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void Menu_DangXuat_Click(object sender, RoutedEventArgs e)
		{
			var title = "Xác nhận";
			var message = "Bạn có chắc chắn muốn đăng xuất ?";
			var ans = ShowConfirmMessage(title, message);

			if (ans == MessageBoxResult.Yes)
			{
				this.Close();

				var loginForm = new Frm_DangNhap();
				loginForm.Show();
			}
		}
		private void Menu_BanHang_Click(object sender, RoutedEventArgs e)
		{
			Frm_BanHang frm_BanHang = new Frm_BanHang();
			frm_BanHang.Show();

			this.Close();
		}
		private MessageBoxResult ShowConfirmMessage(string title, string message)
		{
			return MessageBox.Show(title, message, MessageBoxButton.YesNo, MessageBoxImage.Question);
		}
	}
}
