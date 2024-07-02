SELECT * FROM [dbo].[ORDERS]
SELECT * FROM [dbo].[ORDERITEM]


DELETE FROM [dbo].[ORDERITEM]
DELETE FROM [dbo].[ORDERS]
DELETE FROM [dbo].[RINGPAIRING]
DELETE FROM [dbo].[PENDANTPAIRING]
DELETE FROM [dbo].[EARRINGPAIRING]
GO

DELETE FROM [dbo].[RINGSHAPEDETAIL]
DELETE FROM [dbo].[RING]
DELETE FROM [dbo].[SHAPE]
DELETE FROM [dbo].[METALTYPE]
DELETE FROM [dbo].[RINGSUBTYPE]
DELETE FROM [dbo].[RINGTYPE]

-- Sample data insertion
INSERT INTO RINGTYPE (TypeName) VALUES ('Solitaire'), ('Halo');
INSERT INTO RINGSUBTYPE (RingTypeID, SubtypeName) VALUES 
((SELECT RingTypeID FROM RINGTYPE WHERE TypeName='Solitaire'), 'Classic'),
((SELECT RingTypeID FROM RINGTYPE WHERE TypeName='Halo'), 'Twisted');
INSERT INTO METALTYPE (MetalTypeName) VALUES ('14k White Gold'), ('18k Yellow Gold');
INSERT INTO SHAPE (ShapeName) VALUES ('Round'), ('Emerald');

-- Adding a ring example
INSERT INTO RING (RingTypeID, RingSubtypeID, MetalTypeID, RingName, Price, StockQuantity) VALUES 
((SELECT RingTypeID FROM RINGTYPE WHERE TypeName='Solitaire'),
 (SELECT RingSubtypeID FROM RINGSUBTYPE WHERE SubtypeName='Classic' AND RingTypeID = (SELECT RingTypeID FROM RINGTYPE WHERE TypeName='Solitaire')),
 (SELECT MetalTypeID FROM METALTYPE WHERE MetalTypeName='14k White Gold'),
 'Classic Six-Prong Solitaire Engagement Ring in 14k White Gold', 1000.00, 10);

-- Adding ring shape details
INSERT INTO RINGSHAPEDETAIL (RingID, ShapeID, ImageURL, FrameDescription) VALUES
((SELECT RingID FROM RING WHERE RingName='Classic Six-Prong Solitaire Engagement Ring in 14k White Gold'),
 (SELECT ShapeID FROM SHAPE WHERE ShapeName='Round'),
 'imageURL_Round.jpg', 'Round frame suitable for round diamond'),
((SELECT RingID FROM RING WHERE RingName='Classic Six-Prong Solitaire Engagement Ring in 14k White Gold'),
 (SELECT ShapeID FROM SHAPE WHERE ShapeName='Emerald'),
 'imageURL_Emerald.jpg', 'Square frame suitable for emerald diamond');

SELECT 
    r.RingName, 
    rsd.ImageURL, 
    rsd.FrameDescription 
FROM 
    RING r
JOIN 
    RINGSHAPEDETAIL rsd ON r.RingID = rsd.RingID
JOIN 
    SHAPE s ON rsd.ShapeID = s.ShapeID
WHERE 
    s.ShapeName = 'Round';