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
    [RoutePrefix("api/KetQuaThi")]
    public class KetQuaThiController : ApiController
    {
        KetQuaThiBUS bus = new KetQuaThiBUS();


        [HttpGet]
        [Route("GetByLopHoc/{idKyThi}")]
        public IHttpActionResult GetByLopHoc(int idKyThi)
        {
            return Ok(bus.GetByLopHoc(idKyThi));
        }

        [HttpPost]
        [Route("Luu")]
        public IHttpActionResult Luu(KetQuaThiDTO dto)
        {
            return Ok(bus.Luu(dto));
        }

        [HttpGet]
        [Route("GetByKyThi/{idKyThi}")]
        public List<KetQuaThiDTO> GetByKyThi(int idKyThi)
        {
            return bus.GetByKyThi(idKyThi);
        }


        [HttpPut]
        [Route("Update")]
        public bool Update(KetQuaThiDTO kq)
        {
            return bus.Sua(kq);
        }

    }
}
