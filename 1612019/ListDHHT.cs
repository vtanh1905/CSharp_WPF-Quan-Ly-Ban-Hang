using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace _1612019
{
    class DonHangHienThi
    {
        public int ID { get; set; }
        public ImageSource TinhTrang { get; set; }
        public ImageSource LoaiDonHang { get; set; }
        public string CMND { get; set; }
        public string HoTen { get; set; }
        public string SDT { get; set; }
        public string DiaChi { get; set; }
        public DateTime Ngay { get; set; }
        public string TongTien { get; set; }
        public string TienConLai { get; set; }
    }

    class ListDHHT
    {
        static public List<DonHangHienThi> data = new List<DonHangHienThi>();
    }
}
