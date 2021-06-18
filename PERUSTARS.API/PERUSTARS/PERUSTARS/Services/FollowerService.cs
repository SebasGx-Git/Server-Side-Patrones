using PERUSTARS.Domain.Models;
using PERUSTARS.Domain.Persistence.Repositories;
using PERUSTARS.Domain.Services;
using PERUSTARS.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PERUSTARS.Services
{
    public class FollowerService :IFollowerService
    {
        private readonly IFollowerRepository _followerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public FollowerService(IFollowerRepository followerRepository, IUnitOfWork unitOfWork)
        {
            _followerRepository = followerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<FollowerResponse> AssignFollowerAsync(long hobbyistId, long artistId)
        {
            try {
                await _followerRepository.AssignFollower(hobbyistId,artistId);
                await _unitOfWork.CompleteAsync();
                Follower follower = await _followerRepository.FindByHobbyistIdAndArtistId(hobbyistId, artistId);
                return new FollowerResponse(follower);
            }
            catch (Exception ex) { 
            return new FollowerResponse($"An error ocurred while assign Artist to Hobbyist: {ex.Message}");
            }
        }

        public async Task<int> CountFollowers(long artistId)
        {
            return await _followerRepository.CountFollower(artistId);
        }

        public async Task<IEnumerable<Follower>> ListAsync()
        {
            return await _followerRepository.ListAsync();
        }

        public async Task<IEnumerable<Follower>> ListByArtistIdAsync(long artistId)
        {
            return await _followerRepository.ListByArtistIdAsync(artistId);
        }

        public async Task<IEnumerable<Follower>> ListByHobbyistIdAsync(long hobbyistId)
        {
            return await _followerRepository.ListByHobbyistIdAsync(hobbyistId);
        }

        public async Task<FollowerResponse> UnassignFollowerAsync(long hobbyistId, long artistId)
        {
            try
            {
                Follower follower = await _followerRepository.FindByHobbyistIdAndArtistId(hobbyistId, artistId);
                if (follower == null) throw new Exception();
                await _followerRepository.UnassignFollower(hobbyistId, artistId);
                await _unitOfWork.CompleteAsync();
                return new FollowerResponse(follower);
            }
            catch (Exception ex)
            {
                return new FollowerResponse($"An error ocurred while unassign Artist to Hobbyist: {ex.Message}");
            }
        }
    }
}
