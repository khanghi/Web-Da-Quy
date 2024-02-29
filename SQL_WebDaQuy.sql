use master
go
if exists(select NAME from SYSDATABASES where NAME = 'QL_WebDaQuy')
	drop database QL_WebDaQuy
go
-- Tạo database QL_WebDaQuy
create database QL_WebDaQuy
go
use QL_WebDaQuy
go
set dateformat dmy
-- 1: Tạo Table [Accounts] chứa tài khoản thành viên được phép sử dụng các trang quản trị ----
create table TaiKhoan
(
	taiKhoan varchar(20) primary key not null,
	matKhau varchar(20) not null,
	hoDem nvarchar(50) null,
	tenTV nvarchar(30) not null,
	ngaysinh datetime ,
	gioiTinh bit default 1,
	soDT nvarchar(20),
	email nvarchar(50),
	diaChi nvarchar(250),
	trangThai bit default 0,
	ghiChu ntext
)
go

-- 2: Tạo Table [Customers] chứa Thông tin khách hàng  ---------------------------------------
create table KhachHang
(
	maKH varchar(10) primary key not null,
	tenKH nvarchar(50) not null,
	soDT varchar(20) ,
	email varchar(50),
	diaChi nvarchar(250),
	ngaySinh datetime ,
	gioiTinh bit default 1,
	ghiChu ntext
)
go

-- 3: Tạo Table [Articles] chứa thông tin về các bài viết phục vụ cho quảng bá sản phẩm, ------
--    xu hướng mua sắm hiện nay của người tiêu dùng , ...             ------------------------- 
create table BaiViet
(
	maBV varchar(10) primary key not null,
	tenBV nvarchar(250) not null,
	hinhDD varchar(max),
	ndTomTat nvarchar(2000),
	ngayDang datetime ,
	loaiTin nvarchar(30),
	noiDung nvarchar(4000),
	taiKhoan varchar(20) not null ,
	daDuyet bit default 0,
	foreign key (taiKhoan) references taiKhoan(taiKhoan) on update cascade 
)
go
-- 4: Tạo Table [LoaiSP] chứa thông tin loại sản phẩm, ngành hàng -----------------------------
create table ChungLoai
(
	maChung int primary key not null IDENTITY,
	tenChung nvarchar(88) not null,
	ghiChu ntext default ''
)
go
create table LoaiSP
(
	maChung int null,
	maLoai int primary key not null IDENTITY,
	tenLoai nvarchar(88) not null,
	ghiChu ntext default ''
)
go
alter table LoaiSP 
	add constraint fk_loaiSP_chungLoai foreign key(maChung) references ChungLoai(maChung)
-- 4: Tạo Table [Products] chứa thông tin của sản phẩm mà shop kinh doanh online(giá bán đơn vị nghìn đồng)--------------
create table SanPham
(
	maSP varchar(10) primary key not null,
	tenSP nvarchar(500) not NULL,
	hinhDD varchar(max) DEFAULT '',
	ndTomTat nvarchar(2000) DEFAULT '',
	ngayDang DATETIME DEFAULT CURRENT_TIMESTAMP,
	maLoai int not null references LoaiSP(maLoai),
	noiDung nvarchar(4000) DEFAULT '',
	taiKhoan varchar(20) not null foreign key references taiKhoan(taiKhoan) on update cascade,
	dvt nvarchar(32) default N'Cái',
	daDuyet bit default 0,
	giaBan INTEGER DEFAULT 0, 
	giamGia INTEGER DEFAULT 0 CHECK (giamGia>=0 AND giamGia<=100),
	nhaSanXuat nvarchar(168) default '',
	phanLoai nvarchar(50) default '',
)
go

create table HinhChiTiet
(
	STT int primary key not null identity,
	maSP varchar(10) not null foreign key references SanPham(maSP),
	hinhCT varchar(50) not null 
)
-- 5: Tạo Table [Orders] chứa danh sách đơn hàng mà khách đã đặt mua thông qua web ------------
create table DonHang
(
	soDH varchar(10) primary key not null ,
	maKH varchar(10) not null foreign key references khachHang(maKH),
	taiKhoan varchar(20) not null foreign key references taiKhoan(taiKhoan) on update cascade ,
	ngayDat datetime,
	daKichHoat bit default 1,
	ngayGH datetime,
	diaChiGH nvarchar(250),
	ghiChu ntext
)
go	

