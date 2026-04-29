using System;
using System.ComponentModel.DataAnnotations;

namespace GamesCrudApi.Models
{
    public class Game
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = "";

        [Required]
        public string Genre { get; set; } = "";

        public DateTime ReleaseDate { get; set; }

        public GameCondition Condition { get; set; } = GameCondition.New;

        [Range(0, 1000000)]
        public decimal Price { get; set; }

        public PurchaseOption PurchaseOption { get; set; } = PurchaseOption.Buy;

        public string? ContentDescription { get; set; }

        public bool HasInteractiveElements { get; set; }

        [Range(0, 10)]
        public decimal UserRating { get; set; }

        public string? UserReview { get; set; }

        public string? Critics { get; set; }

        public string? Features { get; set; }

        public int OnlinePlayers { get; set; }

        public int OfflinePlayers { get; set; }

        public string? Publisher { get; set; }

        public string? AgeRating { get; set; }

        public string? SpecsAndRequirements { get; set; }

        public string? CustomersFrequentlyRented { get; set; }

        public bool IsRented { get; set; }

        public DateTime? LastRentedAtUtc { get; set; }

        public string? Description { get; set; }
    }
}
