using Syncfusion.Windows.SampleLayout;
using Syncfusion.UI.Xaml.Charts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for ThongKeSanPham.xaml
    /// </summary>
    public partial class ThongKeSanPham : SampleLayoutWindow
    {
        public ThongKeSanPham()
        {
            InitializeComponent();
        }
    }

    public class ModelKhachHang
    {
        public string TimeXet
        {
            get;
            set;
        }
        public double TienNhap
        {
            get;
            set;
        }
        public double TienBan
        {
            get;
            set;
        }
    }

    public class ViewModelKhachHang
    {
        public ViewModelKhachHang()
        {
            this.DataPoints = new ObservableCollection<ModelKhachHang>();
            
            if (TrangThongKe.KieuThongKe == 0)
            {
                int YearXet = TrangThongKe.Year;
                int MonthXet = TrangThongKe.Month;
                DateTime DateXet = new DateTime(YearXet, MonthXet, 1);
                int DaysOfMonthXet = DateTime.DaysInMonth(YearXet, MonthXet);
                for (int i = 1; i <= DaysOfMonthXet; ++i)
                {
                    DateTime date = new DateTime(YearXet, MonthXet, i);
                    double TongNhap = 0;
                    double TongBan = 0;

                    var db = new MyDatabaseEntities();
                    foreach (var DH in db.DonHangs.ToList())
                    {
                        DateTime ngayTao = (DateTime)DH.NgayTao;
                        if (ngayTao.Day == date.Day && ngayTao.Month == date.Month && ngayTao.Year == date.Year)
                        {
                            TongNhap = (double)DH.TongGiaTriNhap;
                            TongBan = (double)DH.TongGiaTriBan;
                        }
                    }
                    DataPoints.Add(new ModelKhachHang() { TimeXet = i.ToString(), TienNhap = TongNhap / 1000000, TienBan = TongBan / 1000000, });
                }
            }
            else if (TrangThongKe.KieuThongKe == 1)
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

                    double TongNhap = 0;
                    double TongBan = 0;

                    var db = new MyDatabaseEntities();
                    foreach (var DH in db.DonHangs.ToList())
                    {
                        if (DH.NgayTao >= datestart && DH.NgayTao < dateEnd)
                        {
                            TongNhap += (double)DH.TongGiaTriNhap;
                            TongBan += (double)DH.TongGiaTriBan;
                            
                        }
                    }
                    
                    DataPoints.Add(new ModelKhachHang() { TimeXet = i.ToString(), TienNhap = TongNhap / 1000000, TienBan = TongBan / 1000000, });
                }
            }
            else if (TrangThongKe.KieuThongKe == 2)
            {
                int YearXet = TrangThongKe.Year;
                int i = 1;
                while (true)
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


                    double TongNhap = 0;
                    double TongBan = 0;


                    var db = new MyDatabaseEntities();
                    foreach (var DH in db.DonHangs.ToList())
                    {
                        if (DH.NgayTao >= datestart && DH.NgayTao < dateEnd)
                        {
                            TongNhap += (double)DH.TongGiaTriNhap;
                            TongBan += (double)DH.TongGiaTriBan;
                        }
                    }

                    if (i == 1)
                    {
                        DataPoints.Add(new ModelKhachHang() { TimeXet = "Quý 1", TienNhap = TongNhap / 1000000, TienBan = TongBan / 1000000, });
                        i = 4;
                    }
                    else if (i == 4)
                    {
                        DataPoints.Add(new ModelKhachHang() { TimeXet = "Quý 2", TienNhap = TongNhap / 1000000, TienBan = TongBan / 1000000, });
                        i = 7;
                    }
                    else if (i == 7)
                    {
                        DataPoints.Add(new ModelKhachHang() { TimeXet = "Quý 3", TienNhap = TongNhap / 1000000, TienBan = TongBan / 1000000, });
                        i = 10;
                    }
                    else if (i == 10)
                    {
                        DataPoints.Add(new ModelKhachHang() { TimeXet = "Quý 4", TienNhap = TongNhap / 1000000, TienBan = TongBan / 1000000, });
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

                    double TongNhap = 0;
                    double TongBan = 0;

                    var db = new MyDatabaseEntities();
                    foreach (var DH in db.DonHangs.ToList())
                    {
                        if (DH.NgayTao >= datestart && DH.NgayTao < dateEnd)
                        {
                            TongNhap += (double)DH.TongGiaTriNhap;
                            TongBan += (double)DH.TongGiaTriBan;
                        }
                    }
                    DataPoints.Add(new ModelKhachHang() { TimeXet = i.ToString(), TienNhap = TongNhap / 1000000, TienBan = TongBan / 1000000, });
                }
            }else if (TrangThongKe.KieuThongKe == 4)
            {
                DateTime datestart = TrangThongKe.DateStart;
                DateTime dateEnd = TrangThongKe.DateEnd;
                DateTime dateXet = datestart;
                while (true)
                {
                    
                    double TongNhap = 0;
                    double TongBan = 0;

                    var db = new MyDatabaseEntities();
                    foreach (var DH in db.DonHangs.ToList())
                    {
                        DateTime ngayTao = (DateTime)DH.NgayTao;
                        if (ngayTao.Day == dateXet.Day && ngayTao.Month == dateXet.Month && ngayTao.Year == dateXet.Year)
                        {
                            TongNhap = (double)DH.TongGiaTriNhap;
                            TongBan = (double)DH.TongGiaTriBan;
                        }
                    }
                    DataPoints.Add(new ModelKhachHang() { TimeXet = dateXet.ToShortDateString(), TienNhap = TongNhap / 1000000, TienBan = TongBan / 1000000, });
                    dateXet = dateXet.AddDays(1);
                    if(dateXet == dateEnd)
                    {
                        break;
                    }
                }
            }
        }

        public ObservableCollection<ModelKhachHang> DataPoints
        {
            get;
            set;
        }
    }
}
