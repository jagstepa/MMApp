USE [MMApp]
GO

/****** Object:  Table [dbo].[Books_Book]    Script Date: 2018/07/18 7:01:54 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Books_Book](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BookName] [varchar](50) NOT NULL,
	[ShortDescription] [varchar](50) NULL,
	[BookDescription] [varchar](500) NULL,
	[ISBN] [varchar](50) NULL,
	[Year] [int] NULL,
	[Pages] [int] NULL,
	[FileSize] [varchar](50) NULL,
	[FileFormat] [varchar](10) NULL,
	[Website] [varchar](50) NULL,
	[BookPicture] [varchar](50) NULL,
	[PublisherId] [int] NOT NULL,
 CONSTRAINT [PK_Books_Book] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Books_Book]  WITH CHECK ADD  CONSTRAINT [FK_Books_Book_Books_Publisher] FOREIGN KEY([PublisherId])
REFERENCES [dbo].[Books_Publisher] ([Id])
GO

ALTER TABLE [dbo].[Books_Book] CHECK CONSTRAINT [FK_Books_Book_Books_Publisher]
GO


