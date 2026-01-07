
CREATE DATABASE QLTTNN
GO
USE QLTTNN
GO




CREATE TABLE TaiKhoan (
    idTaiKhoan INT IDENTITY PRIMARY KEY,
    tenDangNhap NVARCHAR(100) NOT NULL UNIQUE,
    matKhau NVARCHAR(200) NOT NULL,
    loaiNguoiDung NVARCHAR(20) NOT NULL, -- ADMIN | GIANGVIEN | HOCVIEN
    trangThai BIT DEFAULT 1
)

select * from TaiKhoan



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
select * from GiangVien

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

SELECT * FROM HocVien WHERE idTaiKhoan =1

CREATE TABLE KhoaHoc (
    idKhoaHoc INT IDENTITY PRIMARY KEY,
    tenKhoaHoc NVARCHAR(200),
    level NVARCHAR(50), -- A1, A2, B1, IELTS 5.0...
    soBuoiHoc INT,
    moTa NVARCHAR(500),
	loaiTieng NVARCHAR(50) NOT NULL
)
select * from KhoaHoc







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
select * from LopHoc


CREATE TABLE LichHoc (
    idLichHoc INT IDENTITY PRIMARY KEY,
    idLopHoc INT,
    ngayHoc DATE,
    CaHoc NVARCHAR(50),
	PhongHoc NVARCHAR(50),
    ghiChu NVARCHAR(200),
    FOREIGN KEY (idLopHoc) REFERENCES LopHoc(idLopHoc)
)

--Lấy lịch dạy giảng viên
CREATE PROCEDURE sp_LichDay_GetByGiangVien
    @idGiangVien INT
AS
BEGIN
    SELECT 
        lh.idLichHoc,
        lh.idLopHoc,
        l.tenLopHoc,
        k.tenKhoaHoc,
        lh.ngayHoc,
        lh.CaHoc,
        lh.PhongHoc,
        lh.ghiChu
    FROM LichHoc lh
    JOIN LopHoc l ON lh.idLopHoc = l.idLopHoc
    JOIN KhoaHoc k ON l.idKhoaHoc = k.idKhoaHoc
    WHERE l.idGiangVien = @idGiangVien
    ORDER BY lh.ngayHoc
END




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

select * from DangKyHoc

CREATE TABLE KyThi (
    idKyThi INT IDENTITY PRIMARY KEY,
    tenKyThi NVARCHAR(200),
    loaiKyThi NVARCHAR(50), -- XEP_LOP | GIUA_KY | CUOI_KY
    idLopHoc INT NULL,
    idKhoaHoc INT NULL,
    ngayThi DATE,
    thangDiem INT DEFAULT 10,
	IsMoNhapDiem BIT NOT NULL DEFAULT 0,
    FOREIGN KEY (idLopHoc) REFERENCES LopHoc(idLopHoc),
    FOREIGN KEY (idKhoaHoc) REFERENCES KhoaHoc(idKhoaHoc)
)
select * from Kythi

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
select * from HoaDon



CREATE TABLE ChungChi (
    idChungChi INT IDENTITY PRIMARY KEY,
    idHocVien INT,
    idKhoaHoc INT,
    ngayCap DATE,
    xepLoai NVARCHAR(50),
    FOREIGN KEY (idHocVien) REFERENCES HocVien(idHocVien),
    FOREIGN KEY (idKhoaHoc) REFERENCES KhoaHoc(idKhoaHoc)
)

select * from ChungChi

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


INSERT INTO TaiKhoan (tenDangNhap, matKhau, loaiNguoiDung)
VALUES ('admin', '123', 'ADMIN')

INSERT INTO TaiKhoan (tenDangNhap, matKhau, loaiNguoiDung)
VALUES ('gv01', '123', 'GIANGVIEN')

INSERT INTO GiangVien (tenGiangVien, gioiTinh, trinhDo, chuyenMon, email, soDienThoai, idTaiKhoan)
VALUES (N'Nguyễn Văn A', 1, N'Thạc sĩ', N'Tiếng Anh', 'gv@gmail.com', '0911111111', 2)
select * from GiangVien
select * from TaiKhoan

INSERT INTO TaiKhoan (tenDangNhap, matKhau, loaiNguoiDung)
VALUES ('hv01', '123', 'HOCVIEN')

INSERT INTO HocVien (tenHocVien, ngaySinh, gioiTinh, email, soDienThoai, diaChi, trangThai, idTaiKhoan)
VALUES (N'Trần Văn B', '2002-01-01', 1, 'hv@gmail.com', '0922222222', N'Hà Nội', N'DANG_HOC', 3)


select * from KetQuaThi
SELECT * FROM HocVien WHERE idHocVien = 7

