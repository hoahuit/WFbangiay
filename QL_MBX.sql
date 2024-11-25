CREATE DATABASE QL_MBXE
GO

USE QL_MBXE
GO
--Tao bang 
------------------------------------------------------------------------------------------------------------------
CREATE TABLE [TAIKHOAN]
(
[USENAME] VARCHAR(36) PRIMARY KEY,
[PASSWORD] VARCHAR (36),
[ROLE] VARCHAR (36)
)
GO
------------------------------------------------------------------------------------------------------------------
CREATE TABLE [XE]
(
[MAXE] CHAR(5) NOT NULL PRIMARY KEY,
[TENXE] NVARCHAR(50),
[HANGXE] VARCHAR(20),
[MAUXE] NVARCHAR(50),
[DUNGTICH] NVARCHAR(10),
[TINHTRANG] NVARCHAR(50),
[GIANHAP] INT,
[GIABAN] INT,
[MALOAIXE] CHAR(5),
[MANCC] CHAR(5),
)
GO
------------------------------------------------------------------------------------------------------------------
CREATE TABLE [LOAIXE]
(
MALOAIXE CHAR(5) NOT NULL PRIMARY KEY,
TENLOAIXE NVARCHAR(20)
)
GO
------------------------------------------------------------------------------------------------------------------
CREATE TABLE [NCC]
(
MANCC CHAR(5) NOT NULL PRIMARY KEY,
TENNCC NVARCHAR (50),
DIACHI NVARCHAR(100),
SDT CHAR(10),
EMAIL VARCHAR(50)
)
GO
------------------------------------------------------------------------------------------------------------------
CREATE TABLE [BAOHANH]
(
[MAPHIEUBH] CHAR(5) NOT NULL PRIMARY KEY,
[MAXE] CHAR(5),
[MANV] CHAR(5),
[MAKH] CHAR(5),
[THOIGIANBH] NVARCHAR(20),
[NGAYLAP] DATE,
)
GO
------------------------------------------------------------------------------------------------------------------
CREATE TABLE [CHITIETBH]
(
[MAPHIEUBH] CHAR(5),
[NGAYBHLAN1] DATE,
[NGAYBHLAN2] DATE,
[NGAYBHLAN3] DATE,
)
GO
------------------------------------------------------------------------------------------------------------------
CREATE TABLE [NHANVIEN]
(
[MANV] CHAR(5) NOT NULL PRIMARY KEY,
[TENNV] NVARCHAR(100),
[GIOITINH] NCHAR(5),
[NGAYSINH] DATE,
[DIACHI] NVARCHAR(100),
[SDT] CHAR(10),
[CHUCVU] NVARCHAR(50),
[SOCCCD] CHAR(12),
[NGAYVAOLAM] DATE,
[LUONG] MONEY
)
GO
------------------------------------------------------------------------------------------------------------------
CREATE TABLE [KHACHHANG] 
(
[MAKH] CHAR(5) NOT NULL PRIMARY KEY,
[TENKH] NVARCHAR(100),
[GIOITINH] NCHAR(5),
[DIACHI] NVARCHAR(100),
[SDT] CHAR(10),
)
GO
------------------------------------------------------------------------------------------------------------------
CREATE TABLE [HDXUAT] 
(
[MAHDX] CHAR(5) NOT NULL PRIMARY KEY,
[MANV] CHAR(5),
[MAKH] CHAR(5),
[NGAYXUAT] DATE
)
GO
------------------------------------------------------------------------------------------------------------------
CREATE TABLE [CHITIETHDX]
(
[MAHDX] CHAR(5),
[MAXE] CHAR(5),
[SOLUONG] INT,
[DONGIA] INT,
[TONGTIEN] INT,
PRIMARY KEY ([MAHDX] , [MAXE])
)
GO
------------------------------------------------------------------------------------------------------------------
CREATE TABLE [HDNHAP]
(
[MAHDN] CHAR(5) NOT NULL PRIMARY KEY,
[MANV] CHAR(5),
[MANCC] CHAR(5),
[NGAYNHAP] DATE
)
GO
------------------------------------------------------------------------------------------------------------------
CREATE TABLE [CHITIETHDN]
(
[MAHDN] CHAR(5),
[MAXE] CHAR(5),
[SOLUONG] INT, 
[DONGIA] INT,
[TONGTIEN] INT
PRIMARY KEY ( [MAHDN] , [MAXE] )
)
GO
------------------------------------------------------------------------------------------------------------------
-- KHOA NGOAI
ALTER TABLE [XE] ADD FOREIGN KEY ([MANCC]) REFERENCES [NCC] ([MANCC])
GO 

