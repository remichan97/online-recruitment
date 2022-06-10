create procedure [dbo].[sp_banApplicant] (@id int) as
begin
	if exists (select * from Applicant where Id = @id)
	begin
		update Applicant set [Status] = 4 where Id = @id
		update Interview set [Status] = 3 where ApplicantId = @id
	end
end
GO
