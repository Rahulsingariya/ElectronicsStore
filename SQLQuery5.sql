-- Automatically set image URLs for all products
-- This assumes your image files are named EXACTLY like product names with .jpg extension

UPDATE Products 
SET ImageUrl = '/images/products/' + ProductName + '.jpg';