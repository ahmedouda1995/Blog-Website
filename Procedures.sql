CREATE PROCEDURE list_conferences
	
AS
	
	select * from conferences

go

CREATE proc preview_conference
 @conference_id int
 AS
 Select name, starting_date, venue
 From Conferences c
 Where c.conference_id = @conference_id

go

CREATE proc development_team_conference
 @conference_id int
 AS
 Select d.name AS "development_name",d.email AS "Email", g.name AS "game_name"
 FROM Conferences_presented_at_by_Development_Teams_Games dtc, Games g, Development_Teams d
 Where dtc.conference_id = @conference_id AND  dtc.email = d.email AND g.game_id = dtc.game_id

go
CREATE proc games_at_conference
 @conference_id int 
 AS SELECT g.name, g.game_id 
 From Games g
 Where conference_id = @conference_id
 drop proc games_at_conference
go
CREATE PROCEDURE Check_Attended

@email varchar(50),
@conference_id int,
@checker int output
AS
if(exists( select * from Conferences_attended_by_Members where @email=email and @conference_id=conference_id))
set @checker = 1
else
set @checker = 0
go
CREATE proc view_conference_reviews
 @conference_id int
 AS
 SELECT *
 from Conference_Reviews 
 WHERE conference_id=@conference_id

go
CREATE PROCEDURE Attend_Conference
	
@email varchar(50),
@conference_id int,
@out int output
AS

if(exists( select * 
           from Conferences_attended_by_Members
		   where @email =email and @conference_id=conference_id))
begin
set @out=0
end
else
begin
insert Conferences_attended_by_Members(email,conference_id)
values (@email,@conference_id) 
set @out=1
end
go
CREATE PROCEDURE Member_Type
@email varchar(50),
@Type int output

AS

if(exists( select * from Normal_Users where @email=email)) 
set @Type = 1

if(exists(select * from Development_Teams where @email=email))
set @Type = 2

if(exists(select * from Verified_Reviewers where @email=email))
set @Type = 3


go
CREATE PROCEDURE Available_non_presented_games

@email varchar(50)
AS

select * 
from Development_Teams_develops_Games t1 , Games t2
where t1.email= @email and t1.game_id = t2.game_id and t2.conference_id is null


go
create proc Development_TeamPresents(
@game_id int,
@email varchar(50),
@conference_id int)
AS
INSERT Conferences_presented_at_by_Development_Teams_Games(email, game_id, conference_id)
Values(@email, @game_id, @conference_id)

UPDATE Games 
SET conference_id=@conference_id
WHERE game_id = @game_id


go
create proc member_add_conference_review_to_a_conference
@email varchar(15),
@conference_id int,
@content varchar(1000)
AS
if ( exists(
select * from Conferences_attended_by_Members where @email=email and @conference_id=conference_id
))
insert Conference_Reviews(conference_id,email,content)
values(@conference_id,@email,@content)

go
CREATE PROCEDURE Search_Review

@R_id int

AS

select *
from Conference_Reviews
where @R_id= conferences_reviews_id
go
create proc list_comments_conference
@conference_review_id int
AS
Select crc.comment_id, crc.comment_text, crc.comment_date,m.email
From Conference_Reviews cr, Conference_Reviews_Comments crc, Members m
Where crc.conferences_reviews_id=@conference_review_id and crc.conferences_reviews_id=cr.conferences_reviews_id and m.email=crc.member_email

go
create proc add_comment_conference_review
@conference_id int,
@conferences_reviews_id int,
@member_email varchar(50),
@comment_text varchar(50)
AS
Declare @conference_email varchar(50)
select @conference_email= email
from Conference_Reviews 
where  conferences_reviews_id = @conferences_reviews_id

insert Conference_Reviews_Comments(conference_id, Conferences_reviews_id, conference_email,member_email, comment_text)
values(@conference_id, @Conferences_reviews_id, @conference_email,@member_email, @comment_text)

go
CREATE proc delete_comments_conference
@email varchar(50),
@conference_review_id int,
@comment_id int
AS
Delete From Conference_Reviews_Comments
where member_email=@email and comment_id=@comment_id and conferences_reviews_id=@conference_review_id

go

