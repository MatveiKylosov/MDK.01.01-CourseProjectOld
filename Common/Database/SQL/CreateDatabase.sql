CREATE SCHEMA CarDealership;

CREATE TABLE CarDealership.Brands (
    BrandID INT AUTO_INCREMENT,
    BrandName VARCHAR(255),
    Country VARCHAR(255),
    Manufacturer VARCHAR(255),
    Address VARCHAR(255),
    PRIMARY KEY (BrandID)
);

CREATE TABLE CarDealership.Cars (
    CarID INT AUTO_INCREMENT,
    CarName VARCHAR(255),
    BrandID INT,
    YearOfProduction YEAR,
    Color VARCHAR(255),
    Category VARCHAR(255),
    Price DECIMAL(10, 2),
    PRIMARY KEY (CarID),
    FOREIGN KEY (BrandID) REFERENCES CarDealership.Brands(BrandID)
);

CREATE TABLE CarDealership.Customers (
    CustomerID INT AUTO_INCREMENT,
    FullName VARCHAR(255),
    PassportData VARCHAR(255),
    Address VARCHAR(255),
    BirthDate DATE,
    Gender ENUM('M', 'F'),
    ContactDetails VARCHAR(255),
    PRIMARY KEY (CustomerID)
);

CREATE TABLE CarDealership.Employees (
    EmployeeID INT AUTO_INCREMENT,
    FullName VARCHAR(255),
    WorkExperience INT,
    Salary DECIMAL(10, 2),
    ContactDetails VARCHAR(255),
    PRIMARY KEY (EmployeeID)
);

CREATE TABLE CarDealership.CarSales (
    SaleID INT AUTO_INCREMENT,
    SaleDate DATE,
    EmployeeID INT,
    CarID INT,
    CustomerID INT,
    PRIMARY KEY (SaleID),
    FOREIGN KEY (EmployeeID) REFERENCES CarDealership.Employees(EmployeeID),
    FOREIGN KEY (CarID) REFERENCES CarDealership.Cars(CarID),
    FOREIGN KEY (CustomerID) REFERENCES CarDealership.Customers(CustomerID)
);
