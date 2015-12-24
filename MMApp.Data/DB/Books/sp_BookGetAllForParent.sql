USE MMApp;
GO

IF OBJECT_ID ( 'dbo.sp_BookGetAllForParent', 'P' ) IS NOT NULL
BEGIN
   DROP PROCEDURE dbo.sp_BookGetAllForParent;
END;
GO

SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO

-- ====================================================================================================
-- Author:        Dragan Stevanovic
-- Create Date:   24/11/2015
-- Decription:    Returns all entities for the type for the Parent Entity.
--
-- Modification History:
--
--
-- Example: EXEC dbo.sp_BookGetAllForParent
-- ====================================================================================================

CREATE PROCEDURE dbo.sp_BookGetAllForParent
	@GetAllType NVARCHAR(50),
	@ParentId	INT,
	@SubType NVARCHAR(50)
AS
BEGIN
	IF @GetAllType = 'Author'
	BEGIN
		SELECT A.Id, A.AuthorName FROM Books_Author A
		JOIN Books_BookAuthor BA ON BA.AuthorId = A.Id
		WHERE BA.BookId = @ParentId
		ORDER BY A.AuthorName
	END

	RETURN 0;
END

GO