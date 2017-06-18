
Create trigger Verified_Reviewer_Update on Verified_Reviewers
For Update
AS
if(Exists(select * from Verified_Reviwers Where verified =0))
Delete From Verified_Reviewers
Where verified=0
go
Create trigger Development_team_verfiy on Development_Teams
For Update
AS
if(Exists(select * from Development_Teams Where verified =0))
Delete From Development_Teams
Where verified=0
go
Create trigger Communities_accpets on Communities
for Update
AS
if(Exists(Select* From Communities where accepts=0))
delete From Communities
Where accepts = 0
go
/*Member */
/*1.Sign up by providing my email, password, preffered game genre and the type of my membership
(normal user, verified reviewer or a development team).*/
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
/*2.Search by name for different games, conferences, communities, verified reviewers and development
teams.*/
 create proc search_game
 @gname varchar(15)
 AS
 select *
 from Games
 where name=@gname

 go
create proc search_conference
@cname varchar(15)
AS
select *
from Conferences
where name=@cname
go
create proc search_communties
@comname varchar(15)
AS 
select *
from Communities
where name=@comname
go

create proc search_verified
@verified_fname varchar(15),
@verified_lname varchar(15)
AS
select *
from Verified_Reviewers
where f_name=@verified_fname and l_name=@verified_lname
go
create proc search_development
@dname varchar(15)
AS 
select *
from Development_teams
where name=@dname
go
/*3. View a game and preview its information which includes its name, release date, age limit, the team
who devloped it (if any), screenshots, videos, and a list of reviews written for that game. For a
strategy game, I should be able to see if it’s real-time or not. For an action game, I should see
its sub-genre. For a sport game, I should see its type. For an RPG game, I should see if it has a
storyline or not, and if it has the option of PvP or not.*/

 create proc view_game_info
 @game_id int
 AS
 select * from Games
 where game_id=@game_id
 go
 create proc view_game_type_info 
 @game_id int
 AS
 if(exists(
 	select * From Sport_Games where game_id=@game_id))
 select sport_type from Sport_Games where game_id=@game_id
 if(exists(
 	select * From Action_Games where game_id=@game_id))
 select sub_genre from Action_Games where game_id=@game_id
 if(exists(
 	select * From Strategy_Games where game_id=@game_id))
 select real_time from Strategy_Games where game_id=@game_id
 if(exists(
 	select * From RPG_Games where game_id=@game_id))
 select story_line,pvp from RPG_Games where game_id=@game_id
 go
 create proc view_game_developer
 @game_id int
 AS 
 select DT.name,DT.email from Development_Teams DT, Development_Teams_develops_Games DTDG
 where DT.email=DTDG.email and game_id=@game_id
 	go
 create proc view_game_screenshots
 @game_id int
 AS 
 select * from Games_Screenshots where game_id=@game_id
 go
 create proc view_game_videos
 @game_id int
 AS 
 select * from Games_Videos where game_id=@game_id
 go
 create proc view_game_reviews
 @game_id int
 AS 
 select * from Game_Reviews where game_id=@game_id
 go
/* 4. Rate a game based on the following criteria: graphics, interactivity, uniqueness, and level design.*/

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
go
/* 5. View the overall rating of a game where the overall rating is calculated as the average of ratings
provided by members of the network for that game.*/
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
go
/* 6. view a list of reviews about the games i have rated */
create proc view_reviews
@email varchar(50)
AS
select *
from Game_Reviews GR, Verified_Reviewers VR, Games_rated_by_Members MRG
where GR.email= VR.email and GR.game_id=MRG.game_id and MRG.email=@email
order by VR.experience_years
go
/* 7. View a conference and preview its name, when and where it is held. Also, I should be able to
view the list of the development teams that presented their games in that conference, along with
their game names. Moreover, I should be able to view the list of games that were debuted in that
conference, as well as the list of reviews on that conference. */

create proc preview_conference
 @conference_id int
 AS
 Select name, starting_date, venue
 From Conferences c
 Where c.conference_id = @conference_id
 go
 create proc development_team_conference
 @conference_id int
 AS
 Select d.name,d.email, g.name
 FROM Conferences_presented_at_by_Development_Teams_Games dtc, Games g, Development_Teams d
 Where dtc.conference_id = @conference_id AND  dtc.email = d.email AND g.game_id = dtc.game_id
 go
 create proc games_at_conference
 @conference_id int 
 AS SELECT g.name 
 From Games g
 Where conference_id = @conference_id
 go
 create proc view_conference_reviews
 @conference_id int
 AS
 SELECT *
 from Conference_Reviews 
 WHERE conference_id=@conference_id
 go
