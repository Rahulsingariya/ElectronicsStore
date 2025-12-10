-- Update image URLs based on product names
-- Images should be named exactly like: "ProductName.jpg"

UPDATE Products 
SET ImageUrl = '/images/products/' + REPLACE(ProductName, ' ', '') + '.jpg';

-- If your filenames have spaces, use this instead:
-- UPDATE Products 
-- SET ImageUrl = '/images/products/' + ProductName + '.jpg';

-- OR if your files are named differently, update individually:
-- Example for specific products:
UPDATE Products SET ImageUrl = '/images/products/MacBookProM514-inch.jpg' WHERE ProductName = 'MacBook Pro M5 14-inch';
UPDATE Products SET ImageUrl = '/images/products/DellXPS15.jpg' WHERE ProductName = 'Dell XPS 15';
UPDATE Products SET ImageUrl = '/images/products/HPPavilion14.jpg' WHERE ProductName = 'HP Pavilion 14';
-- ... repeat for all products