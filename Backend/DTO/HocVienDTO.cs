using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrungTamNgoaiNgu.DTO
{
    public class HocVienDTO
    {
        public int IdHocVien { get; set; }
        public string TenHocVien { get; set; }
        public DateTime? NgaySinh { get; set; }
        public bool GioiTinh { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
        public string DiaChi { get; set; }
        public string TrangThai { get; set; }
        public int IdTaiKhoan { get; set; }
    }
}