/* 8. Add a conference to my list of attended conferences. */

create proc member_add_conference
@email varchar(15),
@conference_id int
AS
insert Conferences_attended_by_Members(email,conference_id)
values (@email,@conference_id)
go
/* 9 Add a conference review to a conference that I have attended.*/

create proc member_add_conference_review_to_a_conference
@email varchar(15),
@conference_id int,
@content varchar(1000)
AS
if ( exists(
select * from Conferences_attended_by_Members where @email=email and @conference_id=conference_id
)
)
insert Conference_Reviews(conference_id,email,content)
values(@conference_id,@email,@content)
go
/* 10. Delete a conference review I have written.*/
create proc delete_conference_review
@email varchar(15),
@conferences_reviews_id int
AS 
delete from Conference_Reviews_Comments
where @email=member_email and @conferences_reviews_id=conferences_reviews_id

delete from Conference_Reviews 
where @email=email and @conferences_reviews_id=conferences_reviews_id

go
/* 11. join a community */

create proc join_comm
@email varchar(50),
@community_id int
AS
insert Communities_joined_by_Members(email, community_id)
values(@email, @community_id)
go
/* 12. View a community I have joined */

create proc view_community_info
@community_id int
AS
select name, community_description
from Communities
where community_id=@community_id
go
create proc view_community_members
@community_id int
AS
select email from Communities_joined_by_Members 
where community_id=@community_id
go
create proc view_topics_on_community
@community_id int
AS
select t.name, t.topic_description 
from Communities_posted_Normal_Users_Topic cpt, Topics t
where cpt.community_id=@community_id AND cpt.topic_id=t.topic_id
go
/* 13. Post a topic on a community that I have joined. I should be able to provide a title and a descriptive
text for the topic. */

create proc post_topic_comm
@email varchar(50),
@community_id int,
@name varchar(15),
@topic_description varchar(150)
AS
if(exists(select * from Communities_joined_by_Members where @email=email and @community_id=community_id))
begin
insert Topics(name, topic_description)
values(@name, @topic_description)

declare @id int

select @id = SCOPE_IDENTITY() 
insert Communities_posted_Normal_Users_Topic (topic_id,email, community_id)
values (@id,@email, @community_id)
end
go 
/*14. Delete a topic that I have posted.*/

create proc delete_topic
@topic_id int,
@email varchar(50)
AS 
if(exists(select * from Communities_posted_Normal_Users_Topic where topic_id=@topic_id and email=@email))
delete 
from Topics
where @topic_id=topic_id
delete 
from Communities_posted_Normal_Users_Topic
where topic_id=@topic_id and email=@email
go
/*15.Add a comment on a conference review, a game review, or a topic posted in a community that I
have joined.*/
/* add comment game */
create proc add_comment_game_review
@game_review_id int,
@game_id int,
@game_email varchar(50),
@email varchar(50),
@comment_text varchar(150)
AS
insert Game_Review_Comments(game_review_id,game_id,game_email,email,comment_text)
values(@game_review_id, @game_id, @game_email, @email, @comment_text)
go
/*add comment topic */
create proc add_comment_topic
@topic_id int,
@email varchar(50),
@content varchar(50)
AS
insert Topic_Comments(topic_id,email,content)
values(@topic_id,@email,@content)
go
/*add comment conference */
create proc add_comment_conference_review
@conference_id int,
@conferences_reviews_id int,
@conference_email varchar(50),
@member_email varchar(50),
@comment_text varchar(50)
AS
insert Conference_Reviews_Comments(conference_id, Conferences_reviews_id, conference_email,member_email, comment_text)
values(@conference_id, @Conferences_reviews_id, @conference_email,@member_email, @comment_text)
go
/* 16.View the list of comments on a conference review, a game review or a topic posted on a community
that I have joined.*/
Create proc list_comments_conference
@conference_review_id int
AS
Select crc.comment_text, crc.comment_date,m.email
From Conference_Reviews cr, Conference_Reviews_Comments crc, Members m
Where crc.conferences_reviews_id=@conference_review_id and crc.conferences_reviews_id=cr.conferences_reviews_id and m.email=crc.member_email
go
Create proc list_comments_Game
@game_review_id int
AS
Select grg.comment_text,grg.comment_date ,m.email
From Game_Reviews gr, Game_Review_Comments grg, Members m
Where gr.game_review_id=@game_review_id and grg.game_review_id=gr.game_review_id and grg.email=m.email
go
create proc list_comment_topic
@email varchar(50),
@topic_id int
AS 
Select tc.content,tc.comment_date , tc.email
From Communities_joined_by_Members mjc,Topic_Comments tc,Communities_posted_Normal_Users_Topic nptc
Where mjc.email = @email and mjc.community_id=nptc.community_id and  tc.topic_id=nptc.topic_id and tc.topic_id=@topic_id 
go

