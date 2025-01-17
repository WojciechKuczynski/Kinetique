using Kinetique.Shared.Model;

namespace Kinetique.Appointment.Model.Exceptions;

public class CycleAlreadyHavingReferralException() : KinetiqueException("Active cycle for patient is already having referral!");
