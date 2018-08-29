select * from Memberships
select * from Roles
select * from UsersInRoles

INSERT INTO Roles VALUES (NEWID(),'7AA5B024-5015-47CE-87C6-D30621382DC8','SecurityGuard','SecurityGuard')
INSERT INTO Roles VALUES (NEWID(),'7AA5B024-5015-47CE-87C6-D30621382DC8','Admin','Admin')
INSERT INTO UsersInRoles VALUES ('1A72BC8E-422E-4BCF-A814-443D0AC2321B','1640A61F-2C56-4B09-970C-C8257DC90B3D')

INSERT INTO EntityType VALUES ('Book')
INSERT INTO EntityType VALUES ('Album')
INSERT INTO EntityType VALUES ('Band')
INSERT INTO EntityType VALUES ('City')
INSERT INTO EntityType VALUES ('Musician')
INSERT INTO EntityType VALUES ('Song')
INSERT INTO EntityType VALUES ('Country')

INSERT INTO EntityRelationType VALUES ('Genre')
INSERT INTO EntityRelationType VALUES ('Label')
INSERT INTO EntityRelationType VALUES ('Instrument')
INSERT INTO EntityRelationType VALUES ('Musician')
INSERT INTO EntityRelationType VALUES ('Writer')
INSERT INTO EntityRelationType VALUES ('Author')
INSERT INTO EntityRelationType VALUES ('Category')
INSERT INTO EntityRelationType VALUES ('Seller')
INSERT INTO EntityRelationType VALUES ('Website')
INSERT INTO EntityRelationType VALUES ('Producer')
INSERT INTO EntityRelationType VALUES ('Song')
INSERT INTO EntityRelationType VALUES ('MusicianActivity')
INSERT INTO EntityRelationType VALUES ('City')

