create database G23_NHNT
use G23_NHNT

create table Account(
	idUser int primary key identity,
	userName nvarchar(50) not null,
	password nvarchar(50) not null,
	Role int not null check ( Role between 1 and 2 )  /* với 1 là người cho thuê, 2 là người thuê trọ */ 
)

CREATE TABLE House (
    idHouse INT PRIMARY KEY identity,
    nameHouse NVARCHAR(255) NOT NULL,
    quantityRoom INT NULL,
	category NVARCHAR(50) NOT NULL,  -- Loại nhà (Nhà nguyên căn, Nhà trọ, Chung cư, ...)
    idUser INT NOT NULL, 
    CONSTRAINT FK_House_Account FOREIGN KEY (idUser) REFERENCES Account(idUser) -- Liên kết với bảng Account
);

CREATE TABLE HouseDetail (
    idHouseDetail INT PRIMARY KEY identity,
    idHouse INT NOT NULL,  -- Liên kết tới bảng House
    address NVARCHAR(255) NOT NULL,
    price DECIMAL(10, 2) NOT NULL,
    dienTich FLOAT NOT NULL,  -- Diện tích nhà
    tienDien NVARCHAR(20) NOT NULL,  -- Tiền điện
    tienNuoc NVARCHAR(20) NOT NULL,  -- Tiền nước
    tienDV Nvarchar(20) NOT NULL,  -- Tiền dịch vụ khác
    describe NVARCHAR(MAX) NULL,
    status NVARCHAR(50) NOT NULL,  -- Trạng thái (ví dụ: Đang cho thuê, Đã thuê)
    image NVARCHAR(255) NULL,  -- Đường dẫn ảnh
    timePost DATETIME NOT NULL,  -- Thời gian đăng
    CONSTRAINT FK_HouseDetail_House FOREIGN KEY (idHouse) REFERENCES House(idHouse)
);



CREATE TABLE Room (
    idRoom INT PRIMARY KEY identity,
    nameRoom NVARCHAR(255) NOT NULL,
    typeRoom NVARCHAR(50) NOT NULL,  -- Loại phòng (phòng đơn, phòng đôi, ...)
    idUser INT NOT NULL,  -- Người đăng phòng (chủ nhà hoặc người tìm phòng)
    CONSTRAINT FK_Room_Account FOREIGN KEY (idUser) REFERENCES Account(idUser)  -- Liên kết với bảng Account
);

CREATE TABLE RoomDetail (
    idRoomDetail INT PRIMARY KEY identity,
    idRoom INT NOT NULL,  -- Liên kết tới bảng Room
    price DECIMAL(10, 2) NOT NULL,
	address nvarchar(50) not null,
    dienTich FLOAT NOT NULL,  -- Diện tích phòng
    tienDien nvarchar(20) NOT NULL,
    tienNuoc nvarchar(20) NOT NULL,
    tienDV nvarchar(20) NOT NULL,
    describe NVARCHAR(MAX) NULL,
    status NVARCHAR(50) NOT NULL,  -- Trạng thái phòng (Đang cho thuê, Đã thuê)
    image NVARCHAR(255) NULL,  -- Đường dẫn ảnh
    timePost DATETIME NOT NULL,  -- Thời gian đăng
    CONSTRAINT FK_RoomDetail_Room FOREIGN KEY (idRoom) REFERENCES Room(idRoom)
);


CREATE TABLE Amenities (
    idAmenity INT PRIMARY KEY identity,
    name NVARCHAR(100) NOT NULL
);
create table RoomAmenities (
	idRoom INT NOT NULL,
    idAmenity INT NOT NULL,
    PRIMARY KEY (idRoom, idAmenity),
    FOREIGN KEY (idRoom) REFERENCES Room(idRoom),
    FOREIGN KEY (idAmenity) REFERENCES Amenities(idAmenity)
)

create table HouseAmenities (
    idHouse INT NOT NULL,
    idAmenity INT NOT NULL,
    PRIMARY KEY (idHouse, idAmenity),
    FOREIGN KEY (idHouse) REFERENCES House(idHouse),
    FOREIGN KEY (idAmenity) REFERENCES Amenities(idAmenity)
);


