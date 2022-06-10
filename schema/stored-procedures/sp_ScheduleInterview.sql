create procedure [dbo].[sp_ScheduleInterview] (@id int,@interviewdate date, @starttime time, @endtime time, @applicantId int) as
begin
	update Interview set InterviewDate = @interviewdate, StartTime = @starttime, EndTime = @endtime, [Status] = 1 where Id = 1
	update Applicant set [Status] = 2 where Id = @applicantId
end
GO
