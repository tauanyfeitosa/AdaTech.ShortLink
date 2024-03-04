using AdaTech.ShortLink.DataLibrary;
using Microsoft.EntityFrameworkCore;
using AdaTech.ShortLink.Service;
using AdaTech.ShortLink.Service.Exceptions;
using FluentAssertions;

namespace AdaTech.ShortLink.ServicesTests
{
    public class LinkServiceTests
    {
        private readonly LinkService _service;
        private readonly ApplicationContext _context;

        public LinkServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "LinkServiceTests")
                .Options;
            _context = new ApplicationContext(options);
            _service = new LinkService(_context);
        }

        [Fact]
        public void ShortenLink_WhenCalled_GeneratesUniqueShortLink()
        {
            // Arrange
            var originalUrl = "http://example.com";

            // Act
            var shortUrl = _service.ShortenLink(originalUrl);

            // Assert
            // Verifica se a URL curta não é nula e tem o comprimento esperado
            Assert.NotNull(shortUrl);
            Assert.Equal(7, shortUrl.Length);

            // Verifica se o link foi adicionado ao banco de dados em memória
            var linkInDatabase = _context.Links.FirstOrDefault(l => l.ShortUrl == shortUrl);
            Assert.NotNull(linkInDatabase);
       
        }

        [Fact]
        public void Redirect_WhenShortLinkNotFound_ThrowsNotFoundException()
        {
            // Arrange
            _context.Links.RemoveRange(_context.Links);
            _context.SaveChanges();

            var shortUrl = "nonexistent_short_url";

            // Act & Assert
            FluentActions.Invoking(() => _service.Redirect(shortUrl)).Should().Throw<NotFoundException>().WithMessage("Link not found");
        }

        [Fact]
        public void Redirect_WhenShortLinkExpired_ThrowsExpiredDateException()
        {
            // Arrange
            var shortUrl = _service.ShortenLink("http://example.com");
            var link = _context.Links.FirstOrDefault(l => l.ShortUrl == shortUrl);
            link.Expiration = DateTime.Now.AddDays(-1);

            // Act
            Action act = () => _service.Redirect(shortUrl);

            // Assert
            act.Should().Throw<ExpiredDateException>().WithMessage("Link expired");
        }


        [Fact]
        public void Redirect_WhenShortLinkIsValid_ReturnsOriginalUrl()
        {
            // Arrange
            var shortUrl = _service.ShortenLink("http://example.com");

            // Act
            var result = _service.Redirect(shortUrl);

            // Assert
            result.Should().Be("http://example.com");
        }

        [Fact]
        public void ShortenLink_WhenOriginalUrlIsNullOrEmpty_ThrowsArgumentNullException()
        {
            // Arrange
            string originalUrl = null;

            // Act & Assert
            FluentActions.Invoking(() => _service.ShortenLink(originalUrl)).Should().Throw<DbUpdateException>();
        }
    }
}
