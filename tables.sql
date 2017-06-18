
create table Members(
email varchar(50),
member_password varchar(15),
preferred_genre varchar(15)
CONSTRAINT pk_email PRIMARY KEY (email)
)



create table Normal_Users(
email varchar(50),
f_name varchar(15),
l_name varchar (15),
date_of_birth date,
age as year(current_timestamp)-year(date_of_birth),
CONSTRAINT pk_normal_users PRIMARY KEY (email),
CONSTRAINT fk_email_normal_users FOREIGN KEY (email) references Members ON DELETE cascade on Update cascade
)
go

create table Verified_Reviewers(
email varchar(50),
f_name varchar(15),
l_name varchar (15),
starting_date date,
experience_years as  year(current_timestamp)-year(starting_date),
verified bit,
CONSTRAINT pk_verfied_reviewers PRIMARY KEY (email),
CONSTRAINT fk_email_verfied_reviewers FOREIGN KEY (email) references Members ON DELETE cascade on Update cascade
)
 create table Development_Teams(
 email varchar(50),
 name varchar (15),
 company varchar(15),
 foundation_date date,
 verified bit,
 CONSTRAINT pk_development_teams PRIMARY KEY (email),
 CONSTRAINT fk_email_development_teams FOREIGN KEY (email) references Members ON DELETE cascade on Update cascade
)

create table Conferences(
conference_id int PRIMARY KEY IDENTITY,
Starting_date date,
ending_date date,
duration as DateDiff(day,starting_date,ending_date ),
venue varchar(15),
name varchar (15)
)


create table Games(
game_id int PRIMARY KEY IDENTITY,
name varchar(15),
age_limit int,
release_date date,
conference_id int,
CONSTRAINT fk_conference_id_game FOREIGN KEY (conference_id) REFERENCES Conferences ON DELETE CASCADE ON UPDATE CASCADE
)

create table Sport_Games(
game_id int,
sport_type varchar(15),
CONSTRAINT pk_sport PRIMARY KEY (game_id),
CONSTRAINT fk_game_id_sport FOREIGN KEY (game_id) REFERENCES Games ON DELETE CASCADE ON UPDATE CASCADE
)


create table Action_Games(
game_id int,
sub_genre varchar(15),
CONSTRAINT pk_action PRIMARY KEY (game_id),
CONSTRAINT fk_game_id_action FOREIGN KEY (game_id) REFERENCES Games ON DELETE CASCADE ON UPDATE CASCADE
)


create table Strategy_Games(
game_id int,
real_time bit,
CONSTRAINT pk_startegy PRIMARY KEY (game_id),
CONSTRAINT fk_strategy_game FOREIGN KEY (game_id) REFERENCES Games ON DELETE CASCADE ON UPDATE CASCADE
)



create table RPG_Games(
game_id int,
story_line bit,
pvp bit,
CONSTRAINT pk_rpg PRIMARY KEY (game_id),
CONSTRAINT fk_game_id_rpg FOREIGN KEY (game_id) REFERENCES Games ON DELETE CASCADE ON UPDATE CASCADE
)


create table Communities(
community_id int PRIMARY KEY IDENTITY,
name varchar(15),
community_description varchar (150),
accepts bit
)

create table Topics(
topic_id int PRIMARY KEY IDENTITY,
name varchar(15),
topic_description varchar (150)
)

create table Normal_Users_friends_Normal_Users(
email1 varchar(50)  default 'null',
email2 varchar(50) default 'null',
accepts bit,
CONSTRAINT pk_friends PRIMARY KEY(email1,email2),
CONSTRAINT fk_User1 foreign key(email1) references Normal_Users ,
CONSTRAINT fk_User2 foreign key(email2) references Normal_Users )



create table Communities_created_by_Normal_users(
comm_id int,
email varchar(50),
CONSTRAINT pk_creates PRIMARY KEY(comm_id),
CONSTRAINT fk_Comm foreign KEY(comm_id) references Communities on update cascade on delete cascade,
CONSTRAINT fk_User foreign KEY(email) references Normal_Users on update cascade on delete cascade
)

