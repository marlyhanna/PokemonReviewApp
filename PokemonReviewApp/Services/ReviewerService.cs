using AutoMapper;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using PokemonReviewApp.Services.Interfaces;

namespace PokemonReviewApp.Services
{
    public class ReviewerService : IReviewerService
    {
        private readonly IReviewerRepository _reviewerRepository;
        private readonly IMapper _mapper;

        public ReviewerService(IReviewerRepository reviewerRepository, IMapper mapper)
        {
            _reviewerRepository = reviewerRepository;
            _mapper = mapper;
        }

        public ICollection<ReviewerDto> GetReviewers()
        {
            return _mapper.Map<List<ReviewerDto>>(_reviewerRepository.GetReviewers());
        }

        public ReviewerDto? GetReviewer(int reviewerId)
        {
            if (!_reviewerRepository.ReviewerExists(reviewerId)) return null;
            return _mapper.Map<ReviewerDto>(_reviewerRepository.GetReviewer(reviewerId));
        }

        public ICollection<ReviewDto> GetReviewsByReviewer(int reviewerId)
        {
            return _mapper.Map<List<ReviewDto>>(_reviewerRepository.GetReviewsByReviewer(reviewerId));
        }

        public bool ReviewerExists(int reviewerId) => _reviewerRepository.ReviewerExists(reviewerId);

        public bool CreateReviewer(ReviewerDto reviewerCreate)
        {
            var reviewerMap = _mapper.Map<Reviewer>(reviewerCreate);
            return _reviewerRepository.CreateReviewer(reviewerMap);
        }

        public bool UpdateReviewer(ReviewerDto reviewerUpdate)
        {
            var reviewerMap = _mapper.Map<Reviewer>(reviewerUpdate);
            return _reviewerRepository.UpdateReviewer(reviewerMap);
        }

        public bool DeleteReviewer(int reviewerId)
        {
            if (!_reviewerRepository.ReviewerExists(reviewerId)) return false;
            var reviewerToDelete = _reviewerRepository.GetReviewer(reviewerId);
            return _reviewerRepository.DeleteReviewer(reviewerToDelete);
        }
    }
}