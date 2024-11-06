CREATE DATABASE DAPM
CREATE TABLE Customers
(
  CustomerID int,
  FullName nvarchar(50),
  CusAddress nvarchar(200),
  CusPhone char(10),
  UserName nvarchar(15),
  CusPassword nvarchar(10),
  PRIMARY KEY (CustomerID)
);

CREATE TABLE Employees
(
  EmployeeID INT,
  EmployeePassWord varchar(10),
  EmployeeFullName nvarchar(50),
  BirthYear INT,
  Position INT,
  PRIMARY KEY (EmployeeID)
);

CREATE TABLE Promotions
(
  PromotionID INT,
  Specification char(10),
  Duration INT,
  PRIMARY KEY (PromotionID)
);

CREATE TABLE Coupons
(
  CouponID INT,
  CouponDescription nvarchar(100),
  CouponDiscount int,
  PRIMARY KEY (CouponID)
);

CREATE TABLE Categories
(
  CategoryID INT,
  CategoryName nvarchar(10) not null,
  CategoryIllust varchar(max),
  PRIMARY KEY (CategoryID)
);

CREATE TABLE Products
(
  ProductID INT,
  ProductName nvarchar(50) not null,
  ProductPrice money,
  StockQuantity INT,
  Tax float,
  Brand nvarchar(15),
  ProductImg varchar(max),
  CategoryID INT NOT NULL,
  PRIMARY KEY (ProductID),
  FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID)
);

CREATE TABLE Orders
(
  OrderID INT NOT NULL,
  OrderQuantity INT NOT NULL,
  TotalMoney INT NOT NULL,
  OrderDate date,
  OrderStatus char(2),
  ReceiverName nvarchar(50),
  ReceiverPhoneNum char(10),
  ReceiverAddress nvarchar(200),
  CustomerID INT NOT NULL,
  EmployeeID INT NOT NULL,
  CouponID INT,
  PRIMARY KEY (OrderID),
  FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID),
  FOREIGN KEY (EmployeeID) REFERENCES Employees(EmployeeID),
  FOREIGN KEY (CouponID) REFERENCES Coupons(CouponID)
);

CREATE TABLE OrderDetails
(
  StockQuantity INT NOT NULL,
  ItemPrice int NOT NULL,
  Discount int NOT NULL,
  Total money,
  OrderID INT NOT NULL,
  ProductID INT NOT NULL,
  PromotionID INT,
  FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),
  FOREIGN KEY (ProductID) REFERENCES Products(ProductID),
  FOREIGN KEY (PromotionID) REFERENCES Promotions(PromotionID)
);

CREATE TABLE PromotionDetails
(
  PromotionID INT,
  ProductID INT NOT NULL,
  DateStart Date Not null,
  DateEnd Date NOT NULL,
  PRIMARY KEY (PromotionID, ProductID),
  FOREIGN KEY (PromotionID) REFERENCES Promotions(PromotionID),
  FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);