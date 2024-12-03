using QuanLySieuThi.GUI;
using System.Data;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace QuanLySieuThi
{
    /// <summary>
    /// Interaction logic for Frm_NhanVien.xaml
    /// </summary>
    public partial class Frm_NhanVien : Window
    {
        string chuoi = "select * from NhanVien";
        private DuLieuCBX duLieuCBX = new DuLieuCBX();

        public Frm_NhanVien()
        {
            InitializeComponent();
            duLieuCBX.sqlMaChucVu = "select maChucVu from ChucVu";
            duLieuCBX.tenCotMaChucVu = "maChucVu";
            KetNoiSQL.XuLyCBX(duLieuCBX.sqlMaChucVu, cbb_ChucVu, duLieuCBX.tenCotMaChucVu);
            KetNoiSQL.ChuoiKetNoi(chuoi, dtgvNhanVien);
            //TenBang()
            LoadData();
        }

		private void LoadData()
		{
			try
			{
				// Tải dữ liệu lên DataGrid
				KetNoiSQL.ChuoiKetNoi(chuoi, dtgvNhanVien);

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


		public void TenBang()
        {
            //dtgvNhanVien.Columns[0].Header = "Mã nhân viên";
            //dtgvNhanVien.Columns[1].Header = "Tên nhân viên";
            //dtgvNhanVien.Columns[2].Header = "Ngày sinh";
            //dtgvNhanVien.Columns[3].Header = "Giới tính";
            //dtgvNhanVien.Columns[4].Header = "Điện thoại";
            //dtgvNhanVien.Columns[5].Header = "Mã chức vụ";
        }


        private MessageBoxResult ShowConfirmMessage(string title, string message)
        {
            return MessageBox.Show(title, message, MessageBoxButton.YesNo, MessageBoxImage.Question);
        }

        private void btnLamMoi_Click(object sender, RoutedEventArgs e)
        {
            KetNoiSQL.LamMoi(chuoi, dtgvNhanVien);
            Clear();
        }
        public void Clear()
        {
            txtMaNV.Text = "";
            txtHoTen.Text = "";
            dpNgaySinh.Text = "";
            txtSDT.Text = "";
            cbb_ChucVu.Text = "";
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            string gioiTinh = "";
            if (rbNam.IsChecked == true)
            {
                gioiTinh = "Nam";
            }
            else if (rbNu.IsChecked == true)
            {
                gioiTinh = "Nữ";
            }

            else
            {
                MessageBox.Show("Vui lòng chọn giới tính.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            string maNV = txtMaNV.Text.Trim();
            if (string.IsNullOrEmpty(maNV))
            {
                return;
            }
            else
            {
                foreach (var item in dtgvNhanVien.Items)
                {
                    if (item is DataRowView rowView)
                    {
                        if (rowView["maNV"] != null && rowView["maNV"].ToString() == maNV)
                        {
                            MessageBox.Show("Mã nhân viên đã tồn tại", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                            txtMaNV.Text = "";
                            return;
                        }
                    }
                }
                if (txtMaNV.Text == "" || txtHoTen.Text == "" || dpNgaySinh.Text == "" || gioiTinh == "" || txtSDT.Text == "")
                {
                    MessageBox.Show("Bạn chưa nhập đầy đủ thông tin!", "Lỗi", MessageBoxButton.OK);
                }
                else
                {
					string ngaySinh = DateTime.ParseExact(dpNgaySinh.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
					string sql = $"INSERT INTO NhanVien (maNV, hoTen, ngaySinh, gioiTinh, dienThoai, maChucVu) VALUES ('{txtMaNV.Text}', '{txtHoTen.Text}', '{ngaySinh}', '{gioiTinh}', '{txtSDT.Text}', '{cbb_ChucVu.Text}')";
                    KetNoiSQL.Them(sql, dtgvNhanVien);
                    KetNoiSQL.ChuoiKetNoi(chuoi, dtgvNhanVien);
                    //TenBang();
                    Clear();
                }
            }
        }

		private void RadioButton_Checked(object sender, RoutedEventArgs e)
		{
			string gioiTinh = XuLyRadioButton();
			MessageBox.Show($"Giới tính được chọn: {gioiTinh}");
		}
		public string XuLyRadioButton()
		{
			if (rbNam.IsChecked == true)
			{
				return "Nam";
			}
			else if (rbNu.IsChecked == true)
			{
				return "Nữ";
			}
			return string.Empty;
		}

		private void dtgvNhanVien_Click(object sender, SelectionChangedEventArgs e)
        {
			if (dtgvNhanVien.SelectedItem is DataRowView rowView)
			{
				string gioiTinh = "";
				string gioiTinh1 = XuLyRadioButton();
				txtMaNV.Text = rowView["maNV"].ToString();
				txtHoTen.Text = rowView["hoTen"].ToString();
				dpNgaySinh.Text = rowView["ngaySinh"].ToString();
				gioiTinh1 = rowView["gioiTinh"].ToString();
				txtSDT.Text = rowView["dienThoai"].ToString();
				cbb_ChucVu.Text = rowView["maChucVu"].ToString();
				TenBang();
			}
		}

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            string gioiTinh = "";
			string gioiTinh1 = XuLyRadioButton();
			string ngaySinh = dpNgaySinh.SelectedDate.Value.ToString("yyyy-MM-dd");
			string sql = $"UPDATE NhanVien SET hoTen = '{txtHoTen.Text}', ngaySinh = '{ngaySinh}', gioiTinh = '{gioiTinh1}', dienThoai = '{txtSDT.Text}', maChucVu = '{cbb_ChucVu.Text}' WHERE maNV = '{txtMaNV.Text}'";
			KetNoiSQL.Sua(sql);
            KetNoiSQL.ChuoiKetNoi(chuoi, dtgvNhanVien);
            TenBang();
            Clear();
        }


		private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            string sql = "Delete from NhanVien where maNV = '" + txtMaNV.Text + "'";
            KetNoiSQL.Xoa(sql);
            KetNoiSQL.ChuoiKetNoi(chuoi, dtgvNhanVien);
            TenBang();
            Clear();
        }

        private void btnTim_Click(object sender, RoutedEventArgs e)
        {

			if (!string.IsNullOrEmpty(txtTimKiem.Text))
			{
			    String sqlTimKiem = "Select * from NhanVien where maNV = '" + txtTimKiem.Text + "'";
                KetNoiSQL.TimKiem(sqlTimKiem, dtgvNhanVien);
			}
			else
			{
				String sqlTimKiem = "Select * from NhanVien";
				KetNoiSQL.TimKiem(sqlTimKiem, dtgvNhanVien);
			}
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
		private void Menu_ChucVu_Click(object sender, RoutedEventArgs e)
		{
			Frm_ChucVu frm_ChucVu = new Frm_ChucVu();
			frm_ChucVu.Show();

			this.Close();
		}

		//private MessageBoxResult ShowConfirmMessage(string title, string message)
		//{
		//	return MessageBox.Show(title, message, MessageBoxButton.YesNo, MessageBoxImage.Question);
		//}
	}
}
