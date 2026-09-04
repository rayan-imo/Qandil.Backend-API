IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [DiagnosisQuestions] (
    [Id] uniqueidentifier NOT NULL,
    [CardName] nvarchar(max) NULL,
    [MainTitle] nvarchar(max) NULL,
    [SubTitle] nvarchar(max) NOT NULL,
    [QuestionText] nvarchar(max) NOT NULL,
    [Type] int NOT NULL,
    [Options] nvarchar(max) NULL,
    [Order] int NOT NULL,
    [DeletedAt] datetime2 NULL,
    [CreatedAt] datetime2 NULL,
    CONSTRAINT [PK_DiagnosisQuestions] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Disabilities] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [DeletedAt] datetime2 NULL,
    [CreatedAt] datetime2 NULL,
    CONSTRAINT [PK_Disabilities] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Employees] (
    [Id] uniqueidentifier NOT NULL,
    [FirstName] nvarchar(max) NOT NULL,
    [LastName] nvarchar(max) NOT NULL,
    [Age] int NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [Specicality] nvarchar(max) NULL,
    [DeletedAt] datetime2 NULL,
    [CreatedAt] datetime2 NULL,
    CONSTRAINT [PK_Employees] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Programs] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [SessionNumber] int NOT NULL,
    [SessionDuration] nvarchar(max) NOT NULL,
    [DeletedAt] datetime2 NULL,
    [CreatedAt] datetime2 NULL,
    CONSTRAINT [PK_Programs] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Schools] (
    [Id] uniqueidentifier NOT NULL,
    [SchoolName] nvarchar(max) NOT NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [Address] nvarchar(max) NOT NULL,
    [PrincipalName] nvarchar(max) NULL,
    [Notes] nvarchar(max) NULL,
    [DeletedAt] datetime2 NULL,
    [CreatedAt] datetime2 NULL,
    CONSTRAINT [PK_Schools] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Subject] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [DeletedAt] datetime2 NULL,
    [CreatedAt] datetime2 NULL,
    CONSTRAINT [PK_Subject] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [UserOtp] (
    [Id] uniqueidentifier NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [Code] nvarchar(max) NOT NULL,
    [IsUsed] bit NOT NULL,
    [ExpireDate] datetime2 NOT NULL,
    [DeletedAt] datetime2 NULL,
    [CreatedAt] datetime2 NULL,
    CONSTRAINT [PK_UserOtp] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Users] (
    [Id] uniqueidentifier NOT NULL,
    [Email] nvarchar(max) NULL,
    [Password] nvarchar(max) NULL,
    [Role] int NOT NULL,
    [DeletedAt] datetime2 NULL,
    [CreatedAt] datetime2 NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Levels] (
    [Id] uniqueidentifier NOT NULL,
    [LevelName] nvarchar(max) NOT NULL,
    [ProgramId] uniqueidentifier NULL,
    [DeletedAt] datetime2 NULL,
    [CreatedAt] datetime2 NULL,
    CONSTRAINT [PK_Levels] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Levels_Programs_ProgramId] FOREIGN KEY ([ProgramId]) REFERENCES [Programs] ([Id])
);
GO

CREATE TABLE [Tests] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Title] nvarchar(max) NOT NULL,
    [HasPreTest] bit NOT NULL,
    [LevelId] uniqueidentifier NOT NULL,
    [EmployeeId] uniqueidentifier NULL,
    [DeletedAt] datetime2 NULL,
    [CreatedAt] datetime2 NULL,
    CONSTRAINT [PK_Tests] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Tests_Employees_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [Employees] ([Id]),
    CONSTRAINT [FK_Tests_Levels_LevelId] FOREIGN KEY ([LevelId]) REFERENCES [Levels] ([Id])
);
GO

