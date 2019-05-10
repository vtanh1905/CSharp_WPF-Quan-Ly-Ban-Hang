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
    /// Interaction logic for TrangThongKe.xaml
    /// </summary>
    public partial class TrangThongKe : Page
    {
        public TrangThongKe()
        {
            InitializeComponent();

            for (int i = 2015; i < 2030; ++i)
            {
                cmbYear.Items.Add(i);
            }
            cmbYear.SelectedIndex = 4;
            for (int i = 1; i <= 12; ++i)
            {
                cmbMonth.Items.Add(i);
            }
            cmbMonth.SelectedIndex = 0;

            for (int i = 1; i <= 4; ++i)
            {
                cmbQuy.Items.Add(i);
            }
            cmbQuy.SelectedIndex = 0;


            //Visible Thong Tin Nhap
            lblNgay.Visibility = Visibility.Hidden;
            cmbNgay.Visibility = Visibility.Hidden;

            lblQuy.Visibility = Visibility.Hidden;
            cmbQuy.Visibility = Visibility.Hidden;

            lblThang.Visibility = Visibility.Hidden;
            cmbMonth.Visibility = Visibility.Hidden;

            lblNam.Visibility = Visibility.Hidden;
            cmbYear.Visibility = Visibility.Hidden;

            cldStart.Visibility = Visibility.Hidden;
            cldEnd.Visibility = Visibility.Hidden;
        }

        static public int KieuThongKe = -1; // 0 Theo Ngay, 1 Theo Thang, 2 Theo Quy, 3 Theo Nam, 4 Theo Thoi Gian
        static public int Year = -1;
        static public int Month = -1;
        static public int Day = -1;
        static public int Quy = -1;
        static public DateTime DateStart = DateTime.Now;
        static public DateTime DateEnd = DateTime.Now;

        private void btnNgay_Click(object sender, RoutedEventArgs e)
        {
            btnNgay.Background = Brushes.Red;
            btnThang.Background = Brushes.Black;
            btnQuy.Background = Brushes.Black;
            btnNam.Background = Brushes.Black;
            btnThoiGian.Background = Brushes.Black;
            KieuThongKe = 0;
            LoadDuLieuNhap();
        }

        private void btnThang_Click(object sender, RoutedEventArgs e)
        {
            btnNgay.Background = Brushes.Black;
            btnThang.Background = Brushes.Red;
            btnQuy.Background = Brushes.Black;
            btnNam.Background = Brushes.Black;
            btnThoiGian.Background = Brushes.Black;
            KieuThongKe = 1;
            LoadDuLieuNhap();
        }

        private void btnQuy_Click(object sender, RoutedEventArgs e)
        {
            btnNgay.Background = Brushes.Black;
            btnQuy.Background = Brushes.Red;
            btnThang.Background = Brushes.Black;
            btnNam.Background = Brushes.Black;
            btnThoiGian.Background = Brushes.Black;
            KieuThongKe = 2;
            LoadDuLieuNhap();
        }

        private void btnNam_Click(object sender, RoutedEventArgs e)
        {
            btnNgay.Background = Brushes.Black;
            btnThang.Background = Brushes.Black;
            btnQuy.Background = Brushes.Black;
            btnNam.Background = Brushes.Red;
            btnThoiGian.Background = Brushes.Black;
            KieuThongKe = 3;
            LoadDuLieuNhap();
        }

        private void btnThoiGian_Click(object sender, RoutedEventArgs e)
        {
            btnNgay.Background = Brushes.Black;
            btnThang.Background = Brushes.Black;
            btnQuy.Background = Brushes.Black;
            btnNam.Background = Brushes.Black;
            btnThoiGian.Background = Brushes.Red;
            KieuThongKe = 4;
            LoadDuLieuNhap();
        }

        private void cmbNgay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cmbMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbNgay.Items.Clear();
            for (int i = 1; i <= DateTime.DaysInMonth(int.Parse(cmbYear.SelectedItem.ToString()), int.Parse(cmbMonth.SelectedItem.ToString())); ++i)
            {
                cmbNgay.Items.Add(i);
            }
            cmbNgay.SelectedItem = 1;
        }

        private void btnChonCot_Click(object sender, RoutedEventArgs e)
        {
            btnChonCot.Background = Brushes.Red;
            btnChonTron.Background = Brushes.Black;
            btnChonDuong.Background = Brushes.Black;
            LoadDuLieuNhap();
        }

        private void btnChonTron_Click(object sender, RoutedEventArgs e)
        {
            btnChonCot.Background = Brushes.Black;
            btnChonTron.Background = Brushes.Red;
            btnChonDuong.Background = Brushes.Black;
            LoadDuLieuNhap();
        }

        private void btnChonDuong_Click(object sender, RoutedEventArgs e)
        {
            btnChonCot.Background = Brushes.Black;
            btnChonTron.Background = Brushes.Black;
            btnChonDuong.Background = Brushes.Red;
            LoadDuLieuNhap();
        }

        private void btnXem_Click(object sender, RoutedEventArgs e)
        {
            if (KieuThongKe == -1)
            {
                MessageBox.Show("Vui Lòng Chọn Kiểu Thống Kê");
                return;
            }
            Year = int.Parse(cmbYear.SelectedItem.ToString());
            Month = int.Parse(cmbMonth.SelectedItem.ToString());
            Day = int.Parse(cmbNgay.SelectedItem.ToString());
            Quy = int.Parse(cmbQuy.SelectedItem.ToString());

            if(KieuThongKe == 4)
            {
                try
                {
                    DateStart = (DateTime)cldStart.SelectedDate;
                    DateEnd = (DateTime)cldEnd.SelectedDate;
                }
                catch
                {
                    MessageBox.Show("Vui Lòng Chọn Ngày");
                    return;
                }
                

                if(DateStart >= DateEnd)
                {
                    MessageBox.Show("Vui Lòng Chọn Ngày Chính Xác");
                    return;
                }
            }

            if (btnChonCot.Background == Brushes.Red)
            {
                ThongKeDoanhThu thongKeDoanhThu = new ThongKeDoanhThu();
                thongKeDoanhThu.ShowDialog();
            }
            else if (btnChonTron.Background == Brushes.Red)
            {
                ThongKeHinhTron thongKeHinhTron = new ThongKeHinhTron();
                thongKeHinhTron.ShowDialog();
            }
            else if (btnChonDuong.Background == Brushes.Red)
            {
                ThongKeSanPham thongKeSanPham = new ThongKeSanPham();
                thongKeSanPham.ShowDialog();
            }
            else
            {
                MessageBox.Show("Vui Lòng Chọn Loại Thống Kê");
            }
        }

        void LoadDuLieuNhap()
        {
            if (btnChonCot.Background == Brushes.Red)
            {
               if(KieuThongKe == 0)
                {
                    lblNgay.Visibility = Visibility.Hidden;
                    lblQuy.Visibility = Visibility.Hidden;
                    cmbNgay.Visibility = Visibility.Hidden;
                    cmbQuy.Visibility = Visibility.Hidden;

                    lblThang.Visibility = Visibility.Visible;
                    lblNam.Visibility = Visibility.Visible;
                    cmbMonth.Visibility = Visibility.Visible;
                    cmbYear.Visibility = Visibility.Visible;

                    cldStart.Visibility = Visibility.Hidden;
                    cldEnd.Visibility = Visibility.Hidden;
                }
                else if(KieuThongKe == 1)
                {
                    lblNgay.Visibility = Visibility.Hidden;
                    cmbNgay.Visibility = Visibility.Hidden;

                    lblQuy.Visibility = Visibility.Hidden;
                    cmbQuy.Visibility = Visibility.Hidden;

                    lblThang.Visibility = Visibility.Hidden;
                    cmbMonth.Visibility = Visibility.Hidden;

                    lblNam.Visibility = Visibility.Visible;
                    cmbYear.Visibility = Visibility.Visible;

                    cldStart.Visibility = Visibility.Hidden;
                    cldEnd.Visibility = Visibility.Hidden;
                }
                else if (KieuThongKe == 2)
                {
                    lblNgay.Visibility = Visibility.Hidden;
                    cmbNgay.Visibility = Visibility.Hidden;

                    lblQuy.Visibility = Visibility.Hidden;
                    cmbQuy.Visibility = Visibility.Hidden;

                    lblThang.Visibility = Visibility.Hidden;
                    cmbMonth.Visibility = Visibility.Hidden;

                    lblNam.Visibility = Visibility.Visible;
                    cmbYear.Visibility = Visibility.Visible;

                    cldStart.Visibility = Visibility.Hidden;
                    cldEnd.Visibility = Visibility.Hidden;
                }
                else if (KieuThongKe == 3)
                {
                    lblNgay.Visibility = Visibility.Hidden;
                    cmbNgay.Visibility = Visibility.Hidden;

                    lblQuy.Visibility = Visibility.Hidden;
                    cmbQuy.Visibility = Visibility.Hidden;

                    lblThang.Visibility = Visibility.Hidden;
                    cmbMonth.Visibility = Visibility.Hidden;

                    lblNam.Visibility = Visibility.Hidden;
                    cmbYear.Visibility = Visibility.Hidden;

                    cldStart.Visibility = Visibility.Hidden;
                    cldEnd.Visibility = Visibility.Hidden;
                }
                else if (KieuThongKe == 4)
                {
                    lblNgay.Visibility = Visibility.Hidden;
                    cmbNgay.Visibility = Visibility.Hidden;

                    lblQuy.Visibility = Visibility.Hidden;
                    cmbQuy.Visibility = Visibility.Hidden;

                    lblThang.Visibility = Visibility.Hidden;
                    cmbMonth.Visibility = Visibility.Hidden;

                    lblNam.Visibility = Visibility.Hidden;
                    cmbYear.Visibility = Visibility.Hidden;

                    cldStart.Visibility = Visibility.Visible;
                    cldEnd.Visibility = Visibility.Visible;
                }
            }
            else if (btnChonTron.Background == Brushes.Red)
            {
                if (KieuThongKe == 0)
                {
                    lblNgay.Visibility = Visibility.Visible;
                    lblQuy.Visibility = Visibility.Hidden;
                    cmbNgay.Visibility = Visibility.Visible;
                    cmbQuy.Visibility = Visibility.Hidden;

                    lblThang.Visibility = Visibility.Visible;
                    lblNam.Visibility = Visibility.Visible;
                    cmbMonth.Visibility = Visibility.Visible;
                    cmbYear.Visibility = Visibility.Visible;

                    cldStart.Visibility = Visibility.Hidden;
                    cldEnd.Visibility = Visibility.Hidden;
                }
                else if (KieuThongKe == 1)
                {
                    lblNgay.Visibility = Visibility.Hidden;
                    cmbNgay.Visibility = Visibility.Hidden;

                    lblQuy.Visibility = Visibility.Hidden;
                    cmbQuy.Visibility = Visibility.Hidden;

                    lblThang.Visibility = Visibility.Visible;
                    cmbMonth.Visibility = Visibility.Visible;

                    lblNam.Visibility = Visibility.Visible;
                    cmbYear.Visibility = Visibility.Visible;

                    cldStart.Visibility = Visibility.Hidden;
                    cldEnd.Visibility = Visibility.Hidden;
                }
                else if (KieuThongKe == 2)
                {
                    lblNgay.Visibility = Visibility.Hidden;
                    cmbNgay.Visibility = Visibility.Hidden;

                    lblQuy.Visibility = Visibility.Visible;
                    cmbQuy.Visibility = Visibility.Visible;

                    lblThang.Visibility = Visibility.Hidden;
                    cmbMonth.Visibility = Visibility.Hidden;

                    lblNam.Visibility = Visibility.Visible;
                    cmbYear.Visibility = Visibility.Visible;

                    cldStart.Visibility = Visibility.Hidden;
                    cldEnd.Visibility = Visibility.Hidden;
                }
                else if (KieuThongKe == 3)
                {
                    lblNgay.Visibility = Visibility.Hidden;
                    cmbNgay.Visibility = Visibility.Hidden;

                    lblQuy.Visibility = Visibility.Hidden;
                    cmbQuy.Visibility = Visibility.Hidden;

                    lblThang.Visibility = Visibility.Hidden;
                    cmbMonth.Visibility = Visibility.Hidden;

                    lblNam.Visibility = Visibility.Visible;
                    cmbYear.Visibility = Visibility.Visible;

                    cldStart.Visibility = Visibility.Hidden;
                    cldEnd.Visibility = Visibility.Hidden;
                }
                else if (KieuThongKe == 4)
                {
                    lblNgay.Visibility = Visibility.Hidden;
                    cmbNgay.Visibility = Visibility.Hidden;

                    lblQuy.Visibility = Visibility.Hidden;
                    cmbQuy.Visibility = Visibility.Hidden;

                    lblThang.Visibility = Visibility.Hidden;
                    cmbMonth.Visibility = Visibility.Hidden;

                    lblNam.Visibility = Visibility.Hidden;
                    cmbYear.Visibility = Visibility.Hidden;

                    cldStart.Visibility = Visibility.Visible;
                    cldEnd.Visibility = Visibility.Visible;
                }
            }
            else if (btnChonDuong.Background == Brushes.Red)
            {
                if (KieuThongKe == 0)
                {
                    lblNgay.Visibility = Visibility.Hidden;
                    lblQuy.Visibility = Visibility.Hidden;
                    cmbNgay.Visibility = Visibility.Hidden;
                    cmbQuy.Visibility = Visibility.Hidden;

                    lblThang.Visibility = Visibility.Visible;
                    lblNam.Visibility = Visibility.Visible;
                    cmbMonth.Visibility = Visibility.Visible;
                    cmbYear.Visibility = Visibility.Visible;

                    cldStart.Visibility = Visibility.Hidden;
                    cldEnd.Visibility = Visibility.Hidden;
                }
                else if (KieuThongKe == 1)
                {
                    lblNgay.Visibility = Visibility.Hidden;
                    cmbNgay.Visibility = Visibility.Hidden;

                    lblQuy.Visibility = Visibility.Hidden;
                    cmbQuy.Visibility = Visibility.Hidden;

                    lblThang.Visibility = Visibility.Hidden;
                    cmbMonth.Visibility = Visibility.Hidden;

                    lblNam.Visibility = Visibility.Visible;
                    cmbYear.Visibility = Visibility.Visible;

                    cldStart.Visibility = Visibility.Hidden;
                    cldEnd.Visibility = Visibility.Hidden;
                }
                else if (KieuThongKe == 2)
                {
                    lblNgay.Visibility = Visibility.Hidden;
                    cmbNgay.Visibility = Visibility.Hidden;

                    lblQuy.Visibility = Visibility.Hidden;
                    cmbQuy.Visibility = Visibility.Hidden;

                    lblThang.Visibility = Visibility.Hidden;
                    cmbMonth.Visibility = Visibility.Hidden;

                    lblNam.Visibility = Visibility.Visible;
                    cmbYear.Visibility = Visibility.Visible;

                    cldStart.Visibility = Visibility.Hidden;
                    cldEnd.Visibility = Visibility.Hidden;
                }
                else if (KieuThongKe == 3)
                {
                    lblNgay.Visibility = Visibility.Hidden;
                    cmbNgay.Visibility = Visibility.Hidden;

                    lblQuy.Visibility = Visibility.Hidden;
                    cmbQuy.Visibility = Visibility.Hidden;

                    lblThang.Visibility = Visibility.Hidden;
                    cmbMonth.Visibility = Visibility.Hidden;

                    lblNam.Visibility = Visibility.Hidden;
                    cmbYear.Visibility = Visibility.Hidden;

                    cldStart.Visibility = Visibility.Hidden;
                    cldEnd.Visibility = Visibility.Hidden;
                }
                else if (KieuThongKe == 4)
                {
                    lblNgay.Visibility = Visibility.Hidden;
                    cmbNgay.Visibility = Visibility.Hidden;

                    lblQuy.Visibility = Visibility.Hidden;
                    cmbQuy.Visibility = Visibility.Hidden;

                    lblThang.Visibility = Visibility.Hidden;
                    cmbMonth.Visibility = Visibility.Hidden;

                    lblNam.Visibility = Visibility.Hidden;
                    cmbYear.Visibility = Visibility.Hidden;

                    cldStart.Visibility = Visibility.Visible;
                    cldEnd.Visibility = Visibility.Visible;
                }
            }
        }
    }
}
