create procedure [dbo].[sp_resetPassword] (@userId int, @pw varchar(64)) as
begin
	update Employee set [Password] = @pw where Id = @userId
end
GO
