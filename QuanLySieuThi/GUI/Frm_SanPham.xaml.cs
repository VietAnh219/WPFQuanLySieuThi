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
	/// Interaction logic for Frm_SanPham.xaml
	/// </summary>
	public partial class Frm_SanPham : Window
	{

		public string chuoi = "Select * from SanPham";
		private DuLieuCBX duLieu = new DuLieuCBX();

		public Frm_SanPham()
		{
			InitializeComponent();

			duLieu.sqlMaLoaiSP = "Select maloaiSP from LoaiSanPham";
			duLieu.tenCotMaLoaiSP = "maloaiSP";
			duLieu.sqlMaThuongHieu = "Select maThuongHieu from ThuongHieu";
			duLieu.tenCotMaThuongHieu = "maThuongHieu";
			KetNoiSQL.XuLyCBX(duLieu.sqlMaLoaiSP, cbbLoaiSP, duLieu.tenCotMaLoaiSP);
			KetNoiSQL.XuLyCBX(duLieu.sqlMaThuongHieu, cbbTHieu, duLieu.tenCotMaThuongHieu);
			LoadData();
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

		private void TenBang()
		{
			//dtgv_SanPham.Columns[0].Header = "Mã sản phẩm";
			//dtgv_SanPham.Columns[1].Header = "Tên sản phẩm";
			//dtgv_SanPham.Columns[2].Header = "Mã loại sản phẩm";
			//dtgv_SanPham.Columns[3].Header = "Số lượng tổn";
			//dtgv_SanPham.Columns[4].Header = "Màu sắc";
			//dtgv_SanPham.Columns[5].Header = "Ngày sản xuất";
			//dtgv_SanPham.Columns[6].Header = "Ngày hết hạn";
			//dtgv_SanPham.Columns[7].Header = "Mã thương hiệu";
			//dtgv_SanPham.Columns[8].Header = "Đơn giá";
			//dtgv_SanPham.Columns[9].Header = "kích thước";
			//dtgv_SanPham.Columns[10].Header = "Mô tả";
		}

		public void Clear()
		{
			txtMaSP.Text = "";
			txtTenSP.Text = "";
			cbbLoaiSP.Text = "";
			txtSoLuongTon.Text = "";
			cbbTHieu.Text = "";
			txtMauSac.Text = "";
			txtDonGia.Text = "";
			txtKichThuoc.Text = "";
		}

		private void btnThem_Click(object sender, RoutedEventArgs e)
		{
			string maSP = txtMaSP.Text.Trim();

			if (string.IsNullOrEmpty(maSP))
			{
				return;
			}
			else
			{
				foreach (var item in dtgv_SanPham.Items)
				{
					DataRowView row = item as DataRowView;
					if (row != null && row[0] != null && row[0].ToString() == maSP)
					{
						MessageBox.Show("Mã Sản phẩm đã tồn tại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
						txtMaSP.Text = "";
						return;
					}
				}
				if (txtMaSP.Text == "" || txtTenSP.Text == "" || txtSoLuongTon.Text == "" || txtDonGia.Text == "" || txtKichThuoc.Text == "")
				{
					MessageBox.Show("Bạn chưa nhập đầy đủ thông tin!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
					return;
				}
				else
				{
					int soLuongTon;
					if (!int.TryParse(txtSoLuongTon.Text, out soLuongTon))
					{
						MessageBox.Show("56 lượng tốn phải là số nguyên!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
						return;
					}
					int donGia;
					if (!int.TryParse(txtDonGia.Text, out donGia))
					{
						MessageBox.Show("Đơn giá phải là số nguyên!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
						return;
					}
					string sql1 = $"INSERT INTO SanPham( maSP, tenSP, maLoaiSP, mauSac, soLuongTon, ngaySX, ngayHH, maThuongHieu, donGia, kichThuoc, moTa) VALUES (N'{txtMaSP.Text}', N'{txtTenSP.Text}', N'{cbbLoaiSP.Text}', N'{txtMauSac.Text}', {txtSoLuongTon.Text}, '{dpNgaySX.SelectedDate:yyyy-MM-dd}', '{dpNgayHH.SelectedDate:yyyy-MM-dd}', N'{cbbTHieu.Text}', {txtDonGia.Text}, N'{txtKichThuoc.Text}', N'{txtMoTa.Text}')";
					KetNoiSQL.Them(sql1, dtgv_SanPham);

					KetNoiSQL.ChuoiKetNoi(chuoi, dtgv_SanPham);
					TenBang();
					Clear();
				}
			}
		}

		private void btnLamMoi_Click(object sender, RoutedEventArgs e)
		{
			KetNoiSQL.LamMoi(chuoi, dtgv_SanPham);
			Clear();
		}

		private void btnSua_Click(object sender, RoutedEventArgs e)
		{
			string sql = $"UPDATE SanPham SET tenSP = '{txtTenSP.Text}', maLoaiSP = '{cbbLoaiSP.Text}', mauSac = '{txtMauSac.Text}', soLuongTon = {txtSoLuongTon.Text}, ngaySX = '{dpNgaySX.SelectedDate:yyyy-MM-dd}', ngayHH = '{dpNgayHH.SelectedDate:yyyy-MM-dd}', maThuongHieu = '{cbbTHieu.Text}', donGia = {txtDonGia.Text}, kichThuoc = '{txtKichThuoc.Text}',moTa = '{txtMoTa.Text}' WHERE maSP = '{txtMaSP.Text}'";
			KetNoiSQL.Sua(sql);
			KetNoiSQL.ChuoiKetNoi(chuoi, dtgv_SanPham);
			Clear();
		}

		private void btnXoa_Click(object sender, RoutedEventArgs e)
		{
			string sql = "Delete from SanPham where maSP = '" + txtMaSP.Text + "'";
			KetNoiSQL.Xoa(sql);
			KetNoiSQL.ChuoiKetNoi(chuoi, dtgv_SanPham);
			Clear();
		}

		private void btnTim_Click(object sender, RoutedEventArgs e)
		{
			if (!string.IsNullOrEmpty(txtTimKiem.Text))
			{
				string sqlTimKiem = "SELECT * FROM SanPham WHERE maSP = '" + txtTimKiem.Text.Trim() + "' OR tenSP LIKE '%" + txtTimKiem.Text.Trim() + "%'";
				KetNoiSQL.TimKiem(sqlTimKiem, dtgv_SanPham);
			}
			else
			{
				KetNoiSQL.ChuoiKetNoi(chuoi, dtgv_SanPham);
			}
		}

		private void dtgv_SanPham_SelectedChanged(object sender, SelectionChangedEventArgs e)
		{
			if (dtgv_SanPham.SelectedItem is DataRowView row)
			{
				txtMaSP.Text = row["maSP"].ToString();
				txtTenSP.Text = row["tenSP"].ToString();
				cbbLoaiSP.Text = row["maLoaiSP"].ToString();
				txtSoLuongTon.Text = row["soLuongTon"].ToString();
				txtMauSac.Text = row["mauSac"].ToString();
				dpNgaySX.Text = row["ngaySX"].ToString();
				dpNgayHH.Text = row["ngayHH"].ToString();
				cbbTHieu.Text = row["maThuongHieu"].ToString();
				txtDonGia.Text = row["donGia"].ToString();
				txtKichThuoc.Text = row["kichThuoc"].ToString();
				txtMoTa.Text = row["moTa"].ToString();
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
