USE [MMApp]
GO

/****** Object:  Table [dbo].[Music_AlbumMusician]    Script Date: 2018/07/18 2:27:27 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Music_AlbumMusician](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[AlbumId] [int] NOT NULL,
	[MusicianId] [int] NOT NULL,
 CONSTRAINT [PK_Music_AlbumMusician] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Music_AlbumMusician]  WITH CHECK ADD  CONSTRAINT [FK_Music_AlbumMusician_Music_Album] FOREIGN KEY([AlbumId])
REFERENCES [dbo].[Music_Album] ([Id])
GO

ALTER TABLE [dbo].[Music_AlbumMusician] CHECK CONSTRAINT [FK_Music_AlbumMusician_Music_Album]
GO

ALTER TABLE [dbo].[Music_AlbumMusician]  WITH CHECK ADD  CONSTRAINT [FK_Music_AlbumMusician_Music_Musician] FOREIGN KEY([MusicianId])
REFERENCES [dbo].[Music_Musician] ([Id])
GO

ALTER TABLE [dbo].[Music_AlbumMusician] CHECK CONSTRAINT [FK_Music_AlbumMusician_Music_Musician]
GO


