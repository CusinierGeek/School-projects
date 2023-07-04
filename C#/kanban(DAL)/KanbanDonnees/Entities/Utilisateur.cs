namespace KanbanDonnees.Entities;

public class Utilisateur
{
    private int _id;
    private string _nomComplet;

    // Constructeur par défaut nécessaire pour le mapper
    public Utilisateur()
    {
    }

    public Utilisateur(int id, string nomComplet)
    {
        Id = id;
        NomComplet = nomComplet;
    }

    public Utilisateur(string nomComplet) : this(0, nomComplet)
    {
    }

    public int Id
    {
        get => _id;
        set => _id = value;
    }

    public string NomComplet
    {
        get => _nomComplet;
        set => _nomComplet = value ?? throw new ArgumentNullException(nameof(value));
    }

    // Vous pouvez ajouter le ToString() si vous voulez, mais c'est non nécessaire
    public override string ToString()
    {
        return $"Id: {Id}, Nom complet: {NomComplet}";
    }

}