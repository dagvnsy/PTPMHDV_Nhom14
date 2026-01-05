using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrungTamNgoaiNgu.DAL;
using TrungTamNgoaiNgu.DTO;

namespace TrungTamNgoaiNgu.BUS
{
    public class HoaDonBUS
    {
        private HoaDonDAL dal = new HoaDonDAL();

        public bool Them(HoaDonDTO hd)
        {
            return dal.Them(hd);
        }

        public bool Sua(HoaDonDTO hd)
        {
            return dal.Sua(hd);
        }

        public bool Xoa(int id)
        {
            return dal.Xoa(id);
        }

        public List<HoaDonDTO> GetAll()
        {
            return dal.GetAll();
        }

        public List<HoaDonDTO> GetByHocVien(int idHocVien)
        {
            return dal.GetByHocVien(idHocVien);
        }
    }


}
