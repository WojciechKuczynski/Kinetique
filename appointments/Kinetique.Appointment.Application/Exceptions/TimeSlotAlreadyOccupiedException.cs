namespace Kinetique.Appointment.Application.Exceptions;

public class TimeSlotAlreadyOccupiedException(DateTime date, TimeSpan time) : Exception(
    $"There is already booked Appointment for Patient or Doctor in given Time slot [{date.ToString()} - {date.Add(time)}]");