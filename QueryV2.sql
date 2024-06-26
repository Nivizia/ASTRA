IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'DIAMONDPROJECT')
BEGIN
    CREATE DATABASE DIAMONDPROJECT;
END
GO

USE DIAMONDPROJECT
GO

-- Drop existing tables if they exist
DROP TABLE IF EXISTS WARRANTY;
DROP TABLE IF EXISTS DIAMONDCERTIFICATE;
DROP TABLE IF EXISTS ORDERITEM;
DROP TABLE IF EXISTS PENDANTPAIRING;
DROP TABLE IF EXISTS EARRINGPAIRING;
DROP TABLE IF EXISTS RINGPAIRING;
DROP TABLE IF EXISTS PENDANT;
DROP TABLE IF EXISTS EARRING;
DROP TABLE IF EXISTS RING_SUITABLESHAPE;
DROP TABLE IF EXISTS RING;
DROP TABLE IF EXISTS METALTYPE;
DROP TABLE IF EXISTS SUITABLESHAPE;
DROP TABLE IF EXISTS FRAMETYPE;
DROP TABLE IF EXISTS RINGSUBTYPE;
DROP TABLE IF EXISTS RINGTYPE;
DROP TABLE IF EXISTS DIAMOND;
DROP TABLE IF EXISTS PAYMENT;
DROP TABLE IF EXISTS SHIPPINGADDRESS;
DROP TABLE IF EXISTS ORDERS;
DROP TABLE IF EXISTS CUSTOMER;

-- Customer table
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

-- Orders table
CREATE TABLE ORDERS (
  OrderID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
  CustomerID UNIQUEIDENTIFIER,
  OrderDate DATETIME,
  TotalAmount MONEY,
  OrderStatus VARCHAR(500),
  FOREIGN KEY (CustomerID) REFERENCES CUSTOMER(CustomerID)
);

-- Shipping address table
CREATE TABLE SHIPPINGADDRESS (
  AddressID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
  CustomerID UNIQUEIDENTIFIER,
  AddressLine1 VARCHAR(500),
  AddressLine2 VARCHAR(500),
  City VARCHAR(500),
  District VARCHAR(500),
  FOREIGN KEY (CustomerID) REFERENCES CUSTOMER(CustomerID)
);

-- Payment table
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

-- Diamond table
CREATE TABLE DIAMOND (
  D_ProductID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
  Price MONEY,
  ImageURL VARCHAR(255),
  Shape VARCHAR(100),
  CaratWeight FLOAT,
  Color INT,
  Clarity INT,
  Cut INT,
  Available BIT DEFAULT 1
);

CREATE TABLE RINGTYPE (
    RingTypeID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    TypeName VARCHAR(100) NOT NULL
);

CREATE TABLE RINGSUBTYPE (
    RingSubtypeID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    RingTypeID UNIQUEIDENTIFIER,
    SubtypeName VARCHAR(100) NOT NULL,
    FOREIGN KEY (RingTypeID) REFERENCES RINGTYPE(RingTypeID)
);

CREATE TABLE FRAMETYPE (
    FrameTypeID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    FrameTypeName VARCHAR(100) NOT NULL
);

CREATE TABLE SUITABLESHAPE (
    SuitableShapeID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    SuitableShapeName VARCHAR(100) NOT NULL
);

CREATE TABLE METALTYPE (
    MetalTypeID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    MetalTypeName VARCHAR(100) NOT NULL
);

CREATE TABLE RING (
    RingID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    RingTypeID UNIQUEIDENTIFIER,
    RingSubtypeID UNIQUEIDENTIFIER,
    FrameTypeID UNIQUEIDENTIFIER,
    MetalTypeID UNIQUEIDENTIFIER,
    RingName VARCHAR(500),
    Price MONEY,
    StockQuantity INT,
    ImageURL VARCHAR(255),
    FOREIGN KEY (RingTypeID) REFERENCES RINGTYPE(RingTypeID),
    FOREIGN KEY (RingSubtypeID) REFERENCES RINGSUBTYPE(RingSubtypeID),
    FOREIGN KEY (FrameTypeID) REFERENCES FRAMETYPE(FrameTypeID),
    FOREIGN KEY (MetalTypeID) REFERENCES METALTYPE(MetalTypeID)
);

