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
VALUES ('Dru¿yna1'),('Dru¿yna2'),('Dru¿yna3'),('Dru¿yna4'),('Dru¿yna5'),('Dru¿yna6');
GO

INSERT INTO dbo.FL_TB_MemberRole (RoleName, IsPlayer)
VALUES ('Gracz',1),('Trener',0);
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

INSERT INTO dbo.FL_LG_PlayerStatType (StatName, IsGoal)
VALUES ('Bramka', 1),('¯ó³ta kratka',0 ),('Czerwona kartka', 0),('Asysta', 0);
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

DECLARE @cnt INT = 1;

WHILE @cnt < 6
BEGIN
   INSERT INTO dbo.FL_TB_MatchMember (MatchId, MemberId, IsMemberInHomeTeam)
	   VALUES (1,1, 1),
			  (1,2, 1),
			  (1,(@cnt*2)+1, 0),
			  (1,(@cnt*2)+2, 0);
	SET @cnt = @cnt + 1;
END;

SET @cnt = 2;
WHILE @cnt < 6
BEGIN
   INSERT INTO dbo.FL_TB_MatchMember (MatchId, MemberId, IsMemberInHomeTeam)
	   VALUES (2,3, 1),
			  (2,4, 1),
			  (2,(@cnt*2)+1, 0),
			  (2,(@cnt*2)+2, 0);
	SET @cnt = @cnt + 1;
END;

SET @cnt = 3;
WHILE @cnt < 6
BEGIN
   INSERT INTO dbo.FL_TB_MatchMember (MatchId, MemberId, IsMemberInHomeTeam)
	   VALUES (3,5, 1),
			  (3,6, 1),
			  (3,(@cnt*2)+1, 0),
			  (3,(@cnt*2)+2, 0);
	SET @cnt = @cnt + 1;
END;

SET @cnt = 4;
WHILE @cnt < 6
BEGIN
   INSERT INTO dbo.FL_TB_MatchMember (MatchId, MemberId, IsMemberInHomeTeam)
	   VALUES (4,7, 1),
			  (4,8, 1),
			  (4,(@cnt*2)+1, 0),
			  (4,(@cnt*2)+2, 0);
	SET @cnt = @cnt + 1;
END;

INSERT INTO dbo.FL_TB_MatchMember (MatchId, MemberId, IsMemberInHomeTeam)
VALUES	(5,9, 1),
		(5,10, 1),
		(5,11, 0),
		(5,12, 0);

-- Cards
INSERT INTO dbo.FL_LG_PlayerStatsLog (MatchId, PlayerId, TeamId, StatTypeId, StartMinute)
VALUES	(1, 1, 1, 2, 35),
		(1, 2, 1, 2, 90),
		(2, 2, 1, 2, 40),
		(3, 2, 1, 2, 45),
		(3, 8, 4, 3, 23),
		(5, 2, 1, 2, 67),
		(6, 3, 2, 2, 32),
		(9, 11, 6, 2, 82),
		(10, 8, 4, 2, 26),
		(11, 5, 3, 3, 11),
		(12, 6, 3, 2, 6),
		(13, 8, 4, 2, 10),
		(14, 7, 5, 2, 75),
		(15, 11, 6, 2, 90);

-- Goals
INSERT INTO dbo.FL_LG_PlayerStatsLog (MatchId, PlayerId, TeamId, StatTypeId, StartMinute)
VALUES	(1, 1, 1, 1, 6),
		(1, 2, 1, 1, 20),
		(1, 3, 2, 1, 12),
		(3, 2, 1, 1, 59),
		(3, 8, 4, 1, 60),
		(4, 1, 1, 1, 30),
		(5, 1, 1, 1, 70),
		(5, 2, 1, 1, 80),
		(5, 2, 1, 1, 23),
		(5, 12, 6, 1, 56),
		(6, 3, 2, 1, 80),
		(7, 3, 2, 1, 23),
		(7, 8, 4, 1, 90),
		(9, 11, 6, 1, 91),
		(10, 8, 4, 1, 46),
		(11, 5, 3, 1, 57),
		(11, 5, 3, 1, 21),
		(12, 6, 3, 1, 90),
		(13, 8, 4, 1, 51),
		(13, 7, 4, 1, 20),
		(13, 7, 4, 1, 50),
		(13, 8, 4, 1, 61),
		(13, 10, 5, 1, 23),
		(14, 7, 5, 1, 5),
		(15, 11, 6, 1, 15),
		(15, 12, 6, 1, 53),
		(15, 11, 6, 1, 34);