--Đăng nhập
CREATE PROC sp_TaiKhoan_DangNhap
@tenDangNhap NVARCHAR(100),
@matKhau NVARCHAR(200)
AS
BEGIN
    SELECT 
        idTaiKhoan,
        tenDangNhap,
        loaiNguoiDung,
        trangThai
    FROM TaiKhoan
    WHERE tenDangNhap = @tenDangNhap
      AND matKhau = @matKhau
      AND trangThai = 1
END
GO

--Tài khoản
CREATE PROC sp_TaiKhoan_Them
@tenDangNhap NVARCHAR(100),
@matKhau NVARCHAR(200),
@loaiNguoiDung NVARCHAR(20)
AS
BEGIN
    INSERT INTO TaiKhoan(tenDangNhap, matKhau, loaiNguoiDung, trangThai)
    VALUES (@tenDangNhap, @matKhau, @loaiNguoiDung, 1)
END


CREATE PROC sp_TaiKhoan_DoiMatKhau
@idTaiKhoan INT,
@matKhauMoi NVARCHAR(200)
AS
BEGIN
    UPDATE TaiKhoan
    SET matKhau = @matKhauMoi
    WHERE idTaiKhoan = @idTaiKhoan
END


CREATE PROC sp_TaiKhoan_CapNhatTrangThai
@idTaiKhoan INT,
@trangThai BIT
AS
BEGIN
    UPDATE TaiKhoan
    SET trangThai = @trangThai
    WHERE idTaiKhoan = @idTaiKhoan
END


--Học viên tự đăng kí tài khoản
CREATE PROC sp_HocVien_DangKy
    @tenDangNhap NVARCHAR(100),
    @matKhau NVARCHAR(200),
    @tenHocVien NVARCHAR(100),
    @ngaySinh DATE,
    @gioiTinh BIT,
    @email NVARCHAR(100),
    @soDienThoai NVARCHAR(50),
    @diaChi NVARCHAR(200)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRAN;

    BEGIN TRY
        INSERT INTO TaiKhoan
        (tenDangNhap, matKhau, loaiNguoiDung, trangThai)
        VALUES
        (@tenDangNhap, @matKhau, 'HOCVIEN', 1);

        DECLARE @idTaiKhoan INT;
        SET @idTaiKhoan = SCOPE_IDENTITY();

        INSERT INTO HocVien
        (tenHocVien, ngaySinh, gioiTinh, email, soDienThoai, diaChi, trangThai, idTaiKhoan)
        VALUES
        (@tenHocVien, @ngaySinh, @gioiTinh, @email,
         @soDienThoai, @diaChi, N'DANG_HOC', @idTaiKhoan);

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN;

        DECLARE @ErrorMessage NVARCHAR(4000),
                @ErrorSeverity INT,
                @ErrorState INT;

        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END
GO



CREATE PROC sp_GiangVien_DangKy
    @tenDangNhap NVARCHAR(100),
    @matKhau NVARCHAR(200),
    @tenGiangVien NVARCHAR(100),
    @gioiTinh BIT,
    @trinhDo NVARCHAR(100),
    @chuyenMon NVARCHAR(200),
    @email NVARCHAR(100),
    @soDienThoai NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRAN;

    BEGIN TRY
        -- TẠO TÀI KHOẢN
        INSERT INTO TaiKhoan
        (tenDangNhap, matKhau, loaiNguoiDung, trangThai)
        VALUES
        (@tenDangNhap, @matKhau, 'GIANGVIEN', 1);

        DECLARE @idTaiKhoan INT = SCOPE_IDENTITY();

        -- TẠO GIẢNG VIÊN
        INSERT INTO GiangVien
        (tenGiangVien, gioiTinh, trinhDo, chuyenMon, email, soDienThoai, idTaiKhoan)
        VALUES
        (@tenGiangVien, @gioiTinh, @trinhDo, @chuyenMon, @email, @soDienThoai, @idTaiKhoan);

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN;
        THROW;
    END CATCH
END
GO


select * from TaiKhoan
select * from DangKyHoc

--HocVien
CREATE PROCEDURE sp_HocVien_XemTatCa
AS
BEGIN
    SELECT *
    FROM HocVien
END
GO



CREATE PROCEDURE sp_HocVien_LayTheoId
    @idHocVien INT
AS
BEGIN
    SELECT *
    FROM HocVien
    WHERE idHocVien = @idHocVien
END
GO



CREATE PROCEDURE sp_HocVien_Them
    @tenHocVien NVARCHAR(100),
    @ngaySinh DATE,
    @gioiTinh BIT,
    @email NVARCHAR(100),
    @soDienThoai NVARCHAR(50),
    @diaChi NVARCHAR(200),
    @trangThai NVARCHAR(50),
    @idTaiKhoan INT