-- 6: Tạo Table [OrderDetails] chứa thông tin chi tiết của các đơn hàng ---
--    mà khách đã đặt mua với các mặt hàng cùng số lượng đã chọn ---------- 
create table CtDonHang	
(
	soDH varchar(10) not null foreign key references donHang(soDH),
	maSP varchar(10) not null foreign key references sanPham(maSP),
	soLuong int,
	giaBan bigint,
	giamGia BIGINT,
	PRIMARY KEY (soDH, maSP)
)
go
alter table TaiKhoan add ChucNang int default 0

/*========================== Nhập dữ liệu mẫu ==============================*/
go
insert into TaiKhoan(taiKhoan, matKhau, hoDem, tenTV, ngaysinh, gioiTinh, soDT, email, diaChi, trangThai, ghiChu)
values('admin','abc',N'Huỳnh Thị Ngọc',N'Giàu',24/03/2003,0,0832497984,'giauhn2k3@gmail.com','Thạnh Xuân, Q.12, TP.HCM',1,'')
insert into TaiKhoan(taiKhoan, matKhau, hoDem, tenTV, ngaysinh, gioiTinh, soDT, email, diaChi, trangThai, ghiChu)
values('sang','123',N'Nguyễn Minh','Sang',17/05/2003,1,0368170503,'nguyenminhsang2k3@gmail.com','112/35 An Phú Đông 9, phường APĐ, Q.12, TP.HCM',1,'')

INSERT INTO ChungLoai(tenChung) VALUES (N'Trang Sức');
INSERT INTO ChungLoai(tenChung) VALUES (N'Trang Sức Mệnh');
INSERT INTO ChungLoai(tenChung) VALUES (N'Vật Phẩm Phong Thủy');
INSERT INTO ChungLoai(tenChung) VALUES (N'Đá Quý');
INSERT INTO ChungLoai(tenChung) VALUES (N'Bonsai - Lũa - Tượng');
INSERT INTO ChungLoai(tenChung) VALUES (N'Trang sức Handmade');
INSERT INTO ChungLoai(tenChung) VALUES (N'Tượng Đá - Tinh Thể');
INSERT INTO ChungLoai(tenChung) VALUES (N'Tranh Đá');
INSERT INTO ChungLoai(tenChung) VALUES (N'Kim Cương');
INSERT INTO ChungLoai(tenChung) VALUES (N'Trang Sức Đạo');
INSERT INTO ChungLoai(tenChung) VALUES (N'Ngọc Trai');
INSERT INTO ChungLoai(tenChung) VALUES (N'Sản phẩm trưng bày');
INSERT INTO ChungLoai(tenChung) VALUES (N'Đá Quý');

INSERT INTO LoaiSP(maChung, tenLoai) VALUES (1, N'Nhẫn');
INSERT INTO LoaiSP(maChung, tenLoai) VALUES (1, N'Lắc tay - Vòng tay');
INSERT INTO LoaiSP(maChung, tenLoai) VALUES (1, N'Mặt - Dây chuyền');
INSERT INTO LoaiSP(maChung, tenLoai) VALUES (1, N'Phụ Kiện Charm');
INSERT INTO LoaiSP(maChung, tenLoai) VALUES (1, N'Hoa tai - Bông tai');
INSERT INTO LoaiSP(maChung, tenLoai) VALUES (1, N'Trang sức khác');
INSERT INTO LoaiSP(maChung, tenLoai) VALUES (1, N'Đá quý');
INSERT INTO LoaiSP(maChung, tenLoai) VALUES (2, N'Mệnh Kim');
INSERT INTO LoaiSP(maChung, tenLoai) VALUES (2, N'Mệnh Mộc');
INSERT INTO LoaiSP(maChung, tenLoai) VALUES (2, N'Mệnh Thủy');
INSERT INTO LoaiSP(maChung, tenLoai) VALUES (2, N'Mệnh Hỏa');
INSERT INTO LoaiSP(maChung, tenLoai) VALUES (2, N'Mệnh Thổ');
INSERT INTO LoaiSP(maChung, tenLoai) VALUES (3, N'Tỳ Hưu');
INSERT INTO LoaiSP(maChung, tenLoai) VALUES (3, N'Thiềm Thừ (cỡ nhỏ)');
INSERT INTO LoaiSP(maChung, tenLoai) VALUES (3, N'Tượng Di Lạc');
INSERT INTO LoaiSP(maChung, tenLoai) VALUES (3, N'Quả Cầu');
INSERT INTO LoaiSP(maChung, tenLoai) VALUES (3, N'Tháp Văn Xương');
INSERT INTO LoaiSP(maChung, tenLoai) VALUES (3, N'Bộ Thất Tinh');
INSERT INTO LoaiSP(maChung, tenLoai) VALUES (11, N'Ngọc Trai Akoya');

