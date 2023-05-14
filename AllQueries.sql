Create Database DBM2
Go
USE DBM2
GO
-------------------
insert into SystemAdmin values ('waleed','user123');
insert into SystemUser values ('user123', 'password123');

execute createAllTables

go
Create Procedure createAllTables as
CREATE TABLE SystemUser(
    username VARCHAR(20) PRIMARY KEY,
    password VARCHAR(20)
);

CREATE TABLE SystemAdmin(
   ID INT IDENTITY PRIMARY KEY,
   name VARCHAR(20),
   username VARCHAR(20) FOREIGN KEY REFERENCES SystemUser ON UPDATE CASCADE ON DELETE CASCADE
);

CREATE TABLE SportsAssociationManager(
   ID INT IDENTITY PRIMARY KEY,
   name VARCHAR(20),
   username VARCHAR(20) FOREIGN KEY REFERENCES SystemUser ON UPDATE CASCADE ON DELETE CASCADE
);

CREATE TABLE Fan(
    national_ID VARCHAR(20) PRIMARY KEY,
    name VARCHAR(20),
    birth_date  DATETIME,
    address VARCHAR(20),
    phone_no INT,
    status BIT,
    username VARCHAR(20) FOREIGN KEY REFERENCES SystemUser ON UPDATE CASCADE ON DELETE CASCADE
);

CREATE TABLE Stadium(
    ID INT PRIMARY KEY IDENTITY,
    name VARCHAR(20),
    location VARCHAR(20),
    capacity INT,
    status BIT
);

CREATE TABLE Club(
    club_ID INT PRIMARY KEY IDENTITY,
    name VARCHAR(20),
    location VARCHAR(20)
);

CREATE TABLE StadiumManager(
    ID INT PRIMARY KEY IDENTITY,
    name VARCHAR(20),
    stadium_ID INT UNIQUE FOREIGN KEY REFERENCES Stadium ON UPDATE CASCADE ON DELETE CASCADE,
    username VARCHAR(20) FOREIGN KEY REFERENCES SystemUser ON UPDATE CASCADE ON DELETE CASCADE
);

CREATE TABLE ClubRepresentative(
    ID INT PRIMARY KEY IDENTITY,
    name VARCHAR(20),
    club_ID INT UNIQUE FOREIGN KEY REFERENCES Club ON UPDATE CASCADE ON DELETE CASCADE,
    username VARCHAR(20) FOREIGN KEY REFERENCES SystemUser ON UPDATE CASCADE ON DELETE CASCADE
);

CREATE TABLE Match(
    match_ID INT PRIMARY KEY IDENTITY,
    start_time datetime,
    end_time datetime,
    host_club_ID INT FOREIGN KEY REFERENCES Club ON UPDATE CASCADE ON DELETE SET NULL,
    guest_club_ID INT FOREIGN KEY REFERENCES Club ON UPDATE NO ACTION ON DELETE NO ACTION,
    stadium_ID INT FOREIGN KEY REFERENCES Stadium ON UPDATE CASCADE ON DELETE SET NULL -- wont delete match, will just set to null
);

CREATE TABLE Ticket(
    ID INT PRIMARY KEY IDENTITY,
    status BIT,
    match_ID INT FOREIGN KEY REFERENCES Match ON UPDATE CASCADE ON DELETE CASCADE
);


CREATE TABLE HostRequest(
    ID INT PRIMARY KEY IDENTITY,
    representative_ID INT FOREIGN KEY REFERENCES ClubRepresentative ON UPDATE CASCADE ON DELETE SET NULL,
    manager_ID INT FOREIGN KEY REFERENCES StadiumManager ON UPDATE NO ACTION ON DELETE NO ACTION,
    match_ID INT FOREIGN KEY REFERENCES Match ON UPDATE NO ACTION ON DELETE NO ACTION,
    status varchar(20)
);

CREATE TABLE TicketBuyingTransactions(
    fan_national_ID VARCHAR(20) FOREIGN KEY REFERENCES Fan ON UPDATE CASCADE ON DELETE CASCADE,
    ticket_ID INT FOREIGN KEY REFERENCES Ticket ON UPDATE CASCADE ON DELETE CASCADE
);

