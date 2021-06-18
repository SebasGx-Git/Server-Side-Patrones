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
            CreateMap<Hobbyist, HobbyistResource>();
            CreateMap<Person, PersonResource>();
            CreateMap<Event, EventResource>();
            CreateMap<ClaimTicket, ClaimTicketResource>();
            CreateMap<EventAssistance, EventAssistanceResource>();
            CreateMap<Artwork, ArtworkResource>();
            CreateMap<Specialty, SpecialtyResource>();

        }

    }
}
