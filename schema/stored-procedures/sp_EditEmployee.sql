CREATE procedure [dbo].[sp_EditEmployee] (@userid int, @departmentId int, @fullname nvarchar(100), @email nvarchar(25), @phonenumber varchar(50), @roleId int) as
begin
	if exists (select * from Employee where Email like concat('%', @email, '%') and Id != @userid)
	begin
		raiserror('Someone else already has this Email Address! Please use another one!', 11,1)
	end
	else
	begin
		update Employee set DepartmentId = @departmentId, FullName = @fullname, Email = @email, PhoneNumber = @phonenumber where Id = @userid
	end
end
GO
