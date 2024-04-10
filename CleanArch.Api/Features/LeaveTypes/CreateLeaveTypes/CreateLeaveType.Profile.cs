//using CleanArch.Api.Contracts.LeaveTypes;
//using CleanArch.Api.Features.LeaveTypes.CreateLeaveTypes;
//using CleanArch.Domain.Entities;

//namespace CleanArch.Api.Features.LeaveTypes.CreateLeaveTypes;

//public static partial class CreateLeaveType
//{
//    public sealed partial class Profile : AutoMapper.Profile
//    {
//        public Profile()
//        {
//            CreateMap<CreateLeaveType.Command, LeaveType>()
//                .ForMember(dest => dest.Id, opt => opt.Ignore())
//                .ForMember(dest => dest.DateCreated, opt => opt.Ignore())
//                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
//                .ForMember(dest => dest.DateModified, opt => opt.Ignore())
//                .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore());

//            CreateMap<CreateLeaveTypeRequest, CreateLeaveType.Command>();
//        }
//    }
//}
