using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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
    /// Interaction logic for Frm_KhachHang.xaml
    /// </summary>
    public partial class Frm_KhachHang : Window
    {
		public string chuoi = "SELECT * FROM KhachHang";
		public Frm_KhachHang()
		{
			InitializeComponent();
			KetNoiSQL.ChuoiKetNoi(chuoi, dtgvKhachHang);
		}

		public void TenBang()
		{
			//dtgvKhachHang.Columns[0].Header = "Mã khách hàng";
			//dtgvKhachHang.Columns[0].Header = "Tên khách hàng";
			//dtgvKhachHang.Columns[0].Header = "Ngày sinh";
			//dtgvKhachHang.Columns[0].Header = "Email";
			//dtgvKhachHang.Columns[0].Header = "Điện thoại";
			//dtgvKhachHang.Columns[0].Header = "Địa chỉ";
		}
		private void LoadData()
		{
			try
			{
				// Tải dữ liệu lên DataGrid
				KetNoiSQL.ChuoiKetNoi(chuoi, dtgvKhachHang);

				// Kiểm tra dữ liệu
				if (KetNoiSQL.dt != null && KetNoiSQL.dt.Rows.Count > 0)
				{
					//TenBang();
				}
				else
				{
					MessageBox.Show("Không có dữ liệu trong bảng Khach Hang!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		public void Clear()
		{
			txtMaKH.Text = "";
			txtTenKH.Text = "";
			txtDiaChi.Text = "";
			txtDienThoai.Text = "";
			txtEmail.Text = "";
			dtpNgaySinh.Text = "";
		}

		private void btnLamMoi_Click(object sender, EventArgs e)
		{
			//KetNoiSQL.LamMoi(chuoi, dtgvKhachHang);
			txtMaKH.Clear();
			txtTenKH.Clear();
			txtDiaChi.Clear();
			txtDienThoai.Clear();
			txtEmail.Clear();
			dtpNgaySinh.ClearValue(DatePicker.SelectedDateProperty);
		}

		private void btnThem_Click(object sender, EventArgs e)
		{
			string maKH = txtMaKH.Text.Trim();
			if (string.IsNullOrEmpty(maKH))
			{
				return;
			}

			foreach (var item in dtgvKhachHang.Items)
			{
				DataRowView row = item as DataRowView;
				if (row != null && row[0] != null && row[0].ToString() == maKH)
				{
					MessageBox.Show("Mã khách hàng đã tồn tại !", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
					txtMaKH.Text = "";
					return;
				}
			}
			if (txtMaKH.Text == "" || txtTenKH.Text == "" || dtpNgaySinh.Text == "" || txtEmail.Text == "" || txtDienThoai.Text == "" || txtDiaChi.Text == "")
			{
				MessageBox.Show("Bạn chưa nhập đầy đủ thông tin", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
			}
			else
			{
				string ngaySinh = dtpNgaySinh.SelectedDate.Value.ToString("yyyy-MM-dd");
				string sql = "Insert into KhachHang Values('" + txtMaKH.Text + "', '" + txtTenKH.Text + "', '" + ngaySinh + "', '" + txtEmail.Text + "', '" + txtDienThoai.Text + "', '" + txtDiaChi.Text + "' )";
				KetNoiSQL.Them(sql, dtgvKhachHang);
				KetNoiSQL.ChuoiKetNoi(chuoi, dtgvKhachHang);
				TenBang();
				Clear();
			}
		}

		private void btnSua_Click(object sender, RoutedEventArgs e)
		{
			string ngaySinh = DateTime.ParseExact(dtpNgaySinh.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
			string sql = $"UPDATE KhachHang SET tenKH = '{txtTenKH.Text}', ngaySinh = '{ngaySinh}', diaChi = '{txtDiaChi.Text}', dienThoai = '{txtDienThoai.Text}', email = '{txtEmail.Text}' WHERE maKH = '{txtMaKH.Text}'";
			KetNoiSQL.Sua(sql);
			KetNoiSQL.ChuoiKetNoi(chuoi, dtgvKhachHang);
			TenBang();
			Clear();
		}

		private void btnXoa_Click(object sender, RoutedEventArgs e)
		{
			string sql = "delete from KhachHang where maKH = '" + txtMaKH.Text + "'";
			KetNoiSQL.Xoa(sql);
			KetNoiSQL.ChuoiKetNoi(chuoi, dtgvKhachHang);
			TenBang();
			Clear();
		}

		private void dtgvKhachHang_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (dtgvKhachHang.SelectedItem is DataRowView row)
			{
				txtMaKH.Text = row["maKH"].ToString();
				txtTenKH.Text = row["tenKH"].ToString();
				dtpNgaySinh.Text = row["ngaySinh"].ToString();
				txtEmail.Text = row["email"].ToString();
				txtDienThoai.Text = row["dienThoai"].ToString();
				txtDiaChi.Text = row["diaChi"].ToString();
				//TenBang();
				LoadData();
			}
		}
		private void btnTimKiem_Click(object sender, RoutedEventArgs e)
		{
			if (!string.IsNullOrEmpty(txtMaKHTimKiem.Text))
			{
				string sqlTimKiem = "Select * from KhachHang where maKH = '" + txtMaKHTimKiem.Text + "'";
				KetNoiSQL.TimKiem(sqlTimKiem, dtgvKhachHang);
				TenBang();
			}
			else
			{
				KetNoiSQL.ChuoiKetNoi(chuoi, dtgvKhachHang);
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

		private void Menu_ThuongHieu_Click(object sender, RoutedEventArgs e)
		{
			Frm_ThuongHieu frm_ThuongHieu = new Frm_ThuongHieu();
			frm_ThuongHieu.Show();

			this.Close();
		}

		private void Menu_ChucVu_Click(object sender, RoutedEventArgs e)
		{
			Frm_ChucVu frm_ChucVu = new Frm_ChucVu();
			frm_ChucVu.Show();

			this.Close();
		}

		private MessageBoxResult ShowConfirmMessage(string title, string message)
		{
			return MessageBox.Show(title, message, MessageBoxButton.YesNo, MessageBoxImage.Question);
		}
	}
}
