using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrungTamNgoaiNgu.DTO
{
    public class KyThiDTO
    {
        public int IdKyThi { get; set; }
        public string TenKyThi { get; set; }
        public string LoaiKyThi { get; set; }
        public int? IdLopHoc { get; set; }
        public int? IdKhoaHoc { get; set; }
        public DateTime NgayThi { get; set; }
        public int ThangDiem { get; set; }

        public bool IsMoNhapDiem { get; set; }
        public decimal? Diem { get; set; }
        public string KetQua { get; set; }
    }

}
