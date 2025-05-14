SELECT * FROM Room;

SELECT * FROM Booking;

SELECT * FROM Guest;

SELECT * FROM Booking WHERE GuestId = 1;

SELECT * FROM Booking WHERE RoomId = 103;

SELECT * FROM Room WHERE IsAvailable = 1;

SELECT Room.RoomId, Room.ExtraBeds FROM Room;

SELECT Guest.GuestId, Guest.GuestFirstName, Guest.GuestLastName, COUNT(Booking.BookingId) AS NumberOfBookings
FROM Guest
LEFT JOIN Booking ON Guest.GuestId = Booking.GuestId
GROUP BY Guest.GuestId, Guest.GuestFirstName, Guest.GuestLastName;