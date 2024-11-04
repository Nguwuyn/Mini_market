CREATE TABLE Khách_hàng
(
  ID_khách char(8),
  Họ_tên nvarchar(50),
  Địa_chỉ nvarchar(200),
  Số_điện_thoại char(10),
  PRIMARY KEY (ID_khách)
);

CREATE TABLE Nhân_viên
(
  ID_nhân_viên char(3),
  Họ_tên_nhân_viên nvarchar(50),
  Năm_sinh INT,
  ID_chức_vụ INT,
  PRIMARY KEY (ID_nhân_viên)
);

CREATE TABLE CTKM
(
  ID_CTKM char(10),
  Quy_cách char(4),
  Thời_lượng INT,
  PRIMARY KEY (ID_CTKM)
);

CREATE TABLE Mã_giảm_giá
(
  ID_mã_giảm_giá char(10),
  Nội_dung nvarchar(100),
  Số_tiền_giảm float,
  PRIMARY KEY (ID_mã_giảm_giá)
);

CREATE TABLE Danh_mục
(
  ID_danh_mục INT,
  Tên_danh_mục nvarchar(10) not null,
  PRIMARY KEY (ID_danh_mục)
);

CREATE TABLE Sản_phẩm
(
  ID_sản_phẩm char(6) ,
  Giá_tiền money,
  số_lượng_tồn INT ,
  Thuế float,
  Hãng nvarchar(15),
  ID_danh_mục INT NOT NULL,
  PRIMARY KEY (ID_sản_phẩm),
  FOREIGN KEY (ID_danh_mục) REFERENCES Danh_mục(ID_danh_mục)
);

CREATE TABLE Đơn_mua_hàng
(
  ID_đơn_hàng INT NOT NULL,
  Số_lượng_sản_phẩm INT NOT NULL,
  Tổng_thành_tiền INT NOT NULL,
  Ngày_lập_đơn date,
  Trạng_thái_đơn_hàng char(2),
  Họ_tên_người_nhận nvarchar(50),
  SĐT_người_nhận char(10),
  Địa_chỉ_người_nhận nvarchar(200),
  ID_khách char(8) NOT NULL,
  ID_nhân_viên char(3) NOT NULL,
  ID_mã_giảm_giá char(10),
  PRIMARY KEY (ID_đơn_hàng),
  FOREIGN KEY (ID_khách) REFERENCES Khách_hàng(ID_khách),
  FOREIGN KEY (ID_nhân_viên) REFERENCES Nhân_viên(ID_nhân_viên),
  FOREIGN KEY (ID_mã_giảm_giá) REFERENCES Mã_giảm_giá(ID_mã_giảm_giá)
);

CREATE TABLE Chi_tiết_đơn_hàng
(
  Số_lượng INT NOT NULL,
  Thành_tiền money,
  ID_đơn_hàng INT NOT NULL,
  ID_sản_phẩm char(6) NOT NULL,
  ID_CTKM char(10),
  FOREIGN KEY (ID_đơn_hàng) REFERENCES Đơn_mua_hàng(ID_đơn_hàng),
  FOREIGN KEY (ID_sản_phẩm) REFERENCES Sản_phẩm(ID_sản_phẩm),
  FOREIGN KEY (ID_CTKM) REFERENCES CTKM(ID_CTKM)
);

CREATE TABLE Chi_tiết_CTKM
(
  ID_CTKM char(10),
  ID_sản_phẩm char(6) NOT NULL,
  PRIMARY KEY (ID_CTKM, ID_sản_phẩm),
  FOREIGN KEY (ID_CTKM) REFERENCES CTKM(ID_CTKM),
  FOREIGN KEY (ID_sản_phẩm) REFERENCES Sản_phẩm(ID_sản_phẩm)
);