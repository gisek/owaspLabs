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
	Name varchar(255),
	Price decimal,
	IsAvailable bit
);
go

insert into Products 
(Name, Price, IsAvailable) 
values 
(
	concat('Product_', CONVERT(varchar(255), NEWID())), 
	CONVERT( DECIMAL(13, 4), 10 + (300-10)*RAND(CHECKSUM(NEWID()))),
	CAST(ROUND(RAND(),0) AS BIT)
);
go 20

create table CreditCards(
	Id int not null identity(1,1) primary key,
	Number varchar(255),
	Cvv varchar(255),
	[Owner] varchar(255)
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
go 20

create procedure GetProducts
	@Id int
as
	SELECT * FROM Products WHERE IsAvailable = 1 AND Id = @Id
go

create procedure GetProductsByName
	@Name varchar(256)
as
	SELECT * FROM Products WHERE IsAvailable = 1 AND Name LIKE '%' + @Name + '%'
go

create procedure GetProductsUnsafe
	@Name varchar(256)
as
begin
	declare @rawSql varchar(256)
	set @rawSql = 'SELECT * FROM Products WHERE IsAvailable = 1 AND Name like ''%' + @Name + '%'''
	exec(@rawSql)
end
go

create procedure GetProductsSecured
	@Name varchar(256)
as
begin
	declare @rawSql nvarchar(256)
	set @rawSql = 'SELECT * FROM Products WHERE IsAvailable = 1 AND Name like ''%'' + @NameLike + ''%'''
	exec sp_executesql @rawSql, N'@NameLike varchar(256)', @NameLike = @Name 
end
go
