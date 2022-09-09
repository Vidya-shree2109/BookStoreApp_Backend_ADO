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
BookImage varchar(255)
)
Exec sp_help Books
select * from Book_tbl





----------------------------------------- STORED PROCEDURE FOR ADD BOOK ----------------------------------------------

CREATE PROCEDURE AddBookSP(
@BookName varchar(255),
@Author varchar(255),
@Description varchar(255),
@Quantity int,
@Price money,
@DiscountPrice money,
@TotalRating float,
@RatingCount int,
@BookImage varchar(255) 
)  
As
Begin
insert into Book_tbl(BookName,Author,Description,Quantity,Price,DiscountPrice,TotalRating,RatingCount,BookImage) values(@BookName,@Author,@Description,@Quantity,@Price,@DiscountPrice,@TotalRating,@RatingCount,@BookImage)
end



------------------------------------------ STORED PROCEDURE FOR GET ALL BOOKS ----------------------------------------------

ALTER PROCEDURE GetAllBooksSP
As
Begin
select * from Book_tbl
end

exec GetAllBooksSP




-------------------------------------------STORED PROCEDURE FOR GET BOOK BY ID ----------------------------------------------
CREATE PROCEDURE GetBookByIdSP(
@BookId int
)
As
Begin
select * from Book_tbl where BookId=@BookId
end



--------------------------------------------STORED PROCEDURE TO UPDATE BOOK----------------------------------------------
CREATE PROCEDURE UpdateBookSP(
@BookId int,
@BookName varchar(255),
@Author varchar(255),
@Description varchar(255),
@Quantity int,
@Price money,
@DiscountPrice money,
@TotalRating float,
@RatingCount int,
@BookImage varchar(255) 
)  
As
Begin
update Book_tbl set BookName=@BookName,Author=@Author,Description=@Description,Quantity=@Quantity,Price=@Price,DiscountPrice=@DiscountPrice,TotalRating=@TotalRating,RatingCount=@RatingCount,BookImage=@BookImage where BookId=@BookId
select * from Book_tbl where BookId=@BookId
end




--------------------------------------------- STORED PROCEDURE TO DELETE BOOK ----------------------------------------------

CREATE PROCEDURE DeleteBookSP(
@BookId int
)
As
Begin
delete from Book_tbl where BookId=@BookId
end