CREATE procedure [dbo].[sp_CreateNewEmployee] (@username varchar(30), @password varchar(64), @departmentId int, @fullname nvarchar(100), @email nvarchar(25), @phonenumber varchar(50), @roleId int) as
begin
	if exists (select * from Employee where [Username] like concat('%',@username,'%'))
	begin
		raiserror('Someone else already has this Username. Please use another one', 11, 1)
	end
	begin try
	insert into Employee values (@username, @password, @departmentId, @fullname, @email, @phonenumber, @roleId)
	end try
	begin catch
		raiserror('Someone else already has this Email Address. Please use another one', 11, 1)
	end catch
end
GO
