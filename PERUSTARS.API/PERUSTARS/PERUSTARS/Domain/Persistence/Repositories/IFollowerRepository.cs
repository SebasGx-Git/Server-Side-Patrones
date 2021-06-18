using PERUSTARS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PERUSTARS.Domain.Persistence.Repositories
{
    public interface IFollowerRepository
    {
        Task<IEnumerable<Follower>> ListAsync();
        Task<IEnumerable<Follower>> ListByHobbyistIdAsync(long HobbyistId);
        Task<IEnumerable<Follower>> ListByArtistIdAsync(long ArtistId);
        Task<Follower> FindByHobbyistIdAndArtistId(long HobbyistId, long ArtistId);
        Task AddAsync(Follower follower);
        void Remove(Follower follower);
        Task AssignFollower(long HobbyistId, long ArtistId);
        Task UnassignFollower(long HobbyistId, long ArtistId);

        Task<int> CountFollower(long ArtistId);

    }
}
