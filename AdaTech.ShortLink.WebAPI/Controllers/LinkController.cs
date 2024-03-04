using AdaTech.ShortLink.DataLibrary;
using AdaTech.ShortLink.Service;
using AdaTech.ShortLink.Service.AttributeTags;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[SwaggerDisplayName("Link Shortener")]
public class LinksController : ControllerBase
{
    private readonly LinkService _linkService;
    private readonly ApplicationContext _context;

    public LinksController(LinkService linkService, ApplicationContext applicationContext)
    {
        _linkService = linkService;
        _context = applicationContext;
    }

    /// <summary>
    /// Gera um link curto para uma url existente
    /// </summary>
    /// <param name="originalUrl"></param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult<string> ShortenLink([FromBody] string originalUrl)
    {
        var shortUrl = _linkService.ShortenLink(originalUrl);
        return Ok(shortUrl);
    }

    /// <summary>
    /// Redireciona para a url original a partir do link curto
    /// </summary>
    /// <param name="shortUrl"></param>
    /// <returns></returns>
    [HttpGet("{shortUrl}")]
    public ActionResult RedirectShortLink(string shortUrl)
    {
        var link = _linkService.Redirect(shortUrl);

        return Redirect(link);
    }
}
