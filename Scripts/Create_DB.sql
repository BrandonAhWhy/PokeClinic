CREATE DATABASE pokeDB;
USE pokeDB;
CREATE USER 'Ash' IDENTIFIED BY 'letmein';
GRANT ALL PRIVILEGES ON pokeDB.* TO 'Ash';



CREATE TABLE user (
id BIGINT NOT NULL AUTO_INCREMENT,
name VARCHAR(255) NOT NULL,
email VARCHAR(255) NOT NULL,
password VARCHAR(128) NOT NULL,
date_created DATETIME(6) NOT NULL,
role INTEGER NOT NULL,
CONSTRAINT PK_USER_ID PRIMARY KEY(id),
CONSTRAINT UQ_USER_EMAIL UNIQUE(email),
CONSTRAINT UQ_USER_NAME UNIQUE(name)
);

CREATE TABLE inventory (
id BIGINT NOT NULL AUTO_INCREMENT,
name VARCHAR(255) NOT NULL,
itemQuantity int NOT NULL,
restorationAmount int NOT NULL,
typeLimitation VARCHAR(255) NOT NULL,
CONSTRAINT PK_ITEM_ID PRIMARY KEY(id),
CONSTRAINT UQ_ITEM_NAME UNIQUE(name)
);

CREATE TABLE `order`(
  id BIGINT NOT NULL AUTO_INCREMENT,
  order_date DATETIME NOT NULL,
  PRIMARY KEY(id)
);


CREATE TABLE `item_order`(
  order_id BIGINT NOT NULL,
  item_id BIGINT NOT NULL,
  quantity INT NOT NULL,
  PRIMARY KEY(order_id, item_id),
  FOREIGN KEY(item_id) REFERENCES inventory(id),
  FOREIGN KEY(order_id) REFERENCES `order`(id)
);

CREATE TABLE schedule(
  id BIGINT NOT NULL AUTO_INCREMENT,
  day INT NOT NULL,
  PRIMARY KEY(id)
);

--Get soonest available booking
SELECT day, COUNT(id) AS numBookings
FROM schedule
GROUP BY schedule.day
HAVING numBookings < 3
ORDER BY schedule.day ASC;
--Add a booking
INSERT INTO schedule (day) VALUES (1);


INSERT INTO `order` (order_date) VALUES (current_timestamp());

INSERT INTO `inventory` (name, itemQuantity, restorationAmount, typeLimitation) VALUES ('Health Potion', 10, 20, 'None');
INSERT INTO `inventory` (name, itemQuantity, restorationAmount, typeLimitation) VALUES ('VeryCoolPotion', 69, 20, 'None');

INSERT INTO item_order (order_id, item_id, quantity) VALUES (1, 1, 5);
INSERT INTO `user` (name, email, password, date_created, role) VALUES ('datboi', 'datboi@datemail.com', '$2a$12$t4QQoP18ZAb/8yKBnYOTPu5.hFU4mAJUy.pVJGhhzrEsC6un/OPXy', current_timestamp(), 1);
COMMIT;
