﻿IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'DIAMONDPROJECT')
BEGIN
    CREATE DATABASE DIAMONDPROJECT;
END
GO

USE DIAMONDPROJECT
GO

DROP TABLE IF EXISTS DIAMONDCERTIFICATE;
DROP TABLE IF EXISTS SHIPPINGADDRESS;
DROP TABLE IF EXISTS PAYMENT;
DROP TABLE IF EXISTS WARRANTY;
DROP TABLE IF EXISTS ORDERITEM;
DROP TABLE IF EXISTS ORDERS;
DROP TABLE IF EXISTS CUSTOMER;
DROP TABLE IF EXISTS REFUNDPRODUCT;
DROP TABLE IF EXISTS PENDANTPAIRING;
DROP TABLE IF EXISTS EARRINGPAIRING;
DROP TABLE IF EXISTS RINGPAIRING;
DROP TABLE IF EXISTS PENDANT;
DROP TABLE IF EXISTS EARRING;
DROP TABLE IF EXISTS RING;
DROP TABLE IF EXISTS DIAMOND;

CREATE TABLE CUSTOMER (
  CustomerID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
  FirstName VARCHAR(500),
  LastName VARCHAR(500),
  Email VARCHAR(500),
  Username VARCHAR(500),
  Password VARCHAR(100),
  PhoneNumber VARCHAR(20),
  RegistrationDate DATETIME
);

CREATE TABLE ORDERS (
  OrderID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
  CustomerID UNIQUEIDENTIFIER,
  OrderDate DATETIME,
  TotalAmount MONEY,
  OrderStatus VARCHAR(500),
  FOREIGN KEY (CustomerID) REFERENCES CUSTOMER(CustomerID)
);

CREATE TABLE ORDERITEM (
  OrderItemID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
  OrderID UNIQUEIDENTIFIER,
  DiamondID UNIQUEIDENTIFIER NULL,
  RingPairingID UNIQUEIDENTIFIER NULL,
  EarringPairingID UNIQUEIDENTIFIER NULL,
  PendantPairingID UNIQUEIDENTIFIER NULL,
  Price MONEY,
  ProductType VARCHAR(500),
  FOREIGN KEY (OrderID) REFERENCES ORDERS(OrderID)
);

CREATE TABLE SHIPPINGADDRESS (
  AddressID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
  CustomerID UNIQUEIDENTIFIER,
  AddressLine1 VARCHAR(500),
  AddressLine2 VARCHAR(500),
  City VARCHAR(500),
  District VARCHAR(500),
  FOREIGN KEY (CustomerID) REFERENCES CUSTOMER(CustomerID)
);

CREATE TABLE PAYMENT (
  PaymentID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
  OrderID UNIQUEIDENTIFIER UNIQUE,
  PaymentMethod VARCHAR(500),
  PaymentDate DATETIME,
  PaymentStatus VARCHAR(500),
  PaymentMessage VARCHAR(200),
  Amount MONEY,
  FOREIGN KEY (OrderID) REFERENCES ORDERS(OrderID)
);

CREATE TABLE WARRANTY (
  WarrantyID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
  OrderItemID UNIQUEIDENTIFIER UNIQUE,
  WarrantyStartDate DATETIME,
  WarrantyEndDate DATETIME,
  FOREIGN KEY (OrderItemID) REFERENCES ORDERITEM(OrderItemID)
);

CREATE TABLE DIAMOND (
  D_ProductID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
  Price MONEY,
  ImageURL VARCHAR(255),
  D_Type VARCHAR(100),
  CaratWeight FLOAT,
  Color INT,
  Clarity INT,
  Cut INT,
  Available BIT DEFAULT 1
);

CREATE TABLE RING (
  RingID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
  Name VARCHAR(500),
  Price MONEY,
  StockQuantity INT,
  ImageURL VARCHAR(255),
  MetalType VARCHAR(100),
  RingSize VARCHAR(20)
);

CREATE TABLE EARRING (
  EarringID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
  Name VARCHAR(500),
  Price MONEY,
  StockQuantity INT,
  ImageURL VARCHAR(255),
  MetalType VARCHAR(500)
);

CREATE TABLE PENDANT (
  PendantID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
  Name VARCHAR(500),
  Price MONEY,
  StockQuantity INT,
  ImageURL VARCHAR(255),
  ChainType VARCHAR(100),
  ChainLength VARCHAR(100),
  ClaspType VARCHAR(100),
);

CREATE TABLE RINGPAIRING (
  R_ProductID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
  RingID UNIQUEIDENTIFIER,
  DiamondID UNIQUEIDENTIFIER,
  FOREIGN KEY (RingID) REFERENCES RING(RingID),
  FOREIGN KEY (DiamondID) REFERENCES DIAMOND(D_ProductID)
);

CREATE TABLE EARRINGPAIRING (
  E_ProductID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
  EarringID UNIQUEIDENTIFIER,
  DiamondID UNIQUEIDENTIFIER,
  FOREIGN KEY (EarringID) REFERENCES EARRING(EarringID),
  FOREIGN KEY (DiamondID) REFERENCES DIAMOND(D_ProductID)
);

CREATE TABLE PENDANTPAIRING (
  P_ProductID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
  PendantID UNIQUEIDENTIFIER,
  DiamondID UNIQUEIDENTIFIER,
  FOREIGN KEY (PendantID) REFERENCES PENDANT(PendantID),
  FOREIGN KEY (DiamondID) REFERENCES DIAMOND(D_ProductID)
);

--CREATE TABLE REFUNDPRODUCT (RF_ProductID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),Name VARCHAR(500),Price MONEY,ImageURL VARCHAR(255),Specifications TEXT,CaratWeight FLOAT,Color INT,Clarity INT,Cut INT);

CREATE TABLE DIAMONDCERTIFICATE (
  CertificateID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
  ProductID UNIQUEIDENTIFIER,
  CertificateNumber VARCHAR(500),	
  IssuedBy VARCHAR(500),
  IssueDate DATETIME
);

ALTER TABLE ORDERITEM
  ADD CONSTRAINT FK_Orderitem_Diamond FOREIGN KEY (DiamondID) REFERENCES DIAMOND(D_ProductID);

ALTER TABLE ORDERITEM
  ADD CONSTRAINT FK_Orderitem_Ringpairing FOREIGN KEY (RingPairingID) REFERENCES RINGPAIRING(R_ProductID);

ALTER TABLE ORDERITEM
  ADD CONSTRAINT FK_Orderitem_Earringpairing FOREIGN KEY (EarringPairingID) REFERENCES EARRINGPAIRING(E_ProductID);

ALTER TABLE ORDERITEM
  ADD CONSTRAINT FK_Orderitem_Pendantpairing FOREIGN KEY (PendantPairingID) REFERENCES PENDANTPAIRING(P_ProductID);

ALTER TABLE DIAMONDCERTIFICATE
  ADD CONSTRAINT FK_DiamondCertificate_Diamond FOREIGN KEY (ProductID) REFERENCES DIAMOND(D_ProductID);

-- ALTER TABLE DIAMONDCERTIFICATE ADD CONSTRAINT FK_DiamondCertificate_Refundproduct FOREIGN KEY (ProductID) REFERENCES REFUNDPRODUCT(RF_ProductID);