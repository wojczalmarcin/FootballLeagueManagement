USE FootballLeague;
GO

DELETE FROM dbo.FL_LG_PlayerStatsLog;
DELETE FROM dbo.FL_TB_Match;
DELETE FROM dbo.FL_TB_SeasonTeam;
DELETE FROM dbo.FL_TB_Season;
DELETE FROM dbo.FL_LG_PlayerStatType;
DELETE FROM dbo.FL_TB_Member;
DELETE FROM dbo.FL_TB_MemberRole;
DELETE FROM dbo.FL_TB_Team;
GO

INSERT INTO dbo.FL_TB_Team (TeamName)
VALUES ('Team1'),('Team2'),('Team3'),('Team4'),('Team5'),('Team6');
GO

INSERT INTO dbo.FL_TB_MemberRole (RoleName)
VALUES ('Player'),('Referee'),('Coach');
GO

INSERT INTO dbo.FL_TB_Member (FirstName, LastName, MemberRoleId, TeamId)
VALUES	('Janusz', 'Kowalski', 1, 1),
		('Mariusz', 'Pietruszka', 1, 1),
		('Marcin', 'Nowak', 1, 2),
		('Mateusz', 'D¹b', 1, 2),
		('Eryk', 'Baranowski', 1, 3),
		('Micha³', 'Jastrz¹b', 1, 3),
		('Bartosz', 'Mózg', 1, 4),
		('Marcin', 'D¹browski', 1, 4),
		('Karol', 'B³aszczykowski', 1, 5),
		('Daniel', 'Kulczyk', 1, 5),
		('Pawe³', 'Morawiecki', 1, 6),
		('Patryk', 'Kaczyñski', 1, 6);
GO

INSERT INTO dbo.FL_LG_PlayerStatType (StatName)
VALUES ('Goal'),('YellowCard'),('RedCard'),('Assist');
GO

INSERT INTO dbo.FL_TB_Season (STartDate)
VALUES ('2020-07-01');
GO

INSERT INTO dbo.FL_TB_SeasonTeam
VALUES  (1,1),
		(1,2),
		(1,3),
		(1,4),
		(1,5),
		(1,6);
GO

INSERT INTO dbo.FL_TB_Match (TeamHomeId, TeamAwayId, IsFinished, SeasonId)
VALUES  (1, 2, 1, 1),
		(1, 3, 1, 1),
		(1, 4, 1, 1),
		(1, 5, 1, 1),
		(1, 6, 1, 1),
		(2, 3, 1, 1),
		(2, 4, 1, 1),
		(2, 5, 1, 1),
		(2, 6, 1, 1),
		(3, 4, 1, 1),
		(3, 5, 1, 1),
		(3, 6, 1, 1),
		(4, 5, 1, 1),
		(4, 6, 1, 1),
		(5, 6, 1, 1);
GO

INSERT INTO dbo.FL_LG_PlayerStatsLog (MatchId, PlayerId, TeamId, StatTypeId)
VALUES	(1, 1, 1, 1),
		(1, 2, 1, 1),
		(1, 3, 2, 1),
		(3, 2, 1, 1),
		(3, 8, 4, 1),
		(4, 1, 1, 1),
		(5, 1, 1, 1),
		(5, 2, 1, 1),
		(5, 2, 1, 1),
		(5, 12, 6, 1),
		(6, 3, 2, 1),
		(7, 3, 2, 1),
		(7, 8, 4, 1),
		(9, 11, 6, 1),
		(10, 8, 4, 1),
		(11, 5, 3, 1),
		(11, 5, 3, 1),
		(12, 6, 3, 1),
		(13, 8, 4, 1),
		(13, 7, 4, 1),
		(13, 7, 4, 1),
		(13, 8, 4, 1),
		(13, 10, 5, 1),
		(14, 7, 5, 1),
		(15, 11, 6, 1),
		(15, 12, 6, 1),
		(15, 11, 6, 1);

