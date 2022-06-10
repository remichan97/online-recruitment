create procedure [dbo].[sp_ChangePassword] (@oldpassword varchar(64), @newpassword varchar(64), @username varchar(30)) as
begin
	if exists (select * from Employee where Username = @username and [Password] = @oldpassword)
	begin 
		update Employee set [Password] = @newpassword where Username = @username
		return 1
	end
	else
		return 0
end
GO
