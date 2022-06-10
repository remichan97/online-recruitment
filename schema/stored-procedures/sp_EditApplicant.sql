CREATE procedure [dbo].[sp_EditApplicant] (@id int, @name varchar(50), @phonenumber varchar(50), @email nvarchar(25), @departmentId int, @status int) as
begin
	if exists (select * from Applicant where Email like concat('%',@email,'%') and Id != @id)
	begin
		raiserror('Someone else already has the given email address.', 11, 1)
	end
	else
	begin
		update Applicant set [Name] = @name, PhoneNumber = @phonenumber, Email = @email, DepartmentId = @departmentId, [Status] = @status where Id = @id
	end
end
GO