CREATE TABLE [TestSubjects] (
    [Id] uniqueidentifier NOT NULL,
    [TestId] uniqueidentifier NOT NULL,
    [SubjectId] uniqueidentifier NOT NULL,
    [MaxMark] float NOT NULL,
    [DeletedAt] datetime2 NULL,
    [CreatedAt] datetime2 NULL,
    CONSTRAINT [PK_TestSubjects] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TestSubjects_Subject_SubjectId] FOREIGN KEY ([SubjectId]) REFERENCES [Subject] ([Id]),
    CONSTRAINT [FK_TestSubjects_Tests_TestId] FOREIGN KEY ([TestId]) REFERENCES [Tests] ([Id])
);
GO

CREATE TABLE [Childs] (
    [Id] uniqueidentifier NOT NULL,
    [FirstName] nvarchar(max) NOT NULL,
    [LastName] nvarchar(max) NOT NULL,
    [Gender] int NOT NULL,
    [DateOfBirth] datetime2 NOT NULL,
    [PlaceOfBearth] nvarchar(max) NOT NULL,
    [Address] nvarchar(max) NOT NULL,
    [JoiningDate] datetime2 NOT NULL,
    [IsEnrolledInSchool] bit NOT NULL,
    [SchoolName] nvarchar(max) NULL,
    [SchoolGrade] nvarchar(max) NULL,
    [HasDisability] bit NOT NULL,
    [MotherName] nvarchar(max) NOT NULL,
    [FatherName] nvarchar(max) NOT NULL,
    [FatherJob] nvarchar(max) NULL,
    [MotherJob] nvarchar(max) NULL,
    [FamilyMembers] int NULL,
    [ProgramId] uniqueidentifier NULL,
    [ClassroomId] uniqueidentifier NULL,
    [DeletedAt] datetime2 NULL,
    [CreatedAt] datetime2 NULL,
    CONSTRAINT [PK_Childs] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Childs_Programs_ProgramId] FOREIGN KEY ([ProgramId]) REFERENCES [Programs] ([Id])
);
GO

CREATE TABLE [ChildTests] (
    [Id] uniqueidentifier NOT NULL,
    [Date] datetime2 NOT NULL,
    [Type] int NOT NULL,
    [Result] real NOT NULL,
    [IsPassed] bit NOT NULL,
    [AttemptNumber] int NOT NULL,
    [Nots] nvarchar(max) NULL,
    [EmployeeId] uniqueidentifier NOT NULL,
    [ChildId] uniqueidentifier NOT NULL,
    [TestId] uniqueidentifier NOT NULL,
    [DeletedAt] datetime2 NULL,
    [CreatedAt] datetime2 NULL,
    CONSTRAINT [PK_ChildTests] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ChildTests_Childs_ChildId] FOREIGN KEY ([ChildId]) REFERENCES [Childs] ([Id]),
    CONSTRAINT [FK_ChildTests_Employees_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [Employees] ([Id]),
    CONSTRAINT [FK_ChildTests_Tests_TestId] FOREIGN KEY ([TestId]) REFERENCES [Tests] ([Id])
);
GO

CREATE TABLE [Diagnoses] (
    [Id] uniqueidentifier NOT NULL,
    [DisabilityOnsetDate] datetime2 NOT NULL,
    [MedicalNots] nvarchar(max) NULL,
    [EmployeeId] uniqueidentifier NOT NULL,
    [ChildId] uniqueidentifier NOT NULL,
    [DeletedAt] datetime2 NULL,
    [CreatedAt] datetime2 NULL,
    CONSTRAINT [PK_Diagnoses] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Diagnoses_Childs_ChildId] FOREIGN KEY ([ChildId]) REFERENCES [Childs] ([Id]),
    CONSTRAINT [FK_Diagnoses_Employees_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [Employees] ([Id])
);
GO

CREATE TABLE [SupportivSessions] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [SessionDuration] nvarchar(max) NOT NULL,
    [ChildId] uniqueidentifier NULL,
    [EmployeeId] uniqueidentifier NULL,
    [DeletedAt] datetime2 NULL,
    [CreatedAt] datetime2 NULL,
    CONSTRAINT [PK_SupportivSessions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SupportivSessions_Childs_ChildId] FOREIGN KEY ([ChildId]) REFERENCES [Childs] ([Id]),
    CONSTRAINT [FK_SupportivSessions_Employees_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [Employees] ([Id])
);
GO