create table Games_rated_by_Members(
email varchar(50),
game_id int,
graphics int,
uniqueness int,
interactivity int,
designlvl int,
CONSTRAINT pk_rates PRIMARY KEY(email,game_id),
CONSTRAINT fk_User_rates foreign KEY(email) references Members on update cascade on delete cascade,
CONSTRAINT fk_Game_rates foreign KEY(game_id) references Games on update cascade on delete cascade,

check(graphics>=0 and graphics<=10),
check(uniqueness>=0 and uniqueness<=10),
check(interactivity>=0 and interactivity<=10),
check(designlvl>=0 and designlvl<=10)
)


create table Communities_posted_Normal_Users_Topic(
email varchar(50),
community_id int, 
topic_id int,
constraint pk_post PRIMARY KEY (topic_id),
CONSTRAINT fk_members_post FOREIGN KEY(email) REFERENCES Members  ON DELETE CASCADE ON UPDATE CASCADE,
CONSTRAINT fk_topics_post FOREIGN KEY(topic_id) REFERENCES Topics  ON DELETE CASCADE ON UPDATE CASCADE,
CONSTRAINT fk_communities_post FOREIGN KEY(community_id) REFERENCES Communities ON DELETE CASCADE ON UPDATE CASCADE
)




Create table Communities_joined_by_Members(
email varchar(50),
community_id int,
constraint pk_join PRIMARY KEY (email, community_id),
CONSTRAINT fk_members_join FOREIGN KEY(email) REFERENCES Members  ON DELETE CASCADE ON UPDATE CASCADE,
CONSTRAINT fk_communities_join FOREIGN KEY(community_id) REFERENCES Communities ON DELETE CASCADE ON UPDATE CASCADE
)


create table Development_Teams_develops_Games(
email varchar(50),
game_id int,
constraint pk_develops PRIMARY KEY (email, game_id),
CONSTRAINT fk_development_team_develops FOREIGN KEY(email) REFERENCES Development_Teams  ON DELETE CASCADE ON UPDATE CASCADE,
CONSTRAINT fk_game_developed FOREIGN KEY(game_id) REFERENCES Games ON DELETE CASCADE ON UPDATE CASCADE
)


create table Conferences_presented_at_by_Development_Teams_Games(
email varchar(50),
game_id int,
conference_id int NOT NULL,
constraint pk_present PRIMARY KEY (email, game_id),
CONSTRAINT fk_development_team_present FOREIGN KEY(email) REFERENCES Development_Teams  ON DELETE CASCADE ON UPDATE CASCADE,
CONSTRAINT fk_game_present FOREIGN KEY(game_id) REFERENCES Games ON DELETE CASCADE ON UPDATE CASCADE,
CONSTRAINT fk_conferences_present FOREIGN KEY(conference_id) REFERENCES Conferences )


create table Conferences_attended_by_Members(
email varchar(50),
conference_id int,
constraint pk_attend PRIMARY KEY (email, conference_id),
CONSTRAINT fk_members_attend FOREIGN KEY(email) REFERENCES Members  ON DELETE CASCADE ON UPDATE CASCADE,
CONSTRAINT fk_conferences_attend FOREIGN KEY(conference_id) REFERENCES Conferences ON DELETE CASCADE ON UPDATE CASCADE
)



Create table Games_Videos(
video_id int IDENTITY,
game_id int,
submitted_date As current_timestamp ,
video_description varchar(150),
Constraint pk_videos PRIMARY KEY (game_id, video_id),
CONSTRAINT fk_games_video FOREIGN KEY(game_id) REFERENCES Games  ON DELETE CASCADE ON UPDATE CASCADE
)




Create table Games_Screenshots(
screenshot_id int IDENTITY,
game_id int,
submitted_date AS current_timestamp ,
screenshot_description varchar(150),
Constraint pk_screenshots PRIMARY KEY (game_id, screenshot_id),
CONSTRAINT fk_games_screenshot FOREIGN KEY(game_id) REFERENCES Games  ON DELETE CASCADE ON UPDATE CASCADE
)

