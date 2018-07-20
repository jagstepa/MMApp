USE [MMApp]
GO

/****** Object:  Table [dbo].[Music_AlbumProducer]    Script Date: 2018/07/20 6:49:57 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Music_AlbumProducer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AlbumId] [int] NOT NULL,
	[ProducerId] [int] NOT NULL,
 CONSTRAINT [PK_Music_AlbumProducer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Music_AlbumProducer]  WITH CHECK ADD  CONSTRAINT [FK_Music_AlbumProducer_Music_Album] FOREIGN KEY([AlbumId])
REFERENCES [dbo].[Music_Album] ([Id])
GO

ALTER TABLE [dbo].[Music_AlbumProducer] CHECK CONSTRAINT [FK_Music_AlbumProducer_Music_Album]
GO

ALTER TABLE [dbo].[Music_AlbumProducer]  WITH CHECK ADD  CONSTRAINT [FK_Music_AlbumProducer_Music_Producer] FOREIGN KEY([ProducerId])
REFERENCES [dbo].[Music_Producer] ([Id])
GO

ALTER TABLE [dbo].[Music_AlbumProducer] CHECK CONSTRAINT [FK_Music_AlbumProducer_Music_Producer]
GO