CREATE proc add_comment_topic
@topic_id int,
@email varchar(50),
@content varchar(50)

AS
if(exists(select * from Communities_joined_by_Members where @email=email))
begin
insert Topic_Comments(topic_id,email,content)
values(@topic_id,@email,@content)

end

go
CREATE proc add_comment_topic1
@cid int,
@topic_id int,
@email varchar(50),
@content varchar(50),
@tmp bit output
AS
if(exists(select * from Communities_joined_by_Members where @email=email))
begin
insert Topic_Comments(topic_id,email,content)
values(@topic_id,@email,@content)
set @tmp='1';
end
else 
set @tmp='0';

go
/* 13. Post a topic on a community that I have joined. I should be able to provide a title and a descriptive
text for the topic. */

CREATE proc post_topic_comm
@email varchar(50),
@community_id int,
@name varchar(15),
@topic_description varchar(150),
@tmp bit output
AS
if(exists(select * from Communities_joined_by_Members where @email=email and @community_id=community_id))
begin
insert Topics(name, topic_description)
values(@name, @topic_description)

declare @id int

select @id = SCOPE_IDENTITY() 
insert Communities_posted_Normal_Users_Topic (topic_id,email, community_id)
values (@id,@email, @community_id)

set @tmp='1';
end
else 
set @tmp='0';
go
/*14. Delete a topic that I have posted.*/

create proc delete_topic
@topic_id int,
@email varchar(50),
@tmp bit output
AS 
if(exists(select * from Communities_posted_Normal_Users_Topic where topic_id=@topic_id and email=@email))
begin
delete 
from Topics
where @topic_id=topic_id
delete 
from Communities_posted_Normal_Users_Topic
where topic_id=@topic_id and email=@email

set @tmp='1';
end

else set @tmp='0';

go

create proc point_2_1
AS
select c.community_id,c.name,c.community_description
from Communities c
where c.accepts is not null

go
create proc point_2_2
@cid int
AS
select t.topic_id,t.name
from Communities_posted_Normal_Users_Topic ct,Topics t
where ct.community_id=@cid and t.topic_id=ct.topic_id

go
/*12 Request to create a community providing its name and description.*/

create proc requestCommunity
@email varchar(50),
@name varchar(15),
@community_descrption varchar(100),
@is_normal_user bit output
AS
if(exists(select * from Normal_Users n where n.email=@email))
begin
insert Communities(name,community_description)
Values(@name,@community_descrption)
declare @community_id int
Select @community_id=community_id
From Communities
where name = @name and community_description=@community_descrption
insert Communities_created_by_Normal_users(email,comm_id)
Values(@email,@community_id)

set @is_normal_user='1';
end

else set @is_normal_user='0';

go
CREATE proc third_point
AS
select c.community_id,c.name
from Communities c
where c.accepts is not null

go
/* 11. join a community */

alter proc join_comm
@email varchar(50),
@community_id int
AS
insert Communities_joined_by_Members(email, community_id)
values(@email, @community_id)
go
create proc topic_content_database
@topic_id int
AS
select t.name,t.topic_description
from Topics t
where t.topic_id=@topic_id

go
create proc topic_content_database_1
@topic_id int
AS
select cp.email
from Communities_posted_Normal_Users_Topic cp
where cp.topic_id=@topic_id

go
CREATE proc topic_content_database_2
@topic_id int
AS
select t.comment_id,t.email,t.content,t.comment_date
from Topic_Comments t
where t.topic_id=@topic_id


go
/*14. Delete a topic that I have posted.*/

create proc delete_topic
@topic_id int,
@email varchar(50),
@tmp bit output
AS 
if(exists(select * from Communities_posted_Normal_Users_Topic where topic_id=@topic_id and email=@email))
begin
delete 
from Topics
where @topic_id=topic_id
delete 
from Communities_posted_Normal_Users_Topic
where topic_id=@topic_id and email=@email

set @tmp='1';
end

else set @tmp='0';

go
create proc delete_comment_topic
@email varchar(50),
@topic_id int,
@topic_comment_id int,
@tmp bit output
AS 
if(exists(select * from Topic_Comments t where @email=t.email and t.comment_id=@topic_comment_id and t.topic_id=@topic_id))
begin
delete from Topic_Comments
where email=@email and topic_id = @topic_id and comment_id = @topic_comment_id

