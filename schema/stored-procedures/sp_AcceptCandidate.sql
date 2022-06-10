create procedure [dbo].[sp_AcceptCandidate] (@id int, @applicantId int, @vacancyId int) as
begin
	update Interview set [Status] = 2 where Id = @id
	update Applicant set [Status] = 1 where Id = @applicantId
	update Vacancy set AppliedQuantity = AppliedQuantity + 1 where Id = @vacancyId
end
GO
