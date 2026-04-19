using apbd_tut6.Models;

namespace apbd_tut6;

public class DataStore
{
    public static List<Room> Rooms = new List<Room>()
    {
        new Room
        {
            id = 1, name = "Alpha", buildingCode = "A", floor = 1, capacity = 10, hasProjector = true, isActive = true
        },
        new Room
        {
            id = 2, name = "Beta", buildingCode = "A", floor = 2, capacity = 20, hasProjector = false, isActive = true
        },
        new Room
        {
            id = 3, name = "Gamma", buildingCode = "B", floor = 3, capacity = 15, hasProjector = true, isActive = true
        },
        new Room
        {
            id = 4, name = "Delta", buildingCode = "C", floor = 1, capacity = 8, hasProjector = false, isActive = false
        }
    };

    public static List<Reservation> Reservations = new List<Reservation>()
    {
        new Reservation
        {
            id = 1,
            roomId = 1,
            organizerName = "John Doe",
            topic = "Project Kickoff",
            date = new DateTime(2026, 4, 15),
            startTime = new TimeSpan(9, 0, 0),
            endTime = new TimeSpan(10, 0, 0),
            status = "Planned"
        },
        new Reservation
        {
            id = 2,
            roomId = 2,
            organizerName = "Jane Smith",
            topic = "Marketing Meeting",
            date = new DateTime(2026, 4, 16),
            startTime = new TimeSpan(11, 0, 0),
            endTime = new TimeSpan(12, 30, 0),
            status = "Confirmed"
        },
        new Reservation
        {
            id = 3,
            roomId = 3,
            organizerName = "Mike Brown",
            topic = "Tech Review",
            date = new DateTime(2026, 4, 17),
            startTime = new TimeSpan(14, 0, 0),
            endTime = new TimeSpan(15, 0, 0),
            status = "Cancelled"
        },
        new Reservation
        {
            id = 4,
            roomId = 1,
            organizerName = "Anna White",
            topic = "Team Sync",
            date = new DateTime(2026, 4, 18),
            startTime = new TimeSpan(10, 0, 0),
            endTime = new TimeSpan(11, 0, 0),
            status = "Confirmed"
        }
    };



}