using AutoMapper;
using InsureTrust.API.DTOs.Auth;
using InsureTrust.API.DTOs.Claim;
using InsureTrust.API.DTOs.Notification;
using InsureTrust.API.DTOs.Payment;
using InsureTrust.API.DTOs.Policy;
using InsureTrust.API.DTOs.Support;
using InsureTrust.API.Models;
using System.Text.Json;

namespace InsureTrust.API.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // ── User / Auth ──────────────────────────────────────────────
        CreateMap<User, UserDto>();

        // ── PolicyType ───────────────────────────────────────────────
        CreateMap<PolicyType, PolicyTypeDto>();
        CreateMap<CreatePolicyTypeDto, PolicyType>()
            .ForMember(d => d.IsActive, o => o.MapFrom(_ => true))
            .ForMember(d => d.CreatedAt, o => o.MapFrom(_ => DateTime.UtcNow));

        // ── UserPolicy → PolicyDto (with calculated fields) ──────────
        CreateMap<UserPolicy, PolicyDto>()
            .ForMember(d => d.PolicyTypeName, o => o.MapFrom(s => s.PolicyType != null ? s.PolicyType.Name : string.Empty))
            .ForMember(d => d.Category,       o => o.MapFrom(s => s.PolicyType != null ? s.PolicyType.Category : string.Empty))
            .ForMember(d => d.Icon,           o => o.MapFrom(s => s.PolicyType != null ? s.PolicyType.Icon : string.Empty))
            .ForMember(d => d.UserName,       o => o.MapFrom(s => s.User != null ? s.User.Name : string.Empty))
            .ForMember(d => d.UserEmail,      o => o.MapFrom(s => s.User != null ? s.User.Email : string.Empty))
            // Calculated: days remaining until expiry
            .ForMember(d => d.DaysLeft,       o => o.MapFrom(s => Math.Max(0, (s.ExpiryDate - DateTime.UtcNow).Days)));

        // ── Claim ─────────────────────────────────────────────────────
        CreateMap<Claim, ClaimDto>()
            .ForMember(d => d.PolicyNumber,    o => o.MapFrom(s => s.UserPolicy != null ? s.UserPolicy.PolicyNumber : string.Empty))
            .ForMember(d => d.PolicyTypeName,  o => o.MapFrom(s => s.UserPolicy != null && s.UserPolicy.PolicyType != null ? s.UserPolicy.PolicyType.Name : string.Empty))
            .ForMember(d => d.UserName,        o => o.MapFrom(s => s.UserPolicy != null && s.UserPolicy.User != null ? s.UserPolicy.User.Name : string.Empty))
            // Use IValueConverter to avoid CS0854 (optional args in expression trees)
            .ForMember(d => d.DocumentsCount,  o => o.ConvertUsing(new DocumentsCountConverter(), s => s));

        // ── SupportQuery ──────────────────────────────────────────────
        CreateMap<SupportQuery, SupportQueryDto>()
            .ForMember(d => d.UserName,  o => o.MapFrom(s => s.User != null ? s.User.Name : string.Empty))
            .ForMember(d => d.UserEmail, o => o.MapFrom(s => s.User != null ? s.User.Email : string.Empty));

        // ── Notification ──────────────────────────────────────────────
        CreateMap<Notification, NotificationDto>();

        // ── Payment ───────────────────────────────────────────────────
        CreateMap<Payment, PaymentDto>()
            .ForMember(d => d.PolicyNumber, o => o.MapFrom(s => s.UserPolicy != null ? s.UserPolicy.PolicyNumber : string.Empty));
    }
}

/// <summary>
/// IValueConverter that counts the documents in the JSON array stored on <see cref="Claim"/>.
/// A dedicated converter is required to avoid the CS0854 compile error
/// ("expression tree may not contain a call that uses optional arguments").
/// </summary>
public class DocumentsCountConverter : IValueConverter<Claim, int>
{
    public int Convert(Claim sourceMember, ResolutionContext context)
    {
        try
        {
            var list = JsonSerializer.Deserialize<List<string>>(sourceMember.DocumentsSubmitted ?? "[]");
            return list?.Count ?? 0;
        }
        catch
        {
            return 0;
        }
    }
}
