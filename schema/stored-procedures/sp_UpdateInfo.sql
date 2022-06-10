CREATE procedure [dbo].[sp_UpdateInfo] (@fullname nvarchar(100), @email nvarchar(25), @phonenumber varchar(50), @username varchar(30)) as
begin
	if exists (select * from Employee where Email = @email and Username	not like @username)
	begin
		return 1
	end
	else
	begin
		update Employee set FullName = @fullname, Email = @email, PhoneNumber = @phonenumber where Username = @username
		return 0
	end
end
GO
