using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using TrungTamNgoaiNgu.BUS;
using TrungTamNgoaiNgu.DTO;

namespace TrungTamNgoaiNgu.Controllers
{
    [RoutePrefix("api/ChungChi")]
    public class ChungChiController : ApiController
    {
        ChungChiBUS bus = new ChungChiBUS();

        [HttpGet]
        [Route("GetAll")]
        public List<ChungChiDTO> GetAll()
        {
            return bus.GetAll();
        }

        [HttpGet]
        [Route("GetByHocVien/{id}")]
        public List<ChungChiDTO> GetByHocVien(int id)
        {
            return bus.GetByHocVien(id);
        }

        [HttpPost]
        [Route("Create")]
        public IHttpActionResult Them(ChungChiDTO cc)
        {
            bool kq = bus.CapChungChi(cc);

            if (!kq)
                return BadRequest("Không thể cấp chứng chỉ");

            return Ok("Cấp chứng chỉ thành công");
        }



        [HttpDelete]
        [Route("Xoa/{id}")]
        public bool Xoa(int id)
        {
            return bus.Xoa(id);
        }

        [HttpGet]
        [Route("XuatPDF/{id}")]
        public HttpResponseMessage XuatPDF(int id)
        {
            byte[] fileBytes = bus.XuatPDF(id);
            if (fileBytes == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new ByteArrayContent(fileBytes);
            response.Content.Headers.ContentDisposition =
                new ContentDispositionHeaderValue("inline")
                {
                    FileName = "ChungChi.pdf"
                };
            response.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/pdf");

            return response;
        }


        [HttpGet]
        [Route("GetByKhoaHoc/{id}")]
        public List<ChungChiDTO> GetByKhoaHoc(int id)
        {
            return bus.GetByKhoaHoc(id);
        }

        [HttpGet]
        [Route("Check")]
        public IHttpActionResult Check(int idHocVien, int idKhoaHoc)
        {
            var cc = bus.GetByHocVienVaKhoaHoc(idHocVien, idKhoaHoc);

            if (cc == null)
                return Ok(new { daCap = false });

            return Ok(new
            {
                daCap = true,
                idChungChi = cc.IdChungChi
            });
        }


    }

}
