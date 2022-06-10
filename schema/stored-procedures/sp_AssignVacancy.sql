CREATE procedure [dbo].[sp_AssignVacancy] (@applicantID int, @vacancyId int, @employeeId int) as
begin
if exists (select * from Interview where ApplicantId = @applicantID)
begin
	raiserror('This Applicant already have a vacancy assigned to them.', 11 ,1)
end
else
begin
	update Applicant set [Status] = 1 where Id = @applicantID
	insert into Interview (ApplicantId, EmployeeId, VacancyId, AttachedDate) values (@applicantID, @employeeId, @vacancyId, getdate())
	end
end
GO
