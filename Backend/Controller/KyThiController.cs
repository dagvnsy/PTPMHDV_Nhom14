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
    [RoutePrefix("api/KyThi")]
    public class KyThiController : ApiController
    {
        KyThiBUS bus = new KyThiBUS();

        [HttpGet]
        [Route("GetAll")]
        public List<KyThiDTO> GetAll()
        {
            return bus.GetAll();
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public KyThiDTO GetById(int id)
        {
            return bus.GetById(id);
        }

        [HttpGet]
        [Route("GetByHocVien/{idHocVien}")]
        public IHttpActionResult GetByHocVien(int idHocVien)
        {
            return Ok(bus.GetByHocVien(idHocVien));
        }


        [HttpPost]
        [Route("Create")]
        public bool Create(KyThiDTO kt)
        {
            return bus.Them(kt);
        }

        [HttpPut]
        [Route("Update/{id}")]
        public bool Update(int id, KyThiDTO kt)
        {
            kt.IdKyThi = id;
            return bus.Sua(kt);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public bool Delete(int id)
        {
            return bus.Xoa(id);
        }

        [HttpPut]
        [Route("CapNhatTrangThaiNhapDiem")]
        public IHttpActionResult CapNhatTrangThaiNhapDiem(KyThiDTO dto)
        {
            return Ok(bus.CapNhatTrangThaiNhapDiem(dto.IdKyThi, dto.IsMoNhapDiem));
        }

        [HttpGet]
        [Route("GetByLopHoc/{idLopHoc}")]
        public IHttpActionResult GetByLopHoc(int idLopHoc)
        {
            return Ok(bus.GetByLopHoc(idLopHoc));
        }

    }
}
