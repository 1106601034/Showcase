CREATE TABLE bank_stuff (
  Stuff_ID int NOT NULL,
  Department varchar(50) NOT NULL,
  PRIMARY KEY (Stuff_ID)
);
DESCRIBE bank_stuff;
CREATE TABLE Saving_account (
  Sav_acc_num int NOT NULL,
  Annual_salary int NOT NULL,
  PRIMARY KEY (Sav_acc_num)
);
DESCRIBE Saving_account;
CREATE TABLE Home_loan_account (
  loan_acc_num int NOT NULL,
  Total_limit int NOT NULL,
  Total_requested_loan int NOT NULL,
  PRIMARY KEY (loan_acc_num)
);
DESCRIBE Home_loan_account;
CREATE TABLE Customer (
  Customer_ID int NOT NULL,
  Specified_address varchar(50) NOT NULL,
  Requested_loan int NOT NULL,
  Individual_loan_limit int NOT NULL,
  Sav_acc_num int NOT NULL,
  loan_acc_num int NOT NULL,
  PRIMARY KEY (Customer_ID),
  FOREIGN KEY (Sav_acc_num) REFERENCES Saving_account(Sav_acc_num),
  FOREIGN KEY (loan_acc_num) REFERENCES Home_loan_account(loan_acc_num)
);
DESCRIBE Customer;
CREATE TABLE Property (
  Property_ID int NOT NULL,
  Customer_ID int NOT NULL,
  Sold_Price int NOT NULL,
  Suburb varchar(50) NOT NULL,
  Status_code int NOT NULL,
  Max_loan int NOT NULL,
  Pro_value int NOT NULL,
  PRIMARY KEY (Property_ID),
  FOREIGN KEY (Customer_ID) REFERENCES Customer(Customer_ID)
);
DESCRIBE Property;
CREATE TABLE Loan_application (
  Stuff_ID int NOT NULL,
  Customer_ID int NOT NULL,
  loan_acc_num int NOT NULL,
  Application_ID int NOT NULL,
  Property_ID int NOT NULL,
  PRIMARY KEY (Application_ID),
  FOREIGN KEY (Customer_ID) REFERENCES Customer(Customer_ID),
  FOREIGN KEY (loan_acc_num) REFERENCES Home_loan_account(loan_acc_num),
  FOREIGN KEY (Stuff_ID) REFERENCES bank_stuff(Stuff_ID),
  FOREIGN KEY (Property_ID) REFERENCES Property(Property_ID)
);
DESCRIBE Loan_application;
SELECT table_name FROM user_tables;