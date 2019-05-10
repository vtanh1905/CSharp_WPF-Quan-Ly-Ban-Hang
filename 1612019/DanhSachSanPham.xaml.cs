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
    /// Interaction logic for DanhSachSanPham.xaml
    /// </summary>
    public partial class DanhSachSanPham : Page
    {
        static int MaxTrang = 0;
        static int TrangHienTai = 0;
        static int SoPhanTuTrenMotTrang = 12;
        static ListView ltSanPham_Temp;
        static public ComboBox cmbSapXep_Temp;

        public class SanPhamHienThi
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public ImageSource PathImage { get; set; }
            public string Price { get; set; }
        }

       

        public DanhSachSanPham()
        {
            InitializeComponent();

            ltSanPham_Temp = ltSanPham;

            //Load cmbSapXep
            cmbSapXep_Temp = cmbSapXep;
            cmbSapXep.Items.Add("Mặc Định"); // 0
            cmbSapXep.Items.Add("Từ A -> Z"); // 1
            cmbSapXep.Items.Add("Từ Z -> A"); // 2
            cmbSapXep.Items.Add("Giá Tăng Dần"); // 3
            cmbSapXep.Items.Add("Giá Giảm Dần"); // 4
            cmbSapXep.SelectedIndex = 0;

            // Show San Pham
            LoadSanPham();
        }
        
        static public void LoadSanPham()
        {
            List<SanPhamHienThi> data = new List<SanPhamHienThi>();
            var db = new MyDatabaseEntities();
            var listDatabaseSanPham = db.SanPhams.ToList();

            int CountPhanTuHienThi = 0;
            for (int i = 0; i < listDatabaseSanPham.Count; ++i)
            {
                var element = listDatabaseSanPham[i];
                if(element.HienThi == 1)
                {
                    if(CountPhanTuHienThi >= TrangHienTai * SoPhanTuTrenMotTrang && CountPhanTuHienThi < (TrangHienTai * SoPhanTuTrenMotTrang) + SoPhanTuTrenMotTrang)
                    {
                        //Convert Ảnh
                        System.IO.MemoryStream ms = new System.IO.MemoryStream(element.HinhAnh);
                        ms.Seek(0, System.IO.SeekOrigin.Begin);
                        BitmapImage newBitmapImage = new BitmapImage();
                        newBitmapImage.BeginInit();
                        newBitmapImage.StreamSource = ms;
                        newBitmapImage.EndInit();

                        //Add Element
                        data.Add(new SanPhamHienThi() { ID = element.ID, Name = element.Ten, PathImage = newBitmapImage, Price = string.Format("{0:0,0} VND", (int)element.GiaBan) });
                    }
                    ++CountPhanTuHienThi;
                }
                
            }
            MaxTrang = (int)Math.Ceiling((double)CountPhanTuHienThi / SoPhanTuTrenMotTrang);
            
            if(data.Count >= 1)
            {
                for (int i = 12 - data.Count; i > 0; --i)
                {
                    data.Add(new SanPhamHienThi() { ID = -1});
                }
            }
            ltSanPham_Temp.ItemsSource = data;
        }

        static public int IDofItemSelected = -1;
        private void ltSanPham_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                SanPhamHienThi temp = (SanPhamHienThi)ltSanPham.SelectedItem;
                if(temp.ID != -1)
                {
                    IDofItemSelected = temp.ID;
                    NavigationService.Navigate(new ChiTiecSanPham());
                }
            }
        }

        private void btnThemSanPham_Click(object sender, RoutedEventArgs e)
        {
            //Dashboard.Tabs.SelectedIndex = 2;
            NavigationService.Navigate(new ThemSanPham());
            
        }

        private void btnDichTrai_Click(object sender, RoutedEventArgs e)
        {
            if(TrangHienTai > 0)
            {
                TrangHienTai--;
                LoadSanPham();
            }
            
        }

        private void btnDichPhai_Click(object sender, RoutedEventArgs e)
        {
           
            if (TrangHienTai < MaxTrang - 1)
            {
                TrangHienTai++;
                LoadSanPham();
            }
            
        }

        int CheckHien = 1;
        List<SanPhamHienThi> tempData = new List<SanPhamHienThi>();
        private void cmbSapXep_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<SanPhamHienThi> data = (List<SanPhamHienThi>)ltSanPham.ItemsSource;
            //if (tempData.Count == 0)
            //{
            //    tempData = data;
            //}
            

            if(CheckHien == 0)
            {
                if(cmbSapXep.SelectedIndex == 0)
                {
                    ltSanPham.Items.Refresh();
                    LoadSanPham();
                    return;
                }
                else if(cmbSapXep.SelectedIndex == 1)
                {
                    for(int i = 0; i < data.Count - 1; ++i)
                    {
                        if (data[i].ID == -1)
                        {
                            break;
                        }
                        for (int j = i + 1; j < data.Count ; ++j)
                        {
                            if(data[j].ID == -1)
                            {
                                break;
                            }
                            char s1 = data[i].Name[0];
                            char s2 = data[j].Name[0];
                            if (s1 > s2)
                            {
                                var temp = data[i];
                                data[i] = data[j];
                                data[j] = temp;

                            }
                        }
                    }
                }
                else if (cmbSapXep.SelectedIndex == 2)
                {
                    for (int i = 0; i < data.Count - 1; ++i)
                    {
                        if (data[i].ID == -1)
                        {
                            break;
                        }
                        for (int j = i + 1; j < data.Count; ++j)
                        {
                            if (data[j].ID == -1)
                            {
                                break;
                            }
                            char s1 = data[i].Name[0];
                            char s2 = data[j].Name[0];
                            if (s1 < s2)
                            {
                                var temp = data[i];
                                data[i] = data[j];
                                data[j] = temp;

                            }
                        }
                    }
                }
                else if (cmbSapXep.SelectedIndex == 3)
                {
                    for (int i = 0; i < data.Count - 1; ++i)
                    {
                        if (data[i].ID == -1)
                        {
                            break;
                        }
                        for (int j = i + 1; j < data.Count; ++j)
                        {
                            if (data[j].ID == -1)
                            {
                                break;
                            }
                            string s1 = data[i].Price.Replace("VND", "");
                            s1 = s1.Replace(",", "");
                            string s2 = data[j].Price.Replace("VND", "");
                            s2 = s2.Replace(",", "");
                            if (Int32.Parse(s1) > Int32.Parse(s2))
                            {
                                var temp = data[i];
                                data[i] = data[j];
                                data[j] = temp;

                            }

                        }
                    }
                }
                else if (cmbSapXep.SelectedIndex == 4)
                {
                    for (int i = 0; i < data.Count - 1; ++i)
                    {
                        if (data[i].ID == -1)
                        {
                            break;
                        }
                        for (int j = i + 1; j < data.Count; ++j)
                        {
                            if (data[j].ID == -1)
                            {
                                break;
                            }
                            string s1 = data[i].Price.Replace("VND", "");
                            s1 = s1.Replace(",", "");
                            string s2 = data[j].Price.Replace("VND", "");
                            s2 = s2.Replace(",", "");
                            if (int.Parse(s1) < int.Parse(s2))
                            {
                                var temp = data[i];
                                data[i] = data[j];
                                data[j] = temp;

                            }

                        }
                    }
                }
                ltSanPham.Items.Refresh();
                ltSanPham.ItemsSource = data;
            }
            else
            {
                --CheckHien;
            }
        }
    }
}


