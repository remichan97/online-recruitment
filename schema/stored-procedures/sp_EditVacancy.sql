CREATE procedure [dbo].[sp_EditVacancy] (@id int, @name varchar(100), @description text, @closingdate date, @quantity int, @departmentID int, @status int) as
begin
declare @createdate date
select @createdate = CreatedDate from Vacancy where Id = @id
	if exists (select * from Vacancy where [Name] = @name and Id != @id and [status] = 1 or [status] = 2)
	begin
		raiserror('There is already a vacancy that currently open or suspended existed with the given name.', 11, 1)
	end
	else if (datediff(dd, @closingdate, @createdate) >= 0)
	begin
		raiserror('You cannot set the closing date in the past. Did you mean to close the vacancy?', 12, 1)
	end
	begin
		update Vacancy set [Name] = @name, [Description] = @description, ClosingDate = @closingdate, Quantity = @quantity, DepartmentId = @departmentID, [Status] = @status where Id = @id
	end
end
GO
