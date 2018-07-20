USE [MMApp]
GO

/****** Object:  Table [dbo].[Books_BookSeller]    Script Date: 2018/07/20 7:29:24 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Books_BookSeller](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BookId] [int] NOT NULL,
	[SellerId] [int] NOT NULL,
	[CurrencyId] [int] NOT NULL,
	[BookPrice] [decimal](18, 0) NOT NULL,
	[FormatId] [int] NOT NULL,
 CONSTRAINT [PK_Books_BookSeller] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Books_BookSeller]  WITH CHECK ADD  CONSTRAINT [FK_Books_BookSeller_Books_Book] FOREIGN KEY([BookId])
REFERENCES [dbo].[Books_Book] ([Id])
GO

ALTER TABLE [dbo].[Books_BookSeller] CHECK CONSTRAINT [FK_Books_BookSeller_Books_Book]
GO

ALTER TABLE [dbo].[Books_BookSeller]  WITH CHECK ADD  CONSTRAINT [FK_Books_BookSeller_Books_Currency] FOREIGN KEY([CurrencyId])
REFERENCES [dbo].[Books_Currency] ([Id])
GO

ALTER TABLE [dbo].[Books_BookSeller] CHECK CONSTRAINT [FK_Books_BookSeller_Books_Currency]
GO

ALTER TABLE [dbo].[Books_BookSeller]  WITH CHECK ADD  CONSTRAINT [FK_Books_BookSeller_Books_Format] FOREIGN KEY([FormatId])
REFERENCES [dbo].[Books_Format] ([Id])
GO

ALTER TABLE [dbo].[Books_BookSeller] CHECK CONSTRAINT [FK_Books_BookSeller_Books_Format]
GO

ALTER TABLE [dbo].[Books_BookSeller]  WITH CHECK ADD  CONSTRAINT [FK_Books_BookSeller_Books_Seller] FOREIGN KEY([SellerId])
REFERENCES [dbo].[Books_Seller] ([Id])
GO

ALTER TABLE [dbo].[Books_BookSeller] CHECK CONSTRAINT [FK_Books_BookSeller_Books_Seller]
GO


