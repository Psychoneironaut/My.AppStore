CREATE TABLE [dbo].[Addresses] (
    [ID]       INT            IDENTITY (1, 1) NOT NULL,
    [Address1] NVARCHAR (100) NULL,
    [Address2] NVARCHAR (100) NULL,
    [City]     NVARCHAR (100) NULL,
    [State]    NVARCHAR (100) NULL,
    [Zip]  NVARCHAR (12) NULL,
	[Country] NCHAR(100) NULL, 
	[Created] DATETIME NULL DEFAULT GetUtcDate(),
	[Modified] DATETIME NULL DEFAULT GetUtcDate(),
    CONSTRAINT [PK_Addresses] PRIMARY KEY ([ID] ASC)
)

