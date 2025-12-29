using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrungTamNgoaiNgu.DAL;
using TrungTamNgoaiNgu.DTO;
using TrungTamNgoaiNgu.DTO.TrungTamNgoaiNgu.DTO;

namespace TrungTamNgoaiNgu.BUS
{
    public class LopHocBUS
    {
        LopHocDAL dal = new LopHocDAL();

        public List<LopHocDTO> GetAll() => dal.GetAll();
        public LopHocDTO GetById(int id) => dal.GetById(id);
        public bool Them(LopHocDTO lop) => dal.Them(lop);
        public bool Sua(LopHocDTO lop) => dal.Sua(lop);
        public bool Xoa(int id)
        {
            if (dal.DemHocVienTrongLop(id) > 0)
                return false;

            return dal.Xoa(id);
        }

        public List<LopHocDTO> TimKiem(string tuKhoa) => dal.TimKiem(tuKhoa);

        public List<LopHocDTO> GetByHocVien(int idHocVien)
        {
            return dal.GetByHocVien(idHocVien);
        }
        public List<LopHocDTO> GetByKhoaHoc(int idKhoaHoc)
        => dal.GetByKhoaHoc(idKhoaHoc);
        public List<LopHocDTO> GetByGiangVien(int idGiangVien)
        {
            return dal.GetByGiangVien(idGiangVien);
        }
        public List<HocVienDTO> GetHocVienByLop(int idLopHoc)
        {
            return dal.GetHocVienByLop(idLopHoc);
        }
        public List<LopHocDTO> GetAllTen()
        {

            return dal.GetAllTen();
        }

    }

}
