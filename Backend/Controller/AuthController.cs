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
    [RoutePrefix("api/Auth")]
    public class AuthController : ApiController
    {
        TaiKhoanBUS bus = new TaiKhoanBUS();

        [HttpPost]
        [Route("Login")]
        public IHttpActionResult Login(DangNhapDTO dto)
        {
            var tk = bus.DangNhap(dto);
            if (tk == null)
                return Unauthorized();

            return Ok(tk);
        }
    }
}
