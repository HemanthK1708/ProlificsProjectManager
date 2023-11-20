-- Creating the Database
CREATE DATABASE ProlificsProjectManager

-- Using the Database
USE ProlificsProjectManager

--------------------------------------------------------------------------------------------------------
													-- Project Table --
--------------------------------------------------------------------------------------------------------
-- Creating Project Table
CREATE TABLE Project
(
ProjectId INT NOT NULL,
ProjectName VARCHAR(MAX) NOT NULL,
StartDate DATETIME NOT NULL,
EndDate DATETIME NOT NULL,
ProjectCreatedOn DATETIME DEFAULT GETDATE(),
ProjectModifiedOn DATETIME DEFAULT GETDATE()

CONSTRAINT PK_ProjectId PRIMARY KEY (ProjectID) -- Creating Primary Key Constraint
);

INSERT INTO Project (ProjectId, ProjectName, StartDate, EndDate) VALUES (1, 'PPM', '2023-10-11', '2024-10-11'); 

SELECT * FROM Project
UPDATE Project SET ProjectId = 1 WHERE ProjectId = 2

-- Retrieving data from the table.
SELECT * FROM Project

--------------------------------------------------------------------------------------------------------
													-- Role Table --
--------------------------------------------------------------------------------------------------------
-- Creating Role Table
CREATE TABLE Role
(
RoleId INT NOT NULL,
RoleName VARCHAR(MAX) NOT NULL,
RoleCreatedOn DATETIME DEFAULT GETDATE(),
RoleModifiedOn DATETIME DEFAULT GETDATE()

CONSTRAINT PK_RoleId PRIMARY KEY (RoleId) -- Creating Primary Key constraint
);

-- Retrieving data from the table.
SELECT * FROM Role

--------------------------------------------------------------------------------------------------------
													-- Employee Table --
--------------------------------------------------------------------------------------------------------
-- Creating Employee Table
CREATE TABLE Employee
(
EmployeeId INT NOT NULL,
FirstName VARCHAR(MAX) NOT NULL,
LastName VARCHAR(MAX) NOT NULL,
Email NVARCHAR(255) UNIQUE NOT NULL,
Mobile BIGINT NOT NULL,
Address NVARCHAR(MAX) NOT NULL,
RoleId INT NOT NULL,
EmployeeCreatedOn DATETIME DEFAULT GETDATE(),
EmployeeModifiedOn DATETIME DEFAULT GETDATE()

CONSTRAINT PK_EmployeeId PRIMARY KEY(EmployeeId), -- Creating Primary Key Constraint

CONSTRAINT FK_RoleId FOREIGN KEY(RoleId) REFERENCES Role -- Creating Foreign Key Constraint
);

-- Retrieving data from the table.
SELECT * FROM Employee

--DROP TABLE Employee

--------------------------------------------------------------------------------------------------------
											-- Employee To Project Table --
--------------------------------------------------------------------------------------------------------
-- Creating EmployeeToProject table
CREATE TABLE EmployeeProject
(
ProjectId INT NOT NULL,
ProjectName INT NOT NULL,
EmployeeId INT NOT NULL,
FirstName VARCHAR(MAX) NOT NULL,
RoleId INT NOT NULL,

EmployeeProjectCreatedOn DATETIME DEFAULT GETDATE(),
EmployeeProjectModifiedOn DATETIME DEFAULT GETDATE()


CONSTRAINT PK_Employee_ProjectID PRIMARY KEY (ProjectId, EmployeeId), -- Creating Primary Key Constraint

CONSTRAINT FK_ProjectID FOREIGN KEY (ProjectId) REFERENCES Project, -- Creating Foreign Key Constraint
CONSTRAINT FK_EmployeeID FOREIGN KEY (EmployeeId) REFERENCES Employee, -- Creating Foreign Key Constraint
CONSTRAINT FK_RoleIDD FOREIGN KEY (RoleId) REFERENCES Role -- Creating Foreign Key Constraint
);

-- Retrieving data from the table.
SELECT * FROM EmployeeProject

--DROP TABLE EmployeeProject

--------------------------------------------------------------------------------------------------------
											-- Project Modified Trigger --
--------------------------------------------------------------------------------------------------------

-- Creating the trigger
CREATE TRIGGER Project_ModifiedON ON Project
FOR UPDATE
AS
DECLARE @ProjectId INT
SELECT @ProjectId = i.ProjectId FROM INSERTED i
BEGIN
UPDATE Project SET ProjectModifiedOn = GETDATE() WHERE ProjectId = @ProjectId
END
PRINT 'Project_ModifiedON Trigger fired'


--------------------------------------------------------------------------------------------------------
											-- Role Modified Trigger --
--------------------------------------------------------------------------------------------------------

CREATE TRIGGER Role_ModifiedON ON Role
FOR UPDATE
AS
DECLARE @RoleId INT
SELECT @RoleId = i.RoleId FROM INSERTED i
BEGIN
UPDATE Role SET RoleModifiedOn = GETDATE() WHERE RoleId = @RoleId
END
PRINT 'Role_ModifiedON Trigger fired'

--------------------------------------------------------------------------------------------------------
											-- Employee Modified Trigger --
--------------------------------------------------------------------------------------------------------

CREATE TRIGGER Employee_ModifiedON ON Employee
FOR UPDATE
AS
DECLARE @EmployeeId INT
SELECT @EmployeeId = i.EmployeeId FROM INSERTED i
BEGIN
UPDATE Employee SET EmployeeModifiedOn = GETDATE() WHERE EmployeeId = @EmployeeId
END
PRINT 'Employee_ModifiedON Trigger fired'

--------------------------------------------------------------------------------------------------------
							 	-- Employee To Project Modified Trigger --
--------------------------------------------------------------------------------------------------------

CREATE TRIGGER EmployeeToProject_ModifiedON ON EmployeeToProject
FOR UPDATE
AS
DECLARE @ProjectId INT, @EmployeeId INT
SELECT @EmployeeId = i.EmployeeId FROM INSERTED i
SELECT @ProjectId = i.ProjectId FROM INSERTED i
BEGIN
UPDATE EmployeeToProject SET EmployeeToProjectModifiedOn = GETDATE() WHERE EmployeeId = @EmployeeId AND ProjectId = @ProjectId
END
PRINT 'EmployeeToProject_ModifiedON Trigger fired'