AS
BEGIN
    INSERT INTO HocVien
    (tenHocVien, ngaySinh, gioiTinh, email, soDienThoai, diaChi, trangThai, idTaiKhoan)
    VALUES
    (@tenHocVien, @ngaySinh, @gioiTinh, @email,
     @soDienThoai, @diaChi, @trangThai, @idTaiKhoan)
END
GO



CREATE PROCEDURE sp_HocVien_Sua
    @idHocVien INT,
    @tenHocVien NVARCHAR(100),
    @ngaySinh DATE,
    @gioiTinh BIT,
    @email NVARCHAR(100),
    @soDienThoai NVARCHAR(50),
    @diaChi NVARCHAR(200),
    @trangThai NVARCHAR(50)
AS
BEGIN
    UPDATE HocVien
    SET
        tenHocVien = @tenHocVien,
        ngaySinh   = @ngaySinh,
        gioiTinh   = @gioiTinh,
        email      = @email,
        soDienThoai= @soDienThoai,
        diaChi     = @diaChi,
        trangThai  = @trangThai
    WHERE idHocVien = @idHocVien
END
GO


CREATE PROCEDURE sp_HocVien_Xoa
    @idHocVien INT
AS
BEGIN
    DELETE FROM HocVien
    WHERE idHocVien = @idHocVien
END
GO

CREATE PROCEDURE sp_HocVien_TimKiem
    @tuKhoa NVARCHAR(100)
AS
BEGIN
    SELECT *
    FROM HocVien
    WHERE
        tenHocVien LIKE N'%' + @tuKhoa + N'%'
        OR email LIKE N'%' + @tuKhoa + N'%'
        OR soDienThoai LIKE N'%' + @tuKhoa + N'%'
END
GO



--Khóa học
CREATE PROCEDURE sp_KhoaHoc_Them
    @tenKhoaHoc NVARCHAR(200),
    @level NVARCHAR(50),
    @soBuoiHoc INT,
    @moTa NVARCHAR(500)
AS
BEGIN
    INSERT INTO KhoaHoc(tenKhoaHoc, level, soBuoiHoc, moTa)
    VALUES (@tenKhoaHoc, @level, @soBuoiHoc, @moTa)
END




CREATE PROCEDURE sp_KhoaHoc_Sua
    @idKhoaHoc INT,
    @tenKhoaHoc NVARCHAR(200),
    @level NVARCHAR(50),
    @soBuoiHoc INT,
    @moTa NVARCHAR(500)
AS
BEGIN
    UPDATE KhoaHoc
    SET
        tenKhoaHoc = @tenKhoaHoc,
        level = @level,
        soBuoiHoc = @soBuoiHoc,
        moTa = @moTa
    WHERE idKhoaHoc = @idKhoaHoc
END


CREATE PROCEDURE sp_KhoaHoc_Xoa
    @idKhoaHoc INT
AS
BEGIN
    DELETE FROM KhoaHoc
    WHERE idKhoaHoc = @idKhoaHoc
END


ALTER PROCEDURE sp_KhoaHoc_GetAll
AS
BEGIN
    SELECT
        idKhoaHoc,
        tenKhoaHoc,
        level,
        soBuoiHoc,
        moTa,
        loaiTieng
    FROM KhoaHoc
END





ALTER PROCEDURE sp_KhoaHoc_GetById
    @idKhoaHoc INT
AS
BEGIN
    SELECT
        idKhoaHoc,
        tenKhoaHoc,
        level,
        soBuoiHoc,
        moTa,
        loaiTieng
    FROM KhoaHoc
    WHERE idKhoaHoc = @idKhoaHoc
END



CREATE PROCEDURE sp_KhoaHoc_TimKiem
    @tuKhoa NVARCHAR(200)
AS
BEGIN
    SELECT *
    FROM KhoaHoc
    WHERE tenKhoaHoc LIKE N'%' + @tuKhoa + '%'
       OR level LIKE N'%' + @tuKhoa + '%'
END


CREATE PROCEDURE sp_KhoaHoc_GetByLoaiTieng
    @loaiTieng NVARCHAR(50)
AS
BEGIN
    SELECT
        idKhoaHoc,
        tenKhoaHoc,
        level,
        soBuoiHoc,
        moTa,
        loaiTieng
    FROM KhoaHoc
    WHERE loaiTieng = @loaiTieng
END


--Giảng Viên
CREATE PROCEDURE sp_GiangVien_Them
    @tenGiangVien NVARCHAR(100),
    @gioiTinh BIT,
    @trinhDo NVARCHAR(100),
    @chuyenMon NVARCHAR(200),
    @email NVARCHAR(100),
    @soDienThoai NVARCHAR(50)
AS
INSERT INTO GiangVien
VALUES (@tenGiangVien, @gioiTinh, @trinhDo, @chuyenMon, @email, @soDienThoai, NULL)



