USE [MMApp]
GO

/****** Object:  Table [dbo].[Music_MusicianGenre]    Script Date: 2018/07/18 7:07:05 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Music_MusicianGenre](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MusicianId] [int] NOT NULL,
	[GenreId] [int] NOT NULL,
 CONSTRAINT [PK_Music_MusicianGenre] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Music_MusicianGenre]  WITH CHECK ADD  CONSTRAINT [FK_Music_MusicianGenre_Music_Genre] FOREIGN KEY([MusicianId])
REFERENCES [dbo].[Music_Genre] ([Id])
GO

ALTER TABLE [dbo].[Music_MusicianGenre] CHECK CONSTRAINT [FK_Music_MusicianGenre_Music_Genre]
GO

ALTER TABLE [dbo].[Music_MusicianGenre]  WITH CHECK ADD  CONSTRAINT [FK_Music_MusicianGenre_Music_Musician] FOREIGN KEY([MusicianId])
REFERENCES [dbo].[Music_Musician] ([Id])
GO

ALTER TABLE [dbo].[Music_MusicianGenre] CHECK CONSTRAINT [FK_Music_MusicianGenre_Music_Musician]
GO


