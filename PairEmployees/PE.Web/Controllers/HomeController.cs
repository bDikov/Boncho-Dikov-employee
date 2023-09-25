using InfrastructureOrchestrator.Ochestrate.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PE.BusinessLogic.Interfaces;

public class HomeController : Controller
{
    private readonly IProcessFileData processFile;
    private readonly IPairingService pairingService;

    public HomeController(IProcessFileData processFile, IPairingService pairingService)
    {
        this.processFile = processFile;
        this.pairingService = pairingService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        ViewData["Title"] = "Home Page";
        return View();
    }

    [HttpPost]
    public IActionResult Upload(IFormFile file, CancellationToken cancellationToken)
    {
        var result = processFile.ProcessFile(file, cancellationToken);
        if (result.IsSuccess)
        {
            return View("View", result.Data);
        }
        return View("View", result);
    }

    [HttpGet]
    public IActionResult ShowPairs()
    {
        var x = pairingService.GetPairEmployeesProjects();
        return View("PairView", pairingService.GetPairEmployeesProjects());
    }
}