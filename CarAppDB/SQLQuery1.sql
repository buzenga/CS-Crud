CREATE DATABASE CarManagementDb
USE CarManagementDb

CREATE TABLE Cars (plate_number VARCHAR(50) PRIMARY KEY NOT NULL,
					car_producer VARCHAR(50) NOT NULL,
					car_model VARCHAR(50) NOT NULL,
					car_owner VARCHAR(50) NOT NULL)

SELECT * FROM Cars

