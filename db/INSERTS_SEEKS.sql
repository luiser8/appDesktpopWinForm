-- Insert categories
INSERT INTO [dbo].[Category] ([CategoryItem])
VALUES
('Electronics'),
('Footwear'),
('Fashion'),
('Books'),
('Home'),
('Sports'),
('Toys'),
('Beauty'),
('Jewelry'),
('Grocery'),
('Appliances'),
('Furniture'),
('Garden'),
('Movies'),
('Music'),
('Pets');

-- Insert products
INSERT INTO [dbo].[Product] ([Name], [Price], [Stock], [Unit], [Category])
VALUES
('iPhone 13 Pro', 1099, 50, 1, 'Electronics'),
('Samsung Galaxy S21', 899, 100, 1, 'Electronics'),
('Sony PlayStation 5', 499, 25, 1, 'Electronics'),
('Canon EOS R5', 3899, 10, 1, 'Electronics'),
('Nintendo Switch', 299, 200, 1, 'Electronics'),
('Nike Air Force 1', 100, 150, 1, 'Footwear'),
('Adidas UltraBoost', 180, 75, 1, 'Footwear'),
('Gucci GG Marmont Bag', 1980, 30, 1, 'Fashion'),
('Rolex Submariner', 9000, 5, 1, 'Fashion'),
('Hermes Birkin Bag', 12000, 10, 1, 'Fashion'),
('MacBook Pro', 2399, 20, 1, 'Electronics'),
('Dell XPS 15', 1599, 15, 1, 'Electronics'),
('Samsung QLED 4K TV', 1499, 30, 1, 'Electronics'),
('Sony WH-1000XM4 Headphones', 349, 50, 1, 'Electronics'),
('Canon EOS 5D Mark IV', 2499, 5, 1, 'Electronics'),
('Bose QuietComfort Earbuds', 279, 40, 1, 'Electronics'),
('Apple Watch Series 7', 399, 70, 1, 'Electronics'),
('Nike Air Jordan 1', 170, 100, 1, 'Footwear'),
('Adidas Stan Smith', 80, 200, 1, 'Footwear'),
('Louis Vuitton Speedy Bag', 1200, 20, 1, 'Fashion'),
('Chanel Classic Flap Bag', 6000, 15, 1, 'Fashion'),
('HP Spectre x360', 1299, 25, 1, 'Electronics'),
('Lenovo ThinkPad X1 Carbon', 1699, 20, 1, 'Electronics'),
('LG OLED 4K TV', 1999, 35, 1, 'Electronics'),
('JBL Flip 5 Bluetooth Speaker', 99, 60, 1, 'Electronics'),
('Nikon D850', 3299, 10, 1, 'Electronics'),
('Sennheiser HD 660 S Headphones', 499, 30, 1, 'Electronics'),
('Fitbit Versa 3', 229, 90, 1, 'Electronics'),
('Puma RS-X3', 120, 150, 1, 'Footwear'),
('New Balance 990v5', 175, 100, 1, 'Footwear'),
('Book 1', 15, 50, 1, 'Books'),
('Book 2', 20, 70, 1, 'Books'),
('Home Decor 1', 50, 30, 1, 'Home'),
('Home Decor 2', 80, 25, 1, 'Home'),
('Soccer Ball', 20, 100, 1, 'Sports'),
('Basketball', 25, 80, 1, 'Sports'),
('Lego Set', 40, 120, 1, 'Toys'),
('Barbie Doll', 30, 90, 1, 'Toys'),
('Lipstick', 10, 200, 1, 'Beauty'),
('Perfume', 50, 150, 1, 'Beauty'),
('Necklace', 100, 50, 1, 'Jewelry'),
('Ring', 80, 80, 1, 'Jewelry'),
('Milk', 2, 500, 1, 'Grocery'),
('Bread', 3, 300, 1, 'Grocery'),
('Refrigerator', 1000, 10, 1, 'Appliances'),
('Washing Machine', 800, 15, 1, 'Appliances'),
('Sofa', 500, 20, 1, 'Furniture'),
('Dining Table', 300, 25, 1, 'Furniture'),
('Garden Tools', 50, 50, 1, 'Garden'),
('Plant Pot', 20, 80, 1, 'Garden'),
('John Wick: Chapter 3', 10, 100, 1, 'Movies'),
('John Wick: Chapter 4', 15, 80, 1, 'Movies'),
('EGOIST', 8, 200, 1, 'Music'),
('Kessoku Band', 12, 150, 1, 'Music'),
('Dog Food', 5, 100, 1, 'Pets'),
('Cat Food', 4, 120, 1, 'Pets');

-- Insert Rol
INSERT INTO [dbo].[Rol] ([RolName])
VALUES
('Administrador'),
('Contador'),
('Operador'),
('Vendedor');

-- Insert Usuario
INSERT INTO [dbo].[Account] ([RolId], [FirstName], [LastName], [UserName], [Password], [Email])
VALUES
(1, 'Luis Eduardo', 'Rondon', 'luis', '91c0c59c8f6fc9aa2dc99a89f2fd0ab5', 'leduardo.rondon@gmail.com'),
(2, 'Jose Luis', 'Diaz', 'jose', '91c0c59c8f6fc9aa2dc99a89f2fd0ab5', 'jose@gmail.com');

-- Insert Policies
INSERT INTO [dbo].[Policies] ([Name], [Description])
VALUES
('Inicio', 'Acceso al Inicio'),
('Productos', 'Acceso a Productos'),
('Categorias', 'Acceso a Categorias'),
('Ventas', 'Acceso a Ventas'),
('Facturacion', 'Acceso a Facturacion'),
('Bancos', 'Acceso a Bancos'),
('Metodos de pago', 'Acceso a Metodos de pago'),
('Usuarios', 'Acceso a Usuarios');

-- Insert Rol Policies
INSERT INTO [dbo].[RolPolicies] ([RolId], [PolicyId])
VALUES
(1, 1),
(1, 2),
(1, 3),
(1, 4),
(1, 5),
(1, 6),
(1, 7),
(1, 8),
(2, 1),
(2, 2),
(2, 3),
(2, 4),
(2, 5),
(2, 6),
(2, 7),
(3, 1),
(3, 2),
(3, 3),
(3, 4),
(4, 1),
(4, 2),
(4, 3),
(4, 4),
(4, 5);

-- Insert Banks
INSERT INTO [dbo].[Banks] ([Name], [Abbreviation])
VALUES
('Banco de Venezuela', 'BDV'),
('Banco Mercantil', 'Mercantil'),
('Banco Banesco', 'Banesco');

-- Insert PayMethods
INSERT INTO [dbo].[PayMethods] ([Name])
VALUES
('Deposito'),
('Tarjeta de debito'),
('Tarjeta de credito'),
('Transferencia');