-- Update orders with missing names
UPDATE Orders
SET FullName = 'Customer (Order #' + CAST(OrderId AS VARCHAR) + ')'
WHERE FullName IS NULL OR FullName = '';