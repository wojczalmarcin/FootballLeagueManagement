USE FootballLeague;
GO

DROP TABLE IF EXISTS dbo.FL_LG_PlayerStatsLog;
DROP TABLE IF EXISTS dbo.FL_TB_Match;
DROP TABLE IF EXISTS dbo.FL_LG_PlayerSuspensionLog;
DROP TABLE IF EXISTS dbo.FL_TB_FootballerPitchTime;
DROP TABLE IF EXISTS dbo.FL_TB_Member;
DROP TABLE IF EXISTS dbo.FL_TB_SeasonTeam;
DROP TABLE IF EXISTS dbo.FL_TB_Team;
DROP TABLE IF EXISTS dbo.FL_TB_Stadium;
DROP TABLE IF EXISTS dbo.FL_TB_MemberRole;
DROP TABLE IF EXISTS dbo.FL_LG_PlayerStatType;
DROP TABLE IF EXISTS dbo.FL_TB_Address;
DROP TABLE IF EXISTS dbo.FL_TB_Season;
DROP VIEW IF EXISTS dbo.FL_VW_MatchesScore;

CREATE TABLE dbo.FL_TB_Address(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	City NVARCHAR(25) NOT NULL,
	Street NVARCHAR(50) NULL,
	HouseNumber NVARCHAR(25) NULL,
	PostalCode NVARCHAR(10) NULL
);
GO

CREATE TABLE dbo.FL_LG_PlayerStatType(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	StatName NVARCHAR(15) NOT NULL
);
GO

CREATE TABLE dbo.FL_TB_FootballerPitchTime(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	PlayerId INT NOT NULL,
	AppearanceMinute INT NULL,
	MinutesPlayed INT NULL
);
GO

CREATE TABLE dbo.FL_TB_Match(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	TeamHomeId INT NOT NULL,
	TeamAwayId INT NOT NULL,
	SeasonId INT NOT NULL,
	IsFinished BIT NOT NULL,
	AddressId INT NULL,
	MatchDate DATETIME NULL
);
GO

CREATE TABLE dbo.FL_TB_Member(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	FirstName NVARCHAR(40) NOT NULL,
	LastName NVARCHAR(40) NOT NULL,
	MemberRoleId INT NOT NULL,
	TeamId INT NULL,
	Email NVARCHAR(30) NULL
);
GO

CREATE TABLE dbo.FL_TB_MemberRole(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	RoleName NVARCHAR(16) NOT NULL
);
GO

CREATE TABLE dbo.FL_LG_PlayerStatsLog(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	MatchId INT NOT NULL,
	PlayerId INT NOT NULL,
	TeamId INT NOT NULL,
	StatTypeId INT NOT NULL,
	StartMinute INT NULL
);
GO

CREATE TABLE dbo.FL_LG_PlayerSuspensionLog(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	PlayerId INT NOT NULL,
	StartDate DATETIME NOT NULL,
	EndDate DATETIME NOT NULL
);
GO

CREATE TABLE dbo.FL_TB_Stadium(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	StadiumName NVARCHAR(30) NOT NULL,
	NumberOfSeats INT NULL,
	AddressId INT NULL
);
GO

CREATE TABLE dbo.FL_TB_Team(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	TeamName NVARCHAR(30) NOT NULL,
	AddressId INT NULL,
	StadiumId INT NULL
);
GO

CREATE TABLE dbo.FL_TB_Season(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	StartDate DATETIME NOT NULL,
	EndDate DATETIME NULL,
	Sponsor NVARCHAR(30) NULL
)

CREATE TABLE dbo.FL_TB_SeasonTeam(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	SeasonId INT NOT NULL,
	TeamId INT NOT NULL
)

ALTER TABLE dbo.FL_TB_FootballerPitchTime
WITH CHECK ADD CONSTRAINT FK_Member_FootballerPitchTime
FOREIGN KEY (PlayerId) REFERENCES dbo.FL_TB_Member(Id);
GO

ALTER TABLE dbo.FL_TB_Match
WITH CHECK ADD CONSTRAINT FK_Team_Match_Home
FOREIGN KEY (TeamHomeId) REFERENCES dbo.FL_TB_Team(Id);
GO

ALTER TABLE dbo.FL_TB_Match
WITH CHECK ADD CONSTRAINT FK_Team_Match_Away
FOREIGN KEY (TeamAwayId) REFERENCES dbo.FL_TB_Team(Id);
GO

