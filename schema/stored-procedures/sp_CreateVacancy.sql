CREATE procedure [dbo].[sp_CreateVacancy] (@name varchar(100), @description text, @closingdate date, @quantity int, @employeeId int, @departmentID int) as
begin
	if exists (select * from Vacancy where [Name] = @name and [status] = 1 or [status] = 2)
	begin
		raiserror('There is already a vacancy that currently open or suspended existed with the given name.', 11, 1)
	end
	else
	begin
		insert into Vacancy ([Name], [Description], [ClosingDate], Quantity, EmployeeId, DepartmentId) values (@name, @description, @closingdate, @quantity, @employeeId, @departmentID)
	end
end
GO
