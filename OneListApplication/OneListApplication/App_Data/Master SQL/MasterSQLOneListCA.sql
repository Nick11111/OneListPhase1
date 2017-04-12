USE [master]
GO
/****** Object:  Database [DB_110660_onelistca]    Script Date: 4/7/2017 12:53:57 PM ******/
CREATE DATABASE [DB_110660_onelistca] 
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DB_110660_onelistca].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DB_110660_onelistca] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DB_110660_onelistca] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DB_110660_onelistca] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DB_110660_onelistca] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DB_110660_onelistca] SET ARITHABORT OFF 
GO
ALTER DATABASE [DB_110660_onelistca] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DB_110660_onelistca] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [DB_110660_onelistca] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DB_110660_onelistca] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DB_110660_onelistca] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DB_110660_onelistca] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DB_110660_onelistca] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DB_110660_onelistca] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DB_110660_onelistca] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DB_110660_onelistca] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DB_110660_onelistca] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DB_110660_onelistca] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DB_110660_onelistca] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DB_110660_onelistca] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DB_110660_onelistca] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DB_110660_onelistca] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DB_110660_onelistca] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DB_110660_onelistca] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DB_110660_onelistca] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [DB_110660_onelistca] SET  MULTI_USER 
GO
ALTER DATABASE [DB_110660_onelistca] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DB_110660_onelistca] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DB_110660_onelistca] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DB_110660_onelistca] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [DB_110660_onelistca]
GO
/****** Object:  StoredProcedure [dbo].[UpdateInfousers]    Script Date: 4/7/2017 12:53:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateInfousers]

AS
BEGIN
	-- Stored to update information on user table from aspnetusers
	SET NOCOUNT ON;
	INSERT INTO dbo.[user]  ( [UserID]
      ,[UserName]
	  ,[FirstName]
	  ,[LastName]
      ,[BirthDate]
      ,[Email]
      ,[ActiveUser]
	  ,[Password])
		select Id,UserName,'','','',Email,1,'' from dbo.[aspnetusers] where Id not in (select userId from dbo.[user]);
END

GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 4/7/2017 12:53:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 4/7/2017 12:53:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 4/7/2017 12:53:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 4/7/2017 12:53:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 4/7/2017 12:53:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 4/7/2017 12:53:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Coupon]    Script Date: 4/7/2017 12:53:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Coupon](
	[CouponID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](20) NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
	[DiscountPercentage] [float] NOT NULL,
	[RetailID] [int] NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndingDate] [date] NOT NULL,
 CONSTRAINT [PK_Coupon] PRIMARY KEY CLUSTERED 
(
	[CouponID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Item]    Script Date: 4/7/2017 12:53:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Item](
	[ItemID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [nvarchar](200) NOT NULL,
	[ItemName] [nvarchar](50) NULL,
	[ItemDescription] [nvarchar](50) NULL,
	[ItemCategory] [int] NULL,
 CONSTRAINT [PK_Item_1] PRIMARY KEY CLUSTERED 
(
	[ItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ItemCategory]    Script Date: 4/7/2017 12:53:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemCategory](
	[ItemCategoryID] [int] IDENTITY(1,1) NOT NULL,
	[ItemCategoryName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ItemCategory] PRIMARY KEY CLUSTERED 
(
	[ItemCategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[List]    Script Date: 4/7/2017 12:53:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[List](
	[ListID] [int] IDENTITY(1,1) NOT NULL,
	[CreatorID] [nvarchar](200) NOT NULL,
	[ListName] [nchar](50) NOT NULL,
	[ListTypeID] [int] NOT NULL,
	[CreationDate] [date] NOT NULL,
	[ListStatusID] [int] NOT NULL,
 CONSTRAINT [PK_List_1] PRIMARY KEY CLUSTERED 
(
	[ListID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ListItem]    Script Date: 4/7/2017 12:53:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ListItem](
	[ListID] [int] IDENTITY(1,1) NOT NULL,
	[ItemID] [int] NOT NULL,
	[ListItemSolved] [bit] NOT NULL,
	[ListItemSolver] [nvarchar](200) NULL,
	[ListItemSolvingDate] [date] NOT NULL,
	[ListItemCost] [decimal](18, 0) NULL,
	[ListItemNotes] [nvarchar](50) NULL,
 CONSTRAINT [PK_ListItem] PRIMARY KEY CLUSTERED 
(
	[ListID] ASC,
	[ItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ListStatus]    Script Date: 4/7/2017 12:53:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ListStatus](
	[ListStatusID] [int] IDENTITY(1,1) NOT NULL,
	[StatusName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ListStatus] PRIMARY KEY CLUSTERED 
(
	[ListStatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ListType]    Script Date: 4/7/2017 12:53:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ListType](
	[ListTypeID] [int] IDENTITY(1,1) NOT NULL,
	[TypeName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ListType] PRIMARY KEY CLUSTERED 
(
	[ListTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ListUser]    Script Date: 4/7/2017 12:53:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ListUser](
	[ListUserID] [int] IDENTITY(1,1) NOT NULL,
	[ListID] [int] NOT NULL,
	[SuscriberGroupID] [int] NOT NULL,
	[SuscriptionDate] [nchar](10) NOT NULL,
 CONSTRAINT [PK_ListUser] PRIMARY KEY CLUSTERED 
(
	[ListUserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Retail]    Script Date: 4/7/2017 12:53:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Retail](
	[RetailID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
	[Telephone] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Retail] PRIMARY KEY CLUSTERED 
(
	[RetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SuscriberGroup]    Script Date: 4/7/2017 12:53:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SuscriberGroup](
	[SuscriberGroupID] [int] IDENTITY(1,1) NOT NULL,
	[SuscriberGroupName] [nvarchar](100) NOT NULL,
	[UserID] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_SuscriberGroup] PRIMARY KEY CLUSTERED 
(
	[SuscriberGroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SuscriberGroupUser]    Script Date: 4/7/2017 12:53:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SuscriberGroupUser](
	[SuscriberGroupUserID] [int] IDENTITY(1,1) NOT NULL,
	[SuscriberGroupID] [int] NOT NULL,
	[UserID] [nvarchar](200) NOT NULL,
	[UserTypeID] [int] NOT NULL,
	[ListUserStatus] [nchar](10) NOT NULL,
	[SuscriptionDate] [nchar](10) NOT NULL,
 CONSTRAINT [PK_SuscriberGroupUser] PRIMARY KEY CLUSTERED 
(
	[SuscriberGroupUserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 4/7/2017 12:53:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserID] [nvarchar](200) NOT NULL,
	[UserName] [nvarchar](200) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[BirthDate] [date] NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[ActiveUser] [bit] NOT NULL,
	[ProfilePic] [image] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserType]    Script Date: 4/7/2017 12:53:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserType](
	[UserTypeID] [int] IDENTITY(1,1) NOT NULL,
	[UserTypeName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_UserType] PRIMARY KEY CLUSTERED 
(
	[UserTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
INSERT [dbo].[__MigrationHistory] ([MigrationId], [ContextKey], [Model], [ProductVersion]) VALUES (N'201702211952102_InitialCreate', N'Microsoft.AspNet.Identity.EntityFramework.IdentityDbContext', 0x1F8B0800000000000400DD5CDD6EDC3616BE2FD0771074D52DDC917F3641D6182770C7766B6CFC838C53EC5DC09138632112A58A9463A3E893F5A28FB4AFB0A44469448AD4501A8D465E14483322F99DC3C343F2F0F063FEFBD7DFD30FCF61603DC104FB113AB38F2687B605911B793E5A9DD92959FEF4CEFEF0FEFBEFA6975EF86CFD56D43B61F5684B84CFEC4742E253C7C1EE230C019E84BE9B44385A92891B850EF022E7F8F0F05FCED1910329844DB12C6BFA2945C40F61F683FE9C45C88531494170137930C0FC3B2D9967A8D62D08218E810BCFEC9B12FF1CC7B7904CAE3D48C1C8CBE432FBDF5542EB7E8B92AFB6751EF8802A3887C1D2B60042110184AA7FFA19C3394922B49AC7F403081E5E6248EB2D418021EFD6E9BABA690F0F8F590F9D75C302CA4D3189C296804727DC648EDCBC93E1EDD2A4D4A8B9A158AF33C39ED985093F450135802CF0741624AC720BDB4FAA88079671BB83D2C58E2787ECBF036B9606244DE019822949407060DDA78BC077FF0D5F1EA2AF109D9D1C2D9627EFDEBC05DEC9DB7FC29337D59ED2BED27AC207FAE93E89629850DDE0B2ECBF6D39623B476E5836ABB4C9AD427D89CE16DBBA01CF1F215A91473A8F8EDFD9D695FF0CBDE20B77AECFC8A7938B3622494A7FDEA6410016012CCB9D4699ECCF06A9C76FDEF622F5163CF9AB6CE825F974E224D8B63EC1202BC58F7E9C4F2F61BCBFF06A574914B2DFA27FE5A55FE6519AB8AC3391B6CA0348569088DA4D9DB5F31AB93483EADFAD0BD4F1BB36D3B4EEDECAAAAC435D66422162E8D950E8BB5BB99D3CAE7F6F1BBFA7BD9645F432047ED0C32A6A2085C6354B3F0961D9CB9F23EAB300B5D6F91E604C87D6FB15E0C706D5E95F7B507D0EDD34A10E3527208C772EEDFE3142F0360D176CD60C27ABB7A179F8165D019744C92562ADB6C6FB18B95FA3945C22EF0210F899B80520FBF9E087E600BDA873EEBA10E32BEACCD09B45346C2F00AF1139396E0DC796B27D4731B300F8617318C3D4FC52D4ABC73195626D2053ADA38A649A34FC18AD7C64A061514FA3615EDCAC21AFD356438664A020AFA6D12F2B6D562FAFD25B24988D47FF9B7306FBFFBE43EB267CC58C73BA0CC25F2082095DABBC7B40084CD07A044C16877D4404D9F031A13BDF803249BF8120ED5B54A7D990CDFDFE6743063BFED990A9493F3FF91E0B3D0C0E4845650A6F545F7DF6DA3CE724CD869E0E423787163ECC1AA09B2EE71847AE9FCD02456A8C273644FD69A0666DCE72E4BD913325B463D4D1FD98BA36FD42FB66CB4E75872E600009B4CEDD3C753803D8055EDD8CB4435E0BC58A1D55A1D83A63222AF7634D26F57498B046809D74309DA93E22F569E123D78F41B0D14A524BC32D8CF5BD9421975CC018222670A3254C84AB13244C81528E34289B2C34752A1E67E688D5D074D3802BE354F5880FEC8AAAE858A3198FD976EA8C0A430DE88D0A639848D7A6F60677477E0E311A74F950321E77948E421ACD78D0B47B77140D35B43B8AC6785DEE989F3A8DC65C3A828EC719C583EFFEB6E9BA9586F644C1122373C43C8AA46D086D0113C9192F16AC043E13C5198B2AC98F599847ACB25B3084392462C2651DB62AC349A71944F69D26C0B57F6D00E5B77D8D40AD342BF26E8D883C2A68015B24CB1A61F9EA2EC15646BF8E5DBDF2AC54D45F8CCA6E697482287B56BA42CDBD8D02FE0A8EC21BE4D54AEC780BA3080954BD55B4C1AC71385BE90F1F0303BBA8A24F8D618A3EF46B99C2173758461557194756DD2D2305421ACB147DE8D732DC11371846B1C39BEEF1DDCD226EC93D4DA4221351EE2165D9D4C9C94FFCC3D4D1B0A4A637208E7DB4AAB0A6F8176B9E53A6663FCDDB9386C21CC371B1823B546A5B4A22510256502AA5A2A9A6577E82C905206001581E66E685B56AF51D53B3AE17F2AA9B627DF88A05BEA8CDFE9EB7E8C66052841D1C9AD65F852C70C952DF0A9F5037B718B30D04205164DB67519086481F47E95BE7176BD5F6F9973AC2D491F4AF854A3523D6E25871448CC6AB3E51763B76650CD37DFCF410BA5128C2CFEA38E842523D4A916BAAA2E8F24F7B1B4F5D3CD3F7186E397E43CD3FCE2CA902F04F2D312AE4841A58A5CC1C55E48F5431C512734489245285948A5A6859A582084A560B3AE1692CAAAE612EA14EFEA8A2D74BCD911534902AB4A2B803B64267B9CC1C55C114A9022B8ACDB1D7B41179551DF10EA73DD7F4BD3CE667E1EDD6480DC66E16CA7EB6C8CAB57D15A8F2B92516BF98AF81F1EFA37432ED11B16F27CB3323DB39990643BF4E09F7E0E232D57879AFC7142EB785ADA0E9725F8FD7CE9577EA30B5A3A45CA5945E1E29A5A3E3941FE336BFC2A99DEBF22AB655989186012F98C070C22A4CE6BF07B3C0876CD12F2ADC00E42F212639A1C33E3E3C3A965EEC8CE7F58C83B117B47842238ED900DC2CF40412F7112475A6C4162F4CD6A0B5ECF535F2E0F399FD47D6EA344B96B0BF659F0FAC6BFC19F9BFA7B4E02149A1F5679DDED93FE3BEABDD077F1F616ED5EBFF7CC99B1E5877099D31A7D6A164CB2E232CBE9A68A54DDE740B6DBABDA578BDB349786AA044956643F797050B9FF4F2AAA0D0F287103CFFA3AD6ACA97035B212A5E07F485D78B0975ECFF2E585AE6BF477F928CF9DFAEB3EA97005D54D3BE02F0517B30F90D80F91A54B4DCE33EA338270DB1246576DE48AFDE8A6BB9EF8DA9C6C2DE6AA2D799D62DE0B6605377F08C574644EE6D7754F08C7BC3DEA76BEF9C5C3C163EF19A22B25F1AF1D094246399AF92303C261A1C67F9EC99193CB47FE9F2B963E65CB6A0008FC9C138DF6BCF5CDFA11D4C97CB1DB38399937AC7E45FFBDE1EF7E15DC6DBE3DE89BA757692E6FE4595E4DDC4C5CD33E2F4F4BE88A807E4D1627EE1A2E688E984AD3D452B705D452F544F4E6B12BC91D7DB2CB09D30BE9F374AE4759AC56A489B4DB2F952DF289BD76996ADA145EE8342AC2429AA38DD1B962FCD6D9E921D3E42CAB0B603C666101C537375FE0A18C2DB1B4298259AEBDDD11382B737439FD3A20501B87E3B4B77C4CABFA4487765ECAFD610ECDF5544D015F6C2B2CE355A46C5962C69545491722A3790008F6E94E709F197C025B4986585B307DA59A68DDD4D2CA0778DEE5212A7847619868B404851B1ADBD497EC67216759EDEC5D93F31D24717A89A3ECBA6DFA19F533FF04ABDAF14591C0D048B19780E968D2561B9D8D54B89741B2143206EBE32D47980611C50307C87E6E00976D18DBADF47B802EECB3A67A703D93C10A2D9A7173E582520C41C63DD9EFEA43EEC85CFEFFF07D8F4487350540000, N'6.1.3-40302')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'Administrator', N'Administrator')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'User', N'User')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'754f76ef-aebe-4263-ad47-54ceecc2f027', N'Administrator')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c647e456-8439-4e5c-963b-019bf397c541', N'Administrator')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'14aaa334-d3bb-404a-8c92-28f706109ee1', N'User')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'21cbe0e4-0b8e-434e-955a-4b2a81f3c5fb', N'User')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'607f5266-5e5c-48e2-a107-f9cc0e0a9a84', N'User')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'60863fc2-9747-4e4b-8be9-491d5da207b6', N'User')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'6f74b845-02a4-46cd-a26e-076da0ca3663', N'User')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c647e456-8439-4e5c-963b-019bf397c541', N'User')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'14aaa334-d3bb-404a-8c92-28f706109ee1', N'xmercadoca@gmail.com', 1, N'AJKlwfB1vXD6LNiwjokyDbE+MMSz8rv6UsyAXy/mxDbLa3ZXp0Tu6vMiqXv1ZubD2Q==', N'806a0fcd-0bb8-4087-9d3c-4100f0bfc967', NULL, 0, 0, NULL, 1, 0, N'xavier')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'209f065f-03fc-4c2f-b83e-9c86b3b78d3c', N'ing.fxmm@gmail.com', 1, N'ALao9VH90gEItI3ocHj++dBzFGgl9V7fi1p5RUWRf2xocXXDZ9dl8qYCcsH81Rlduw==', N'1a14a138-b3d6-488b-9f25-160c2b6f8e34', NULL, 0, 0, NULL, 1, 0, N'XavierMercado')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'21cbe0e4-0b8e-434e-955a-4b2a81f3c5fb', N'orotenxillo@gmail.com', 1, N'AB2//GDpZDRkqVnCiyvWmIhv11TW/HCSpMhrzrD7D23PjYNGYPJhs5uxmpdmgfCpWg==', N'2f89e803-cafa-4bf7-9a25-582444e2554f', NULL, 0, 0, NULL, 1, 0, N'Victoria')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'607f5266-5e5c-48e2-a107-f9cc0e0a9a84', N'l.uriel.mf@gmail.com', 1, N'ADSM+kK7rj6iFM4bg1IEhQwj7Ip14sRXgfxSSmbyvF3UvBoAoAwYTfJ3Uo3dfJHrXg==', N'd2e09d0d-f9c3-49d5-a564-97c6c3642cd4', NULL, 0, 0, NULL, 1, 0, N'Uriel')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'60863fc2-9747-4e4b-8be9-491d5da207b6', N'eileenwang.1025@gmail.com', 1, N'APNKkeoNAdFZlb/x6rwdla8Y2Ic1/mzfFAD0xzvKJQeR4uuuPAyV1/8XDD12TkhF+A==', N'5b2deac4-6c9b-4bce-a4b8-a3315936481a', NULL, 1, 0, NULL, 1, 0, N'eileen')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'6f74b845-02a4-46cd-a26e-076da0ca3663', N'max_asky@yahoo.com', 1, N'AFA3kZBNkfe31fRnsvJa7yYiKfUcuAaOxk6kCPQ5XntsC1+D2eZG3qFUMScWWipLIQ==', N'ce60e692-7cde-42dd-acbe-9fe57b18e589', NULL, 0, 0, NULL, 1, 0, N'user')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'754f76ef-aebe-4263-ad47-54ceecc2f027', N'maxsiwu@gmail.com', 1, N'AJpI3GANADXAI6A/dkno16p7m1lzwr8k8KBG7wIXVz56r8SgCH4BXBSK40jry5fJMA==', N'bec6e9f4-cddb-468a-97fb-f6779e9b43e5', NULL, 0, 0, CAST(0x0000A74C014E2CC6 AS DateTime), 1, 0, N'Max')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'c647e456-8439-4e5c-963b-019bf397c541', N'pmcgee@bcit.ca', 1, N'AEJIwJOJRDyw4i2jiKLD/R7D4as+8e2mTCztca9XAmm2F4Y7OtWHwJNj0hUJUz71kg==', N'b7a03bef-1187-4d98-9c23-75d1f7b685e4', NULL, 0, 0, NULL, 1, 0, N'Pat')
SET IDENTITY_INSERT [dbo].[Coupon] ON 

INSERT [dbo].[Coupon] ([CouponID], [Title], [Description], [DiscountPercentage], [RetailID], [StartDate], [EndingDate]) VALUES (1, N'Bananas Discount!', N'Buy all bananas with a 20% off', 20, 1, CAST(0x753C0B00 AS Date), CAST(0x843C0B00 AS Date))
INSERT [dbo].[Coupon] ([CouponID], [Title], [Description], [DiscountPercentage], [RetailID], [StartDate], [EndingDate]) VALUES (2, N'Ham Magic!', N'5% off when buying more than 200grms of Ham', 5, 2, CAST(0x7B3C0B00 AS Date), CAST(0x843C0B00 AS Date))
INSERT [dbo].[Coupon] ([CouponID], [Title], [Description], [DiscountPercentage], [RetailID], [StartDate], [EndingDate]) VALUES (3, N'Chocolate lovers', N'all chocolates have 2x1', 50, 3, CAST(0x753C0B00 AS Date), CAST(0x843C0B00 AS Date))
SET IDENTITY_INSERT [dbo].[Coupon] OFF
SET IDENTITY_INSERT [dbo].[Item] ON 

INSERT [dbo].[Item] ([ItemID], [UserID], [ItemName], [ItemDescription], [ItemCategory]) VALUES (6, N'6f74b845-02a4-46cd-a26e-076da0ca3663', N'bananas', N'good food', 6)
INSERT [dbo].[Item] ([ItemID], [UserID], [ItemName], [ItemDescription], [ItemCategory]) VALUES (7, N'6f74b845-02a4-46cd-a26e-076da0ca3663', N'oranges', N'juice', 4)
INSERT [dbo].[Item] ([ItemID], [UserID], [ItemName], [ItemDescription], [ItemCategory]) VALUES (10, N'6f74b845-02a4-46cd-a26e-076da0ca3663', N'apples', N'and stuff', 1)
INSERT [dbo].[Item] ([ItemID], [UserID], [ItemName], [ItemDescription], [ItemCategory]) VALUES (13, N'14aaa334-d3bb-404a-8c92-28f706109ee1', N'Bananas', N'Buy Bananas(x2)', 4)
SET IDENTITY_INSERT [dbo].[Item] OFF
SET IDENTITY_INSERT [dbo].[ItemCategory] ON 

INSERT [dbo].[ItemCategory] ([ItemCategoryID], [ItemCategoryName]) VALUES (1, N'General')
INSERT [dbo].[ItemCategory] ([ItemCategoryID], [ItemCategoryName]) VALUES (2, N'Pendings')
INSERT [dbo].[ItemCategory] ([ItemCategoryID], [ItemCategoryName]) VALUES (3, N'Work')
INSERT [dbo].[ItemCategory] ([ItemCategoryID], [ItemCategoryName]) VALUES (4, N'Groseries')
INSERT [dbo].[ItemCategory] ([ItemCategoryID], [ItemCategoryName]) VALUES (5, N'Things to do before christmas.')
INSERT [dbo].[ItemCategory] ([ItemCategoryID], [ItemCategoryName]) VALUES (6, N'To do before session 3.')
INSERT [dbo].[ItemCategory] ([ItemCategoryID], [ItemCategoryName]) VALUES (7, N'Starwars')
INSERT [dbo].[ItemCategory] ([ItemCategoryID], [ItemCategoryName]) VALUES (8, N'test')
INSERT [dbo].[ItemCategory] ([ItemCategoryID], [ItemCategoryName]) VALUES (9, N'Phase 3')
SET IDENTITY_INSERT [dbo].[ItemCategory] OFF
SET IDENTITY_INSERT [dbo].[ListStatus] ON 

INSERT [dbo].[ListStatus] ([ListStatusID], [StatusName]) VALUES (1, N'Created')
INSERT [dbo].[ListStatus] ([ListStatusID], [StatusName]) VALUES (2, N'In progress')
INSERT [dbo].[ListStatus] ([ListStatusID], [StatusName]) VALUES (3, N'Completed')
SET IDENTITY_INSERT [dbo].[ListStatus] OFF
SET IDENTITY_INSERT [dbo].[ListType] ON 

INSERT [dbo].[ListType] ([ListTypeID], [TypeName]) VALUES (1, N'checklist')
INSERT [dbo].[ListType] ([ListTypeID], [TypeName]) VALUES (2, N'groseries')
SET IDENTITY_INSERT [dbo].[ListType] OFF
SET IDENTITY_INSERT [dbo].[Retail] ON 

INSERT [dbo].[Retail] ([RetailID], [Name], [Address], [Description], [Telephone]) VALUES (1, N'SaveonFoods', N'4721 Gladstone St', N'Groseries shopping all over Canada', N'7788882243')
INSERT [dbo].[Retail] ([RetailID], [Name], [Address], [Description], [Telephone]) VALUES (2, N'Shoppers', N'Burrard St', N'Drugstore and retail', N'7788882243')
INSERT [dbo].[Retail] ([RetailID], [Name], [Address], [Description], [Telephone]) VALUES (3, N'88 Supermarket', N'Victoria Dr and Kinsgway St', N'Only the best asian supermarket in Vancouver', N'7788882243')
SET IDENTITY_INSERT [dbo].[Retail] OFF
SET IDENTITY_INSERT [dbo].[SuscriberGroup] ON 

INSERT [dbo].[SuscriberGroup] ([SuscriberGroupID], [SuscriberGroupName], [UserID]) VALUES (1, N'Family', N'6f74b845-02a4-46cd-a26e-076da0ca3663')
INSERT [dbo].[SuscriberGroup] ([SuscriberGroupID], [SuscriberGroupName], [UserID]) VALUES (2, N'Business', N'6f74b845-02a4-46cd-a26e-076da0ca3663')
INSERT [dbo].[SuscriberGroup] ([SuscriberGroupID], [SuscriberGroupName], [UserID]) VALUES (12, N'Student', N'754f76ef-aebe-4263-ad47-54ceecc2f027')
INSERT [dbo].[SuscriberGroup] ([SuscriberGroupID], [SuscriberGroupName], [UserID]) VALUES (13, N'Family', N'754f76ef-aebe-4263-ad47-54ceecc2f027')
SET IDENTITY_INSERT [dbo].[SuscriberGroup] OFF
SET IDENTITY_INSERT [dbo].[SuscriberGroupUser] ON 

INSERT [dbo].[SuscriberGroupUser] ([SuscriberGroupUserID], [SuscriberGroupID], [UserID], [UserTypeID], [ListUserStatus], [SuscriptionDate]) VALUES (8, 1, N'14aaa334-d3bb-404a-8c92-28f706109ee1', 2, N'Active    ', N'4/7/2017  ')
INSERT [dbo].[SuscriberGroupUser] ([SuscriberGroupUserID], [SuscriberGroupID], [UserID], [UserTypeID], [ListUserStatus], [SuscriptionDate]) VALUES (9, 1, N'209f065f-03fc-4c2f-b83e-9c86b3b78d3c', 2, N'Active    ', N'4/7/2017  ')
INSERT [dbo].[SuscriberGroupUser] ([SuscriberGroupUserID], [SuscriberGroupID], [UserID], [UserTypeID], [ListUserStatus], [SuscriptionDate]) VALUES (11, 2, N'60863fc2-9747-4e4b-8be9-491d5da207b6', 2, N'Active    ', N'4/7/2017  ')
INSERT [dbo].[SuscriberGroupUser] ([SuscriberGroupUserID], [SuscriberGroupID], [UserID], [UserTypeID], [ListUserStatus], [SuscriptionDate]) VALUES (13, 12, N'209f065f-03fc-4c2f-b83e-9c86b3b78d3c', 2, N'Active    ', N'4/7/2017  ')
INSERT [dbo].[SuscriberGroupUser] ([SuscriberGroupUserID], [SuscriberGroupID], [UserID], [UserTypeID], [ListUserStatus], [SuscriptionDate]) VALUES (14, 13, N'4f8bab33-d49a-49f9-811d-8d3df7d38533', 3, N'Active    ', N'4/7/2017  ')
SET IDENTITY_INSERT [dbo].[SuscriberGroupUser] OFF
INSERT [dbo].[User] ([UserID], [UserName], [FirstName], [LastName], [BirthDate], [Email], [Password], [ActiveUser], [ProfilePic]) VALUES (N'14aaa334-d3bb-404a-8c92-28f706109ee1', N'xavier', N'', N'', CAST(0x5B950A00 AS Date), N'xmercadoca@gmail.com', N'', 1, NULL)
INSERT [dbo].[User] ([UserID], [UserName], [FirstName], [LastName], [BirthDate], [Email], [Password], [ActiveUser], [ProfilePic]) VALUES (N'209f065f-03fc-4c2f-b83e-9c86b3b78d3c', N'XavierMercado', N'', N'', CAST(0x5B950A00 AS Date), N'ing.fxmm@gmail.com', N'', 1, NULL)
INSERT [dbo].[User] ([UserID], [UserName], [FirstName], [LastName], [BirthDate], [Email], [Password], [ActiveUser], [ProfilePic]) VALUES (N'21cbe0e4-0b8e-434e-955a-4b2a81f3c5fb', N'Victoria', N'', N'', CAST(0x5B950A00 AS Date), N'orotenxillo@gmail.com', N'', 1, NULL)
INSERT [dbo].[User] ([UserID], [UserName], [FirstName], [LastName], [BirthDate], [Email], [Password], [ActiveUser], [ProfilePic]) VALUES (N'3141e321-1df5-495d-b492-5275a9c888ad', N'rain', N'', N'', CAST(0x5B950A00 AS Date), N'rainl@sfu.ca', N'', 1, NULL)
INSERT [dbo].[User] ([UserID], [UserName], [FirstName], [LastName], [BirthDate], [Email], [Password], [ActiveUser], [ProfilePic]) VALUES (N'35fd4cd6-1594-4454-aa56-d105effb34be', N'Nick', N'', N'', CAST(0x5B950A00 AS Date), N'rainliu1991@gmail.com', N'', 1, NULL)
INSERT [dbo].[User] ([UserID], [UserName], [FirstName], [LastName], [BirthDate], [Email], [Password], [ActiveUser], [ProfilePic]) VALUES (N'43262f2d-b2a6-400a-877c-e50ccbfdb095', N'rain', N'', N'', CAST(0x5B950A00 AS Date), N'rainl@sfu.ca', N'', 1, NULL)
INSERT [dbo].[User] ([UserID], [UserName], [FirstName], [LastName], [BirthDate], [Email], [Password], [ActiveUser], [ProfilePic]) VALUES (N'4f8bab33-d49a-49f9-811d-8d3df7d38533', N'rain', N'', N'', CAST(0x5B950A00 AS Date), N'rainl@sfu.ca', N'', 1, NULL)
INSERT [dbo].[User] ([UserID], [UserName], [FirstName], [LastName], [BirthDate], [Email], [Password], [ActiveUser], [ProfilePic]) VALUES (N'607f5266-5e5c-48e2-a107-f9cc0e0a9a84', N'Uriel', N'', N'', CAST(0x5B950A00 AS Date), N'l.uriel.mf@gmail.com', N'', 1, NULL)
INSERT [dbo].[User] ([UserID], [UserName], [FirstName], [LastName], [BirthDate], [Email], [Password], [ActiveUser], [ProfilePic]) VALUES (N'60863fc2-9747-4e4b-8be9-491d5da207b6', N'eileen', N'', N'', CAST(0x5B950A00 AS Date), N'eileenwang.1025@gmail.com', N'', 1, NULL)
INSERT [dbo].[User] ([UserID], [UserName], [FirstName], [LastName], [BirthDate], [Email], [Password], [ActiveUser], [ProfilePic]) VALUES (N'6f74b845-02a4-46cd-a26e-076da0ca3663', N'user', N'Alfred', N'Writerrr', CAST(0x5B950A00 AS Date), N'max_asky@yahoo.com', N'', 1, 0x)
INSERT [dbo].[User] ([UserID], [UserName], [FirstName], [LastName], [BirthDate], [Email], [Password], [ActiveUser], [ProfilePic]) VALUES (N'754f76ef-aebe-4263-ad47-54ceecc2f027', N'Max', N'max', N'wu', CAST(0x5B950A00 AS Date), N'maxsiwu@gmail.com', N'', 1, NULL)
INSERT [dbo].[User] ([UserID], [UserName], [FirstName], [LastName], [BirthDate], [Email], [Password], [ActiveUser], [ProfilePic]) VALUES (N'8da946a9-7463-46b2-867a-44dba7bd846d', N'rain', N'', N'', CAST(0x5B950A00 AS Date), N'rainl@sfu.ca', N'', 1, NULL)
INSERT [dbo].[User] ([UserID], [UserName], [FirstName], [LastName], [BirthDate], [Email], [Password], [ActiveUser], [ProfilePic]) VALUES (N'c647e456-8439-4e5c-963b-019bf397c541', N'Pat', N'', N'', CAST(0x5B950A00 AS Date), N'pmcgee@bcit.ca', N'', 1, NULL)
SET IDENTITY_INSERT [dbo].[UserType] ON 

INSERT [dbo].[UserType] ([UserTypeID], [UserTypeName]) VALUES (1, N'List Publisher')
INSERT [dbo].[UserType] ([UserTypeID], [UserTypeName]) VALUES (2, N'List Collaborator')
INSERT [dbo].[UserType] ([UserTypeID], [UserTypeName]) VALUES (3, N'List Subscriber')
SET IDENTITY_INSERT [dbo].[UserType] OFF
SET ANSI_PADDING ON

GO
/****** Object:  Index [RoleNameIndex]    Script Date: 4/7/2017 12:54:00 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 4/7/2017 12:54:00 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 4/7/2017 12:54:00 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_RoleId]    Script Date: 4/7/2017 12:54:00 PM ******/
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 4/7/2017 12:54:00 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserNameIndex]    Script Date: 4/7/2017 12:54:00 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Coupon]  WITH CHECK ADD  CONSTRAINT [FK_Coupon_Retail] FOREIGN KEY([RetailID])
REFERENCES [dbo].[Retail] ([RetailID])
GO
ALTER TABLE [dbo].[Coupon] CHECK CONSTRAINT [FK_Coupon_Retail]
GO
ALTER TABLE [dbo].[Item]  WITH CHECK ADD  CONSTRAINT [FK_Item_Item] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Item] CHECK CONSTRAINT [FK_Item_Item]
GO
ALTER TABLE [dbo].[Item]  WITH CHECK ADD  CONSTRAINT [FK_Item_ItemCategory] FOREIGN KEY([ItemCategory])
REFERENCES [dbo].[ItemCategory] ([ItemCategoryID])
GO
ALTER TABLE [dbo].[Item] CHECK CONSTRAINT [FK_Item_ItemCategory]
GO
ALTER TABLE [dbo].[List]  WITH CHECK ADD  CONSTRAINT [FK_List_List] FOREIGN KEY([ListTypeID])
REFERENCES [dbo].[ListType] ([ListTypeID])
GO
ALTER TABLE [dbo].[List] CHECK CONSTRAINT [FK_List_List]
GO
ALTER TABLE [dbo].[List]  WITH CHECK ADD  CONSTRAINT [FK_List_ListStatus] FOREIGN KEY([ListStatusID])
REFERENCES [dbo].[ListStatus] ([ListStatusID])
GO
ALTER TABLE [dbo].[List] CHECK CONSTRAINT [FK_List_ListStatus]
GO
ALTER TABLE [dbo].[List]  WITH CHECK ADD  CONSTRAINT [FK_List_User] FOREIGN KEY([CreatorID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[List] CHECK CONSTRAINT [FK_List_User]
GO
ALTER TABLE [dbo].[ListItem]  WITH CHECK ADD  CONSTRAINT [FK_ListItem_Item] FOREIGN KEY([ItemID])
REFERENCES [dbo].[Item] ([ItemID])
GO
ALTER TABLE [dbo].[ListItem] CHECK CONSTRAINT [FK_ListItem_Item]
GO
ALTER TABLE [dbo].[ListItem]  WITH CHECK ADD  CONSTRAINT [FK_ListItem_List] FOREIGN KEY([ListID])
REFERENCES [dbo].[List] ([ListID])
GO
ALTER TABLE [dbo].[ListItem] CHECK CONSTRAINT [FK_ListItem_List]
GO
ALTER TABLE [dbo].[ListUser]  WITH CHECK ADD  CONSTRAINT [FK_ListUser_List] FOREIGN KEY([ListID])
REFERENCES [dbo].[List] ([ListID])
GO
ALTER TABLE [dbo].[ListUser] CHECK CONSTRAINT [FK_ListUser_List]
GO
ALTER TABLE [dbo].[ListUser]  WITH CHECK ADD  CONSTRAINT [FK_ListUser_SuscriberGroup] FOREIGN KEY([SuscriberGroupID])
REFERENCES [dbo].[SuscriberGroup] ([SuscriberGroupID])
GO
ALTER TABLE [dbo].[ListUser] CHECK CONSTRAINT [FK_ListUser_SuscriberGroup]
GO
ALTER TABLE [dbo].[SuscriberGroup]  WITH CHECK ADD  CONSTRAINT [FK_SuscriberGroup_OwnerUser] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[SuscriberGroup] CHECK CONSTRAINT [FK_SuscriberGroup_OwnerUser]
GO
ALTER TABLE [dbo].[SuscriberGroupUser]  WITH CHECK ADD  CONSTRAINT [FK_Suscribergroupuser_GroupID] FOREIGN KEY([SuscriberGroupID])
REFERENCES [dbo].[SuscriberGroup] ([SuscriberGroupID])
GO
ALTER TABLE [dbo].[SuscriberGroupUser] CHECK CONSTRAINT [FK_Suscribergroupuser_GroupID]
GO
ALTER TABLE [dbo].[SuscriberGroupUser]  WITH CHECK ADD  CONSTRAINT [FK_Suscribergroupuser_UserID] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[SuscriberGroupUser] CHECK CONSTRAINT [FK_Suscribergroupuser_UserID]
GO
USE [master]
GO
ALTER DATABASE [DB_110660_onelistca] SET  READ_WRITE 
GO