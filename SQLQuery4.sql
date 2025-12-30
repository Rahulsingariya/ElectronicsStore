


UPDATE Products 
SET ImageUrl = '/images/products/' + REPLACE(ProductName, ' ', '') + '.jpg';


UPDATE Products SET ImageUrl = '/images/products/MacBookProM514-inch.jpg' WHERE ProductName = 'MacBook Pro M5 14-inch';
UPDATE Products SET ImageUrl = '/images/products/DellXPS15.jpg' WHERE ProductName = 'Dell XPS 15';
UPDATE Products SET ImageUrl = '/images/products/HPPavilion14.jpg' WHERE ProductName = 'HP Pavilion 14';
