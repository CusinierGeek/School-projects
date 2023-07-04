using AutoMapper;
using KanbanDonnees.DAO.Interfaces;
using KanbanDonnees.Entities;
using KanbanWeb.DTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.AspNetCore.Mvc;

namespace KanbanWeb.Controllers;

[Route("api/tableaux/{tableauId:int}/listes/{listeId:int}/cartes")]
[ApiController]
public class CarteApiController : ControllerBase
{
    private readonly ILogger<CarteApiController> _logger;
    private readonly ICarteDao _carteDao;
    private readonly IMapper _mapper;

    public CarteApiController(ILogger<CarteApiController> logger, IMapper mapper, ICarteDao carteDao)
    {
        _logger = logger;
        _mapper = mapper;
        _carteDao = carteDao;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<Carte> CreerCarte(int listeId, [FromBody] PostCarteDto nvlleCarteDto)
    {
        List<Carte> cartes = _carteDao.SelectAllByListeId(listeId);
        Carte carte = new Carte(nvlleCarteDto.Titre, nvlleCarteDto.Description, nvlleCarteDto.Echeance, cartes.Count,
            listeId);
        carte.Responsables = nvlleCarteDto.Responsables;
        carte = _carteDao.Insert(carte);
        return Ok(carte);
        //return CreatedAtRoute("GetTableau", new { id = tableau.Id }, tableau);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult MettreAjourCarte(int id, [FromBody] PutCarteDto modifCarteDto)
    {
        Carte? carte = _carteDao.Select(id);
        if (carte == null) return NotFound($"Carte avec l'id {id} est inexistante.");
        _mapper.Map(modifCarteDto, carte);
        carte = _carteDao.Update(carte);
        return Ok(carte);
    }

    [HttpPatch("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult MettreAjourPartiellementCarte(int id,
        [FromBody] JsonPatchDocument<PatchCarteDto> patch)
    {
        Carte? carte = _carteDao.Select(id);
        if (carte == null) return NotFound($"Carte avec l'id {id} est inexistante.");

        PatchCarteDto patchCarteDto = _mapper.Map<PatchCarteDto>(carte);
        patch.ApplyTo(patchCarteDto, ModelState);
        TryValidateModel(patchCarteDto);
        if (!ModelState.IsValid) return BadRequest(ModelState);

        _mapper.Map(patchCarteDto, carte);
        carte = _carteDao.Update(carte);

        Operation? operation = patch.Operations.FirstOrDefault(o => o.path == "/ordre");
        if (operation != null) // Il faut mettre à jour l'ordre des cartes car une a changé
        {
            // Important de prendre listeId de la carte et non celui de l'URl car il peut avoir changé
            List<Carte> cartes = _carteDao.SelectAllByListeId(carte.ListeId);
            int nvlIndex = 0;
            foreach (var c in cartes)
            {
                if (c.Id != carte.Id)
                {
                    if (carte.Ordre == nvlIndex) nvlIndex++;
                    c.Ordre = nvlIndex;
                    _carteDao.Update(c);
                    nvlIndex++;
                }
            }
        }

        carte = _carteDao.Update(carte);
        
        // Faudrait faire un create route (location)
        return Ok(carte);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult SupprimerCarte(int listeId, int id)
    {
        bool reussite = _carteDao.Delete(id);

        // Il faut mettre à jour l'ordre des cartes car nous venons d'en supprimer une
        if (reussite)
        {
            List<Carte> cartes = _carteDao.SelectAllByListeId(listeId);
            for (int i = 0; i < cartes.Count; i++)
            {
                cartes[i].Ordre = i;
                _carteDao.Update(cartes[i]);
            }
        }

        return reussite ? NoContent() : NotFound();
    }
}