set @tmp='1';
end

else 
set @tmp='0';

go
/* 12. View a community I have joined */

create proc view_community_info
@community_id int
AS
select name, community_description
from Communities
where community_id=@community_id

go
create proc point_2_1
AS
select c.community_id,c.name,c.community_description
from Communities c
where c.accepts is not null
go
create proc point_2_2
@cid int
AS
select t.topic_id,t.name
from Communities_posted_Normal_Users_Topic ct,Topics t
where ct.community_id=@cid and t.topic_id=ct.topic_id

go
/*5 Accept/Reject the friendship request of other normal users.*/
create proc accept_rejectFriendship(
@email_reciever varchar(50),
@email_sender varchar(50),
@accepts bit)
AS
Update Normal_Users_friends_Normal_Users
Set accepts=@accepts
WHERE email1=@email_sender AND email2 = @email_reciever
go

/*getting the info of the user*/
Create Proc getinfoVerifiedReviewer(
@email varchar(50))
AS
Select ns.f_name as 'first_name',ns.l_name as 'last_name', ns.experience_years as 'years', ns.starting_date as 'd_o_s',m.preferred_genre as 'genre'
From Verified_Reviewers ns, Members m
where @email = ns.email and m.email = ns.email
go
Create Proc getinfoNormalUser(
@email varchar(50))
AS
Select ns.f_name as first_name,ns.l_name as last_name, ns.age as age, ns.date_of_birth as d_o_b,m.preferred_genre as genre
From Normal_Users ns, Members m
where @email = ns.email and m.email = ns.email
go
/*getting the info of the user*/
Create Proc getinfoDevelopment(
@email varchar(50))
AS
Select ns.name as 'name', ns.company as 'company', ns.foundation_date as 'd_o_f',m.preferred_genre as 'genre'
From Development_Teams ns, Members m
where @email = ns.email and m.email = ns.email
go
/*getting the type*/
CREATE proc getusertype(
@email varchar(50),
@type int output)
As
if(Exists(Select* From Normal_Users where @email=email))
Set @type = 0;

if(Exists(Select* From Verified_Reviewers where @email=email))
Set @type = 1;

if(Exists(Select* From Development_Teams where @email=email))
Set @type = 2;
go
/*login procedure*/
CREATE PROC loginProcedure(
@email varchar(50),
@password varchar(15),
@count Int output)
AS
if(EXISTS(SELECT * from Members where email=@email and @password = member_password))
Set @count = 1
else
Set @count = 0
go
/*pending friends*/
create proc pendingFriends(@email varchar(50))
AS
SELECT email1
From Normal_Users_friends_Normal_Users
WHere email2=@email AND accepts is NULL
go
/*sign UP*/
create proc sign_up
	@email varchar(50),
	@password varchar(15),
	@preferred_genre varchar(15),
	@type varchar(30)
	AS
	insert Members(email,member_password,preferred_genre)
		values (@email,@password,@preferred_genre)

		if(@type='Normal User')
		insert Normal_Users(email)
			values (@email)

		else if(@type='Verified Reviewer')
		insert Verified_Reviewers(email)
			values (@email)

		else if(@type='Devolopment Team')
		insert Development_Teams(email)
			values (@email)

go
/*8 Send and receive multiple messages through threads with my friends.*/
create proc send_message(
@email_sender varchar(50),
@email_receiver varchar(50),
@message varchar(100))
AS
if(exists(
select *
from Normal_Users_friends_Normal_Users
where email1=@email_sender AND email2=@email_receiver AND accepts=1
union
SELECT *
from Normal_Users_friends_Normal_Users
where email1=@email_receiver AND email2=@email_sender AND accepts=1
))
INSERT Normal_User_Messages(email_send,email_received,text_message)
Values(@email_sender,@email_receiver,@message)
go
create proc view_message(
@email1 varchar(50),
@email2 varchar(50))
AS
Select *
from Normal_Users n, Normal_User_Messages nm
Where (n.email=@email1 AND nm.email_send= @email1 AND email_received=@email2) OR (n.email=@email2 AND nm.email_received= @email1 AND nm.email_send=@email2)
go
/*search game*/
create proc search_game
 @gname varchar(15)
 AS
 select *
 from Games
 where name like '%'+@gname+'%'
 go
 /*search community*/
 create proc search_communties
