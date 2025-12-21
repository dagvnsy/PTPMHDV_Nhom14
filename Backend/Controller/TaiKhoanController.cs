using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TrungTamNgoaiNgu.BUS;
using TrungTamNgoaiNgu.DTO.TrungTamNgoaiNgu.DTO;

namespace TrungTamNgoaiNgu.Controllers
{
    [RoutePrefix("api/TaiKhoan")]
    public class TaiKhoanController : ApiController
    {
        TaiKhoanBUS bus = new TaiKhoanBUS();

        [HttpPost]
        [Route("Them")]
        public IHttpActionResult Them(TaiKhoanDTO tk)
        {
            return Ok(bus.Them(tk));
        }

        [HttpPut]
        [Route("DoiMatKhau/{id}")]
        public IHttpActionResult DoiMatKhau(int id, [FromBody] string matKhauMoi)
        {
            return Ok(bus.DoiMatKhau(id, matKhauMoi));
        }

        [HttpPut]
        [Route("TrangThai/{id}")]
        public IHttpActionResult CapNhatTrangThai(int id, bool trangThai)
        {
            return Ok(bus.CapNhatTrangThai(id, trangThai));
        }
    }
}
