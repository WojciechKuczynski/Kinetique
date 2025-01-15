using Kinetique.Appointment.Application.Dtos;

namespace Kinetique.Appointment.Application.Mappers;

public static partial class Mapper
{
    public static ReferralDto MapToDto(this Model.Referral referral)
        => new ReferralDto()
        {
            Id = referral.Id,
            Uid = referral.Uid,
            Pesel = referral.Pesel,
            Code = referral.Code,
            CreatedOn = referral.CreatedOn
        };

    public static Model.Referral MapToEntity(this ReferralDto referralDto)
        => new Model.Referral()
        {
            Id = referralDto.Id,
            Uid = referralDto.Uid,
            Pesel = referralDto.Pesel,
            Code = referralDto.Code,
            CreatedOn = referralDto.CreatedOn
        };
}