@comname varchar(15)
AS 
select *
from Communities
where name like '%'+@comname+'%' and accepts=1
go
/*search conferences*/


create proc search_conference
@cname varchar(15)
AS
select *
from Conferences
where name like '%'+@cname+'%'
go
/*search verified reviewer*/
create proc search_verified
@name varchar(15)
AS
select *
from Verified_Reviewers
where f_name like '%' +@name+'%' or l_name like '%'+@name+'%'
go
/*search development teams*/

create proc search_development
@dname varchar(15)
AS 
select *
from Development_teams
where name like '%'+@dname+'%'
go
/*development team update*/
create proc update_development_teams
@email varchar(50),
@name varchar(15),
@foundation_date date,
@company varchar(15)
AS
Update Development_Teams
SET name = @name , foundation_date = @foundation_date, company=@company
where email= @email
go
/*verified reviewer update*/
create proc update_verfiedReviewer
  @email varchar(50),
  @f_name varchar(15),
  @l_name varchar(15),
  @years_experience int
  AS
  declare @startyear int
  set @startyear = year(current_timestamp)-@years_experience
  update Verified_Reviewers
  set f_name=@f_name , l_name=@l_name, starting_date=CONCAT('1/1', '/', @startyear)
  where email = @email
  go
  /*update normal user*/
  
create proc update_NormalUsers(
@email varchar(50),
@f_name varchar(15),
@l_name varchar(15),
@date_birth date)
AS 
Update Normal_Users
SET f_name=@f_name ,l_name=@l_name,date_of_birth=@date_birth
Where email=@email
go
/*6 View my friend list.*/
create proc view_friends(
@email varchar(50))
AS
SELECT email2 AS'email', n.f_name, n.l_name
from Normal_Users_friends_Normal_Users, Normal_Users n
where email1=@email AND accepts=1 AND n.email = email2
union
SELECT email1 AS 'email' ,n.f_name, n.l_name
from Normal_Users_friends_Normal_Users, Normal_Users n
where email2=@email AND accepts=1 AND n.email = email1
go

create proc third_point_side_kick
@cid int,
@tmp bit output
AS
if(exists(select * from Communities_joined_by_Members cj where cj.community_id=@cid))
set @tmp='1';
else set @tmp='0';

create proc get_game_type
@game_id int,
@game_type varchar(20) OUTPUT
AS 
if (exists(select * from Sport_Games where game_id=@game_id))
set @game_type = 'Sport Game'
if (exists(select * from Action_Games where game_id=@game_id))
set @game_type = 'Action Game'
if (exists(select * from Strategy_Games where game_id=@game_id))
set @game_type = 'Stategy Game'
if (exists(select * from RPG_Games where game_id=@game_id))
set @game_type = 'RPG Game'

declare @game_type varchar(20)
exec get_game_type 3, @game_type OUTPUT
print @game_type

go
create proc get_game_type_info
@game_id int,
@game_type_info varchar(50) OUTPUT
AS
if (exists(select * from Sport_Games where game_id=@game_id))
begin
select @game_type_info = sport_type from Sport_Games
where @game_id = game_id
end
if (exists(select * from Action_Games where game_id=@game_id))
select @game_type_info = sub_genre from Action_Games
where @game_id = game_id
if (exists(select * from Strategy_Games where game_id=@game_id AND real_time=1))
set @game_type_info = 'Real Time'
if (exists(select * from Strategy_Games where game_id=@game_id AND real_time=0))
set @game_type_info = 'Not Real Time'
if (exists(select * from RPG_Games where game_id=@game_id AND story_line=0 AND pvp=0))
set @game_type_info = 'No Story Line, not pvp'
if (exists(select * from RPG_Games where game_id=@game_id AND story_line=0 AND pvp=1))
set @game_type_info = 'No Story Line, pvp'
if (exists(select * from RPG_Games where game_id=@game_id AND story_line=1 AND pvp=0))
set @game_type_info = 'With Story Line, not pvp'
if (exists(select * from RPG_Games where game_id=@game_id AND story_line=1 AND pvp=1))
set @game_type_info = 'With Story Line, pvp'

