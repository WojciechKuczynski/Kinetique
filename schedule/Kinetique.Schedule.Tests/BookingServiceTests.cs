using System.Xml.Serialization;
using Kinetique.Schedule.Models;
using Kinetique.Schedule.Repositories;
using Kinetique.Schedule.Requests;
using Kinetique.Schedule.Services;
using Kinetique.Shared;
using Kinetique.Shared.Model.Abstractions;

namespace Kinetique.Schedule.Tests;

public class BookingServiceTests
{
    private readonly IClock _clock = new UtcClock();

    [Fact]
    public void SlotsAreBetweenRequestedAppointment_CanBook()
    {
        //Setup
        var now = _clock.GetTodayMidnight();
        var repo = new InMemoryScheduleRepository();
        var service = new ScheduleBookingService(repo);

        // arrange
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
        Assert.True(result.Any(x => now.Add(x.StartTime) <= request.StartDate) && result.Any(x => now.Add(x.EndTime) >= request.EndDate));
    }

    [Fact]
    public void SlotsAreBetweenRequestedAppointment_WithBlockedSamePeriod_CannotBook()
    {
        //Setup
        var now = _clock.GetTodayMidnight();
        var repo = new InMemoryScheduleRepository();
        var service = new ScheduleBookingService(repo);
        
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
        Assert.False(result.Any(x => now.Add(x.StartTime) <= request.StartDate) && result.Any(x => now.Add(x.EndTime) >= request.EndDate));
    }

    [Fact]
    public void ThereAreNoSlotsForCurrentTime_CannotBook()
    {
        //Setup
        var now = _clock.GetTodayMidnight();
        var repo = new InMemoryScheduleRepository();
        var service = new ScheduleBookingService(repo);

        var doctorSchedule = new DoctorSchedule()
            { DoctorId = 1, StartDate = now.AddDays(-3), EndDate = now.AddDays(3) };
        repo.Add(doctorSchedule);
        
        var request = new BookTimeRequest()
            { DoctorId = 1, StartDate = now.AddHours(11).AddMinutes(30), EndDate = now.AddHours(12).AddMinutes(30) };


        var result = service.GetSlotsForRequestedTime(request).Result;
        
        //assert
        Assert.False(result.Any(x => now.Add(x.StartTime) <= request.StartDate) && result.Any(x => now.Add(x.EndTime) >= request.EndDate));
    }
    
    [Fact]
    public void ThereIsNoScheduleForCurrentDate_CannotBook()
    {
        //Setup
        var now = _clock.GetTodayMidnight();
        var repo = new InMemoryScheduleRepository();
        var service = new ScheduleBookingService(repo);

        var request = new BookTimeRequest()
            { DoctorId = 1, StartDate = now.AddHours(11).AddMinutes(30), EndDate = now.AddHours(12).AddMinutes(30) };


        var result = service.GetSlotsForRequestedTime(request).Result;
        
        //assert
        Assert.False(result.Any(x => now.Add(x.StartTime) <= request.StartDate) && result.Any(x => now.Add(x.EndTime) >= request.EndDate));
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
