

-- Insert values
use G23_NHNT
DBCC CHECKIDENT ('[Accounts]', RESEED, 0);
GO
delete from HouseType
DBCC CHECKIDENT ('[HouseType]', RESEED, 0);
GO
delete from Houses
DBCC CHECKIDENT ('[Houses]', RESEED, 0);
GO

DBCC CHECKIDENT ('[HouseDetails]', RESEED, 0);
GO

DBCC CHECKIDENT ('[Amenities]', RESEED, 0);
GO


DBCC CHECKIDENT ('[Reviews]', RESEED, 0);
GO





-- Insert data into Account
INSERT INTO Accounts (userName, password, Role) VALUES
(N'user1', N'123', 1),  -- House owner
(N'user2', N'345', 2),  -- Tenant
(N'user3', N'password3', 1),
(N'user4', N'password4', 2),
(N'user5', N'password5', 1);

select *from Accounts

INSERT INTO HouseType (Name, Type)
VALUES 
    (N'Nhà trọ & phòng trọ', 1),
    (N'Nhà nguyên căn', 2),
    (N'Nhà tập thể', 3),
    (N'Kí túc xá', 4),
    (N'Chung cư mi ni', 5);

	select *from HouseType

-- Insert data into House
INSERT INTO Houses (nameHouse, idUser,HouseTypeId) VALUES
(N'Nhà phố 1',  1,1),
(N'Chung cư 1',  2,2),
(N'Nhà trọ 1', 1,3),
(N'Nhà phố 2', 3,4),
(N'Chung cư 2', 4,5);

select *from Houses

-- Insert data into HouseDetail
INSERT INTO HouseDetails (idHouse, address, price, dienTich, tienDien, tienNuoc, tienDV, describe, status, image, timePost) VALUES
(1, N'123 Đường ABC, TP HN', 5000000, 100, N'200.000', N'100.000', N'50.000', N'Nhà mới xây, đầy đủ tiện nghi', N'Đang cho thuê', NULL, GETDATE()),
(2, N'456 Đường DEF, TP HN', 7000000, 80, N'250.000', N'120.000', N'60.000', N'Chung cư cao cấp, an ninh tốt', N'Đang cho thuê', NULL, GETDATE()),
(3, N'789 Đường GHI, TP HN', 3000000, 50, N'150.000', N'80.000', N'30.000', N'Nhà trọ tiện lợi, gần trường', N'Đang cho thuê', NULL, GETDATE()),
(4, N'101 Đường JKL, TP HN', 4500000, 70, N'200.000', N'90.000', N'40.000', N'Nhà phố thoáng mát', N'Đang cho thuê', NULL, GETDATE()),
(5, N'202 Đường MNO, TP HN', 8000000, 90, N'300.000', N'110.000', N'70.000', N'Chung cư gần trung tâm', N'Đang cho thuê', NULL, GETDATE());

-- Insert data into Amenities
INSERT INTO Amenities (name) VALUES
(N'Wifi'),
(N'Máy lạnh'),
(N'Tủ lạnh'),
(N'Bếp'),
(N'Giặt ủi'),
(N'Máy giặt'),
(N'Quạt điện'),
(N'Tủ quần áo'),
(N'Khu để xe'),
(N'Giường và đệm'),
(N'Bàn ghế học tập/làm việc'),
(N'Camera an ninh');

-- Insert data into HouseAmenities
INSERT INTO AmenityHouse (IdAmenitiesIdAmenity, IdHousesIdHouse) VALUES
(3, 1), -- House 1 has Wifi
(4, 2), -- House 1 has Air conditioning
(5, 3), -- Apartment 1 has Refrigerator
(6, 4), -- Apartment 1 has Stove
(7, 5); -- House 3 has Laundry service

-- Insert data into Review
INSERT INTO Reviews (idUser, idHouse, rating, content, reviewDate) VALUES
(1, 1, 5, N'Nhà đẹp và đầy đủ tiện nghi', GETDATE()),
(1, 2, 4, N'Chung cư sạch sẽ và thoải mái', GETDATE()),
(3, 1, 3, N'Nhà có vị trí tốt nhưng hơi ồn', GETDATE()),
(5, 3, 5, N'Nhà trọ gần trường học, tiện nghi đầy đủ', GETDATE());


drop table HouseType 
-- DECLARE @DatabaseName nvarchar(50)
-- SET @DatabaseName = N'G23_NHNT'

-- DECLARE @SQL varchar(max)

-- SELECT @SQL = COALESCE(@SQL,'') + 'Kill ' + Convert(varchar, SPId) + ';'
-- FROM MASTER..SysProcesses
-- WHERE DBId = DB_ID(@DatabaseName) AND SPId <> @@SPId

-- --SELECT @SQL 
-- EXEC(@SQL)

DROP DATABASE G23_NHNT

CREATE DATABASE G23_NHNT;
