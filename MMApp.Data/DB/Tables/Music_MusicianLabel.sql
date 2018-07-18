USE [MMApp]
GO

/****** Object:  Table [dbo].[Music_MusicianLabel]    Script Date: 2018/07/18 7:08:04 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Music_MusicianLabel](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MusicianId] [int] NOT NULL,
	[LabelId] [int] NOT NULL,
 CONSTRAINT [PK_Music_MusicianLabel] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Music_MusicianLabel]  WITH CHECK ADD  CONSTRAINT [FK_Music_MusicianLabel_Music_Label] FOREIGN KEY([LabelId])
REFERENCES [dbo].[Music_Label] ([Id])
GO

ALTER TABLE [dbo].[Music_MusicianLabel] CHECK CONSTRAINT [FK_Music_MusicianLabel_Music_Label]
GO

ALTER TABLE [dbo].[Music_MusicianLabel]  WITH CHECK ADD  CONSTRAINT [FK_Music_MusicianLabel_Music_Musician] FOREIGN KEY([MusicianId])
REFERENCES [dbo].[Music_Musician] ([Id])
GO

ALTER TABLE [dbo].[Music_MusicianLabel] CHECK CONSTRAINT [FK_Music_MusicianLabel_Music_Musician]
GO


