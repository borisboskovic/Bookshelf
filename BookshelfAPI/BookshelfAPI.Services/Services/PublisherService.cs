using BookshelfAPI.Data;
using BookshelfAPI.Services.DTOs.Publisher;
using BookshelfAPI.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookshelfAPI.Services.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly BookshelfDbContext _context;
        private readonly IConfiguration _configuration;

        public PublisherService(BookshelfDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public List<PublisherDto> SearchPublishers(string searchString)
        {
            var keywords = searchString.Split(" ");
            return _context.Publisher
            .AsEnumerable()
                .Where(e => keywords.All(keyword =>
                    e.Name.ToLower().Contains(keyword.ToLower())
                ))
                .Take(5)
                .Select(e => new PublisherDto
                {
                    Id = e.Id,
                    ImageUrl = $"{_configuration["Azure:BlobStorageUrl"]}/{e.ImageUrl}",
                    Name = e.Name
                })
                .ToList();
        }
    }
}
