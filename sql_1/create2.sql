CREATE TABLE Customer_ (
  Customer_ID int NOT NULL,
  Name varchar(50) NOT NULL,
  PRIMARY KEY (Customer_ID)
);
DESCRIBE Customer_;
CREATE TABLE Order_ (
  Customer_ID int NOT NULL,
  Order_ID int NOT NULL,
  Sender_add varchar(50) NOT NULL,
  Receiver_add varchar(50) NOT NULL,
  Sender_num int NOT NULL,
  Receiver_num int NOT NULL,
  Status_code int NOT NULL,
  PRIMARY KEY (Order_ID),
  FOREIGN KEY (Customer_ID) REFERENCES Customer_(Customer_ID)
);
DESCRIBE Order_;
CREATE TABLE Tracking_statement_ (
  Tracking_num int NOT NULL,
  Order_ID int NOT NULL,
  PRIMARY KEY (Tracking_num),
  FOREIGN KEY (Order_ID) REFERENCES Order_(Order_ID)
);
DESCRIBE Tracking_statement_;
CREATE TABLE Carrier_owner_ (
  Owner_id int NOT NULL,
  Name varchar(50) NOT NULL,
  PRIMARY KEY (Owner_id)
);
DESCRIBE Carrier_owner_;
CREATE TABLE Store_ (
  Store_ID int NOT NULL,
  Address varchar(50) NOT NULL,
  Name varchar(50) NOT NULL,
  PRIMARY KEY (Store_ID)
);
DESCRIBE Store_;
CREATE TABLE Stuff_ (
  Store_ID int NOT NULL,
  Name varchar(50) NOT NULL,
  Stuff_ID int NOT NULL,
  PRIMARY KEY (Stuff_ID),
  FOREIGN KEY (Store_ID) REFERENCES Store_(Store_ID)
);
DESCRIBE Stuff_;
CREATE TABLE Carrier_ (
  Owner_id int NOT NULL,
  Carrier_ID int NOT NULL,
  PRIMARY KEY (Carrier_ID),
  FOREIGN KEY (Owner_id) REFERENCES Carrier_owner_(Owner_id)
);
DESCRIBE Carrier_;
CREATE TABLE Transportation_ (
  Transportation_ID int NOT NULL,
  Tracking_num int NOT NULL,
  Start_from varchar(50) NOT NULL,
  Arrival_at varchar(50) NOT NULL,
  Start_time date NOT NULL,
  End_time date NOT NULL,
  Stuff_ID int NOT NULL,
  Carrier_ID int NOT NULL,
  PRIMARY KEY (Transportation_ID),
  FOREIGN KEY (Tracking_num) REFERENCES Tracking_statement_(Tracking_num),
  FOREIGN KEY (Stuff_ID) REFERENCES Stuff_(Stuff_ID),
  FOREIGN KEY (Carrier_ID) REFERENCES Carrier_(Carrier_ID)
);
DESCRIBE Transportation_;
SELECT table_name FROM user_tables;