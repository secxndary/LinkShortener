using LinkShortener.Service.Contracts;
using LinkShortener.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
namespace LinkShortener.Controllers;

public class LinkController : Controller
{
    private readonly IServiceManager _service;
    public LinkController(IServiceManager service) => _service = service;


    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var links = await _service.LinkService.GetAllLinksAsync();
        return View(links);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(LinkDto link)
    {
        if (!Uri.IsWellFormedUriString(link.UrlLong, UriKind.Absolute))
            ModelState.AddModelError("UrlLong", "Please, enter correct URL.");

        if (ModelState.IsValid)
        {
            await _service.LinkService.CreateLinkAsync(link);
            return RedirectToAction("Index");
        }
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var link = await _service.LinkService.GetLinkAsync(id);
        if (link is null)
            return NotFound();
        return View(link);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Guid id, LinkDto link)
    {
        if (!Uri.IsWellFormedUriString(link.UrlLong, UriKind.Absolute))
            ModelState.AddModelError("UrlLong", "Please, enter correct URL.");

        if (ModelState.IsValid)
        {
            await _service.LinkService.UpdateLinkAsync(id, link);
            return RedirectToAction("Index");
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.LinkService.DeleteLinkAsync(id);
        return RedirectToAction("Index");
    }

    [HttpGet("{urlShort}")]
    public async Task<IActionResult> RedirectToLongUrl(string urlShort)
    {
        var link = await _service.LinkService.GetLinkByUrlShortAsync(urlShort);

        if (link is null)
            return NotFound();

        link.NumberOfClicks++;
        await _service.LinkService.UpdateLinkAsync(link.Id, link);

        return Redirect(link.UrlLong!);
    }
}