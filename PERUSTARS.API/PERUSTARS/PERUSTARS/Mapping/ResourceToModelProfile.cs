using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PERUSTARS.Domain.Models;
using PERUSTARS.Resources; 

namespace PERUSTARS.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile() {

            CreateMap<SaveEventAssistanceResource, EventAssistance>();
            CreateMap<SavePersonResource, Person>();
            CreateMap<SaveArtworkResource, Artwork>();
            CreateMap<SaveEventResource, Event>();
            CreateMap<SaveArtistResource, Artist>();
            CreateMap<SaveClaimTicketResource, ClaimTicket>();
            CreateMap<SaveHobbyistResource, Hobbyist>();
        }
    }
}
