USE [MMApp]
GO

/****** Object:  Table [dbo].[Music_CountryCity]    Script Date: 2018/07/18 7:04:29 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Music_CountryCity](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CityId] [int] NOT NULL,
	[CountryId] [int] NOT NULL,
 CONSTRAINT [PK_Music_CountryCity] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Music_CountryCity]  WITH CHECK ADD  CONSTRAINT [FK_Music_CountryCity_Music_City] FOREIGN KEY([CityId])
REFERENCES [dbo].[Music_City] ([Id])
GO

ALTER TABLE [dbo].[Music_CountryCity] CHECK CONSTRAINT [FK_Music_CountryCity_Music_City]
GO

ALTER TABLE [dbo].[Music_CountryCity]  WITH CHECK ADD  CONSTRAINT [FK_Music_CountryCity_Music_Country] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Music_Country] ([Id])
GO

ALTER TABLE [dbo].[Music_CountryCity] CHECK CONSTRAINT [FK_Music_CountryCity_Music_Country]
GO


