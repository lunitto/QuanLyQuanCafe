USE [master]
GO
/****** Object:  Database [QuanLyQuanCafe]    Script Date: 01/08/2019 3:45:05 SA ******/
CREATE DATABASE [QuanLyQuanCafe]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'QuanLyQuanCafe', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\QuanLyQuanCafe.mdf' , SIZE = 4288KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'QuanLyQuanCafe_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\QuanLyQuanCafe_log.ldf' , SIZE = 1072KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [QuanLyQuanCafe] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QuanLyQuanCafe].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QuanLyQuanCafe] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET ARITHABORT OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QuanLyQuanCafe] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QuanLyQuanCafe] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET  ENABLE_BROKER 
GO
ALTER DATABASE [QuanLyQuanCafe] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QuanLyQuanCafe] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET RECOVERY FULL 
GO
ALTER DATABASE [QuanLyQuanCafe] SET  MULTI_USER 
GO
ALTER DATABASE [QuanLyQuanCafe] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QuanLyQuanCafe] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QuanLyQuanCafe] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [QuanLyQuanCafe] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'QuanLyQuanCafe', N'ON'
GO
USE [QuanLyQuanCafe]
GO
/****** Object:  UserDefinedFunction [dbo].[fuConvertToUnsign1]    Script Date: 01/08/2019 3:45:05 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fuConvertToUnsign1] ( @strInput NVARCHAR(4000) ) RETURNS NVARCHAR(4000) AS BEGIN IF @strInput IS NULL RETURN @strInput IF @strInput = '' RETURN @strInput DECLARE @RT NVARCHAR(4000) DECLARE @SIGN_CHARS NCHAR(136) DECLARE @UNSIGN_CHARS NCHAR (136) SET @SIGN_CHARS = N'ăâđêôơưàảãạáằẳẵặắầẩẫậấèẻẽẹéềểễệế ìỉĩịíòỏõọóồổỗộốờởỡợớùủũụúừửữựứỳỷỹỵý ĂÂĐÊÔƠƯÀẢÃẠÁẰẲẴẶẮẦẨẪẬẤÈẺẼẸÉỀỂỄỆẾÌỈĨỊÍ ÒỎÕỌÓỒỔỖỘỐỜỞỠỢỚÙỦŨỤÚỪỬỮỰỨỲỶỸỴÝ' +NCHAR(272)+ NCHAR(208) SET @UNSIGN_CHARS = N'aadeoouaaaaaaaaaaaaaaaeeeeeeeeee iiiiiooooooooooooooouuuuuuuuuuyyyyy AADEOOUAAAAAAAAAAAAAAAEEEEEEEEEEIIIII OOOOOOOOOOOOOOOUUUUUUUUUUYYYYYDD' DECLARE @COUNTER int DECLARE @COUNTER1 int SET @COUNTER = 1 WHILE (@COUNTER <=LEN(@strInput)) BEGIN SET @COUNTER1 = 1 WHILE (@COUNTER1 <=LEN(@SIGN_CHARS)+1) BEGIN IF UNICODE(SUBSTRING(@SIGN_CHARS, @COUNTER1,1)) = UNICODE(SUBSTRING(@strInput,@COUNTER ,1) ) BEGIN IF @COUNTER=1 SET @strInput = SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)-1) ELSE SET @strInput = SUBSTRING(@strInput, 1, @COUNTER-1) +SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)- @COUNTER) BREAK END SET @COUNTER1 = @COUNTER1 +1 END SET @COUNTER = @COUNTER +1 END SET @strInput = replace(@strInput,' ','-') RETURN @strInput END
GO
/****** Object:  Table [dbo].[Account]    Script Date: 01/08/2019 3:45:05 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[userName] [nvarchar](100) NOT NULL,
	[displayName] [nvarchar](100) NOT NULL DEFAULT (N'user'),
	[passWord] [nvarchar](1000) NOT NULL,
	[type] [int] NOT NULL DEFAULT ((0)),
PRIMARY KEY CLUSTERED 
(
	[userName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Bill]    Script Date: 01/08/2019 3:45:05 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bill](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[DateCheckIn] [date] NOT NULL DEFAULT (getdate()),
	[DatecheckOut] [date] NULL,
	[idTable] [int] NOT NULL,
	[status] [int] NOT NULL DEFAULT ((0)),
	[discount] [int] NULL,
	[totalPrice] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BillInfo]    Script Date: 01/08/2019 3:45:05 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BillInfo](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idBill] [int] NOT NULL,
	[idFood] [int] NOT NULL,
	[count] [int] NOT NULL DEFAULT ((0)),
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Food]    Script Date: 01/08/2019 3:45:05 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Food](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NOT NULL DEFAULT (N'Chưa đặt tên'),
	[idCategory] [int] NOT NULL,
	[price] [float] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FoodCategory]    Script Date: 01/08/2019 3:45:05 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FoodCategory](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NOT NULL DEFAULT (N'Chưa đặt tên'),
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TableFood]    Script Date: 01/08/2019 3:45:05 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TableFood](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NOT NULL DEFAULT (N'Bàn chưa đặt tên'),
	[status] [nvarchar](100) NOT NULL DEFAULT (N'Trống'),
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
INSERT [dbo].[Account] ([userName], [displayName], [passWord], [type]) VALUES (N'Admin', N'lunitto', N'1', 1)
INSERT [dbo].[Account] ([userName], [displayName], [passWord], [type]) VALUES (N'staff', N'staff', N'1', 0)
SET IDENTITY_INSERT [dbo].[Bill] ON 

INSERT [dbo].[Bill] ([id], [DateCheckIn], [DatecheckOut], [idTable], [status], [discount], [totalPrice]) VALUES (11, CAST(N'2019-07-27' AS Date), CAST(N'2019-07-27' AS Date), 2, 1, 0, 3)
INSERT [dbo].[Bill] ([id], [DateCheckIn], [DatecheckOut], [idTable], [status], [discount], [totalPrice]) VALUES (12, CAST(N'2019-07-27' AS Date), CAST(N'2019-07-27' AS Date), 1, 1, 0, 269000)
INSERT [dbo].[Bill] ([id], [DateCheckIn], [DatecheckOut], [idTable], [status], [discount], [totalPrice]) VALUES (13, CAST(N'2019-07-27' AS Date), CAST(N'2019-07-27' AS Date), 7, 1, 0, 24000)
INSERT [dbo].[Bill] ([id], [DateCheckIn], [DatecheckOut], [idTable], [status], [discount], [totalPrice]) VALUES (1011, CAST(N'2019-07-28' AS Date), CAST(N'2019-07-29' AS Date), 2, 1, 0, 40000)
INSERT [dbo].[Bill] ([id], [DateCheckIn], [DatecheckOut], [idTable], [status], [discount], [totalPrice]) VALUES (1012, CAST(N'2019-07-28' AS Date), CAST(N'2019-07-29' AS Date), 1, 1, 0, 30000)
INSERT [dbo].[Bill] ([id], [DateCheckIn], [DatecheckOut], [idTable], [status], [discount], [totalPrice]) VALUES (1013, CAST(N'2019-07-29' AS Date), CAST(N'2019-07-29' AS Date), 11, 1, 0, 40000)
INSERT [dbo].[Bill] ([id], [DateCheckIn], [DatecheckOut], [idTable], [status], [discount], [totalPrice]) VALUES (1014, CAST(N'2019-07-29' AS Date), CAST(N'2019-07-29' AS Date), 3, 1, 0, 30000)
INSERT [dbo].[Bill] ([id], [DateCheckIn], [DatecheckOut], [idTable], [status], [discount], [totalPrice]) VALUES (1015, CAST(N'2019-07-29' AS Date), CAST(N'2019-07-29' AS Date), 11, 1, 0, 0)
INSERT [dbo].[Bill] ([id], [DateCheckIn], [DatecheckOut], [idTable], [status], [discount], [totalPrice]) VALUES (1016, CAST(N'2019-07-29' AS Date), CAST(N'2019-07-29' AS Date), 12, 1, 0, 30000)
INSERT [dbo].[Bill] ([id], [DateCheckIn], [DatecheckOut], [idTable], [status], [discount], [totalPrice]) VALUES (1017, CAST(N'2019-07-29' AS Date), CAST(N'2019-07-29' AS Date), 11, 1, 0, 0)
INSERT [dbo].[Bill] ([id], [DateCheckIn], [DatecheckOut], [idTable], [status], [discount], [totalPrice]) VALUES (1018, CAST(N'2019-07-29' AS Date), CAST(N'2019-07-29' AS Date), 2, 1, 0, 60000)
INSERT [dbo].[Bill] ([id], [DateCheckIn], [DatecheckOut], [idTable], [status], [discount], [totalPrice]) VALUES (1019, CAST(N'2019-07-29' AS Date), CAST(N'2019-07-29' AS Date), 11, 1, 0, 0)
INSERT [dbo].[Bill] ([id], [DateCheckIn], [DatecheckOut], [idTable], [status], [discount], [totalPrice]) VALUES (1020, CAST(N'2019-07-29' AS Date), CAST(N'2019-07-29' AS Date), 11, 1, 0, 0)
INSERT [dbo].[Bill] ([id], [DateCheckIn], [DatecheckOut], [idTable], [status], [discount], [totalPrice]) VALUES (1021, CAST(N'2019-07-29' AS Date), CAST(N'2019-07-29' AS Date), 3, 1, 0, 0)
INSERT [dbo].[Bill] ([id], [DateCheckIn], [DatecheckOut], [idTable], [status], [discount], [totalPrice]) VALUES (1022, CAST(N'2019-07-29' AS Date), CAST(N'2019-07-29' AS Date), 7, 1, 0, 120000)
INSERT [dbo].[Bill] ([id], [DateCheckIn], [DatecheckOut], [idTable], [status], [discount], [totalPrice]) VALUES (1023, CAST(N'2019-07-29' AS Date), CAST(N'2019-07-29' AS Date), 11, 1, 0, 0)
INSERT [dbo].[Bill] ([id], [DateCheckIn], [DatecheckOut], [idTable], [status], [discount], [totalPrice]) VALUES (1024, CAST(N'2019-07-29' AS Date), NULL, 13, 0, 0, NULL)
INSERT [dbo].[Bill] ([id], [DateCheckIn], [DatecheckOut], [idTable], [status], [discount], [totalPrice]) VALUES (1025, CAST(N'2019-07-29' AS Date), CAST(N'2019-07-29' AS Date), 6, 1, 0, 0)
INSERT [dbo].[Bill] ([id], [DateCheckIn], [DatecheckOut], [idTable], [status], [discount], [totalPrice]) VALUES (1026, CAST(N'2019-07-29' AS Date), CAST(N'2019-07-29' AS Date), 1, 1, 0, 0)
SET IDENTITY_INSERT [dbo].[Bill] OFF
SET IDENTITY_INSERT [dbo].[BillInfo] ON 

INSERT [dbo].[BillInfo] ([id], [idBill], [idFood], [count]) VALUES (29, 11, 3, 2)
INSERT [dbo].[BillInfo] ([id], [idBill], [idFood], [count]) VALUES (30, 12, 3, 2)
INSERT [dbo].[BillInfo] ([id], [idBill], [idFood], [count]) VALUES (31, 13, 9, 2)
INSERT [dbo].[BillInfo] ([id], [idBill], [idFood], [count]) VALUES (32, 11, 1, 1)
INSERT [dbo].[BillInfo] ([id], [idBill], [idFood], [count]) VALUES (33, 11, 7, 2)
INSERT [dbo].[BillInfo] ([id], [idBill], [idFood], [count]) VALUES (34, 11, 4, 1)
INSERT [dbo].[BillInfo] ([id], [idBill], [idFood], [count]) VALUES (35, 11, 8, 1)
INSERT [dbo].[BillInfo] ([id], [idBill], [idFood], [count]) VALUES (36, 12, 10, 2)
INSERT [dbo].[BillInfo] ([id], [idBill], [idFood], [count]) VALUES (37, 12, 5, 2)
INSERT [dbo].[BillInfo] ([id], [idBill], [idFood], [count]) VALUES (38, 12, 6, 1)
INSERT [dbo].[BillInfo] ([id], [idBill], [idFood], [count]) VALUES (39, 12, 2, 1)
INSERT [dbo].[BillInfo] ([id], [idBill], [idFood], [count]) VALUES (1026, 1011, 2, 1)
INSERT [dbo].[BillInfo] ([id], [idBill], [idFood], [count]) VALUES (1027, 1012, 3, 2)
INSERT [dbo].[BillInfo] ([id], [idBill], [idFood], [count]) VALUES (1029, 1013, 2, 1)
INSERT [dbo].[BillInfo] ([id], [idBill], [idFood], [count]) VALUES (1030, 1014, 3, 2)
INSERT [dbo].[BillInfo] ([id], [idBill], [idFood], [count]) VALUES (1033, 1016, 3, 2)
INSERT [dbo].[BillInfo] ([id], [idBill], [idFood], [count]) VALUES (1036, 1018, 3, 2)
INSERT [dbo].[BillInfo] ([id], [idBill], [idFood], [count]) VALUES (1043, 1022, 1, 1)
SET IDENTITY_INSERT [dbo].[BillInfo] OFF
SET IDENTITY_INSERT [dbo].[Food] ON 

INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (1, N'Mực một nằng nướng sa tế', 2, 120000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (2, N'Nghêu hấp xả', 2, 40000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (3, N'Nem tai lợn cuốn lá xung', 1, 30000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (4, N'Cút lộn xào me', 1, 40000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (5, N'Nầm dê nướng', 3, 50000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (6, N'Thịt lợn rừng nướng ', 3, 75000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (7, N'Cơm tấm mushi', 1, 99999)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (8, N'7 Up', 4, 12000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (9, N'Cafe', 4, 12000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (10, N'Coca ', 4, 12000)
SET IDENTITY_INSERT [dbo].[Food] OFF
SET IDENTITY_INSERT [dbo].[FoodCategory] ON 

INSERT [dbo].[FoodCategory] ([id], [name]) VALUES (1, N'Nông Sản')
INSERT [dbo].[FoodCategory] ([id], [name]) VALUES (2, N'Hải Sản')
INSERT [dbo].[FoodCategory] ([id], [name]) VALUES (3, N'Lâm Sản')
INSERT [dbo].[FoodCategory] ([id], [name]) VALUES (4, N'Đồ uống')
SET IDENTITY_INSERT [dbo].[FoodCategory] OFF
SET IDENTITY_INSERT [dbo].[TableFood] ON 

INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (1, N'Bàn 1', N'Trống')
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (2, N'Bàn 2', N'Trống')
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (3, N'Bàn 3', N'Trống')
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (4, N'Bàn 4', N'Trống')
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (5, N'Bàn 5', N'Trống')
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (6, N'Bàn 6', N'Trống')
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (7, N'Bàn 7', N'Trống')
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (8, N'Bàn 8', N'Trống')
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (9, N'Bàn 9', N'Trống')
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (10, N'Bàn 10', N'Trống')
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (11, N'Bàn 11', N'Trống')
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (12, N'Bàn 12', N'Trống')
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (13, N'Bàn 13', N'Trống')
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (14, N'Bàn 14', N'Trống')
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (15, N'Bàn 15', N'Trống')
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (16, N'Bàn 16', N'Trống')
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (17, N'Bàn 17', N'Trống')
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (18, N'Bàn 18', N'Trống')
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (19, N'Bàn 19', N'Trống')
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (20, N'Bàn 20', N'Trống')
SET IDENTITY_INSERT [dbo].[TableFood] OFF
ALTER TABLE [dbo].[Bill]  WITH CHECK ADD FOREIGN KEY([idTable])
REFERENCES [dbo].[TableFood] ([id])
GO
ALTER TABLE [dbo].[BillInfo]  WITH CHECK ADD FOREIGN KEY([idBill])
REFERENCES [dbo].[Bill] ([id])
GO
ALTER TABLE [dbo].[BillInfo]  WITH CHECK ADD FOREIGN KEY([idFood])
REFERENCES [dbo].[Food] ([id])
GO
ALTER TABLE [dbo].[Food]  WITH CHECK ADD FOREIGN KEY([idCategory])
REFERENCES [dbo].[FoodCategory] ([id])
GO
/****** Object:  StoredProcedure [dbo].[USP_CheckUpdateForTable]    Script Date: 01/08/2019 3:45:05 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_CheckUpdateForTable]
@idTable INT, @idBill INT, @idBillInfo INT
AS BEGIN
	SELECT @idTable = idTable FROM dbo.Bill WHERE id = @idBill
	DECLARE @count INT = 0 
	SELECT @count = COUNT(*) FROM dbo.BillInfo AS bi,dbo.Bill AS b WHERE b.id = bi.idBill AND b.id =@idBill AND b.status = 0

	IF(@count = 0)
		UPDATE dbo.TableFood SET status = N'Trống' WHERE id = @idTable
END

GO
/****** Object:  StoredProcedure [dbo].[USP_GetAccountByUserName]    Script Date: 01/08/2019 3:45:05 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROC [dbo].[USP_GetAccountByUserName]
@userName nvarchar(100)
AS
BEGIN
	SELECT * FROM dbo.Account WHERE UserName = @userName
END


GO
/****** Object:  StoredProcedure [dbo].[USP_GetListBillByDate]    Script Date: 01/08/2019 3:45:05 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_GetListBillByDate]
@checkIn DATE, @checkOut DATE
AS BEGIN
		SELECT t.name AS [Tên bàn], b.totalPrice AS [Tổng tiền] , b.DateCheckIn AS [Ngày vào], b.DatecheckOut AS [Ngày ra],b.discount AS [Giảm giá] 
		FROM dbo.Bill AS b, dbo.TableFood AS t
		WHERE b.DateCheckIn >= @checkIn AND b.DatecheckOut <= @checkOut AND b. status = 1
		AND t.id = b.idTable
END

GO
/****** Object:  StoredProcedure [dbo].[USP_GetListFood]    Script Date: 01/08/2019 3:45:05 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_GetListFood]
AS BEGIN
SELECT f.name AS [Tên món], c.name AS [Loại món],f.price AS [Giá], f.id as [Mã] 
FROM dbo.Food AS f, dbo.FoodCategory AS c 
WHERE c.id = f.idCategory
END

GO
/****** Object:  StoredProcedure [dbo].[USP_GetListFoodIntoDtdvFood]    Script Date: 01/08/2019 3:45:05 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_GetListFoodIntoDtdvFood]
AS BEGIN
SELECT f.name AS [Tên món], c.name AS [Loại món],f.price AS [Giá], f.id as [mã] 
FROM dbo.Food AS f, dbo.FoodCategory AS c 
WHERE c.id = f.idCategory
END

GO
/****** Object:  StoredProcedure [dbo].[USP_GetTableList]    Script Date: 01/08/2019 3:45:05 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_GetTableList]
AS SELECT * FROM dbo.TableFood

GO
/****** Object:  StoredProcedure [dbo].[USP_GrossTable]    Script Date: 01/08/2019 3:45:05 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_GrossTable]
@idBillGoss1 INT, @idBillGoss2 INT, @idTableGoss1 INT
AS BEGIN
		UPDATE dbo.BillInfo SET idBill = @idBillGoss2 WHERE idBill = @idBillGoss1
		UPDATE dbo.Bill SET status = 1 WHERE id = @idBillGoss1
		UPDATE dbo.TableFood SET status = N'Trống' WHERE id = @idTableGoss1
END


GO
/****** Object:  StoredProcedure [dbo].[USP_InsertBill]    Script Date: 01/08/2019 3:45:05 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_InsertBill]
@idTable INT
AS
BEGIN
	INSERT dbo.Bill
	        ( DateCheckIn ,
	          DatecheckOut ,
	          idTable ,
	          status,
			  discount
	        )
	VALUES  ( GETDATE() , -- DateCheckIn - date
	          NULL , -- DatecheckOut - date
	          @idTable , -- idTable - int
	          0,  -- status - int
			  0
	        )
END

GO
/****** Object:  StoredProcedure [dbo].[USP_InsertBillInfo]    Script Date: 01/08/2019 3:45:05 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_InsertBillInfo]
@idBill INT, @idFood INT, @count INT
AS
BEGIN

	DECLARE @isExitsBill INT
	DECLARE @foodCount INT

	SELECT @isExitsBill = id, @foodCount =count FROM dbo.BillInfo WHERE idBill = @idBill AND idFood = @idFood

	IF(@isExitsBill > 0)
	BEGIN
		DECLARE @newCount INT = @foodCount + @count
		IF(@newCount > 0)
		UPDATE dbo.BillInfo SET count = @foodCount + @count WHERE idFood=@idFood
		ELSE
		DELETE dbo.BillInfo WHERE idBill = @idBill AND idFood = @idFood
	END
	ELSE
	BEGIN
		INSERT dbo.BillInfo
	        ( idBill, idFood, count )
	VALUES  ( @idBill, -- idBill - int
	          @idFood, -- idFood - int
	          @count  -- count - int
	          )
	END	
END

GO
/****** Object:  StoredProcedure [dbo].[USP_Login]    Script Date: 01/08/2019 3:45:05 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
 CREATE PROC [dbo].[USP_Login]
 @userName NVARCHAR(100), @passWord NVARCHAR(100)
 AS
 BEGIN
	SELECT *FROM dbo.Account WHERE userName = @userName AND passWord = @passWord   
 END

GO
/****** Object:  StoredProcedure [dbo].[USP_SwitchBillOffTable]    Script Date: 01/08/2019 3:45:05 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_SwitchBillOffTable]
@idTable1 INT, @idTable2 INT, @idBill INT  
  AS
  BEGIN
		DECLARE @b INT
		 
		SELECT @b =idTable FROM dbo.Bill WHERE id = @idBill
	
		IF(@b = @idTable1) UPDATE dbo.Bill SET idTable = @idTable2 WHERE id = @idBill
		ELSE UPDATE dbo.Bill SET idTable =@idTable1 WHERE id =@idBill
		IF ((SELECT status FROM dbo.TableFood WHERE id = @idTable1) = N'Trống') UPDATE dbo.TableFood SET status = N'Có người' WHERE id = @idTable1
		ELSE UPDATE dbo.TableFood SET status = N'Trống' WHERE id = @idTable1
		IF ((SELECT status FROM dbo.TableFood WHERE id = @idTable2) = N'Trống') UPDATE dbo.TableFood SET status = N'Có người' WHERE id = @idTable2
		ELSE UPDATE dbo.TableFood SET status = N'Trống' WHERE id = @idTable2

  END

GO
/****** Object:  StoredProcedure [dbo].[USP_SwitchTable]    Script Date: 01/08/2019 3:45:05 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE PROC [dbo].[USP_SwitchTable]
  @idFirstTable INT, @idSecondTable INT 
  AS
  BEGIN
		DECLARE @a INT
		SELECT @a = dbo.Bill.idTable FROM dbo.Bill WHERE id = @idFirstTable

		DECLARE @a2 INT
		SELECT @a2 = dbo.Bill.idTable FROM dbo.Bill WHERE id = @idSecondTable

		UPDATE dbo.Bill SET idTable = @a2 WHERE id = @idFirstTable
		UPDATE dbo.Bill SET idTable = @a WHERE id = @idSecondTable
  END

GO
/****** Object:  StoredProcedure [dbo].[USP_UpdateAccount]    Script Date: 01/08/2019 3:45:05 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_UpdateAccount]
@userName NVARCHAR(100), @disPlayName NVARCHAR(100), @passWord NVARCHAR(100), @newPassWord NVARCHAR(100)
AS BEGIN
		 DECLARE @isRightPass INT =0
		 SELECT @isRightPass = COUNT(*) FROM dbo.Account WHERE userName = @userName AND passWord = @passWord
		 IF(@isRightPass = 1)
		 BEGIN 
			IF (@newPassWord = NULL OR @newPassWord = '')
			
				UPDATE dbo.Account SET displayName = @disPlayName WHERE userName = @userName
			
            ELSE
				UPDATE dbo.Account SET displayName = @disPlayName, passWord = @newPassWord WHERE userName = @userName    
		END
END


GO
USE [master]
GO
ALTER DATABASE [QuanLyQuanCafe] SET  READ_WRITE 
GO
