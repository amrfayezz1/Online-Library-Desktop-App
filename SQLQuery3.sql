USE Final;
create Table Admin(
Admin_ID int Not Null primary key ,
Admin_Name varchar(20),
);
alter table Admin 
add Admin_PW varchar(20) ;
alter table Admin 
add Admin_email varchar(30);

Insert Into  .Admin( Admin_ID,Admin_Name)
Values('1','Amr Fayez', 'amr'),('2', 'Youssef Ahmed', 'kramla'),('3','Malak Walid', 'malak'),('4','Youssef Khaled', 'joe'),('5','Habiba Ahmed', 'bomb');


create Table Student(
Student_ID int Not NULL primary key,
Student_Name varchar(30) Not NUll,
Password varchar(50) Not Null,
Email varchar(100) Null,
);
insert into Student(Student_ID,Student_Name,Password,Email)
Values ('20210262','Omar Ayman','1234','omar@gmail.com'),('20210302','kamal thrwat','12345','kamal@gmail.com'),('20210016','Ahmed Khaled','19876','Ahmed@icloud.com');
insert into Student(Student_ID,Student_Name,Password,Email)
Values ('20210122','Habiba Magdy','Habiba6543','Habiba@gmail.com'),('20210384','Mariam elwakeel','Mariam@!@','Mariam@gmail.com'),('20210129','Rania Khaled','rania48','Rania@icloud.com'),('20210457','youstena wahba','yous234','Youstenaw@icloud.com'),('20210562','karin amir','Karin@2amir','KarinAmir@icloud.com'),('20210433','Noureldin tarek','12345678','Noureldin@icloud.com'),('20210017','renad foad','renad42019','reno@gmail.com'),('20210018','maya tamer','maya458','maya76@gmail.com'),('20210019','maryam diaa','maryam24diaa','maryaam2gmail.com'),('20210020','salma hany','salma55$hany','salma@icloud.com'),('20210021','zeina wael','zeina44$','zeina2@icloud.com'),('20210022','batoul kareem','baty5$5','batoulkaree5@gmail.com'),('20210023','karma korayem','karma5$karma','karma@gmail.com'),('20210024','hanah mahmoud','hannaah7','hannah@gmail.com'),('20210025','shahd hani','shahd2hani#','shahd@gmail.com'),('20210026','hana agmi','hanah2@agmi','hannah@gmail.com'),('20210227','nour ghoneim','nourghon2eim2','nour2ghonim@gmail.com'),('20210188','judy khalid','judy2#judy','judykhalid@gmail.com'),('20210566','ahmed tawfik','ahmed7tt2$','ahmed2@gmail.com'),('20210778','renada ehab','renadafarouk65','renada@gmail.com');


Create Table StudentPhone(
Student_ID int Not NULL,
Phone_num varchar(30) Not Null,
Foreign Key (Student_ID) References Student(Student_ID)
);
insert into StudentPhone(Student_ID,Phone_num)
values('20210262','01022257711'),('20210262','01022257790'),('20210302','01150868659'),('20210302','01150868658'),('20210016','01064397295');
insert into StudentPhone(Student_ID,Phone_num)
values('20210433','01274547197'),('20210433','012274115756'),('20210562','01010682888'),('20210457','01008377107'),('20210287','01027465267'),('20210287','01094430863'),('20210122','01003783856'),('20210129','01121023945'),('20210129','01100666624'),('20210017','01026986819'),('20210018','01009719689'),('20210019','01143773428'),('20210020','0128877898'),('2021020','01120531313'),('20210020','01069607408'),('20210021','01151976309')('20210022','01117809638'),('20210023','01021542617'),('20210024','01115053299'),('20210024','01140883978'),('20210024','01093263625'),('20210025','01157273230'),('20210026','012036955590'),('20210227','01146916754'),('20210188','01021960765'),('20210188','01030225274'),('20210566','01056140233'),('20210778','01281119388');

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
values('1','rich dad','robert','200.50','15'),('2','the art','mark manson','170','9'),('3','It starts with us','collen Hover','124.75','17'),('4','Tale of Two cities','charles Dickens','79','3'),('5','Great Expectations','charles Dickens','150','8')();
insert into Book(Book_ID,Book_Name,Book_Author,price,Number_Of_Copies)
values ('6','it ends with us','colleen hoover','350.75','25'),('7','verity','colleen hoover','400','5'),('8','allyourperfects','colleen hoover','340','10'),('9','without merit','colleen hoover','400','5'),('10','ugly love','colleen hoover','275','30'),('11','layla','colleen hoover','450.5','16'),('12','burn after writing','sharon jonfes','375','30'),('13','confess','colleen hoover','400','20'),('14','reminders of him','colleen hoover','280','30'),('15','maybe someday','colleen hoover','375','19'),('16','regretting you','colleen hoover','250.5','14'),('17','heart bones','colleen hoover','500','17'),('18','too late','colleen hoover','475','10'),('19','the kite runner','khaled hosseini','300','5'),('20','harry potter','j.k rowling','450,75','16'),('21','to kill the mockingbird','harper lee','400','30'),('22','the book theif','marcus zusak','300','27'),('23','animal farm','george orwell','275','16'),('24','litlle woman','louisa may alclott','170','17'),('25','charlotte web','e.b white','400','15'),('26','mn daftar nhayty','Omar Fathy','200','7');
Create Table Category(
Book_ID int Not Null,
Book_category varchar(30) not null,
CONSTRAINT FK_Category_BookID FOREIGN KEY (Book_ID) REFERENCES Book (Book_ID),
);
Insert into Category (Book_ID,Book_category)
values( '1','personal finnance'),('2','self help'),('3','Drama'),('4','Novel'),('5','Novel');
Insert into Category (Book_ID,Book_category)
values ('6','romantic novel'),('7','fiction'),('8','psychological fiction'),('9','domestic fiction'),('10','romantic novel'),('11','romantic suspense'),('12','self help'),('13','romantic novel'),('14',' romantic novel'),('15','fiction'),('16','domestic fiction'),('17','romantic novel'),('18','fiction'),('19','drama'),('20','fantasy'),('21','coming of age'),('22','novel'),('23','fiction'),('24','novel'),('25','fiction'),('26','horror');

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


SELECT * FROM Book;