INSERT INTO SanPham(maSP, tenSP, hinhDD, maLoai, noiDung, taiKhoan, daDuyet, giaBan, phanLoai)
VALUES ('001', N'Bộ Thất Tinh Đá Thạch Anh Hồng', 'sanpham1.jpg', 3, N'	Màu: Hồng
																		Mệnh: Hỏa, Thổ
																		Chất liệu: Thạch Anh Hồng tự nhiên
																		Xuất xứ: Madagascar
																		Ý nghĩa: Giới doanh nhân coi đĩa thất tinh là bùa chú cầu may, bởi tác dụng kỳ diệu mà nó mang lại cho công việc làm ăn của họ.',
		 'admin', 1, 1399, 'qt');
INSERT INTO SanPham(maSP, tenSP, hinhDD, maLoai, noiDung, taiKhoan, daDuyet, giaBan, phanLoai)
VALUES ('002', N'Tháp Văn Xương Thạch Anh Trắng', 'sanpham2.jpg', 17, N'Màu: Trắng
Mệnh: Kim, Thủy
Chất liệu: Thạch Anh Trắng tự nhiên
Xuất xứ: Brazil
Ý nghĩa: Biểu tượng của sự thành công, thăng tiến trong công danh, sự nghiệp, học tập. Hóa sát, trừ tà', 'admin', 1, 2000, 'qt');
INSERT INTO SanPham(maSP, tenSP, hinhDD, maLoai, noiDung, taiKhoan, daDuyet, giaBan, phanLoai)
VALUES ('003', N'Quả Cầu Đá Canxit Vàng', 'sanpham3.jpg', 16, N'Màu: Vàng
Mệnh: Thổ, Kim
Chất liệu: Thạch Anh Canxit tự nhiên
Xuất xứ: New Mexico, Brazil, Trung Quốc
Ý nghĩa: Quả cầu đá phong thủy là biểu tượng của thành công, chiêu tài hóa sát, mang lại vận may cho người sử dụng', 'admin', 1, 499, 'qt');
INSERT INTO SanPham(maSP, tenSP, hinhDD, maLoai, noiDung, taiKhoan, daDuyet, giaBan, phanLoai)
VALUES ('004', N'Đại Thế Chí Bồ Tát Đá Ngọc Bích Nephrite', 'sanpham4.jpg', 3, N'Màu: Xanh
Mệnh: Hỏa, Mộc
Chất liệu: Đá Mã Ngọc Bích Nephrite tự nhiên
Xuất xứ: Canada, Nga
Ý nghĩa: Phật Đại Thế Chí Bồ Tát tượng trưng cho ánh sáng và trí tuệ, có thể phù hộ cho những người sinh năm Ngọ tích luỹ được tiền bạc, thuận lợi bình an.', 'admin', 1, 2500, 'qt');
INSERT INTO SanPham(maSP, tenSP, hinhDD, maLoai, noiDung, taiKhoan, daDuyet, giaBan, phanLoai)
VALUES ('005', N'Vòng Tay Ngọc Bích Nephrite Mix Charm Hoa Sen Vàng', 'sanpham5.jpg', 2, N'Màu: Xanh
Mệnh: Mộc, Hỏa
Chất liệu: Ngọc Bích Nephrite
Xuất xứ: Canada, Nga
Ý nghĩa: Ngọc Bích là biểu tượng của bình an, may mắn. Mang Ngọc Bích bên mình thường xuyên giúp tăng cường sức khỏe, kéo dài tuổi thọ, chống nhược thị, hỗ trợ luân chuyển những dòng khí trong cơ thể.', 'admin', 1, 8500, 'qt');
INSERT INTO SanPham(maSP, tenSP, hinhDD, maLoai, noiDung, taiKhoan, daDuyet, giaBan, phanLoai)
VALUES ('006', N'Dây Chuyền Bạch Kim Full Kim Cương – Sapphire Blue MDS16', 'sanpham18.jpg', 3, N'Dây Chuyền Bạch Kim Đính Sapphire Xanh MDS15
Đá chủ: Sapphire xanh hero 5.68*4.60
Đá tấm: Kim cương thiên nhiên 12viên 1.1 ly
Chất liệu dây: Bạch Kim ( Platinum)
Kích thước dây: 42cm, đường kính 0.7mm', 'admin', 1, 9900, 'qt');
INSERT INTO SanPham(maSP, tenSP, hinhDD, maLoai, noiDung, taiKhoan, daDuyet, giaBan, phanLoai)
VALUES ('007', N'Bộ Trang Sức Đá Topaz Thiên Nhiên - Combo 3 Món', 'sanpham7.jpg', 3, N'Bộ Trang Sức Đá Topaz Thiên Nhiên

Bao gồm các sản phẩm: Nhẫn, Mặt dây chuyền, Bông tai
Chất liệu : bạc mạ vàng ( có thay thể sang vàng)

Kích thước

Đá chủ nhẫn và mặt dây:  9.6*9.6*4.0 và được mài tam giác
Đá chủ bông tai 6.0*3.0*2.5 Mài hình chữ nhật', 'admin', 1, 3990, 'qt');
INSERT INTO SanPham(maSP, tenSP, hinhDD, maLoai, noiDung, taiKhoan, daDuyet, giaBan, phanLoai)
VALUES ('008', N'Sapphire Vàng Đậm Giọt Nước – VJS17', 'sanpham8.jpg', 3, N'Sapphire vàng đậm mài oval
Kích thước: 6.78*5.63*3.95
Trọng lượng: 1.2ct
Màu sắc: Vàng
Kiểu mài: Oval', 'admin', 1, 2590, 'qt');
INSERT INTO SanPham(maSP, tenSP, hinhDD, maLoai, noiDung, taiKhoan, daDuyet, giaBan, phanLoai)
VALUES ('009', N'Bông Tai Đá Ruby BTR9', 'sanpham9.jpg', 5, N'Tên sản phẩm:	Bông đá ruby
Đá chủ:	Ruby
Đá tấm:	CZ
Màu sắc: Đỏ
Giác mài: Oval
Chất liệu: bạc 925 mạ vàng
Mã số: BTR9', 'admin', 1, 2500, 'qt');
INSERT INTO SanPham(maSP, tenSP, hinhDD, maLoai, noiDung, taiKhoan, daDuyet, giaBan, phanLoai)
VALUES ('010', N' Combo 3 in 1 Hoa Mai Đá Thiên Nhiên | Charm – Mặt Dây – Móc Khoá MDF3', 'sanpham12.jpg', 3, N'1 sản phẩm với 3 công dụng vừa làm móc khoá, vừa làm charm, vừa làm mặt dây

Tuỳ chọn màu sắc dưới đây:
– Vàng: Thạch anh vàng
– Xanh lá trong: Florit
– Xanh biển: Aquamarine
– Trắng: Thạch anh trắng
Kích thước: 1.2 * 2.0 ( tính kèm khoen )

Lưu ý: Tuỳ chọn màu sắc quý khách ghi vào phần ghi  chú đơn hàng', 'admin', 1, 250, 'tl');
INSERT INTO SanPham(maSP, tenSP, hinhDD, maLoai, noiDung, taiKhoan, daDuyet, giaBan, phanLoai)
VALUES ('011', N'Vòng Đá Ngọc Bích Mix Charm Lá Ginko VCN5', 'sanpham13.jpg', 2, N'Chất Liệu: Ngọc Bích
Kích Thước:	Giao động theo Size
Màu Sắc: Xanh Lá
Mệnh: Mệnh Hỏa, Mệnh Mộc
Danh mục: Lắc Tay - Vòng Tay, Vòng tay Mix Charm', 'admin', 1, 450, 'tl');
INSERT INTO SanPham(maSP, tenSP, hinhDD, maLoai, noiDung, taiKhoan, daDuyet, giaBan, phanLoai)
VALUES ('012', N'Ngọc Trai Akoya Biển Loại 1', 'sanpham14.jpg', 19, N'Size 3-6li ;  600.000đ 
Size 6.5-7.5 li  : 700.000đ
Size 7.5-8li  : 1.400.000đ
Size 8.5-9li  : 2.500.000đ
Size 9.5 -10 : 4.000.000đ', 'admin', 1, 600, 'tl');
INSERT INTO SanPham(maSP, tenSP, hinhDD, maLoai, noiDung, taiKhoan, daDuyet, giaBan, phanLoai)
VALUES ('013', N'[Sale 27%] Nhẫn Unisex Saphire Blue Mix Kim Cương NW13', 'sanpham15.jpg', 1, N'Đá chủ	1 viên Sapphire
Màu sắc	Xanh blue
Kích thước	6ly
Đá tấm	6 viên kim cương
Kích thước	2.3ly
Chất liệu	Vàng trắng 10K', 'admin', 1, 16000, 'tl qt');
INSERT INTO SanPham(maSP, tenSP, hinhDD, maLoai, noiDung, taiKhoan, daDuyet, giaBan, phanLoai)
VALUES ('014', N'[THANH LÝ] Đá Spinel Tấm Hàng Li Size  1mm – 4mm', 'sanpham16.jpg', 7, N'Đá Spinel đa dạng về màu sắc cùng với lửa rực rỡ khi được
 mài facet tạo độ óng ánh rất đẹp. Thích hợp làm trang sức nhẫn, bông tai, mặt dây chuyền …', 'admin', 1, 50, 'tl');
INSERT INTO SanPham(maSP, tenSP, hinhDD, maLoai, noiDung, taiKhoan, daDuyet, giaBan, phanLoai)
VALUES ('015', N'Nhẫn Cưới Sapphire NC39', 'sanpham20.jpg', 1, N'Nhẫn Cưới Trái Tim NC39
Chất liệu tùy chọn:  Bạc, Vàng vàng, vàng hồng, vàng trắng, Platinum
Tuỳ chọn đá: Sapphire Chọn tại đây
Chất liệu gia công: vàng 10K, 14k và 18k, và bạc 925
Trọng lượng ước tính: 2,5 chỉ/ cặp ( giao động tùy theo size tay)
Gói sản phẩm bao gồm: Nhẫn, hộp đựng, phiếu mua hàng kiêm bảo hành', 'admin', 1, 19900, '');
INSERT INTO SanPham(maSP, tenSP, hinhDD, maLoai, noiDung, taiKhoan, daDuyet, giaBan, phanLoai)
VALUES ('016', N'Mặt Dây Chuyền Sapphire Vàng MDS17', 'sanpham21.webp', 2, N'Mặt dây chuyền sapphire vàng

Đá sapphire vàng thiên nhiên cắt giọt nước
Chất liệu vàng: vàng 18K

Đá thiên nhiên thoải mái kiểm định trên toàn quốc', 'admin', 1, 2100, 'tl');
INSERT INTO SanPham(maSP, tenSP, hinhDD, maLoai, noiDung, taiKhoan, daDuyet, giaBan, phanLoai)
VALUES ('017', N'Mặt Dây Chuyền Serpentine Đá Thiên Nhiên MDQX2', 'sanpham22.jpg', 2, N'Mặt dây chuyền Serpentine VNJ mài từ đá thiên nhiên được 
kết hợp cùng chất liệu bạc giúp sản phẩm đẹp tinh tế', 'admin', 1, 850, 'tl qt');

INSERT INTO HinhChiTiet(maSP, hinhCT) VALUES ('001', 'chiTiet-1.jpg');
INSERT INTO HinhChiTiet(maSP, hinhCT) VALUES ('001', 'chiTiet-2.jpg');
INSERT INTO HinhChiTiet(maSP, hinhCT) VALUES ('001', 'chiTiet-3.jpg');
INSERT INTO HinhChiTiet(maSP, hinhCT) VALUES ('001', 'chiTiet-4.jpg');
INSERT INTO HinhChiTiet(maSP, hinhCT) VALUES ('001', 'chiTiet-5.jpg');
INSERT INTO HinhChiTiet(maSP, hinhCT) VALUES ('001', 'chiTiet-6.jpg');
GO

select * from LoaiSP
select * from ChungLoai
go
---tat ca san pham
drop View V_allSP
create View V_allSP
as
select top(10) *
from SanPham
where daDuyet = 1
order by maSP
---san pham moi
drop View V_newSP
create View V_newSP
as
select top(10) *
from SanPham
where daDuyet = 1
order by ngayDang DESC
--san pham thanh ly
drop View V_tlSP
create View V_tlSP
as
select top(10) *
from SanPham
where phanLoai like N'%tl%' and daDuyet = 1
order by ngayDang 
--san pham ban chay
drop View V_hotSP
create View V_hotSP
as
select top(10) *
from SanPham
where phanLoai like N'%qt%' and daDuyet = 1
order by ngayDang 
---so luong theo ma loai
drop function F_soSP
create function F_soSP
(
	@maLoai int
)
returns int
as
begin
	declare @soSP int = 0
	select @soSP = Count(maLoai)
	from SanPham
	where maLoai = @maLoai
	Group by maLoai
	return @soSP
end
go
select dbo.F_soSP(9)
--
drop view V_soSP
create view V_soSP
as
select maLoai, dbo.F_soSP(maLoai) as SoLuong
from LoaiSP




