--------------------------------STORED PROCEDURE FOR USER REGISTRATION--------------------------

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



--------------------------------STORED PROCEDURE FOR GET ALL USERS---------------------------------




CREATE PROCEDURE GetAllUsersSP
As
Begin
select * from Users_tbl
end


-------------------------------------STORED PROCEDURE FOR USER LOGIN---------------------------------------


ALTER PROCEDURE UserLoginSP(
@EmailId varchar(255),
@Password varchar(255)
)
As
Begin try
select * from Users_tbl where EmailId=@EmailId and Password = @Password
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH

exec UserLoginSP 'saividya123@gmail.com' ,'Saividya@1234'


-------------------------------------STORED PROCEDURE FOR FORGOT PASSWORD ---------------------------------------



CREATE PROCEDURE UserForgotPasswordSP(
@EmailId varchar(255)
)
As
Begin 
select * from Users_tbl where EmailId=@EmailId 
end 



-------------------------------------STORED PROCEDURE FOR RESET PASSWORD ---------------------------------------



CREATE PROCEDURE UserResetPasswordSP(
@EmailId varchar(50),
@Password varchar(20)
)
As
Begin
update Users_tbl set Password=@Password where EmailId=@EmailId
end