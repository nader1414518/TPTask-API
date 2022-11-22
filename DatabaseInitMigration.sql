USE tp_task

CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar] (50),
	[Phone] [varchar] (14),
	[Email] [varchar](50) NOT NULL,
	[Password] [varchar](20) NOT NULL,
	[Type] [varchar](20) Not Null,
	CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED
	(
		[UserId] ASC
	)
);

INSERT [dbo].[Users] (Email, Password, Type, Name, Phone) VALUES ('admin@tp.com', '12345678', 'admin', 'Admin', '+201023281829');


CREATE TABLE [dbo].[Records](
	[RecordId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Name] [varchar] (50),
	[Phone] [varchar] (14),
	[Email] [varchar](50) NOT NULL,
	[ImgUrl] [varchar](max) Not Null,
	CONSTRAINT [PK_Records] PRIMARY KEY CLUSTERED
	(
		[RecordId] ASC
	),
	CONSTRAINT [FK_UserId] FOREIGN KEY (UserId)
    REFERENCES Users(UserId)
);