CREATE PROCEDURE sp_GiangVien_Sua
    @idGiangVien INT,
    @tenGiangVien NVARCHAR(100),
    @gioiTinh BIT,
    @trinhDo NVARCHAR(100),
    @chuyenMon NVARCHAR(200),
    @email NVARCHAR(100),
    @soDienThoai NVARCHAR(50)
AS
UPDATE GiangVien
SET tenGiangVien=@tenGiangVien,
    gioiTinh=@gioiTinh,
    trinhDo=@trinhDo,
    chuyenMon=@chuyenMon,
    email=@email,
    soDienThoai=@soDienThoai
WHERE idGiangVien=@idGiangVien



CREATE PROCEDURE sp_GiangVien_Xoa
    @idGiangVien INT
AS
DELETE FROM GiangVien WHERE idGiangVien=@idGiangVien



CREATE PROCEDURE sp_GiangVien_GetAll
AS
SELECT * FROM GiangVien


CREATE PROCEDURE sp_GiangVien_GetById
    @idGiangVien INT
AS
SELECT * FROM GiangVien
WHERE idGiangVien = @idGiangVien



CREATE PROCEDURE sp_GiangVien_TimKiem
    @tuKhoa NVARCHAR(200)
AS
SELECT *
FROM GiangVien
WHERE tenGiangVien LIKE '%' + @tuKhoa + '%'
   OR chuyenMon LIKE '%' + @tuKhoa + '%'


--Lớp học
CREATE PROC sp_LopHoc_Them
    @idKhoaHoc INT,
    @tenLopHoc NVARCHAR(200),
    @idGiangVien INT,
    @hocPhi DECIMAL(18,2),
    @ngayKhaiGiang DATE,
    @ngayKetThuc DATE
AS
BEGIN
    INSERT INTO LopHoc
    (idKhoaHoc, tenLopHoc, idGiangVien, hocPhi, ngayKhaiGiang, ngayKetThuc)
    VALUES
    (@idKhoaHoc, @tenLopHoc, @idGiangVien, @hocPhi, @ngayKhaiGiang, @ngayKetThuc)
END


CREATE PROC sp_LopHoc_Sua
    @idLopHoc INT,
    @idKhoaHoc INT,
    @tenLopHoc NVARCHAR(200),
    @idGiangVien INT,
    @hocPhi DECIMAL(18,2),
    @ngayKhaiGiang DATE,
    @ngayKetThuc DATE
AS
BEGIN
    UPDATE LopHoc
    SET
        idKhoaHoc = @idKhoaHoc,
        tenLopHoc = @tenLopHoc,
        idGiangVien = @idGiangVien,
        hocPhi = @hocPhi,
        ngayKhaiGiang = @ngayKhaiGiang,
        ngayKetThuc = @ngayKetThuc
    WHERE idLopHoc = @idLopHoc
END




CREATE PROC sp_LopHoc_Xoa
    @idLopHoc INT
AS
BEGIN
    DELETE FROM LopHoc WHERE idLopHoc = @idLopHoc
END


CREATE PROC sp_LopHoc_GetAll
AS
BEGIN
    SELECT * FROM LopHoc
END

--Lấy cả tên khoahoc vag tên giangvien
CREATE PROCEDURE sp_LopHoc_GetAllTen
AS
BEGIN
    SELECT 
        lh.idLopHoc,
        lh.tenLopHoc,
        kh.tenKhoaHoc,
        gv.tenGiangVien,
        lh.hocPhi,
        lh.ngayKhaiGiang,
        lh.ngayKetThuc
    FROM LopHoc lh
    JOIN KhoaHoc kh ON lh.idKhoaHoc = kh.idKhoaHoc
    JOIN GiangVien gv ON lh.idGiangVien = gv.idGiangVien
END



CREATE PROC sp_LopHoc_GetById
    @idLopHoc INT
AS
BEGIN
    SELECT * FROM LopHoc WHERE idLopHoc = @idLopHoc
END

CREATE PROCEDURE sp_LopHoc_GetByHocVien
    @idHocVien INT
AS
BEGIN
    SELECT 
        lh.idLopHoc,
        lh.tenLopHoc,
        kh.tenKhoaHoc,
		gv.tenGiangVien,
        lh.ngayKhaiGiang,
        lh.ngayKetThuc
    FROM DangKyHoc dk
    JOIN LopHoc lh ON dk.idLopHoc = lh.idLopHoc
    JOIN KhoaHoc kh ON lh.idKhoaHoc = kh.idKhoaHoc
	JOIN GiangVien gv ON lh.idGiangVien = gv.idGiangVien
    WHERE dk.idHocVien = @idHocVien
END

CREATE PROCEDURE sp_LopHoc_GetByKhoaHoc
    @idKhoaHoc INT
