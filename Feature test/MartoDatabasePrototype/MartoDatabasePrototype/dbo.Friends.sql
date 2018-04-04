CREATE TABLE [dbo].[Friends] (
    [Id]       INT           NOT NULL,
    [Name]     NVARCHAR (50) NOT NULL,
    [Username] NVARCHAR (50) NOT NULL,
    [Tag]      NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

