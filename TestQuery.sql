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