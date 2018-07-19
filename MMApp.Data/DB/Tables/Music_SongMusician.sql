USE [MMApp]
GO

/****** Object:  Table [dbo].[Music_SongMusician]    Script Date: 2018/07/18 2:31:44 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Music_SongMusician](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SongId] [int] NOT NULL,
	[MusicianId] [int] NOT NULL,
 CONSTRAINT [PK_Music_SongMusician] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Music_SongMusician]  WITH CHECK ADD  CONSTRAINT [FK_Music_SongMusician_Music_Musician] FOREIGN KEY([MusicianId])
REFERENCES [dbo].[Music_Musician] ([Id])
GO

ALTER TABLE [dbo].[Music_SongMusician] CHECK CONSTRAINT [FK_Music_SongMusician_Music_Musician]
GO

ALTER TABLE [dbo].[Music_SongMusician]  WITH CHECK ADD  CONSTRAINT [FK_Music_SongMusician_Music_Song] FOREIGN KEY([SongId])
REFERENCES [dbo].[Music_Song] ([Id])
GO

ALTER TABLE [dbo].[Music_SongMusician] CHECK CONSTRAINT [FK_Music_SongMusician_Music_Song]
GO


