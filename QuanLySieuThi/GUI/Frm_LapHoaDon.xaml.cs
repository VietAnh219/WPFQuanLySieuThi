using Microsoft.Data.SqlClient;
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
    /// Interaction logic for Frm_LapHoaDon.xaml
    /// </summary>
    public partial class Frm_LapHoaDon : Window
    {
		private DuLieuCBX duLieuCBX = new DuLieuCBX();
		private string chuoi = "Select * from HoaDon";

		private bool conHang = true;
		public Frm_LapHoaDon()
		{
			InitializeComponent();
			//LoadComboBoxData();
			//LoadDataGridData();
			duLieuCBX.sqlMaNV = "select maNV from NhanVien";
			duLieuCBX.tenCotMaNV = "maNV";
			duLieuCBX.sqlMaKH = "select maKH from KhachHang";
			duLieuCBX.tenCotMaKH = "maKH";
			duLieuCBX.sqlMaSP = "select maSP from SanPham";
			duLieuCBX.tenCotMaSP = "maSP";
			duLieuCBX.sqlTyLeKM = $"select phanTramKhuyenMai from KhuyenMai where maKH = '{cbxPTKM.Text}'";
			duLieuCBX.tenCotTyLeKM = "phanTramKhuyenMai";

			KetNoiSQL.XuLyCBX(duLieuCBX.sqlMaNV, cbxMaNV, duLieuCBX.tenCotMaNV);
			KetNoiSQL.XuLyCBX(duLieuCBX.sqlMaKH, cbxMaKH, duLieuCBX.tenCotMaKH);
			KetNoiSQL.XuLyCBX(duLieuCBX.sqlMaSP, cbxMaSP, duLieuCBX.tenCotMaSP);
			KetNoiSQL.XuLyCBX(duLieuCBX.sqlTyLeKM, cbxPTKM, duLieuCBX.tenCotTyLeKM);
			KetNoiSQL.ChuoiKetNoi(chuoi, dtgvHD);

			if (KetNoiSQL.dt != null && KetNoiSQL.dt.Rows.Count > 0)
			{
				//TenBang();
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
				KetNoiSQL.ChuoiKetNoi(chuoi, dtgvHD);

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

		private void LoadData2()
		{
			try
			{
				// Tải dữ liệu lên DataGrid
				KetNoiSQL.ChuoiKetNoi(chuoi, dtgvChiTietHoaDon);

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
		//private void LoadDataGridData()
		//{
		//	KetNoiSQL.ChuoiKetNoi(chuoi, dtgvHD);

		//	if (dtgvChiTietHoaDon.Items.Count != 0)
		//	{
		//		TenBangHoaDonChiTiet();
		//	}

		//	if (dtgvHD.Items.Count != 0)
		//	{
		//		TenBangHoaDon();
		//	}
		//}

		private void TenBangHoaDon()
		{
			//dtgvHD.Columns[0].Header = "Mã hóa đơn";
			//dtgvHD.Columns[1].Header = "Mã nhân viên";
			//dtgvHD.Columns[2].Header = "Mã khách hàng";
			//dtgvHD.Columns[3].Header = "Ngày lập";
			//dtgvHD.Columns[4].Header = "Chú thích";
		}

		private void TenBangHoaDonChiTiet()
		{
			//if (dtgvChiTietHoaDon.Items.Count != 0)
			//{
			//	dtgvChiTietHoaDon.Columns[0].Header = "Mã sản phẩm";
			//	dtgvChiTietHoaDon.Columns[1].Header = "Số lượng";
			//	dtgvChiTietHoaDon.Columns[2].Header = "Đơn giá";
			//	dtgvChiTietHoaDon.Columns[3].Header = "Mã hóa đơn";
			//	dtgvChiTietHoaDon.Columns[4].Header = "Thành tiền";
			//	txtDonGia.IsEnabled = false;
			//}
		}

		private void Clear()
		{
			txtMaHD.Text = "";
			cbxMaNV.Text = "";
			cbxMaKH.Text = "";
			dpNgayLap.Text = "";
			txtGhiChu.Text = "";
		}

		private void Clear1()
		{
			cbxMaSP.Text = "";
			txtSoLuong.Text = "";
			txtDonGia.Text = "";
		}

		private void btnThem_Click(object sender, EventArgs e)
		{
			if (txtMaHD.Text == "" || cbxMaKH.Text == "" || cbxMaNV.Text == "" || dpNgayLap.Text == "")
			{
				MessageBox.Show("Bạn chưa nhập đầy đủ thông tin!", "Lỗi", MessageBoxButton.OK);
			}
			else
			{
				string sqlCheck = "SELECT COUNT(*) FROM HoaDon WHERE MaHD = '" + txtMaHD.Text + "'";
				int count = KetNoiSQL.Dem(sqlCheck);

				if (count > 0)
				{
					MessageBox.Show("Mã hóa đơn đã tồn tại!", "Lỗi", MessageBoxButton.OK);
				}
				else
				{
					string ngayLap = dpNgayLap.SelectedDate.Value.ToString("yyyy-MM-dd");
					string sql = "INSERT INTO HoaDon VALUES ('" + txtMaHD.Text + "','" + cbxMaNV.Text + "','" + cbxMaKH.Text + "','" + ngayLap + "','" + txtGhiChu.Text + "')";
					KetNoiSQL.Them(sql, dtgvHD);
					KetNoiSQL.ChuoiKetNoi(chuoi, dtgvHD);
					TenBangHoaDon();
					Clear1();
				}
			}
		}

		private void btnXoa_Click(object sender, EventArgs e)
		{
			string sql = "DELETE FROM HoaDon WHERE MaHD = '" + txtMaHD.Text + "'";
			KetNoiSQL.Xoa(sql);
			KetNoiSQL.ChuoiKetNoi(chuoi, dtgvHD);
			TenBangHoaDon();
		}

		private void btnSua_Click(object sender, EventArgs e)
		{
			string sql = "UPDATE HoaDon SET maNV = '" + cbxMaNV.Text + "', maKH = N'" + cbxMaKH.Text + "', NgayLap = N'" + dpNgayLap.Text + "', ghiChu = N'" + txtGhiChu.Text + "' WHERE MaHD = '" + txtMaHD.Text + "'";
			KetNoiSQL.Sua(sql);
			KetNoiSQL.ChuoiKetNoi(chuoi, dtgvHD);
			TenBangHoaDon();
			Clear();
		}

		private void btnLamMoi_Click(object sender, EventArgs e)
		{
			string chuoi1 = "SELECT * FROM HoaDonChiTiet WHERE MaHD = '" + txtMaHD.Text + "'";
			//KetNoiSQL.LamMoi(chuoi1, dtgvHD);
			Clear();
			//TongThanhTien();
		}
		private void btnThem1_Click(object sender, EventArgs e)
		{
			string maSP = cbxMaSP.Text.Trim();

			// Nếu mã sản phẩm trống thì không cần kiểm tra
			if (string.IsNullOrEmpty(maSP))
			{
				return;
			}

			// Duyệt qua từng hàng trong dtgv_PhieuNhap để kiểm tra mã sản phẩm đã tồn tại chưa
			foreach (var item in dtgvChiTietHoaDon.Items)
			{
				DataRowView row = item as DataRowView;
				if (row != null && row[0] != null && row[0].ToString() == maSP)
				{
					MessageBox.Show("Mã sản phẩm đã tồn tại trong bảng hóa đơn chi tiết!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
					cbxMaSP.Text = "";
					return;
				}
			}

			// Kiểm tra xem maPN có tồn tại trong dtgv_PN không
			bool maHDTonTai = false;
			foreach (var item in dtgvHD.Items)
			{
				DataRowView row = item as DataRowView;
				if (row != null && row[0].ToString() == txtMaHD.Text)
				{
					maHDTonTai = true;
					break;
				}
			}

			if (!maHDTonTai)
			{
				MessageBox.Show("Mã hóa đơn không tồn tại!", "Lỗi", MessageBoxButton.OK);
				return;
			}

			// Tiếp tục thêm dữ liệu vào bảng PhieuNhapChiTiet
			if (txtMaHD.Text == "" || cbxMaSP.Text == "" || txtSoLuong.Text == "" || txtDonGia.Text == "")
			{
				MessageBox.Show("Bạn chưa nhập đầy đủ thông tin!", "Lỗi", MessageBoxButton.OK);
			}
			else
			{
				string chuoi1 = "Select * From HoaDonChiTiet Where maHD = '" + txtMaHD.Text + "'";

				double soLuong = double.Parse(txtSoLuong.Text);
				double gia = double.Parse(txtDonGia.Text);

				double thanhTien = soLuong * gia;
				//GiamSoLuongTon();
				if (conHang)
				{
					string sql1 = $"INSERT INTO HoaDonChiTiet (maHD, maSP, soLuong, donGia, thanhTien) VALUES ('{txtMaHD.Text}', '{cbxMaSP.Text}', {txtSoLuong.Text}, {gia}, {thanhTien})";

					//string sql = "Insert into HoaDonChiTiet values('" + cbxMaSP.Text + "', '" + txtSoLuong.Text + "', '" + gia + "', '" + txtMaHD.Text + "','" + thanhTien + "')";
					KetNoiSQL.Them(sql1, dtgvChiTietHoaDon);
					KetNoiSQL.ChuoiKetNoi(chuoi, dtgvChiTietHoaDon);
					LoadData2();
					//TenBangHoaDonChiTiet();
					//TongThanhTien();
				}
				else
				{
					return;
				}
				Clear();
			}
		}
		private void btnXoa1_Click(object sender, EventArgs e)
		{
			string chuoi1 = "Select * From HoaDonChiTiet Where maHD = '" + txtMaHD.Text + "'";
			string sql = "Delete from HoaDonChiTiet where maSP = '" + cbxMaSP.Text + "'";
			KetNoiSQL.Xoa(sql);
			KetNoiSQL.ChuoiKetNoi(chuoi1, dtgvChiTietHoaDon);
			TenBangHoaDonChiTiet();
			//TongThanhTien();
		}
		private void cbxMaSP_SelectedIndexChanged(object sender, EventArgs e)
		{
			string maSP = cbxMaSP.Text.Trim();

			// Nếu mã sản phẩm không trống, tiếp tục lấy thông tin đơn giá
			string sql = "SELECT donGia FROM SanPham WHERE maSP = '" + maSP + "'";
			SqlDataReader reader = KetNoiSQL.Showtext(sql);

			if (reader != null && reader.HasRows)
			{
				reader.Read();
				string donGia = reader["donGia"].ToString();
				txtDonGia.Text = donGia;
			}
			reader.Close();
		}
		private void TongThanhTien()
		{
			double tongThanhTien = 0;
			if (txtTienKhach.Text == "" || cbxPTKM.Text == "")
			{
				txtTienKhach.Text = "0";
				cbxPTKM.Text = "0";
			}
			else
			{
				double phanTramKhuyenMai = double.Parse(cbxPTKM.Text);
				double tyLeKhuyenMai = phanTramKhuyenMai / 100.0;
				int tienKhach = int.Parse(txtTienKhach.Text);
				foreach (var item in dtgvChiTietHoaDon.Items)
				{
					DataRowView row = item as DataRowView;
					double thanhTien;
					if (double.TryParse(row["thanhTien"].ToString(), out thanhTien))
					{
						tongThanhTien += thanhTien;
					}
				}
				double tienGiamGia = (tongThanhTien * tyLeKhuyenMai) / 100;
				double thanhToan = tongThanhTien - tienGiamGia;
				double tienThua = tienKhach - thanhToan;
				//txtTongTien.Text = thanhToan.ToString();
				//txtTienThua.Text = tienThua.ToString();
			}
		}
		private void dtgvHD_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			duLieuCBX.sqlTyLeKM = $"select phanTramKhuyenMai from KhuyenMai where maKH = '{cbxMaKH.Text}'";
			duLieuCBX.tenCotTyLeKM = "phanTramKhuyenMai";
			//dtgvChiTietHoaDon.Columns.Clear();

			if (dtgvHD.SelectedItem is DataRowView row)
			{
				string maHoaDon = row["maHD"].ToString();
				string sql = $"Select * from HoaDonChiTiet where maHD = '{maHoaDon}'";

				txtMaHD.Text = row["maHD"].ToString();
				cbxMaNV.Text = row["maNV"].ToString();
				cbxMaKH.Text = row["maKH"].ToString();
				dpNgayLap.Text = row["ngayLap"].ToString();

				KetNoiSQL.ClickDTGV(sql, dtgvChiTietHoaDon);
				KetNoiSQL.XuLyCBX(duLieuCBX.sqlTyLeKM, cbxPTKM, duLieuCBX.tenCotTyLeKM);
				//TenBangHoaDon();
				LoadData();
				//TongThanhTien();
			}
		}
		private void dtgvChiTietHoaDon_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (dtgvChiTietHoaDon.SelectedItem is DataRowView row)
			{
				cbxMaSP.Text = row["maSP"].ToString();
				txtSoLuong.Text = row["soLuong"].ToString();
				txtDonGia.Text = row["donGia"].ToString();
				txtMaHD.Text = row["maHD"].ToString();
				LoadData();
				//TenBangHoaDonChiTiet();
			}
		}
		private void GiamSoLuongTon()
		{
			string sqlSoLuongTon = "SELECT soLuongTon FROM SanPham WHERE maSP = '" + cbxMaSP.Text.Trim() + "'";
			SqlDataReader reader = KetNoiSQL.Showtext(sqlSoLuongTon);
			if (reader != null && reader.HasRows)
			{
				reader.Read();

				int soLuongTonHienTai = Convert.ToInt32(reader["soLuongTon"]);
				int soLuongMoi = Convert.ToInt32(txtSoLuong.Text);
				int soLuongTonMoi = soLuongTonHienTai - soLuongMoi;
				if (soLuongTonMoi <= 0)
				{
					conHang = false;
					string thongBao = "Sản phẩm hiện chỉ còn " + soLuongTonHienTai + " sản phẩm tồn. Không đủ số lượng.";
					MessageBox.Show(thongBao, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
				}
				else
				{
					string sqlCapNhatSoLuong = "UPDATE SanPham SET soLuongTon = " + soLuongTonMoi + " WHERE maSP = '" + cbxMaSP.Text.Trim() + "'";
					KetNoiSQL.CapNhatSoLuongTon(sqlCapNhatSoLuong);
					conHang = true;
					MessageBox.Show("Thêm phiếu nhập chi tiết thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
				}
				reader.Close();
			}
			else
			{
				reader.Close();
				MessageBox.Show("Không tìm thấy sản phẩm trong cơ sở dữ liệu!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
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
