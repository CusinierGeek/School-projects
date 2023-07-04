using KanbanDonnees.DAO.Mysql;
using KanbanDonnees.Entities;
using MySql.Data.MySqlClient;

string chaineDeConnexion = "server=localhost;port=3306;uid=dev;pwd=dev;database=projet;";
UtilisateurMysqlDao utilisateurlDao = new UtilisateurMysqlDao(chaineDeConnexion);
CarteMysqlDao carteDao = new CarteMysqlDao(chaineDeConnexion, utilisateurlDao);
ListeMysqlDao listeDao = new ListeMysqlDao(chaineDeConnexion,carteDao);
TableauMysqlDao tableaulDao = new TableauMysqlDao(chaineDeConnexion, listeDao);

TestCrudlCarte(carteDao);

// TestCrudListe(listeDao);

// TestCrudUtilisateur(utilisateurlDao);
// TestCrudlTableau(tableaulDao);

void TestCrudlTableau(TableauMysqlDao tableauDao)
{
    try
    {
        Console.WriteLine("==========SELECT==========");
        Tableau? tableau = tableauDao.Select(2);
        Console.WriteLine(tableau?.ToString());
        
        Console.WriteLine("======CARTES=========");

        foreach (Liste l in tableau.Listes)
        {
            Console.WriteLine(l.ToString());
        }
        
        Console.WriteLine("==========SELECT ALL==========");
        List<Tableau> tableaux = tableauDao.SelectAll();
        foreach (Tableau t in tableaux)
        {
            Console.WriteLine(t.ToString());
        }
        
        Console.WriteLine("==========INSERT==========");
        Tableau tableau2 = new Tableau("Nouveau tableau");
        tableau2 = tableauDao.Insert(tableau2);
        Console.WriteLine(tableauDao.Select(tableau2.Id));
        
        Console.WriteLine("==========UPDATE==========");
        tableau2.Nom = "Nom modifié";
        tableau2 = tableauDao.Update(tableau2);
        Console.WriteLine(tableauDao.Select(tableau2.Id));
        
        // Console.WriteLine("==========DELETE==========");
        // bool supprime = tableauDao.Delete(tableau2.Id);
        // Console.WriteLine($"Supprimé ? {supprime}");

    }
    catch (MySqlException e)
    {
        Console.WriteLine(e.Message);
        
    }
}

void TestCrudUtilisateur(UtilisateurMysqlDao utilisateurDao)
{
    Console.WriteLine("==========SELECT ALL==========");
    List<Utilisateur> utilisateurs = utilisateurlDao.SelectAll();
    foreach (Utilisateur u in utilisateurs)
    {
        Console.WriteLine(u.ToString());
    }
}

void TestCrudListe(ListeMysqlDao listeMysqlDao)
{
    try
    {
        Console.WriteLine("==========SELECT==========");
        Liste? liste = listeDao.Select(2);
        Console.WriteLine(liste?.ToString());
        
        Console.WriteLine("======CARTES=========");

        foreach (Carte c in liste.Cartes)
        {
            Console.WriteLine(c.ToString());
        }
        
        Console.WriteLine("==========SELECT ALL==========");
        List<Liste> listes = listeDao.SelectAllByTableauId(2);
        foreach (Liste l in listes)
        {
            Console.WriteLine(l.ToString());
        }
        
        Console.WriteLine("==========INSERT==========");
        Liste liste2 = new Liste("New List",0,2);
        liste2 = listeDao.Insert(liste2);
        Console.WriteLine(listeDao.Select(liste2.Id));
        
        Console.WriteLine("==========UPDATE==========");
        liste2.Nom = "Nom modifié";
        liste2 = listeDao.Update(liste2);
        Console.WriteLine(listeDao.Select(liste2.Id));
        
        Console.WriteLine("==========DELETE==========");
        bool supprime = listeDao.Delete(liste2.Id);
        Console.WriteLine($"Supprimé ? {supprime}");

    }
    catch (MySqlException e)
    {
        Console.WriteLine(e.Message);
        
    }

}


static void TestCrudlCarte(CarteMysqlDao carteDao)
{
    try
    {
        Console.WriteLine("==========SELECT==========");
        Carte? carte = carteDao.Select(4);
        Console.WriteLine(carte?.ToString());
        Console.WriteLine("=====RESPONSABLES========");
        foreach (Utilisateur u in carte.Responsables)
        {
            Console.WriteLine(u.ToString());

        }

        Console.WriteLine("==========SELECT ALL==========");
        List<Carte> cartes = carteDao.SelectAllByListeId(4);
        foreach (Carte c in cartes)
        {
            Console.WriteLine(c.ToString());
        }

        Console.WriteLine("==========INSERT==========");
        Carte carte2 = new Carte("Test CRUDL","test crudl en console",null,35,4);
        carte2 = carteDao.Insert(carte2);
        // Allons vérifier dans Workbench que l'article a bien été inséré.
        Console.WriteLine(carteDao.Select(carte2.Id));
        
        Console.WriteLine("==========UPDATE==========");
        carte2.Titre = "titre modifié 222";
        carte2 = carteDao.Update(carte2);
        // Allons vérifier dans Workbench que l'article a bien été modifié.
        Console.WriteLine(carteDao.Select(carte2.Id));
        
        // Console.WriteLine("==========DELETE==========");
        // bool supprime = carteDao.Delete(carte2.Id);
        // Console.WriteLine($"Supprimé ? {supprime}");
    }
    catch (MySqlException ex)
    {
        Console.WriteLine(ex.Message);
    }
    
    
}