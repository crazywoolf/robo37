CREATE TABLE [dbo].[Courses] (
    [CourseID]    INT             IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (100)  NOT NULL,
    [Teacher]     NVARCHAR (100)  NOT NULL,
    [Description] NVARCHAR (500)  NOT NULL,
    [Platform]    NVARCHAR (50)   NOT NULL,
	[Genre]       NVARCHAR (50)   NOT NULL,
    [Price]       DECIMAL (16, 2) NOT NULL,
    PRIMARY KEY CLUSTERED ([CourseID] ASC)
);

