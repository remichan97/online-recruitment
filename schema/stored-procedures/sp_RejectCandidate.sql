create procedure [dbo].[sp_RejectCandidate] (@id int, @applicantId int) as
begin
	update Interview set [Status] = 3 where Id = @id
	update Applicant set [Status] = 3 where Id = @applicantId
end
GO
