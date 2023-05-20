using AutoMapper;
using TaskVacancy.Dto;
using TaskVacancy.Models;

namespace TaskVacancy.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Dto to Model
            //CreateMap<UserCreatDto, User>();
            //CreateMap<UserUpdateDto, User>();


            //Model to Dto
            CreateMap<AccountTransactions, ReceiptReplenishmentViewDto>()
                .ForMember(x => x.FullName, y => y.MapFrom(c => c.Account.User.FirstName + " " + c.Account.User.LastName))
                .ForMember(x => x.Phone, y => y.MapFrom(c => c.Account.User.Phone));
        }
    }
}
