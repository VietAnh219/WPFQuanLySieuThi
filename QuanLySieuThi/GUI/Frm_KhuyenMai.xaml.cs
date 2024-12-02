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
    /// Interaction logic for Frm_KhuyenMai.xaml
    /// </summary>
    public partial class Frm_KhuyenMai : Window
    {
		public string chuoi = "Select * from KhuyenMai";
		private DuLieuCBX duLieuCBX = new DuLieuCBX();
		public Frm_KhuyenMai()
		{
			InitializeComponent();

			duLieuCBX.sqlMaKH = "select maKH from KhachHang";
			duLieuCBX.tenCotMaKH = "maKH";
			KetNoiSQL.XuLyCBX(duLieuCBX.sqlMaKH, cbbMaKH, duLieuCBX.tenCotMaKH);
			KetNoiSQL.XuLyCBX(duLieuCBX.sqlMaKH, cbbMaKH1, duLieuCBX.tenCotMaKH);
			KetNoiSQL.ChuoiKetNoi(chuoi, dtgvKhuyenMai);
			if (KetNoiSQL.dt != null && KetNoiSQL.dt.Rows.Count > 0)
			{
				TenBang();
			}
			else
			{
				MessageBox.Show("Khuyến mãi không có dữ liệu !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
			}
		}

		private void LoadData()
		{
			try
			{
				// Tải dữ liệu lên DataGrid
				KetNoiSQL.ChuoiKetNoi(chuoi, dtgvKhuyenMai);

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
		}

		private void dtgvKhuyenMai_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (dtgvKhuyenMai.SelectedItem is DataRowView row)
			{
				txtMaKM.Text = row["maKM"].ToString();
				txtPhanTramKM.Text = row["phanTramKhuyenMai"].ToString();

				string ngayBDString = row["ngayBatDau"].ToString();
				DateTime ngayBD;
				if (DateTime.TryParse(ngayBDString, out ngayBD))
				{
					dpNgayBD.SelectedDate = ngayBD;
				}

				string ngayKTString = row["ngayKetThuc"].ToString();
				DateTime ngayKT;
				if (DateTime.TryParse(ngayKTString, out ngayKT))
				{
					dpNgayKT.SelectedDate = ngayKT;
				}

				cbbMaKH.Text = row["maKH"].ToString();
				txtMoTa.Text = row["moTa"].ToString();
				//TenBang();
				LoadData();
			}
		}

		private void btnThem_Click(object sender, RoutedEventArgs e)
		{
			string maKhuyenMai = txtMaKM.Text.Trim();
			if (string.IsNullOrEmpty(maKhuyenMai))
			{
				return;
			}
			else
			{
				foreach (var item in dtgvKhuyenMai.Items)
				{
					DataRowView row = item as DataRowView;
					if (row != null && row[0] != null && row[0].ToString() == maKhuyenMai)
					{
						MessageBox.Show("Mã khuyến mãi đã tồn tại !", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
						txtMaKM.Text = "";
						return;
					}
				}
				// Lấy giá trị ngày tháng năm từ DatutinePicker
				string ngayBD = dpNgayBD.SelectedDate?.ToString("yyyy-MM-dd") ?? string.Empty;
				string ngayKT = dpNgayKT.SelectedDate?.ToString("yyyy-MM-dd") ?? string.Empty;

				if (txtMaKM.Text == "" || txtPhanTramKM.Text == "" || txtMoTa.Text == "")
				{
					MessageBox.Show("Bạn chưa nhập đầy đủ thông tin", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
				}
				else
				{
					string sql1 = "Insert into KhuyenMai Values(N'" + txtMaKM.Text + "', '" + float.Parse(txtPhanTramKM.Text) + "', '" + ngayBD + "', '" + ngayKT + "', '" + cbbMaKH.Text + "', N'" + txtMoTa.Text + "' )";
					KetNoiSQL.Them(sql1, dtgvKhuyenMai);
					KetNoiSQL.ChuoiKetNoi(chuoi, dtgvKhuyenMai);
					TenBang();
				}
			}
			Clear();
		}

		private void Clear()
		{
			txtMaKM.Text = "";
			cbbMaKH.Text = "";
			txtPhanTramKM.Clear();
			txtMoTa.Text = "";
		}

		private void btnLamMoi_Click(object sender, RoutedEventArgs e)
		{
			KetNoiSQL.LamMoi("Select * from KhuyenMai", dtgvKhuyenMai);
			Clear();
		}

		private void btnXoa_Click(object sender, RoutedEventArgs e)
		{
			string sql = "delete from KhuyenMai where maKM = N'" + txtMaKM.Text + "'";
			KetNoiSQL.Xoa(sql);
			KetNoiSQL.ChuoiKetNoi(chuoi, dtgvKhuyenMai);
			TenBang();
			Clear();
		}

		private void btnSua_Click(object sender, RoutedEventArgs e)
		{
			string ngayBD = dpNgayBD.SelectedDate?.ToString("yyyy-MM-dd") ?? string.Empty;
			string ngayKT = dpNgayKT.SelectedDate?.ToString("yyyy-MM-dd") ?? string.Empty;
			string sql = "Update KhuyenMai set phanTramkhuyenMai = '" + float.Parse(txtPhanTramKM.Text) + "', ngayBatDau = '" + ngayBD + "', ngayKetThuc = '" + ngayKT + "', maKH = (SELECT maKH FROM KhachHang WHERE maKH = '" + cbbMaKH.Text + "'),moTa = '" + txtMoTa.Text + "' Where maKM ='" + txtMaKM.Text + "'";
			KetNoiSQL.Sua(sql);
			KetNoiSQL.ChuoiKetNoi(chuoi, dtgvKhuyenMai);
			TenBang();
			Clear();
		}

		private void btnTim_Click(object sender, RoutedEventArgs e)
		{
			string maKHTimKiem = cbbMaKH1.Text;
			string sqlTimKiem = "Select * from KhuyenMai where maKH LIKE '%" + maKHTimKiem + "%'";
			KetNoiSQL.TimKiem(sqlTimKiem, dtgvKhuyenMai);
			TenBang();
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
