USE [MMApp]
GO

/****** Object:  Table [dbo].[Books_BookWebsite]    Script Date: 2018/07/20 7:09:40 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Books_BookWebsite](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BookId] [int] NOT NULL,
	[WebsiteId] [int] NOT NULL,
 CONSTRAINT [PK_Books_BookWebsite] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Books_BookWebsite]  WITH CHECK ADD  CONSTRAINT [FK_Books_BookWebsite_Books_Book] FOREIGN KEY([BookId])
REFERENCES [dbo].[Books_Book] ([Id])
GO

ALTER TABLE [dbo].[Books_BookWebsite] CHECK CONSTRAINT [FK_Books_BookWebsite_Books_Book]
GO

ALTER TABLE [dbo].[Books_BookWebsite]  WITH CHECK ADD  CONSTRAINT [FK_Books_BookWebsite_Books_Website] FOREIGN KEY([WebsiteId])
REFERENCES [dbo].[Books_Website] ([Id])
GO

ALTER TABLE [dbo].[Books_BookWebsite] CHECK CONSTRAINT [FK_Books_BookWebsite_Books_Website]
GO


