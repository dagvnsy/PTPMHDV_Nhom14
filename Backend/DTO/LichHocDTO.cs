using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrungTamNgoaiNgu.DTO
{
    public class LichHocDTO
    {
        public int IdLichHoc { get; set; }
        public int IdLopHoc { get; set; }
        public DateTime NgayHoc { get; set; }
        public string CaHoc { get; set; }
        public string PhongHoc { get; set; }
        public string GhiChu { get; set; }

        public string TenLopHoc { get; set; }
        public string TenKhoaHoc { get; set; }
        public string TenGiangVien { get; set; }
    }

}
