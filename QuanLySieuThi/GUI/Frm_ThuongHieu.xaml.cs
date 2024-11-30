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
    /// Interaction logic for Frm_ThuongHieu.xaml
    /// </summary>
    public partial class Frm_ThuongHieu : Window
    {
		public string chuoi = "Select * from ThuongHieu";
		public Frm_ThuongHieu()
		{
			InitializeComponent();
			LoadData();
		}

		private void LoadData()
		{
			KetNoiSQL.ChuoiKetNoi(chuoi, dtgv_ThuongHieu);
			if (KetNoiSQL.dt != null && KetNoiSQL.dt.Rows.Count > 0)
			{
			}
			else
			{
				MessageBox.Show("Thương hiệu không có dữ Liệu1", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
			}
		}

		private void btnThem_Click(object sender, EventArgs e)
		{
			string maThuongHieu = txt_MaThuongHieu.Text.Trim();

			if (string.IsNullOrEmpty(maThuongHieu))
			{
				return;
			}
			else
			{
				foreach (var item in dtgv_ThuongHieu.Items)
				{
					DataRowView row = item as DataRowView;
					if (row != null && row[0] != null && row[0].ToString() == maThuongHieu)
					{
						MessageBox.Show("Mã thương hiệu đã tồn tại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
						txt_MaThuongHieu.Text = "";
						return;
					}
				}

				if (txt_MaThuongHieu.Text == "" || txt_TenThuongHieu.Text == "" || txt_QuocGia.Text == "")
				{
					MessageBox.Show("Bạn chưa nhập đầy đủ thông tin!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
				}
				else
				{
					string sql1 = "Insert into ThuongHieu values(N'" + txt_MaThuongHieu.Text + "', N'" + txt_TenThuongHieu.Text + "', N'" + txt_QuocGia.Text + "', N'" + txt_MoTa.Text + "')";
					KetNoiSQL.Them(sql1, dtgv_ThuongHieu);

					KetNoiSQL.ChuoiKetNoi(chuoi, dtgv_ThuongHieu);
				}
			}
		}

		private void Clear()
		{
			txt_MaThuongHieu.Text = "";
			txt_TenThuongHieu.Text = "";
			txt_QuocGia.Text = "";
			txt_MoTa.Text = "";
		}
		private void btnLamMoi_Click(object sender, EventArgs e)
		{
			KetNoiSQL.LamMoi("Select * from ThuongHieu", dtgv_ThuongHieu);
			Clear();
		}

		private void btnXoa_Click(object sender, EventArgs e)
		{
			string sql = "delete from ThuongHieu where maThuongHieu = N'" + txt_MaThuongHieu.Text.Trim() + "'";
			KetNoiSQL.Xoa(sql);
			KetNoiSQL.ChuoiKetNoi(chuoi, dtgv_ThuongHieu);
			Clear();
		}

		private void dtgv_ThuongHieu_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (dtgv_ThuongHieu.SelectedItem is DataRowView row)
			{
				txt_MaThuongHieu.Text = row["maThuongHieu"].ToString();
				txt_TenThuongHieu.Text = row["tenThuongHieu"].ToString();
				txt_QuocGia.Text = row["quocGia"].ToString();
				txt_MoTa.Text = row["moTa"].ToString();
				LoadData();
			}
		}

		private void btnSua_Click(object sender, EventArgs e)
		{
			string sql = $"UPDATE ThuongHieu SET tenThuongHieu = '{txt_TenThuongHieu.Text}', quocGia = '{txt_QuocGia.Text}', moTa = '{txt_MoTa.Text}'  WHERE maThuongHieu = '{txt_MaThuongHieu.Text}'";
			KetNoiSQL.Sua(sql);
			KetNoiSQL.ChuoiKetNoi(chuoi, dtgv_ThuongHieu);
			Clear();
		}

		private void btnTim_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(txt1_TenThuongHieu.Text))
			{
				string sqlTimKiem = "Select * from ThuongHieu where tenThuongHieu LIKE N'%" + txt1_TenThuongHieu.Text.Trim() + "%'";
				KetNoiSQL.TimKiem(sqlTimKiem, dtgv_ThuongHieu);
			}
			else
			{
				KetNoiSQL.ChuoiKetNoi(chuoi, dtgv_ThuongHieu);
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
