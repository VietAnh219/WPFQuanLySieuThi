using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QuanLySieuThi.GUI
{
    /// <summary>
    /// Interaction logic for Frm_BanHang.xaml
    /// </summary>
    public partial class Frm_BanHang : Window
    {
		string chuoi = "Select maSP, tenSP, maLoaiSP, soLuongTon, kichThuoc, donGia from SanPham";
		public Frm_BanHang()
		{
			InitializeComponent();
			KetNoiSQL.ChuoiKetNoi(chuoi, dtgv_SanPham);
		}
		private void TenBang()
		{
			//dtgv_SanPham.Columns[0].Header = "Mã sản phẩm";
			//dtgv_SanPham.Columns[1].Header = "Tên sản phẩm";
			//dtgv_SanPham.Columns[2].Header = "Mã loại sản phẩm";
			//dtgv_SanPham.Columns[3].Header = "Số lượng tồn";
			//dtgv_SanPham.Columns[4].Header = "Kích thước";
			//dtgv_SanPham.Columns[5].Header = "Đơn giá";
		}

		private void LoadData()
		{
			try
			{
				// Tải dữ liệu lên DataGrid
				KetNoiSQL.ChuoiKetNoi(chuoi, dtgv_SanPham);

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
		private void dtgv_SanPham_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (dtgv_SanPham.SelectedItem is DataRowView row)
			{
				// Lấy dữ liệu từ dòng được chọn và gán vào các ô nhập liệu chi tiết
				txt_MaSP.Text = row["maSP"].ToString();
				txt_TenSP.Text = row["tenSP"].ToString();
				txt_LoaiSP.Text = row["maLoaiSP"].ToString();
				txt_SoLuongTon.Text = row["soLuongTon"].ToString();
				txt_KichThuoc.Text = row["kichThuoc"].ToString();
				txt_DonGia.Text = row["donGia"].ToString();

				LoadData();
			}
		}

		private void btn_Them_Click(object sender, EventArgs e)
		{
			if (txt_MaSP.Text == "" || txt_SoLuong.Text == "")
			{
				MessageBox.Show("Bạn chưa nhập đầy đủ thông tin!", "Lỗi", MessageBoxButton.OK);
			}
			else
			{
				double soLuong = double.Parse(txt_SoLuong.Text);
				double gia = double.Parse(txt_DonGia.Text);
				double thanhTien = soLuong * gia;
				string sql = $"UPDATE PhieuNhapChiTiet SET soLuong = {txt_SoLuong.Text}, donGia = {gia}, maPhieuNhap = '{txt_maPhieuNhap.Text}', thanhTien = {thanhTien} WHERE maSP = '{txt_MaSP.Text}';";
				KetNoiSQL.Them(sql, dtgv_BanHang);
				string sql2 = "SELECT pn.maSP, sp.tenSP, pn.soLuong, pn.donGia,pn.maPhieuNhap, pn.thanhTien FROM PhieuNhapChiTiet pn JOIN SanPham sp ON pn.maSP = sp.maSP";
				KetNoiSQL.ChuoiKetNoi(sql2, dtgv_BanHang);
				TenBang();
			}
		}
		private void btn_LamMoi_Click(object sender, EventArgs e)
		{
			txt_MaSP.Clear();
			txt_TenSP.Clear();
			txt_LoaiSP.Clear();
			txt_SoLuongTon.Clear();
			txt_KichThuoc.Clear();
			txt_DonGia.Clear();
			txt_SoLuong.Clear();
			txt_maPhieuNhap.Clear();
		}
		private void btn_Xoa_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(txt_MaSP.Text))
			{
				MessageBox.Show("Vui lòng chọn một sản phẩm để xóa!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}

			MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm này không?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
			if (result == MessageBoxResult.Yes)
			{
				try
				{
					string sql = $"DELETE FROM SanPham WHERE maSP = '{txt_MaSP.Text}'";
					KetNoiSQL.Xoa(sql);
					MessageBox.Show("Xóa sản phẩm thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
					KetNoiSQL.ChuoiKetNoi(chuoi, dtgv_SanPham);
					LoadData();
				}
				catch (Exception ex)
				{
					MessageBox.Show("Lỗi khi xóa sản phẩm: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}
		}
		private void btn_Sua_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(txt_MaSP.Text))
			{
				MessageBox.Show("Vui lòng chọn một sản phẩm để sửa!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}

			try
			{
				double gia = double.Parse(txt_DonGia.Text);
				string sql = $"UPDATE SanPham SET tenSP = '{txt_TenSP.Text}', maLoaiSP = '{txt_LoaiSP.Text}', soLuongTon = {txt_SoLuongTon.Text}, kichThuoc = '{txt_KichThuoc.Text}', donGia = {gia} WHERE maSP = '{txt_MaSP.Text}'";
				KetNoiSQL.Sua(sql);
				MessageBox.Show("Cập nhật sản phẩm thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
				KetNoiSQL.ChuoiKetNoi(chuoi, dtgv_SanPham);
				//LoadData() ;

			}
			catch (Exception ex)
			{
				MessageBox.Show("Lỗi khi cập nhật sản phẩm: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void btn_TimKiem_Click(object sender, RoutedEventArgs e)
		{
			string sqlSearch = $"Select * from SanPham where maSP = '{txt_TimKiem.Text}'";
			KetNoiSQL.TimKiem(sqlSearch, dtgv_SanPham);
			//LoadData();
		}

		private void txt_TenSP_TextChanged(object sender, TextChangedEventArgs e)
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
		private void Menu_BanHang_Click(object sender, RoutedEventArgs e)
		{
			Frm_BanHang frm_BanHang = new Frm_BanHang();
			frm_BanHang.Show();

			this.Close();
		}

		private void Menu_NhaCungCap_Click(object sender, RoutedEventArgs e)
		{
			Frm_NhaCungCap frm_NhaCungCap = new Frm_NhaCungCap();
			frm_NhaCungCap.Show();

			this.Close();
		}
		private void Menu_ThuongHieu_Click(object sender, RoutedEventArgs e)
		{
			Frm_ThuongHieu frm_ThuongHieu = new Frm_ThuongHieu();
			frm_ThuongHieu.Show();

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
		private MessageBoxResult ShowConfirmMessage(string title, string message)
		{
			return MessageBox.Show(title, message, MessageBoxButton.YesNo, MessageBoxImage.Question);
		}
	}
}
