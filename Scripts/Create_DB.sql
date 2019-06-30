CREATE DATABASE pokeDB;
CREATE USER 'Ash' IDENTIFIED BY 'letmein';
GRANT ALL PRIVILEGES ON pokeDB.* TO 'Ash';

USE pokeDB;

CREATE TABLE user (
id BIGINT NOT NULL AUTO_INCREMENT,
name VARCHAR(255) NOT NULL,
email VARCHAR(255) NOT NULL,
password VARCHAR(128) NOT NULL,
date_created DATETIME(6) NOT NULL,
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

INSERT INTO `user` (name, email, password, date_created) VALUES ('datboi', 'datboi@datemail.com', 'dope', current_timestamp());
INSERT INTO `inventory` (name, itemQuantity, restorationAmount, typeLimitation) VALUES ('Health Potion', 10, 20, 'None');

COMMIT;