
DROP DATABASE pokeDB;

CREATE DATABASE pokeDB;
USE pokeDB;



CREATE TABLE customer (
customerID BIGINT AUTO_INCREMENT NOT NULL,
customerName VARCHAR(255) NOT NULL,
customerEmail VARCHAR(255) NOT NULL,
customerPassword VARCHAR(128) NOT NULL,
dateCreated DATETIME NOT NULL,
customerRole INTEGER NOT NULL,
CONSTRAINT PK_USER_ID PRIMARY KEY(customerID),
CONSTRAINT UQ_USER_EMAIL UNIQUE(customerEmail),
CONSTRAINT UQ_USER_NAME UNIQUE(customerName)
);

CREATE TABLE itemInventory (
itemID BIGINT AUTO_INCREMENT NOT NULL,
itemName VARCHAR(255) NOT NULL,
itemQuantity int NOT NULL DEFAULT '0',
-- restorationAmount int NOT NULL,  
-- typeLimitation VARCHAR(255) NOT NULL,
CONSTRAINT PK_ITEM_ID PRIMARY KEY(itemID),
CONSTRAINT UQ_ITEM_NAME UNIQUE(itemName)
);

CREATE TABLE customerOrder(
  customerOrderID BIGINT AUTO_INCREMENT NOT NULL,
  orderDate DATETIME NOT NULL,
  PRIMARY KEY(customerOrderID)
);


CREATE TABLE CustomerOrder_Inventory(
  orderID BIGINT NOT NULL,
  itemID BIGINT NOT NULL,
  quantityOrdered INT NOT NULL,
  PRIMARY KEY(orderID, itemID),
  FOREIGN KEY(itemID) REFERENCES itemInventory(itemID),
  FOREIGN KEY(orderID) REFERENCES customerOrder(customerOrderID)
);

CREATE TABLE schedule(
  scheduleID BIGINT AUTO_INCREMENT NOT NULL,
  calendarDay INT NOT NULL,
  -- how many products used on what day with which customer?? QuantityUsed, CustomerID
  PRIMARY KEY(scheduleID)
);

CREATE TABLE supplierDelivery(
	deliveryID BIGINT AUTO_INCREMENT NOT NULL,
	deliveryDate DATETIME NOT NULL,
	PRIMARY KEY (deliveryID)
);

CREATE TABLE supplierDelivery_Inventory(
	deliveryID BIGINT AUTO_INCREMENT NOT NULL,
	itemID BIGINT NOT NULL,
	quantityDelivered INT NOT NULL,
	PRIMARY KEY(deliveryID, itemID),
	FOREIGN KEY(itemID) REFERENCES itemInventory(itemID),
	FOREIGN KEY(deliveryID) REFERENCES supplierDelivery(deliveryID)
  );



-- COMMIT;

