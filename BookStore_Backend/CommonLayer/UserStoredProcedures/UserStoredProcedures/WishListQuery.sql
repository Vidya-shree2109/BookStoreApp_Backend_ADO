use BookStore_DB

create table WishList_tbl(
WishListId int primary key identity,
UserId int  not null FOREIGN KEY (UserId) REFERENCES Users_tbl(UserId),
BookId int  not null FOREIGN KEY (BookId) REFERENCES Book_tbl(BookId),
)




-------------------------------------------stored procedure for AddBookTOCart------------------------------
Alter procedure AddToWishListSP(
@UserId int,
@BookId int
)
As
Begin try
DECLARE @Wcount int,@Ccount int;
SET @Ccount=(select count(CartId) from Cart_tbl where UserId IN (@UserId) AND BookId IN (@BookId))
SET @Wcount=(select count(WishListId) from WishList_tbl where UserId IN (@UserId) AND BookId IN (@BookId))
IF(@Ccount = 0)
  IF(@Wcount = 0)
   insert into WishList_tbl(UserId,BookId) values(@UserId,@BookId)
  ELSE
   print'Check if Book is available or Its already in WishList!!'
ElSE
  print'Your Book is already in cart!!'
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH
select * from WishList_tbl
select * from Book_tbl
select * from Cart_tbl


--======================================================================

--stored procedure for GetAllWishList
Alter procedure GetAllWishListSP(
@UserId int
)
As
Begin try
select 
w.WishListId,b.BookId,b.BookName,b.Author,b.Description,b.Price,b.DiscountPrice,b.BookImg
from WishList_tbl w INNER JOIN Book_tbl b ON w.BookId = b.BookId where UserId = @UserId
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH

--======================================================================

--stored procedure for DeleteWishListItem
Alter procedure DeleteWishListItemSP(
@WishListId int,
@UserId int
)
As
Begin try
delete from WishList_tbl where WishListId = @WishListId AND UserId = @UserId
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH

--======================================================================

--stored procedure for GetWishListItemByBookId
Alter procedure GetWishListItemByBookIdSP(
@UserId int,
@WishListId int
)
As
Begin try
select 
w.WishListId,b.BookId,b.BookName,b.Author,b.Description,b.Price,b.DiscountPrice,b.BookImg
from WishList_tbl w INNER JOIN Book_tbl b ON w.BookId = b.BookId where UserId = @UserId
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH