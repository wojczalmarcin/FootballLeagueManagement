USE FootballLeague;
GO

DELETE FROM dbo.FL_TB_MatchMember;
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
		('Patryk', 'Kaczyñski', 1, 6),
		('Bartosz', 'Magik', 1, 1),
		('Mateusz', 'Kapustka', 1, 1),
		('Wojciech', 'Rydzyk', 1, 1),
		('Karol', 'Rodzynek', 1, 1),
		('Marcin', 'Niewiadomski', 1, 1),
		('Tymoteusz', 'Smolec', 1, 1),
		('Sylwester', 'Samotny', 1, 2),
		('Józef', 'Wielki', 1, 2),
		('Patryk', 'Król', 1, 2),
		('Karol', 'M³ot', 1, 2),
		('Nikodem', 'Gruszka', 1, 2),
		('Andrzej', 'Pietruszka', 1, 2),
		('Jaros³aw', 'Gawlik', 1, 3),
		('Szymon', 'Samozwaniec', 1, 3),
		('Grzegorz', 'Nowak', 1, 3),
		('Krzysztof', 'Myœliciel', 1, 3),
		('Mateusz', 'Góralski', 1, 3),
		('Tadeusz', 'Lewandowski', 1, 3),
		('Stefan', 'M¹czyñski', 1, 4),
		('Cyryl', 'Wêdrownik', 1, 4),
		('Janusz', 'Ma³y', 1, 4),
		('Aleksander', 'Kowalczyk', 1, 4),
		('Miron', 'Urbañski', 1, 4),
		('Andrzej', 'Krawczyk', 1, 4),
		('Przemys³aw', 'Kubiak', 1, 5),
		('Boles³aw', 'Zieliñski', 1, 5),
		('Dorian', 'Rutkowski', 1, 5),
		('Ignacy', 'Stêpieñ', 1, 5),
		('Emil', 'Makowski', 1, 5),
		('Remigiusz', 'Andrzejewski', 1, 5),
		('Ludwik', 'Michalak', 1, 6),
		('Marcin', 'Szczepañski', 1, 6),
		('Ariel', 'Ostrowski', 1, 6),
		('Edward', 'Pawlak', 1, 6),
		('Filip', 'Witkowski', 1, 6),
		('Piotr', 'Zawadzki', 1, 6);
GO

INSERT INTO dbo.FL_LG_PlayerStatType (StatName, IsGoal)
VALUES ('Bramka', 1),('¯ó³ta kratka',0 ),('Czerwona kartka', 0),('Asysta', 0);
GO

INSERT INTO dbo.FL_TB_Season (StartDate, EndDate)
VALUES ('2020-07-01', '2021-06-30');
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

-- Filling MatchMember table
DECLARE @matchCount INT = 1;

WHILE @matchCount < 16
BEGIN
	DECLARE @TeamHomeId INT = (SELECT TeamHomeId FROM FL_TB_Match WHERE Id = @matchCount);
	DECLARE @TeamAwayId INT = (SELECT TeamAwayId FROM FL_TB_Match WHERE Id = @matchCount);
	DECLARE @PlayerId INT;
	DECLARE @TeamId INT;

	DECLARE PlayersCursor CURSOR FAST_FORWARD READ_ONLY
	FOR
    SELECT Id, TeamId 
	FROM FL_TB_Member 
	WHERE TeamId = @TeamHomeId;

	OPEN PlayersCursor

	FETCH NEXT FROM PlayersCursor INTO @PlayerId, @TeamId

	WHILE @@FETCH_STATUS = 0
		BEGIN

		IF @TeamId = @TeamHomeId
			INSERT INTO dbo.FL_TB_MatchMember (MatchId, MemberId, IsMemberInHomeTeam)
			VALUES (@matchCount,@PlayerId, 1)
		ELSE
			INSERT INTO dbo.FL_TB_MatchMember (MatchId, MemberId, IsMemberInHomeTeam)
			VALUES (@matchCount,@PlayerId, 0)

        FETCH NEXT FROM PlayersCursor INTO @PlayerId, @TeamId

		END

	CLOSE PlayersCursor
	DEALLOCATE PlayersCursor

	SET @matchCount = @matchCount + 1;
END;

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