CREATE TABLE RING_SUITABLESHAPE (
    RingSuitableShapeID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    RingID UNIQUEIDENTIFIER,
    SuitableShapeID UNIQUEIDENTIFIER,
    FOREIGN KEY (RingID) REFERENCES RING(RingID),
    FOREIGN KEY (SuitableShapeID) REFERENCES SUITABLESHAPE(SuitableShapeID)
);

-- Earring table
CREATE TABLE EARRING (
  EarringID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
  Name VARCHAR(500),
  Price MONEY,
  StockQuantity INT,
  ImageURL VARCHAR(255),
  MetalType VARCHAR(500)
);

-- Pendant table
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

-- Ring pairing table
CREATE TABLE RINGPAIRING (
  R_ProductID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
  RingID UNIQUEIDENTIFIER,
  DiamondID UNIQUEIDENTIFIER,
  FOREIGN KEY (RingID) REFERENCES RING(RingID),
  FOREIGN KEY (DiamondID) REFERENCES DIAMOND(D_ProductID)
);

-- Earring pairing table
CREATE TABLE EARRINGPAIRING (
  E_ProductID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
  EarringID UNIQUEIDENTIFIER,
  DiamondID UNIQUEIDENTIFIER,
  FOREIGN KEY (EarringID) REFERENCES EARRING(EarringID),
  FOREIGN KEY (DiamondID) REFERENCES DIAMOND(D_ProductID)
);

-- Pendant pairing table
CREATE TABLE PENDANTPAIRING (
  P_ProductID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
  PendantID UNIQUEIDENTIFIER,
  DiamondID UNIQUEIDENTIFIER,
  FOREIGN KEY (PendantID) REFERENCES PENDANT(PendantID),
  FOREIGN KEY (DiamondID) REFERENCES DIAMOND(D_ProductID)
);

-- Order item table
CREATE TABLE ORDERITEM (
  OrderItemID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
  OrderID UNIQUEIDENTIFIER,
  DiamondID UNIQUEIDENTIFIER NULL,
  RingPairingID UNIQUEIDENTIFIER NULL,
  EarringPairingID UNIQUEIDENTIFIER NULL,
  PendantPairingID UNIQUEIDENTIFIER NULL,
  Price MONEY,
  ProductType VARCHAR(500),
  FOREIGN KEY (OrderID) REFERENCES ORDERS(OrderID),
  CONSTRAINT FK_Orderitem_Diamond FOREIGN KEY (DiamondID) REFERENCES DIAMOND(D_ProductID),
  CONSTRAINT FK_Orderitem_Ringpairing FOREIGN KEY (RingPairingID) REFERENCES RINGPAIRING(R_ProductID),
);

-- Diamond certificate table
CREATE TABLE DIAMONDCERTIFICATE (
  CertificateID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
  ProductID UNIQUEIDENTIFIER,
  CertificateNumber VARCHAR(500),	
  IssuedBy VARCHAR(500),
  IssueDate DATETIME,
  CONSTRAINT FK_DiamondCertificate_Diamond FOREIGN KEY (ProductID) REFERENCES DIAMOND(D_ProductID)
);

-- Warranty table
CREATE TABLE WARRANTY (
  WarrantyID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
  OrderItemID UNIQUEIDENTIFIER UNIQUE,
  WarrantyStartDate DATETIME,
  WarrantyEndDate DATETIME,
  FOREIGN KEY (OrderItemID) REFERENCES ORDERITEM(OrderItemID)
);