ALTER TABLE [XE] ADD FOREIGN KEY ([MALOAIXE]) REFERENCES [LOAIXE] ([MALOAIXE])
GO 
------------------------------------------------------------------------------------------------------------------
ALTER TABLE [BAOHANH] ADD FOREIGN KEY ([MAXE]) REFERENCES [XE] ([MAXE])
GO 

ALTER TABLE [BAOHANH] ADD FOREIGN KEY ([MANV]) REFERENCES [NHANVIEN] ([MANV])
GO

ALTER TABLE [BAOHANH] ADD FOREIGN KEY ([MAKH]) REFERENCES [KHACHHANG] ([MAKH])
GO
------------------------------------------------------------------------------------------------------------------
ALTER TABLE [CHITIETBH] ADD FOREIGN KEY ([MAPHIEUBH]) REFERENCES [BAOHANH] ([MAPHIEUBH])
GO
------------------------------------------------------------------------------------------------------------------
ALTER TABLE [HDXUAT] ADD FOREIGN KEY ([MANV]) REFERENCES [NHANVIEN] ([MANV])
GO

ALTER TABLE [HDXUAT] ADD FOREIGN KEY ([MAKH]) REFERENCES [KHACHHANG] ([MAKH])
GO
------------------------------------------------------------------------------------------------------------------
ALTER TABLE [CHITIETHDX] ADD FOREIGN KEY ([MAHDX]) REFERENCES [HDXUAT] ([MAHDX])
GO
ALTER TABLE [CHITIETHDX] ADD FOREIGN KEY ([MAXE]) REFERENCES [XE] ([MAXE])
GO
------------------------------------------------------------------------------------------------------------------
ALTER TABLE [HDNHAP] ADD FOREIGN KEY ([MANV]) REFERENCES [NHANVIEN] ([MANV])
GO 

ALTER TABLE [HDNHAP] ADD FOREIGN KEY ([MANCC]) REFERENCES [NCC] ([MANCC])
GO 
------------------------------------------------------------------------------------------------------------------
ALTER TABLE [CHITIETHDN] ADD FOREIGN KEY ([MAHDN]) REFERENCES [HDNHAP] ([MAHDN])
GO

ALTER TABLE [CHITIETHDN] ADD FOREIGN KEY ([MAXE]) REFERENCES [XE] ([MAXE])
GO
------------------------------------------------------------------------------------------------------------------
--Nhập liệu 
------------------------------------------------------------------------------------------------------------------

INSERT INTO [NCC] VALUES ('NCC01','HONDA Hưng Phát',N'145,Nguyễn Thái Bình,Q1,TP.HCM','0934567891','thaibinh01@gmail.com')
INSERT INTO [NCC] VALUES ('NCC02','YAMAHA Hoàng Hà',N'277, Nguyễn Văn Cừ,Q5,TP.HCM','0987654321','hoangha02@gmail.com')
INSERT INTO [NCC] VALUES ('NCC03','SUZUKI Hòa Bình',N'64,Lê Trọng Tấn,Q.Tân Phú,TP.HCM','0123456789','honda01@gmail.com')
GO
select * from NCC
------------------------------------------------------------------------------------------------------------------
INSERT INTO [LOAIXE] VALUES('LX001',N'Xe Số')
INSERT INTO [LOAIXE] VALUES('LX002',N'Xe Tay Ga')
INSERT INTO [LOAIXE] VALUES('LX003',N'Xe Côn Tay')
GO
select * from LOAIXE
------------------------------------------------------------------------------------------------------------------
--Hãng hondda
INSERT INTO [XE] VALUES ('A0001','Wave Alpha 110','HONDA',N'Đỏ','110cc',N'Còn',15000000,18000000,'LX001','NCC01')
INSERT INTO [XE] VALUES ('A0002','Wave Alpha 110','HONDA',N'Đen','110cc',N'Còn',15000000,18000000,'LX001','NCC01')
INSERT INTO [XE] VALUES ('A0003','Wave Alpha 110','HONDA',N'Xanh','110cc',N'Còn',15000000,18000000,'LX001','NCC01')
INSERT INTO [XE] VALUES ('A0004','Wave Alpha 110','HONDA',N'Trắng','110cc',N'Còn',15000000,18000000,'LX001','NCC01')
	
