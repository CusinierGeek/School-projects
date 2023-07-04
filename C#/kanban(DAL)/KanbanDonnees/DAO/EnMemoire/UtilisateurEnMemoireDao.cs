using KanbanDonnees.DAO.Interfaces;
using KanbanDonnees.Entities;

namespace KanbanDonnees.DAO.EnMemoire;

public class UtilisateurEnMemoireDao : IUtilisateurDao
{
    public List<Utilisateur> SelectAll()
    {
        return DonneesEnMemoire.Utilisateurs;
    }
}