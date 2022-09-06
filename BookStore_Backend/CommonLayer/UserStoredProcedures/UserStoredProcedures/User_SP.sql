CREATE PROCEDURE UserRegistrationSP(
@FullName varchar(255),
@EmailId varchar(255),
@Password varchar(255),
@Phone bigint
)
As
Begin 

insert into Users_tbl(FullName,EmailId,Password,Phone) values(@FullName,@EmailId,@Password,@Phone)

end 

select * from Users_tbl;