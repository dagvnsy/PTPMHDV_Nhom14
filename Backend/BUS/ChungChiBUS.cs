using System;
using System.Collections.Generic;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TrungTamNgoaiNgu.DAL;
using TrungTamNgoaiNgu.DTO;


namespace TrungTamNgoaiNgu.BUS
{
    public class ChungChiBUS
    {
        ChungChiDAL dal = new ChungChiDAL();

        public bool CapChungChi(ChungChiDTO cc)
        {
            return dal.Them(
                cc.IdHocVien,
                cc.IdKhoaHoc,
                DateTime.Now,
                cc.XepLoai
            );
        }

        public bool Xoa(int id)
        {
            return dal.Xoa(id);
        }

        public List<ChungChiDTO> GetAll()
        {
            return dal.GetAll();
        }

        public List<ChungChiDTO> GetByHocVien(int id)
        {
            return dal.GetByHocVien(id);
        }



        //xuất chứng chỉ
        public byte[] XuatPDF(int idChungChi)
        {
            var data = dal.LayThongTinInPDF(idChungChi);
            if (data == null) return null;

            using (MemoryStream ms = new MemoryStream())
            {
                Document doc = new Document(PageSize.A4);
                PdfWriter.GetInstance(doc, ms);
                doc.Open();

                string fontPath = Path.Combine(
                     AppDomain.CurrentDomain.BaseDirectory,
                     "Fonts",
                     "times.ttf"
                );

                BaseFont bf = BaseFont.CreateFont(
                    fontPath,
                    BaseFont.IDENTITY_H,
                    BaseFont.EMBEDDED
                );

                Font titleFont = new Font(bf, 18, Font.BOLD);
                Font textFont = new Font(bf, 12);


                doc.Add(new Paragraph("CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM", titleFont)
                {
                    Alignment = Element.ALIGN_CENTER
                });

                doc.Add(new Paragraph("Độc lập - Tự do - Hạnh phúc", titleFont)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 30
                });

                doc.Add(new Paragraph("CHỨNG CHỈ HOÀN THÀNH KHÓA HỌC", titleFont)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 40
                });

                doc.Add(new Paragraph("Học viên: " + data.TenHocVien, textFont));
                doc.Add(new Paragraph("Đã hoàn thành khóa học: " + data.TenKhoaHoc, textFont));
                doc.Add(new Paragraph("Xếp loại: " + data.XepLoai, textFont));
                doc.Add(new Paragraph(
                    "Ngày cấp: " + data.NgayCap.ToString("dd/MM/yyyy"),
                    textFont
                ));
                doc.Add(new Paragraph("Hội đồng kiểm tra: Trung tâm ngoại ngữ ABC ", textFont)
                {
                    SpacingAfter = 20
                });

                doc.Add(new Paragraph("\nTrung tâm ngoại ngữ", textFont)
                {
                    Alignment = Element.ALIGN_LEFT,

                });
                doc.Add(new Paragraph("\nNgười cấp", textFont)
                {
                    Alignment = Element.ALIGN_LEFT,

                });
                doc.Add(new Paragraph("\nAdmin", textFont)
                {
                    Alignment = Element.ALIGN_LEFT,
                });

                doc.Close();
                return ms.ToArray();
            }
        }


        public List<ChungChiDTO> GetByKhoaHoc(int idKhoaHoc)
        {
            return dal.GetByKhoaHoc(idKhoaHoc);
        }

        public ChungChiDTO GetByHocVienVaKhoaHoc(int idHocVien, int idKhoaHoc)
        {
            return dal.GetByHocVienVaKhoaHoc(idHocVien, idKhoaHoc);
        }

    }

}
