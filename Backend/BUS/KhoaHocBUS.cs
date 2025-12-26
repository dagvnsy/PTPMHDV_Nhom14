using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrungTamNgoaiNgu.DAL;
using TrungTamNgoaiNgu.DTO;

namespace TrungTamNgoaiNgu.BUS
{
    public class KhoaHocBUS
    {
        KhoaHocDAL dal = new KhoaHocDAL();

        public List<KhoaHocDTO> GetAll() => dal.GetAll();
        public bool Them(KhoaHocDTO kh) => dal.Them(kh);
        public bool Sua(KhoaHocDTO kh) => dal.Sua(kh);
        public bool Xoa(int id) => dal.Xoa(id);
        public KhoaHocDTO GetById(int id) => dal.GetById(id);
        public List<KhoaHocDTO> TimKiem(string keyword)
        {
            return dal.TimKiem(keyword);
        }
        public List<KhoaHocDTO> LayTheoLoaiTieng(string loaiTieng)
        {
            return dal.GetByLoaiTieng(loaiTieng);
        }


    }
}
