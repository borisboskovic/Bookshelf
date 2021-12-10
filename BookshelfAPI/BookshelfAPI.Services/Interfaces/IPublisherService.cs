using BookshelfAPI.Services.DTOs.Publisher;
using System.Collections.Generic;

namespace BookshelfAPI.Services.Interfaces
{
    public interface IPublisherService
    {
        List<PublisherDto> SearchPublishers(string searchString);
    }
}