CREATE TABLE [Trackings] (
    [Id] uniqueidentifier NOT NULL,
    [StartDate] datetime2 NOT NULL,
    [EndDate] datetime2 NOT NULL,
    [IsCurrentSchool] bit NOT NULL,
    [ChildId] uniqueidentifier NULL,
    [SchoolId] uniqueidentifier NULL,
    [DeletedAt] datetime2 NULL,
    [CreatedAt] datetime2 NULL,
    CONSTRAINT [PK_Trackings] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Trackings_Childs_ChildId] FOREIGN KEY ([ChildId]) REFERENCES [Childs] ([Id]),
    CONSTRAINT [FK_Trackings_Schools_SchoolId] FOREIGN KEY ([SchoolId]) REFERENCES [Schools] ([Id])
);
GO

CREATE TABLE [ChildTestSubjectMarks] (
    [Id] uniqueidentifier NOT NULL,
    [ObtainMark] real NOT NULL,
    [Notes] nvarchar(max) NULL,
    [ChildTestId] uniqueidentifier NOT NULL,
    [SubjectId] uniqueidentifier NOT NULL,
    [EmployeeId] uniqueidentifier NULL,
    [TestSubjectId] uniqueidentifier NULL,
    [DeletedAt] datetime2 NULL,
    [CreatedAt] datetime2 NULL,
    CONSTRAINT [PK_ChildTestSubjectMarks] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ChildTestSubjectMarks_ChildTests_ChildTestId] FOREIGN KEY ([ChildTestId]) REFERENCES [ChildTests] ([Id]),
    CONSTRAINT [FK_ChildTestSubjectMarks_Employees_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [Employees] ([Id]),
    CONSTRAINT [FK_ChildTestSubjectMarks_Subject_SubjectId] FOREIGN KEY ([SubjectId]) REFERENCES [Subject] ([Id]),
    CONSTRAINT [FK_ChildTestSubjectMarks_TestSubjects_TestSubjectId] FOREIGN KEY ([TestSubjectId]) REFERENCES [TestSubjects] ([Id])
);
GO

CREATE TABLE [Classrooms] (
    [Id] uniqueidentifier NOT NULL,
    [MaxCapacity] int NOT NULL,
    [RoomNumber] int NOT NULL,
    [CurrentCapacity] int NOT NULL,
    [ProgramId] uniqueidentifier NULL,
    [LevelId] uniqueidentifier NULL,
    [EmployeeId] uniqueidentifier NULL,
    [ChildTestId] uniqueidentifier NULL,
    [DeletedAt] datetime2 NULL,
    [CreatedAt] datetime2 NULL,
    CONSTRAINT [PK_Classrooms] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Classrooms_ChildTests_ChildTestId] FOREIGN KEY ([ChildTestId]) REFERENCES [ChildTests] ([Id]),
    CONSTRAINT [FK_Classrooms_Employees_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [Employees] ([Id]),
    CONSTRAINT [FK_Classrooms_Levels_LevelId] FOREIGN KEY ([LevelId]) REFERENCES [Levels] ([Id]),
    CONSTRAINT [FK_Classrooms_Programs_ProgramId] FOREIGN KEY ([ProgramId]) REFERENCES [Programs] ([Id])
);
GO

CREATE TABLE [DiagnosisAnswers] (
    [Id] uniqueidentifier NOT NULL,
    [BooleanValue] bit NULL,
    [ScoreValue] int NULL,
    [TextValue] nvarchar(max) NULL,
    [SelectedOption] nvarchar(max) NULL,
    [Notes] nvarchar(max) NULL,
    [DiagnosisId] uniqueidentifier NOT NULL,
    [QuestionId] uniqueidentifier NOT NULL,
    [DeletedAt] datetime2 NULL,
    [CreatedAt] datetime2 NULL,
    CONSTRAINT [PK_DiagnosisAnswers] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_DiagnosisAnswers_Diagnoses_DiagnosisId] FOREIGN KEY ([DiagnosisId]) REFERENCES [Diagnoses] ([Id]),
    CONSTRAINT [FK_DiagnosisAnswers_DiagnosisQuestions_QuestionId] FOREIGN KEY ([QuestionId]) REFERENCES [DiagnosisQuestions] ([Id])
);
GO

