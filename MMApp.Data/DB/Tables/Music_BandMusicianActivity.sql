USE [MMApp]
GO

/****** Object:  Table [dbo].[Music_BandMusicianActivity]    Script Date: 2018/07/18 2:30:35 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Music_BandMusicianActivity](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BandId] [int] NOT NULL,
	[MusicianId] [int] NOT NULL,
	[YearFrom] [int] NOT NULL,
	[YearTo] [int] NULL,
 CONSTRAINT [PK_Music_BandMusicianActivity] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Music_BandMusicianActivity]  WITH CHECK ADD  CONSTRAINT [FK_Music_BandMusicianActivity_Music_Band] FOREIGN KEY([BandId])
REFERENCES [dbo].[Music_Band] ([Id])
GO

ALTER TABLE [dbo].[Music_BandMusicianActivity] CHECK CONSTRAINT [FK_Music_BandMusicianActivity_Music_Band]
GO

ALTER TABLE [dbo].[Music_BandMusicianActivity]  WITH CHECK ADD  CONSTRAINT [FK_Music_BandMusicianActivity_Music_Musician] FOREIGN KEY([MusicianId])
REFERENCES [dbo].[Music_Musician] ([Id])
GO

ALTER TABLE [dbo].[Music_BandMusicianActivity] CHECK CONSTRAINT [FK_Music_BandMusicianActivity_Music_Musician]
GO


