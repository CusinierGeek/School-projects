using KanbanDonnees.DAO.Interfaces;
using KanbanDonnees.Entities;
using Microsoft.AspNetCore.Mvc;

namespace KanbanWeb.Controllers;

[Route("api/utilisateurs")]
[ApiController]
public class UtilisateurApiController : ControllerBase
{
    private readonly ILogger<UtilisateurApiController> _logger;
    private readonly IUtilisateurDao _utilisateurDao;
    
    public UtilisateurApiController(ILogger<UtilisateurApiController> logger, IUtilisateurDao utilisateurDao)
    {
        _logger = logger;
        _utilisateurDao = utilisateurDao;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<Utilisateur>> GetAllUtilisateurs()
    {
        return Ok(_utilisateurDao.SelectAll());
    }
}