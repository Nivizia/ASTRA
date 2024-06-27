USE DIAMONDPROJECT;
GO

DELETE FROM PENDANT;
DELETE FROM RING;
DELETE FROM DIAMOND;

INSERT INTO DIAMOND (Price, D_Type, CaratWeight, Color, Clarity, Cut)
VALUES
(4500, 'Princess', '1.20', 7, 5, 2),
(4100, 'Emerald', '1.50', 4, 3, 3),
(3990, 'Oval', '1.10', 8, 4, 2),
(3850, 'Pear', '0.95', 6, 2, 1),
(3700, 'Marquise', '1.25', 5, 6, 3),
(3550, 'Asscher', '1.30', 7, 7, 4),
(3400, 'Cushion', '1.40', 8, 8, 4),
(3250, 'Radiant', '1.15', 3, 3, 1),
(3100, 'Heart', '1.00', 2, 2, 2),
(2950, 'Round', '1.05', 1, 1, 1),
(2800, 'Princess', '1.10', 7, 5, 3),
(2650, 'Emerald', '0.90', 4, 4, 2),
(2500, 'Oval', '0.85', 6, 2, 1),
(2350, 'Pear', '0.75', 5, 6, 3),
(2200, 'Marquise', '1.05', 3, 7, 4),
(2050, 'Asscher', '1.20', 7, 4, 2),
(1900, 'Cushion', '0.95', 8, 3, 1),
(1750, 'Radiant', '0.85', 2, 2, 3),
(1600, 'Heart', '1.00', 1, 8, 4),
(1450, 'Round', '1.20', 8, 6, 2);
GO


INSERT INTO RING (Name, Price, StockQuantity, MetalType, RingSize)
VALUES
('Classic Solitaire Engagement Ring In 18k Yellow Gold', 1200, 75, '18K Yellow Gold', '2.50mm'),
('Vintage Halo Engagement Ring In Platinum', 3400, 50, 'Platinum', '2.20mm'),
('Elegant Diamond Ring In 14k Rose Gold', 980, 120, '14K Rose Gold', '2.10mm'),
('Twisted Shank Diamond Ring In 14k White Gold', 1350, 90, '14K White Gold', '2.30mm'),
('Modern Tension Setting Ring In 14k Yellow Gold', 1500, 85, '14K Yellow Gold', '3.00mm'),
('Three-Stone Diamond Ring In 18k White Gold', 2750, 60, '18K White Gold', '2.50mm'),
('Princess Cut Diamond Ring In Platinum', 3200, 40, 'Platinum', '2.80mm'),
('Bezel Set Diamond Ring In 14k Rose Gold', 1100, 110, '14K Rose Gold', '2.00mm'),
('Eternity Band Diamond Ring In 18k White Gold', 2100, 70, '18K White Gold', '2.60mm'),
('Cathedral Diamond Ring In 14k White Gold', 1450, 80, '14K White Gold', '2.40mm'),
('Cushion Cut Diamond Ring In 14k Yellow Gold', 1600, 95, '14K Yellow Gold', '2.20mm'),
('Pave Diamond Ring In 14k White Gold', 1300, 100, '14K White Gold', '2.00mm'),
('Halo Diamond Ring In 18k Yellow Gold', 1800, 65, '18K Yellow Gold', '2.50mm'),
('Double Halo Diamond Ring In Platinum', 3500, 30, 'Platinum', '2.90mm'),
('Solitaire Diamond Ring In 14k Rose Gold', 950, 115, '14K Rose Gold', '2.10mm'),
('Infinity Twist Diamond Ring In 14k White Gold', 1400, 75, '14K White Gold', '2.30mm'),
('Vintage Style Diamond Ring In 18k White Gold', 2700, 50, '18K White Gold', '2.70mm'),
('Round Brilliant Cut Diamond Ring In Platinum', 3100, 45, 'Platinum', '2.80mm'),
('Split Shank Diamond Ring In 14k Yellow Gold', 1550, 90, '14K Yellow Gold', '2.00mm'),
('Art Deco Diamond Ring In 18k White Gold', 2500, 55, '18K White Gold', '2.60mm');
GO


INSERT INTO PENDANT (Name, Price, StockQuantity, ChainType, ChainLength, ClaspType)
VALUES
    ('Elegant Pearl Pendant', 150, 25, 'Cable Chain', '16-18 inches', 'Lobster Claw Clasp'),
    ('Sparkling Diamond Pendant', 251, 30, 'Box Chain', '18 inches', 'Spring Ring Clasp'),
    ('Golden Heart Pendant', 200, 10, 'Rope Chain', '20 inches', 'Magnetic Clasp'),
    ('Silver Star Pendant', 121, 50, 'Snake Chain', '22 inches', 'Toggle Clasp'),
    ('Rose Gold Infinity Pendant', 175, 15, 'Figaro Chain', '16 inches', 'Box Clasp'),
    ('Emerald Teardrop Pendant', 220, 20, 'Ball Chain', '18-20 inches', 'Barrel Clasp'),
    ('Blue Sapphire Pendant', 200, 12, 'Singapore Chain', '20 inches', 'S Hook Clasp'),
    ('Ruby Pendant with Diamonds', 321, 5, 'Wheat Chain', '24 inches', 'Fish Hook Clasp'),
    ('Platinum Cross Pendant', 275, 18, 'Cable Chain', '16-18 inches', 'Spring Ring Clasp'),
    ('Amethyst Circle Pendant', 146, 25, 'Rope Chain', '18 inches', 'Magnetic Clasp'),
    ('Pearl Pendant in Gold', 180, 22, 'Snake Chain', '20 inches', 'Lobster Claw Clasp'),
    ('Garnet Heart Pendant', 160, 28, 'Box Chain', '22 inches', 'Toggle Clasp'),
    ('Peridot Drop Pendant', 190, 16, 'Figaro Chain', '16-18 inches', 'Box Clasp'),
    ('Citrine Sun Pendant', 175, 10, 'Ball Chain', '18-20 inches', 'Barrel Clasp'),
    ('Opal Teardrop Pendant', 211, 7, 'Singapore Chain', '20 inches', 'S Hook Clasp'),
    ('Aquamarine Pendant', 195, 30, 'Wheat Chain', '24 inches', 'Fish Hook Clasp'),
    ('Turquoise Oval Pendant', 170, 12, 'Cable Chain', '16-18 inches', 'Spring Ring Clasp'),
    ('Onyx Stone Pendant', 150, 20, 'Rope Chain', '18 inches', 'Magnetic Clasp'),
    ('Moonstone Pendant', 226, 10, 'Snake Chain', '20 inches', 'Lobster Claw Clasp'),
    ('Topaz Pendant with Silver', 205, 18, 'Box Chain', '22 inches', 'Toggle Clasp');
GO