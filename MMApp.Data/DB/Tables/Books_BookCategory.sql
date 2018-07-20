USE [MMApp]
GO

/****** Object:  Table [dbo].[Books_BookCategory]    Script Date: 2018/07/20 6:58:51 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Books_BookCategory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BookId] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
 CONSTRAINT [PK_Books_BookCategory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Books_BookCategory]  WITH CHECK ADD  CONSTRAINT [FK_Books_BookCategory_Books_Book] FOREIGN KEY([BookId])
REFERENCES [dbo].[Books_Book] ([Id])
GO

ALTER TABLE [dbo].[Books_BookCategory] CHECK CONSTRAINT [FK_Books_BookCategory_Books_Book]
GO

ALTER TABLE [dbo].[Books_BookCategory]  WITH CHECK ADD  CONSTRAINT [FK_Books_BookCategory_Books_Category] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Books_Category] ([Id])
GO

ALTER TABLE [dbo].[Books_BookCategory] CHECK CONSTRAINT [FK_Books_BookCategory_Books_Category]
GO


