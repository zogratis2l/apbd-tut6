using System.ComponentModel.DataAnnotations;

namespace apbd_tut6.DTOs;

public class CreateRoomDto
{
    [Required]
    public string name { get; set; }
    [Required]
    public string buildingCode { get; set; }
    public int floor  { get; set; }
    [Range(0, int.MaxValue)]
    public int capacity { get; set; }
    public bool hasProjector { get; set; }
    public bool isActive { get; set; }
}