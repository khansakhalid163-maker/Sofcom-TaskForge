select * from Users;
UPDATE Users
SET Role = 'Employee'
WHERE Email = 'ali@example.com';
select * from Users;