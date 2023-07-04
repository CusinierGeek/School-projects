using System.Text;

namespace Projet_final;

class PlainPgm : NetPbmFormat
{
    public PlainPgm(string cheminImage) : base(cheminImage)
    {
        string? nombreMagique = sr.ReadLine()!.Trim(' ');
        if (nombreMagique != "P2")
        {
            throw new NetPbmFormatException($"Nombre magique non valide");
        }

        CreerMatriceOrigine();
        string valeurMax = sr.ReadLine()!.Trim(' ');
        if (!int.TryParse(valeurMax, out valeurMaxPixel) || valeurMaxPixel < 0)
        {
            throw new NetPbmFormatException($"Erreur tons non valide");
        }

        RemplirMatriceOrigine();
    }

    public override void EcrireImageSurDisque(string cheminNvlleImage)
    {
        using StreamWriter sw = new StreamWriter(cheminNvlleImage, false, Encoding.Default);
        sw.WriteLine("P2");
        sw.WriteLine(largeurDouble + " " + hauteurDouble);
        sw.WriteLine(valeurMaxPixel);
        for (int i = 0; i < matriceDouble.GetLength(0); i++)
        {
            for (int j = 0; j < matriceDouble.GetLength(1); j++)
            {
                sw.WriteLine(matriceDouble[i, j]);
            }
        }
    }
}