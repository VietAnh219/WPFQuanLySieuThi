using Azure;
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
    /// Interaction logic for Frm_Nhom8.xaml
    /// </summary>
    public partial class Frm_Nhom9 : Window
    {
        public Frm_Nhom9()
        {
            InitializeComponent();
		}

        public void btn_Navi (object sender, RoutedEventArgs e)
        {
            Frm_DangNhap frm_DangNhap = new Frm_DangNhap();
            frm_DangNhap.Show();

            this.Close();
        }
		private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
