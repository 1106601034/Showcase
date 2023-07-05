--------------------------
-- Task 1.1: [1 Marks]
-- Write one SQL query to list the region names and the number of countries for each region from the above database.
--------------------------
SELECT regions.region_name, COUNT(countries.region_id) AS countries_count
	FROM regions
LEFT JOIN countries
	ON regions.region_id = countries.region_id
GROUP BY regions.region_name
;
--------------------------
-- Task 1.2: [1 Marks]
-- Write one SQL query to find all customers who have made orders before 2016. List must include the customer ID, customer name, and ordered by their ID values in descending.
--------------------------
SELECT customers.customer_id, customers.name, orders.order_date
	FROM customers
INNER JOIN orders
	ON customers.customer_id = orders.customer_id
WHERE orders.order_date < to_date('1-JAN-16','DD-MON-RR')
ORDER BY customers.customer_id DESC
;
--------------------------
-- Task 1.3: [1 Marks]
-- Write one SQL query to list all customers who have the sequential letters ‘co’ in the customer name where the condition ‘co’ is case-insensitive, i.e., ‘CO’, ‘Co’, … can also be retrieved. List must include the customers’ ID, names and ordered by their names in ascending.
--------------------------
SELECT customers.customer_id, customers.name
	FROM customers
WHERE LOWER(customers.name) LIKE '%co%'
ORDER BY customers.name ASC
;
--------------------------
-- Task 1.4: [1 Marks]
-- Write one SQL query to list all products’ ID, Name and price where the products haven’t been purchased by any customer in the database. The list must be ordered by the product price.
--------------------------
SELECT products.product_id, products.product_name, products.list_price
	FROM products
WHERE products.product_id NOT IN (
	SELECT product_id FROM order_items
	-- this could work when customers must make order and pay before they cancel.
)
ORDER BY products.list_price ASC
;
--------------------------
-- Task 1.5: [1 Marks]
-- Write one SQL query to list the employees and the number of orders that each employee processed in the database. The output list must include employee ID, name, and the number of orders. The list must be sorted by the number of orders in the descending order.
--------------------------
SELECT employees.employee_id, 
	CONCAT(CONCAT(employees.first_name,' '), employees.last_name) AS name, 
	COUNT(orders.salesman_id) AS order_count
	FROM employees
LEFT JOIN orders
	ON orders.salesman_id = employees.employee_id
GROUP BY employees.employee_id, 
	CONCAT(CONCAT(employees.first_name,' '), employees.last_name)
ORDER BY order_count DESC
;
--------------------------
-- Task 1.6: [1 Marks]
-- Write one SQL query to list all the warehouses and the revenue of each warehouse. Here, given a product, the revenue of the product is calculated by the sold quantity of the product and its List_Price. The list must be ordered by the revenue value in the descending. [Reminder: if one product_ID links to more than one warehouses in the provided database, you can simply count it into its linked warehouses’ revenue.]
--------------------------
SELECT inventories.warehouse_id, SUM(income) AS revenue
	FROM(
		SELECT id, sold_quantity, products.list_price, 
		sold_quantity*products.list_price AS income
			FROM(
				SELECT order_items.product_id AS id,
					SUM(order_items.quantity) AS sold_quantity
					FROM order_items
				INNER JOIN orders
					ON order_items.order_id = orders.order_id
				WHERE orders.status != 'Canceled'
				GROUP BY order_items.product_id
				ORDER BY order_items.product_id
			), products
		WHERE id = products.product_id
		GROUP BY id, sold_quantity, products.list_price
	), inventories
WHERE id = inventories.product_id
GROUP BY inventories.warehouse_id
ORDER BY revenue DESC
;
--------------------------
-- Task 2.2: [1 Marks]
-- Write a set of SQL queries to create your normalized tables for building the database schema. Each table must declare the primary keys, foreign keys if applicable.
-- (Marking Rubric: 1 mark if there is no mistake to create the tables or only has one mistake; otherwise, 0 mark will be given.)
--------------------------
CREATE TABLE product_ (
	product_id NUMBER PRIMARY KEY,
	product_name VARCHAR2 (50),
	category VARCHAR2 (50)
);
CREATE TABLE store_ (
	store_id NUMBER PRIMARY KEY,
	store_name VARCHAR2 (50),
	address VARCHAR2 (255)
);
CREATE TABLE company_ (
	company_id NUMBER PRIMARY KEY,
	company_contact VARCHAR2 (50),
	company_name VARCHAR2 (50)
);
CREATE TABLE customer_ (
	customer_id NUMBER PRIMARY KEY,
	customer_name VARCHAR2 (50),
	payment_type VARCHAR2 (50),
	company_id NUMBER,
	CONSTRAINT fk_customer_company
		FOREIGN KEY (company_id)
		REFERENCES company_ (company_id) 
		ON DELETE CASCADE
);
CREATE TABLE order_ (
	order_number NUMBER PRIMARY KEY,
	store_id NUMBER,
	customer_id NUMBER,
	product_id NUMBER,
	amount NUMBER,
	price NUMBER,
	transaction_date DATE,
	CONSTRAINT fk_order_store
		FOREIGN KEY (store_id)
		REFERENCES store_ (store_id) 
		ON DELETE CASCADE,
	CONSTRAINT fk_order_customer
		FOREIGN KEY (customer_id)
		REFERENCES customer_ (customer_id) 
		ON DELETE CASCADE,
	CONSTRAINT fk_order_product
		FOREIGN KEY (product_id)
		REFERENCES product_ (product_id) 
		ON DELETE CASCADE
);
--------------------------
-- Task 2.3: [1 Marks]
-- Write a set of SQL queries to add data into the database implemented in Task 2.2. The database must include all the provided information. If need, you can add unique identifiers or ids for tables.
--------------------------
ALTER TABLE customer_ DISABLE CONSTRAINT fk_customer_company;
ALTER TABLE order_ DISABLE CONSTRAINT fk_order_store;
ALTER TABLE order_ DISABLE CONSTRAINT fk_order_customer;
ALTER TABLE order_ DISABLE CONSTRAINT fk_order_product;

