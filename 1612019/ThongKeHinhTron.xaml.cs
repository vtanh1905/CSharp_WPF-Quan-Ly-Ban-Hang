#region Copyright Syncfusion Inc. 2001-2018.
// Copyright Syncfusion Inc. 2001-2018. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.UI.Xaml.Charts;
using Syncfusion.Windows.SampleLayout;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    public partial class ThongKeHinhTron : SampleLayoutWindow
    {
        Doughnut doughnutSeries = new Doughnut();

        //MultipleDoughnut multipleDoughnutSeries = new MultipleDoughnut();

        public ThongKeHinhTron()
        {
            InitializeComponent();
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var combobox = sender as ComboBox;
            if (contentcontrol != null && combobox != null)
            {
                if (combobox.SelectedIndex == 0)
                {
                    contentcontrol.Content = new Doughnut();
                }
                else if (combobox.SelectedIndex == 1)
                {
                    //contentcontrol.Content = new MultipleDoughnut();
                }
                else
                {
                    //contentcontrol.Content = new StackedDoughnutDemo();
                }
            }
        }
    }

    public class ModelHinhTron
    {
        public string Category { get; set; }

        public double Percentage { get; set; }
    }

    public class Populations
    {
        public string Continent { get; set; }

        public string Countries { get; set; }

        public string States { get; set; }

        public double PopulationinStates { get; set; }

        public double PopulationinCountries { get; set; }

        public double PopulationinContinents { get; set; }

        public string Category { get; set; }

        public double Expenditure { get; set; }

        public Uri Image { get; set; }
    }

    public class ViewModelHinhTronHinhTron
    {
        public ViewModelHinhTronHinhTron()
        {
            this.Tax = new List<ModelHinhTron>();
            var db = new MyDatabaseEntities();



            if (TrangThongKe.KieuThongKe == 0)
            {
                int YearXet = TrangThongKe.Year;
                int MonthXet = TrangThongKe.Month;
                int DayXet = TrangThongKe.Day;
                //Tong SLL SP
                int TongSLDaBan = 0;

                List<GioHang> data = new List<GioHang>();
                foreach (var DH in db.DonHangs.ToList())
                {
                    DateTime dateTime = (DateTime)DH.NgayTao;
                    if (dateTime.Year == YearXet && dateTime.Month == MonthXet && dateTime.Day == DayXet)
                    {

                        foreach (var GH in db.GioHangs.ToList())
                        {
                            if (DH.GioHang.ID == GH.ID)
                            {
                                data.Add(GH);
                                TongSLDaBan += (int)GH.SoLuong;
                            }
                        }
                    }
                }

                for (int i = 0; i < data.Count; ++i)
                {
                    if (data[i].SanPham_ID == 0)
                    {
                        continue;
                    }
                    int TongSL = (int)data[i].SoLuong;
                    for (int j = i + 1; j < data.Count; ++j)
                    {
                        if (data[i].SanPham_ID == data[j].SanPham_ID)
                        {
                            TongSL += (int)data[j].SoLuong;
                            data[j].SanPham_ID = 0;
                        }
                    }
                    Tax.Add(new ModelHinhTron() { Category = data[i].SanPham.Ten, Percentage = (double)TongSL * 100 / TongSLDaBan });
                }

            }
            else if (TrangThongKe.KieuThongKe == 1)
            {
                int YearXet = TrangThongKe.Year;
                int MonthXet = TrangThongKe.Month;

                //Tong SLL SP
                int TongSLDaBan = 0;

                List<GioHang> data = new List<GioHang>();
                foreach (var DH in db.DonHangs.ToList())
                {
                    DateTime dateTime = (DateTime)DH.NgayTao;
                    if (dateTime.Year == YearXet && dateTime.Month == MonthXet)
                    {

                        foreach (var GH in db.GioHangs.ToList())
                        {
                            if (DH.GioHang.ID == GH.ID)
                            {
                                data.Add(GH);
                                TongSLDaBan += (int)GH.SoLuong;
                            }
                        }
                    }
                }

                for (int i = 0; i < data.Count; ++i)
                {
                    if (data[i].SanPham_ID == 0)
                    {
                        continue;
                    }
                    int TongSL = (int)data[i].SoLuong;
                    for (int j = i + 1; j < data.Count; ++j)
                    {
                        if (data[i].SanPham_ID == data[j].SanPham_ID)
                        {
                            TongSL += (int)data[j].SoLuong;
                            data[j].SanPham_ID = 0;
                        }
                    }
                    Tax.Add(new ModelHinhTron() { Category = data[i].SanPham.Ten, Percentage = (double)TongSL * 100 / TongSLDaBan });
                }

            }
            else if (TrangThongKe.KieuThongKe == 2)
            {
                int YearXet = TrangThongKe.Year;

                int QuyXet = TrangThongKe.Quy;
                int MonthStart = -1;
                int MonthEnd = -1;
                if (QuyXet == 1)
                {
                    MonthStart = 1;
                    MonthEnd = 3;
                }
                else if (QuyXet == 2)
                {
                    MonthStart = 4;
                    MonthEnd = 6;
                }
                else if (QuyXet == 3)
                {
                    MonthStart = 7;
                    MonthEnd = 9;
                }
                else if (QuyXet == 4)
                {
                    MonthStart = 10;
                    MonthEnd = 12;
                }

                //Tong SLL SP
                int TongSLDaBan = 0;

                List<GioHang> data = new List<GioHang>();
                foreach (var DH in db.DonHangs.ToList())
                {
                    DateTime dateTime = (DateTime)DH.NgayTao;
                    if (dateTime.Year == YearXet && dateTime.Month >= MonthStart && dateTime.Month <= MonthEnd)
                    {

                        foreach (var GH in db.GioHangs.ToList())
                        {
                            if (DH.GioHang.ID == GH.ID)
                            {
                                data.Add(GH);
                                TongSLDaBan += (int)GH.SoLuong;
                            }
                        }
                    }
                }

                for (int i = 0; i < data.Count; ++i)
                {
                    if (data[i].SanPham_ID == 0)
                    {
                        continue;
                    }
                    int TongSL = (int)data[i].SoLuong;
                    for (int j = i + 1; j < data.Count; ++j)
                    {
                        if (data[i].SanPham_ID == data[j].SanPham_ID)
                        {
                            TongSL += (int)data[j].SoLuong;
                            data[j].SanPham_ID = 0;
                        }
                    }
                    Tax.Add(new ModelHinhTron() { Category = data[i].SanPham.Ten, Percentage = (double)TongSL * 100 / TongSLDaBan });
                }

            }
            else if (TrangThongKe.KieuThongKe == 3)
            {
                int YearXet = TrangThongKe.Year;

                //Tong SLL SP
                int TongSLDaBan = 0;

                List<GioHang> data = new List<GioHang>();
                foreach (var DH in db.DonHangs.ToList())
                {
                    DateTime dateTime = (DateTime)DH.NgayTao;
                    if (dateTime.Year == YearXet)
                    {

                        foreach (var GH in db.GioHangs.ToList())
                        {
                            if (DH.GioHang.ID == GH.ID)
                            {
                                data.Add(GH);
                                TongSLDaBan += (int)GH.SoLuong;
                            }
                        }
                    }
                }

                for (int i = 0; i < data.Count; ++i)
                {
                    if (data[i].SanPham_ID == 0)
                    {
                        continue;
                    }
                    int TongSL = (int)data[i].SoLuong;
                    for (int j = i + 1; j < data.Count; ++j)
                    {
                        if (data[i].SanPham_ID == data[j].SanPham_ID)
                        {
                            TongSL += (int)data[j].SoLuong;
                            data[j].SanPham_ID = 0;
                        }
                    }
                    Tax.Add(new ModelHinhTron() { Category = data[i].SanPham.Ten, Percentage = (double)TongSL * 100 / TongSLDaBan });
                }

            }
            else if (TrangThongKe.KieuThongKe == 4)
            {
                DateTime datestart = TrangThongKe.DateStart;
                DateTime dateEnd = TrangThongKe.DateEnd;
                DateTime dateNow = datestart;

                //Tong SLL SP
                int TongSLDaBan = 0;

                List<GioHang> data = new List<GioHang>();
                foreach (var DH in db.DonHangs.ToList())
                {
                    DateTime dateTime = (DateTime)DH.NgayTao;
                    if (dateTime >= datestart && dateTime <= dateEnd)
                    {

                        foreach (var GH in db.GioHangs.ToList())
                        {
                            if (DH.GioHang.ID == GH.ID)
                            {
                                data.Add(GH);
                                TongSLDaBan += (int)GH.SoLuong;
                            }
                        }
                    }
                }

                for (int i = 0; i < data.Count; ++i)
                {
                    if (data[i].SanPham_ID == 0)
                    {
                        continue;
                    }
                    int TongSL = (int)data[i].SoLuong;
                    for (int j = i + 1; j < data.Count; ++j)
                    {
                        if (data[i].SanPham_ID == data[j].SanPham_ID)
                        {
                            TongSL += (int)data[j].SoLuong;
                            data[j].SanPham_ID = 0;
                        }
                    }
                    Tax.Add(new ModelHinhTron() { Category = data[i].SanPham.Ten, Percentage = (double)TongSL * 100 / TongSLDaBan });
                }
            }
        }

        public IList<ModelHinhTron> Tax
        {
            get;
            set;
        }

        public IList<Populations> Population
        {
            get;
            set;
        }

        public IList<Populations> ExpenditureData
        {
            get;
            set;
        }
    }
    public class Labelconvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return String.Format("{0} %", value);
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }

    public class LegendConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
