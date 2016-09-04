-- Creating ExtremeSportDatabase
USE master;
IF DB_ID(N'ExtremeSportDB') IS NOT NULL
	DROP DATABASE ExtremeSportDB;
CREATE DATABASE ExtremeSportDB ON
(
	NAME = ExtremeSportDB,
	-- Change Your path and correct: SELECT filename FROM sys.sysaltfiles;
	FILENAME = 'D:\Programs\SQLServer\MSSQL12.BARTEKSERVER\MSSQL\DATA\ExtremeSportDB.mdf',
	SIZE = 5,
	MAXSIZE = UNLIMITED,
	FILEGROWTH = 1
)
LOG ON
(
	NAME = ExtremeSportDBLog,
	-- Change Your path and correct: SELECT filename FROM sys.sysaltfiles;
	FILENAME = 'D:\Programs\SQLServer\MSSQL12.BARTEKSERVER\MSSQL\DATA\ExtremeSportDBLog.ldf',
	SIZE = 1,
	MAXSIZE = UNLIMITED,
	FILEGROWTH = 10%
);

USE ExtremeSportDB;
IF OBJECT_ID(N'dbo.ServiceDate', N'U') IS NOT NULL
	DROP TABLE dbo.ServiceDate;
IF OBJECT_ID(N'dbo.ServiceToCustomer', N'U') IS NOT NULL
	DROP TABLE dbo.ServiceToCustomer;
IF OBJECT_ID(N'dbo.EmployeeToSportType', N'U') IS NOT NULL
	DROP TABLE dbo.EmployeeToSportType;
-- IF OBJECT_ID(N'dbo.Payment', N'U') IS NOT NULL
-- 		DROP TABLE dbo.Payment;
IF OBJECT_ID(N'dbo.Service', N'U') IS NOT NULL
	DROP TABLE dbo.Service;
IF OBJECT_ID(N'dbo.ServiceType', N'U') IS NOT NULL
	DROP TABLE dbo.ServiceType;
IF OBJECT_ID(N'dbo.Customer', N'U') IS NOT NULL
	DROP TABLE dbo.Customer;
IF OBJECT_ID(N'dbo.Employee', N'U') IS NOT NULL
	DROP TABLE dbo.Employee;
IF OBJECT_ID(N'dbo.SportType', N'U') IS NOT NULL
	DROP TABLE dbo.SportType;
IF OBJECT_ID(N'dbo.Position', N'U') IS NOT NULL
	DROP TABLE dbo.Position;

CREATE TABLE dbo.Customer
(
	id						INT IDENTITY(1, 1)	NOT NULL PRIMARY KEY,
	name					NVARCHAR(100)		NOT NULL,
	surname					NVARCHAR(100)		NOT NULL,
	email					NVARCHAR(100)		NOT	NULL UNIQUE,
	phoneNumber				NVARCHAR(100)			NULL,
	icePhoneNumber			NVARCHAR(100)			NULL,
	personalIdentityNumber	NVARCHAR(100)		NOT	NULL UNIQUE,
	identityCardNumber		NVARCHAR(100)			NULL,
	city					NVARCHAR(100)			NULL,
	street					NVARCHAR(100)			NULL,
	postalCode				NVARCHAR(100)			NULL,
	houseNumber				NVARCHAR(100)			NULL,
	apartmentNumber			NVARCHAR(100)			NULL,
	password				NVARCHAR(4000)		NOT NULL
);

CREATE TABLE dbo.SportType
(
	id						INT IDENTITY(1, 1)	NOT NULL PRIMARY KEY,
	name					NVARCHAR(100)		NOT NULL UNIQUE,
	description				NTEXT					NULL
);

CREATE TABLE dbo.Position
(
	id						INT IDENTITY(1, 1)	NOT NULL PRIMARY KEY,
	name					NVARCHAR(100)		NOT NULL UNIQUE,
	description				NTEXT					NULL,
);

CREATE TABLE dbo.Employee
(
	id						INT IDENTITY(1, 1)	NOT NULL PRIMARY KEY,
	name					NVARCHAR(20)		NOT NULL,
	surname					NVARCHAR(40)		NOT NULL,
	email					NVARCHAR(100)		NOT NULL UNIQUE,
	phoneNumber				NVARCHAR(100)		NOT	NULL,
	icePhoneNumber			NVARCHAR(100)			NULL,
	personalIdentityNumber	NVARCHAR(100)		NOT NULL UNIQUE,
	identityCardNumber		NVARCHAR(100)		NOT	NULL UNIQUE,
	city					NVARCHAR(100)		NOT NULL,
	street					NVARCHAR(100)		NOT NULL,
	postalCode				NVARCHAR(100)		NOT	NULL,
	houseNumber				NVARCHAR(100)		NOT	NULL,
	apartmentNumber			NVARCHAR(100)		NOT	NULL,
	positionID				INT					NOT NULL FOREIGN KEY REFERENCES dbo.Position(id),
	description				NTEXT					NULL,
	supervisorID			INT						NULL FOREIGN KEY REFERENCES dbo.Employee(id),
	password				NVARCHAR(4000)		NOT NULL
);

