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
    /// Interaction logic for TrangDonHang.xaml
    /// </summary>
    public partial class TrangDonHang : Page
    {

        static public ListView ltDonHang_Temp;

        public TrangDonHang()
        {
            InitializeComponent();

            ltDonHang_Temp = ltDonHang;

            LoadListView();
        }

        public void LoadListView()
        {
            MyDatabaseEntities db = new MyDatabaseEntities();
            foreach (var value in db.DonHangs.ToList())
            {
                DonHangHienThi x = new DonHangHienThi();


                if (value.TinhTrang == 0)
                {
                    x.TinhTrang = new BitmapImage(new Uri(@"images/cancel-button.png", UriKind.Relative));
                }
                else
                {
                    x.TinhTrang = new BitmapImage(new Uri(@"images/checking-mark-circle.png", UriKind.Relative));
                }

                if (value.LoaiThanhDon == 0)
                {
                    x.LoaiDonHang = new BitmapImage(new Uri(@"images/online-shopping-cart.png", UriKind.Relative));
                }
                else if (value.LoaiThanhDon == 1)
                {
                    x.LoaiDonHang = new BitmapImage(new Uri(@"images/delivery-truck.png", UriKind.Relative));
                }
                else
                {
                    x.LoaiDonHang = new BitmapImage(new Uri(@"images/coins.png", UriKind.Relative));
                }

                if (value.KhachHang_CMND != null)
                {
                    x.CMND = value.KhachHang_CMND;
                    x.HoTen = value.KhachHang.HoTen;
                    x.SDT = value.KhachHang.SDT;
                    x.DiaChi = value.KhachHang.DiaChi;
                    x.TienConLai = string.Format("{0:0,0}", value.TienConLaiPhaiTra);
                }
                else
                {
                    x.TienConLai = "0";
                }
                x.ID = value.ID;
                x.Ngay = (DateTime)value.NgayTao;
                x.TongTien = string.Format("{0:0,0}", value.TongGiaTriBan);


                ListDHHT.data.Add(x);
            }

            ltDonHang.ItemsSource = ListDHHT.data;
        }

        private void btnTatCa_Click(object sender, RoutedEventArgs e)
        {
            
        }

        static public int IDofItemSelected = -1;
        private void ltDonHang_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                DonHangHienThi temp = (DonHangHienThi)ltDonHang.SelectedItem;
                IDofItemSelected = temp.ID;
                NavigationService.Navigate(new TrangHoaDon());
            }
        }
    }
}