INSERT INTO [XE] VALUES ('A0005','Wave RSX','HONDA',N'Đen-Đỏ','110cc',N'Còn',18000000,22000000,'LX001','NCC01')
INSERT INTO [XE] VALUES ('A0006','Wave RSX','HONDA',N'Đen-Xanh','110cc',N'Còn',18000000,22000000,'LX001','NCC01')

INSERT INTO [XE] VALUES ('A0007','Future','HONDA',N'Đỏ-Đen','125cc',N'Còn',27000000,31700000,'LX001','NCC01')
INSERT INTO [XE] VALUES ('A0008','Future','HONDA',N'Xanh-Đen','125cc',N'Còn',27000000,31500000,'LX001','NCC01')
INSERT INTO [XE] VALUES ('A0009','Future','HONDA',N'Trắng-Đen','125cc',N'Còn',27000000,31500000,'LX001','NCC01')

INSERT INTO [XE] VALUES ('A0010','Vision','HONDA',N'Đỏ','110cc',N'Còn',27500000,32000000,'LX002','NCC01')
INSERT INTO [XE] VALUES ('A0011','Vision','HONDA',N'Trắng','110cc',N'Còn',27500000,32000000,'LX002','NCC01')

INSERT INTO [XE] VALUES ('A0012','Vario','HONDA',N'Đen','150cc',N'Còn',40000000,50000000,'LX002','NCC01')
INSERT INTO [XE] VALUES ('A0013','Vario','HONDA',N'Đỏ','150cc',N'Còn',40000000,50000000,'LX002','NCC01')
INSERT INTO [XE] VALUES ('A0014','Vario','HONDA',N'Xanh','150cc',N'Còn',40000000,50000000,'LX002','NCC01')

INSERT INTO [XE] VALUES ('A0015','SH','HONDA',N'Đen','150cc',N'Còn',85000000,100000000,'LX002','NCC01')
INSERT INTO [XE] VALUES ('A0016','SH','HONDA',N'Đỏ','150cc',N'Còn',85000000,100200000,'LX002','NCC01')
INSERT INTO [XE] VALUES ('A0017','SH','HONDA',N'Trắng','150cc',N'Còn',85000000,100000000,'LX002','NCC01')

INSERT INTO [XE] VALUES ('A0018','Winner X','HONDA',N'Đen','150cc',N'Còn',42000000,50000000,'LX003','NCC01')
INSERT INTO [XE] VALUES ('A0019','Winner X','HONDA',N'Đỏ-Đen','150cc',N'Còn',42000000,10000000,'LX003','NCC01')
INSERT INTO [XE] VALUES ('A0020','Winner X','HONDA',N'Bạc-Đen','150cc',N'Còn',42000000,50000000,'LX003','NCC01')

--Hãng Yamaha
INSERT INTO [XE] VALUES ('B0001','Sirius','YAMAHA',N'Đen','110cc',N'Còn',17000000,21000000,'LX001','NCC02')
INSERT INTO [XE] VALUES ('B0002','Sirius','YAMAHA',N'Xám-Xanh','110cc',N'Còn',17000000,22000000,'LX001','NCC02')
INSERT INTO [XE] VALUES ('B0003','Sirius','YAMAHA',N'Xanh-Đen','110cc',N'Còn',17000000,22000000,'LX001','NCC02')
INSERT INTO [XE] VALUES ('B0004','Sirius','YAMAHA',N'Đỏ-Đen','110cc',N'Còn',17000000,22000000,'LX001','NCC02')

INSERT INTO [XE] VALUES ('B0005','Jupiter','YAMAHA',N'Đen','114cc',N'Còn',25000000,30000000,'LX001','NCC02')
INSERT INTO [XE] VALUES ('B0006','Jupiter','YAMAHA',N'Xám','114cc',N'Còn',25000000,30000000,'LX001','NCC02')
INSERT INTO [XE] VALUES ('B0007','Jupiter','YAMAHA',N'Đỏ','114cc',N'Còn',25000000,30000000,'LX001','NCC02')

INSERT INTO [XE] VALUES ('B0008','Janus','YAMAHA',N'Đen','125cc',N'Còn',24000000,28500000,'LX002','NCC02')
INSERT INTO [XE] VALUES ('B0009','Janus','YAMAHA',N'Đỏ-Đen','125cc',N'Còn',24000000,28500000,'LX002','NCC02')
INSERT INTO [XE] VALUES ('B0010','Janus','YAMAHA',N'Trắng-Xám','125cc',N'Còn',24000000,28500000,'LX002','NCC02')

