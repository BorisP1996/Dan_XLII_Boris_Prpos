--Creating database only if database is not created yet
IF DB_ID('Zadatak_42') IS NULL
CREATE DATABASE Zadatak_42
GO
USE Zadatak_42;

--tables are deleted if they already exist
if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblEmployes')
drop table tblEmployes;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblLocations')
drop table tblLocations;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblGenders')
drop table tblGenders;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblSectors')
drop table tblSectors;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'vwEmploye')
drop view vwEmploye;


--creating table and adding primary key and constraints
create table tblEmployes (
EmployeID int identity(1,1) primary key,
UserName nvarchar (50) not null ,
Surname nvarchar (50) not null,
IdNumber nvarchar(20) not null,
JMBG varchar(13) unique not null, check(LEN(JMBG) = 13 and ISNUMERIC(JMBG) = 1),
DateOfBirth varchar(20) not null,
GenderID int not null,
Number nvarchar(15) not null,
SectorID int not null,
LocationID int not null,
Menager_ID int,
)

create table tblLOCATIONS(
LocationID int identity(1,1) primary key,
Adress nvarchar(50),
Place nvarchar (50),
States nvarchar(50),
)

create table tblSectors(
SectorID int identity(1,1) primary key,
SectorName nvarchar(30) not null
)

Create table tblGenders(
GenderID int identity(1,1) primary key,
Gender nvarchar (10) not null
)

--adding foreign keys, LocationID is primary key in table tblLOCATIONS and foreign key in table tblUSERS
ALTER TABLE tblEmployes
ADD FOREIGN KEY (LocationID) REFERENCES tblLOCATIONS(LocationID);

Alter Table tblEmployes
Add foreign key (GenderID) references tblGenders(GenderID);

ALTER TABLE tblEmployes
ADD FOREIGN KEY (SectorID) REFERENCES tblSectors(SectorID);



use Zadatak_42
GO
CREATE view vwEmploye AS (
SELECT tblSectors.SectorName, tblLOCATIONS.Place, tblEmployes.EmployeID, tblEmployes.Surname, tblEmployes.UserName,
tblEmployes.IdNumber,tblEmployes.JMBG,tblEmployes.DateOfBirth,tblEmployes.GenderID,tblEmployes.Number,tblEmployes.SectorID,tblEmployes.LocationID,tblEmployes.Menager_ID
  FROM tblLocations Inner JOIN tblEmployes
 on tblEmployes.LocationID=tblLocations.LocationID
 Inner JOIN tblSectors on tblSectors.SectorID=tblEmployes.SectorID );
 GO

insert into tblGenders values ('Musko'),('Zensko'),('Ostalo')

