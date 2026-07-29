using PokemonReviewApp.Dto;

namespace PokemonReviewApp.Services.Interfaces
{
    public interface IReviewerService
    {
        ICollection<ReviewerDto> GetReviewers();
        ReviewerDto? GetReviewer(int reviewerId);
        ICollection<ReviewDto> GetReviewsByReviewer(int reviewerId);
        bool ReviewerExists(int reviewerId);
        bool CreateReviewer(ReviewerDto reviewerCreate);
        bool UpdateReviewer(ReviewerDto reviewerUpdate);
        bool DeleteReviewer(int reviewerId);
    }
}