CREATE TABLE Review (
    idReview int identity primary key,
    idUser int not null,
    idHouse int null,
    idRoom int null,
    rating int check (rating between 1 and 5),
    content nvarchar(max),
    reviewDate DATETIME default Current_Timestamp,
    FOREIGN KEY (idUser) REFERENCES Account(idUser),
    FOREIGN KEY (idRoom) REFERENCES Room(idRoom),
    FOREIGN KEY (idHouse) REFERENCES House(idHouse),
    CHECK ((idHouse IS NOT NULL AND idRoom IS NULL) OR (idHouse IS NULL AND idRoom IS NOT NULL)) -- Đảm bảo chỉ một trong hai trường là NULL
);
-- Chèn dữ liệu vào bảng Account
INSERT INTO Account (userName, password, Role) VALUES
(N'user1', N'123', 1),  -- Chủ nhà
(N'user2', N'345', 2),  -- Người thuê
(N'user3', N'password3', 1),
(N'user4', N'password4', 2),
(N'user5', N'password5', 1);

-- Chèn dữ liệu vào bảng House
INSERT INTO House (nameHouse, quantityRoom, category, idUser) VALUES
(N'Nhà phố 1', 5, N'Nhà nguyên căn', 1),
(N'Chung cư 1', 10, N'Chung cư', 2),
(N'Nhà trọ 1', 20, N'Nhà trọ', 1),
(N'Nhà phố 2', 3, N'Nhà nguyên căn', 3),
(N'Chung cư 2', 15, N'Chung cư', 4);

-- Chèn dữ liệu vào bảng HouseDetail
INSERT INTO HouseDetail (idHouse, address, price, dienTich, tienDien, tienNuoc, tienDV, describe, status, image, timePost) VALUES
(1, N'123 Đường ABC, TP HCM', 5000000, 100, N'200.000', N'100.000', N'50.000', N'Nhà mới xây, đầy đủ tiện nghi', N'Đang cho thuê', NULL, GETDATE()),
(2, N'456 Đường DEF, TP HCM', 7000000, 80, N'250.000', N'120.000', N'60.000', N'Chung cư cao cấp, an ninh tốt', N'Đang cho thuê', NULL, GETDATE()),
(3, N'789 Đường GHI, TP HCM', 3000000, 50, N'150.000', N'80.000', N'30.000', N'Nhà trọ tiện lợi, gần trường', N'Đang cho thuê', NULL, GETDATE()),
(4, N'101 Đường JKL, TP HCM', 4500000, 70, N'200.000', N'90.000', N'40.000', N'Nhà phố thoáng mát', N'Đang cho thuê', NULL, GETDATE()),
(5, N'202 Đường MNO, TP HCM', 8000000, 90, N'300.000', N'110.000', N'70.000', N'Chung cư gần trung tâm', N'Đang cho thuê', NULL, GETDATE());

update HouseDetail
set image = 'https://cdn.vatgia.com/pictures/thumb/0x0/2015/04/hjf1429101120.png'
where idHouse = 4
				

-- Chèn dữ liệu vào bảng Room
INSERT INTO Room (nameRoom, typeRoom, idUser) VALUES
(N'Phòng đơn 1', N'Phòng đơn', 1),
(N'Phòng đôi 1', N'Phòng đôi', 2),
(N'Phòng đơn 2', N'Phòng đơn', 3),
(N'Phòng đôi 2', N'Phòng đôi', 4),
(N'Phòng đơn 3', N'Phòng đơn', 1);

