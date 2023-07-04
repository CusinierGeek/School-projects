using KanbanDonnees.DAO.Interfaces;
using KanbanDonnees.Entities;

namespace KanbanDonnees.DAO.EnMemoire;

public class TableauEnMemoireDao : ITableauDao
{
    private readonly  ListeEnMemoireDao _listeDao;

    public TableauEnMemoireDao()
    {
        _listeDao = new ListeEnMemoireDao();
    }
    
    public Tableau? Select(int id)
    {
        Tableau? tableau = DonneesEnMemoire.Tableaux.FirstOrDefault(t => t.Id == id);
        if (tableau != null) tableau.Listes = _listeDao.SelectAllByTableauId(tableau.Id);
        return tableau;
    }

    public List<Tableau> SelectAll()
    {
        return DonneesEnMemoire.Tableaux;
    }

    public Tableau Insert(Tableau tableau)
    {
        tableau.Id = DonneesEnMemoire.Tableaux.MaxBy(t => t.Id)?.Id + 1 ?? 1;
        DonneesEnMemoire.Tableaux.Add(tableau);
        return tableau;
    }

    public Tableau Update(Tableau tableau)
    {
        Tableau? ancienTableau = Select(tableau.Id);
        if (ancienTableau == null) throw new ArgumentException("Tableau inexistant");
        
        ancienTableau.Nom = tableau.Nom;

        return ancienTableau;
    }

    public bool Delete(int id)
    {
        Tableau? tableauAsupprimer = Select(id);
        if (tableauAsupprimer != null)
        {
            tableauAsupprimer.Listes.ForEach(l => _listeDao.Delete(l.Id));
            DonneesEnMemoire.Tableaux.Remove(tableauAsupprimer);
            return true;
        }

        return false;
    }
}