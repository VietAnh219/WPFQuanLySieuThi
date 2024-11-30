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
    /// Interaction logic for Frm_TaiKhoan.xaml
    /// </summary>
    public partial class Frm_TaiKhoan : Window
    {
		public string chuoi = "SELECT * FROM Taikhoan";
		private DuLieuCBX duLieuCBX = new DuLieuCBX();
		public Frm_TaiKhoan()
		{
			InitializeComponent();

			duLieuCBX.sqlMaChucVu = "select maChucVu from ChucVu";
			duLieuCBX.tenCotMaChucVu = "maChucVu";
			KetNoiSQL.XuLyCBX(duLieuCBX.sqlMaChucVu, combo_MaCV, duLieuCBX.tenCotMaChucVu);
			LoadData();
		}
		private void LoadData()
		{
			KetNoiSQL.ChuoiKetNoi(chuoi, dtgv_TaiKhoan);
			if (KetNoiSQL.dt != null && KetNoiSQL.dt.Rows.Count > 0)
			{
				
			}
			else
			{
				MessageBox.Show("Dtqv_TaiKhoan không có dữ liệu !", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void TenBang()
		{
			//dtgv_TaiKhoan.Columns[8].Header = "Tài khoản";
			//dtgv_TaiKhoan.Columns[1].Header = "Mật khẩu";
			//dtgv_TaiKhoan.Columns[2].Header = "Mã chức vụ";
		}

		private void Clear()
		{
			txt_TaiKhoan.Text = "";
			txt_MatKhau.Text = "";
			combo_MaCV.SelectedIndex = 0;
		}
		private void btn_LamMoi_Click(object sender, EventArgs e)
		{
			KetNoiSQL.LamMoi("Select * from TaiKhoan", dtgv_TaiKhoan);
			Clear();
		}

		private void btn_Them_Click(object sender, EventArgs e)
		{
			if (combo_MaCV != null && !string.IsNullOrEmpty(txt_TaiKhoan.Text) && !string.IsNullOrEmpty(txt_MatKhau.Text) && combo_MaCV.SelectedIndex != -1)
			{
				string taikhoan = txt_TaiKhoan.Text.Trim();
				if (string.IsNullOrEmpty(taikhoan))
				{
					return;
				}
				else
				{
					foreach (var item in dtgv_TaiKhoan.Items)
					{
						DataRowView row = item as DataRowView;
						if (row != null && row[0] != null && row[0].ToString() == taikhoan)
						{
							MessageBox.Show("Tài khoản đã tồn tại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
							txt_TaiKhoan.Text = "";
							return;
						}
					}
					string sql = $"INSERT INTO TaiKhoan VALUES(N'{txt_TaiKhoan.Text}', N'{txt_MatKhau.Text}', N'{combo_MaCV.Text}')";
					KetNoiSQL.Them(sql, dtgv_TaiKhoan);
					KetNoiSQL.ChuoiKetNoi(chuoi, dtgv_TaiKhoan);
					TenBang();
					Clear();
				}
			}
			else
			{
				MessageBox.Show("Bạn chưa nhập đầy đủ thông tin!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void btn_Sua_Click(object sender, EventArgs e)
		{
			if (combo_MaCV != null && !string.IsNullOrEmpty(txt_TaiKhoan.Text) && !string.IsNullOrEmpty(txt_MatKhau.Text) && combo_MaCV.SelectedIndex != -1)
			{
				string sql = $"UPDATE TaiKhoan SET matKhau = N'{txt_MatKhau.Text}', maChucVu = (SELECT maChucVu FROM ChucVu WHERE maChucVu = N'{combo_MaCV.Text}') WHERE taiKhoan = N'{txt_TaiKhoan.Text}'";
				KetNoiSQL.Sua(sql);
				KetNoiSQL.ChuoiKetNoi(chuoi, dtgv_TaiKhoan);
				Clear();
			}
		}

		private void btn_Xoa_Click(object sender, EventArgs e)
		{
			string sql = $"DELETE FROM TaiKhoan WHERE taiKhoan = N'{txt_TaiKhoan.Text}'";
			KetNoiSQL.Xoa(sql);
			KetNoiSQL.ChuoiKetNoi(chuoi, dtgv_TaiKhoan);
			Clear();
		}

		private void dtgv_TaiKhoan_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (dtgv_TaiKhoan.SelectedItem is DataRowView row)
			{
				txt_TaiKhoan.Text = row["taiKhoan"].ToString();
				txt_MatKhau.Text = row["matKhau"].ToString();
				combo_MaCV.Text = row["maChucVu"].ToString();
			}
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
