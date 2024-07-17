SELECT * FROM [DIAMONDPROJECT].[dbo].[ORDERS]
SELECT * FROM [dbo].[ORDERITEM]


--See if order stuffs still exist
SELECT * FROM [dbo].[RINGPAIRING]
SELECT * FROM [dbo].[PENDANTPAIRING]
SELECT * FROM [dbo].[EARRINGPAIRING]

SELECT * FROM DIAMOND

--Reset orders
DELETE FROM [dbo].[ORDERITEM]
DELETE FROM [dbo].[ORDERS]
DELETE FROM [dbo].[RINGPAIRING]
DELETE FROM [dbo].[PENDANTPAIRING]
DELETE FROM [dbo].[EARRINGPAIRING]
UPDATE DIAMOND
SET Available = 1;
--Ends here

SELECT * FROM [dbo].[CUSTOMER]
DELETE FROM [dbo].[CUSTOMER]

--Don't touch here oge
SELECT 
    r.RingName, 
    rsd.ImageURL, 
    rsd.FrameDescription 
FROM 
    RING r
	--ogey (=w=)=b
JOIN 
    RINGSHAPEDETAIL rsd ON r.RingID = rsd.RingID
JOIN 
    SHAPE s ON rsd.ShapeID = s.ShapeID
WHERE 
    s.ShapeName = 'Round';

-- For testing hangfire
UPDATE Orders
SET OrderDate = DATEADD(DAY, -1, OrderDate)
WHERE OrderID = '4ABAD377-5D48-4CC6-B953-0572E34992B0';

UPDATE ORDERS
SET OrderEmail = 'khangbinh167@gmail.com'

UPDATE ORDERS
SET OrderStatus = 'Processing'
WHERE OrderID = '4ABAD377-5D48-4CC6-B953-0572E34992B0';

SELECT * FROM [DIAMONDPROJECT].[dbo].[ORDERS]
SELECT * FROM [dbo].[ORDERITEM]