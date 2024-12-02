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
	/// Interaction logic for Frm_BaoCao.xaml
	/// </summary>
	public partial class Frm_BaoCao : Window
	{
		public string chuoi = @"SELECT HoaDon.maHD, HoaDon.maKH, HoaDon.maNV, HoaDon.ngayLap, HoaDonChiTiet.maSP, HoaDonChiTiet.thanhTien, HoaDon.ghiChu 
                          FROM HoaDon, HoaDonChiTiet 
                          WHERE HoaDon.maHD = HoaDonChiTiet.maHD
                          GROUP BY HoaDon.maHD, HoaDon.maKH, HoaDon.maNV, HoaDon.ngayLap, HoaDonChiTiet.maSP, HoaDonChiTiet.thanhTien, HoaDon.ghiChu";

		public Frm_BaoCao()
		{
			InitializeComponent();
			KetNoiSQL.ChuoiKetNoi(chuoi, dtgvChiTiet);
			TenBang();
		}

		private void TenBang()
		{

		}

		private void dtpChonNgay_ValueChanged(object sender, EventArgs e)
		{
			// Lấy ngày được chọn từ DateTimePicker
			string ngayChon = dtpChonNgay.SelectedDate.Value.ToString("yyyy-MM-dd");

			// Truy vấn dữ liệu theo chỉ định ngày lập
			string sql = @"SELECT HoaDon.maHD, HoaDon.maKH, HoaDon.maNV, HoaDon.ngayLap, HoaDonChiTiet.maSP, HoaDonChiTiet.thanhTien, HoaDon.ghiChu 
                          FROM HoaDon, HoaDonChiTiet 
                          WHERE HoaDon.maHD = HoaDonChiTiet.maHD AND ngayLap = '" + ngayChon + @" '
                          GROUP BY HoaDon.maHD, HoaDon.maKH, HoaDon.maNV, HoaDon.ngayLap, HoaDonChiTiet.maSP, HoaDonChiTiet.thanhTien, HoaDon.ghiChu";

			KetNoiSQL.ChuoiKetNoi(sql, dtgvChiTiet);
			TenBang();

			decimal tongDoanhThu = 0;
			foreach (DataRow row in KetNoiSQL.dt.Rows)
			{
				// Sử dụng kiểu dữ liệu decimal để tránh mất mát chính xác trong phép tính tiền tệ
				tongDoanhThu += Convert.ToDecimal(row["thanhTien"]);
			}
			txtTongDoanhThu.Text = $"Tổng doanh thu: {tongDoanhThu}";
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
		private void Menu_ThuongHieu_Click(object sender, RoutedEventArgs e)
		{
			Frm_ThuongHieu frm_ThuongHieu = new Frm_ThuongHieu();
			frm_ThuongHieu.Show();

			this.Close();
		}
		private MessageBoxResult ShowConfirmMessage(string title, string message)
		{
			return MessageBox.Show(title, message, MessageBoxButton.YesNo, MessageBoxImage.Question);
		}
	}
}
