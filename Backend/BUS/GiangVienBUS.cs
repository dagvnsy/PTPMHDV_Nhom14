using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrungTamNgoaiNgu.DAL;
using TrungTamNgoaiNgu.DTO;

namespace TrungTamNgoaiNgu.BUS
{
    public class GiangVienBUS
    {
        GiangVienDAL dal = new GiangVienDAL();

        public List<GiangVienDTO> GetAll()
        {
            return dal.GetAll();
        }

        public GiangVienDTO GetById(int id)
        {
            return dal.GetById(id);
        }

        public List<GiangVienDTO> TimKiem(string tuKhoa)
        {
            return dal.TimKiem(tuKhoa);
        }

        public bool Them(GiangVienDTO gv)
        {
            return dal.Them(gv);
        }

        public bool Sua(GiangVienDTO gv)
        {
            return dal.Sua(gv);
        }

        public bool Xoa(int id)
        {
            return dal.Xoa(id);
        }

        public GiangVienDTO GetByTaiKhoan(int idTaiKhoan)
        {
            return dal.GetByTaiKhoan(idTaiKhoan);
        }

    }
}
