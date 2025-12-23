using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrungTamNgoaiNgu.DTO
{
    public class GiangVienDTO
    {
        public int IdGiangVien { get; set; }
        public string TenGiangVien { get; set; }
        public bool GioiTinh { get; set; }
        public string TrinhDo { get; set; }
        public string ChuyenMon { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
    }
}
