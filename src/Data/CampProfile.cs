using AutoMapper;
using CoreCodeCamp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCodeCamp.Data
{
    public class CampProfile : Profile
    {
        public CampProfile()
        {
            this.CreateMap<Camp, CampModel>()
                .ForMember(o => o.Venue, m => m.MapFrom(c => c.Location.VenueName))
                .ForMember(o => o.Address1, m => m.MapFrom(c => c.Location.Address1))
                .ForMember(o => o.Address2, m => m.MapFrom(c => c.Location.Address2))
                .ForMember(o => o.Address3, m => m.MapFrom(c => c.Location.Address3))
                .ForMember(o => o.CityTown, m => m.MapFrom(c => c.Location.CityTown))
                .ForMember(o => o.StateProvince, m => m.MapFrom(c => c.Location.StateProvince))
                .ForMember(o => o.PostalCode, m => m.MapFrom(c => c.Location.PostalCode))
                .ForMember(o => o.Country, m => m.MapFrom(c => c.Location.Country));
                
        }
    }
}
