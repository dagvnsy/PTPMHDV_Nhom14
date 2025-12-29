using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrungTamNgoaiNgu.DTO
{
    public class LopHocDTO
    {
        public int IdLopHoc { get; set; }
        public int IdKhoaHoc { get; set; }
        public string TenLopHoc { get; set; }
        public int IdGiangVien { get; set; }
        public decimal HocPhi { get; set; }
        public DateTime NgayKhaiGiang { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public string TenKhoaHoc { get; set; }
        public string TenGiangVien { get; set; }
    }


}
