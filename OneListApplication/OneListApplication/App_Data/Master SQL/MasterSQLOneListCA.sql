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
/****** Object:  Table [dbo].[Item]    Script Date: 2/9/2017 1:43:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Item](
	[ItemID] [int] NOT NULL,
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
/****** Object:  Table [dbo].[ItemCategory]    Script Date: 2/9/2017 1:43:32 PM ******/
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
/****** Object:  Table [dbo].[List]    Script Date: 2/9/2017 1:43:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[List](
	[ListID] [int] NOT NULL,
	[CreatorID] [int] NOT NULL,
	[ListName] [nchar](10) NOT NULL,
	[ListTypeID] [int] NOT NULL,
	[CreationDate] [date] NOT NULL,
	[ListStatusID] [int] NOT NULL,
 CONSTRAINT [PK_List_1] PRIMARY KEY CLUSTERED 
(
	[ListID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ListItem]    Script Date: 2/9/2017 1:43:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ListItem](
	[ListID] [int] NOT NULL,
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
/****** Object:  Table [dbo].[ListStatus]    Script Date: 2/9/2017 1:43:33 PM ******/
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
/****** Object:  Table [dbo].[ListType]    Script Date: 2/9/2017 1:43:33 PM ******/
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
/****** Object:  Table [dbo].[ListUser]    Script Date: 2/9/2017 1:43:33 PM ******/
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
/****** Object:  Table [dbo].[User]    Script Date: 2/9/2017 1:43:33 PM ******/
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
/****** Object:  Table [dbo].[UserType]    Script Date: 2/9/2017 1:43:33 PM ******/
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
USE [master]
GO
ALTER DATABASE [OneListCA] SET  READ_WRITE 
GO
--part 3.- creating default and test information
