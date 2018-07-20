USE [MMApp]
GO

/****** Object:  Table [dbo].[Music_SongWriter]    Script Date: 2018/07/20 6:41:31 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Music_SongWriter](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SongId] [int] NOT NULL,
	[MusicianId] [int] NOT NULL,
 CONSTRAINT [PK_Music_SongWriter] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Music_SongWriter]  WITH CHECK ADD  CONSTRAINT [FK_Music_SongWriter_Music_Musician] FOREIGN KEY([MusicianId])
REFERENCES [dbo].[Music_Musician] ([Id])
GO

ALTER TABLE [dbo].[Music_SongWriter] CHECK CONSTRAINT [FK_Music_SongWriter_Music_Musician]
GO

ALTER TABLE [dbo].[Music_SongWriter]  WITH CHECK ADD  CONSTRAINT [FK_Music_SongWriter_Music_Song] FOREIGN KEY([SongId])
REFERENCES [dbo].[Music_Song] ([Id])
GO

ALTER TABLE [dbo].[Music_SongWriter] CHECK CONSTRAINT [FK_Music_SongWriter_Music_Song]
GO