CREATE TABLE [DiagnosisDisabilities] (
    [Id] uniqueidentifier NOT NULL,
    [DiagnosisId] uniqueidentifier NOT NULL,
    [DisabilityId] uniqueidentifier NOT NULL,
    [DeletedAt] datetime2 NULL,
    [CreatedAt] datetime2 NULL,
    CONSTRAINT [PK_DiagnosisDisabilities] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_DiagnosisDisabilities_Diagnoses_DiagnosisId] FOREIGN KEY ([DiagnosisId]) REFERENCES [Diagnoses] ([Id]),
    CONSTRAINT [FK_DiagnosisDisabilities_Disabilities_DisabilityId] FOREIGN KEY ([DisabilityId]) REFERENCES [Disabilities] ([Id])
);
GO

CREATE TABLE [EvaluationCards] (
    [Id] uniqueidentifier NOT NULL,
    [DiagnosisId] uniqueidentifier NOT NULL,
    [CardName] nvarchar(max) NOT NULL,
    [MainTitleScoresJson] nvarchar(max) NOT NULL,
    [TotalScore] int NOT NULL,
    [EvaluationMessage] nvarchar(max) NOT NULL,
    [DeletedAt] datetime2 NULL,
    [CreatedAt] datetime2 NULL,
    CONSTRAINT [PK_EvaluationCards] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_EvaluationCards_Diagnoses_DiagnosisId] FOREIGN KEY ([DiagnosisId]) REFERENCES [Diagnoses] ([Id])
);
GO

CREATE INDEX [IX_Childs_ClassroomId] ON [Childs] ([ClassroomId]);
GO

CREATE INDEX [IX_Childs_ProgramId] ON [Childs] ([ProgramId]);
GO

CREATE INDEX [IX_ChildTests_ChildId] ON [ChildTests] ([ChildId]);
GO

CREATE INDEX [IX_ChildTests_EmployeeId] ON [ChildTests] ([EmployeeId]);
GO

CREATE INDEX [IX_ChildTests_TestId] ON [ChildTests] ([TestId]);
GO

CREATE INDEX [IX_ChildTestSubjectMarks_ChildTestId] ON [ChildTestSubjectMarks] ([ChildTestId]);
GO

CREATE INDEX [IX_ChildTestSubjectMarks_EmployeeId] ON [ChildTestSubjectMarks] ([EmployeeId]);
GO

CREATE INDEX [IX_ChildTestSubjectMarks_SubjectId] ON [ChildTestSubjectMarks] ([SubjectId]);
GO

CREATE INDEX [IX_ChildTestSubjectMarks_TestSubjectId] ON [ChildTestSubjectMarks] ([TestSubjectId]);
GO

CREATE INDEX [IX_Classrooms_ChildTestId] ON [Classrooms] ([ChildTestId]);
GO

CREATE INDEX [IX_Classrooms_EmployeeId] ON [Classrooms] ([EmployeeId]);
GO

CREATE INDEX [IX_Classrooms_LevelId] ON [Classrooms] ([LevelId]);
GO

CREATE INDEX [IX_Classrooms_ProgramId] ON [Classrooms] ([ProgramId]);
GO

CREATE UNIQUE INDEX [IX_Diagnoses_ChildId] ON [Diagnoses] ([ChildId]);
GO

CREATE INDEX [IX_Diagnoses_EmployeeId] ON [Diagnoses] ([EmployeeId]);
GO

CREATE INDEX [IX_DiagnosisAnswers_DiagnosisId] ON [DiagnosisAnswers] ([DiagnosisId]);
GO

CREATE INDEX [IX_DiagnosisAnswers_QuestionId] ON [DiagnosisAnswers] ([QuestionId]);
GO

