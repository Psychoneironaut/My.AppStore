CREATE TABLE [dbo].[MyUsers] (
    [ID]        INT            IDENTITY (1, 1) NOT NULL,
    [FirstName] NVARCHAR (100) NULL,
    [LastName]  NVARCHAR (100) NULL,
    [Email]     NVARCHAR (100) NULL,
    [Birthday]  DATE           NULL,
    CONSTRAINT [PK_MyUsers] PRIMARY KEY CLUSTERED ([ID] ASC)
);