/* 17.Delete a comment that I have posted on a conference review, a game review or a topic posted on a
community that I have joined.*/
Create proc delete_comments_conference
@email varchar(50),
@conference_review_id int,
@comment_id int
AS
Delete From Conference_Reviews_Comments
where member_email=@email and comment_id=@comment_id and conferences_reviews_id=@conference_review_id
go
Create proc delete_comments_Game
@email varchar(50),
@game_email varchar(50),
@game_id int,
@game_review_comment_id int

AS
delete from Game_Review_Comments
where email=@email and game_review_comment_id=@game_review_comment_id and game_email=@game_email and game_id = @game_id
go
create proc delete_comment_topic
@email varchar(50),
@topic_id int,
@topic_comment_id int
AS 
delete from Topic_Comments
where email=@email and topic_id = @topic_id and comment_id = @topic_comment_id 
go
/*18 Show top 5 members with the most attended conferences in common with me.*/
create proc top_5_members_attend_conf_with_me
@email varchar(50)
AS
select top(5) t1.email, count(*)
from Conferences_attended_by_Members t1 , Conferences_attended_by_Members t2
where t2.conference_id= t1.conference_id and t2.email<>t1.email and t2.email=@email 
group by t1.email
order by count(*) desc
go
/*19 Show top 10 game recommendations based on how many times they have been recommended by
other members of the system. This should exclude games that I have rated or have already been
recommended.*/

create proc top_10_game_recommendations
@email varchar(50)
AS
select top(10) t3.name
from  Games_recommended_by_Normal_Users_to_Normal_Users t2, Games t3
where t3.game_id NOT IN(Select t1.game_id
                    From Games_rated_by_Members t1
					Where t1.email=@email
    ) and t3.game_id Not IN(Select t4.game_id
						From Games_recommended_by_Normal_Users_to_Normal_Users t4
						Where @email=t4.email_received) and t3.game_id = t2.game_id
group by t3.name 
order by count(*) desc
go
/*“As a normal user, I should be able to ...”*/


/*1 Update my account information by providing my ﬁrst name, last name and date of birth.*/

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
/*2 Send friendship requests to other members of the system.*/
Create proc request_friendship(
@email_sender varchar(50),
@email_reciever varchar(50))
AS
Insert Normal_Users_friends_Normal_Users(email1,email2)
Values(@email_sender,@email_reciever)
go
/*3 Search the members of the network to ﬁnd friends.*/
create proc search_for_friends(
@name varchar(31))
AS
Select f_name,email
From Normal_Users
Where f_name like '%'+@name+'%'

go
/*4 View a list of pending friendship requests.*/
CREATE proc pendingFriends(@email varchar(50))
AS
SELECT email2
From Normal_Users_friends_Normal_Users
WHere email1=@email AND accepts is NULL
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
/*6 View my friend list.*/
Create proc view_friends(
@email varchar(50))
AS
SELECT email2
from Normal_Users_friends_Normal_Users
where email1=@email AND accepts=1
union
SELECT email1
from Normal_Users_friends_Normal_Users
where email2=@email AND accepts=1
go
/*7 View a friend’s proﬁle. I should be able to view his/her information (ﬁrst name, last name and age), a list of the conferences that he/she have attended, 
as well as the list of games that he/she have rated along with the rating he/she provided for each game.*/

Create proc view_friendsProfile(
@email varchar(50))
AS
SELECT n.f_name, n.l_name, n.age, c.name,g.name,mg.*
from Normal_Users n, Conferences_attended_by_Members  mc,Conferences c,Games_rated_by_Members mg, Games g
WHERE n.email=@email AND n.email = mc.email AND mc.email = mg.email And c.conference_id=mc.conference_id AND g.game_id=mg.game_id

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
/*9 View my diﬀerent thread messages.*/
create proc view_message(
@email1 varchar(50),
@email2 varchar(50))
AS
Select n.f_name, n.l_name ,nm.text_message
from Normal_Users n, Normal_User_Messages nm
Where n.email=@email1 AND nm.email_send= @email1 AND email_received=@email2
union
Select n.f_name, n.l_name ,nm.text_message
from Normal_Users n, Normal_User_Messages nm
Where n.email=@email2 AND nm.email_received= @email1 AND nm.email_send=@email2

