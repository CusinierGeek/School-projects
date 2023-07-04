using AutoMapper;
using KanbanDonnees.DAO.Interfaces;
using KanbanDonnees.Entities;
using KanbanWeb.DTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.AspNetCore.Mvc;

namespace KanbanWeb.Controllers;

[Route("api/tableaux/{tableauId:int}/listes")]
[ApiController]
public class ListeApiController : ControllerBase
{
    private readonly ILogger<ListeApiController> _logger;
    private readonly IListeDao _listeDao;
    private readonly IMapper _mapper;

    public ListeApiController(ILogger<ListeApiController> logger, IMapper mapper, IListeDao listeDao)
    {
        _logger = logger;
        _mapper = mapper;
        _listeDao = listeDao;
    }

    [HttpGet("{id:int}", Name = "GetListe")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Tableau> GetListe(int id)
    {
        Liste? liste = _listeDao.Select(id);
        if (liste == null) return NotFound($"La liste avec l'id {id} est inexistante.");
        return Ok(liste);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public ActionResult<Liste> CreerListe(int tableauId, [FromBody] PostListeDto nvlleListeDto)
    {
        List<Liste> listes = _listeDao.SelectAllByTableauId(tableauId);
        Liste liste = new Liste(nvlleListeDto.Nom, listes.Count, tableauId);
        liste = _listeDao.Insert(liste);
        return CreatedAtRoute("GetListe", new { tableauId = liste.TableauId, id = liste.Id }, liste);
    }

    [HttpPatch("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult MettreAjourPartiellementListe(int tableauId, int id,
        [FromBody] JsonPatchDocument<PatchListeDto> patch)
    {
        Liste? liste = _listeDao.Select(id);
        if (liste == null) return NotFound($"La liste avec l'id {id} est inexistante.");

        PatchListeDto patchListeDto = _mapper.Map<PatchListeDto>(liste);
        patch.ApplyTo(patchListeDto, ModelState);
        TryValidateModel(patchListeDto); // Sans ça, la validation ne se fait pas
        if (!ModelState.IsValid) return BadRequest(ModelState);

        _mapper.Map(patchListeDto, liste);

        Operation? operation = patch.Operations.FirstOrDefault(o => o.path == "/ordre");
        if (operation != null) // Il faut mettre à jour l'ordre des listes car une a changé
        {
            List<Liste> listes = _listeDao.SelectAllByTableauId(tableauId);
            int nvlIndex = 0;
            foreach (var l in listes)
            {
                if (l.Id != liste.Id)
                {
                    if (liste.Ordre == nvlIndex) nvlIndex++;
                    l.Ordre = nvlIndex;
                    _listeDao.Update(l);
                    nvlIndex++;
                }
            }
        }

        liste = _listeDao.Update(liste);
        
        // Faudrait faire un create route (location)
        return Ok(liste);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult SupprimerListe(int tableauId, int id)
    {
        bool reussite = _listeDao.Delete(id);
        
        // Il faut mettre à jour l'ordre des listes car nous venons d'en supprimer une
        if (reussite)
        {
            List<Liste> listes = _listeDao.SelectAllByTableauId(tableauId);
            for (int i = 0; i < listes.Count; i++)
            {
                listes[i].Ordre = i;
                _listeDao.Update(listes[i]);
            }
        }
        
        return reussite ? NoContent() : NotFound();
    }
}