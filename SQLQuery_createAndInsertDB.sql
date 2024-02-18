CREATE DATABASE MyDb
GO

USE MyDb
GO

CREATE TABLE MyTable(
	Id INT IDENTITY(1,1) NOT NULL,
	[Text] NVARCHAR(50) NOT NULL);
GO

INSERT INTO MyTable
VALUES
('string1'),
('string2'),
('string3'),
('string4'),
('string5');
