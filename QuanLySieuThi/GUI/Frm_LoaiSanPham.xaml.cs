using Microsoft.IdentityModel.Tokens;
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
using static System.Net.Mime.MediaTypeNames;

namespace QuanLySieuThi.GUI
{
    /// <summary>
    /// Interaction logic for Frm_LoaiSanPham.xaml
    /// </summary>
    public partial class Frm_LoaiSanPham : Window
    {
        public string chuoi = "Select * from LoaiSanPham";
        public Frm_LoaiSanPham()
        {
            InitializeComponent();

            KetNoiSQL.ChuoiKetNoi(chuoi, dtgv_LoaiSanPham);

            if (KetNoiSQL.dt != null && KetNoiSQL.dt.Rows.Count > 0)
            {
                TenBang();
            }
            else
            {
                MessageBox.Show("Loại sản phẩm không có dữ liệu !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void TenBang()
        {
            //dtgv_LoaiSanPham.Columns[0].Header = "Mã loại sản phẩm";
            //dtgv_LoaiSanPham.Columns[0].Width = 150;
            //dtgv_LoaiSanPham.Columns[1].Header = "Tên loại sản phẩm";
            //dtgv_LoaiSanPham.Columns[1].Width = 150;
            //dtgv_LoaiSanPham.Columns[2].Header = "Mô tả";
            //dtgv_LoaiSanPham.Columns[2].Width = 440;
        }

		private void btnThem_Click(object sender, RoutedEventArgs e)
		{
			string maLoaiSP = txtMaLoai.Text.Trim();
			if (string.IsNullOrEmpty(maLoaiSP))
			{
				MessageBox.Show("Mã loại sản phẩm không được để trống!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}

			foreach (var item in dtgv_LoaiSanPham.Items)
			{
				if (item is DataRowView rowView)
				{
					if (rowView["maLoaiSP"] != null && rowView["maLoaiSP"].ToString() == maLoaiSP)
					{
						MessageBox.Show("Mã loại sản phẩm đã tồn tại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
						txtMaLoai.Text = "";
						return;
					}
				}
			}

			if (string.IsNullOrEmpty(txtTenLoai.Text) || string.IsNullOrEmpty(txtMoTa.Text))
			{
				MessageBox.Show("Bạn chưa nhập đầy đủ thông tin!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
			}
			else
			{
				string sql1 = $"INSERT INTO LoaiSanPham (maLoaiSP, tenLoaiSP, moTa) VALUES (N'{txtMaLoai.Text.Trim()}', N'{txtTenLoai.Text.Trim()}', N'{txtMoTa.Text.Trim()}')";
				KetNoiSQL.Them(sql1, dtgv_LoaiSanPham);
				KetNoiSQL.ChuoiKetNoi(chuoi, dtgv_LoaiSanPham);
				TenBang();
			}

			Clear();
		}

		public void Clear()
        {
            txtMaLoai.Text = "";
            txtTenLoai.Text = "";
            txtMoTa.Text = "";
        }

        private void btnLamMoi_Click(object sender, RoutedEventArgs e)
        {
            KetNoiSQL.LamMoi("Select * from LoaiSanPham", dtgv_LoaiSanPham); 
            Clear();
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            string sql = "delete from LoaiSanPham where maLoaiSP = N'" + txtMaLoai.Text.Trim() + "'";
            KetNoiSQL.Xoa(sql);
            KetNoiSQL.ChuoiKetNoi(chuoi, dtgv_LoaiSanPham);
            Clear();
        }

        private void dtgv_LoaiSanPham_SelectionChanged(object sender, SelectionChangedEventArgs e )
        {
			if (dtgv_LoaiSanPham.SelectedItem is DataRowView rowView)
			{
				// Lấy dữ liệu từ dòng được chọn và gán vào các ô nhập liệu chi tiết
				txtMaLoai.Text = rowView["maLoaiSP"].ToString();
				txtTenLoai.Text = rowView["tenLoaiSP"].ToString();
				txtMoTa.Text = rowView["moTa"].ToString();

				// Gọi hàm TenBang nếu cần thiết
				//TenBang();
			}
		}

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
			string sql1 = "Update LoaiSanPham set tenLoaiSP = '" + txtTenLoai.Text.Trim() + "', moTa = '" + txtMoTa.Text.Trim() + "' Where maLoaiSP ='" + txtMaLoai.Text.Trim() + "'";

			//string sql = "Update LoaiSanPham set tenLoaiSP = N'" + txtTenLoai.Text.Trim() + "', moTa = N'" + txtMoTa.Text.Trim() + "' where maLoaiSP = N '" + txtMaLoai.Text.Trim() + "')";
            KetNoiSQL.Sua(sql1);
            KetNoiSQL.ChuoiKetNoi(chuoi, dtgv_LoaiSanPham);
            //TenBang();
            Clear();
        }

        private void btnTim_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTimKiem.Text.Trim()))
            {
                string sqlTimkiem = "Select * from LoaiSanPham where maLoaiSP = '" + txtTimKiem.Text.Trim() + "'";
                KetNoiSQL.TimKiem(sqlTimkiem, dtgv_LoaiSanPham);
                TenBang();
            }
            else
            {
				string sqlTimkiem = "Select * from LoaiSanPham ";
				KetNoiSQL.TimKiem(sqlTimkiem, dtgv_LoaiSanPham);
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
