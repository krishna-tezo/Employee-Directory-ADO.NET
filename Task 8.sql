--Employee Directory using ADO.NET
USE KrishnaAnandDB;
GO

--Add a foreign key constraint
ALTER TABLE Employee
ADD CONSTRAINT FK_ProjectId FOREIGN KEY (ProjectId) REFERENCES Project(ProjectId);

--Rename Column
EXEC sp_rename 'Employee.EmpId',  'Id', 'COLUMN';
EXEC sp_rename 'Employee.Dob',  'DOB', 'COLUMN';
EXEC sp_rename 'Role.RoleName',  'Name', 'COLUMN';
EXEC sp_rename 'Role.RoleDesc',  'Description', 'COLUMN';
EXEC sp_rename 'Role.RoleId',  'Id', 'COLUMN';

SELECT 
R.Id,R.Name,R.Description, D.DeptName, L.LocationName 
FROM Role R
JOIN Department D ON R.DeptId = D.DeptId
JOIN Location L On R.LocationId = L.LocationId;



--Change column type
ALTER TABLE Role
ALTER COLUMN LocationId varchar(6);

DROP TABLE Project;
DROP TABLE Manager;
DROP TABLE Location;
DROP TABLE Employee;
DROP TABLE Role;
DROP TABLE Department;


CREATE TABLE Department(
	DeptId VARCHAR(6) PRIMARY KEY,
	DeptName VARCHAR(30)
);

CREATE TABLE Role(
	RoleId VARCHAR(6)PRIMARY KEY,
	RoleName VARCHAR(30),
	DeptId VARCHAR (6) FOREIGN KEY REFERENCES Department(DeptId),
	RoleDesc VARCHAR (200),
);

CREATE TABLE Employee(
	EmpId VARCHAR(8)PRIMARY KEY,
	FirstName VARCHAR(100),
	LastName VARCHAR(100),
	Email VARCHAR(100),
	Dob DATE,
	MobileNumber VARCHAR(10),
	JoinDate DATE,
	ProjectId VARCHAR(6),
	RoleId VARCHAR(6) FOREIGN KEY REFERENCES Role(RoleId)
);

CREATE Table Manager(
	ManagerId varchar(6) PRIMARY KEY,
	EmpId VARCHAR(8) REFERENCES Employee(EmpId)
);

CREATE TABLE Project(
	ProjectId varchar(6) PRIMARY KEY,
	ProjectName varchar(30),
	ManagerId varchar(6) FOREIGN KEY REFERENCES Manager(ManagerId)
);

CREATE TABLE Location(
	LocationId varchar(6) PRIMARY KEY,
	LocationName varchar(30),
	RoleId VARCHAR(6) FOREIGN KEY REFERENCES Role(RoleId)
);


--INSERT QUERIES FOR DEPARTMENT
INSERT INTO Department VALUES('DPT001','Human Resources');
INSERT INTO Department VALUES('DPT002','Product Engg.');
INSERT INTO Department VALUES('DPT003','Marketing');
SELECT * FROM Department;

--INSERT QUERIES FOR Location
INSERT INTO Location VALUES('LOC001','Hyderabad');
INSERT INTO Location VALUES('LOC002','Bangalore');
INSERT INTO Location VALUES('LOC003','Mumbai');
INSERT INTO Location VALUES('LOC004','Patna');
SELECT * FROM Location;

--INSERT QUERIES FOR Manager
INSERT INTO Manager VALUES('MR0001','TZKC5679');
INSERT INTO Manager VALUES('MR0002','TZLD0129');
INSERT INTO Manager VALUES('MR0003','TZFY1988');
INSERT INTO Manager VALUES('MR0004','TZEW5169');
INSERT INTO Manager VALUES('MR0005','TZOT7637');
SELECT * FROM Manager;

ALTER Table Manager 
DROP CONSTRAINT FK__Manager__EmpId__5D95E53A

ALTER TABLE Manager
ADD CONSTRAINT FK_EmpId FOREIGN KEY (EmpId) REFERENCES Employee(EmpId);

--INSERT QUERIES FOR Role
INSERT INTO Role VALUES('RL0001','Full Stack Developer','DPT002','Handles full web development stack','LOC001');
INSERT INTO Role VALUES('RL0002','Talent Acquisition','DPT001','Talent Hiring','LOC002');
INSERT INTO Role VALUES('RL0003','Backend Developer','DPT002','Handles Backend','LOC003');
INSERT INTO Role VALUES('RL0004','Data Scientist','DPT002','Handles Data Oriented Tasks','LOC003');
INSERT INTO Role VALUES('RL0005','Data Analyst','DPT001','Handles full web development stack','LOC002');
INSERT INTO Role VALUES('RL0006','Sales Manager','DPT003','Handles Sales','LOC004');

