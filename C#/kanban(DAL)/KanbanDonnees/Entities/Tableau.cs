namespace KanbanDonnees.Entities;

public class Tableau
{
    private int _id;
    private string _nom;
    private List<Liste> _listes;

    public Tableau(int id, string nom)
    {
        Id = id;
        Nom = nom;
        Listes = new List<Liste>();
    }

    public Tableau(string nom) : this(0, nom)
    {
    }

    public int Id
    {
        get => _id;
        set => _id = value;
    }

    public string Nom
    {
        get => _nom;
        set => _nom = value ?? throw new ArgumentNullException(nameof(value));
    }

    public List<Liste> Listes
    {
        get => _listes;
        set => _listes = value ?? throw new ArgumentNullException(nameof(value));
    }
    
    // Vous pouvez ajouter le ToString() si vous voulez, mais c'est non nécessaire
    public override string ToString()
    {
        return $"Tableau : Id = {Id}, Nom = {Nom}, Nombre de listes = {Listes.Count}";
    }

}