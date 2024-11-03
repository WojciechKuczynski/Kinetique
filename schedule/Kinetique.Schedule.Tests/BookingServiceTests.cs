using System.Xml.Serialization;
using Kinetique.Schedule.Models;
using Kinetique.Schedule.Repositories;
using Kinetique.Schedule.Requests;
using Kinetique.Schedule.Services;

namespace Kinetique.Schedule.Tests;

public class BookingServiceTests
{
    [Fact]
    public void SlotsAreBetweenRequestedAppointment_CanBook()
    {
        var now = DateTime.UtcNow;
        var repo = new InMemoryScheduleRepository();
        //Setup
        var service = new ScheduleBookingService(repo);

        now = now.AddHours(-1 * now.Hour).AddMinutes(-1 * now.Minute).AddSeconds(-1 * now.Second);

        var slots = GetSlots(now);
        
        var doctorSchedule = new DoctorSchedule()
            { DoctorId = 1, StartDate = now.AddDays(-3), EndDate = now.AddDays(3) };
        doctorSchedule.AddSlots(slots);
        repo.Add(doctorSchedule);
        
        var request = new BookTimeRequest()
            { DoctorId = 1, StartDate = now.AddHours(11).AddMinutes(30), EndDate = now.AddHours(12).AddMinutes(30) };
        
        // act
        var result = service.GetSlotsForRequestedTime(request).Result;

        // assert
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public void SlotsAreBetweenRequestedAppointment_WithBlockedSamePeriod_CannotBook()
    {
        var now = DateTime.UtcNow;
        var repo = new InMemoryScheduleRepository();
        
        //Setup
        var service = new ScheduleBookingService(repo);

        now = now.AddHours(-1 * now.Hour).AddMinutes(-1 * now.Minute).AddSeconds(-1 * now.Second);
        
        //Arrange
        var slots = GetSlots(now);
        var block = new ScheduleBlocker()
        {
            DoctorId = 1, StartDate = now.AddHours(11),EndDate = now.AddHours(11).AddMinutes(59)
        };

        var doctorSchedule = new DoctorSchedule()
            { DoctorId = 1, StartDate = now.AddDays(-3), EndDate = now.AddDays(3) };
        doctorSchedule.AddSlots(slots);
        doctorSchedule.AddBlocks(new [] {block});
        repo.Add(doctorSchedule);
        
        var request = new BookTimeRequest()
            { DoctorId = 1, StartDate = now.AddHours(11).AddMinutes(30), EndDate = now.AddHours(12).AddMinutes(30) };
        
        // act
        var result = service.GetSlotsForRequestedTime(request).Result;
        
        // assert
        Assert.Equal(1, result.Count);
    }

    [Fact]
    public void ThereAreNoSlotsForCurrentTime_CannotBook()
    {
        
    }
    
    [Fact]
    public void ThereIsNoScheduleForCurrentDate_CannotBook()
    {
        
    }
    
    
    private IEnumerable<DoctorScheduleSlot> GetSlots(DateTime now)
    {
        var slots = new List<DoctorScheduleSlot>
        {
            new DoctorScheduleSlot()
            {
                DayOfWeek = now.DayOfWeek, StartTime = TimeSpan.Parse("10:00:00"),
                EndTime = TimeSpan.Parse("10:59:00")
            },
            new DoctorScheduleSlot()
            {
                DayOfWeek = now.DayOfWeek, StartTime = TimeSpan.Parse("11:00:00"),
                EndTime = TimeSpan.Parse("11:59:00")
            },
            new DoctorScheduleSlot()
            {
                DayOfWeek = now.DayOfWeek, StartTime = TimeSpan.Parse("12:00:00"),
                EndTime = TimeSpan.Parse("12:59:00")
            },
        };
        return slots;
    }
}