AS
BEGIN
    SELECT 
        lh.idLopHoc,
        lh.tenLopHoc,
        kh.tenKhoaHoc,
        gv.tenGiangVien,
        lh.hocPhi,
        lh.ngayKhaiGiang,
        lh.ngayKetThuc
    FROM LopHoc lh
    JOIN KhoaHoc kh ON lh.idKhoaHoc = kh.idKhoaHoc
    JOIN GiangVien gv ON lh.idGiangVien = gv.idGiangVien
    WHERE lh.idKhoaHoc = @idKhoaHoc
END

CREATE PROCEDURE sp_LopHoc_GetByGiangVien
    @idGiangVien INT
AS
BEGIN
    SELECT 
        lh.idLopHoc,
        lh.tenLopHoc,
        kh.tenKhoaHoc,
        lh.ngayKhaiGiang,
        lh.ngayKetThuc
    FROM LopHoc lh
    INNER JOIN KhoaHoc kh ON lh.idKhoaHoc = kh.idKhoaHoc
    WHERE lh.idGiangVien = @idGiangVien
END
GO

CREATE PROCEDURE sp_HocVien_GetByLopHoc
    @idLopHoc INT
AS
BEGIN
    SELECT
        hv.idHocVien,
        hv.tenHocVien,
        hv.ngaySinh,
        hv.gioiTinh,
        hv.email,
        hv.soDienThoai,
        hv.diaChi
    FROM DangKyHoc dk
    INNER JOIN HocVien hv ON dk.idHocVien = hv.idHocVien
    WHERE dk.idLopHoc = @idLopHoc
END
GO

CREATE PROC sp_LopHoc_TimKiem
    @tuKhoa NVARCHAR(200)
AS
BEGIN
    SELECT *
    FROM LopHoc
    WHERE tenLopHoc LIKE '%' + @tuKhoa + '%'
END


--Đăng kí khóa học, ghi danh học viên
CREATE PROC sp_DangKyHoc_Them
    @idHocVien INT,
    @idLopHoc INT,
    @trangThai NVARCHAR(50)
AS
BEGIN
    IF EXISTS (
        SELECT 1 FROM DangKyHoc
        WHERE idHocVien = @idHocVien AND idLopHoc = @idLopHoc
    )
        RETURN

    INSERT INTO DangKyHoc (idHocVien, idLopHoc, trangThai)
    VALUES (@idHocVien, @idLopHoc, @trangThai)
END



CREATE PROC sp_DangKyHoc_Xoa
    @idDangKy INT
AS
BEGIN
    DELETE FROM DangKyHoc WHERE idDangKy = @idDangKy
END



CREATE PROC sp_DangKyHoc_GetAll
AS
BEGIN
    SELECT * FROM DangKyHoc
END



CREATE PROC sp_DangKyHoc_GetById
    @idDangKy INT
AS
BEGIN
    SELECT * FROM DangKyHoc WHERE idDangKy = @idDangKy
END



CREATE PROC sp_DangKyHoc_GetByLopHoc
    @idLopHoc INT
AS
BEGIN
    SELECT hv.*
    FROM DangKyHoc dk
    JOIN HocVien hv ON dk.idHocVien = hv.idHocVien
    WHERE dk.idLopHoc = @idLopHoc
END



CREATE PROC sp_DangKyHoc_GetByHocVien
    @idHocVien INT
AS
BEGIN
    SELECT lh.*
    FROM DangKyHoc dk
    JOIN LopHoc lh ON dk.idLopHoc = lh.idLopHoc
    WHERE dk.idHocVien = @idHocVien
END







--Xếp lịch học
CREATE PROC sp_LichHoc_Them
    @idLopHoc INT,
    @ngayHoc DATE,
    @caHoc NVARCHAR(50),
    @phongHoc NVARCHAR(50),
    @ghiChu NVARCHAR(200)
AS
BEGIN
    INSERT INTO LichHoc
    (idLopHoc, ngayHoc, CaHoc, PhongHoc, ghiChu)
    VALUES
    (@idLopHoc, @ngayHoc, @caHoc, @phongHoc, @ghiChu)
END


CREATE PROC sp_LichHoc_Sua
    @idLichHoc INT,
    @ngayHoc DATE,
    @caHoc NVARCHAR(50),
    @phongHoc NVARCHAR(50),
    @ghiChu NVARCHAR(200)
AS
BEGIN
    UPDATE LichHoc
    SET
        ngayHoc = @ngayHoc,
        CaHoc = @caHoc,
        PhongHoc = @phongHoc,
        ghiChu = @ghiChu
    WHERE idLichHoc = @idLichHoc
END



CREATE PROC sp_LichHoc_Xoa
    @idLichHoc INT
