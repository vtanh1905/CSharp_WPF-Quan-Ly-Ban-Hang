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
    /// Interaction logic for TrangHoaDon.xaml
    /// </summary>
    public partial class TrangHoaDon : Page
    {
        public TrangHoaDon()
        {
            InitializeComponent();

            //Add ComboLoaiHoaDoan
            cmbLoaiHoaDon.Items.Add("Trực Tiếp"); // 0
            cmbLoaiHoaDon.Items.Add("Giao Hàng"); // 1
            cmbLoaiHoaDon.Items.Add("Đặt Cọc"); // 2
            cmbLoaiHoaDon.SelectedIndex = 0;

            //Load Thong tin Don Hang
            int ID_HD_Load = TrangDonHang.IDofItemSelected;
            var db = new MyDatabaseEntities();
            DonHang x = db.DonHangs.Find(ID_HD_Load);

            if(x.TinhTrang == 1)
            {
                btnThanhToan.Visibility = Visibility.Hidden;
            }

            lblTongTien.Content = string.Format("{0:0,0}", x.TongGiaTriBan);
            txtTienDatCoc.Text = string.Format("{0:0,0}", (x.TongGiaTriBan - x.TienConLaiPhaiTra));
            if (x.MaKhuyenMai != null)
            {
                txtMaKhuyenMai.Text = x.MaKhuyenMai.Ten;
                if (x.MaKhuyenMai.Loai == "Tiền Cố Định")
                {
                    lblKhuyenMai.Content = string.Format("- {0:0,0}", Int32.Parse(x.MaKhuyenMai.GiaTri));
                    lblTongGiaTri.Content = string.Format("{0:0,0}", (x.TongGiaTriBan + Int32.Parse(x.MaKhuyenMai.GiaTri)));
                }
                else
                {
                    float GiaTriKM = float.Parse(x.MaKhuyenMai.GiaTri);
                    float PhanChia = (1 - GiaTriKM / 100);
                    lblTongGiaTri.Content = string.Format("{0:0,0}", ((float)x.TongGiaTriBan / PhanChia));
                    lblKhuyenMai.Content = string.Format("- {0:0,0}", ((float)x.TongGiaTriBan / PhanChia) * GiaTriKM / 100);
                }
            }
            else
            {
                lblTongGiaTri.Content = lblTongTien.Content;
            }


            cmbLoaiHoaDon.SelectedIndex = (int)x.LoaiThanhDon;
            if(x.KhachHang_CMND != null)
            {
                txtCMND.Text = x.KhachHang_CMND;
                txtHoTen.Text = x.KhachHang.HoTen;
                txtSDT.Text = x.KhachHang.SDT;
                txtDiaChi.Text = x.KhachHang.DiaChi;
            }
            

            //Load ltGioHang
             List<SanPhamGioHangHienThi> Temp = new List<SanPhamGioHangHienThi>();
            foreach(var value in db.GioHangs)
            {
                if(x.GioHang.ID == value.ID)
                {
                    SanPham SP = db.SanPhams.Find(value.SanPham_ID);
                    //Convert Ảnh
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(SP.HinhAnh);
                    ms.Seek(0, System.IO.SeekOrigin.Begin);
                    BitmapImage newBitmapImage = new BitmapImage();
                    newBitmapImage.BeginInit();
                    newBitmapImage.StreamSource = ms;
                    newBitmapImage.EndInit();

                    Temp.Add(new SanPhamGioHangHienThi() { PathImage = newBitmapImage, Name = SP.Ten, Price = string.Format("{0}", (int)SP.GiaBan),  Count = value.SoLuong.ToString() });
                }
            }
            ltGioHang.ItemsSource = Temp;
        }

        

        private void imgBack_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TrangDonHang.ltDonHang_Temp.Items.Refresh();
            TrangDonHang.ltDonHang_Temp.ItemsSource = ListDHHT.data;
            NavigationService.GoBack();
        }

        private void cmbLoaiHoaDon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbLoaiHoaDon.SelectedIndex == 0)
            {
                lblTienDatCoc.Visibility = Visibility.Hidden;
                txtTienDatCoc.Visibility = Visibility.Hidden;
                gbThongTinKhachHang.Visibility = Visibility.Hidden;
            }
            else
            {
                lblTienDatCoc.Visibility = Visibility.Visible;
                txtTienDatCoc.Visibility = Visibility.Visible;
                gbThongTinKhachHang.Visibility = Visibility.Visible;
            }
        }

        private void btnThanhToan_Click(object sender, RoutedEventArgs e)
        {
            int ID_HD_Load = TrangDonHang.IDofItemSelected;
            var db = new MyDatabaseEntities();
            DonHang x = db.DonHangs.Find(ID_HD_Load);
            x.TinhTrang = 1;
            db.SaveChanges();

            foreach(var value in ListDHHT.data)
            {
                if(value.ID == x.ID)
                {
                    value.TinhTrang = new BitmapImage(new Uri(@"images/checking-mark-circle.png", UriKind.Relative));
                    break;
                }
            }
            MessageBox.Show("Thành Công");
            btnThanhToan.Visibility = Visibility.Hidden;
        }
    }
}
