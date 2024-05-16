CREATE SCHEMA CarDealership;

USE CarDealership;

CREATE TABLE CarDealership.Brands (
  `BrandID` int(11) NOT NULL AUTO_INCREMENT,
  `BrandName` varchar(255) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `Country` varchar(255) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `Manufacturer` varchar(255) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `Address` varchar(255) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`BrandID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE CarDealership.Cars (
  `CarID` int(11) NOT NULL AUTO_INCREMENT,
  `CarName` varchar(255) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `BrandID` int(11) DEFAULT NULL,
  `YearOfProduction` year(4) DEFAULT NULL,
  `Color` varchar(255) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `Category` varchar(255) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `Price` decimal(10,2) DEFAULT NULL,
  PRIMARY KEY (`CarID`),
  KEY `BrandID` (`BrandID`),
  CONSTRAINT `cars_ibfk_1` FOREIGN KEY (`BrandID`) REFERENCES `Brands` (`BrandID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE CarDealership.Customers (
  `CustomerID` int(11) NOT NULL AUTO_INCREMENT,
  `FullName` varchar(255) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `PassportData` varchar(255) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `Address` varchar(255) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `BirthDate` date DEFAULT NULL,
  `Gender` BOOLEAN DEFAULT NULL,
  `ContactDetails` varchar(255) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`CustomerID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE CarDealership.Employees (
  `EmployeeID` int(11) NOT NULL AUTO_INCREMENT,
  `FullName` varchar(255) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `WorkExperience` int(11) DEFAULT NULL,
  `Salary` decimal(10,2) DEFAULT NULL,
  `ContactDetails` varchar(255) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`EmployeeID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE CarDealership.CarSales (
  `SaleID` int(11) NOT NULL AUTO_INCREMENT,
  `SaleDate` date DEFAULT NULL,
  `EmployeeID` int(11) DEFAULT NULL,
  `CarID` int(11) DEFAULT NULL,
  `CustomerID` int(11) DEFAULT NULL,
  PRIMARY KEY (`SaleID`),
  KEY `EmployeeID` (`EmployeeID`),
  KEY `CarID` (`CarID`),
  KEY `CustomerID` (`CustomerID`),
  CONSTRAINT `carsales_ibfk_1` FOREIGN KEY (`EmployeeID`) REFERENCES `Employees` (`EmployeeID`),
  CONSTRAINT `carsales_ibfk_2` FOREIGN KEY (`CarID`) REFERENCES `Cars` (`CarID`),
  CONSTRAINT `carsales_ibfk_3` FOREIGN KEY (`CustomerID`) REFERENCES `Customers` (`CustomerID`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
