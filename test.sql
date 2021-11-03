use test1

bulk insert Ho from 'D:\Ho.txt'
with
(
	rowterminator = '\n'
)

bulk insert Dem from 'D:\Dem.txt'
with
(
	rowterminator = '\n'
)


bulk insert Ten from 'D:\Ten.txt'
with
(
	rowterminator = '\n'
)

bulk insert Duong from 'D:\Duong.txt'
with
(
	rowterminator = '\n'
)

bulk insert Phuong from 'D:\Phuong.txt'
with
(
	rowterminator = '\n'
)

bulk insert Quan from 'D:\Quan.txt'
with
(
	rowterminator = '\n'
)

go

create table KhachHang
(
	MaKH char(10),
	Hoten nvarchar(30),
	NgSinh date,
	SoNha int,
	Duong nvarchar(30),
	Phuong nvarchar(30),
	Quan nvarchar(30),
	TPho nvarchar(30),
	DienThoai varchar(15)
	primary key (MaKH)
)

go

create procedure Addrows
	@nrow int
as
begin
	declare @NgSinh date
	DECLARE @Ho NVARCHAR(50)
	DECLARE @Dem NVARCHAR(50)		
	DECLARE @Ten NVARCHAR(50)
	declare @HoTen nvarchar(50)
	declare @SoNha int
	declare @Duong nvarchar(30)
	declare @Phuong	nvarchar(30)
	declare @Quan nvarchar(30)
	declare @TP nvarchar(30)
	declare @SDT varchar(15)
	set nocount on
	DECLARE @id int
	DECLARE @iteration INT = 0
	WHILE @iteration < @nrow
		begin
			set @id=@iteration+1
			set @NgSinh = DATEADD(DAY, ABS(CHECKSUM(NEWID()) % 364 ), '2011-01-01')
			SELECT @SoNha = RAND()*(400- 1)+1 ;
			set @Ho	= (select Top 1 * FROM Ho order by newid() )
			set @Dem = (select top 1 * FROM Dem order by newid())
			set @Ten = (select top 1 * FROM Ten order by newid()) 
			select @HoTen = Isnull(@Ho,' ') +' '+ Isnull(@Dem,' ')+' '+ Isnull(@Ten,' ') 
			set @Duong = (select Top 1 * FROM Duong order by newid())
			set @Phuong = (select Top 1 * FROM Phuong order by newid())
			set @Quan = (select Top 1 * FROM Quan order by newid())
			set @TP= (select Top 1 * FROM TP order by newid())
			set @SDT =(select TOP 10 ABS(CAST(CAST(NEWID() AS VARBINARY) AS INT)))
			insert into KhachHang values(@id,@HoTen,@NgSinh,@SoNha,@Duong,@Phuong,@Quan,@TP,@SDT)
		--	UPDATE Hoten
		--	SET @HoTen = @Ho + ' ' + @Dem + ' ' + @Ten
		--	WHERE ISNULL(@HoTen,'') = '' AND  ISNULL(@Ho,'') <> '' AND ISNULL(@Dem,'') <> '' AND ISNULL(@Ten,'') <> ''
			SET @iteration += 1
		end
	SET NOCOUNT OFF
end

EXEC dbo.Addrows 1000
go

create table SanPham
(
	MaSP char(10),
	TenSP nvarchar(20),
	SoLuongTon int,
	MoTa nvarchar(30),
	Gia int
	primary key (MaSP)
)

bulk insert DSSP from 'D:\SP.txt'
with
(
	rowterminator = '\n'
)
go

create procedure AddrowsSP
	@nrow int
as
begin
	DECLARE @Ten NVARCHAR(50)
	declare @slt int
	declare @gia int 
	set nocount on
	DECLARE @id int
	DECLARE @iteration INT = 0
	WHILE @iteration < @nrow
		begin
			set @id=@iteration +1
			set @Ten = (select Top 1 * FROM DSSP WHERE SP IS NOT NULL order by newid())
			SELECT @slt = RAND()*(1000 - 1)+1 ;
			set @gia = RAND()*(1000000 - 10000)+10000 ;
			insert into SanPham values (@id,@Ten,@slt,NULL,@gia)
			SET @iteration += 1
		end
	SET NOCOUNT OFF
end

EXEC dbo.AddrowsSP 1000
select * from SanPham
go

create procedure AddrowsHD
	@nrow int
as
begin
	DECLARE @makh char(10)
	declare @ngaylap date
	declare @tongtien int 
	set nocount on
	DECLARE @id int
	DECLARE @iteration INT = 0
	WHILE @iteration < @nrow
		begin
			set @id=@iteration +1
			set @makh = (select top 1 kh.MaKH from KhachHang kh order by newid())
			declare @FromDate date = '2020-05-01'
			declare @ToDate date = '2021-06-30'
			select @ngaylap = dateadd(day, 
               rand(checksum(newid()))*(1+datediff(day, @FromDate, @ToDate)), 
               @FromDate)
			insert into HoaDon values (@id,@makh,@ngaylap,0)
			SET @iteration += 1
		end
	SET NOCOUNT OFF
end
EXEC dbo.AddrowsHD 1000
select * from HoaDon
go

create procedure AddrowsCT
	@nrow int
as
begin
	declare @mahd char(10)
	DECLARE @masp char(10)
	declare @sl int
	declare @giaban int 
	declare @giagiam int 
	declare @thanhtien int 
	set nocount on
	DECLARE @iteration INT = 0
	WHILE @iteration < @nrow
		begin
			set @masp = (select top 1 sp.MaSP from SanPham sp order by newid())
			set @mahd = (select top 1 hd.MaHD from HoaDon hd order by newid())
			set @sl = RAND()*(1000 - 1)+1 ;
			set @giaban = RAND()*(1000000 - 10000)+10000 ;
			set @giagiam = RAND()*(100000 - 10000)+10000 ;
			set @thanhtien = @sl*(@giaban - @giagiam);
			insert into CT_HoaDon values (@mahd,@masp,@sl,@giaban,@giagiam,@thanhtien)
			SET @iteration += 1
		end
	SET NOCOUNT OFF
end

EXEC dbo.AddrowsCT 100
select * from CT_HoaDon

go

create procedure SumHD(@mahd varchar(10))
as
begin
	declare @tong int
	set @tong = (select sum(ct.ThanhTien)
				from CT_HoaDon ct
				where ct.MaHD = @mahd)
	update HoaDon set TongTien = @tong where MaHD= @mahd
end

select * from HoaDon where MaHD='149'
select * from CT_HoaDon

exec SumHD '149'