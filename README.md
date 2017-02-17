# mergeApp

### Tip

/'
// sql 명령어
        // Database create/show
        // mysql> CREATE DATABASE dbname;
        // mysql> SHOW DATABASE;
        // mysql> USE dbname; // declare specific database
        // DROP DATABASE [IF EXISTS] dbname; // delete database

        // Create & Show Table
        // mysql> CREATE TABLE tablename (
        // column_name1 INT PRIMARY KEY AUTO_INCREMENT,
        // column_name2 VARCHAR(15) NOT NULL,
        // column_name3 INT
        // ) ENGINE=INNODB;
        // mysql> SHOW TABLES;
        // mysql> EXPLAIN tablesname;
        // mysql> DESCRIBE tablename;
        // mysql> RENAME TABLE tablename1 TO tablename2[, tablename3 TO tablename4];
        // mysql> DROP TABLE [IF EXISTS] tablename;

        // Insert
        // mysql> INSERT INTO tablename VALUES(value1, value2, ...);
        // mysql> INSERT INTO tablename (col1, col2, ...) VALUES(value1, value2, ...);

        // Select
        // mysql> SELECT col1, col2, ... FROM tablename;
        // you can use * instead of col, it means select all column.
        // mysql> SELECT col1 AS 'Name', col2 AS 'Score' FROM grade;
        // mysql> SELECT * FROM tablename ORDER BY col1 DESC;
        // mysql> SELECT col1, korean + math english AS 'Total Score' FROM tablename ORDER BY 'Total Score' ASC;
        // DESC : descending order, ASC : ascending order;
        // mysql> SELECT * FROM grade WHERE korean < 90;
        // mysql> SELECT * FROM grade LIMIT 10;
        // take 10 result from first
        // mysql> SELECT * FROM grade LIMIT 100, 10;
        // take 10 result from 100th. ( records are start from 0 )

        // Update
        // mysql> UPDATE tablename SET col1=newvalue WHERE condition

        // Delete
        // mysql> DELETE FROM tablename WHERE condition
        
/'
       
