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
    [RoutePrefix("api/HoaDon")]
    public class HoaDonController : ApiController
    {
        HoaDonBUS bus = new HoaDonBUS();

        [HttpGet]
        [Route("GetAll")]
        public List<HoaDonDTO> GetAll()
        {
            return bus.GetAll();
        }

        [HttpPost]
        [Route("Them")]
        public bool Them(HoaDonDTO hd)
        {
            return bus.Them(hd);
        }

        [HttpPut]
        [Route("Sua")]
        public bool Sua(HoaDonDTO hd)
        {
            return bus.Sua(hd);
        }

        [HttpDelete]
        [Route("Xoa/{id}")]
        public bool Xoa(int id)
        {
            return bus.Xoa(id);
        }


        [HttpGet]
        [Route("GetByHocVien/{idHocVien}")]
        public IHttpActionResult GetByHocVien(int idHocVien)
        {
            var ds = bus.GetByHocVien(idHocVien);
            return Ok(ds);
        }
    }

}
