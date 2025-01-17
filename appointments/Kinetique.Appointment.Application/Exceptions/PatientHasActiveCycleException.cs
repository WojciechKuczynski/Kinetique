using Kinetique.Shared.Model;

namespace Kinetique.Appointment.Application.Exceptions;

public class PatientHasActiveCycleException() : KinetiqueException("There is already active cycle for this patient");
