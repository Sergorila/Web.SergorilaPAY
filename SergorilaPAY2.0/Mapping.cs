using Entities;
using AutoMapper;
using SergorilaPAY2._0.Views;

namespace SergorilaPAY2._0;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<UserView, User>();
    }
    
}