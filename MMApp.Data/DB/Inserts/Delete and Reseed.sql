SELECT *
FROM Music_Country

DELETE FROM Music_Country

DBCC CHECKIDENT ('Music_Country', RESEED, 0)