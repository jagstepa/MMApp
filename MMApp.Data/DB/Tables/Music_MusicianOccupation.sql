USE [MMApp]
GO

/****** Object:  Table [dbo].[Music_MusicianOccupation]    Script Date: 2018/07/18 7:08:33 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Music_MusicianOccupation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MusicianId] [int] NOT NULL,
	[OccupationId] [int] NOT NULL,
 CONSTRAINT [PK_Music_MusicianOccupation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Music_MusicianOccupation]  WITH CHECK ADD  CONSTRAINT [FK_Music_MusicianOccupation_Music_Musician] FOREIGN KEY([MusicianId])
REFERENCES [dbo].[Music_Musician] ([Id])
GO

ALTER TABLE [dbo].[Music_MusicianOccupation] CHECK CONSTRAINT [FK_Music_MusicianOccupation_Music_Musician]
GO

ALTER TABLE [dbo].[Music_MusicianOccupation]  WITH CHECK ADD  CONSTRAINT [FK_Music_MusicianOccupation_Music_Occupation] FOREIGN KEY([OccupationId])
REFERENCES [dbo].[Music_Occupation] ([Id])
GO

ALTER TABLE [dbo].[Music_MusicianOccupation] CHECK CONSTRAINT [FK_Music_MusicianOccupation_Music_Occupation]
GO