CREATE TABLE dbo.EmployeeToSportType
(
	employeeID		INT NOT NULL FOREIGN KEY REFERENCES dbo.Employee(id),
	sportTypeID		INT NOT NULL FOREIGN KEY REFERENCES dbo.SportType(id),

	PRIMARY KEY(employeeID, sportTypeID)
);

CREATE TABLE dbo.ServiceType
(
	id					INT IDENTITY(1, 1)	NOT NULL PRIMARY KEY,
	sportTypeID			INT					NOT NULL FOREIGN KEY REFERENCES dbo.SportType(id),
	name				NVARCHAR(100)		NOT NULL UNIQUE,
	description			NTEXT					NULL,
	durationInMinutes	INT					NOT NULL,
	price				SMALLMONEY			NOT NULL,
	isCourse			BIT					NOT NULL			
);

CREATE TABLE dbo.Service
(
	id				INT IDENTITY(1, 1)	NOT NULL PRIMARY KEY,
	serviceTypeID	INT					NOT NULL FOREIGN KEY REFERENCES dbo.ServiceType(id),
	employeeID		INT					NOT NULL FOREIGN KEY REFERENCES dbo.Employee(id)
);

CREATE TABLE dbo.ServiceToCustomer
(
	customerID		INT NOT NULL FOREIGN KEY REFERENCES dbo.Customer(id),
	serviceID		INT NOT NULL FOREIGN KEY REFERENCES dbo.Service(id),

	PRIMARY KEY(customerID, serviceID)
);

CREATE TABLE dbo.ServiceDate
(
	id			INT IDENTITY(1, 1)	NOT NULL PRIMARY KEY,
	date		SMALLDATETIME		NOT NULL,
	serviceID	INT					NOT NULL FOREIGN KEY REFERENCES dbo.Service(id)
);

-- CREATE TABLE dbo.Payment
-- (
-- 	id			INT IDENTITY(1, 1)	NOT NULL PRIMARY KEY,
-- 	customerID	INT					NOT NULL FOREIGN KEY REFERENCES dbo.Customer(id),
-- 	serviceID	INT					NOT NULL FOREIGN KEY REFERENCES dbo.Service(id),
-- 	value		SMALLMONEY			NOT NULL,
-- 	date		SMALLDATETIME		NOT NULL,
-- 	description	NTEXT					NULL
-- )

INSERT INTO dbo.SportType VALUES
	(N'Skoki spadochronowe', N'Skok ze spadochronem'),
	(N'Nurkowanie', NULL),
	(N'Skuter wodny',N'Jazda na skuterze wodnym');

INSERT INTO dbo.ServiceType VALUES
	(1, N'Jednorazowy skok ze spadochronem', NULL, 90, 199.99, 0),
	(2, N'Kurs nurkwania dla początkujących', N'Kurs przeznaczony głównie dla początkujących', 180, 500, 1),
	(3, N'Kurs jazdy na skuterze wodnym dla zaawansowanych', N'Kurs przeznaczony wyłącznie dla doświdczonych klientów', 120, 1099.99, 1),
	(3, N'Jednorazowa jazda na skuterze wodnym', NULL, 150, 349.99, 0);

INSERT INTO dbo.Position VALUES
	(N'Instruktor', N'Instruktor może samodzielnie prowadzić kursy i szkolenia'),
	(N'Admin', N'Administrator aplikacji'),
	(N'Stażysta', N'Stażysta jest podopiecznym instruktora, prowadzi wyłącznie indywidualne zajęcia');

INSERT INTO dbo.Employee VALUES
	(N'Ewa', N'Kowalska', N'ewa.kowalska@hotmail.com', N'+48 609-243-897', N'+48 500-001-002', N'60090909333', N'AUU3322', N'Warszawa', N'Wspólna', 
		N'90-001', N'12/14', N'12', 1, N'Jest doświadczonym pracownikiem', NULL, '2G9IPWRxejXo4zdReLTISFT9z5g7p9oKBOzMrpHztw0='),
	(N'Beata', N'Smolna', N'beata.smolna@buziaczek.com', N'+48 619-223-297', NULL, N'69098989333', 'AGH1222', N'Łódź', N'Hoża', 
		N'91-201', N'212A', N'2', 2, NULL, 1, 'LLpJKoATP7rPV4TR9tmunVhTRMsCu7231+AnreGr/RU=');

