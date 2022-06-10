create table Roles (
	Id int primary key identity,
	[Name] varchar(25) not null
)

insert into Roles values ('Admin'), ('HR Group'), ('Interviewer')

create table Department (
	Id int primary key identity,
	[Name] nvarchar(100) not null
)

insert into Department values ('Management'), ('Java Developer'), ('Frontend Developer'), ('Python Developer'), ('C# Developer')

create table Employee (
	Id int primary key identity,
	[Username] varchar(30) not null,
	[Password] nvarchar(64) not null,
	DepartmentId int not null,
	FullName nvarchar(100) not null,
	Email nvarchar(25) not null unique,
	PhoneNumber varchar(50) not null,
	RoleId int not null,
	foreign key (DepartmentId) references Department(Id),
	foreign key (RoleId) references Roles(Id),
)

create table VacancyStatus (
	Id int primary key identity,
	[Name] varchar(25),
)

insert into VacancyStatus values ('Open'), ('Suspended'), ('Closed')

create table Vacancy (
	Id int primary key identity,
	[Name] varchar(100) not null,
	[Description] text not null,
	CreatedDate date not null default getdate(),
	ClosingDate date,
	Quantity int not null,
	EmployeeId int not null,
	[Status] int not null default 1,
	foreign key (EmployeeId) references Employee(Id),
	foreign key ([Status]) references VacancyStatus(Id) 
)

create table ApplicantStatus (
	Id int primary key identity,
	[Name] varchar(50) not null
)

insert into ApplicantStatus values ('Hired'), ('Vacancy Attached'), ('No Vacant'), ('Banned')

create table Applicant (
	Id int primary key identity,
	[Name] nvarchar(50) not null,
	PhoneNumber varchar(50) not null,
	Email nvarchar(25) not null unique,
	CvFile varchar(255) not null,
	[Status] int not null default 3,
	DepartmentId int not null,
	foreign key (DepartmentId) references Department(Id),
	foreign key ([Status]) references ApplicantStatus(Id)
)

create table InterviewStatus (
	Id int primary key identity,
	[Name] varchar(25),
)

insert into InterviewStatus values ('Interview Scheduled'), ('Selected'), ('Rejected'), ('Waiting for Interviewer')

create table Interview (
	ApplicantId int not null,
	EmployeeId int not null,
	VacancyId int not null,
	AttachedDate date not null default getdate(),
	InterviewDate date,
	StartTime time,
	EndTime time,
	[Status] int not null default 4,
	foreign key (ApplicantId) references Applicant(Id),
	foreign key (EmployeeId) references Employee(Id),
	foreign key (VacancyId) references Vacancy(Id),
)