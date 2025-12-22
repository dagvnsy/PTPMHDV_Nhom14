using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrungTamNgoaiNgu.DTO;
using TrungTamNgoaiNgu.DAL;

namespace TrungTamNgoaiNgu.BUS
{
    public class HocVienBUS
    {
        private HocVienDAL dal = new HocVienDAL();

        public List<HocVienDTO> XemTatCa() => dal.XemTatCa();

        public HocVienDTO LayTheoId(int idHocVien)
        {
            if (idHocVien <= 0) return null;
            return dal.LayTheoId(idHocVien);
        }

        public bool Them(HocVienDTO hv)
        {
            if (string.IsNullOrWhiteSpace(hv.TenHocVien)) return false;
            if (string.IsNullOrWhiteSpace(hv.Email)) return false;
            return dal.Them(hv);
        }

        public bool Sua(HocVienDTO hv)
        {
            return dal.Sua(hv);
        }


        public bool Xoa(int idHocVien)
        {
            return dal.Xoa(idHocVien);
        }


        public List<HocVienDTO> TimKiem(string tuKhoa)
        {
            if (string.IsNullOrWhiteSpace(tuKhoa)) return new List<HocVienDTO>();
            return dal.TimKiem(tuKhoa);
        }

        public HocVienDTO GetByTaiKhoan(int idTaiKhoan)
        {
            return dal.GetByTaiKhoan(idTaiKhoan);
        }

    }
}
