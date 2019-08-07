
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server Compact Edition
-- --------------------------------------------------
-- Date Created: 08/07/2019 13:13:59
-- Generated from EDMX file: C:\Users\Dan\OneDrive\GitHub\VS2017\KBR\ESB2\ESB2.Database\EntityDataModelESB2.edmx
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- NOTE: if the constraint does not exist, an ignorable error will be reported.
-- --------------------------------------------------

    ALTER TABLE [ExceptionEventLog] DROP CONSTRAINT [FK_ApplicationExceptionInnerException];
GO
    ALTER TABLE [EquipmentGroupings] DROP CONSTRAINT [FK_SystemGroupEquipmentGroup];
GO
    ALTER TABLE [EquipmentListing] DROP CONSTRAINT [FK_EquipmentGroupEquipment];
GO
    ALTER TABLE [Users_UserLogin] DROP CONSTRAINT [FK_UserLogin_inherits_User];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- NOTE: if the table does not exist, an ignorable error will be reported.
-- --------------------------------------------------

    DROP TABLE [Users];
GO
    DROP TABLE [ExceptionEventLog];
GO
    DROP TABLE [EventLogLog];
GO
    DROP TABLE [SystemGroupings];
GO
    DROP TABLE [EquipmentGroupings];
GO
    DROP TABLE [EquipmentListing];
GO
    DROP TABLE [Users_UserLogin];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Users'
CREATE TABLE [Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Username] nvarchar(4000)  NOT NULL,
    [Firstname] nvarchar(4000)  NOT NULL,
    [Lastname] nvarchar(4000)  NOT NULL,
    [UserPermissions] int  NOT NULL
);
GO

-- Creating table 'ExceptionEventLog'
CREATE TABLE [ExceptionEventLog] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Timestamp] datetime  NOT NULL,
    [Message] nvarchar(4000)  NOT NULL,
    [Source] nvarchar(4000)  NOT NULL,
    [StackTrace] nvarchar(4000)  NOT NULL,
    [IsInnerException] bit  NOT NULL,
    [ApplicationExceptionInnerException_ApplicationException1_Id] int  NULL
);
GO

-- Creating table 'EventLogLog'
CREATE TABLE [EventLogLog] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Timestamp] datetime  NOT NULL,
    [User] nvarchar(4000)  NOT NULL,
    [Event] int  NOT NULL
);
GO

-- Creating table 'SystemGroupings'
CREATE TABLE [SystemGroupings] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nomenclature] nvarchar(4000)  NOT NULL,
    [Description] nvarchar(4000)  NOT NULL
);
GO

-- Creating table 'EquipmentGroupings'
CREATE TABLE [EquipmentGroupings] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(4000)  NOT NULL,
    [Description] nvarchar(4000)  NOT NULL,
    [SystemGroupId] int  NOT NULL
);
GO

-- Creating table 'EquipmentListing'
CREATE TABLE [EquipmentListing] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nomenclature] nvarchar(4000)  NOT NULL,
    [Description] nvarchar(4000)  NOT NULL,
    [EquipmentStatus] int  NOT NULL,
    [OperationalStatus] int  NOT NULL,
    [EquipmentGroupId] int  NOT NULL
);
GO

-- Creating table 'Users_UserLogin'
CREATE TABLE [Users_UserLogin] (
    [Password] varbinary(8000)  NOT NULL,
    [LastLogin] datetime  NOT NULL,
    [FailedLoginTimestamp] datetime  NOT NULL,
    [FailedLoginCount] int  NOT NULL,
    [AccountLocked] bit  NOT NULL,
    [UserMustChangePassword] bit  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY ([Id] );
GO

-- Creating primary key on [Id] in table 'ExceptionEventLog'
ALTER TABLE [ExceptionEventLog]
ADD CONSTRAINT [PK_ExceptionEventLog]
    PRIMARY KEY ([Id] );
GO

-- Creating primary key on [Id] in table 'EventLogLog'
ALTER TABLE [EventLogLog]
ADD CONSTRAINT [PK_EventLogLog]
    PRIMARY KEY ([Id] );
GO

-- Creating primary key on [Id] in table 'SystemGroupings'
ALTER TABLE [SystemGroupings]
ADD CONSTRAINT [PK_SystemGroupings]
    PRIMARY KEY ([Id] );
GO

-- Creating primary key on [Id] in table 'EquipmentGroupings'
ALTER TABLE [EquipmentGroupings]
ADD CONSTRAINT [PK_EquipmentGroupings]
    PRIMARY KEY ([Id] );
GO

-- Creating primary key on [Id] in table 'EquipmentListing'
ALTER TABLE [EquipmentListing]
ADD CONSTRAINT [PK_EquipmentListing]
    PRIMARY KEY ([Id] );
GO

-- Creating primary key on [Id] in table 'Users_UserLogin'
ALTER TABLE [Users_UserLogin]
ADD CONSTRAINT [PK_Users_UserLogin]
    PRIMARY KEY ([Id] );
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ApplicationExceptionInnerException_ApplicationException1_Id] in table 'ExceptionEventLog'
ALTER TABLE [ExceptionEventLog]
ADD CONSTRAINT [FK_ApplicationExceptionInnerException]
    FOREIGN KEY ([ApplicationExceptionInnerException_ApplicationException1_Id])
    REFERENCES [ExceptionEventLog]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ApplicationExceptionInnerException'
CREATE INDEX [IX_FK_ApplicationExceptionInnerException]
ON [ExceptionEventLog]
    ([ApplicationExceptionInnerException_ApplicationException1_Id]);
GO

-- Creating foreign key on [SystemGroupId] in table 'EquipmentGroupings'
ALTER TABLE [EquipmentGroupings]
ADD CONSTRAINT [FK_SystemGroupEquipmentGroup]
    FOREIGN KEY ([SystemGroupId])
    REFERENCES [SystemGroupings]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SystemGroupEquipmentGroup'
CREATE INDEX [IX_FK_SystemGroupEquipmentGroup]
ON [EquipmentGroupings]
    ([SystemGroupId]);
GO

-- Creating foreign key on [EquipmentGroupId] in table 'EquipmentListing'
ALTER TABLE [EquipmentListing]
ADD CONSTRAINT [FK_EquipmentGroupEquipment]
    FOREIGN KEY ([EquipmentGroupId])
    REFERENCES [EquipmentGroupings]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EquipmentGroupEquipment'
CREATE INDEX [IX_FK_EquipmentGroupEquipment]
ON [EquipmentListing]
    ([EquipmentGroupId]);
GO

-- Creating foreign key on [Id] in table 'Users_UserLogin'
ALTER TABLE [Users_UserLogin]
ADD CONSTRAINT [FK_UserLogin_inherits_User]
    FOREIGN KEY ([Id])
    REFERENCES [Users]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------