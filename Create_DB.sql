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
-- datboi password is 'dope'
INSERT INTO `user` (name, email, password, date_created) VALUES ('datboi', 'datboi@datemail.com', '$2a$12$t4QQoP18ZAb/8yKBnYOTPu5.hFU4mAJUy.pVJGhhzrEsC6un/OPXy', current_timestamp());
COMMIT;

select * from user;