CREATE TABLE Customers
(
  CustomerID INT IDENTITY,
  FullName NVARCHAR(100) NOT NULL,
  CusAddress NVARCHAR(255) NOT NULL,
  PhoneNumber VARCHAR(15) NOT NULL,
  Username NVARCHAR(50) NOT NULL,
  CusPassword NVARCHAR(255) NOT NULL,
  PRIMARY KEY (CustomerID)
);

CREATE TABLE Employees
(
  EmployeeID INT NOT NULL IDENTITY,
  FullName NVARCHAR(100) NOT NULL,
  BirthYear int NOT NULL,
  PositionID INT NOT NULL,
  PRIMARY KEY (EmployeeID)
);

CREATE TABLE Coupons
(
  CouponID INT NOT NULL IDENTITY,
  CouponDescription NVARCHAR(255) NOT NULL,
  CouponDiscount DECIMAL(15, 2) NOT NULL,
  PRIMARY KEY (CouponID)
);

CREATE TABLE Categories
(
  CategoryID INT NOT NULL IDENTITY,
  CategoryName NVARCHAR(100) NOT NULL,
  CategoryIllust NVARCHAR(255),
  PRIMARY KEY (CategoryID)
);

CREATE TABLE Products
(
  ProductID INT NOT NULL IDENTITY,
  Price DECIMAL(15, 2) NOT NULL,
  StockQuantity INT NOT NULL,
  Tax DECIMAL(5, 2) NOT NULL,
  Brand NVARCHAR(100) NOT NULL,
  ProductName NVARCHAR(100) NOT NULL,
  ProductDescription NVARCHAR(500),
  ProductImg NVARCHAR(255),
  CategoryID INT NOT NULL,
  PRIMARY KEY (ProductID),
  FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID)
);

CREATE TABLE Orders
(
  OrderID INT NOT NULL IDENTITY,
  ProductQuantity INT NOT NULL,
  TotalAmount DECIMAL(15, 2) NOT NULL,
  OrderDate DATE NOT NULL,
  OrderStatus NVARCHAR(50) NOT NULL,
  ReceiverName NVARCHAR(100) NOT NULL,
  ReceiverPhoneNum VARCHAR(15) NOT NULL,
  ReceiverAddress NVARCHAR(255) NOT NULL,
  CustomerID INT NOT NULL,
  EmployeeID INT,
  CouponID INT,
  PRIMARY KEY (OrderID),
  FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID),
  FOREIGN KEY (EmployeeID) REFERENCES Employees(EmployeeID),
  FOREIGN KEY (CouponID) REFERENCES Coupons(CouponID)
);

CREATE TABLE Promotions
(
  PromotionID INT NOT NULL IDENTITY,
  Terms NVARCHAR(255) NOT NULL,
  StartDate DATE NOT NULL,
  EndDate DATE NOT NULL,
  ProductID INT NOT NULL,
  PRIMARY KEY (PromotionID),
  FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);
CREATE TABLE OrderDetails
(
  Quantity INT NOT NULL,
  Subtotal DECIMAL(15, 2) NOT NULL,
  OrderID INT NOT NULL,
  ProductID INT NOT NULL,
  PromotionID INT,
  FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),
  FOREIGN KEY (ProductID) REFERENCES Products(ProductID),
  FOREIGN KEY (PromotionID) REFERENCES Promotions(PromotionID)
);
CREATE TABLE AdminUser
(
	ID int not null,
	UserName nvarchar(50) not null,
	CustomerID int
	Foreign key (CustomerID) references Customers(CustomerID)
)