AS
BEGIN
    DELETE FROM LichHoc WHERE idLichHoc = @idLichHoc
END


CREATE PROC sp_LichHoc_GetAll
AS
BEGIN
    SELECT * FROM LichHoc
END




CREATE PROC sp_LichHoc_GetById
    @idLichHoc INT
AS
BEGIN
    SELECT * FROM LichHoc WHERE idLichHoc = @idLichHoc
END

CREATE PROC sp_LichHoc_GetAllFull
AS
BEGIN
    SELECT
        lh.idLichHoc,
        lh.ngayHoc,
        lh.CaHoc,
        lh.PhongHoc,
        lh.ghiChu,

        l.idLopHoc,
        l.tenLopHoc,

        k.tenKhoaHoc,
        gv.tenGiangVien
    FROM LichHoc lh
    JOIN LopHoc l ON lh.idLopHoc = l.idLopHoc
    JOIN KhoaHoc k ON l.idKhoaHoc = k.idKhoaHoc
    LEFT JOIN GiangVien gv ON l.idGiangVien = gv.idGiangVien
    ORDER BY lh.ngayHoc, lh.CaHoc
END



CREATE PROC sp_LichHoc_GetByLopHoc
    @idLopHoc INT
AS
BEGIN
    SELECT *
    FROM LichHoc
    WHERE idLopHoc = @idLopHoc
    ORDER BY ngayHoc
END




--Kỳ Thi
CREATE PROC sp_KyThi_Them
@tenKyThi NVARCHAR(200),
@loaiKyThi NVARCHAR(50),
@idLopHoc INT,
@idKhoaHoc INT,
@ngayThi DATE,
@thangDiem INT
AS
INSERT INTO KyThi
VALUES (@tenKyThi,@loaiKyThi,@idLopHoc,@idKhoaHoc,@ngayThi,@thangDiem)



CREATE PROCEDURE sp_KyThi_GetByLopHoc
    @idLopHoc INT
AS
BEGIN
    SELECT 
        kt.idKyThi,
        kt.tenKyThi,
        kt.loaiKyThi,
        kt.ngayThi,
        kt.thangDiem,
        kt.idLopHoc,
        kt.idKhoaHoc,
        kt.IsMoNhapDiem
    FROM KyThi kt
    WHERE kt.idLopHoc = @idLopHoc
    ORDER BY kt.ngayThi DESC
END




CREATE PROC sp_KyThi_GetAll
AS
SELECT * FROM KyThi

CREATE PROC sp_KyThi_GetById
@idKyThi INT
AS
SELECT * FROM KyThi WHERE idKyThi = @idKyThi



CREATE PROCEDURE sp_KyThi_GetByHocVien
    @idHocVien INT
AS
BEGIN
    SELECT 
        kt.idKyThi,
        kt.tenKyThi,
        kt.loaiKyThi,
        kt.idLopHoc,
        kt.idKhoaHoc,
        kt.ngayThi,
        kt.thangDiem
    FROM KyThi kt
    JOIN DangKyHoc dk ON kt.idLopHoc = dk.idLopHoc
    WHERE dk.idHocVien = @idHocVien
END

ALTER PROCEDURE sp_KyThi_GetByHocVien
    @idHocVien INT
AS
BEGIN
    SELECT 
        kt.idKyThi,
        kt.tenKyThi,
        kt.loaiKyThi,
        kt.idLopHoc,
        kt.idKhoaHoc,
        kt.ngayThi,
        kt.thangDiem,

        kqt.diem,
        kqt.ketQua
    FROM KyThi kt
    JOIN DangKyHoc dk 
        ON kt.idLopHoc = dk.idLopHoc

    LEFT JOIN KetQuaThi kqt 
        ON kt.idKyThi = kqt.idKyThi
       AND kqt.idHocVien = @idHocVien

    WHERE dk.idHocVien = @idHocVien
END


CREATE PROC sp_KyThi_Sua
@idKyThi INT,
@tenKyThi NVARCHAR(200),
@loaiKyThi NVARCHAR(50),
@idLopHoc INT,
@idKhoaHoc INT,
@ngayThi DATE,
@thangDiem INT
AS
UPDATE KyThi
SET tenKyThi = @tenKyThi,
    loaiKyThi = @loaiKyThi,
    idLopHoc = @idLopHoc,
    idKhoaHoc = @idKhoaHoc,
    ngayThi = @ngayThi,
    thangDiem = @thangDiem
WHERE idKyThi = @idKyThi



CREATE PROC sp_KyThi_Xoa
@idKyThi INT
AS
DELETE FROM KyThi WHERE idKyThi = @idKyThi





