using System.Text;

namespace Projet_final;

class PlainPbm : NetPbmFormat
{
    public PlainPbm(string cheminImage) : base(cheminImage)
    {
        string nombreMagique = sr.ReadLine()!.Trim(' ');
        if (nombreMagique != "P1")
        {
            throw new NetPbmFormatException($"Nombre magique non valide");
        }

        CreerMatriceOrigine();
        RemplirMatriceOrigine();
    }

    public override void EcrireImageSurDisque(string cheminNvlleImage)
    {
        using StreamWriter sw = new StreamWriter(cheminNvlleImage, false, Encoding.Default);
        sw.WriteLine("P1");
        sw.WriteLine(largeurDouble + " " + hauteurDouble);
        for (int i = 0; i < matriceDouble.GetLength(0); i++)
        {
            for (int j = 0; j < matriceDouble.GetLength(1); j++)
            {
                sw.WriteLine(matriceDouble[i, j]);
            }
        }
    }
}