ALTER TABLE dbo.FL_TB_Match
WITH CHECK ADD CONSTRAINT FK_Season_Match
FOREIGN KEY (SeasonId) REFERENCES dbo.FL_TB_Season(Id);
GO

ALTER TABLE dbo.FL_TB_Member
WITH CHECK ADD CONSTRAINT FK_MemberRole_Member
FOREIGN KEY (MemberRoleId) REFERENCES dbo.FL_TB_MemberRole(Id);
GO

ALTER TABLE dbo.FL_TB_Member
WITH CHECK ADD CONSTRAINT FK_Team_Member
FOREIGN KEY (TeamId) REFERENCES dbo.FL_TB_Team(Id);
GO

ALTER TABLE dbo.FL_LG_PlayerStatsLog
WITH CHECK ADD CONSTRAINT FK_Member_PlayerStatsLog
FOREIGN KEY (PlayerId) REFERENCES dbo.FL_TB_Member(Id);
GO

ALTER TABLE dbo.FL_LG_PlayerStatsLog
WITH CHECK ADD CONSTRAINT FK_Match_PlayerStatsLog
FOREIGN KEY (MatchId) REFERENCES dbo.FL_TB_Match(Id);
GO

ALTER TABLE dbo.FL_LG_PlayerStatsLog
WITH CHECK ADD CONSTRAINT FK_PlayerStatType_PlayerStatsLog
FOREIGN KEY (StatTypeId) REFERENCES dbo.FL_LG_PlayerStatType(Id);
GO

ALTER TABLE dbo.FL_LG_PlayerStatsLog
WITH CHECK ADD CONSTRAINT FK_Team_PlayerStatsLog
FOREIGN KEY (TeamId) REFERENCES dbo.FL_TB_Team(Id);
GO

ALTER TABLE dbo.FL_LG_PlayerSuspensionLog
WITH CHECK ADD CONSTRAINT FK_Member_PlayerSuspensionLog
FOREIGN KEY (PlayerId) REFERENCES dbo.FL_TB_Member(Id);
GO

ALTER TABLE dbo.FL_TB_Stadium
WITH CHECK ADD CONSTRAINT FK_Address_Stadium
FOREIGN KEY (AddressId) REFERENCES dbo.FL_TB_Address(Id);
GO

ALTER TABLE dbo.FL_TB_Team
WITH CHECK ADD CONSTRAINT FK_Address_Team
FOREIGN KEY (AddressId) REFERENCES dbo.FL_TB_Address(Id);
GO

ALTER TABLE dbo.FL_TB_Team
WITH CHECK ADD CONSTRAINT FK_Stadium_Team
FOREIGN KEY (StadiumId) REFERENCES dbo.FL_TB_Stadium(Id);
GO

ALTER TABLE dbo.FL_TB_SeasonTeam
WITH CHECK ADD CONSTRAINT FK_Season_SeasonTeam
FOREIGN KEY (SeasonId) REFERENCES dbo.FL_TB_Season(Id);
GO


ALTER TABLE dbo.FL_TB_SeasonTeam
WITH CHECK ADD CONSTRAINT FK_Team_SeasonTeam
FOREIGN KEY (TeamId) REFERENCES dbo.FL_TB_Team(Id);
GO


CREATE VIEW dbo.FL_VW_MatchesScore AS
SELECT mt.Id AS 'MatchId'
	,tmh.Id AS 'HomeTeamId'
	,tma.Id AS 'AwayTeamId'
	,COUNT(CASE WHEN psl.TeamId=mt.TeamHomeId THEN psl.PlayerId END) as 'HomeGoals'
	,COUNT(CASE WHEN psl.TeamId=mt.TeamAwayId THEN psl.PlayerId END) as 'AwayGoals'
	,mt.SeasonId
FROM FL_TB_Match AS mt
JOIN FL_TB_Team AS tmh ON mt.TeamHomeId = tmh.Id
LEFT JOIN FL_LG_PlayerStatsLog AS psl ON mt.Id = psl.MatchId 
	AND psl.StatTypeId=(SELECT Id FROM FL_LG_PlayerStatType WHERE UPPER(StatName)='GOAL') 
JOIN FL_TB_Team AS tma ON mt.TeamAwayId = tma.Id
GROUP BY mt.Id,tmh.Id, tma.Id, mt.SeasonId;

--CREATE NONCLUSTERED INDEX tabela_prop ON tabela (prop ASC)