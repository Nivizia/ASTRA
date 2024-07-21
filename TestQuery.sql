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

SELECT * FROM RING WHERE RingID = '2A097147-C80E-4D8F-8289-00D6A8A26157'
SELECT * FROM RINGTYPE WHERE RingTypeID = '9C5B5F5D-B782-442F-98FC-E9EF6B1D5D2F'
SELECT * FROM RINGSUBTYPE WHERE RingSubtypeID = ''
SELECT * FROM FRAMETYPE WHERE FrameTypeID = 'D593848E-E179-435B-BFAC-973ED171B42A'
SELECT * FROM STONECUT WHERE StoneCutID = 'B6F293E6-3391-42F5-947B-B97054596C39'