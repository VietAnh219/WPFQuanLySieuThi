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
    /// Interaction logic for Frm_NhaCungCap.xaml
    /// </summary>
    public partial class Frm_NhaCungCap : Window
    {
		public string chuoi = "Select * from NhaCungCap";
		public Frm_NhaCungCap()
		{
			InitializeComponent();
			LoadData();
		}

		public void TenBang()
		{
			//dtgv_NhaCungCap.Columns[0].Header = "Mã nhà cung cấp ";
			//dtgv_NhaCungCap.Columns[1].Header = "Tên nhà cung cấp";
			//dtgv_NhaCungCap.Columns[2].Header = "Dịa chi";
			//dtgv_NhaCungCap.Columns[3].Header = "Điên thoại";
			//dtgv_NhaCungCap.Columns[4].Header = "Email";
		}

		private void LoadData()
		{
			try
			{
				// Tải dữ liệu lên DataGrid
				KetNoiSQL.ChuoiKetNoi(chuoi, dtgv_NhaCungCap);

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

		private void btnThem_Click(object sender, EventArgs e)
		{
			string maNhaCC = txt_MaNhaCC.Text.Trim();
			if (string.IsNullOrEmpty(maNhaCC))
			{
				return;
			}
			foreach (var item in dtgv_NhaCungCap.Items)
			{
				DataRowView row = item as DataRowView;
				if (row != null && row[0] != null && row[0].ToString() == maNhaCC)
				{
					MessageBox.Show("Mã nhà cung cấp đã tồn tại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
					txt_MaNhaCC.Text = "";
					return;
				}
			}

			if (txt_MaNhaCC.Text == "" || txt_TenNhaCC.Text == "" || txt_DiaChi.Text == "" || txt_Email.Text == "")
			{
				MessageBox.Show("Bạn chưa nhập đầy đủ thông tin!", "Lỗi", MessageBoxButton.OK);
			}
			else
			{
				string sql1 = "Insert into NhaCungCap values('" + txt_MaNhaCC.Text + "', '" + txt_TenNhaCC.Text + "', '" + txt_DiaChi.Text + "', N'" + txt_DienThoai.Text + "', '" + txt_Email.Text + "')";
				KetNoiSQL.Them(sql1, dtgv_NhaCungCap);
				KetNoiSQL.ChuoiKetNoi(chuoi, dtgv_NhaCungCap);
				TenBang();
				Clear();
			}
		}

		public void Clear()
		{
			txt_MaNhaCC.Text = "";
			txt_TenNhaCC.Text = "";
			txt_DiaChi.Text = "";
			txt_DienThoai.Text = "";
			txt_Email.Text = "";
		}

		private void btnLamMoi_Click(object sender, EventArgs e)
		{
			KetNoiSQL.LamMoi("Select * from NhaCungCap", dtgv_NhaCungCap);
			Clear();
		}
		private void dtgv_NhaCungCap_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (dtgv_NhaCungCap.SelectedItem is DataRowView row)
			{
				txt_MaNhaCC.Text = row["maNhaCC"].ToString();
				txt_TenNhaCC.Text = row["tenNhaCC"].ToString();
				txt_DiaChi.Text = row["diaChi"].ToString();
				txt_DienThoai.Text = row["dienThoai"].ToString();
				txt_Email.Text = row["email"].ToString();
				TenBang();
				LoadData();
			}
		}

		private void btnSua_Click(object sender, EventArgs e)
		{
			string sql = "Update NhaCungCap set tenNhaCC = '" + txt_TenNhaCC.Text + "',diaChi = '" + txt_DiaChi.Text + "',dienThoai = '" + txt_DienThoai.Text + "',email = '" + txt_Email.Text + "' Where maNhaCC = '" + txt_MaNhaCC.Text + "'";
			KetNoiSQL.Sua(sql);
			KetNoiSQL.ChuoiKetNoi(chuoi, dtgv_NhaCungCap);
			TenBang();
			Clear();
		}

		private void btnTimkiem_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(txt1_TimKiem.Text))
			{
				String sqlTimKiem = "Select * from NhaCungCap where maNhaCC = '" + txt1_TimKiem.Text + "' ";
				KetNoiSQL.TimKiem(sqlTimKiem, dtgv_NhaCungCap);
			}
			else
			{
				String sqlTimKiem = "Select * from NhaCungCap";
				KetNoiSQL.TimKiem(sqlTimKiem, dtgv_NhaCungCap);
			}
		}

		private void btnXoa_Click(object sender, EventArgs e)
		{
			string sql = "Delete from NhaCungCap where maNhaCC = '" + txt_MaNhaCC.Text + "'";
			KetNoiSQL.Xoa(sql);
			KetNoiSQL.ChuoiKetNoi(chuoi, dtgv_NhaCungCap);
			TenBang();
			Clear();
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
