using Syncfusion.UI.Xaml.Charts;
using Syncfusion.Windows.SampleLayout;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ThongKeDoanhThu : SampleLayoutWindow
    {

        public ThongKeDoanhThu()
        {
            InitializeComponent();
        }
    }

    public class Model
    {
        public double ItemsCount { get; set; }

        public string Brand { get; set; }
    }

    public class ViewModel
    {
        public ViewModel()
        {
            this.SneakersDetail = new ObservableCollection<Model>();

            if(TrangThongKe.KieuThongKe == 0)
            {
                int YearXet = TrangThongKe.Year;
                int MonthXet = TrangThongKe.Month;
                DateTime DateXet = new DateTime(YearXet, MonthXet, 1);
                int DaysOfMonthXet = DateTime.DaysInMonth(YearXet, MonthXet);
                for (int i = 1; i <= DaysOfMonthXet; ++i)
                {
                    DateTime date= new DateTime(YearXet, MonthXet, i);
                    double DoanhThu = 0;

                    var db = new MyDatabaseEntities();
                    foreach (var DH in db.DonHangs.ToList())
                    {
                        DateTime ngayTao = (DateTime)DH.NgayTao;
                        if (ngayTao.Day == date.Day && ngayTao.Month == date.Month && ngayTao.Year == date.Year)
                        {
                            DoanhThu += (double)DH.TongGiaTriBan - (double)DH.TongGiaTriNhap;
                        }
                    }
                    SneakersDetail.Add(new Model() { Brand = "" + i, ItemsCount = DoanhThu / 1000000 });
                }
            }
            else if(TrangThongKe.KieuThongKe == 1)
            {
                int YearXet = TrangThongKe.Year;
                for (int i = 1; i <= 12; ++i)
                {
                    DateTime datestart = new DateTime(YearXet, i, 1);
                    DateTime dateEnd;
                    if (i + 1 > 12)
                    {
                        dateEnd = new DateTime(YearXet + 1, i, 1);
                    }
                    else
                    {
                        dateEnd = new DateTime(YearXet, i + 1, 1);
                    }


                    double DoanhThu = 0;

                    var db = new MyDatabaseEntities();
                    foreach (var DH in db.DonHangs.ToList())
                    {
                        if (DH.NgayTao >= datestart && DH.NgayTao < dateEnd)
                        {
                            DoanhThu += (double)DH.TongGiaTriBan - (double)DH.TongGiaTriNhap;
                        }
                    }
                    SneakersDetail.Add(new Model() { Brand = "" + i, ItemsCount = DoanhThu / 1000000 });
                }
            }
            else if(TrangThongKe.KieuThongKe == 2)
            {
                int YearXet = TrangThongKe.Year;
                int i = 1;
                while(true)
                {
                    DateTime datestart = new DateTime(YearXet, i, 1);
                    DateTime dateEnd;
                    if (i == 10)
                    {
                         dateEnd = new DateTime(YearXet + 1, 1, 1);
                    }
                    else
                    {
                        dateEnd = new DateTime(YearXet, i + 3, 1);

                    }


                    double DoanhThu = 0;

                    var db = new MyDatabaseEntities();
                    foreach (var DH in db.DonHangs.ToList())
                    {
                        if (DH.NgayTao >= datestart && DH.NgayTao < dateEnd)
                        {
                            DoanhThu += (double)DH.TongGiaTriBan / 1000000 - (double)DH.TongGiaTriNhap / 1000000;
                        }
                    }

                    if(i == 1)
                    {
                        SneakersDetail.Add(new Model() { Brand = "Quý " + 1, ItemsCount = DoanhThu });
                        i = 4;
                    }
                    else if(i == 4)
                    {
                        SneakersDetail.Add(new Model() { Brand = "Quý " + 2, ItemsCount = DoanhThu });
                        i = 7;
                    }
                    else if (i == 7)
                    {
                        SneakersDetail.Add(new Model() { Brand = "Quý " + 3, ItemsCount = DoanhThu });
                        i = 10;
                    }
                    else if (i == 10)
                    {
                        SneakersDetail.Add(new Model() { Brand = "Quý " + 4, ItemsCount = DoanhThu });
                        break;
                    }
                }
            }
            else if (TrangThongKe.KieuThongKe == 3)
            {
                int YearNow = DateTime.Now.Year;
                for (int i = 2015; i <= YearNow; ++i)
                {
                    DateTime datestart = new DateTime(i, 1, 1);
                    DateTime dateEnd = new DateTime(i + 1, 1, 1); ;
                    
                    double DoanhThu = 0;

                    var db = new MyDatabaseEntities();
                    foreach (var DH in db.DonHangs.ToList())
                    {
                        if (DH.NgayTao >= datestart && DH.NgayTao < dateEnd)
                        {
                            DoanhThu += (double)DH.TongGiaTriBan - (double)DH.TongGiaTriNhap;
                        }
                    }
                    SneakersDetail.Add(new Model() { Brand = "" + i, ItemsCount = DoanhThu / 1000000 });
                }
            }
            else if (TrangThongKe.KieuThongKe == 4)
            {
                DateTime datestart = TrangThongKe.DateStart;
                DateTime dateEnd = TrangThongKe.DateEnd;
                DateTime dateNow = datestart;
                while(true)
                {
                    double DoanhThu = 0;
                    var db = new MyDatabaseEntities();
                    foreach (var DH in db.DonHangs.ToList())
                    {
                        DateTime temp  = (DateTime)DH.NgayTao;
                        if (temp.Month == dateNow.Month && temp.Year == dateNow.Year && temp.Day == dateNow.Day)
                        {
                            DoanhThu += (double)DH.TongGiaTriBan - (double)DH.TongGiaTriNhap;
                        }
                    }
                    SneakersDetail.Add(new Model() { Brand = dateNow.ToShortDateString(), ItemsCount = DoanhThu / 1000000 });
                    dateNow = dateNow.AddDays(1);
                    if(dateNow == dateEnd)
                    {
                        break;
                    }
                }
            }
        }

        public ObservableCollection<Model> SneakersDetail { get; set; }
    }

}

