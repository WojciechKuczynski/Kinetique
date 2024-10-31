namespace Kinetique.Schedule.Requests;

[Serializable]
public class BookTimeRequest
{
    public long DoctorId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}