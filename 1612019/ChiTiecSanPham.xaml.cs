using Microsoft.Win32;
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
    /// Interaction logic for ChiTiecSanPham.xaml
    /// </summary>
    public partial class ChiTiecSanPham : Page
    {
        int ID_OFItemSelected = DanhSachSanPham.IDofItemSelected;
        BitmapImage bitmapImageSanPham;

        public ChiTiecSanPham()
        {
            InitializeComponent();

            LoadThongTinSanPham();
        }

        public void LoadThongTinSanPham()
        {
            var db = new MyDatabaseEntities();
            var Element = db.SanPhams.Find(ID_OFItemSelected);
            txtTenSanPham.Text = Element.Ten;
            txtGiaNhap.Text = ((Int32)Element.GiaNhap).ToString();
            txtGiaBan.Text = ((Int32)Element.GiaBan).ToString();
            txtSoLuong.Text = Element.SoLuong.ToString();

            cmbHangSanXuat.Items.Clear();
            cmbHangSanXuat.Items.Add(Element.LoaiSanPham.HangSanXuat);
            cmbHangSanXuat.SelectedIndex = 0;
            cmbMucDich.Items.Clear();
            cmbMucDich.Items.Add(Element.LoaiSanPham.MucDich);
            cmbMucDich.SelectedIndex = 0;

            System.IO.MemoryStream ms = new System.IO.MemoryStream(Element.HinhAnh);
            ms.Seek(0, System.IO.SeekOrigin.Begin);
            BitmapImage newBitmapImage = new BitmapImage();
            newBitmapImage.BeginInit();
            newBitmapImage.StreamSource = ms;
            newBitmapImage.EndInit();
            img_SanPham.Source = newBitmapImage;
            bitmapImageSanPham = newBitmapImage;
        }

        public void LoadComboBox()
        {
            var db = new MyDatabaseEntities();

            string Temp_HangSanXuat = cmbHangSanXuat.SelectedItem.ToString();
            string Temp_MucDich = cmbMucDich.SelectedItem.ToString();

            cmbHangSanXuat.Items.Clear();

            cmbHangSanXuat.Items.Add(Temp_HangSanXuat);
            cmbHangSanXuat.SelectedIndex = 0;

            foreach (var value in db.LoaiSanPhams.ToList())
            {
                if (cmbHangSanXuat.Items.IndexOf(value.HangSanXuat) == -1)
                {
                    cmbHangSanXuat.Items.Add(value.HangSanXuat);
                }
            }

            for(int i = 0; i < cmbMucDich.Items.Count; ++i)
            {
                if(cmbMucDich.Items[i].ToString() == Temp_MucDich)
                {
                    cmbMucDich.SelectedIndex = i;
                    break;
                }
            }

        }

        private void imgBack_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            NavigationService.GoBack();
        }

        

        private void cmbHangSanXuat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Khong Xu Ly
            if (cmbHangSanXuat.SelectedIndex == -1)
            {
                return;
            }
            var db = new MyDatabaseEntities();
            cmbMucDich.Items.Clear();
            foreach (var value in db.LoaiSanPhams.ToList())
            {
                if (cmbHangSanXuat.SelectedValue.ToString() == value.HangSanXuat)
                {
                    cmbMucDich.Items.Add(value.MucDich);
                }

            }
        }

        
        private void btnXacNhanSuaSanPham_Click(object sender, RoutedEventArgs e)
        {
            //Kiem Tra Form Rỗng
            if ("" == txtTenSanPham.Text || "" == txtGiaNhap.Text || "" == txtGiaBan.Text || "" == txtSoLuong.Text || cmbHangSanXuat.SelectedIndex == -1 || cmbMucDich.SelectedIndex == -1)
            {
                MessageBox.Show("Vui Lòng Không Để Trống");
                return;
            }

            if (Int32.Parse(txtSoLuong.Text) < 0 || Int32.Parse(txtGiaBan.Text) < 0 || Int32.Parse(txtGiaNhap.Text) < 0)
            {
                MessageBox.Show("Vui Lòng Kiểm Tra Lại Giá Trị Nhập");
                return;
            }

            var db = new MyDatabaseEntities();
            var element = db.SanPhams.Find(ID_OFItemSelected);

            //Convert Ảnh
            byte[] imageData = new byte[bitmapImageSanPham.StreamSource.Length];
            bitmapImageSanPham.StreamSource.Seek(0, System.IO.SeekOrigin.Begin);
            bitmapImageSanPham.StreamSource.Read(imageData, 0, imageData.Length);

            //Tim ID Loai San Pham
            int ID_LoaiSanPham = -1;
            foreach (var value in db.LoaiSanPhams.ToList())
            {
                if (cmbHangSanXuat.SelectedValue.ToString() == value.HangSanXuat && cmbMucDich.SelectedValue.ToString() == value.MucDich)
                {
                    ID_LoaiSanPham = value.ID;
                    break;
                }

            }
            // Lay Gia Tri Hien Thi
            element.HinhAnh = imageData;
            element.Ten = txtTenSanPham.Text;
            element.LoaiSanPham_ID = ID_LoaiSanPham;
            element.GiaNhap = Int32.Parse(txtGiaNhap.Text);
            element.GiaBan = Int32.Parse(txtGiaBan.Text);
            element.SoLuong = Int32.Parse(txtSoLuong.Text);
            
            db.SaveChanges();
            MessageBox.Show("Sữa Thành Công");
            DanhSachSanPham.LoadSanPham(); // Load Lai Sau Khi Sua
        }

        private void img_SanPham_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog choofdlog = new OpenFileDialog();
            choofdlog.Filter = "Image|*.jpg;*.jpeg;*.png;";
            choofdlog.FilterIndex = 1;
            choofdlog.Multiselect = false;

            if (choofdlog.ShowDialog() == true)
            {
                string sFileName = choofdlog.FileName;
                //string[] arrAllFiles = choofdlog.FileNames; //used when Multiselect = true

                //Load File Ảnh
                bitmapImageSanPham = new BitmapImage();
                bitmapImageSanPham.BeginInit();
                bitmapImageSanPham.StreamSource = System.IO.File.OpenRead(sFileName);
                bitmapImageSanPham.EndInit();
                img_SanPham.Source = bitmapImageSanPham; // Đẩy Ảnh Lên UI
            }
        }


        private void btnBatGiaoDich_Click(object sender, RoutedEventArgs e)
        {
            btnBatGiaoDich.Background = Brushes.Red;
            btnBatSuaThongTin.Background = Brushes.Black;
            btnXacNhanSuaSanPham.Visibility = Visibility.Hidden;
            btnMua.Visibility = Visibility.Visible;

            LoadThongTinSanPham();
            txtTenSanPham.IsReadOnly = true;
            txtGiaBan.IsReadOnly = true;
            txtGiaNhap.IsReadOnly = true;
            txtSoLuong.IsReadOnly = true;
            cmbHangSanXuat.IsEnabled = false;
            cmbMucDich.IsEnabled = false;
        }

        private void btnBatSuaThongTin_Click(object sender, RoutedEventArgs e)
        {
            btnBatGiaoDich.Background = Brushes.Black;
            btnBatSuaThongTin.Background = Brushes.Red;
            btnXacNhanSuaSanPham.Visibility = Visibility.Visible;
            btnMua.Visibility = Visibility.Hidden;
            txtTenSanPham.IsReadOnly = false;
            txtGiaBan.IsReadOnly = false;
            txtGiaNhap.IsReadOnly = false;
            txtSoLuong.IsReadOnly = false;
            cmbHangSanXuat.IsEnabled = true;
            cmbMucDich.IsEnabled = true;
            LoadComboBox();
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Bạn Chắc Chắn Muốn Xóa Chứ ?", "Thông Báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var db = new MyDatabaseEntities();
                var Element = db.SanPhams.Find(ID_OFItemSelected);
                Element.HienThi = 0;
                db.SaveChanges();
                DanhSachSanPham.LoadSanPham(); // Load Lai Hien Thi Danh Sach San Pham
                NavigationService.GoBack();
            }
        }


        private void btnMua_Click(object sender, RoutedEventArgs e)
        {
            if (txtSoLuongMua.Text == "")
            {
                MessageBox.Show("Vui Lòng Không Để Trống");
                return;
            }

            if (int.Parse(txtSoLuongMua.Text) > int.Parse(txtSoLuong.Text))
            {
                MessageBox.Show("Số Lượng Mua Không Hợp Lệ");
                return;
            }

            var db = new MyDatabaseEntities();
            var element = db.SanPhams.Find(ID_OFItemSelected);

            //Convert Ảnh
            System.IO.MemoryStream ms = new System.IO.MemoryStream(element.HinhAnh);
            ms.Seek(0, System.IO.SeekOrigin.Begin);
            BitmapImage newBitmapImage = new BitmapImage();
            newBitmapImage.BeginInit();
            newBitmapImage.StreamSource = ms;
            newBitmapImage.EndInit();

            ListSPGHHT.dataGioHang.Add(new SanPhamGioHangHienThi() { ID = element.ID, PathImage = newBitmapImage, Name = element.Ten, Price = string.Format("{0}", (int)element.GiaBan), PriceNhap = string.Format("{0}", (int)element.GiaNhap), Count = txtSoLuongMua.Text });
            

            MessageBox.Show("Đã Thêm Vào Giỏ Hàng");
            NavigationService.GoBack();
        }
    }
}
