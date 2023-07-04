using System.Text;

namespace Projet_final;

abstract class NetPbmFormat
{
    protected StreamReader sr;
    protected string cheminImage;
    protected int[,] matriceOrigine = null!;
    protected int[,] matriceDouble = null!;
    private int largeur;
    private int hauteur;
    protected int largeurDouble;
    protected int hauteurDouble;
    private int valeurMinPixel = 0;
    protected int valeurMaxPixel = 1;

    protected NetPbmFormat(string cheminImage)
    {
        this.cheminImage = cheminImage;
        sr = new StreamReader(this.cheminImage, Encoding.GetEncoding("iso-8859-1"));
    }

    protected void CreerMatriceOrigine()
    {
        string[] dimensions = sr.ReadLine()!.Split(' ');
        hauteur = Convert.ToInt32(dimensions[1]);
        largeur = Convert.ToInt32(dimensions[0]);
        if (hauteur < 0 || largeur < 0)
        {
            throw new NetPbmFormatException($"Erreur de dimenssions");
        }

        matriceOrigine = new int[hauteur, largeur];
    }

    protected void RemplirMatriceOrigine()
    {
        for (int i = 0; i < hauteur; i++)
        {
            for (int j = 0; j < largeur; j++)
            {
                try
                {
                    matriceOrigine[i, j] = Convert.ToInt32(sr.ReadLine()!.Trim());
                }
                catch (NullReferenceException)
                {
                    throw new NetPbmFormatException($"Il manque des pixels");
                }
                catch (FormatException)
                {
                    throw new NetPbmFormatException($"Pixel non valide");
                }

                if (matriceOrigine[i, j] > valeurMaxPixel || matriceOrigine[i, j] < valeurMinPixel)
                {
                    throw new NetPbmFormatException($"Pixel hors tons");
                }
            }
        }

        string erreur = sr.ReadLine()!;
        if (erreur != null)
        {
            throw new NetPbmFormatException($"Il y a trop de pixels");
        }

        sr.Dispose();
    }

    public void DoublerImage()
    {
        int[,] matriceAgrandi = new int[2 * hauteur - 1, 2 * largeur - 1];
        hauteurDouble = matriceAgrandi.GetLength(0);
        largeurDouble = matriceAgrandi.GetLength(1);

        for (int i = 0; i < hauteur; i++)
        {
            for (int j = 0; j < largeur; j++)
            {
                matriceAgrandi[i * 2, j * 2] = matriceOrigine[i, j];
            }
        }

        for (int i = 0; i < hauteurDouble; i = i + 2)
        {
            for (int j = 1; j < largeurDouble; j = j + 2)
            {
                matriceAgrandi[i, j] = (matriceAgrandi[i, j - 1] + matriceAgrandi[i, j + 1]) / 2;
            }
        }

        for (int i = 1; i < hauteurDouble; i = i + 2)
        {
            for (int j = 0; j < largeurDouble; j++)
            {
                matriceAgrandi[i, j] = (matriceAgrandi[i - 1, j] + matriceAgrandi[i + 1, j]) / 2;
            }
        }

        matriceDouble = matriceAgrandi;
    }

    public abstract void EcrireImageSurDisque(string cheminNvlleImage);
}