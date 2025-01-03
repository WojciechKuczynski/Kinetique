using Kinetique.Shared.Model;

namespace Kinetique.Nfz.Model;

public class Referral : BaseModel
{
    /// <summary>
    /// Taken from Patient, will be already validated
    /// </summary>
    public string PatientPesel { get; init; }
    /// <summary>
    /// no need validation
    /// </summary>
    public string ReferralCode { get; init; }
    public DateTime RegistrationTime { get; init; }
}