using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrungTamNgoaiNgu.DTO
{
    public class KetQuaThiDTO
    {
        public int IdKyThi { get; set; }
        public int IdHocVien { get; set; }
        public string TenHocVien { get; set; }
        public decimal? Diem { get; set; }
        public string KetQua { get; set; }
    }

}
