using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TrungTamNgoaiNgu.BUS;
using TrungTamNgoaiNgu.DTO;

namespace TrungTamNgoaiNgu.Controllers
{
    [RoutePrefix("api/BaoCao")]
    public class BaoCaoController : ApiController
    {
        BaoCaoBUS bus = new BaoCaoBUS();

        [HttpGet]
        [Route("SoLopTheoKhoaHoc")]
        public List<BaoCaoSoLopDTO> SoLopTheoKhoaHoc()
        {
            return bus.SoLopTheoKhoaHoc();
        }

        [HttpGet]
        [Route("HocVienTheoLop/{id}")]
        public List<BaoCaoHocVienDTO> HocVienTheoLop(int id)
        {
            return bus.HocVienTheoLop(id);
        }

        [HttpGet]
        [Route("KetQuaThi/{id}")]
        public List<BaoCaoKetQuaThiDTO> KetQuaThi(int id)
        {
            return bus.KetQuaThi(id);
        }


    }

}
