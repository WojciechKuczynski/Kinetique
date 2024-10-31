namespace Kinetique.Schedule.Models.ValueObjects;

public class Date
{
    public DateTime Value { get; }
    
    public Date(DateTime value)
    {
        Value = value.AddHours(value.Hour * -1).AddMinutes(value.Minute * -1).AddSeconds(value.Second * -1);
    }
    
    public static implicit operator DateTime(Date date) => date.Value;
    public static implicit operator Date(DateTime date) => new(date);
}