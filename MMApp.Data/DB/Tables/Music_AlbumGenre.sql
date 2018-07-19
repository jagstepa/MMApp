USE [MMApp]
GO

/****** Object:  Table [dbo].[Music_AlbumGenre]    Script Date: 2018/07/18 2:25:53 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Music_AlbumGenre](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AlbumId] [int] NOT NULL,
	[GenreId] [int] NOT NULL,
 CONSTRAINT [PK_Music_AlbumGenre] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Music_AlbumGenre]  WITH CHECK ADD  CONSTRAINT [FK_Music_AlbumGenre_Music_Album] FOREIGN KEY([AlbumId])
REFERENCES [dbo].[Music_Album] ([Id])
GO

ALTER TABLE [dbo].[Music_AlbumGenre] CHECK CONSTRAINT [FK_Music_AlbumGenre_Music_Album]
GO

ALTER TABLE [dbo].[Music_AlbumGenre]  WITH CHECK ADD  CONSTRAINT [FK_Music_AlbumGenre_Music_Genre] FOREIGN KEY([GenreId])
REFERENCES [dbo].[Music_Genre] ([Id])
GO

ALTER TABLE [dbo].[Music_AlbumGenre] CHECK CONSTRAINT [FK_Music_AlbumGenre_Music_Genre]
GO


