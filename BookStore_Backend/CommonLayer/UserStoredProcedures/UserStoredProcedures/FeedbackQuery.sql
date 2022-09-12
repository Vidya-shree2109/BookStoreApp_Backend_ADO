Use BookStore_DB

create table Feedback_tbl(
FeedbackId int primary key identity,
Rating float not null,
Comment varchar(max) not null,
BookId int not null foreign key (BookId) references Book_tbl(BookId),
UserId int not null foreign key (UserId) references Users_tbl(UserId)
)

select * from Feedback_tbl




-----------------------------------------------------stored procedure for Adding Feedback-----------------------------------------

create procedure AddFeedbackSP(
@Rating float,
@Comment varchar(max),
@BookId int,
@UserId int
)
as
DECLARE @TotalRating float;
BEGIN TRY
	if(not exists(select * from Feedback_tbl where BookId=@BookId and UserId=@UserId))
	BEGIN
		insert into Feedback_tbl(Rating,Comment,BookId,UserId) values(@Rating,@Comment,@BookId,@UserId)
		set @TotalRating = (select AVG(TotalRating) from Book_tbl where BookId = @BookId);
		Update Book_tbl set TotalRating = @TotalRating, RatingCount = (RatingCount+1) where BookId = @BookId;
	END
END TRY
BEGIN CATCH
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH

select * from Feedback_tbl
select * from Book_tbl

--=================================================================

-- stored procedures to GetAllFeddbacksbyBookId

create procedure GetFeedbackByBookId(
@BookId int
)
As
BEGIN TRY
	select f.FeedbackId,f.Comment,f.BookId,f.Rating,f.UserId,u.FullName
	from Feedback_tbl f inner join Users_tbl u on f.UserId = u.UserId where BookId = @BookId
END TRY
BEGIN CATCH
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH

--======================================================================

--stored procedure for deleteFeedback ByBookId for a Book
create procedure DeleteFeedbackByIdSP(
@FeedbackId int
)
As
Begin try
delete from Feedback_tbl where FeedbackId = @FeedbackId
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH