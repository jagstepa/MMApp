USE [MMApp]
GO

/****** Object:  Table [dbo].[Music_Band]    Script Date: 2018/07/18 10:08:17 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Music_Band](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BandName] [varchar](50) NOT NULL,
	[AlsoKnownAs] [varchar](50) NULL,
	[Website] [varchar](50) NULL,
	[CityId] [int] NOT NULL,
	[CountryId] [int] NOT NULL,
 CONSTRAINT [PK_Music_Band] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Music_Band]  WITH CHECK ADD  CONSTRAINT [FK_Music_Band_Music_City] FOREIGN KEY([CityId])
REFERENCES [dbo].[Music_City] ([Id])
GO

ALTER TABLE [dbo].[Music_Band] CHECK CONSTRAINT [FK_Music_Band_Music_City]
GO

ALTER TABLE [dbo].[Music_Band]  WITH CHECK ADD  CONSTRAINT [FK_Music_Band_Music_Country] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Music_Country] ([Id])
GO

ALTER TABLE [dbo].[Music_Band] CHECK CONSTRAINT [FK_Music_Band_Music_Country]
GO