-- Chèn dữ liệu vào bảng RoomDetail
INSERT INTO RoomDetail (idRoom, price, address, dienTich, tienDien, tienNuoc, tienDV, describe, status, image, timePost) VALUES
(1, 1500000, N'123 Đường ABC, TP HCM', 25, N'50.000', N'30.000', N'20.000', N'Phòng sạch sẽ, thoáng mát', N'Đang cho thuê',N'https://trongoixaynha.com/wp-content/uploads/2023/04/chi-phi-xay-phong-tro-35m2-4.jpg', GETDATE()),
(2, 2000000, N'456 Đường DEF, TP HCM', 30, N'60.000', N'40.000', N'30.000', N'Phòng rộng, đầy đủ tiện nghi', N'Đang cho thuê', N'https://trongoixaynha.com/wp-content/uploads/2023/04/chi-phi-xay-phong-tro-35m2-8.jpg', GETDATE()),
(3, 1700000, N'789 Đường GHI, TP HCM', 20, N'55.000', N'35.000', N'25.000', N'Phòng yên tĩnh, thích hợp cho sinh viên', N'Đang cho thuê', N'https://ecogreen-saigon.vn/uploads/phong-tro-la-loai-hinh-nha-o-pho-bien-gia-re-tien-loi-cho-sinh-vien-va-nguoi-di-lam.png', GETDATE()),
(4, 2500000, N'101 Đường JKL, TP HCM', 35, N'65.000', N'45.000', N'35.000', N'Phòng đẹp, gần trường học', N'Đang cho thuê', N'https://namidesign.vn/wp-content/uploads/2021/10/thiet-ke-nha-tro-6.jpg', GETDATE()),
(5, 1800000, N'202 Đường MNO, TP HCM', 28, N'52.000', N'38.000', N'28.000', N'Phòng có ban công, thoáng mát', N'Đang cho thuê', N'https://xaydungtienthanh.vn/wp-content/uploads/2020/09/thiet-ke-phong-tro-tro-dep-co-gac-4-1000x692.jpg', GETDATE());

-- Chèn dữ liệu vào bảng Amenities
INSERT INTO Amenities (name) VALUES
(N'Wifi'),
(N'Máy lạnh'),
(N'Tủ lạnh'),
(N'Bếp'),
(N'Giặt ủi')

INSERT INTO Amenities (name) VALUES
(N'Máy giặt'),
(N'Quạt điện'),
(N'Tủ quần áo'),
(N'Khu để xe'),
(N'Giường và đệm'),
(N'Bàn ghế học tập/làm việc'),
(N'Camera an ninh')

-- Chèn dữ liệu vào bảng RoomAmenities
INSERT INTO RoomAmenities (idRoom, idAmenity) VALUES
(1, 1), -- Phòng đơn 1 có Wifi
(1, 2), -- Phòng đơn 1 có Máy lạnh
(2, 3), -- Phòng đôi 1 có Tủ lạnh
(2, 4), -- Phòng đôi 1 có Bếp
(3, 5); -- Phòng đơn 2 có Giặt ủi

-- Chèn dữ liệu vào bảng HouseAmenities
INSERT INTO HouseAmenities (idHouse, idAmenity) VALUES
(1, 1), -- Nhà phố 1 có Wifi
(1, 2), -- Nhà phố 1 có Máy lạnh
(2, 3), -- Chung cư 1 có Tủ lạnh
(2, 4), -- Chung cư 1 có Bếp
(3, 5); -- Nhà trọ 1 có Giặt ủi

-- Chèn dữ liệu vào bảng Review
INSERT INTO Review (idUser, idHouse, idRoom, rating, content, reviewDate) VALUES
(3, 1, NULL, 5, N'đẹppppppp tesstt', GETDATE()),
(2, NULL, 2, 4, N'Phòng rộng rãi, tiện nghi đầy đủ.', GETDATE()),
(3, 1, NULL, 3, N'Nhà có vị trí tốt nhưng hơi ồn.', GETDATE()),
(4, NULL, 1, 5, N'Phòng rất đẹp và yên tĩnh.', GETDATE()),
(5, 3, NULL, 4, N'Nhà ở sạch sẽ, gần chợ.', GETDATE());


INSERT INTO Review (idUser, idHouse, idRoom, rating, content, reviewDate) VALUES
(2, 2, NULL, 5, N'đẹppppppp tesst32', GETDATE())


update Account
set password = N'123'
where idUser = 1;


SELECT * FROM Account;
SELECT * FROM House;
SELECT * FROM HouseDetail;
SELECT * FROM Room;
SELECT * FROM RoomDetail;
SELECT * FROM Amenities;
SELECT * FROM RoomAmenities;
SELECT * FROM HouseAmenities;
SELECT * FROM Review;

delete Account
delete House
delete HouseDetail
delete RoomDetail
delete Room
delete Amenities
delete RoomAmenities
delete HouseAmenities
delete Review


drop table Account
drop table House
drop table HouseDetail
drop table RoomDetail
drop table Room
drop table Amenities
drop table HouseAmenities
drop table RoomAmenities
drop table Review