using ECommerce_App.Models;
using System.ComponentModel.DataAnnotations;

namespace ECommerce_App.DTOs
{
    public class CustomerBasketDto
    {

        [Required]
        public string Id { get; set; }

        public List<BasketItemDto> Items { get; set; }

    }
}
