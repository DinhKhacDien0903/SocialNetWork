drop database if exists NetworkSocial;
create database NetworkSocial;
use NetworkSocial;

create table Users(
	UserID uniqueIdentifier primary key default NewSequentialID(),
	UserName varchar(30) not null,
	PasswordHash varchar(64) not null,
	Email varchar(256),
	CreatedAt datetime2 default getDate(),
	UpdateAt datetime2,
	IsActive bit default 0,
	LastLogin datetime2
);

create table UserInfor(
	UserID uniqueidentifier primary key,
	FirstName nvarchar(50),
	LastName nvarchar(50),
	Address nvarchar(255),
	Phone varchar(20),
	AvatarUrl varchar(255),
	Gender bit,
	DateOfBirth date
	--foreign key on delete casede
)

create table Roles(
	RoleID uniqueIdentifier primary key default NewSequentialID(),
	RoleName nvarchar(50)
);

create table UserRole(
	UserID uniqueIdentifier not null,
	RoleID uniqueIdentifier not null
);

create table Follow(
	FollowerID uniqueidentifier not null,
	FollowingID uniqueidentifier not null,
	Status smallint,
	FollowAt Date default getDate(), 
	--fore key on delete casede and unique
);

create table Post(
	PostID uniqueidentifier primary key default NewSequentialID(),
	UserID uniqueidentifier not null,
	Content nvarchar(max) not null,
	PostAt datetime2,
	UpdatedAt datetime2,
	IsDelete bit default 0
	--fore key on delete casede
)

create table ImageOfPost(
	ImageOfPostID uniqueidentifier primary key default NewSequentialID(),
	PostID uniqueidentifier not null,
	ImgUrl varchar(255),
	CreatedAt datetime2 default getDate(),
	IsDeleted bit default 0
	--fore key on delete casede
);

create table Comment(
	CommentID uniqueidentifier primary key default NewSequentialID(),
	UserID uniqueidentifier not null,
	PostID uniqueidentifier not null,
	ParentCommentID uniqueidentifier null,
	Content nvarchar(max) not null,
	CreatedAt datetime2 default getDate(),
	UpdatedAt datetime2,
	IsDelete bit default 0
	--fore key on delete casede
);

create table Messages(
	MessageID uniqueidentifier primary key default NewSequentialID(),
	Content nvarchar(max) not null,
	SenderID uniqueidentifier not null,
	ReciverID uniqueidentifier not null,
	IsDeleted bit default 0,
	SendAt datetime2 default getDate(),
	UpdatedAt datetime2,
);

create table MessageStatus(
	MessageID uniqueidentifier primary key,
	IsRead bit default 0,
	IsRecived bit default 0,
	ReadAt datetime2
	--them khoa chinh va khoa phu 
);

create table MessageImage(
	MessageImageID uniqueidentifier primary key default NewSequentialID(),
	MessageID uniqueidentifier not null,
	ImageUrl varchar(255),
	IsDeleted bit default 0
);

create table GroupChat(
	GroupChatID uniqueidentifier primary key default NewSequentialID(),
	GroupName nvarchar(50),
	Description nvarchar(255),
	CreatedAt datetime2 default getDate(),
	UpdatedAt datetime2
);

create table GroupChatMemeber(
	GroupChatID uniqueidentifier not null,
	UserID uniqueidentifier not null,
	JoinAt datetime2 default getDate(),
	IsAdmin bit default 0,
	IsLeaved bit default 0
	--them khoa chinh ket hop, on delete cased
);

create table GroupChatMessage(
	GroupChatMessageID uniqueidentifier primary key default NewSequentialID(),
	GroupChatID uniqueidentifier not null,
	UserID uniqueidentifier not null,
	Content nvarchar(max) not null,
	IsDeleted bit default 0,
	SendAt datetime2 default getDate(),
	UpdatedAt datetime2,
	--them khoa chinh ket hop, on deleted case
);

create table GroupChatMessageStatus(
	GroupChatMessageID uniqueidentifier not null,
	GroupChatID uniqueidentifier not null,
	UserID uniqueidentifier not null,
	IsRead bit default 0,
	IsRecived bit default 0,
	ReadAt datetime2
	--them khoa chinh va khoa phu 
);

create table GroupChatMessageImage(
	GroupChatMessageImageID uniqueidentifier primary key default NewSequentialID(),
	GroupChatMessageID uniqueidentifier not null,
	UserID uniqueidentifier not null,
	ImageUrl varchar(255),
	IsDeleted bit default 0
	--add forekey and delete on case
);

create table EmotionType(
	EmotionTypeID uniqueidentifier primary key default NewSequentialID(),
	EmotionName nvarchar(20)
);

create table Reaction(
	ReactionID uniqueidentifier primary key default NewSequentialID(),
	UserID uniqueidentifier not null,
	EmotionTypeID uniqueidentifier not null,
	ReactedAt datetime2 default getDate(),
	UpdateAt datetime2,
	IsDeleted bit default 0
)

create table ReactionPost(
	ReactionID uniqueidentifier not null,
	PostID uniqueidentifier not null,
);

create table ReactionComment(
	ReactionID uniqueidentifier not null,
	CommentID uniqueidentifier not null,
);

create table ReactionMessage(
	ReactionID uniqueidentifier not null,
	MessageID uniqueidentifier not null,
);