declare @game_type_info varchar(50)
exec get_game_type_info 7,@game_type_info OUTPUT
print @game_type_info

go
create proc get_development_teams_names
@game_id int
AS
select d.name,d.email from Development_Teams d, Development_Teams_develops_Games dtdg
where dtdg.game_id=@game_id AND dtdg.email=d.email

exec get_development_teams_names 1

go
create proc check_verified_reviewer
@email varchar(50),
@verified bit OUTPUT
AS
if (exists(select * from Verified_Reviewers where email=@email))
set @verified = 1
else 
set @verified = 0

declare @verified bit
exec check_verified_reviewer 'hazem@mn.com', @verified output
print @verified 

go
create proc get_screenshot_description 
@screenshot_id int,
@screenshot_description varchar(100) OUTPUT
AS
select @screenshot_description = screenshot_description from Games_Screenshots where screenshot_id = @screenshot_id

declare @screenshot_description varchar(100)
exec get_screenshot_description 4 , @screenshot_description output
print @screenshot_description

go
create proc get_video_description 
@video_id int,
@video_description varchar(100) OUTPUT
AS
select @video_description = video_description from Games_Videos where video_id = @video_id

declare @video_description varchar(100)
exec get_video_description 4 , @video_description output
print @video_description

go
create proc get_game_review_info
@game_review_id int
AS select * from Game_Reviews where @game_review_id = game_review_id

exec get_game_review_info 3

go

 create proc view_game_info
 @game_id int
 AS
 select * from Games
 where game_id=@game_id

create proc overall_rating
@game_id int,
@rating int OUTPUT
AS
DECLARE @graphics_avg int
DECLARE @graphics_sum int
DECLARE @uniqueness_avg int
DECLARE @uniqueness_sum int
DECLARE @designlvl_avg int
DECLARE @designlvl_sum int
DECLARE @interactivity_avg int
DECLARE @interactivity_sum int

DECLARE @numOfRatings int

select @graphics_sum=SUM(graphics),@uniqueness_sum=SUM(uniqueness), 
@designlvl_sum=SUM(designlvl),@interactivity_sum=SUM(interactivity), @numOfRatings=COUNT(*)
from Games_rated_by_Members
where @game_id=game_id
set @graphics_avg= @graphics_sum/@numOfRatings
set @uniqueness_avg= @uniqueness_sum/@numOfRatings
set @designlvl_avg= @designlvl_sum/@numOfRatings
set @interactivity_avg= @interactivity_sum/@numOfRatings

set @rating = (@graphics_avg + @uniqueness_avg + @designlvl_avg + @interactivity_avg)/4

create proc view_game_reviews
@game_id int
AS 
select * from Game_Reviews where game_id=@game_id

create proc recommend_game(
@email_send varchar(50),
@email_receive varchar(50),
@game_name varchar(15))
AS
declare @game_id int
select @game_id=game_id
from Games
Where name=@game_name
insert Games_recommended_by_Normal_Users_to_Normal_Users(email_send,email_received,game_id)
Values (@email_send,@email_receive,@game_id)

 create proc view_game_screenshots
 @game_id int
 AS 
 select * from Games_Screenshots where game_id=@game_id

 create proc view_game_videos
 @game_id int
 AS 
 select * from Games_Videos where game_id=@game_id

create proc add_game_review
@content varchar(1000),
@game_id int,
@email varchar(50)
AS
if(exists(
select *
from Verified_Reviewers
where email=@email and verified=1
))
insert Game_Reviews(game_id, email,content) 
values (@game_id, @email,@content)

create proc Rate_game
@g_id int,
@m_email varchar(50),
@graphics_r int,
@interactivity_r int,
@uniqueness_r int,
@designlvl_r int
AS
Insert Games_rated_by_Members(email,game_id,graphics,uniqueness,interactivity,designlvl)
values (@m_email, @g_id, @uniqueness_r,@graphics_r, @interactivity_r, @designlvl_r)

create proc get_game_review_info
@game_review_id int
AS select * from Game_Reviews where @game_review_id = game_review_id