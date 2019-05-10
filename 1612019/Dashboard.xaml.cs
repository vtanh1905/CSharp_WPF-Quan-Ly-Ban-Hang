using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        public static TabControl Tabs;

        public Dashboard()
        {
            InitializeComponent();
            Tabs = tabs;

            Canvas.SetZIndex(lblMarkerSanPham, 2);
        }

       

        private void layoutDashboard_Closed(object sender, EventArgs e)
        {
            MainWindow.layout.Visibility = Visibility.Visible;
        }

        private void Button_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }

        ObservableCollection<TabItem> screens = new ObservableCollection<TabItem>();

        private void layoutDashboard_Loaded(object sender, RoutedEventArgs e)
        {
            screens = new ObservableCollection<TabItem>()
            {
                new TabItem() {
                    Header ="Sản phẩm",
                    Content = new Frame() {
                        NavigationUIVisibility = NavigationUIVisibility.Hidden,
                        Content = new DanhSachSanPham()
                    },
                },

                 new TabItem() {
                    Header ="Giỏ Hàng",
                    Content = new Frame() {
                        NavigationUIVisibility = NavigationUIVisibility.Hidden,
                        Content = new TrangGioHang()
                    },
                 },

                new TabItem() {
                    Header ="Đơn Hàng",
                    Content = new Frame() {
                        NavigationUIVisibility = NavigationUIVisibility.Hidden,
                        Content = new TrangDonHang()
                    },
                 },

                new TabItem() {
                    Header ="Khuyến Mãi",
                    Content = new Frame() {
                        NavigationUIVisibility = NavigationUIVisibility.Hidden,
                        Content = new TrangMaKhuyenMai()
                    },
                 },

                new TabItem() {
                    Header ="Thống Kê",
                    Content = new Frame() {
                        NavigationUIVisibility = NavigationUIVisibility.Hidden,
                        Content = new TrangThongKe()
                    },
                 }
             };
            
            tabs.ItemsSource = screens;
            tabs.SelectedIndex = 0;
        }

        private void btnSanPham_Click(object sender, RoutedEventArgs e)
        {
            Canvas.SetZIndex(lblMarkerSanPham, 2);
            Canvas.SetZIndex(lblMarkerGioHang, 1);
            Canvas.SetZIndex(lblMarkerHoaDon, 1);
            Canvas.SetZIndex(lblMarkerKhuyenMai, 1);
            Canvas.SetZIndex(lblMarkerThongKe, 1);
            tabs.SelectedIndex = 0;
        }

        private void btnGioHang_Click(object sender, RoutedEventArgs e)
        {
            Canvas.SetZIndex(lblMarkerSanPham, 1);
            Canvas.SetZIndex(lblMarkerGioHang, 2);
            Canvas.SetZIndex(lblMarkerHoaDon, 1);
            Canvas.SetZIndex(lblMarkerKhuyenMai, 1);
            Canvas.SetZIndex(lblMarkerThongKe, 1);
            // Load 1 Page Mới Tranh TH Bi Loi Break Mode
            screens[1] = new TabItem()
            {
                Header = "Giỏ Hàng",
                Content = new Frame()
                {
                    NavigationUIVisibility = NavigationUIVisibility.Hidden,
                    Content = new TrangGioHang()
                },
            };
            tabs.ItemsSource = screens;
            //=============================================
            tabs.SelectedIndex = 1;
        }

        private void btnHoaDon_Click(object sender, RoutedEventArgs e)
        {
            Canvas.SetZIndex(lblMarkerSanPham, 1);
            Canvas.SetZIndex(lblMarkerGioHang, 1);
            Canvas.SetZIndex(lblMarkerHoaDon, 2);
            Canvas.SetZIndex(lblMarkerKhuyenMai, 1);
            Canvas.SetZIndex(lblMarkerThongKe, 1);

            // Load 1 Page Mới Tranh TH Bi Loi Break Mode
            screens[2] = new TabItem()
            {
                Header = "Đơn Hàng",
                Content = new Frame()
                {
                    NavigationUIVisibility = NavigationUIVisibility.Hidden,
                    Content = new TrangDonHang()
                },
            };
            tabs.ItemsSource = screens;
            //=============================================
            tabs.SelectedIndex = 2;

        }

        private void btnKhuyenMai_Click(object sender, RoutedEventArgs e)
        {
            Canvas.SetZIndex(lblMarkerSanPham, 1);
            Canvas.SetZIndex(lblMarkerGioHang, 1);
            Canvas.SetZIndex(lblMarkerHoaDon, 1);
            Canvas.SetZIndex(lblMarkerKhuyenMai, 2);
            Canvas.SetZIndex(lblMarkerThongKe, 1);
            tabs.SelectedIndex = 3;
        }

        private void btnThongKe_Click(object sender, RoutedEventArgs e)
        {
            Canvas.SetZIndex(lblMarkerSanPham, 1);
            Canvas.SetZIndex(lblMarkerGioHang, 1);
            Canvas.SetZIndex(lblMarkerHoaDon, 1);
            Canvas.SetZIndex(lblMarkerKhuyenMai, 1);
            Canvas.SetZIndex(lblMarkerThongKe, 2);
            tabs.SelectedIndex = 4;
        }

        private void btnThoat_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

       
    }
}