REM INSERTING into product_ SET DEFINE OFF;
Insert into product_ (product_id, product_name, category) 
	values (1,'ThinkPad', 'Electronic');
Insert into product_ (product_id, product_name, category) 
	values (2,'Mouse', 'Electronic');
Insert into product_ (product_id, product_name, category) 
	values (3,'Office Chair', 'Office');
Insert into product_ (product_id, product_name, category) 
	values (4,'Camera', 'Electronic');
Insert into product_ (product_id, product_name, category) 
	values (5,'Keyboard', 'Electronic');
SELECT * FROM product_;

REM INSERTING into store_ SET DEFINE OFF;
Insert into store_ (store_id, store_name, address) 
	values (1,'Freeland Choice', '20 Avenue, Burwood, VIC3125');
Insert into store_ (store_id, store_name, address) 
	values (2,'City Bunnings', '36 Maple street, Melbourne, VIC3000');
Insert into store_ (store_id, store_name, address) 
	values (3,'Burwood Electronic', '20 Avenue, Burwood');
Insert into store_ (store_id, store_name, address) 
	values (4,'Burwood Officework', '606 Burwood Hwy, Vermont South VIC 3133');
Insert into store_ (store_id, store_name, address) 
	values (5,'Burwood Bunnings', '606-634 Burwood Hwy, Vermont South VIC 3133');
SELECT * FROM store_;
	
REM INSERTING into company_ SET DEFINE OFF;
Insert into company_ (company_id, company_contact, company_name) 
	values (1, 111111, 'IBM');
Insert into company_ (company_id, company_contact, company_name) 
	values (2, 222222, 'Luxo Narelle Mesh');
SELECT * FROM company_;

REM INSERTING into customer_ SET DEFINE OFF;
Insert into customer_ (customer_id, customer_name, payment_type, company_id) 
	values (1, 'Kevin Li', 'Visa', 1);
Insert into customer_ (customer_id, customer_name, payment_type, company_id) 
	values (2, 'David Andrews', 'Mastercard', 1);
Insert into customer_ (customer_id, customer_name, payment_type, company_id) 
	values (3, 'Daniel Andrews', 'Mastercard', 2);
SELECT * FROM customer_;

REM INSERTING into order_ SET DEFINE OFF;
Insert into order_ (order_number, store_id, customer_id, product_id, amount, price, transaction_date)
	values (1, 1, 1, 1, 1, 1500, to_date('21/03/2021','DD/MM/RR'));
Insert into order_ (order_number, store_id, customer_id, product_id, amount, price, transaction_date)
	values (2, 1, 1, 2, 2, 30, to_date('21/03/2021','DD/MM/RR'));
Insert into order_ (order_number, store_id, customer_id, product_id, amount, price, transaction_date)
	values (3, 2, 3, 3, 5, 79, to_date('21/03/2021','DD/MM/RR'));
Insert into order_ (order_number, store_id, customer_id, product_id, amount, price, transaction_date)
	values (4, 3, 1, 4, 1, 100, to_date('22/03/2021','DD/MM/RR'));
Insert into order_ (order_number, store_id, customer_id, product_id, amount, price, transaction_date)
	values (5, 3, 1, 5, 2, 50, to_date('22/03/2021','DD/MM/RR'));
Insert into order_ (order_number, store_id, customer_id, product_id, amount, price, transaction_date)
	values (6, 4, 2, 1, 2, 1350, to_date('23/03/2021','DD/MM/RR'));
Insert into order_ (order_number, store_id, customer_id, product_id, amount, price, transaction_date)
	values (7, 5, 3, 3, 4, 80, to_date('24/03/2021','DD/MM/RR'));
SELECT * FROM order_;

ALTER TABLE customer_ ENABLE CONSTRAINT fk_customer_company;
ALTER TABLE order_ ENABLE CONSTRAINT fk_order_store;
ALTER TABLE order_ ENABLE CONSTRAINT fk_order_customer;
ALTER TABLE order_ ENABLE CONSTRAINT fk_order_product;

-- DROP TABLE order_;
-- DROP TABLE customer_;
-- DROP TABLE product_;
-- DROP TABLE store_;
-- DROP TABLE company_;