use pubs
go

select * from publishers

select * from titles

select title,pub_id from titles

select title, pub_name 
from titles join publishers
on titles.pub_id = publishers.pub_id

--print the publisher deatils of the publisher who has never published
select * from publishers where pub_id not in
(select distinct pub_id from titles)

select title, pub_name 
from titles right outer join publishers
on titles.pub_id = publishers.pub_id

--Select the author_id for all the books. Print the author_id and the book name
SELECT ta.au_id AS author_id, t.title AS book_name
FROM titleauthor ta
JOIN titles t ON ta.title_id = t.title_id;


SELECT CONCAT(au_fname,' ',au_lname) Author_Name, title_id 
FROM authors a
JOIN titleauthor ta on a.au_id=ta.au_id
ORDER BY 2;


SELECT CONCAT(au_fname,' ',au_lname) Author_Name, title Book_Name
FROM authors a
JOIN titleauthor ta on a.au_id=ta.au_id
JOIN titles t on t.title_id=ta.title_id;

--Print the publisher's name, book name and the order date of the  books

SELECT p.pub_name Publisher_Name, t.title Book_name, s.ord_date Order_Date
FROM publishers p
JOIN titles t ON p.pub_id=t.pub_id
JOIN sales s ON s.title_id=t.title_id
ORDER BY 3 DESC;

--Print the publisher name and the first book sale date for all the publishers
SELECT p.pub_name Publisher_Name, MIN(s.ord_date) First_Sale_Date
FROM publishers p
JOIN titles t ON p.pub_id = t.pub_id
JOIN sales s ON t.title_id = s.title_id
GROUP BY p.pub_name;

SELECT p.pub_name Publisher_Name, MIN(s.ord_date) First_Sale_Date
FROM publishers p
LEFT JOIN titles t ON p.pub_id = t.pub_id
LEFT JOIN sales s ON t.title_id = s.title_id
GROUP BY p.pub_name
ORDER BY 2 DESC;

-- Print the boook name and store address of the sale

SELECT t.title Book_Name, st.stor_name Store_Name, st.stor_address Store_Address
FROM titles t
JOIN sales s ON t.title_id=s.title_id
JOIN stores st ON s.stor_id=st.stor_id
ORDER BY 1;


