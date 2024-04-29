CREATE DATABASE ProjectName
GO 

USE ProjectName

CREATE SCHEMA TablesDB;
GO

CREATE TABLE TablesDB.TBComputer(
    ComputerId INT IDENTITY(1,1) PRIMARY KEY,
    Motherboard NVARCHAR(50),
    CPUCores INT,
    ReleaseDate DATE,
    Price DECIMAL(18,4),
    
);