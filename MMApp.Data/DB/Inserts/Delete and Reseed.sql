/*
SELECT *
FROM Music_Country
INSERT INTO Music_Country (CountryName,Website) VALUES ('UK','Test')
*/

DELETE FROM Music_Country

DBCC CHECKIDENT ('Music_Country', RESEED, 0)

