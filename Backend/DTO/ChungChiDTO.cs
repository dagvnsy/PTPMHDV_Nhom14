using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrungTamNgoaiNgu.DTO
{
    public class ChungChiDTO
    {
        public int IdChungChi { get; set; }
        public int IdHocVien { get; set; }
        public int IdKhoaHoc { get; set; }
        public DateTime NgayCap { get; set; }
        public string XepLoai { get; set; }
        public string TenHocVien { get; set; }
        public decimal Diem { get; set; }
    }

}
