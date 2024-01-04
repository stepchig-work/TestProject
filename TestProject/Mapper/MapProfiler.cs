using AutoMapper;
using TestProject.Models;
using TestProject.ViewModels;

namespace TestProject.Mapper;

public class MapProfiler : Profile
{
	public MapProfiler()
	{
		CreateMap<User, UserViewModel>();
		CreateMap<UserViewModel, User>().ForMember(x => x.Id, opt => opt.Ignore());
	}
}
