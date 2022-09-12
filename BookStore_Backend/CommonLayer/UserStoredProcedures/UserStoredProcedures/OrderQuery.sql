Use BookStore_DB

create table Order_tbl(
OrderId int primary key identity,
UserId int not null FOREIGN KEY (UserId) REFERENCES Users_tbl(UserId),
BookId int not null FOREIGN KEY (BookId) REFERENCES Book_tbl(BookId),
AddressId int not null FOREIGN KEY (AddressId) REFERENCES Address_tbl(AddressId),
OrderQuantity int not null,
TotalOrderPrice money not null,
OrderDate datetime Default getdate()   --sysdatetime and getdate will give same output 
)

select * from Order_tbl




-----------------------------------------------------stored procedure for adding orders----------------------------


Alter procedure AddOrderSP(
@CartId int,
@AddressId int
)
As
Begin try
Select c.CartId,c.UserId,c.BookId,b.Quantity,b.DiscountPrice,c.BookQuantity into tempdetails from Cart_tbl c Inner join Book_tbl b on c.BookId = b.BookId where CartId = @CartId
Declare @UserId int = (select UserId from tempdetails)
Declare @BookId int = (select BookId from tempdetails)
DECLARE @BookPrice float =(select DiscountPrice from tempdetails)
DECLARE @QuantityAvail int =(select Quantity from tempdetails)
Declare @OrderQuantity int = (select BookQuantity from tempdetails)

IF ((@UserId != 0) AND (@QuantityAvail > @OrderQuantity))
BEGIN
    Declare @TotalOrderPrice money = (@BookPrice * @OrderQuantity)
	DECLARE @BookPresentInWishList int =(select Count(WishListId) from WishList where BookId= @BookId)	
	IF(@BookPresentInWishList>0)
	BEGIN
	 delete from WishList_tbl where BookId = @BookId and UserId=@UserId
    END
	 insert into Orders_tbl(UserId,BookId,AddressId,OrderQuantity,TotalOrderPrice) values(@UserId,@BookId,1,@OrderQuantity,@TotalOrderPrice)
	 IF(@@ROWCOUNT > 0)
	 BEGIN
	   delete from Cart_tbl where CartId = @CartId
	   DECLARE @Booksleft int = @QuantityAvail - @OrderQuantity
	   update Book_tbl SET Quantity = @Booksleft where BookId = @BookId
	 END
	 drop table tempdetails
END
ELSE
	drop table tempdetails
    print 'Books available in stock are only'+CAST(@BookId AS VARCHAR(255))
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

--stored procedure for GetAllOrders
Alter procedure GetAllOrdersSP(
@UserId int
)
As
Begin try
select 
o.OrderId,o.UserId,o.BookId,o.AddressId,o.OrderQuantity,o.TotalOrderPrice,o.OrderDate,b.BookName,b.Author,b.BookImg
from Order_tbl o INNER JOIN Book_tbl b ON o.BookId = b.BookId where UserId = @UserId
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH