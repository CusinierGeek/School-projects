using KanbanDonnees.DAO.Interfaces;
using KanbanDonnees.Entities;
using KanbanWeb.DTO;
using Microsoft.AspNetCore.Mvc;

namespace KanbanWeb.Controllers;

[Route("api/tableaux")]
[ApiController]
public class TableauApiController : ControllerBase
{
    private readonly ILogger<TableauApiController> _logger;
    private readonly ITableauDao _tableauDao;
    private readonly IListeDao _listeDao;
    private readonly ICarteDao _carteDao;

    public TableauApiController(ILogger<TableauApiController> logger, ITableauDao tableauDao, IListeDao listeDao, ICarteDao carteDao)
    {
        _logger = logger;
        _tableauDao = tableauDao;
        _listeDao = listeDao;
        _carteDao = carteDao;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<Tableau>> GetAllTableaux()
    {
        return Ok(_tableauDao.SelectAll());
    }
    
    [HttpGet("{id:int}", Name = "GetTableau")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Tableau> GetTableau(int id)
    {
        Tableau? tableau = _tableauDao.Select(id);
        if (tableau == null) return NotFound($"Tableau avec l'id {id} est inexistant.");
        return Ok(tableau);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public ActionResult<Tableau> CreerTableau([FromBody] PostPutTableauDto nvTableauDto)
    {
        Tableau tableau = new Tableau(nvTableauDto.Nom);
        tableau = _tableauDao.Insert(tableau);
        // Pour ajouter un header Location dans la réponse
        return CreatedAtRoute("GetTableau", new { id = tableau.Id }, tableau);
    }
    
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult MettreAjourTableau(int id, [FromBody] PostPutTableauDto modifTableauDto)
    {
        Tableau? tableau = _tableauDao.Select(id);
        if (tableau == null) return NotFound($"Tableau avec l'id {id} est inexistant.");
        tableau.Nom = modifTableauDto.Nom;
        tableau = _tableauDao.Update(tableau);
        return Ok(tableau);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult SupprimerTableau(int id)
    {
        bool reussite = _tableauDao.Delete(id);
        return reussite ? NoContent() : NotFound();
    }
}