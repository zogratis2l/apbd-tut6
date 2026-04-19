using System.ComponentModel.DataAnnotations;

namespace apbd_tut6.DTOs;

public class CreateReservationDto
{
    public int roomId { get; set; }
    [Required]
    public string organizerName { get; set; }
    [Required]
    public string topic { get; set; }
    public DateTime date { get; set; }
    public TimeSpan startTime { get; set; }
    public TimeSpan endTime { get; set; }
    public string status { get; set; }
}