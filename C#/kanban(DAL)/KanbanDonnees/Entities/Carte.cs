namespace KanbanDonnees.Entities;

public class Carte
{
    private int _id;
    private string _titre;
    private string _description;
    private DateTime? _echeance;
    private int _ordre;
    private int _listeId;
    private List<Utilisateur> _responsables;

    // Constructeur par défaut nécessaire pour le mapper
    public Carte()
    {
    }

    public Carte(int id, string titre, string description, DateTime? echeance, int ordre, int listeId)
    {
        Id = id;
        Titre = titre;
        Description = description;
        Echeance = echeance;
        Ordre = ordre;
        ListeId = listeId;
        Responsables = new List<Utilisateur>();
    }

    public Carte(string titre, string description, DateTime? echeance, int ordre, int listeId) : this(0, titre,
        description, echeance, ordre, listeId)
    {
    }

    public int Id
    {
        get => _id;
        set => _id = value;
    }

    public string Titre
    {
        get => _titre;
        set => _titre = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Description
    {
        get => _description;
        set => _description = value ?? throw new ArgumentNullException(nameof(value));
    }

    public DateTime? Echeance
    {
        get => _echeance;
        set => _echeance = value;
    }

    public List<Utilisateur> Responsables
    {
        get => _responsables;
        set => _responsables = value ?? throw new ArgumentNullException(nameof(value));
    }

    public int Ordre
    {
        get => _ordre;
        set => _ordre = value;
    }

    public int ListeId
    {
        get => _listeId;
        set => _listeId = value;
    }

    public override string ToString()
    {
        // string responsables = String.Join(", ", Responsables.Select(r => r.Nom));
        
        return
            $"Carte [{_id}]: {_titre}\nDescription: {_description}\nÉchéance: {_echeance}\nOrdre: {_ordre}\nListe ID: {_listeId}\n";
    }
}