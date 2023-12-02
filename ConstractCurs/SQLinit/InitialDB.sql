USE ComputerClub;

DELETE FROM Foods;

INSERT INTO Foods (FoodInfo,Count, Price)
VALUES 
		('Энергетик "Adrenaline"',10,150),
		('Кола 0.5',20,75),
		('Вода',30,50);

DELETE FROM Users;

INSERT INTO Users (FirstName, Username, Password, UserType,Balance,Bonuses)
VALUES
		('Илья','soondio', 'password1', 'Client', 1000,200),
		('Далер','iridon', 'm532203', 'Client', 400, 150),
		('Владислав','vladislave', 'admin_password', 'Administrator', 10000,2000);

DELETE FROM FoodOrders;

INSERT INTO FoodOrders (UserID, FoodId, FoodCount, OrderDateTime, TotalPrice, OrderStatus)
VALUES
  (1, 1, 2, '2023-11-30 14:00:00', 25.50, 'выполнен'),
  (2, 2, 1, '2023-11-29 17:30:00', 15.75, 'забронирован'),
  (3, 3, 3, '2023-11-28 08:45:00', 32.20, 'отменён'),
  (1, 1, 1, '2023-11-30 12:00:00', 12.80, 'выполнен'),
  (2, 2, 2, '2023-11-29 20:00:00', 22.90, 'отменён'),
  (3, 3, 1, '2023-11-28 10:30:00', 10.50, 'выполнен');

DELETE FROM ComputerPlaces;

INSERT INTO ComputerPlaces (Name, PricePerHour)
VALUES
    ('Компьютер #1', 150),
    ('Компьютер #2',70),
    ('Компьютер #3',80),
	('Компьютер #4',60),
	('Компьютер #5',200);

DELETE FROM VideoCard;

INSERT INTO VideoCard (Name, Specifications)
VALUES
    ('NVIDIA GeForce GTX 1660', '6GB GDDR5'),
    ('AMD Radeon RX 570', '4GB GDDR5'),
    ('NVIDIA GeForce RTX 3080', '10GB GDDR6X');

DELETE FROM RAM;

INSERT INTO RAM (Name, Specifications)
VALUES
    ('Corsair Vengeance LPX', '16GB DDR4 3200MHz'),
    ('Kingston HyperX Fury', '8GB DDR4 2666MHz'),
    ('Crucial Ballistix Sport', '32GB DDR4 3600MHz');

DELETE FROM Processor;

INSERT INTO Processor (Name, Specifications)
VALUES
    ('Intel Core i7-9700K', '8-core, 3.6GHz'),
    ('AMD Ryzen 5 3600', '6-core, 3.6GHz'),
    ('Intel Core i9-9900K', '8-core, 3.6GHz');

DELETE FROM Headphones;

INSERT INTO Headphones (Name, Specifications)
VALUES
    ('SteelSeries Arctis 7', 'Wireless, 7.1 Surround Sound'),
    ('HyperX Cloud II', 'Wired, Virtual 7.1 Surround Sound'),
    ('Logitech G Pro X', 'Wired, Detachable Mic');

DELETE FROM KeyBoard;

INSERT INTO Keyboard (Name, Specifications)
VALUES
    ('Corsair K95 RGB Platinum XT', 'Mechanical, RGB Backlit'),
    ('Razer BlackWidow Elite', 'Mechanical, Chroma RGB Lighting'),
    ('Logitech G Pro X', 'Mechanical, Tenkeyless');

DELETE FROM Mouse;

INSERT INTO Mouse (Name, Specifications)
VALUES
    ('Logitech G Pro Wireless', 'Wireless, HERO Sensor'),
    ('SteelSeries Rival 600', 'Wired, Dual Sensors'),
    ('Razer DeathAdder Elite', 'Wired, Optical Sensor');

DELETE FROM Monitor;

INSERT INTO Monitor (Name, Specifications)
VALUES 
  ('ASUS VG245H', '24-inch, 1920x1080, TN, 75Hz, FreeSync'),
  ('LG 27GL83A-B', '27-inch, 2560x1440, Nano IPS, 144Hz, G-Sync Compatible'),
  ('Samsung Odyssey G9', '49-inch ultrawide, 5120x1440, QLED, 240Hz, HDR1000');
 
 DELETE FROM ComputerComposition;

 INSERT INTO ComputerComposition (ProcessorID, VideoCardID, RAMID, HeadphonesID, KeyboardID, MouseID, MonitorID, PlaceId)
VALUES
  (1, 2, 3, 1, 2, 3, 1, 1),
  (2, 3, 1, 2, 3, 1, 2, 2),
  (3, 1, 2, 3, 1, 2, 3, 3),
  (1, 2, 3, 1, 2, 3, 1, 4),
  (2, 3, 1, 2, 3, 1, 2, 5);
	