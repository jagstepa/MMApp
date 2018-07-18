USE [MMApp]
GO

/****** Object:  Table [dbo].[Music_MusicianInstrument]    Script Date: 2018/07/18 7:07:32 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Music_MusicianInstrument](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MusicianId] [int] NOT NULL,
	[InstrumentId] [int] NOT NULL,
 CONSTRAINT [PK_Music_MusicianInstrument] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Music_MusicianInstrument]  WITH CHECK ADD  CONSTRAINT [FK_Music_MusicianInstrument_Music_Instrument] FOREIGN KEY([InstrumentId])
REFERENCES [dbo].[Music_Instrument] ([Id])
GO

ALTER TABLE [dbo].[Music_MusicianInstrument] CHECK CONSTRAINT [FK_Music_MusicianInstrument_Music_Instrument]
GO

ALTER TABLE [dbo].[Music_MusicianInstrument]  WITH CHECK ADD  CONSTRAINT [FK_Music_MusicianInstrument_Music_Musician] FOREIGN KEY([MusicianId])
REFERENCES [dbo].[Music_Musician] ([Id])
GO

ALTER TABLE [dbo].[Music_MusicianInstrument] CHECK CONSTRAINT [FK_Music_MusicianInstrument_Music_Musician]
GO