--INSERT QUERIES FOR Employee
INSERT INTO Employee VALUES ('TZEJ7917', 'Edgar', 'Jones', 'edgar@tezo.com', '2002-02-12', '9999999999', '2023-03-12', 'PR0001', 'RL0001');
INSERT INTO Employee VALUES ('TZKC7993', 'Kensley', 'Curry', 'edgar@tezo.com', '2002-02-12', '9999999999', '2023-03-12', 'PR0002', 'RL0002');
INSERT INTO Employee VALUES ('TZLD0129', 'Liam', 'Davis', 'liam@tezo.com', '1993-08-27', '6666666666', '2023-03-12', 'PR0003', 'RL0003');
INSERT INTO Employee VALUES ('TZFY1988', 'Fernanda', 'Yu', 'sophia@tezo.com', '1985-09-30', '7777777777', '2023-03-12', 'PR0004', 'RL0004');
INSERT INTO Employee VALUES ('TZNS5420', 'Nathan', 'Smith', 'nathan@tezo.com', '1990-04-15', '8888888888', '2023-03-12', 'PR0004', 'RL0004');
INSERT INTO Employee VALUES ('TZEJ0547', 'Emily', 'Johnson', 'emily@tezo.com', '1988-09-21', '7777777777', '2023-03-12', 'PR0002', 'RL0005');
INSERT INTO Employee VALUES ('TZEW5169', 'Ella', 'Wilson', 'ella@tezo.com', '1991-05-18', '4444444444', '2023-03-12', 'PR0002', 'RL0006');
INSERT INTO Employee VALUES ('TZOT7637', 'Olivia', 'Taylor', 'olivia@tezo.com', '1996-11-05', '5555555555', '2023-03-12', 'PR0001', 'RL0003');
INSERT INTO Employee VALUES ('TZBP0176', 'Briggs', 'Peterson', 'alice@tezo.com', '1990-05-18', '8888888888', '2023-03-12', 'PR0002', 'RL0005');
INSERT INTO Employee VALUES ('TZEB9666', 'Ethan', 'Brown', 'ethan@tezo.com', '1987-12-10', '3333333333', '2023-03-12', 'PR0004', 'RL0001');
INSERT INTO Employee VALUES ('TZBS2962', 'Badgar', 'Smith', 'alice@tezo.com', '1990-05-18', '8888888888', '2023-03-12', 'PR0004', 'RL0004');
INSERT INTO Employee VALUES ('TZAJ8515', 'Ava', 'Jones', 'ava@tezo.com', '1984-03-26', '2222222222', '2023-03-12', 'PR0002', 'RL0006');
INSERT INTO Employee VALUES ('TZBJ2034', 'Badger', 'Johnson', 'sophia@tezo.com', '1985-09-30', '7777777777', '2023-03-12', 'PR0001', 'RL0002');
INSERT INTO Employee VALUES ('TZCW1929', 'Cadger', 'Williams', 'michael@tezo.com', '1993-07-22', '6666666666', '2023-03-12', 'PR0003', 'RL0001');
INSERT INTO Employee VALUES ('TZCW3988', 'Cadero', 'Williamso', 'michael@tezo.com', '1993-07-22', '6666666666', '2023-03-12', 'PR0001', 'RL0005');
INSERT INTO Employee VALUES ('TZOM9148', 'Olivia', 'Martinez', 'olivia@tezo.com', '1996-04-28', '4444444444', '2023-03-12', 'PR0002', 'RL0002');
INSERT INTO Employee VALUES ('TZDN8774', 'Dangelo', 'Navarro', 'edgar@tezo.com', '2002-02-12', '9999999999', '2023-03-12', 'PR0004', 'RL0003');
INSERT INTO Employee VALUES ('TZWF4032', 'Winter', 'Franco', 'edgar@tezo.com', '2002-02-12', '9999999999', '2023-03-12', 'PR0003', 'RL0006');
INSERT INTO Employee VALUES ('TZRC6057', 'Reign', 'Colon', 'edgar@tezo.com', '2002-02-12', '9999999999', '2023-03-12', 'PR0002', 'RL0002');






SELECT * FROM Employee;

--INSERT QUERIES FOR Project
INSERT INTO Project VALUES('PR0001','Project 1','MR0001');
INSERT INTO Project VALUES('PR0002','Project 2','MR0002');
INSERT INTO Project VALUES('PR0003','Project 3','MR0003');
INSERT INTO Project VALUES('PR0004','Project 4','MR0004');
 
SELECT * FROM Project;

SELECT * FROM Role;

SELECT * FROM Employee;
ALTER TABLE Employee 
Add IsDeleted BIT;


GO

SELECT E.Id, E.FirstName, E.LastName, R.Name, D.DeptName, L.LocationName,JoinDate,P.ProjectName, M.ManagerId FROM Employee E
JOIN Role R ON E.RoleId = R.Id
JOIN Location L ON R.LocationId = L.LocationId
JOIN Department D ON R.DeptId = D.DeptId
JOIN Project P ON P.ProjectId = E.ProjectId
JOIN Manager M ON P.ManagerId = M.ManagerId;

SELECT E.Id, E.FirstName, E.LastName, M.ManagerId,CONCAT(K.FirstName,' ',K.LastName) As ManagerName FROM Employee E
JOIN Project P ON P.ProjectId = E.ProjectId
JOIN Manager M ON P.ManagerId = M.ManagerId
LEFT JOIN Employee K ON K.Id = M.EmpId;

SELECT P.ProjectId,P.ProjectName,CONCAT(E.FirstName,' ',E.LastName) as ManagerName FROM Project P
JOIN Manager M ON P.ManagerId = M.ManagerId
JOIN Employee E ON M.EmpId = E.Id

UPDATE MANAGER SET 
FirstName = 'TZBP0176' WHERE ManagerId = 'MR0001';
SELECT * FROM Manager;

SELECT * FROM Employee;

SELECT LocationId FROM Location WHERE LocationName = 'Hyderabad'; 