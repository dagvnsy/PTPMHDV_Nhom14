
CREATE DATABASE QuanLyTTNN
GO
USE QuanLyTTNN
GO




CREATE TABLE TaiKhoan (
    idTaiKhoan INT IDENTITY PRIMARY KEY,
    tenDangNhap NVARCHAR(100) NOT NULL UNIQUE,
    matKhau NVARCHAR(200) NOT NULL,
    loaiNguoiDung NVARCHAR(20) NOT NULL, -- ADMIN | GIANGVIEN | HOCVIEN
    idNguoiDung INT NOT NULL,
    trangThai BIT DEFAULT 1
)




CREATE TABLE Admin (
    idAdmin INT IDENTITY PRIMARY KEY,
    tenAdmin NVARCHAR(100),
    email NVARCHAR(100),
    soDienThoai NVARCHAR(50),
	idTaiKhoan INT UNIQUE,
    FOREIGN KEY (idTaiKhoan) REFERENCES TaiKhoan(idTaiKhoan)
)


CREATE TABLE GiangVien (
    idGiangVien INT IDENTITY PRIMARY KEY,
    tenGiangVien NVARCHAR(100),
    gioiTinh BIT,
    trinhDo NVARCHAR(100),
    chuyenMon NVARCHAR(200),
    email NVARCHAR(100),
    soDienThoai NVARCHAR(50),
	idTaiKhoan INT UNIQUE,
    FOREIGN KEY (idTaiKhoan) REFERENCES TaiKhoan(idTaiKhoan)
)


CREATE TABLE HocVien (
    idHocVien INT IDENTITY PRIMARY KEY,
    tenHocVien NVARCHAR(100),
    ngaySinh DATE,
    gioiTinh BIT,
    email NVARCHAR(100),
    soDienThoai NVARCHAR(50),
    diaChi NVARCHAR(200),
    trangThai NVARCHAR(50), -- DANG_HOC | BAO_LUU | HOAN_THANH
	idTaiKhoan INT UNIQUE,
    FOREIGN KEY (idTaiKhoan) REFERENCES TaiKhoan(idTaiKhoan)
)



CREATE TABLE KhoaHoc (
    idKhoaHoc INT IDENTITY PRIMARY KEY,
    tenKhoaHoc NVARCHAR(200),
    level NVARCHAR(50), -- A1, A2, B1, IELTS 5.0...
    soBuoiHoc INT,
    moTa NVARCHAR(500)
)



CREATE TABLE LopHoc (
    idLopHoc INT IDENTITY PRIMARY KEY,
    idKhoaHoc INT NOT NULL,
    tenLopHoc NVARCHAR(200),
    idGiangVien INT,
    hocPhi DECIMAL(18,2),
    ngayKhaiGiang DATE,
    ngayKetThuc DATE,
    FOREIGN KEY (idKhoaHoc) REFERENCES KhoaHoc(idKhoaHoc),
    FOREIGN KEY (idGiangVien) REFERENCES GiangVien(idGiangVien)
)



CREATE TABLE LichHoc (
    idLichHoc INT IDENTITY PRIMARY KEY,
    idLopHoc INT,
    ngayHoc DATE,
    CaHoc NVARCHAR(50),
	PhongHoc NVARCHAR(50),
    ghiChu NVARCHAR(200),
    FOREIGN KEY (idLopHoc) REFERENCES LopHoc(idLopHoc)
)



CREATE TABLE DangKyHoc (
    idDangKy INT IDENTITY PRIMARY KEY,
    idHocVien INT,
    idLopHoc INT,
    ngayDangKy DATE DEFAULT GETDATE(),
    trangThai NVARCHAR(50),
    UNIQUE (idHocVien, idLopHoc),
    FOREIGN KEY (idHocVien) REFERENCES HocVien(idHocVien),
    FOREIGN KEY (idLopHoc) REFERENCES LopHoc(idLopHoc)
)



CREATE TABLE KyThi (
    idKyThi INT IDENTITY PRIMARY KEY,
    tenKyThi NVARCHAR(200),
    loaiKyThi NVARCHAR(50), -- XEP_LOP | GIUA_KY | CUOI_KY
    idLopHoc INT NULL,
    idKhoaHoc INT NULL,
    ngayThi DATE,
    thangDiem INT DEFAULT 10,
    FOREIGN KEY (idLopHoc) REFERENCES LopHoc(idLopHoc),
    FOREIGN KEY (idKhoaHoc) REFERENCES KhoaHoc(idKhoaHoc)
)


