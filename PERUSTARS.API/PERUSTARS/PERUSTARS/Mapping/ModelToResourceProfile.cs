using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PERUSTARS.Domain.Models;
using PERUSTARS.Resources;

namespace PERUSTARS.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile() {
            CreateMap<Artist, ArtistResource>();
            CreateMap<ClaimTicket, ClaimTicketResource>();
            CreateMap<Person, PersonResource>();
            CreateMap<Event, EventResource>();
            CreateMap<EventAssistance, EventAssistanceResource>();
            CreateMap<Artwork, ArtworkResource>();
            CreateMap<Specialty, SpecialtyResource>();
            CreateMap<Hobbyist, HobbyistResource>();
        }

    }
}
