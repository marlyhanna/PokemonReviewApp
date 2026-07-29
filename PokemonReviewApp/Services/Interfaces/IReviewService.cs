using PokemonReviewApp.Dto;

namespace PokemonReviewApp.Services.Interfaces
{
    public interface IReviewService
    {
        ICollection<ReviewDto> GetReviews();
        ReviewDto? GetReview(int reviewId);
        ICollection<ReviewDto> GetReviewsOfAPokemon(int pokeId);
        bool ReviewExists(int reviewId);
        bool CreateReview(int reviewerId, int pokeId, ReviewDto reviewCreate);
        bool UpdateReview(ReviewDto reviewUpdate);
        bool DeleteReview(int reviewId);
        bool DeleteReviews(List<ReviewDto> reviews);
    }
}