USE [MMApp]
GO

/****** Object:  UserDefinedTableType [dbo].[ParametersFilter]    Script Date: 2018/07/30 8:25:03 AM ******/
DROP TYPE [dbo].[ParametersFilter]
GO

/****** Object:  UserDefinedTableType [dbo].[ParametersFilter]    Script Date: 2018/07/30 8:25:03 AM ******/
CREATE TYPE [dbo].[ParametersFilter] AS TABLE(
	[ParamType] [varchar](50) NULL,
	[ParamValue] [varchar](max) NULL
)
GO