CREATE INDEX [IX_DiagnosisDisabilities_DiagnosisId] ON [DiagnosisDisabilities] ([DiagnosisId]);
GO

CREATE INDEX [IX_DiagnosisDisabilities_DisabilityId] ON [DiagnosisDisabilities] ([DisabilityId]);
GO

CREATE INDEX [IX_EvaluationCards_DiagnosisId] ON [EvaluationCards] ([DiagnosisId]);
GO

CREATE INDEX [IX_Levels_ProgramId] ON [Levels] ([ProgramId]);
GO

CREATE INDEX [IX_SupportivSessions_ChildId] ON [SupportivSessions] ([ChildId]);
GO

CREATE INDEX [IX_SupportivSessions_EmployeeId] ON [SupportivSessions] ([EmployeeId]);
GO

CREATE INDEX [IX_Tests_EmployeeId] ON [Tests] ([EmployeeId]);
GO

CREATE INDEX [IX_Tests_LevelId] ON [Tests] ([LevelId]);
GO

CREATE INDEX [IX_TestSubjects_SubjectId] ON [TestSubjects] ([SubjectId]);
GO

CREATE INDEX [IX_TestSubjects_TestId] ON [TestSubjects] ([TestId]);
GO

CREATE UNIQUE INDEX [IX_Trackings_ChildId] ON [Trackings] ([ChildId]) WHERE [ChildId] IS NOT NULL;
GO

CREATE INDEX [IX_Trackings_SchoolId] ON [Trackings] ([SchoolId]);
GO

ALTER TABLE [Childs] ADD CONSTRAINT [FK_Childs_Classrooms_ClassroomId] FOREIGN KEY ([ClassroomId]) REFERENCES [Classrooms] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260828174013_init', N'8.0.26');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Users] ADD [AdminId] uniqueidentifier NULL;
GO

CREATE INDEX [IX_Users_AdminId] ON [Users] ([AdminId]);
GO

ALTER TABLE [Users] ADD CONSTRAINT [FK_Users_Users_AdminId] FOREIGN KEY ([AdminId]) REFERENCES [Users] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260831202132_AddAdminColumn', N'8.0.26');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260831202917_InitialCreate', N'8.0.26');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DROP TABLE [EvaluationCards];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tests]') AND [c].[name] = N'Title');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Tests] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Tests] DROP COLUMN [Title];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[DiagnosisQuestions]') AND [c].[name] = N'CardName');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [DiagnosisQuestions] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [DiagnosisQuestions] DROP COLUMN [CardName];
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[DiagnosisQuestions]') AND [c].[name] = N'Options');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [DiagnosisQuestions] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [DiagnosisQuestions] DROP COLUMN [Options];
GO

ALTER TABLE [DiagnosisQuestions] ADD [CardType] int NULL;
GO

ALTER TABLE [DiagnosisQuestions] ADD [MaxValue] int NULL;
GO

ALTER TABLE [DiagnosisQuestions] ADD [MinValue] int NULL;
GO

ALTER TABLE [DiagnosisQuestions] ADD [ScoreInputType] int NULL;
GO

CREATE TABLE [QuestionOption] (
    [Id] uniqueidentifier NOT NULL,
    [Text] nvarchar(max) NOT NULL,
    [Value] int NULL,
    [Order] int NOT NULL,
    [DiagnosisQuestionId] uniqueidentifier NOT NULL,
    [DeletedAt] datetime2 NULL,
    [CreatedAt] datetime2 NULL,
    CONSTRAINT [PK_QuestionOption] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_QuestionOption_DiagnosisQuestions_DiagnosisQuestionId] FOREIGN KEY ([DiagnosisQuestionId]) REFERENCES [DiagnosisQuestions] ([Id])
);
GO

CREATE INDEX [IX_QuestionOption_DiagnosisQuestionId] ON [QuestionOption] ([DiagnosisQuestionId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260904135329_init2', N'8.0.26');
GO

COMMIT;
GO

