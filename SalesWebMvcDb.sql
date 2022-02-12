USE master
GO
IF EXISTS (SELECT * FROM sys.databases WHERE name = 'SalesWebMvcDb') 
	DROP DATABASE SalesWebMvcDb
GO
CREATE DATABASE SalesWebMvcDb
GO
USE SalesWebMvcDb
GO

SELECT * FROM [SalesWebMvcDb].[dbo].[Department]
SELECT * FROM [SalesWebMvcDb].[dbo].[SalesRecord]
SELECT * FROM [SalesWebMvcDb].[dbo].[Seller]

insert into Seller(Name, Email, Birthdate, BaseSalary, DepartmentId)
values('Bob Brown II', 'bobII@gmail.com', '2000-04-21', 1000.00, 4)


alter procedure groupByDepartment
As
Begin
	select Date, Amount, Status, Seller.Name,Department.Name 'Department'
	from SalesRecord inner join Seller on SellerId = Seller.Id
		inner join Department on DepartmentId = Department.Id
	group by Department.Name, SalesRecord.Date, SalesRecord.Amount, SalesRecord.Status, Seller.Name
End
GO