DELETE FROM reservation;
DELETE FROM site;
DELETE FROM campground;
DELETE FROM park;


INSERT INTO park
	(name, location, establish_date, area, visitors, description)
VALUES
	('Test Park', 'Washington', '1900-03-25', 1, 100, 'Honestly it is like totally none of your business')

DECLARE @parkId int = @@IDENTITY

INSERT INTO campground
	(park_id, name, open_from_mm, open_to_mm, daily_fee)
VALUES
	(@parkId, 'Test Camp', 1, 12, 10)

DECLARE @campId int = @@identity

INSERT INTO site
	(campground_id, site_number, max_occupancy, accessible, max_rv_length, utilities)
VALUES
	(@campId, 1, 6, 0, 0, 1)

DECLARE @siteId int = @@IDENTITY

INSERT INTO reservation
	(site_id, name, from_date, to_date, create_date)
VALUES
	(@siteId, 'testRev', '2020-01-05', '2020-01-08', '2020-01-03')

DECLARE @reserveId int = @@IDENTITY


SELECT @reserveId AS newResId
