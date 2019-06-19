CREATE DATABASE pokeDB;
CREATE USER 'pokeDB' IDENTIFIED BY 'letmein';
GRANT ALL PRIVILEGES ON pokeDB.* TO 'pokeDB';

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

INSERT INTO USER (name, email, password, date_created) VALUES ('datboi', 'datboi@datemail.com', 'dope', current_timestamp());

commit;

select * from user;