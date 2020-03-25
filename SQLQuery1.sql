-- zaczac od studies

insert into Studies
values (1, 'informatyka');


insert into Studies
values (2, 'fizyka');


insert into Studies
values (3, 'matematyka');

insert into Enrollment
values (1, 4, 1, '20200325');


insert into Enrollment
values (2, 5, 2, '20190513');


insert into Enrollment
values (3, 3, 3, '20180623');

insert into Student
values (18239, 'Macierz', 'Jaworski', '19980115', 1);


insert into Student
values (69420, 'Anton', 'Gribanow', '19900521', 3);

select * 
from Student;