INSERT INTO [XE] VALUES ('B0011','Freego','YAMAHA',N'Xám-Đen','125cc',N'Còn',30000000,34500000,'LX002','NCC02')
INSERT INTO [XE] VALUES ('B0012','Freego','YAMAHA',N'Đen-Đỏ','125cc',N'Còn',30000000,34500000,'LX002','NCC02')
INSERT INTO [XE] VALUES ('B0013','Freego','YAMAHA',N'Xanh-Đen','125cc',N'Còn',30000000,34500000,'LX002','NCC02')

INSERT INTO [XE] VALUES ('B0014','Grande','YAMAHA',N'Hồng-Đen','125cc',N'Còn',46000000,52000000,'LX002','NCC02')
INSERT INTO [XE] VALUES ('B0015','Grande','YAMAHA',N'Đen','125cc',N'Còn',46000000,52000000,'LX002','NCC02')
INSERT INTO [XE] VALUES ('B0016','Grande','YAMAHA',N'Xám-Đen','125cc',N'Còn',46000000,52000000,'LX002','NCC02')

INSERT INTO [XE] VALUES ('B0017','Exciter','YAMAHA',N'Đen','125cc',N'Còn',48000000,54000000,'LX003','NCC02')
INSERT INTO [XE] VALUES ('B0018','Exciter','YAMAHA',N'Xanh','125cc',N'Còn',48000000,54000000,'LX003','NCC02')
INSERT INTO [XE] VALUES ('B0019','Exciter','YAMAHA',N'Xám-Xanh','125cc',N'Còn',48000000,54000000,'LX003','NCC02')
INSERT INTO [XE] VALUES ('B0020','Exciter','YAMAHA',N'Trắng-Đen','125cc',N'Còn',48000000,54000000,'LX003','NCC02')

-- Hãng Suzuki
INSERT INTO [XE] VALUES ('C0001','Burgman Street 125','SUZUKI',N'Xám Mờ','125cc',N'Còn',43000000,48600000,'LX002','NCC03')
INSERT INTO [XE] VALUES ('C0002','Burgman Street 125','SUZUKI',N'Trắng-Vàng Đồng','125cc',N'Còn',43000000,48600000,'LX002','NCC03')
INSERT INTO [XE] VALUES ('C0003','Burgman Street 125','SUZUKI',N'Đen- Vàng Đồng','125cc',N'Còn',43000000,48600000,'LX002','NCC03')

INSERT INTO [XE] VALUES ('C0004','Raider','SUZUKI',N'Đỏ-Đen','150cc',N'Còn',43000000,51100000,'LX003','NCC03')
INSERT INTO [XE] VALUES ('C0005','Raider','SUZUKI',N'Xanh-Đen','150cc',N'Còn',43000000,51100000,'LX003','NCC03')
INSERT INTO [XE] VALUES ('C0006','Raider','SUZUKI',N'Xám-Đen','150cc',N'Còn',43000000,51100000,'LX003','NCC03')

INSERT INTO [XE] VALUES ('C0007','V-Strom 250SX','SUZUKI',N'Vàng-Đen','249cc',N'Còn',43000000,133000000,'LX003','NCC03')
INSERT INTO [XE] VALUES ('C0008','V-Strom 250SX','SUZUKI',N'Cam-Đen','249cc',N'Còn',43000000,133000000,'LX003','NCC03')
INSERT INTO [XE] VALUES ('C0009','V-Strom 250SX','SUZUKI',N'Đen','249cc',N'Còn',43000000,133000000,'LX003','NCC03')
select * from XE 

