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
    /// Interaction logic for TrangMaKhuyenMai.xaml
    /// </summary>
    public partial class TrangMaKhuyenMai : Page
    {
        MyDatabaseEntities db = new MyDatabaseEntities();
        public TrangMaKhuyenMai()
        {
            InitializeComponent();

            //Load Combobox
            cmbLoai.Items.Add("Tiền Cố Định"); // 0
            cmbLoai.Items.Add("Phần Trăm"); // 1
            cmbLoai.SelectedIndex = 0;

            //Load MaKM
            ltMaKhuyenMai.ItemsSource = db.MaKhuyenMais.ToList();
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            if(txtGiaTri.Text == "" || txtMa.Text == "")
            {
                MessageBox.Show("Vui Lòng Nhập Đầy Thông Tin");
                return;
            }

            try
            {
                Int32.Parse(txtGiaTri.Text);
            }
            catch
            {
                MessageBox.Show("Không Nhập Đúng Định Dạng");
                return;
            }

            
            db.MaKhuyenMais.Add(new MaKhuyenMai() { Ten = txtMa.Text, GiaTri = txtGiaTri.Text, Loai = cmbLoai.SelectedItem.ToString()});
            db.SaveChanges();

            ltMaKhuyenMai.ItemsSource = db.MaKhuyenMais.ToList();

            txtMa.Text = "";
            txtGiaTri.Text = "";
            cmbLoai.SelectedIndex = 0;
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

        private void ltMaKhuyenMai_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                if (DuocXoa == true)
                {
                    if (MessageBox.Show("Bạn Chắc Chắn Muốn Xóa Chứ ?", "Thông Báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        MaKhuyenMai ItemSelected = (MaKhuyenMai)ltMaKhuyenMai.Items[ltMaKhuyenMai.SelectedIndex];
                        
                        db.MaKhuyenMais.Remove(ItemSelected);
                        db.SaveChanges();

                        //Load MaKM
                        ltMaKhuyenMai.ItemsSource = db.MaKhuyenMais.ToList();
                    }
                }
            }
        }
    }
}
