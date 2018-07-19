USE [MMApp]
GO

/****** Object:  Table [dbo].[Music_BandAlbum]    Script Date: 2018/07/18 2:29:04 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Music_BandAlbum](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AlbumId] [int] NOT NULL,
	[BandId] [int] NOT NULL,
 CONSTRAINT [PK_Music_BandAlbum] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Music_BandAlbum]  WITH CHECK ADD  CONSTRAINT [FK_Music_BandAlbum_Music_Album] FOREIGN KEY([AlbumId])
REFERENCES [dbo].[Music_Album] ([Id])
GO

ALTER TABLE [dbo].[Music_BandAlbum] CHECK CONSTRAINT [FK_Music_BandAlbum_Music_Album]
GO

ALTER TABLE [dbo].[Music_BandAlbum]  WITH CHECK ADD  CONSTRAINT [FK_Music_BandAlbum_Music_Band] FOREIGN KEY([BandId])
REFERENCES [dbo].[Music_Band] ([Id])
GO

ALTER TABLE [dbo].[Music_BandAlbum] CHECK CONSTRAINT [FK_Music_BandAlbum_Music_Band]
GO