create table ReactionGroupChatMessage(
	ReactionID uniqueidentifier not null,
	GroupChatMessageID uniqueidentifier not null,
);
--add constraint
alter table UserInfor add constraint FK_Users_UserInfor foreign key (UserID) references Users(UserID) on delete cascade;

alter table UserRole add constraint FK_Users_UserRole foreign key (UserID) references Users(UserID) on delete cascade,
						 constraint FK_Role_UserRole foreign key (RoleID) references Roles(RoleID) on delete cascade,
						 constraint PK_UserRole primary key (UserID, RoleID);

alter table Follow add constraint FK_Follow_Follower foreign key (FollowerID) references Users(UserID) on delete cascade,
					constraint FK_Follow_Following foreign key (FollowingID) references Users(UserID) on delete no action,
					constraint PK_Follow primary key(FollowerID, FollowingID);

alter table Post add constraint FK_Users_Post foreign key(UserID) references Users(UserID) on delete cascade;

alter table ImageOfPost add  constraint FK_Post_ImageOfPost foreign key(PostID) references Post(PostID) on delete cascade;
--them trigger cho truong hop xoa commnet cha se xoa comment con tuong tu voi user
alter table Comment add constraint FK_User_Comment foreign key(UserID) references Users(UserID) on delete no action,
						constraint FK_Post_Comment foreign key(PostID) references Post(PostID) on delete cascade,
						constraint FK_Parent_Comment foreign key(ParentCommentID) references Comment(CommentID),
						constraint UniqueConstraint Unique(CommentID, UserID);

alter table Messages add constraint FK_User_Messages foreign key(SenderID) references Users(UserID) on delete cascade,
						constraint FK_Recevier_Message foreign key(ReciverID) references Users(UserID) on delete no action;

alter table MessageStatus add constraint FK_Message_MessageStatus foreign key(MessageID) references Messages(MessageID) on delete no action;

alter table MessageImage add constraint FK_Message_MessageImage foreign key(MessageID) references Messages(MessageID) on delete no action;

alter table GroupChatMemeber add constraint FK_User_GroupChatMember foreign key(UserID) references Users(UserID) on delete cascade,
							constraint FK_GroupChat_GroupChatMember foreign key(GroupChatID) references GroupChat(GroupChatID) on delete cascade,
							constraint PK_GroupChatMemeber primary key (UserID, GroupChatID);

alter table GroupChatMessage add constraint FK_GroupChat_GroupChatMessage foreign key(GroupChatID) references GroupChat(GroupChatID) on delete cascade,
							constraint FK_USer_GroupChatMessage foreign key(UserID) references Users(UserID) on delete cascade;

--them trigger cho viec xu ly xoa user va xoa message se xoa status
alter table GroupChatMessageStatus add constraint FK_GroupChat_GroupChatMessageStatus foreign key(GroupChatID) references GroupChat(GroupChatID) on delete cascade,
							constraint FK_User_GroupChatMessageStatus foreign key(UserID) references Users(UserID) on delete no action,
							constraint FK_GroupCharMessage_GroupChatMessageStatus foreign key(GroupChatMessageID) references GroupChatMessage(GroupChatMessageID) on delete no action,
							constraint PK_GroupChatMessageStatus primary key(GroupChatID, UserID,GroupChatMessageID);

							--add trigger tuong tu
alter table GroupChatMessageImage add constraint FK_User_GroupChatMessageImage foreign key(UserID) references Users(UserID) on delete no action,
							constraint FK_GroupCharMessage_GroupChatMessageImage foreign key(GroupChatMessageID) references GroupChatMessage(GroupChatMessageID) on delete no action;

alter table Reaction add constraint FK_User_Reaction foreign key (UserID) references Users(UserID) on delete cascade,		
						 constraint FK_EmotionType_Reaction foreign key (EmotionTypeID) references EmotionType(EmotionTypeID);	

alter table ReactionPost add constraint PK_ReactionPost primary key (ReactionID,PostID),
							 constraint FK_Reaction_ReactionPost foreign key (ReactionID) references Reaction(ReactionID),
							 constraint FK_Post_ReactionPost foreign key (PostID) references Post(PostID) on delete cascade;

alter table ReactionComment add constraint PK_ReactionComment primary key (ReactionID,CommentID),
							 constraint FK_Reaction_ReactionComment foreign key (ReactionID) references Reaction(ReactionID),
							 constraint FK_Comment_ReactionComment foreign key (CommentID) references Comment(CommentID) on delete cascade;

alter table ReactionMessage add constraint PK_ReactionMessage primary key (ReactionID,MessageID),
							 constraint FK_Reaction_ReactionMessage foreign key (ReactionID) references Reaction(ReactionID),
							 constraint FK_Message_ReactionMessage foreign key (MessageID) references Messages(MessageID) on delete cascade;

alter table ReactionGroupChatMessage add constraint PK_ReactionGroupChatMessage primary key (ReactionID,GroupChatMessageID),
							 constraint FK_Reaction_ReactionGroupChatMessage foreign key (ReactionID) references Reaction(ReactionID),
							 constraint FK_GroupChatMessage_ReactionGroupChatMessage foreign key (GroupChatMessageID) references GroupChatMessage(GroupChatMessageID) on delete cascade;