INSERT INTO dbo.EmployeeToSportType VALUES
	(1, 1),
	(1, 2),
	(2, 2),
	(2, 3);

INSERT INTO dbo.Service VALUES 
	(1, 1),
	(2, 1),
	(3, 1),
	(4, 1),
	(2, 1);

INSERT INTO dbo.ServiceDate VALUES
	('2015-12-12 13:00:00', 1),
	('2015-12-13 20:30:00', 2),
	('2015-12-14 19:00:00', 2),
	('2015-12-15 17:00:00', 3),
	('2015-12-16 16:00:00', 3),
	('2015-12-17 15:00:00', 3),
	('2015-12-18 14:00:00', 4),
	('2015-12-19 11:00:00', 5),
	('2015-12-20 12:00:00', 5);

INSERT INTO dbo.Customer VALUES
	(N'Ryszard', N'Pietrzykowski', N'r.piet@gmail.com', N'+48 631-243-812', N'+48 599-001-992', N'60090999999', N'EII0000', N'Poznań', N'Marszałkowska', 
		N'99-007', N'12', N'12', N'a'),
	(N'Barbara', N'Nowak', N'nowak@onet.com', N'+48 689-212-812', N'+48 765-001-992', N'55090999199', N'GHB0000', N'Gdańsk', N'Piotrkowska', 
		N'99-117', N'12', N'12', N'a'),
	(N'Adrian', N'Zielinski', N'az@gmail.com', NULL, NULL, N'55190999199', NULL, NULL, NULL, NULL, NULL, NULL, 'a='),
	(N'Korwin', N'Malaszynski', N'km@gmail.com', NULL, NULL, N'55110999199', NULL, NULL, NULL, NULL, NULL, NULL, 'a='),
	(N'Mateusz', N'Kowalski', N'kowal@gmail.com', NULL, NULL, N'55114499199', NULL, NULL, NULL, NULL, NULL, NULL, 'RsUYfCfBgwgairAsmhuxmNeiM//mUZosIInZ/45sSF4=');

INSERT INTO dbo.ServiceToCustomer VALUES
	(1, 1),
	(2, 2),
	(3, 2),
	(4, 3),							
	(5, 3),
	(1, 3),
	(2, 4),
	(3, 4),
	(4, 5),
	(5, 5),
	(1, 5),
	(2, 5);

-- INSERT INTO dbo.Payment VALUES
-- 	(1, 1, 99.99, '2015-12-11 13:00:00', NULL),
-- 	(2, 2, 199.99, '2015-11-12 13:00:00', N'Pierwsza rata'),
-- 	(3, 2, 299.99, '2015-12-13 13:00:00', NULL),
-- 	(4, 3, 399.99, '2015-12-14 13:00:00', N'Cała kwota'),							
-- 	(5, 3, 499.99, '2015-10-12 13:00:00', N'Druga rata'),
-- 	(1, 3, 599.99, '2015-12-18 13:00:00', NULL),
-- 	(2, 4, 699.99, '2015-12-19 13:00:00', NULL),
-- 	(3, 4, 100, '2015-12-12 23:00:00', NULL),
-- 	(4, 5, 209, '2015-12-12 4:00:00', NULL),
-- 	(5, 5, 77.77, '2015-12-11 13:00:00', NULL),
-- 	(1, 5, 112, '2015-12-22 13:00:00', NULL),
-- 	(2, 5, 150, '2016-01-27 13:00:00', NULL),
-- 	(1, 5, 132, '2016-12-22 13:00:00', NULL),
-- 	(2, 5, 171, '2017-01-27 13:00:00', NULL);


/*	SELECT ServiceDate.*, Service.*, ServiceType.*, SportType.*, Employee.*, Position.*, ServiceToCustomer.*, Customer.*
--	, Payment.*
		FROM ServiceDate
		JOIN Service ON Service.id = ServiceDate.serviceID
		JOIN ServiceType ON ServiceType.id = Service.serviceTypeID
		JOIN SportType ON SportType.id = ServiceType.sportTypeID
		JOIN Employee ON Employee.id = Service.employeeID
		JOIN Position ON Position.id = Employee.positionID
		JOIN ServiceToCustomer ON Service.id = ServiceToCustomer.serviceID
		JOIN Customer ON Customer.id = ServiceToCustomer.customerID
--		JOIN Payment ON Payment.customerID = Customer.id 
*/