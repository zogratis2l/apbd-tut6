namespace apbd_tut6.Models;

public class Room
{
   public int id { get; set; }
    public string name { get; set; }
    public string buildingCode  { get; set; }
    public int floor {get; set;}
    public int capacity {get; set;}
    public bool hasProjector {get; set;}
    public bool isActive  {get; set;}

    
}