--Kết quả thi
CREATE PROC sp_KetQuaThi_Them
@idKyThi INT,
@idHocVien INT,
@diem DECIMAL(4,2),
@ketQua NVARCHAR(50)
AS
INSERT INTO KetQuaThi VALUES (@idKyThi,@idHocVien,@diem,@ketQua)

CREATE PROC sp_KetQuaThi_Luu
    @idKyThi INT,
    @idHocVien INT,
    @diem DECIMAL(4,2),
    @ketQua NVARCHAR(50)
AS
BEGIN
    IF EXISTS (
        SELECT 1 FROM KetQuaThi
        WHERE idKyThi = @idKyThi
          AND idHocVien = @idHocVien
    )
    BEGIN
        UPDATE KetQuaThi
        SET diem = @diem,
            ketQua = @ketQua
        WHERE idKyThi = @idKyThi
          AND idHocVien = @idHocVien
    END
    ELSE
    BEGIN
        INSERT INTO KetQuaThi(idKyThi,idHocVien,diem,ketQua)
        VALUES (@idKyThi,@idHocVien,@diem,@ketQua)
    END
END


CREATE PROC sp_KetQuaThi_GetByKyThi
@idKyThi INT
AS
SELECT hv.tenHocVien, kq.*
FROM KetQuaThi kq
JOIN HocVien hv ON kq.idHocVien = hv.idHocVien
WHERE kq.idKyThi=@idKyThi


CREATE PROC sp_KetQuaThi_GetByLopHoc
    @idKyThi INT
AS
BEGIN
    SELECT
        hv.idHocVien        AS IdHocVien,
        hv.tenHocVien       AS TenHocVien,
        ISNULL(kq.diem, NULL)     AS Diem,
        ISNULL(kq.ketQua, '')     AS KetQua
    FROM KyThi kt
    JOIN DangKyHoc dk 
        ON kt.idLopHoc = dk.idLopHoc
    JOIN HocVien hv 
        ON dk.idHocVien = hv.idHocVien
    LEFT JOIN KetQuaThi kq 
        ON kq.idKyThi = kt.idKyThi
        AND kq.idHocVien = hv.idHocVien
    WHERE kt.idKyThi = @idKyThi
END




CREATE PROC sp_KetQuaThi_Sua
@idKyThi INT,
@idHocVien INT,
@diem DECIMAL(4,2),
@ketQua NVARCHAR(50)
AS
UPDATE KetQuaThi
SET diem = @diem,
    ketQua = @ketQua
WHERE idKyThi = @idKyThi AND idHocVien = @idHocVien


select * from HocVien
select * from KyThi
select * from LopHoc



--Hóa đơn
CREATE PROC sp_HoaDon_Them
@idHocVien INT,
@idLopHoc INT,
@soTien DECIMAL(18,2),
@trangThai NVARCHAR(50),
@ghiChu NVARCHAR(200)
AS
INSERT INTO HoaDon(idHocVien,idLopHoc,soTien,trangThai,ghiChu)
VALUES (@idHocVien,@idLopHoc,@soTien,@trangThai,@ghiChu)


CREATE PROC sp_HoaDon_Sua
@idHoaDon INT,
@soTien DECIMAL(18,2),
@trangThai NVARCHAR(50),
@ghiChu NVARCHAR(200)
AS
UPDATE HoaDon
SET soTien=@soTien,
    trangThai=@trangThai,
    ghiChu=@ghiChu
WHERE idHoaDon=@idHoaDon


CREATE PROC sp_HoaDon_Xoa
@idHoaDon INT
AS
DELETE FROM HoaDon WHERE idHoaDon=@idHoaDon




CREATE PROC sp_HoaDon_GetAll
AS
BEGIN
    SELECT 
        hd.idHoaDon,
        hd.idHocVien,
        hv.tenHocVien,
        hd.idLopHoc,
        lh.tenLopHoc,
        hd.soTien,
        hd.ngayLap,
        hd.trangThai,
        hd.ghiChu
    FROM HoaDon hd
    JOIN HocVien hv ON hd.idHocVien = hv.idHocVien
    JOIN LopHoc lh ON hd.idLopHoc = lh.idLopHoc
    ORDER BY hd.ngayLap DESC
END



CREATE PROC sp_HoaDon_GetById
@idHoaDon INT
AS
SELECT * FROM HoaDon WHERE idHoaDon=@idHoaDon


CREATE PROCEDURE sp_HoaDon_GetByHocVien
    @idHocVien INT
AS
BEGIN
    SELECT 
        hd.idHoaDon,
        hd.idHocVien,
        hd.idLopHoc,
        lh.tenLopHoc,
        hd.soTien,
        hd.ngayLap,
        hd.trangThai,
        hd.ghiChu
    FROM HoaDon hd
    JOIN LopHoc lh ON hd.idLopHoc = lh.idLopHoc
    WHERE hd.idHocVien = @idHocVien
    ORDER BY hd.ngayLap DESC
