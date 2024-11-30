using QuanLySieuThi.GUI;
using System;
using System.Collections.Generic;
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

namespace QuanLySieuThi
{
    /// <summary>
    /// Interaction logic for Frm_DangNhap.xaml
    /// </summary>
    public partial class Frm_DangNhap : Window
    {
        public Frm_DangNhap()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private MessageBoxResult ShowConfirmMessage (string title, string message)
        {
            return MessageBox.Show(title, message, MessageBoxButton.YesNo, MessageBoxImage.Question);
        }

        private void btn_Thoat_Click(object sender, RoutedEventArgs e)
        {
            var title = "Xác nhận";
            var message = "Bạn có chắc chắn muốn hủy bỏ ?";
            var ans = ShowConfirmMessage(title, message);

            if (ans == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void btn_DangNhap_Click(object sender, RoutedEventArgs e)
        {
            string taiKhoan = txt_TaiKhoan.Text;
            string matKhau = txt_MatKhau.Password;

            if (KetNoiSQL.KiemTraDangNhap(taiKhoan, matKhau))
            {
                if (sender is Button btn && btn == btn_DangNhap)
                {
                    var newForm = new Frm_TrangChu();
                    newForm.Show();
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("Thông tin đăng nhập không chính xác. Vui lòng thử lại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Frm_DangNhap1_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
