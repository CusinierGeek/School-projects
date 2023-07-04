using KanbanDonnees.DAO.Interfaces;
using KanbanDonnees.Entities;

namespace KanbanDonnees.DAO.EnMemoire;

public class ListeEnMemoireDao : IListeDao
{
    private readonly CarteEnMemoireDao _carteDao;
    
    public ListeEnMemoireDao()
    {
        _carteDao = new CarteEnMemoireDao();
    }
    
    public Liste? Select(int id)
    {
        Liste? liste = DonneesEnMemoire.Listes.FirstOrDefault(l => l.Id == id);
        if (liste != null) liste.Cartes = _carteDao.SelectAllByListeId(liste.Id);
        return liste;
    }

    public List<Liste> SelectAllByTableauId(int tableauId)
    {
        List<Liste> listes = DonneesEnMemoire.Listes.FindAll(l => l.TableauId == tableauId);
        listes.ForEach(l => l.Cartes = _carteDao.SelectAllByListeId(l.Id));
        return listes.OrderBy(l => l.Ordre).ToList();
    }

    public Liste Insert(Liste liste)
    {
        liste.Id = DonneesEnMemoire.Listes.MaxBy(l => l.Id)?.Id + 1 ?? 1;
        DonneesEnMemoire.Listes.Add(liste);
        return liste;
    }

    public Liste Update(Liste liste)
    {
        Liste? ancienneListe = Select(liste.Id);
        if (ancienneListe == null) throw new ArgumentException("Liste inexistante");
        
        ancienneListe.Nom = liste.Nom;
        ancienneListe.Ordre = liste.Ordre;
        
        return ancienneListe;
    }

    public bool Delete(int id)
    {
        Liste? listeAsupprimer = Select(id);
        if (listeAsupprimer != null)
        {
            listeAsupprimer.Cartes.ForEach(c => _carteDao.Delete(c.Id));
            DonneesEnMemoire.Listes.Remove(listeAsupprimer);
            return true;
        }

        return false;
    }
}