CREATE database BookStore_DB

Use BookStore_DB

CREATE table Users_tbl(
UserId int primary key identity,
FullName varchar(255) not null,
EmailId varchar(255) not null,
Password varchar(255) not null,
Phone bigint not null
)

Select *from Users_tbl;
