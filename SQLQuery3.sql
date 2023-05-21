USE Final;
create Table Admin(
Admin_ID int Not Null primary key ,
Admin_Name varchar(20),
);
alter table Admin 
add Admin_pass varchar(20) ;
alter table Admin 
add Admin_email varchar(30);

Insert Into  .Admin( Admin_ID,Admin_Name)
Values('1','Amr Fayez'),('2', 'Youssef Ahmed'),('3','Malak Walid'),('4','Youssef Khaled'),('5','Habiba Ahmed');


create Table Student(
Student_ID int Not NULL primary key,
Student_Name varchar(30) Not NUll,
Password varchar(50) Not Null,
Email varchar(100) Null,
);
insert into Student(Student_ID,Student_Name,Password,Email)
Values ('20210262','Omar Ayman','1234','omar@gmail.com'),('20210302','kamal thrwat','12345','kamal@gmail.com'),('20210016','Ahmed Khaled','19876','Ahmed@icloud.com');

Create Table StudentPhone(
Student_ID int Not NULL,
Phone_num varchar(30) Not Null,
Foreign Key (Student_ID) References Student(Student_ID)
);
insert into StudentPhone(Student_ID,Phone_num)
values('20210262','01022257711'),('20210262','01022257790'),('20210302','01150868659'),('20210302','01150868658'),('20210016','01064397295');

create Table Dependent(
Admin_ID int Not Null ,
Student_ID int Null,
Dep_FName varchar(30) Null,
Dep_LName varchar(30)Null,
A_Password Varchar(30)Null,
Name AS  ( Dep_FName + '  ' + Dep_LName),
constraint FK_Admin_ID Foreign Key (Admin_ID) References Admin (Admin_ID),
constraint FK_Student_ID Foreign Key (Student_ID) References Student (Student_ID) 
);

insert into Dependent(Admin_ID,Student_ID,Dep_FName,Dep_LName,A_Password)
values ('1','20210262','Omar','Ayman','12345'),('2','20210302','Kamal','tharwat','1234');

Create Table Book(
Book_ID int Not Null Primary key,
Book_Name varchar(20) Not NULL,
Book_Author varchar(30) Not Null,
price float Not NULL,
Number_Of_Copies int Not NUll,
);

insert into Book(Book_ID,Book_Name,Book_Author,price,Number_Of_Copies)
values('1','rich dad','robert','200.50','15'),('2','the art','mark manson','170','9'),('3','It starts with us','collen Hover','124.75','17'),('4','Tale of Two cities','charles Dickens','79','3'),('5','Great Expectations','charles Dickens','150','8');

Create Table Category(
Book_ID int Not Null,
Book_category varchar(30) not null,
CONSTRAINT FK_Category_BookID FOREIGN KEY (Book_ID) REFERENCES Book (Book_ID),
);
Insert into Category (Book_ID,Book_category)
values( '1','personal finnance'),('2','self help'),('3','Drama'),('4','Novel'),('5','Novel');


Create table BorrowedBook(
Book_ID int Not Null,
Student_ID int Not NULl,
From_D Date Not NUll,
Num_Of_days int Not Null,
TO_D Date Not Null,
Foreign Key (Book_ID) References Book (Book_ID),
Foreign Key (Student_ID) References Student (Student_ID),
);
Insert into  BorrowedBook(Book_ID,Student_ID,From_D,Num_Of_days,TO_D)
values ('1','20210262', '21-April-2023',DATEDIFF(Day,'21-April-2023','26-April-2023'),'26-April-2023'),('5','20210302', '17-May-2023',DATEDIFF(Day,'17-May-2023','26-May-2023'),'26-May-2023');
