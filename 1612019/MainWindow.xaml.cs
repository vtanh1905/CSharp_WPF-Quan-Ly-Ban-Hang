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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _1612019
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Window layout;
        public MainWindow()
        {
            InitializeComponent();
            layout = layoutLogin;
        }

        private void btnDangNhap_Click(object sender, RoutedEventArgs e)
        {
            if(txtUsername.Text == "" && txtPassword.Password == "")
            {
                MessageBox.Show("Vui Lòng Nhập Username và Password", "Lỗi Đăng Nhập", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if(txtUsername.Text == "admin" && txtPassword.Password == "123")
            {
                //MessageBox.Show("Đúng");
                layoutLogin.Visibility = Visibility.Hidden;
                Dashboard dashboard = new Dashboard();
                dashboard.Show();
            }
            else
            {
                MessageBox.Show("Username hoặc Password Không Chính Xác", "Lỗi Đăng Nhập", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
