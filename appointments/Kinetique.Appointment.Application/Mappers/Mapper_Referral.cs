using Kinetique.Appointment.Application.Dtos;

namespace Kinetique.Appointment.Application.Mappers;

public static partial class Mapper
{
    public static ReferralDto MapToDto(this Model.Referral referral)
        => new ReferralDto()
        {
            Id = referral.Id,
        };

    public static Model.Referral MapToEntity(this ReferralDto referralDto)
        => new Model.Referral()
        {
            Id = referralDto.Id,
        };
}