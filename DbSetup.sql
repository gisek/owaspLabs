use master;
go

drop login owaspTestLogin;
go

DECLARE @kill varchar(8000) = '';  
SELECT @kill = @kill + 'kill ' + CONVERT(varchar(5), session_id) + ';'  
FROM sys.dm_exec_sessions
WHERE database_id  = db_id('Owasp');
EXEC(@kill);
go

drop database Owasp;
go

---------------------------------

use master;
go

create login owaspTestLogin with password = 'owaspPassword';
go

create database Owasp;
go

use Owasp;
go

create user owaspTestUser for login owaspTestLogin;
exec sp_addrolemember 'db_owner', 'owaspTestUser';
go

create table Products(
	Id int not null identity(1,1) primary key,
	Name text,
	Price decimal,
);
go

insert into Products 
(Name, Price) 
values 
(
	concat('Product_', CONVERT(varchar(255), NEWID())), 
	CONVERT( DECIMAL(13, 4), 10 + (300-10)*RAND(CHECKSUM(NEWID())))
);
go 10

create table CreditCards(
	Id int not null identity(1,1) primary key,
	Number text,
	Cvv text,
	[Owner] text
);
go

insert into CreditCards
(Number, Cvv, [Owner]) 
values 
(
	CONVERT( varchar(255), CAST(1000000000000000 + (1000000000000000)*RAND(CHECKSUM(NEWID())) AS decimal(16,0))),
	CONVERT( varchar(255), CAST(100 + (999-100)*RAND(CHECKSUM(NEWID())) as decimal(3,0))),
	concat('Name_', CONVERT(varchar(255), NEWID()))
);
go 10