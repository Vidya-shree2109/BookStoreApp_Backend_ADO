use BookStore_DB

create table Address_tbl(
AddressId int primary key identity,
UserId int  not null FOREIGN KEY (UserId) REFERENCES Users_tbl(UserId),
AddressType int not null FOREIGN KEY (AddressType) REFERENCES AddressType(AddressTypeId),
FullAddress varchar(255) unique not null,
City varchar(255) not null,
State varchar(255) not null,
)

select * from Address_tbl


create table AddressType(
AddressTypeId int primary key identity,
AddressType varchar(20) not null
)

select * from AddressType

insert into AddressType(AddressType) values ('Other')




--stored procedure for AddAddress
create procedure AddAddressSP(
@UserId int,
@AddressType int, 
@FullAddress varchar(255),
@City varchar(255),
@State varchar(255)
)
As
Begin try
   insert into Address_tbl(UserId,AddressType,FullAddress,City,State) values(@UserId,@AddressType,@FullAddress,@City,@State)
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH

Select * from Address_tbl

--======================================================================

--stored procedure for GetAllAddress
create procedure GetAllAddressSP(
@UserId int
)
As
Begin try
select 
a.AddressId,a.AddressType,a.FullAddress,a.City,a.State,u.UserId,u.FullName,u.Phone
from Address_tbl a INNER JOIN Users_tbl u ON a.UserId = u.UserId
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

--stored procedure for DeleteAddressById
create procedure DeleteAddressByIdSP(
@AddressId int,
@UserId int
)
As
Begin try
delete from Address_tbl where AddressId = @AddressId AND UserId = @UserId
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

--stored procedure for UpdateAddressbyId
create procedure UpdateAddressbyIdSP(
@AddressId int,
@UserId int,
@AddressType int, 
@FullAddress varchar(255),
@City varchar(255),
@State varchar(255)
)
As
Begin try
update Address_tbl set AddressType=@AddressType,FullAddress=@FullAddress,City=@City,State=@State where UserId=@UserId
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

--stored procedure for GetAddressById
create procedure GetAddressByIdSP(
@AddressId int,
@UserId int
)
As
Begin try
select 
a.AddressId,a.AddressType,a.FullAddress,a.City,a.State,u.UserId,u.FullName,u.Phone
from Address_tbl a INNER JOIN Users_tbl u ON a.UserId = u.UserId where AddressId=@AddressId
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH