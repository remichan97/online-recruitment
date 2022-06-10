create procedure [dbo].[sp_CreateApplicant] (@name nvarchar(50), @phonenumber varchar(50), @email nvarchar(25), @departmentId int, @cvfile varchar(255)) as
begin
	if exists (select * from Applicant where [Email] like concat('%', @email, '%'))
	begin
		raiserror('Someone else already have this email. You cannot use the email again for different person', 11, 1)
	end
	else
	begin
		insert into Applicant ([Name], PhoneNumber, Email, DepartmentId, CvFile) values (@name, @phonenumber, @email, @departmentId, @cvfile)
	end
end
GO
