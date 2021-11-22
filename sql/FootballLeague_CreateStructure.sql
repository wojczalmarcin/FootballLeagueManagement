USE FootballLeague;
GO

-- Drop tables
DROP TABLE IF EXISTS dbo.FL_TB_MatchMember;
DROP TABLE IF EXISTS dbo.FL_LG_PlayerStatsLog;
DROP TABLE IF EXISTS dbo.FL_TB_Match;
DROP TABLE IF EXISTS dbo.FL_LG_PlayerSuspensionLog;
DROP TABLE IF EXISTS dbo.FL_TB_Member;
DROP TABLE IF EXISTS dbo.FL_TB_SeasonTeam;
DROP TABLE IF EXISTS dbo.FL_TB_Team;
DROP TABLE IF EXISTS dbo.FL_TB_Stadium;
DROP TABLE IF EXISTS dbo.FL_TB_MemberRole;
DROP TABLE IF EXISTS dbo.FL_LG_PlayerStatType;
DROP TABLE IF EXISTS dbo.FL_TB_Address;
DROP TABLE IF EXISTS dbo.FL_TB_Season;
DROP VIEW IF EXISTS dbo.FL_VW_MatchScore;

-- Create tables
CREATE TABLE dbo.FL_TB_Address(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	City NVARCHAR(25) NOT NULL,
	Street NVARCHAR(50) NULL,
	HouseNumber NVARCHAR(25) NULL,
	PostalCode NVARCHAR(10) NOT NULL
);
GO

CREATE TABLE dbo.FL_LG_PlayerStatType(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	StatName NVARCHAR(15) NOT NULL,
	IsGoal BIT NOT NULL
);
GO

CREATE TABLE dbo.FL_TB_Match(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	TeamHomeId INT NOT NULL,
	TeamAwayId INT NOT NULL,
	SeasonId INT NOT NULL,
	IsFinished BIT NOT NULL,
	StadiumId INT NULL,
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
	RoleName NVARCHAR(16) NOT NULL,
	IsPlayer BIT NOT NULL
);
GO

CREATE TABLE dbo.FL_TB_MatchMember(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	MatchId INT NOT NULL,
	MemberId INT NOT NULL,
	IsMemberInHomeTeam BIT NOT NULL,
	AppearanceMinute INT NULL,
	MinutesPlayed INT NULL
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

-- Foreign keys

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

ALTER TABLE dbo.FL_TB_Match
WITH CHECK ADD CONSTRAINT FK_Stadium_Match
FOREIGN KEY (StadiumId) REFERENCES dbo.FL_TB_Stadium(Id);
GO

ALTER TABLE dbo.FL_TB_Member
WITH CHECK ADD CONSTRAINT FK_MemberRole_Member
FOREIGN KEY (MemberRoleId) REFERENCES dbo.FL_TB_MemberRole(Id);
GO

ALTER TABLE dbo.FL_TB_Member
WITH CHECK ADD CONSTRAINT FK_Team_Member
FOREIGN KEY (TeamId) REFERENCES dbo.FL_TB_Team(Id);
GO

ALTER TABLE dbo.FL_TB_MatchMember
WITH CHECK ADD CONSTRAINT FK_Match_MatchMember
FOREIGN KEY (MatchId) REFERENCES dbo.FL_TB_Match(Id);
GO

ALTER TABLE dbo.FL_TB_MatchMember
WITH CHECK ADD CONSTRAINT FK_Member_MatchMember
FOREIGN KEY (MemberId) REFERENCES dbo.FL_TB_Member(Id);
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

-- Create views
CREATE VIEW dbo.FL_VW_MatchScore AS
SELECT mt.Id AS 'MatchId'
	,COUNT(CASE WHEN psl.TeamId=mt.TeamHomeId THEN psl.PlayerId END) as 'HomeGoals'
	,COUNT(CASE WHEN psl.TeamId=mt.TeamAwayId THEN psl.PlayerId END) as 'AwayGoals'
FROM FL_TB_Match AS mt
LEFT JOIN FL_LG_PlayerStatsLog AS psl ON mt.Id = psl.MatchId 
	AND psl.StatTypeId=(SELECT Id FROM FL_LG_PlayerStatType WHERE IsGoal = 1) 
GROUP BY mt.Id;
GO

-- Create indexes

CREATE NONCLUSTERED INDEX TeamHomeId ON dbo.FL_TB_Match (TeamHomeId ASC);
CREATE NONCLUSTERED INDEX TeamAwayId ON dbo.FL_TB_Match (TeamAwayId ASC);
CREATE NONCLUSTERED INDEX SeasonId ON dbo.FL_TB_Match (SeasonId ASC);
CREATE NONCLUSTERED INDEX StadiumId ON dbo.FL_TB_Match (StadiumId ASC);

CREATE NONCLUSTERED INDEX MemberRoleId ON dbo.FL_TB_Member (MemberRoleId ASC);
CREATE NONCLUSTERED INDEX TeamId ON dbo.FL_TB_Member (TeamId ASC);

CREATE NONCLUSTERED INDEX MatchId ON dbo.FL_TB_MatchMember (MatchId ASC);
CREATE NONCLUSTERED INDEX MemberId ON dbo.FL_TB_MatchMember (MemberId ASC);

CREATE NONCLUSTERED INDEX MatchId ON dbo.FL_LG_PlayerStatsLog (MatchId ASC);
CREATE NONCLUSTERED INDEX PlayerId ON dbo.FL_LG_PlayerStatsLog (PlayerId ASC);
CREATE NONCLUSTERED INDEX TeamId ON dbo.FL_LG_PlayerStatsLog (TeamId ASC);
CREATE NONCLUSTERED INDEX StatTypeId ON dbo.FL_LG_PlayerStatsLog (StatTypeId ASC);

CREATE NONCLUSTERED INDEX PlayerId ON dbo.FL_LG_PlayerSuspensionLog (PlayerId ASC);

CREATE NONCLUSTERED INDEX AddressId ON dbo.FL_TB_Stadium (AddressId ASC);

CREATE NONCLUSTERED INDEX AddressId ON dbo.FL_TB_Team (AddressId ASC);
CREATE NONCLUSTERED INDEX StadiumId ON dbo.FL_TB_Team (StadiumId ASC);

CREATE NONCLUSTERED INDEX SeasonId ON dbo.FL_TB_SeasonTeam (SeasonId ASC);
CREATE NONCLUSTERED INDEX TeamId ON dbo.FL_TB_SeasonTeam (TeamId ASC);