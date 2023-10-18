using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using dbkonstruktion.Models;

namespace dbkonstruktion.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IConfiguration _configuration;

    public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public IActionResult Index()
    {
        UndanflyktModel um = new UndanflyktModel(_configuration);
        DataTable undanflyktdata = um.GetUndanflykter();
        ViewBag.undanflyktdata = undanflyktdata;
        return View();
    }

    public IActionResult SearchKampanj(int kampanjID)
    {
        KampanjModel km = new KampanjModel(_configuration);
        DataTable kampanjdata = km.GetKampanjer(kampanjID);
        ViewBag.kampanjdata = kampanjdata;
        ViewBag.kampanjID = kampanjID;

        return View();
    }

    public IActionResult ArkiveraKampanj(int kampanjIDAttArkivera)
    {
        KampanjModel km = new KampanjModel(_configuration);
        km.ArkiveraKampanj(kampanjIDAttArkivera);

        return RedirectToAction("Index");
    }

    public IActionResult SkapaKampanj(int nyttKampanjID,string nyttSlutdatum, string nyKommentar,  string nyUndanflykt)
    {
        KampanjModel km = new KampanjModel(_configuration);
        km.CreateKampanj(nyttKampanjID, nyttSlutdatum, nyKommentar, nyUndanflykt);

        return RedirectToAction("Index");
    }

    public IActionResult Undanflykt()
    {
        UndanflyktModel um = new UndanflyktModel(_configuration);
        DataTable undanflyktdata = um.GetUndanflykter();
        ViewBag.undanflyktdata = undanflyktdata;
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

