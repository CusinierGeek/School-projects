using Projet_final;
NetPbmFormat image = null!;
string cheminImage;
do
{
    Console.Write("Dossier contenant les images: ");
    cheminImage = Console.ReadLine()!;
    if (!Directory.Exists(cheminImage))
    {
        Console.WriteLine($"Le dossier {cheminImage} est inexistant");
        Console.ReadLine();
        Console.Clear();
    }
} while (!Directory.Exists(cheminImage));

string[] fichiers = Directory.GetFiles(cheminImage);
foreach (string nomFichier in fichiers)
{
    try
    {
        image = new PlainPbm($@"{nomFichier}");
    }
    catch (NetPbmFormatException)
    {
        try
        {
            image = new PlainPgm($@"{nomFichier}");
        }
        catch (NetPbmFormatException)
        {
            Console.WriteLine($"{nomFichier} n'est pas une image de format NetPbm");
        }
    }

    if (image != null)
    {
        string cheminNvlleImage = Path.GetDirectoryName(nomFichier) + Path.DirectorySeparatorChar + "double-" +
                                  Path.GetFileName(nomFichier);
        image.DoublerImage();
        image.EcrireImageSurDisque(cheminNvlleImage);
        Console.WriteLine(
            $"Traitement de {Path.GetFileName(nomFichier)} --> {Path.GetFileName(cheminNvlleImage)}");
    }
}