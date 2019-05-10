using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Interaction logic for ThemSanPham.xaml
    /// </summary>
    public partial class ThemSanPham : Page
    {

        public ThemSanPham()
        {
            InitializeComponent();

            //Load ComboBox
            LoadComboBox();


        }

        public void LoadComboBox()
        {
            var db = new MyDatabaseEntities();
            cmbHangSanXuat.Items.Clear();
            foreach (var value in db.LoaiSanPhams.ToList())
            {
                if (cmbHangSanXuat.Items.IndexOf(value.HangSanXuat) == -1)
                {
                    cmbHangSanXuat.Items.Add(value.HangSanXuat);
                }
            }
        }

        private void imgBack_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DanhSachSanPham.cmbSapXep_Temp.SelectedIndex = 0;
            DanhSachSanPham.LoadSanPham();
            NavigationService.GoBack();
        }

        private void btnThemLoaiSanPham_Click(object sender, RoutedEventArgs e)
        {
            btnThemLoaiSanPham.Background = Brushes.Red;
            btnThemSanPham.Background = Brushes.Black;
            btnThemBangExcel.Background = Brushes.Black;
            gbLoaiSanPham.Visibility = Visibility.Visible;
            gbSanPham.Visibility = Visibility.Hidden;
            gbImportExecel.Visibility = Visibility.Hidden;
        }

        private void btnThemSanPham_Click(object sender, RoutedEventArgs e)
        {
            btnThemLoaiSanPham.Background = Brushes.Black;
            btnThemSanPham.Background = Brushes.Red;
            btnThemBangExcel.Background = Brushes.Black;
            gbLoaiSanPham.Visibility = Visibility.Hidden;
            gbSanPham.Visibility = Visibility.Visible;
            gbImportExecel.Visibility = Visibility.Hidden;

        }

        private void btnXacNhanThemLoaiSanPham_Click(object sender, RoutedEventArgs e)
        {
            //Kiem Tra Form Rỗng
            if ("" == txtHangSanXuat.Text || "" == txtMucDich.Text)
            {
                MessageBox.Show("Vui Lòng Không Để Trống");
                return;
            }

            var db = new MyDatabaseEntities();
            //Kiem Tra Tồn Tại
            foreach (LoaiSanPham loaiSanPham in db.LoaiSanPhams.ToList())
            {
                if(loaiSanPham.HangSanXuat == txtHangSanXuat.Text && loaiSanPham.MucDich == txtMucDich.Text)
                {
                    MessageBox.Show("Đã Tồn Tại");
                    return;
                }
            }
            db.LoaiSanPhams.Add(new LoaiSanPham() { HangSanXuat = txtHangSanXuat.Text, MucDich = txtMucDich.Text });
            db.SaveChanges();
            MessageBox.Show("Thêm Thành Công");
            txtHangSanXuat.Text = "";
            txtMucDich.Text = "";
        }

       
        private void cmbHangSanXuat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Khong Xu Ly
            if(cmbHangSanXuat.SelectedIndex == -1)
            {
                return;
            }
            var db = new MyDatabaseEntities();
            cmbMucDich.Items.Clear();//Xoa hết Phan từ
            foreach (var value in db.LoaiSanPhams.ToList())
            {
                if (cmbHangSanXuat.SelectedValue.ToString() == value.HangSanXuat)
                {
                    cmbMucDich.Items.Add(value.MucDich);
                }

            }
        }

        private void btnXacNhanThemSanPham_Click(object sender, RoutedEventArgs e)
        {
            //Kiem Tra Form Rỗng
            if ("" == txtTenSanPham.Text || "" == txtGiaNhap.Text || "" == txtGiaBan.Text || "" == txtSoLuong.Text || cmbHangSanXuat.SelectedIndex == -1 || cmbMucDich.SelectedIndex == -1)
            {
                MessageBox.Show("Vui Lòng Không Để Trống");
                return;
            }

            if(Int32.Parse(txtSoLuong.Text) < 0 || Int32.Parse(txtGiaBan.Text) < 0 || Int32.Parse(txtGiaNhap.Text) < 0)
            {
                MessageBox.Show("Vui Lòng Kiểm Tra Lại Giá Trị Nhập");
                return;
            }

            if(bitmapImageSanPham == null)
            {
                MessageBox.Show("Vui Lòng Chọn Ảnh Hiện Thị");
                return;
            }

            var db = new MyDatabaseEntities();

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
            int CheckHienThi = cbHienThi.IsChecked == true ? 1 : 0;
            db.SanPhams.Add(new SanPham() { HinhAnh = imageData, Ten = txtTenSanPham.Text, LoaiSanPham_ID = ID_LoaiSanPham, GiaNhap = Int32.Parse(txtGiaNhap.Text) , GiaBan = Int32.Parse(txtGiaBan.Text) , HienThi = CheckHienThi, SoLuong = Int32.Parse(txtSoLuong.Text)});
            db.SaveChanges();
            MessageBox.Show("Thêm Thành Công");

            txtTenSanPham.Text = "";
            txtGiaNhap.Text = "";
            txtGiaBan.Text = "";
            txtSoLuong.Text = "";
            cmbHangSanXuat.SelectedIndex = -1;
            cmbMucDich.SelectedIndex = -1;
            img_SanPham.Source =  new BitmapImage(new Uri("images/device-camera-icon.png", UriKind.Relative));
            bitmapImageSanPham = null;
        }

        BitmapImage bitmapImageSanPham;
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

        private void btnThemBangExcel_Click(object sender, RoutedEventArgs e)
        {
            btnThemLoaiSanPham.Background = Brushes.Black;
            btnThemSanPham.Background = Brushes.Black;
            btnThemBangExcel.Background = Brushes.Red;
            gbLoaiSanPham.Visibility = Visibility.Hidden;
            gbSanPham.Visibility = Visibility.Hidden;
            gbImportExecel.Visibility = Visibility.Visible;

            
        }

        private void btnBroswerLoaiSanPham_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog choofdlog = new OpenFileDialog();
            choofdlog.Filter = "File Excel|*.xlsx;";
            choofdlog.FilterIndex = 1;
            choofdlog.Multiselect = false;
            if (choofdlog.ShowDialog() == true)
            {
                string sFileName = choofdlog.FileName;
                txtPathLoaiSanPham.Text = sFileName;
            }
        }

        private void btnExcelXNLoaiSanPham_Click(object sender, RoutedEventArgs e)
        {
            if(txtPathLoaiSanPham.Text == "")
            {
                MessageBox.Show("Vui Lòng Chọn Đường Dẫn", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var db = new MyDatabaseEntities();
            try
            {
                Excel excel = new Excel(txtPathLoaiSanPham.Text, 1);
                
                for(int i = 1;; ++i)
                {
                    string value_HangSanXuat = excel.ReadCell(i, 0);
                    string value_MucDich = excel.ReadCell(i, 1);
                    if (value_HangSanXuat == "")
                    {
                        break;
                    }

                    //Kiểm Tra Tồn Tại
                    bool CheckTonTai = false;
                    foreach (LoaiSanPham loaiSanPham in db.LoaiSanPhams.ToList())
                    {
                        if (loaiSanPham.HangSanXuat == value_HangSanXuat && loaiSanPham.MucDich == value_MucDich)
                        {
                            CheckTonTai = true;
                            break;
                        }
                    }
                    if(CheckTonTai == false)
                    {
                        db.LoaiSanPhams.Add(new LoaiSanPham() { HangSanXuat = value_HangSanXuat, MucDich = value_MucDich });
                        db.SaveChanges();
                    }
                }
            }
            catch
            {
                MessageBox.Show("File Không Hợp Lệ", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            MessageBox.Show("Thêm Thành Công");
        }

        private void btnBroswerSanPham_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog choofdlog = new OpenFileDialog();
            choofdlog.Filter = "File Excel|*.xlsx;";
            choofdlog.FilterIndex = 1;
            choofdlog.Multiselect = false;
            if (choofdlog.ShowDialog() == true)
            {
                string sFileName = choofdlog.FileName;
                txtPathSanPham.Text = sFileName;
            }
        }

        private void btnExcelXNSanPham_Click(object sender, RoutedEventArgs e)
        {
            if (txtPathSanPham.Text == "")
            {
                MessageBox.Show("Vui Lòng Chọn Đường Dẫn", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var db = new MyDatabaseEntities();
            try
            {
                Excel excel = new Excel(txtPathSanPham.Text, 1);

                for (int i = 1; ; ++i)
                {
                    string value_Ten = excel.ReadCell(i, 0);
                    if (value_Ten == "")
                    {
                        break;
                    }
                    string value_LinkAnh = excel.ReadCell(i, 1);
                    string value_HangSanXuat = excel.ReadCell(i, 2);
                    string value_MucDich = excel.ReadCell(i, 3);
                    string value_GiaNhap =  excel.ReadCell(i, 4);
                    string value_GiaBan = excel.ReadCell(i, 5);
                    string value_SoLuong = excel.ReadCell(i, 6);

                    //Convert Ảnh
                    string PathFolder = txtPathSanPham.Text.Substring(0, txtPathSanPham.Text.LastIndexOf('\\'));
                    bitmapImageSanPham = new BitmapImage();
                    bitmapImageSanPham.BeginInit();
                    bitmapImageSanPham.StreamSource = System.IO.File.OpenRead(PathFolder + "\\" + value_LinkAnh);
                    bitmapImageSanPham.EndInit();
                    byte[] imageData = new byte[bitmapImageSanPham.StreamSource.Length];
                    bitmapImageSanPham.StreamSource.Seek(0, System.IO.SeekOrigin.Begin);
                    bitmapImageSanPham.StreamSource.Read(imageData, 0, imageData.Length);

                    //Kiểm Tra Tồn Tại
                    bool CheckTonTai = false;
                    int IDLoaiSanPhamTonTai = -1;
                    foreach (LoaiSanPham loaiSanPham in db.LoaiSanPhams.ToList())
                    {
                        if (loaiSanPham.HangSanXuat == value_HangSanXuat && loaiSanPham.MucDich == value_MucDich)
                        {
                            CheckTonTai = true;
                            IDLoaiSanPhamTonTai = loaiSanPham.ID;
                            break;
                        }
                    }
                    if (CheckTonTai == false)
                    {
                        db.LoaiSanPhams.Add(new LoaiSanPham() { HangSanXuat = value_HangSanXuat, MucDich = value_MucDich });
                        db.SaveChanges();
                        db.SanPhams.Add(new SanPham() { Ten = value_Ten, HinhAnh = imageData, LoaiSanPham_ID = db.LoaiSanPhams.ToList()[db.LoaiSanPhams.ToList().Count - 1].ID ,GiaNhap = Int32.Parse(value_GiaNhap), GiaBan = Int32.Parse(value_GiaBan), SoLuong = Int32.Parse(value_SoLuong) , HienThi = 1});
                        db.SaveChanges();
                    }
                    else
                    {
                        db.SanPhams.Add(new SanPham() { Ten = value_Ten, HinhAnh = imageData, LoaiSanPham_ID = IDLoaiSanPhamTonTai, GiaNhap = Int32.Parse(value_GiaNhap), GiaBan = Int32.Parse(value_GiaBan), SoLuong = Int32.Parse(value_SoLuong), HienThi = 1 });
                        db.SaveChanges();
                    }
                }
            }
            catch
            {
                MessageBox.Show("File Không Hợp Lệ", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBox.Show("Thêm Thành Công");
        }
    }
}
