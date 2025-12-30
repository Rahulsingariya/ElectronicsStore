-- Laptops Category (CategoryId = 1)
INSERT INTO Products (ProductName, Description, Price, StockQuantity, CategoryId, IsActive, CreatedDate, ImageUrl, DiscountPrice)
VALUES
('MacBook Pro M5 14-inch', 'Apple MacBook Pro with M5 chip, 16GB RAM, 512GB SSD, stunning Liquid Retina XDR display', 169999, 15, 1, 1, GETDATE(), NULL, 159999),
('Dell XPS 15', 'Premium laptop with Intel i9, 32GB RAM, 1TB SSD, 15.6" 4K OLED display', 149999, 20, 1, 1, GETDATE(), NULL, NULL),
('ASUS ROG Strix G16', 'Gaming laptop with RTX 4070, 16GB RAM, 512GB SSD, 16" QHD 165Hz display', 129999, 12, 1, 1, GETDATE(), NULL, 119999),
('Lenovo ThinkPad X1 Carbon', 'Business ultrabook with Intel i7, 16GB RAM, 512GB SSD, 14" WUXGA display', 119999, 18, 1, 1, GETDATE(), NULL, NULL),
('Microsoft Surface Laptop 6', 'Sleek laptop with Intel i7, 16GB RAM, 256GB SSD, 13.5" PixelSense touchscreen', 99999, 25, 1, 1, GETDATE(), NULL, 94999);

-- Smartphones Category (CategoryId = 2)
INSERT INTO Products (ProductName, Description, Price, StockQuantity, CategoryId, IsActive, CreatedDate, ImageUrl, DiscountPrice)
VALUES
('iPhone 16 Pro Max', 'Apple flagship with A18 Bionic, 256GB, 6.7" Super Retina XDR, 48MP camera system', 139999, 30, 2, 1, GETDATE(), NULL, NULL),
('Samsung Galaxy S25 Ultra', 'Flagship Android with Snapdragon 8 Gen 4, 256GB, 6.8" Dynamic AMOLED, 200MP camera', 129999, 35, 2, 1, GETDATE(), NULL, 119999),
('Google Pixel 9 Pro', 'AI-powered smartphone with Tensor G5, 256GB, 6.7" LTPO OLED, 50MP camera', 89999, 40, 2, 1, GETDATE(), NULL, NULL),
('Samsung Galaxy Z Fold 6', 'Foldable smartphone with 512GB, 7.6" main display, 50MP triple camera', 159999, 10, 2, 1, GETDATE(), NULL, 149999),
('OnePlus 13', 'Flagship killer with Snapdragon 8 Gen 4, 256GB, 6.7" AMOLED 120Hz, 50MP camera', 69999, 45, 2, 1, GETDATE(), NULL, 64999),
('iPhone 15', 'Previous gen Apple phone with A17 chip, 128GB, 6.1" Super Retina XDR display', 79999, 50, 2, 1, GETDATE(), NULL, 74999);

-- Headphones Category (CategoryId = 3)
INSERT INTO Products (ProductName, Description, Price, StockQuantity, CategoryId, IsActive, CreatedDate, ImageUrl, DiscountPrice)
VALUES
('AirPods Pro 2', 'Apple wireless earbuds with active noise cancellation, spatial audio, MagSafe charging', 24999, 60, 3, 1, GETDATE(), NULL, 22999),
('Sony WH-1000XM6', 'Industry-leading noise cancellation over-ear headphones, 30-hour battery, LDAC support', 34999, 40, 3, 1, GETDATE(), NULL, NULL),
('Bose QuietComfort Ultra', 'Premium ANC headphones with spatial audio, 24-hour battery, luxurious comfort', 39999, 25, 3, 1, GETDATE(), NULL, 36999),
('Samsung Galaxy Buds3 Pro', 'True wireless earbuds with intelligent ANC, 360 audio, 8-hour playtime', 19999, 55, 3, 1, GETDATE(), NULL, NULL),
('JBL Tune 770NC', 'Budget wireless ANC headphones, 70-hour battery, powerful bass', 9999, 80, 3, 1, GETDATE(), NULL, 8999),
('Sennheiser Momentum 4', 'Audiophile-grade wireless headphones, adaptive ANC, 60-hour battery', 32999, 30, 3, 1, GETDATE(), NULL, 29999);

-- Cameras Category (CategoryId = 4)
INSERT INTO Products (ProductName, Description, Price, StockQuantity, CategoryId, IsActive, CreatedDate, ImageUrl, DiscountPrice)
VALUES
('Sony A7 IV', 'Full-frame mirrorless camera, 33MP sensor, 4K 60fps video, 5-axis stabilization', 249999, 8, 4, 1, GETDATE(), NULL, NULL),
('Canon EOS R6 Mark II', 'Professional mirrorless camera, 24MP sensor, 40fps burst, 6K video', 229999, 10, 4, 1, GETDATE(), NULL, 219999),
('Nikon Z6 III', 'Versatile full-frame camera, 24MP sensor, 4K 120fps, in-body stabilization', 209999, 12, 4, 1, GETDATE(), NULL, NULL),
('GoPro Hero 13 Black', 'Action camera with 5.3K video, HyperSmooth 7.0, waterproof design', 42999, 35, 4, 1, GETDATE(), NULL, 39999),
('DJI Osmo Action 5 Pro', 'Vlogging camera with 4K 120fps, magnetic mounting, 10m waterproof', 39999, 30, 4, 1, GETDATE(), NULL, 36999),
('Fujifilm X-T5', 'Retro-styled mirrorless camera, 40MP sensor, film simulations, 4K 60fps', 169999, 15, 4, 1, GETDATE(), NULL, NULL);

-- Smartwatches Category (CategoryId = 5)
INSERT INTO Products (ProductName, Description, Price, StockQuantity, CategoryId, IsActive, CreatedDate, ImageUrl, DiscountPrice)
VALUES
('Apple Watch Series 10', 'Premium smartwatch with health tracking, ECG, blood oxygen, 18-hour battery', 44999, 50, 5, 1, GETDATE(), NULL, NULL),
('Samsung Galaxy Watch 7', 'Advanced health monitoring, sleep tracking, 40-hour battery, Wear OS', 29999, 60, 5, 1, GETDATE(), NULL, 27999),
('Garmin Fenix 8', 'Rugged multisport GPS watch, 29-day battery, advanced training metrics', 99999, 20, 5, 1, GETDATE(), NULL, NULL),
('Fitbit Charge 6', 'Fitness tracker with heart rate monitoring, stress management, 7-day battery', 14999, 70, 5, 1, GETDATE(), NULL, 12999),
('Google Pixel Watch 3', 'Stylish Wear OS smartwatch with Fitbit integration, health insights', 34999, 40, 5, 1, GETDATE(), NULL, NULL),
('Amazfit GTR 4', 'Budget smartwatch with 14-day battery, 150+ sports modes, GPS', 12999, 90, 5, 1, GETDATE(), NULL, 10999);
