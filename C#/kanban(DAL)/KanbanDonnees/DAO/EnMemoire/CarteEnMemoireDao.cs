using KanbanDonnees.DAO.Interfaces;
using KanbanDonnees.Entities;

namespace KanbanDonnees.DAO.EnMemoire;

public class CarteEnMemoireDao : ICarteDao
{
    public Carte? Select(int id)
    {
        Carte? carte = DonneesEnMemoire.Cartes.FirstOrDefault(c => c.Id == id);
        if (carte != null) carte.Responsables = SelectUtilisateursCarte(carte);
        return carte;
    }

    public List<Carte> SelectAllByListeId(int listeId)
    {
        List<Carte> cartes = DonneesEnMemoire.Cartes.FindAll(c => c.ListeId == listeId);
        cartes.ForEach(c => c.Responsables = SelectUtilisateursCarte(c));
        return cartes.OrderBy(c => c.Ordre).ToList();
    }

    public Carte Insert(Carte carte)
    {
        carte.Id = DonneesEnMemoire.Cartes.MaxBy(c => c.Id)?.Id + 1 ?? 1;
        DonneesEnMemoire.Cartes.Add(carte);
        carte.Responsables.ForEach(u =>
            DonneesEnMemoire.CarteUtilisateurs.Add(new DonneesEnMemoire.CarteUtilisateur(u.Id, carte.Id)));
        return carte;
    }

    public Carte Update(Carte carte)
    {
        // On fait une copie de la liste des responsables car on perd la référence en récupérant l'ancienne carte
        List<Utilisateur> nvResponsables = carte.Responsables.ToList(); 
        
        Carte? ancienneCarte = Select(carte.Id);
        if (ancienneCarte == null) throw new ArgumentException("Carte inexistante");

        ancienneCarte.Titre = carte.Titre;
        ancienneCarte.Description = carte.Description;
        ancienneCarte.Echeance = carte.Echeance;
        ancienneCarte.Ordre = carte.Ordre;
        ancienneCarte.ListeId = carte.ListeId;

        UpdateUtilisateursCarte(ancienneCarte, nvResponsables);

        return ancienneCarte;
    }

    public bool Delete(int id)
    {
        Carte? carteAsupprimer = Select(id);
        if (carteAsupprimer != null)
        {
            DonneesEnMemoire.Cartes.Remove(carteAsupprimer);
            DeleteUtilisateursCarte(carteAsupprimer);
            return true;
        }

        return false;
    }

    private List<Utilisateur> SelectUtilisateursCarte(Carte carte)
    {
        List<Utilisateur> reponse = new List<Utilisateur>();
        List<DonneesEnMemoire.CarteUtilisateur> cus =
            DonneesEnMemoire.CarteUtilisateurs.FindAll(cu => cu.CarteId == carte.Id);
        foreach (DonneesEnMemoire.CarteUtilisateur cu in cus)
        {
            Utilisateur? u = DonneesEnMemoire.Utilisateurs.FirstOrDefault(u => u.Id == cu.UtilisateurId);
            if (u != null) reponse.Add(u);
        }

        return reponse;
    }

    private void UpdateUtilisateursCarte(Carte carte, List<Utilisateur> nvResponsables)
    {
        DeleteUtilisateursCarte(carte);
        foreach (var r in nvResponsables)
            DonneesEnMemoire.CarteUtilisateurs.Add(new DonneesEnMemoire.CarteUtilisateur(r.Id, carte.Id));
        
        carte.Responsables = SelectUtilisateursCarte(carte);
    }

    private void DeleteUtilisateursCarte(Carte carte)
    {
        var temp = DonneesEnMemoire.CarteUtilisateurs.ToArray();
        foreach (DonneesEnMemoire.CarteUtilisateur cu in temp)
            if (cu.CarteId == carte.Id)
                DonneesEnMemoire.CarteUtilisateurs.Remove(cu);
    }
}