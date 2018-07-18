USE [MMApp]
GO

/****** Object:  Table [dbo].[Books_BookAuthor]    Script Date: 2018/07/18 7:02:33 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Books_BookAuthor](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BookId] [int] NOT NULL,
	[AuthorId] [int] NOT NULL,
 CONSTRAINT [PK_Books_BookAuthor] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Books_BookAuthor]  WITH CHECK ADD  CONSTRAINT [FK_Books_BookAuthor_Books_Author] FOREIGN KEY([AuthorId])
REFERENCES [dbo].[Books_Author] ([Id])
GO

ALTER TABLE [dbo].[Books_BookAuthor] CHECK CONSTRAINT [FK_Books_BookAuthor_Books_Author]
GO

ALTER TABLE [dbo].[Books_BookAuthor]  WITH CHECK ADD  CONSTRAINT [FK_Books_BookAuthor_Books_Book] FOREIGN KEY([BookId])
REFERENCES [dbo].[Books_Book] ([Id])
GO

ALTER TABLE [dbo].[Books_BookAuthor] CHECK CONSTRAINT [FK_Books_BookAuthor_Books_Book]
GO


