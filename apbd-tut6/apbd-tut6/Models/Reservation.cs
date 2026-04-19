namespace apbd_tut6.Models;

public class Reservation
{
    public int id {get; set;}
    public int roomId {get; set;}
    public string organizerName {get; set;}
    public string topic {get; set;}
    public DateTime date {get; set;}
    public TimeSpan startTime {get; set;}
    public TimeSpan endTime {get; set;}
    public string status {get; set;}
    
}