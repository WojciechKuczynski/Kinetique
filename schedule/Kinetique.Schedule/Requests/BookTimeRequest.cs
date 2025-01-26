namespace Kinetique.Schedule.Requests;

[Serializable]
public class BookTimeRequest
{
    public string DoctorCode { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}