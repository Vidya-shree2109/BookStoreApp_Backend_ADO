use BookStore_DB

Create Table Admin_tbl
(
AdminId int identity(1,1) primary key,
FullName varchar (255) not null,
EmailId varchar (255) NOT NULL,
Password varchar (255) NOT NULL,
Phone BigInt NOT NULL,
)
Insert into Admin_tbl
values('Shree Vidya','shree123@gmail.com','Shree@1234','9876453458');

select * from Admin_tbl


---------------- Store procedure for Admin login --------------

CREATE PROCEDURE AdminLoginSP
(
	@EmailId varchar(255),
	@Password varchar(255)
)
as
begin
	select * from Admin_tbl where EmailId=@EmailId and Password=@Password; 
end

exec AdminLoginSP 'shree123@gmail.com' , 'Shree@1234'
