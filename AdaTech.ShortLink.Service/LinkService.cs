using AdaTech.ShortLink.DataLibrary;
using AdaTech.ShortLink.Entities;
using AdaTech.ShortLink.Service.Exceptions;

namespace AdaTech.ShortLink.Service
{
    public class LinkService
    {
        private readonly ApplicationContext _context;

        public LinkService(ApplicationContext context)
        {
            _context = context;
        }

        public string ShortenLink(string originalUrl)
        {
            string shortUrl;
            do
            {
                shortUrl = GenerateShortUrl(7);
            } while (_context.Links.Any(l => l.ShortUrl == shortUrl));

            var link = new Link
            {
                OriginalUrl = originalUrl,
                ShortUrl = shortUrl,
                Expiration = DateTime.Now.Date.AddDays(1)
            };

            _context.Links.Add(link);
            _context.SaveChanges();

            return shortUrl;
        }

        private string GenerateShortUrl(int length)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public string Redirect(string shortUrl)
        {
            var link = _context.Links.FirstOrDefault(l => l.ShortUrl == shortUrl);

            if (link == null)
            {
                throw new NotFoundException("Link not found"); 
            }

            if (DateTime.Now > link.Expiration)
            {
                throw new ExpiredDateException("Link expired");
            }

            return link.OriginalUrl;
        }
    }
}
