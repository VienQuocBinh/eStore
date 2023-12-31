USE [master]
GO
/****** Object:  Database [EStore]    Script Date: 23/07/2022 11:08:40 pm ******/
CREATE DATABASE [EStore]
 
GO
ALTER DATABASE [EStore] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [EStore].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [EStore] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [EStore] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [EStore] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [EStore] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [EStore] SET ARITHABORT OFF 
GO
ALTER DATABASE [EStore] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [EStore] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [EStore] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [EStore] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [EStore] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [EStore] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [EStore] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [EStore] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [EStore] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [EStore] SET  ENABLE_BROKER 
GO
ALTER DATABASE [EStore] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [EStore] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [EStore] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [EStore] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [EStore] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [EStore] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [EStore] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [EStore] SET RECOVERY FULL 
GO
ALTER DATABASE [EStore] SET  MULTI_USER 
GO
ALTER DATABASE [EStore] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [EStore] SET DB_CHAINING OFF 
GO
ALTER DATABASE [EStore] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [EStore] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [EStore] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [EStore] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'EStore', N'ON'
GO
ALTER DATABASE [EStore] SET QUERY_STORE = OFF
GO
USE [EStore]
GO
/****** Object:  Table [dbo].[Member]    Script Date: 23/07/2022 11:08:40 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Member](
	[MemberId] [int] IDENTITY(1,1) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[CompanyName] [nvarchar](40) NOT NULL,
	[City] [varchar](15) NOT NULL,
	[Country] [varchar](15) NOT NULL,
	[Password] [varchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MemberId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 23/07/2022 11:08:40 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[MemberId] [int] NOT NULL,
	[OrderDate] [datetime] NOT NULL,
	[RequiredDate] [datetime] NULL,
	[ShippedDate] [datetime] NULL,
	[Freight] [money] NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 23/07/2022 11:08:40 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[OrderId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[UnitPrice] [money] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Discount] [float] NOT NULL,
 CONSTRAINT [Order_Detail_PK] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC,
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 23/07/2022 11:08:40 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryId] [int] NOT NULL,
	[ProductName] [varchar](40) NOT NULL,
	[Weight] [varchar](20) NOT NULL,
	[UnitPrice] [money] NOT NULL,
	[UnitInStock] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Member] ON 

INSERT [dbo].[Member] ([MemberId], [Email], [CompanyName], [City], [Country], [Password]) VALUES (1, N'binhvq@gmail.com', N'FPT', N'HCM', N'Vietnam', N'binhvq')
INSERT [dbo].[Member] ([MemberId], [Email], [CompanyName], [City], [Country], [Password]) VALUES (2, N'thuyenva@gmail.com', N'MOMO', N'HCM', N'Vietnam', N'thuyenva')
INSERT [dbo].[Member] ([MemberId], [Email], [CompanyName], [City], [Country], [Password]) VALUES (3, N'huyphm@gmail.com', N'Zalo', N'Cu Chi', N'Vietnam', N'huyphm')
INSERT [dbo].[Member] ([MemberId], [Email], [CompanyName], [City], [Country], [Password]) VALUES (4, N'taict@gmail.com', N'HPT', N'HCM', N'Vietnam', N'taict')
INSERT [dbo].[Member] ([MemberId], [Email], [CompanyName], [City], [Country], [Password]) VALUES (5, N'quocnda@gmail.com', N'Shoppe', N'HCM', N'Vietnam', N'1')
SET IDENTITY_INSERT [dbo].[Member] OFF
GO
SET IDENTITY_INSERT [dbo].[Order] ON 

INSERT [dbo].[Order] ([OrderId], [MemberId], [OrderDate], [RequiredDate], [ShippedDate], [Freight]) VALUES (1, 5, CAST(N'2022-01-01T00:00:00.000' AS DateTime), CAST(N'2022-01-12T08:12:32.000' AS DateTime), CAST(N'2022-01-15T10:12:32.000' AS DateTime), 10000.0000)
INSERT [dbo].[Order] ([OrderId], [MemberId], [OrderDate], [RequiredDate], [ShippedDate], [Freight]) VALUES (2, 3, CAST(N'2022-01-01T19:50:11.200' AS DateTime), CAST(N'2022-01-14T06:12:32.000' AS DateTime), CAST(N'2022-01-16T03:12:22.000' AS DateTime), 10000.0000)
INSERT [dbo].[Order] ([OrderId], [MemberId], [OrderDate], [RequiredDate], [ShippedDate], [Freight]) VALUES (3, 2, CAST(N'2022-06-15T13:01:12.000' AS DateTime), CAST(N'2022-06-15T13:01:12.000' AS DateTime), CAST(N'2022-06-20T14:57:12.000' AS DateTime), 12000.0000)
INSERT [dbo].[Order] ([OrderId], [MemberId], [OrderDate], [RequiredDate], [ShippedDate], [Freight]) VALUES (4, 2, CAST(N'2022-03-15T12:01:12.000' AS DateTime), CAST(N'2022-03-15T12:01:12.000' AS DateTime), CAST(N'2022-03-21T10:01:12.000' AS DateTime), 15000.0000)
INSERT [dbo].[Order] ([OrderId], [MemberId], [OrderDate], [RequiredDate], [ShippedDate], [Freight]) VALUES (5, 1, CAST(N'2022-06-27T00:00:00.000' AS DateTime), CAST(N'2022-06-28T13:35:00.000' AS DateTime), CAST(N'2022-07-15T13:35:00.000' AS DateTime), 1000.0000)
INSERT [dbo].[Order] ([OrderId], [MemberId], [OrderDate], [RequiredDate], [ShippedDate], [Freight]) VALUES (6, 3, CAST(N'2022-07-01T00:00:00.000' AS DateTime), CAST(N'2022-07-15T13:36:00.000' AS DateTime), CAST(N'2022-07-16T13:36:00.000' AS DateTime), 50000.0000)
INSERT [dbo].[Order] ([OrderId], [MemberId], [OrderDate], [RequiredDate], [ShippedDate], [Freight]) VALUES (7, 1, CAST(N'2022-07-23T14:01:07.237' AS DateTime), CAST(N'2022-08-02T14:01:07.237' AS DateTime), CAST(N'2022-08-02T14:01:07.237' AS DateTime), 1000.0000)
INSERT [dbo].[Order] ([OrderId], [MemberId], [OrderDate], [RequiredDate], [ShippedDate], [Freight]) VALUES (8, 2, CAST(N'2022-07-01T00:00:00.000' AS DateTime), CAST(N'2022-07-23T14:09:00.000' AS DateTime), CAST(N'2022-07-23T14:09:00.000' AS DateTime), 50000.0000)
INSERT [dbo].[Order] ([OrderId], [MemberId], [OrderDate], [RequiredDate], [ShippedDate], [Freight]) VALUES (9, 1, CAST(N'2022-07-23T14:10:07.340' AS DateTime), CAST(N'2022-08-02T14:10:07.340' AS DateTime), CAST(N'2022-08-02T14:10:07.340' AS DateTime), 1000.0000)
INSERT [dbo].[Order] ([OrderId], [MemberId], [OrderDate], [RequiredDate], [ShippedDate], [Freight]) VALUES (10, 1, CAST(N'2022-07-23T14:36:19.670' AS DateTime), CAST(N'2022-08-02T14:36:19.670' AS DateTime), CAST(N'2022-08-02T14:36:19.670' AS DateTime), 1000.0000)
SET IDENTITY_INSERT [dbo].[Order] OFF
GO
INSERT [dbo].[OrderDetail] ([OrderId], [ProductId], [UnitPrice], [Quantity], [Discount]) VALUES (1, 1, 10000.0000, 2, 0)
INSERT [dbo].[OrderDetail] ([OrderId], [ProductId], [UnitPrice], [Quantity], [Discount]) VALUES (1, 3, 40000.0000, 2, 0)
INSERT [dbo].[OrderDetail] ([OrderId], [ProductId], [UnitPrice], [Quantity], [Discount]) VALUES (3, 1, 10000.0000, 3, 0)
INSERT [dbo].[OrderDetail] ([OrderId], [ProductId], [UnitPrice], [Quantity], [Discount]) VALUES (3, 2, 7000.0000, 5, 0)
INSERT [dbo].[OrderDetail] ([OrderId], [ProductId], [UnitPrice], [Quantity], [Discount]) VALUES (4, 3, 40000.0000, 1, 0)
INSERT [dbo].[OrderDetail] ([OrderId], [ProductId], [UnitPrice], [Quantity], [Discount]) VALUES (7, 1, 10000.0000, 1, 0)
INSERT [dbo].[OrderDetail] ([OrderId], [ProductId], [UnitPrice], [Quantity], [Discount]) VALUES (9, 2, 7000.0000, 3, 0)
INSERT [dbo].[OrderDetail] ([OrderId], [ProductId], [UnitPrice], [Quantity], [Discount]) VALUES (10, 1, 10000.0000, 1, 0)
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([ProductId], [CategoryId], [ProductName], [Weight], [UnitPrice], [UnitInStock]) VALUES (1, 1, N'Coca-Cola', N'300', 10000.0000, 13)
INSERT [dbo].[Product] ([ProductId], [CategoryId], [ProductName], [Weight], [UnitPrice], [UnitInStock]) VALUES (2, 2, N'Apple', N'500', 7000.0000, 53)
INSERT [dbo].[Product] ([ProductId], [CategoryId], [ProductName], [Weight], [UnitPrice], [UnitInStock]) VALUES (3, 3, N'Burger', N'600', 40000.0000, 21)
INSERT [dbo].[Product] ([ProductId], [CategoryId], [ProductName], [Weight], [UnitPrice], [UnitInStock]) VALUES (4, 3, N'Water', N'100', 10000.0000, 4)
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD FOREIGN KEY([MemberId])
REFERENCES [dbo].[Member] ([MemberId])
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD FOREIGN KEY([OrderId])
REFERENCES [dbo].[Order] ([OrderId])
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
USE [master]
GO
ALTER DATABASE [EStore] SET  READ_WRITE 
GO