GO
-------------------
-- 2.1 b
CREATE PROCEDURE dropAllTables
AS
DROP TABLE TicketBuyingTransactions
DROP TABLE HostRequest
DROP TABLE Ticket
DROP TABLE Match
DROP TABLE ClubRepresentative
DROP TABLE StadiumManager
DROP TABLE Club
DROP TABLE Stadium
DROP TABLE Fan
DROP TABLE SportsAssociationManager
DROP TABLE SystemAdmin
DROP TABLE  SystemUser

GO
-------------------
--2.1 c
CREATE PROCEDURE dropAllProceduresFunctionsViews
AS
DROP PROCEDURE createAllTables
DROP PROCEDURE dropAllTables
DROP PROCEDURE clearAllTables
DROP VIEW allAssocManagers
DROP VIEW  allClubRepresentatives
DROP VIEW allStadiumManagers
DROP VIEW allFans
DROP VIEW allMatches
DROP VIEW allTickets
DROP VIEW allCLubs -- mind the spelling??
DROP VIEW allStadiums
DROP VIEW allRequests
DROP PROCEDURE addAssociationManager
DROP PROCEDURE addNewMatch
DROP VIEW clubsWithNoMatches
DROP PROCEDURE deleteMatch
DROP PROCEDURE deleteMatchesOnStadium
DROP PROCEDURE addClub
DROP PROCEDURE addTicket
DROP PROCEDURE deleteClub
DROP PROCEDURE addStadium
DROP PROCEDURE deleteStadium
DROP PROCEDURE blockFan
DROP PROCEDURE unblockFan
DROP PROCEDURE addRepresentative
DROP FUNCTION viewAvailableStadiumsOn
DROP PROCEDURE addHostRequest
DROP FUNCTION allUnassignedMatches
DROP PROCEDURE addStadiumManager
DROP FUNCTION allPendingRequests
DROP PROCEDURE acceptRequest
DROP PROCEDURE rejectRequest
DROP PROCEDURE addFan
DROP FUNCTION upcomingMatchesOfClub
DROP FUNCTION availableMatchesToAttend
DROP PROCEDURE purchaseTicket
DROP PROCEDURE updateMatchHost
DROP VIEW matchesPerTeam
DROP VIEW clubsNeverMatched
DROP FUNCTION clubsNeverPlayed
DROP FUNCTION matchWithHighestAttendance
DROP FUNCTION matchesRankedByAttendance
DROP FUNCTION requestsFromClub

GO
------------
--2.1 d
CREATE PROCEDURE clearAllTables
AS
DELETE FROM TicketBuyingTransactions
DELETE FROM HostRequest
DELETE FROM Ticket
DELETE FROM Match
DELETE FROM ClubRepresentative
DELETE FROM StadiumManager
DELETE FROM Club
DELETE FROM Stadium
DELETE FROM Fan
DELETE FROM SportsAssociationManager
DELETE FROM SystemAdmin
DELETE FROM SystemUser

-----------------
GO
CREATE PROCEDURE addClub
@name VARCHAR(20), 
@location VARCHAR(20)
AS
BEGIN
insert into Club (name,location) VALUES(@name,@location)
END
GO
-----------------
-----------------
go

CREATE VIEW clubsNeverMatched(Club1, Club2)
AS

SELECT C1.name , C2.name 
From Club C1, Club C2
where C1.club_ID<>C2.club_ID AND C1.club_ID<C2.club_ID 
except(
    Select C11.name, C22.name
    From Club C11 INNER JOIN Match M on C11.club_ID=M.guest_club_ID
                  INNER JOIN Club C22 on C22.club_ID=M.host_club_ID
      UNION
       Select C22.name, C11.name
    From Club C11 INNER JOIN Match M on C11.club_ID=M.guest_club_ID
                  INNER JOIN Club C22 on C22.club_ID=M.host_club_ID
      
                  
                  )

-----------------

go
CREATE FUNCTION clubsNeverPlayed
(@name VARCHAR(20))                        

RETURNS TABLE
AS
RETURN(
SELECT  C.name           --*****distinct to make sure its not repeated even tho from the previous view it wasnt
FROM Club C, clubsNeverMatched
WHERE ((@name=clubsNeverMatched.Club1 AND C.name = clubsNeverMatched.Club2) OR (@name=clubsNeverMatched.Club2 AND C.name = clubsNeverMatched.Club1)) AND C.name<>@name);

go
-----------------

GO