go
/*10 Recommend a game to another normal user.*/
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
go
/*11 View the recommendations for diﬀerent games that I have recieved.*/
create proc view_recommendation(
 @email varchar(50))
 AS
 SELECT g.name
 From Games_recommended_by_Normal_Users_to_Normal_Users ngn, Games g
 Where ngn.game_id = g.game_id AND ngn.email_received=@email
 go
/*12 Request to create a community providing its name and description.*/

create proc requestCommunity
@email varchar(50),
@name varchar(15),
@community_descrption varchar(100)
AS
insert Communities(name,community_description)
Values(@name,@community_descrption)
declare @community_id int
Select @community_id=community_id
From Communities
where name = @name and community_description=@community_descrption
insert Communities_created_by_Normal_users(email,comm_id)
Values(@email,@community_id)

go
/*“As a veriﬁed reviewer, I should be able to ...”*/

/*1 Update my account information by providing my ﬁrst name, last name and years of experience.*/
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
/*2 Add a game review.*/
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
go
/*3 Delete a game review that I have written.*/
create proc delete_game_review
@game_id int,
@email varchar(50)
AS
delete 
from Game_Reviews
where game_id=@game_id and @email=email

go
/*4 View my top 10 game reviews based on the number of comments on them.*/
Create proc top_reviews
  @email varchar(50)
  AS
  Select TOp(10)gr.content, gr.review_date,Count(grc.game_review_comment_id) as comment_count
  From  Verified_Reviewers vr,Game_Reviews gr, Game_Review_Comments grc
  where vr.email= gr.email and vr.email = @email and gr.game_review_id=grc.game_review_id
  Group by gr.content, gr.review_date
  order by  Count(grc.game_review_comment_id) desc

  go
/*“As a development team, we should be able to ...”*/

/*1 Update our account’s information by providing our team name, date of formation and company we work for.*/
Create proc update_development_teams
@email varchar(50),
@name varchar(15),
@foundation_date date,
@company varchar(15)
AS
Update Development_Teams
SET name = @name , foundation_date = @foundation_date, company=@company
where email= @email

go
/*2 Add a game to the list of games that we have developed.*/
CREATE proc add_game_developed(
@game_id int,
@email varchar(50))
AS
if(exists(
select *
from Development_Teams
where email=@email and verified=1
))
INSERT Development_Teams_develops_Games(game_id,email)
Values(@game_id, @email)

go
/*3 Add screenshots and videos to a game that we have developed.*/
CREATE proc add_Screenshots(
@game_id int,
@description varchar(150))
AS
INSERT Games_Screenshots(game_id,screenshot_description)
values(@game_id,@description)
go
CREATE proc add_Videos(
@game_id int,
@description varchar(150))
AS
INSERT Games_Videos(game_id,video_description)
values(@game_id,@description)
go

/*4 Add a conference to the list of conferences that we have presented in. We should also be able to add the game(s) that we presented in that conference.*/
Create proc Development_TeamPresents(
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
/*“As a system administrator, I should be able to ...”*/

/*1 View a list of requests to form communities.*/
create proc list_of_requests
AS
select name,community_description
from Communities
where accepts is null
go

/*2 Accept/Reject a request to create a community. In case of acceptance, the community should be created with the provided information (name and description).*/
create proc community_acceptance
@community_id int,
@accepts bit
As
update Communities
Set accepts = @accepts
Where @community_id=community_id
go

/*3 Verify members who signed up as veriﬁed reviewers or development teams.*/
create proc verfiy_rev_dev
@email varchar(50),
@verified bit
As
if(
exists(
select *
from Development_Teams
where email=@email
))
update Development_Teams
set verified=@verified
else
update Verified_Reviewers
set verified=@verified
go
/*4 Create a conference with its information (names, start date, end date, duration and venue).*/
CREATE Proc create_Conference(
@name varchar(50),
@start_date date,
@end_date date,
@venue varchar(15))

AS
INSERT Conferences(name, Starting_date, ending_date,venue)
VALUES(@name, @start_date,@end_date,@venue)

go
/*5 Create a game with its information (name, release date, age limit, and an intial rating equals to 0).*/
create proc createGame(
@name varchar(15),
@release_date  date,
@age_limit int)
AS
INSERT Games(name,release_date,age_limit)
Values(@name, @release_date, @age_limit)

go
/*6 Delete a community, a conference, or a game.*/
CREATE proc delete_Games(
@game_id int)
AS
DELETE FROM Games
Where game_id = @game_id
go
CREATE proc delete_Communities(
@community_id int)
AS
DELETE FROM Communities
Where community_id = @community_id
go
CREATE proc delete_Conferences(
@conference_id int)
AS
DELETE FROM Conferences
Where conference_id = @conference_id

go