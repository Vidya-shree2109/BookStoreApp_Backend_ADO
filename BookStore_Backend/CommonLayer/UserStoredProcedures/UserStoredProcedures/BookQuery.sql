use BookStore_DB

create table Book_tbl(
BookId int primary key identity,
BookName varchar(255) unique not null,
Author varchar(255) unique not null,
Description varchar(255) not null,
Quantity int not null,
Price money not null,
DiscountPrice money not null,
TotalRating float,
RatingCount int,
BookImg varchar(255)
)
Exec sp_help Book_tbl
select * from Book_tbl




----------------------------------------------------stored procedure for AddBook-------------------------------------

Alter procedure AddBookSP(
@BookName varchar(255),
@Author varchar(255),
@Description Nvarchar(255),
@Quantity int,
@Price money,
@DiscountPrice money,
@TotalRating float,
@RatingCount int,
@BookImg varchar(255)
)
As
Begin try
insert into Book_tbl(BookName,Author,Description,Quantity,Price,DiscountPrice,TotalRating,RatingCount,BookImg) values(@BookName,@Author,@Description,@Quantity,@Price,@DiscountPrice,@TotalRating,@RatingCount,@BookImg)
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH

-------------------------------------------stored procedure for Get All Books-----------------------------

create procedure GetAllBooksSP
As
Begin try
select * from Book_tbl
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH


------------------------------------------------stored procedure for Update Book------------------------------------

create procedure UpdateBooksSP(
@BookId int,
@BookName varchar(255),
@Author varchar(255),
@Description Nvarchar(255),
@Quantity int,
@Price money,
@DiscountPrice money,
@TotalRating float,
@RatingCount int,
@BookImg varchar(255)
)
As
Begin try
update Book_tbl set BookName=@BookName,Author=@Author,Description=@Description,Quantity=@Quantity,Price=@Price,DiscountPrice=@DiscountPrice,TotalRating=@TotalRating,RatingCount=@RatingCount,BookImg=@BookImg where BookId=@BookId
select * from Book_tbl where BookId=@BookId
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH


---------------------------------stored procedure for GetBooksByIdSP----------------------------------------

create procedure GetBooksByIdSP(
@BookId int
)
As
Begin try
select * from Book_tbl where BookId=@BookId
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH



------------------------------------------stored procedure for deleteBook-------------------------------------------

create procedure DeleteBookSP(
@BookId int
)
As
Begin try
delete from Book_tbl where BookId=@BookId
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH