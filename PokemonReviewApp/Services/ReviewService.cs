using AutoMapper;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using PokemonReviewApp.Services.Interfaces;

namespace PokemonReviewApp.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IReviewerRepository _reviewerRepository;
        private readonly IMapper _mapper;

        public ReviewService(
            IReviewRepository reviewRepository,
            IPokemonRepository pokemonRepository,
            IReviewerRepository reviewerRepository,
            IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _pokemonRepository = pokemonRepository;
            _reviewerRepository = reviewerRepository;
            _mapper = mapper;
        }

        public ICollection<ReviewDto> GetReviews()
        {
            return _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviews());
        }

        public ReviewDto? GetReview(int reviewId)
        {
            if (!_reviewRepository.ReviewExists(reviewId)) return null;
            return _mapper.Map<ReviewDto>(_reviewRepository.GetReview(reviewId));
        }

        public ICollection<ReviewDto> GetReviewsOfAPokemon(int pokeId)
        {
            return _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviewsOfAPokemon(pokeId));
        }

        public bool ReviewExists(int reviewId) => _reviewRepository.ReviewExists(reviewId);

        public bool CreateReview(int reviewerId, int pokeId, ReviewDto reviewCreate)
        {
            var reviewMap = _mapper.Map<Review>(reviewCreate);
            reviewMap.Pokemon = _pokemonRepository.GetPokemon(pokeId);
            reviewMap.Reviewer = _reviewerRepository.GetReviewer(reviewerId);
            return _reviewRepository.CreateReview(reviewMap);
        }

        public bool UpdateReview(ReviewDto reviewUpdate)
        {
            var reviewMap = _mapper.Map<Review>(reviewUpdate);
            return _reviewRepository.UpdateReview(reviewMap);
        }

        public bool DeleteReview(int reviewId)
        {
            if (!_reviewRepository.ReviewExists(reviewId)) return false;
            var reviewToDelete = _reviewRepository.GetReview(reviewId);
            return _reviewRepository.DeleteReview(reviewToDelete);
        }

        public bool DeleteReviews(List<ReviewDto> reviews)
        {
            var reviewMaps = _mapper.Map<List<Review>>(reviews);
            return _reviewRepository.DeleteReviews(reviewMaps);
        }
    }
}