CREATE FUNCTION matchesRankedByAttendance    --DO WE KEEP MATHC ID AS COLUMN
()
returns table
as
return     --(SELECT total, HostClub, GuestClub
           --FROM(
(
SELECT M.match_ID,COUNT(T.ID) as total , H.name as HostClub, G.name as GuestClub
FROM Ticket T INNER JOIN Match M on T.match_ID=M.match_ID
              INNER JOIN Club H on M.host_club_ID= H.club_ID
              INNER JOIN Club G on M.guest_club_ID = G.club_ID
WHERE  T.status=0
GROUP BY M.match_ID, H.name, G.name
ORDER BY total DESC OFFSET 0 ROWS

) 

GO
------------------
GO



create function matchWithHighestAttendance  --offset 0 when ordering by in function
()
returns table
as
return( SELECT TOP 1 WITH TIES HostClub,GuestClub 
        FROM dbo.matchesRankedByAttendance()
        ORDER BY dbo.matchesRankedByAttendance.total DESC)
GO




--------------------


CREATE FUNCTION requestsFromClub
(@stadium VARCHAR(20), @club VARCHAR(20))
RETURNS TABLE
AS
return(
    SELECT CHost.name as Host_Name,CGuest.name as Guest_Name
    FROM HostRequest HR 
    INNER JOIN StadiumManager SM ON HR.manager_ID = SM.ID
    INNER JOIN ClubRepresentative CR ON HR.representative_ID = CR.ID
    INNER JOIN Stadium S ON SM.stadium_ID = S.ID
    INNER JOIN Club CHost ON CR.club_ID = CHost.club_ID
    INNER JOIN Match M ON CR.club_ID = M.host_club_ID
    INNER JOIN Club CGuest ON M.guest_club_ID = CGuest.club_ID

    where S.name=@stadium AND CHost.name=@club
    )


GO
------------

Use DBM2
go
CREATE VIEW allMatches AS
    SELECT  C1.name as Club1, C2.name as Club2, M.start_time
    FROM Match M INNER JOIN Club C1 ON M.host_club_ID = C1.club_ID
        INNER JOIN Club C2 ON M.guest_club_ID = C2.club_ID;
go
--
CREATE VIEW allTickets AS
    SELECT C1.name as Club1, C2.name AS Club2, S.name, M.start_time
    FROM Ticket T INNER JOIN Match M ON T.match_ID = M.match_ID
        INNER JOIN Club C1 ON M.host_club_ID = C1.club_ID
        INNER JOIN Club C2 ON M.guest_club_ID = C2.club_ID
        INNER JOIN Stadium S ON M.stadium_ID = S.ID;
go
--
CREATE VIEW allCLubs AS
    SELECT C.name, C.location
    FROM Club C;
go
--
CREATE VIEW allStadiums AS
    SELECT S.name, S.location, S.capacity, S.status
    FROM Stadium S;
go
--status available/unavailable represented as bits?

--


CREATE PROC addHostRequest
@clubName VARCHAR(20),
@stadiumName VARCHAR(20),
@startTime DATETIME
AS
DECLARE @representative_ID INT
DECLARE @manager_ID INT
DECLARE @club_ID INT
DECLARE @stadium_ID INT
DECLARE @match_ID INT
DECLARE @stadium_status BIT
DECLARE @stadium_has_match INT

SELECT @club_ID = C.club_ID, @representative_ID = CR.ID
FROM Club C INNER JOIN ClubRepresentative CR ON C.club_ID = CR.club_ID
WHERE C.name = @clubName

SELECT @stadium_ID = S.ID, @manager_ID = SM.ID, @stadium_status = S.status
FROM Stadium S INNER JOIN StadiumManager SM ON S.ID = SM.stadium_ID
WHERE S.name = @stadiumName

SELECT @match_ID = M.match_ID
FROM Match M
WHERE M.start_time = @startTime AND M.host_club_ID = @club_ID AND M.stadium_ID = @stadium_ID

SELECT @stadium_has_match = count(M.match_ID) 
FROM Match M
Where M.stadium_ID = @stadium_ID and M.start_time <=  @startTime and M.end_time > @startTime

IF @stadium_has_match > 0
    Print 'Stadium is already booked'
Else IF @stadium_status = 0
    Print 'Stadium is Unavailable'
Else
    INSERT INTO HostRequest (representative_ID, manager_ID, match_ID, status) VALUES (@representative_ID, @manager_ID, @match_ID, 'unhandled');
go
--request status at first is unhandled, later accepted or rejected in another function


CREATE FUNCTION allUnassignedMatches
(@clubName VARCHAR(20))
RETURNS TABLE
AS
RETURN(
    SELECT C2.name AS ClubName, M.start_time AS StartTime
    FROM Match M 
    INNER JOIN Club C1 ON M.host_club_ID = C1.club_ID
    INNER JOIN Club C2 ON M.guest_club_ID = C2.club_ID
    WHERE C1.name = @clubName AND M.stadium_ID IS NULL
);
go
--

CREATE PROC addStadiumManager
@name VARCHAR(20),
@stadiumName VARCHAR(20),
@username VARCHAR(20),
@password VARCHAR(20)
AS
DECLARE @stadium_ID INT

SELECT @stadium_ID = S.ID
FROM Stadium S
WHERE S.name = @stadiumName;

INSERT INTO SystemUser VALUES (@username, @password)

INSERT INTO StadiumManager VALUES (@name, @stadium_ID, @username);
GO
--

CREATE FUNCTION allPendingRequests
(@managerUser VARCHAR(20))
RETURNS @query TABLE(
    representative VARCHAR(20),
    club VARCHAR(20),
    start_time DATETIME
)
AS
BEGIN
DECLARE @manager_ID INT

SELECT @manager_ID = SM.ID
FROM StadiumManager SM
WHERE Sm.username = @managerUser

INSERT INTO @query
SELECT CR.name as Representative, C.name as Club, M.start_time
FROM HostRequest HR INNER JOIN ClubRepresentative CR ON HR.representative_ID = CR.ID
    INNER JOIN Match M ON HR.match_ID = M.match_ID
    INNER JOIN Club C ON M.guest_club_ID = C.club_ID
WHERE HR.manager_ID = @manager_ID AND HR.status='unhandled';
RETURN;
END;
go
-- check logic again


 --VII
 GO   
 CREATE PROC addTicket  
 @c_host VARCHAR(20),  
 @c_guest VARCHAR(20),  
 @start_time DATETIME     
 
 AS  
 DECLARE @match_id INT  
 SET @match_id =  (
    SELECT M.match_ID    
    FROM Match M   
    INNER JOIN Club c_host ON M.host_club_ID=c_host.club_ID   
    INNER JOIN Club c_guest ON M.guest_club_ID=c_guest.club_ID   
    WHERE c_host.name=@c_host AND c_guest.name=@c_guest AND @start_time=M.start_time)   

INSERT INTO Ticket (status,match_ID) VALUES (1,@match_id);


go
CREATE PROC acceptRequest
@managerUser VARCHAR(20),
@hostName VARCHAR(20),
@guestName VARCHAR(20),
@startTime DATETIME
AS
DECLARE @stadium_ID INT
DECLARE @host_ID INT
DECLARE @guest_ID INT
DECLARE @match_ID INT
DECLARE @manager_ID INT

SELECT @host_ID = C.club_ID
FROM Club C
WHERE C.name = @hostName

SELECT @guest_ID = C.club_ID
FROM Club C
WHERE C.name = @guestName

SELECT @stadium_ID = SM.stadium_ID, @manager_ID = SM.ID
FROM StadiumManager SM
WHERE SM.username = @managerUser

SELECT @match_ID = M.match_ID
FROM Match M
WHERE M.start_time = @startTime AND M.host_club_ID = @host_ID AND M.guest_club_ID = @guest_ID

UPDATE HostRequest
SET status = 'accepted'
WHERE match_ID = @match_ID and manager_ID = @manager_ID

UPDATE Match
SET stadium_ID = @stadium_ID
WHERE match_ID = @match_ID

DECLARE @i int = (SELECT capacity FROM Stadium WHERE ID = @stadium_ID)
WHILE @i >0
    BEGIN
        EXECUTE addTicket @hostName, @guestName, @startTime
        SET @i = @i -1
    END
go

--

go
CREATE PROC rejectRequest
@managerUser VARCHAR(20),
@hostName VARCHAR(20),
@guestName VARCHAR(20),
@startTime DATETIME
AS
DECLARE @stadium_ID INT
DECLARE @host_ID INT
DECLARE @guest_ID INT
DECLARE @match_ID INT
DECLARE @manager_ID INT

SELECT @host_ID = C.club_ID
FROM Club C
WHERE C.name = @hostName

SELECT @guest_ID = C.club_ID
FROM Club C
WHERE C.name = @guestName

SELECT @match_ID = M.match_ID
FROM Match M
WHERE M.start_time = @startTime AND M.host_club_ID = @host_ID AND M.guest_club_ID = @guest_ID

SELECT @manager_ID = SM.ID
FROM StadiumManager SM
WHERE SM.username = @managerUser

UPDATE HostRequest
SET status = 'rejected'
WHERE match_ID = @match_ID and manager_ID = @manager_ID

go


--2.2 a
GO
CREATE VIEW allAssocManagers
AS
SELECT U.username,U.password,A.name
FROM SportsAssociationManager A 
    INNER JOIN SystemUser U ON A.username=U.username;



--2.2 b
GO
CREATE VIEW allClubRepresentatives
AS
SELECT U.username,U.password,R.name as Representative_Name,C.name as Club_Name
FROM SystemUser U 
    INNER JOIN ClubRepresentative R ON U.username=R.username
    INNER JOIN Club C  ON C.club_ID=R.club_ID;

--


--2.2 C
GO 
CREATE VIEW allStadiumManagers
AS
SELECT U.username,U.password,M.name as Manager_Name,S.name Stadium_Name
FROM SystemUser U 
    INNER JOIN StadiumManager M ON U.username=M.username
    INNER JOIN Stadium S  ON S.ID = M.stadium_ID; 

--

--2.2 d
GO 
CREATE VIEW allFans
AS 
SELECT U.username,U.password,F.name,F.national_ID,F.birth_date, F.status
FROM  SystemUser U 
    INNER JOIN Fan F ON U.username=F.username

--XXI
GO
CREATE PROC addFan
@name VARCHAR(20),@username VARCHAR(20),@password VARCHAR(20),
@nat_id VARCHAR(20),@b_date DATETIME,@address VARCHAR(20), @phone INT
AS

INSERT INTO SystemUser (username,password)
VALUES (@username,@password);

INSERT INTO Fan (national_ID,name,birth_date,address,phone_no,status,username)
VALUES (@nat_id,@name,@b_date,@address,@phone,1,@username);

--ASSUMING FAN IS NOT BLOCKED WHEN ADDED FOR THE FIRST TIME STATUS = "UNBLOCKED"
-- CHECK FAN STATUS 3 DIF VALUES AND UPDATE TO VARCHAR.

----
--XXII
GO
CREATE FUNCTION upcomingMatchesOfClub
  (@club_name VARCHAR(20))
RETURNS @upcomingMatchesClub TABLE
(
    club1_name VARCHAR (20),
    club2_name VARCHAR (20),
    start_time DATETIME UNIQUE,
    stadium_name VARCHAR(20)
)
AS 
BEGIN 
  INSERT INTO @upcomingMatchesClub
    SELECT C1.name,C2.name, M.start_time,S.name
    FROM Club C1 
        INNER JOIN Match M   ON C1.club_ID=M.host_club_ID
        INNER JOIN Club C2   ON C2.club_ID=M.guest_club_ID
        INNER JOIN Stadium S ON S.ID=M.stadium_ID
        WHERE ( (M.start_time > CURRENT_TIMESTAMP) AND (C1.name=@club_name OR C2.name=@club_name) )
  RETURN
END;



--XXIII
GO
CREATE FUNCTION availableMatchesToAttend
(@given_datetime DATETIME)
RETURNS @upcomingMatchesDate TABLE
(
    c_host VARCHAR(20),
    c_guest VARCHAR(20),
    start_time DATETIME,
    stadium_name VARCHAR(20)
)
AS
BEGIN 
    INSERT INTO @upcomingMatchesDate
        SELECT C_host.name,C_GUEST.name,M.start_time,S.name
        FROM Match M
        INNER JOIN Club C_host  ON M.host_club_ID=C_host.club_ID
        INNER JOIN Club C_Guest ON M.guest_club_ID=C_Guest.club_ID
        INNER JOIN Stadium S    ON M.stadium_ID  = S.ID
        WHERE M.match_ID IN
            (SELECT M1.match_ID FROM 
            Match M1 INNER JOIN Ticket T ON M1.match_ID=T.match_ID
            WHERE (T.status=1 AND M.start_time>@given_datetime)
            );
    RETURN
END;
--SHOULD TIME BE TIME >= OR >
----
--XXIV
GO
CREATE PROC purchaseTicket 
@fan_id VARCHAR(20),@host_name VARCHAR(20),@guest_name VARCHAR(20),@start_time DATETIME
AS

DECLARE @purchased_ticket INT
DECLARE @fan_status BIT 

SET @fan_status =
    (SELECT F.status
    FROM Fan F
    WHERE F.national_ID=@fan_id);

IF @fan_status = 0
    PRINT 'BLOCKED.CAN NOT BUY TICKET'
ELSE
   
   BEGIN
    SET @purchased_ticket = 
        (SELECT TOP 1 T.ID 
        FROM Ticket T  
        INNER JOIN Match M ON (M.match_ID=T.match_ID)
        INNER JOIN Club C_Host ON (C_Host.club_ID = M.host_club_ID)
        INNER JOIN Club C_Guest ON (C_Guest.club_ID = M.guest_club_ID)
        WHERE (C_Host.name=@host_name AND C_Guest.name=@guest_name AND M.start_time=@start_time AND T.status=1));

    --IF IS NULL DID NOT WORK CREATE A FUNCTION OR STH?
    IF @purchased_ticket IS NULL
        PRINT 'TICKETS ARE SOLD OUT';
    ELSE 
       BEGIN
        UPDATE Ticket
        SET status=0
        WHERE ID=@purchased_ticket;

        INSERT INTO TicketBuyingTransactions (fan_national_ID,ticket_ID)
        VALUES (@fan_id,@purchased_ticket);
       END;
    END;

---XXV
  GO 
  CREATE PROC updateMatchHost
  @host_name VARCHAR(20),@guest_name VARCHAR(20),@start_time DATETIME
  AS

  DECLARE @new_host INT
  DECLARE @new_guest INT
  DECLARE @match_id INT

  SELECT @new_host=C_guest.club_ID , @new_guest=C_host.club_ID,@match_id=M.match_ID
  FROM  Match M 
  INNER JOIN Club C_host ON (M.host_club_ID=C_host.club_ID)
  INNER JOIN Club C_guest ON (M.guest_club_ID=C_guest.club_ID)
  WHERE (C_host.name = @host_name AND C_guest.name=@guest_name 
        AND M.start_time=@start_time);

 UPDATE  Match 
 SET host_club_ID =@new_host,
     guest_club_ID=@new_guest
 WHERE match_ID=@match_id;
 ----

   
--XXVI 
GO
CREATE VIEW matchesPerTeam
AS
SELECT clubsAndMatches.name , COUNT(clubsAndMatches.match_ID) as number_of_matches
FROM (Select c.name as name, m.match_ID as match_ID
      From Club c, Match m
      Where (c.club_ID = m.host_club_ID or c.club_ID = m.guest_club_ID) and M.end_time < CURRENT_TIMESTAMP) As clubsAndMatches
--Club C INNER JOIN Match M ON C.club_ID = M.host_club_ID INNER JOIN Club C2 ON C2.club_ID = M.guest_club_ID 
GROUP BY clubsAndMatches.name 
 


-- 2.2 
-- i
go
Create view allRequests as
Select cr.username as Club_Representative_username, sm.username as Stadium_Manager_username, hr.status as Request_status
From HostRequest hr, ClubRepresentative cr, StadiumManager sm
Where hr.representative_ID = cr.ID and hr.manager_ID = sm.ID

-- 2.3
-- i
go
Create Procedure addAssociationManager
@name varchar(20),
@username varchar(20),
@password varchar(20)
as
Insert into SystemUser (username, password) values (@username,@password)
Insert into SportsAssociationManager (name, username) values (@name,@username)

-- ii
go
Create Procedure addNewMatch
@hostClubName varchar(20),
@guestClubName varchar(20),
@startTime datetime,
@endTime datetime
as
Insert into Match (host_club_ID, guest_club_ID, start_time, end_time) 
Select host.club_ID, guest.club_ID, @startTime, @endTime
From Club host, Club guest
Where host.name = @hostClubName and guest.name = @guestClubName




-- iii
go
Create View clubsWithNoMatches as
Select c.name
From Club c
except (
Select c1.name
From Club c1, match m1
Where c1.club_ID = m1.host_club_ID or c1.club_ID = m1.guest_club_ID
)

-- iv
go
Create Procedure deleteMatch -- deletes match 
--(tickets already cascade on delete and ticket transaction cascade on ticket delete)
@hostClubName varchar(20),
@guestClubName varchar(20)
as

UPDATE HostRequest SET match_ID = NULL
Where HostRequest.match_ID in ( 
	Select match_ID 
	From Match inner join Club c1 on Match.host_club_ID = c1.club_ID inner join Club c2 on Match.guest_club_ID = c2.club_ID
	Where c1.name = @hostClubName and c2.name = @guestClubName) 


Delete From Match
Where @hostClubName in (
	Select Club.name 
	From Club 
	Where Match.host_club_ID = Club.club_ID) 
and
@guestClubName in (
	Select Club.name 
	From Club 
	Where Match.guest_club_ID = Club.club_ID)

--------
go
CREATE PROC deleteMatchesOnStadium
@stadiumName VARCHAR(20)
AS

UPDATE HostRequest SET match_ID = NULL
Where HostRequest.match_ID in ( 
	Select match_ID 
	From Match inner join Stadium on Match.stadium_ID = Stadium.ID
	Where Stadium.name = @stadiumName) 

DELETE 
From Match
Where Match.stadium_ID in (
    Select Stadium.ID
    From Stadium
    Where Stadium.name = @stadiumName ) and CURRENT_TIMESTAMP < Match.start_time 

go
--viii
go
Create Procedure deleteClub -- deletes club
-- (club rep already cascades on club delete)
@name varchar(20)
as 

UPDATE HostRequest SET representative_ID = NULL 
Where HostRequest.representative_ID in (
	Select ClubRepresentative.ID
	From Club, ClubRepresentative 
	Where Club.name = @name and ClubRepresentative.club_ID = Club.club_ID)

UPDATE Match SET guest_club_ID = NULL
Where Match.guest_club_ID in (
	Select Club.club_ID
	From Club
	Where Club.name = @name)

Delete From SystemUser 
Where SystemUser.username in (
	Select ClubRepresentative.username
	From ClubRepresentative, Club
	Where Club.name = @name and ClubRepresentative.club_ID = Club.club_ID)

Delete from Club 
Where Club.name = @name

-- ix
go
Create Procedure addStadium
@name varchar(20),
@location varchar(20),
@capacity int
as
insert into Stadium (name, location, capacity, status) values (@name, @location, @capacity,1)

-- x
go
Create Procedure deleteStadium -- deletes stadium
-- (Matches and stadium manager already cascade on delete and tickets cascade on match delete)
@name varchar(20)
as 

UPDATE HostRequest SET manager_ID = NULL 
Where HostRequest.manager_ID in (
	Select StadiumManager.ID
	From StadiumManager, Stadium
	Where Stadium.name = @name and StadiumManager.stadium_ID = Stadium.ID)

Delete From SystemUser 
Where SystemUser.username in (
	Select StadiumManager.username
	From StadiumManager, Stadium
	Where Stadium.name = @name and StadiumManager.stadium_ID = Stadium.ID)

Delete From Ticket
Where Ticket.match_ID in (
	Select Match.match_ID 
	From Match 
	Where Match.start_time > CURRENT_TIMESTAMP and Match.stadium_ID = (Select Stadium.ID From Stadium Where Stadium.name = @name)) 

Delete from Stadium 
Where Stadium.name = @name

-- xi
go
Create Procedure blockFan
@national_ID varchar(20)
as
Update Fan set status = 0 Where @national_ID = Fan.national_ID

-- xii
go
Create Procedure unblockFan
@national_ID varchar(20)
as
Update Fan set status = 1 Where @national_ID = Fan.national_ID


-- xiii
go
Create Procedure addRepresentative
@name varchar(20),
@club_name varchar(20),
@username varchar(20),
@password varchar(20)
as 
Insert into SystemUser (username, password) values (@username, @password)
Insert into ClubRepresentative (name, club_ID, username)
Select @name, Club.club_ID , @username
From Club
Where Club.name = @club_name

-- xiv
go
Create Function viewAvailableStadiumsOn (@date datetime) -- returns available stadiums on a given date (not date and time)
returns table
as 
return
	Select s.name, s.location, s.capacity
	From Stadium s
	Where s.status = 1 and 
	not exists 
	(Select m.stadium_ID
	 From Match m 
	 Where CAST(@date AS date) = CAST(m.start_time AS date))

go
