using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrungTamNgoaiNgu.DTO
{
    public class HoaDonDTO
    {
        public int IdHoaDon { get; set; }
        public int IdHocVien { get; set; }
        public string TenHocVien { get; set; }
        public int IdLopHoc { get; set; }
        public string TenLopHoc { get; set; }
        public decimal SoTien { get; set; }
        public DateTime NgayLap { get; set; }
        public string TrangThai { get; set; }
        public string GhiChu { get; set; }
    }

}
