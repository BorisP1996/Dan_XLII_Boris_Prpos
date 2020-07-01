--Creating database only if database is not created yet
IF DB_ID('Zadatak_24') IS NULL
CREATE DATABASE Zadatak_24
GO
USE Zadatak_24;

--tables are deleted if they already exist
if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblEVIDENTION_REPORTS')
drop table tblEVIDENTION_REPORTS;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblCHARGE_REPORTS')
drop table tblCHARGE_REPORTS;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblUSERS')
drop table tblUSERS;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'vwUser')
drop view vwUser;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblLOCATIONS')
drop table tblLOCATIONS;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblID_CARDS')
drop table tblID_CARDS;

--creating table and adding primary key and constraints
create table tblUSERS (
UserID int identity(1,1) primary key,
UserName nvarchar (50) not null ,
Surname nvarchar (50) not null,
--length is 13 and every element is numeric
JMBG varchar(13) unique not null, check(LEN(JMBG) = 13 and ISNUMERIC(JMBG) = 1),
--date of birth equals todays date minus 16 years
DateOfBirth date not null, check(DateOfBirth<=dateadd(year,(-16),getdate())),
--Gender can only be specified with one letter chosen from these three letters
Gender nvarchar (1) check (Gender in('X','M','Z')) not null,
LocationID int,
IdCard_ID int,
)


create table tblLOCATIONS(
LocationID int identity(1,1) primary key,
Adress nvarchar(50),
Place nvarchar (50),
States nvarchar(50),
)

create table tblEVIDENTION_REPORTS(
EvidentionID int identity(1,1) primary key,
ChargeReportID int ,
UserID int
)

create table tblCHARGE_REPORTS(
ChargeReportID int identity (1,1) primary key,
ReportName nvarchar(50) not null,
Verball bit not null,
)

Create table tblID_CARDS(
IdCard_ID int identity (1,1) primary key,
--RegisterNumber is varchar and is greater than 8 but maximum length is 9
RegisterNumber varchar (9) not null,check (datalength(RegisterNumber)>8),
--date of issue must be 'smaller' than today
DateOfIssue date not null, check(DateOfIssue < GetDate()),
--date of expire must be 'bigger' than date of issue
DateOfExpire date not null,check(DateOfExpire > DateOfIssue),
Issuer nvarchar (50) not null,
)
--adding foreign keys, LocationID is primary key in table tblLOCATIONS and foreign key in table tblUSERS
ALTER TABLE tblUSERS
ADD FOREIGN KEY (LocationID) REFERENCES tblLOCATIONS(LocationID);

ALTER TABLE tblUSERS
ADD FOREIGN KEY (IdCard_ID) REFERENCES tblID_CARDS(IdCard_ID);

ALTER TABLE tblEVIDENTION_REPORTS
ADD FOREIGN KEY (ChargeReportID) REFERENCES tblCHARGE_REPORTS(ChargeReportID);

ALTER TABLE tblEVIDENTION_REPORTS
ADD FOREIGN KEY (UserID) REFERENCES tblUSERS(UserID);

--DOMACI DAN XIV
--2.Inserting data into tables
insert into tblLOCATIONS values ('Kozarska','Prijedor','Republika Srpska'),('Petefi Sandora','Budimpesta','Madjarska'),('Puskinova','Novi Sad','Srbija')

insert into tblID_CARDS values ('123123123','2000-10-10','2010-10-10','MUP RS'),('222333444','2017-05-05','2027-05-05','MUP RS'),('100200300','2019-12-30','2029-12-30','MUP RS')

insert into tblUSERS values ('Petar','Petrovic','2510996164444','1996-10-25','M','1','1'),
('Marko','Maric','0101999160008','1999-01-01','M','2','2'),
('Janko','Jankovic','0202002167775','2000-02-02','M','3','3');

insert into tblCHARGE_REPORTS values ('Pljacka',1),('Pronevjera',0),('Brza Voznja',1)


use Zadatak_24
GO
CREATE view vwUser AS (
SELECT tblID_CARDS.RegisterNumber, tblUSERS.UserName, tblUSERS.Surname, tblLOCATIONS.Adress,
 tblID_CARDS.DateOfIssue, tblID_CARDS.DateOfExpire FROM tblUSERS LEFT JOIN tblID_CARDS
 on tblID_CARDS.IdCard_ID=tblUSERS.IdCard_ID 
 LEFT JOIN tblLOCATIONS on tblLOCATIONS.LocationID = tblUSERS.LocationID);
 GO