CREATE TABLE KetQuaThi (
    idKyThi INT,
    idHocVien INT,
    diem DECIMAL(4,2),
    ketQua NVARCHAR(50),
    PRIMARY KEY (idKyThi, idHocVien),
    FOREIGN KEY (idKyThi) REFERENCES KyThi(idKyThi),
    FOREIGN KEY (idHocVien) REFERENCES HocVien(idHocVien)
)


CREATE TABLE HoaDon (
    idHoaDon INT IDENTITY PRIMARY KEY,
    idHocVien INT,
    idLopHoc INT,
    soTien DECIMAL(18,2),
    ngayLap DATE DEFAULT GETDATE(),
    trangThai NVARCHAR(50), -- DA_THANH_TOAN | CHUA_THANH_TOAN
    ghiChu NVARCHAR(200),
    FOREIGN KEY (idHocVien) REFERENCES HocVien(idHocVien),
    FOREIGN KEY (idLopHoc) REFERENCES LopHoc(idLopHoc)
)


CREATE TABLE ChungChi (
    idChungChi INT IDENTITY PRIMARY KEY,
    idHocVien INT,
    idKhoaHoc INT,
    ngayCap DATE,
    xepLoai NVARCHAR(50),
    FOREIGN KEY (idHocVien) REFERENCES HocVien(idHocVien),
    FOREIGN KEY (idKhoaHoc) REFERENCES KhoaHoc(idKhoaHoc)
)


INSERT INTO KhoaHoc (tenKhoaHoc, level, soBuoiHoc, moTa)
VALUES
(N'Tiếng Anh A1', 'A1', 24, N'Khóa học tiếng Anh cơ bản cho người mới bắt đầu'),
(N'Tiếng Anh A2', 'A2', 30, N'Nâng cao kỹ năng giao tiếp'),
(N'IELTS 5.5', 'IELTS', 36, N'Luyện thi IELTS trình độ trung bình'),
(N'IELTS 6.5', 'IELTS', 40, N'Luyện thi IELTS nâng cao');

select * from KhoaHoc
INSERT INTO GiangVien 
(tenGiangVien, gioiTinh, trinhDo, chuyenMon, email, soDienThoai)
VALUES
(N'Nguyễn Văn An', 1, N'Thạc sĩ', N'Tiếng Anh giao tiếp', 'an.gv@gmail.com', '0901111111'),
(N'Trần Thị Bình', 0, N'Cử nhân', N'IELTS', 'binh.gv@gmail.com', '0902222222');


INSERT INTO LopHoc
(idKhoaHoc, tenLopHoc, idGiangVien, hocPhi, ngayKhaiGiang, ngayKetThuc)
VALUES
(1, N'Lớp A1 - Sáng', 1, 2500000, '2025-01-10', '2025-03-10'),
(3, N'IELTS 5.5 - Tối', 2, 4500000, '2025-02-01', '2025-04-30');

INSERT INTO Admin (tenAdmin, email, soDienThoai, idTaiKhoan)
VALUES (N'Quản trị viên', 'admin@gmail.com', '0900000000', 1)


INSERT INTO TaiKhoan (tenDangNhap, matKhau, loaiNguoiDung, idNguoiDung)
VALUES ('admin', '123', 'ADMIN', 1)

INSERT INTO TaiKhoan (tenDangNhap, matKhau, loaiNguoiDung, idNguoiDung)
VALUES ('gv01', '123', 'GIANGVIEN', 1)

INSERT INTO GiangVien (tenGiangVien, gioiTinh, trinhDo, chuyenMon, email, soDienThoai, idTaiKhoan)
VALUES (N'Nguyễn Văn A', 1, N'Thạc sĩ', N'Tiếng Anh', 'gv@gmail.com', '0911111111', 2)
select * from GiangVien


INSERT INTO TaiKhoan (tenDangNhap, matKhau, loaiNguoiDung, idNguoiDung)
VALUES ('hv01', '123', 'HOCVIEN', 1)

INSERT INTO HocVien (tenHocVien, ngaySinh, gioiTinh, email, soDienThoai, diaChi, trangThai, idTaiKhoan)
VALUES (N'Trần Văn B', '2002-01-01', 1, 'hv@gmail.com', '0922222222', N'Hà Nội', N'DANG_HOC', 3)
