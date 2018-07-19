USE [MMApp]
GO

/****** Object:  Table [dbo].[Music_BandGenre]    Script Date: 2018/07/18 2:29:34 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Music_BandGenre](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BandId] [int] NOT NULL,
	[GenreId] [int] NOT NULL,
 CONSTRAINT [PK_Music_BandGenre] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Music_BandGenre]  WITH CHECK ADD  CONSTRAINT [FK_Music_BandGenre_Music_BandGenre] FOREIGN KEY([BandId])
REFERENCES [dbo].[Music_Band] ([Id])
GO

ALTER TABLE [dbo].[Music_BandGenre] CHECK CONSTRAINT [FK_Music_BandGenre_Music_BandGenre]
GO

ALTER TABLE [dbo].[Music_BandGenre]  WITH CHECK ADD  CONSTRAINT [FK_Music_BandGenre_Music_Genre] FOREIGN KEY([GenreId])
REFERENCES [dbo].[Music_Genre] ([Id])
GO

ALTER TABLE [dbo].[Music_BandGenre] CHECK CONSTRAINT [FK_Music_BandGenre_Music_Genre]
GO