create table Topic_Comments(
comment_id int IDENTITY,
email varchar(50),
topic_id int,
content varchar(50),
comment_date As current_timestamp,
CONSTRAINT pk_topic_comments PRIMARY KEY (comment_id,email, topic_id),
CONSTRAINT fk_members_topic FOREIGN KEY(email) REFERENCES Members  ON DELETE CASCADE ON UPDATE CASCADE,
CONSTRAINT fk_topics FOREIGN KEY(topic_id) REFERENCES Topics  ON DELETE CASCADE ON UPDATE CASCADE
)

create table Conference_Reviews(
conferences_reviews_id int IDENTITY,
email varchar(50),
conference_id int,
review_date As current_timestamp,
content varchar(1000),
CONSTRAINT pk_conference_reviews PRIMARY KEY (conferences_reviews_id,email, conference_id),
CONSTRAINT fk_members_reviewd FOREIGN KEY(email) REFERENCES Members  ON DELETE CASCADE ON UPDATE CASCADE,
CONSTRAINT fk_conferences_reviewed FOREIGN KEY(conference_id) REFERENCES Conferences  ON DELETE CASCADE ON UPDATE CASCADE
)


create table Conference_Reviews_Comments(
comment_id int IDENTITY,
conferences_reviews_id int,
conference_email varchar(50),
conference_id int,
member_email varchar(50),
comment_date As current_timestamp,
comment_text varchar(50),
CONSTRAINT pk_conference_comments PRIMARY KEY (comment_id,conferences_reviews_id,conference_email, conference_id, member_email),
CONSTRAINT fk_members_commented FOREIGN KEY(member_email) REFERENCES Members  ON DELETE CASCADE ON UPDATE CASCADE,
CONSTRAINT fk_conference_commented FOREIGN KEY(conferences_reviews_id ,conference_email,conference_id) REFERENCES Conference_Reviews
)


 create table Game_Reviews(
 game_review_id int IDENTITY,
 game_id int ,
 email varchar(50),
 review_date As current_timestamp,
 content varchar(1000),
 CONSTRAINT pk_game_review PRIMARY KEY (game_review_id, game_id, email) ,
 CONSTRAINT fk_game_reviewed FOREIGN KEY (game_id) REFERENCES Games ON DELETE CASCADE ON UPDATE CASCADE,
 CONSTRAINT  fk_verfied_reviewers FOREIGN KEY (email) REFERENCES Verified_Reviewers ON DELETE CASCADE ON UPDATE CASCADE
 )
 

 
 create table Game_Review_Comments(
 game_review_comment_id int PRIMARY KEY IDENTITY,
  game_review_id int ,
 game_id int ,
 game_email varchar(50),
 email varchar(50),
 comment_date AS current_timestamp,
 comment_text varchar(150),
 CONSTRAINT fk_game_review FOREIGN KEY (game_review_id, game_id, game_email) REFERENCES Game_Reviews ON DELETE CASCADE ON UPDATE CASCADE,
 CONSTRAINT  fk_normal_users_comment FOREIGN KEY (email) REFERENCES Normal_Users 
 )


 create table Normal_User_Messages(
  message_id int IDENTITY,
 email_send varchar(50),
 email_received varchar(50),
 text_message varchar(100),
 sent_date As current_timestamp,
 Constraint pk_normal_user_messages PRIMARY KEY (message_id, email_send, email_received),
 Constraint fk_normal_user_sent_messages FOREIGN KEY (email_send) REFERENCES Normal_Users,
 Constraint fk_normal_user_received_messages FOREIGN KEY (email_received) REFERENCES Normal_Users)

 create table Games_recommended_by_Normal_Users_to_Normal_Users(
 email_send varchar(50),
 email_received varchar(50),
 game_id int,
 Constraint pk_recommends PRIMARY KEY (game_id, email_send, email_received),
 Constraint fk_game_recommended FOREIGN KEY (game_id) REFERENCES Games ON DELETE CASCADE ON UPDATE CASCADE,
 Constraint fk_normal_user_sent_games FOREIGN KEY (email_send) REFERENCES Normal_Users ON DELETE CASCADE ON UPDATE CASCADE,
 Constraint fk_normal_user_received_games FOREIGN KEY (email_received) REFERENCES Normal_Users)

