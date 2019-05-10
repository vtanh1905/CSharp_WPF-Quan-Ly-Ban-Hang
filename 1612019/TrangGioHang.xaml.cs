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
    /// Interaction logic for TrangGioHang.xaml
    /// </summary>
    public partial class TrangGioHang : Page
    {
        Int32 TongGiaTri;
        Int32 TongTienNhap;
        Int32 TongTien;
        public TrangGioHang()
        {
            InitializeComponent();

            //Add ComboLoaiHoaDoan
            cmbLoaiHoaDon.Items.Add("Trực Tiếp"); // 0
            cmbLoaiHoaDon.Items.Add("Giao Hàng"); // 1
            cmbLoaiHoaDon.Items.Add("Đặt Cọc"); // 2
            cmbLoaiHoaDon.SelectedIndex = 0;

            //Load Dữ Liệu
            ltGioHang.ItemsSource = ListSPGHHT.dataGioHang;

            //Load Tổng Giá Tri
            TongGiaTri = 0;
            foreach(var x in ListSPGHHT.dataGioHang)
            {
                TongGiaTri += Int32.Parse(x.Price) * Int32.Parse(x.Count);
                TongTienNhap += Int32.Parse(x.PriceNhap) * Int32.Parse(x.Count);
            }
            TongTien = TongGiaTri;

            lblTongGiaTri.Content = TongGiaTri.ToString();
            lblTongTien.Content = TongTien.ToString();
        }

        bool DuocXoa = false;
        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (btnXoa.Background == Brushes.Red)
            {
                btnXoa.Background = Brushes.Black;
                DuocXoa = false;
            }
            else
            {
                btnXoa.Background = Brushes.Red;
                DuocXoa = true;
            }
            
        }

        private void ltGioHang_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                if(DuocXoa == true)
                {
                    if (MessageBox.Show("Bạn Chắc Chắn Muốn Xóa Chứ ?", "Thông Báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        ListSPGHHT.dataGioHang.RemoveAt(ltGioHang.SelectedIndex);
                        ltGioHang.ItemsSource = null;
                        ltGioHang.ItemsSource = ListSPGHHT.dataGioHang;
                    }
                }
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Bạn Chắc Chắn Muốn Xóa Tất Cả Chứ ?", "Thông Báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                ListSPGHHT.dataGioHang.Clear();
                ltGioHang.ItemsSource = null;
                ltGioHang.ItemsSource = ListSPGHHT.dataGioHang;
                lblTongTien.Content = "0";
                lblTongGiaTri.Content = "0";
                lblKhuyenMai.Content = "0";
            }
        }

        private void btnThanhToan_Click(object sender, RoutedEventArgs e)
        {
            if(cmbLoaiHoaDon.SelectedIndex != 0 && (txtTienDatCoc.Text == "" || txtCMND.Text == "" || txtHoTen.Text == "" || txtDiaChi.Text == "" || txtSDT.Text == ""))
            {
                MessageBox.Show("Vui Lòng Điền Đầy Đủ Thông Tin");
                return;
            }

            var Data = ListSPGHHT.dataGioHang;

            if(Data.Count == 0)
            {
                MessageBox.Show("Chưa Có Sản Phẩm Trong Giỏ Hàng");
                return;
            }
            var db = new MyDatabaseEntities();

            //Add Vao DATA GioHang && Cap Nhat Lai SL San Pham
            int IDGioHang = 0;
            foreach(var value in db.GioHangs.ToList())
            {
                if(value.ID >= IDGioHang)
                {
                    IDGioHang = value.ID + 1;
                }
            }
            int STT_GioHang = -1;
            foreach (var value in Data)
            {
                db.GioHangs.Add(new GioHang(){ID = IDGioHang, SanPham_ID = value.ID, SoLuong = Int32.Parse(value.Count)});
                if (STT_GioHang == -1)
                {
                    db.SaveChanges();
                    STT_GioHang = db.GioHangs.ToList().Last().STT;
                }
                var SanPham = db.SanPhams.Find(value.ID);
                SanPham.SoLuong -= Int32.Parse(value.Count);
            }
            

            //Add Database HoaDon
            if(cmbLoaiHoaDon.SelectedIndex == 0)
            {
                if(IDMaKhuyenMai == -1)
                {
                    db.DonHangs.Add(new DonHang() { GioHang_STT = STT_GioHang, LoaiThanhDon = cmbLoaiHoaDon.SelectedIndex, TinhTrang = 1, NgayTao = DateTime.Now, TongGiaTriBan = TongTien, TongGiaTriNhap = TongTienNhap, TienConLaiPhaiTra = 0 });
                }
                else
                {
                    db.DonHangs.Add(new DonHang() { GioHang_STT = STT_GioHang, MaKhuyenMai_ID = IDMaKhuyenMai, LoaiThanhDon = cmbLoaiHoaDon.SelectedIndex, TinhTrang = 1, NgayTao = DateTime.Now, TongGiaTriBan = TongTien, TongGiaTriNhap = TongTienNhap, TienConLaiPhaiTra = 0 });
                }
            }
            else
            {
                //Add Thong Tin Khach Hang
                KhachHang KH = db.KhachHangs.Find(txtCMND.Text);
                if (KH == null)
                {
                    db.KhachHangs.Add(new KhachHang() { CMND = txtCMND.Text, HoTen = txtHoTen.Text, SDT = txtSDT.Text, DiaChi = txtDiaChi.Text });
                }
                else
                {
                    KH.HoTen = txtHoTen.Text;
                    KH.SDT = txtSDT.Text;
                    KH.DiaChi = txtDiaChi.Text;
                }

                //Add Thong Tin Hoa Don
                if (IDMaKhuyenMai == -1)
                {
                    db.DonHangs.Add(new DonHang() { GioHang_STT = STT_GioHang, KhachHang_CMND = txtCMND.Text, LoaiThanhDon = cmbLoaiHoaDon.SelectedIndex, TinhTrang = 0, NgayTao = DateTime.Now, TongGiaTriBan = TongTien, TongGiaTriNhap = TongTienNhap, TienConLaiPhaiTra = TongTien - Int32.Parse(txtTienDatCoc.Text) });
                }
                else
                {
                    db.DonHangs.Add(new DonHang() { GioHang_STT = STT_GioHang, MaKhuyenMai_ID = IDMaKhuyenMai, KhachHang_CMND = txtCMND.Text, LoaiThanhDon = cmbLoaiHoaDon.SelectedIndex, TinhTrang = 0, NgayTao = DateTime.Now, TongGiaTriBan = TongTien, TongGiaTriNhap = TongTienNhap, TienConLaiPhaiTra = TongTien - Int32.Parse(txtTienDatCoc.Text) });
                }
                
            }

            db.SaveChanges();
            MessageBox.Show("Thành Công");
            ListSPGHHT.dataGioHang.Clear();
            Dashboard.Tabs.SelectedIndex = 0; // Ve Trang San Pham
        }

        private void cmbLoaiHoaDon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cmbLoaiHoaDon.SelectedIndex == 0)
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
                txtTienDatCoc.Text = TongTien.ToString();
            }
        }

        int IDMaKhuyenMai = -1;
        private void btnKTKhuyenMai_Click(object sender, RoutedEventArgs e)
        {
            var db = new MyDatabaseEntities();
            
            foreach(var value in db.MaKhuyenMais)
            {
                if(txtMaKhuyenMai.Text == value.Ten)
                {
                    IDMaKhuyenMai = value.ID;
                    if (value.Loai == "Tiền Cố Định")
                    {
                        lblKhuyenMai.Content = "- " + value.GiaTri;
                        TongTien = TongGiaTri - int.Parse(value.GiaTri);
                        lblTongTien.Content = TongTien.ToString();
                        return;
                    }
                    else
                    {
                        float PhanTramKhuyenMai = (float.Parse(value.GiaTri) / 100);
                        float TienGiam = TongGiaTri * PhanTramKhuyenMai;
                        lblKhuyenMai.Content = "- " + (TongGiaTri * int.Parse(value.GiaTri) / 100);
                        TongTien = TongGiaTri - (int)TienGiam;
                        lblTongTien.Content = TongTien.ToString();
                        return;
                    }
                    
                }
            }

            MessageBox.Show("Mã Khuyến Mãi Không Tồn Tại");
        }

        private void btnKiemTraCMND_Click(object sender, RoutedEventArgs e)
        {
            if(txtCMND.Text != "")
            {
                var db = new MyDatabaseEntities();
                KhachHang KH = db.KhachHangs.Find(txtCMND.Text);
                if(KH != null)
                {
                    txtHoTen.Text = KH.HoTen;
                    txtSDT.Text = KH.SDT;
                    txtDiaChi.Text = KH.DiaChi;
                }
            }
        }
    }
}