----------------------------------------------------------------------
INSERT INTO [NHANVIEN]  VALUES('NV001', N'Nguyễn Văn An', N'Nam', '1985-05-15', N'Vĩnh Long', '0123456789', N'Nhân viên', '123456789012', '2020-01-10', 5000000)
INSERT INTO [NHANVIEN]  VALUES('NV002', N'Trần Thị Nguyệt', N'Nữ', '1990-08-22', N'Cần Thơ', '0987654321', N'Quản lý', '109876543210', '2019-03-15', 8000000)
INSERT INTO [NHANVIEN]  VALUES('NV003', N'Lê Văn Quân', N'Nam', '1995-12-05', N'Bến Tre', '0123987654', N'Kỹ Thuật', '123456789098', '2021-06-20', 6000000)
INSERT INTO [NHANVIEN]  VALUES('NV004', N'Phạm Thị Nguyên', N'Nữ', '1988-02-10', N'Đồng Tháp', '0246802468', N'Nhân Viên', '456789123456', '2022-07-15', 5000000)
INSERT INTO [NHANVIEN]  VALUES('NV005', N'Trương Minh Mẫn', N'Nam', '1992-04-25', N'Cà Mau', '0345678901', N'Nhân viên', '789012345678', '2021-05-05', 5000000)
INSERT INTO [NHANVIEN]  VALUES('NV006', N'Nguyễn Thị Mỹ Duyên', N'Nữ', '1980-09-30', N'Bạc Liêu', '0765432189', N'Nhân Viên','321654987123', '2018-11-20', 5000000)
INSERT INTO [NHANVIEN]  VALUES('NV007', N'Lê Văn Giang', N'Nam', '1993-03-17', N'Long An', '0981234567', N'Kỹ Thuật', '654321098765', '2023-01-01', 6000000)
INSERT INTO [NHANVIEN]  VALUES('NV008', N'Đặng Minh Hiếu', N'Nam', '1987-11-05', N'T.HCM', '0123456780', N'Kỹ Thuật', '987654321012', '2020-08-18', 6000000)
INSERT INTO [NHANVIEN]  VALUES('NV009', N'Vũ Văn Thanh', N'Nam', '1991-06-20', N'Tây Ninh', '0345678910', N'Kỹ Thuật', '321098765432', '2022-02-25', 6000000)
INSERT INTO [NHANVIEN]  VALUES('NV010', N'Ngô Thị Tú', N'Nữ', '1989-12-12', N'Tp.HCM', '0765432101', N'Nhân viên', '123098456789', '2019-09-30', 50000000)
select * from NHANVIEN
------------------------------------------------------------------------------------

INSERT INTO [KHACHHANG] VALUES('KH001', N'Trần Văn A', N'Nam', N'Số 1, Đường A, Quận 1, TP.HCM', '0123456789')
INSERT INTO [KHACHHANG] VALUES('KH002', N'Nguyễn Thị B', N'Nữ', N'Số 2, Đường B, Quận 2, TP.HCM', '0987654321')
INSERT INTO [KHACHHANG] VALUES('KH003', N'Lê Văn C', N'Nam', N'Số 3, Đường C, Quận 3, TP.HCM', '0912345678')
INSERT INTO [KHACHHANG] VALUES('KH004', N'Phạm Thị D', N'Nữ', N'Số 4, Đường D, Quận 4, TP.HCM', '0945678901')
INSERT INTO [KHACHHANG] VALUES('KH005', N'Hoàng Văn E', N'Nam', N'Số 5, Đường E, Quận 5, TP.HCM', '0971234567')
select * from KHACHHANG
-------------------------------------------------------------------------------------
INSERT INTO [HDNHAP] VALUES('HDN01','NV001','NCC01','2023-12-10')
INSERT INTO [HDNHAP] VALUES('HDN02','NV004','NCC02','2024-10-10')
INSERT INTO [HDNHAP] VALUES('HDN03','NV005','NCC03','2023-08-10')
INSERT INTO [HDNHAP] VALUES('HDN04','NV001','NCC02','2024-01-20')
INSERT INTO [HDNHAP] VALUES('HDN05','NV010','NCC01','2022-02-15')
SELECT * FROM HDNHAP
-------------------------------------------------------------------------------------
INSERT INTO [CHITIETHDN] VALUES('HDN01','A0001',10,15000000,150000000)
INSERT INTO [CHITIETHDN] VALUES('HDN02','B0017',5,48000000,240000000)
INSERT INTO [CHITIETHDN] VALUES('HDN03','C0005',20,43000000,860000000)
INSERT INTO [CHITIETHDN] VALUES('HDN04','B0001',10,17000000,170000000)
INSERT INTO [CHITIETHDN] VALUES('HDN05','A0020',10,42000000,630000000)
SELECT * FROM CHITIETHDN
-------------------------------------------------------------------------------------

SELECT COUNT(1) FROM TAIKHOAN WHERE USENAME='admin' and PASSWORD='123'
SELECT ROLE FROM TAIKHOAN WHERE USENAME=@username and PASSWORD=@password