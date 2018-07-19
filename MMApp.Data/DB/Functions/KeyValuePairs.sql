USE MMApp;
GO

IF OBJECT_ID ( 'dbo.KeyValuePairs') IS NOT NULL
BEGIN
   DROP FUNCTION dbo.KeyValuePairs;
END;
GO

SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO

-- ====================================================================================================
-- Author:        Dragan Stevanovic
-- Create Date:   17/07/2018
-- Decription:    Returns Key Value Pairs.
--
-- Modification History:
--
--
-- Example: SELECT * FROM dbo.sp_BookAddEntity()
-- ====================================================================================================

CREATE FUNCTION dbo.KeyValuePairs( @inputStr VARCHAR(MAX)) 
RETURNS @OutTable TABLE 
	(KeyName VARCHAR(MAX), KeyValue VARCHAR(MAX))
AS
BEGIN

	DECLARE @separator CHAR(1), @keyValueSeperator CHAR(1)
	SET @separator = '@'
	SET @keyValueSeperator = '#'

	DECLARE @separator_position INT , @keyValueSeperatorPosition INT
	DECLARE @match VARCHAR(MAX) 
	
	SET @inputStr = @inputStr + @separator
	
	WHILE PATINDEX('%' + @separator + '%' , @inputStr) <> 0 
	 BEGIN
	  SELECT @separator_position =  PATINDEX('%' + @separator + '%' , @inputStr)
	  SELECT @match = LEFT(@inputStr, @separator_position - 1)
	  IF @match <> '' 
		  BEGIN
            SELECT @keyValueSeperatorPosition = PATINDEX('%' + @keyValueSeperator + '%' , @match)
            IF @keyValueSeperatorPosition <> -1 
              BEGIN
        		INSERT @OutTable
				 VALUES (LEFT(@match,@keyValueSeperatorPosition -1), RIGHT(@match,LEN(@match) - @keyValueSeperatorPosition))
              END
		   END		
 	  SELECT @inputStr = STUFF(@inputStr, 1, @separator_position, '')
	END

	RETURN
END
GO