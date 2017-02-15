--part 1--
USE [master]

GO
/****** Object:  Database [OneListCA]    Script Date: 2/9/2017 1:43:32 PM ******/
IF EXISTS(SELECT * FROM master.sys.databases 
          WHERE name='OneListCA')
BEGIN
	ALTER DATABASE OneListCA SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE OneListCA
END

CREATE DATABASE [OneListCA]

GO

USE [OneListCA]
GO
--part 2--
USE [OneListCA]
GO
/****** Object:  Table [dbo].[Item]    Script Date: 2/9/2017 3:02:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Item](
	[ItemID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[ItemName] [nvarchar](50) NULL,
	[ItemDescription] [nvarchar](50) NULL,
	[ItemCategory] [int] NULL,
 CONSTRAINT [PK_Item_1] PRIMARY KEY CLUSTERED 
(
	[ItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ItemCategory]    Script Date: 2/9/2017 3:02:48 PM ******/
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
/****** Object:  Table [dbo].[List]    Script Date: 2/9/2017 3:02:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[List](
	[ListID] [int] IDENTITY(1,1) NOT NULL,
	[CreatorID] [int] NOT NULL,
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
/****** Object:  Table [dbo].[ListItem]    Script Date: 2/9/2017 3:02:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ListItem](
	[ListID] [int] IDENTITY(1,1) NOT NULL,
	[ItemID] [int] NOT NULL,
	[ListItemSolved] [bit] NOT NULL,
	[ListItemSolver] [int] NOT NULL,
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
/****** Object:  Table [dbo].[ListStatus]    Script Date: 2/9/2017 3:02:48 PM ******/
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
/****** Object:  Table [dbo].[ListType]    Script Date: 2/9/2017 3:02:48 PM ******/
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
/****** Object:  Table [dbo].[ListUser]    Script Date: 2/9/2017 3:02:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ListUser](
	[ListID] [nchar](10) NOT NULL,
	[UserID] [int] NOT NULL,
	[UserTypeID] [int] NOT NULL,
	[ListUserStatus] [nchar](10) NOT NULL,
	[SuscriptionDate] [nchar](10) NOT NULL,
 CONSTRAINT [PK_ListUser] PRIMARY KEY CLUSTERED 
(
	[ListID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 2/9/2017 3:02:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
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
/****** Object:  Table [dbo].[UserType]    Script Date: 2/9/2017 3:02:48 PM ******/
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
ALTER TABLE [dbo].[ListItem]  WITH CHECK ADD  CONSTRAINT [FK_ListItem_User] FOREIGN KEY([ListItemSolver])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[ListItem] CHECK CONSTRAINT [FK_ListItem_User]
GO
ALTER TABLE [dbo].[ListUser]  WITH CHECK ADD  CONSTRAINT [FK_ListUser_ListUser] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[ListUser] CHECK CONSTRAINT [FK_ListUser_ListUser]
GO
ALTER TABLE [dbo].[ListUser]  WITH CHECK ADD  CONSTRAINT [FK_ListUser_UserType] FOREIGN KEY([UserTypeID])
REFERENCES [dbo].[UserType] ([UserTypeID])
GO
ALTER TABLE [dbo].[ListUser] CHECK CONSTRAINT [FK_ListUser_UserType]
GO

--coupon for CORS webAPI
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

ALTER TABLE [dbo].[Coupon]  WITH CHECK ADD  CONSTRAINT [FK_Coupon_Retail] FOREIGN KEY([RetailID])
REFERENCES [dbo].[Retail] ([RetailID])
GO

ALTER TABLE [dbo].[Coupon] CHECK CONSTRAINT [FK_Coupon_Retail]
GO




USE [master]
GO
ALTER DATABASE [OneListCA] SET  READ_WRITE 
GO


--part 3.- creating default and test information

use[OneListCA]
--status
insert into ListStatus(StatusName)values('Created');
insert into ListStatus(StatusName)values('In progress');
insert into ListStatus(StatusName)values('Completed');
--list types
insert into ListType(TypeName)values('checklist');
insert into ListType(TypeName)values('groseries');
--user type
insert into UserType(UserTypeName) values('Administrator');
insert into UserType(UserTypeName) values('List Publisher');
insert into UserType(UserTypeName) values('List Suscriber');
insert into UserType(UserTypeName) values('List Colaborator');
--item category
insert into ItemCategory(ItemCategoryName) values('Things to Do');
insert into ItemCategory(ItemCategoryName) values('Pendings');
insert into ItemCategory(ItemCategoryName) values('Work');
insert into ItemCategory(ItemCategoryName) values('Groseries');
--users
insert into [User](FirstName,LastName,BirthDate,Email,Password,Activeuser,ProfilePic)values('Max','Wu','09/15/1989','max@siwu.com','12345','1','');
insert into [User](FirstName,LastName,BirthDate,Email,Password,Activeuser,ProfilePic)values('Nick','Liu','04/15/1994','nick@liu.com','12345','1','');
insert into [User](FirstName,LastName,BirthDate,Email,Password,Activeuser,ProfilePic)values('Xavier','Mercado','09/30/1989','xavier@mercado.com','12345','1','');
--Lists
insert into List(CreatorID,ListName,ListTypeID,CreationDate,ListStatusID)values(1,'List of things to do',1,GETDATE(),1);
insert into List(CreatorID,ListName,ListTypeID,CreationDate,ListStatusID)values(2,'Shopping List',1,GETDATE(),1);
insert into List(CreatorID,ListName,ListTypeID,CreationDate,ListStatusID)values(3,'Groseries List',2,GETDATE(),1);

--retail examples for the WebAPI
insert into dbo.Retail(Name,Address,Description,Telephone)values('SaveonFoods','4721 Gladstone St','Groseries shopping all over Canada','7788882243')
insert into dbo.Retail(Name,Address,Description,Telephone)values('Shoppers','Burrard St','Drugstore and retail','7788882243')
insert into dbo.Retail(Name,Address,Description,Telephone)values('88 Supermarket','Victoria Dr and Kinsgway St','Only the best asian supermarket in Vancouver','7788882243')

--coupons examples
insert into coupon (Title,Description,DiscountPercentage,RetailID,StartDate,EndingDate) values('Bananas Discount!','Buy all bananas with a 20% off',20,1,'02/14/2017','03/01/2017')
insert into coupon (Title,Description,DiscountPercentage,RetailID,StartDate,EndingDate) values('Ham Magic!','5% off when buying more than 200grms of Ham',5,2,'02/20/2017','03/01/2017')
insert into coupon (Title,Description,DiscountPercentage,RetailID,StartDate,EndingDate) values('Chocolate lovers','all chocolates have 2x1',50,3,'02/14/2017','03/01/2017')