END
GO





--Chứng chỉ
CREATE PROC sp_ChungChi_Them
@idHocVien INT,
@idKhoaHoc INT,
@ngayCap DATE,
@xepLoai NVARCHAR(50)
AS
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM ChungChi
        WHERE idHocVien=@idHocVien AND idKhoaHoc=@idKhoaHoc
    )
    BEGIN
        INSERT INTO ChungChi(idHocVien,idKhoaHoc,ngayCap,xepLoai)
        VALUES (@idHocVien,@idKhoaHoc,@ngayCap,@xepLoai)
    END
END


CREATE PROC sp_ChungChi_GetAll
AS
SELECT cc.*, hv.tenHocVien, kh.tenKhoaHoc
FROM ChungChi cc
JOIN HocVien hv ON cc.idHocVien = hv.idHocVien
JOIN KhoaHoc kh ON cc.idKhoaHoc = kh.idKhoaHoc


CREATE PROC sp_ChungChi_GetByHocVien
@idHocVien INT
AS
SELECT cc.*, kh.tenKhoaHoc
FROM ChungChi cc
JOIN KhoaHoc kh ON cc.idKhoaHoc = kh.idKhoaHoc
WHERE cc.idHocVien=@idHocVien


CREATE PROC sp_ChungChi_GetById
@idChungChi INT
AS
SELECT * FROM ChungChi WHERE idChungChi=@idChungChi


CREATE PROC sp_CapChungChi_GetByKhoaHoc
    @idKhoaHoc INT
AS
BEGIN
    SELECT
        hv.idHocVien,
        hv.tenHocVien,
        kt.idKyThi,
        kq.diem,
        CASE
            WHEN kq.diem >= 8 THEN N'Giỏi'
            WHEN kq.diem >= 6.5 THEN N'Khá'
            WHEN kq.diem >= 5 THEN N'Trung bình'
            ELSE N'Không đạt'
        END AS xepLoai
    FROM HocVien hv
    JOIN DangKyHoc dk ON hv.idHocVien = dk.idHocVien
    JOIN LopHoc l ON dk.idLopHoc = l.idLopHoc
    JOIN KyThi kt ON kt.idLopHoc = l.idLopHoc
    JOIN KetQuaThi kq ON kq.idKyThi = kt.idKyThi
                     AND kq.idHocVien = hv.idHocVien
    WHERE l.idKhoaHoc = @idKhoaHoc
      AND kt.loaiKyThi = 'CUOI_KY'
END


--In chứng chỉ PDF
CREATE PROC sp_ChungChi_InPDF
@idChungChi INT
AS
SELECT 
    hv.tenHocVien,
    kh.tenKhoaHoc,
    cc.ngayCap,
    cc.xepLoai
FROM ChungChi cc
JOIN HocVien hv ON cc.idHocVien = hv.idHocVien
JOIN KhoaHoc kh ON cc.idKhoaHoc = kh.idKhoaHoc
WHERE cc.idChungChi = @idChungChi

CREATE PROC sp_ChungChi_Check
@idHocVien INT,
@idKhoaHoc INT
AS
BEGIN
    SELECT idChungChi
    FROM ChungChi
    WHERE idHocVien = @idHocVien
      AND idKhoaHoc = @idKhoaHoc
END


--Báo cáo
CREATE PROC sp_BaoCao_SoLopTheoKhoaHoc
AS
SELECT kh.tenKhoaHoc, COUNT(lh.idLopHoc) AS soLop
FROM KhoaHoc kh
LEFT JOIN LopHoc lh ON kh.idKhoaHoc = lh.idKhoaHoc
GROUP BY kh.tenKhoaHoc




CREATE PROC sp_BaoCao_HocVienTheoLop
@idLopHoc INT
AS
SELECT hv.idHocVien, hv.tenHocVien, hv.email, hv.soDienThoai
FROM DangKyHoc dk
JOIN HocVien hv ON dk.idHocVien = hv.idHocVien
WHERE dk.idLopHoc = @idLopHoc


CREATE PROC sp_BaoCao_KetQuaThi
@idKyThi INT
AS
SELECT hv.tenHocVien, kq.diem, kq.ketQua
FROM KetQuaThi kq
JOIN HocVien hv ON kq.idHocVien = hv.idHocVien
WHERE kq.idKyThi = @idKyThi



CREATE PROC sp_BaoCao_DoanhThu
@tuNgay DATE,
@denNgay DATE
AS
SELECT 
    COUNT(idHoaDon) AS soHoaDon,
    SUM(soTien) AS tongDoanhThu
FROM HoaDon
WHERE trangThai = N'DA_THANH_TOAN'
AND ngayLap BETWEEN @tuNgay AND @denNgay




