using System;
using System.Collections;
using System.Collections.Generic;

    /// <summary>
    ///  La class Matrice permet de modifier des matrice ainsi que de les charger/sauvegarder dans des fichiers.
    /// </summary>
    public class Matrice
    {
        /// <summary> Liste de liste (contient les données).</summary>
        protected List<List<int>> _matrice;
        /// <summary> Hauteur du tableau.</summary>
        protected int _hauteur;
        /// <summary> Largeur du tableau.</summary>
        protected int _largeur;

        /// <summary>
        /// Constructeur par default.
        /// </summary>
        public Matrice()
        {
            _matrice = null;
            _hauteur = 0;
            _largeur = 0;
        }

        /// <summary>
        /// Constructeur paramètrique.
        /// </summary>
        public Matrice(int hauteur, int largeur, int init = 0)
        {
            _matrice = new List<List<int>>();
            _hauteur = hauteur;
            _largeur = largeur;

            for (int i = 0; i < hauteur; i++)
            {
                _matrice.Add(new List<int>());

                for (int j = 0; j < largeur; j++)
                {
                    _matrice[i].Add(init);
                }
            }
        }

        /// <summary>
        /// Constructeur par copy.
        /// </summary>
        public Matrice(Matrice M)
        {
            copy(M);
        }

        /// <summary>
        /// Copie toute les valeur et le format d'une Matrice M.
        /// </summary>
        /// <param name="M"> La matrice que l'on veut copier </param>
        public void copy(Matrice M)
        {
            _matrice = new List<List<int>>();
            _hauteur = M.hauteur();
            _largeur = M.largeur();

            for (int i = 0; i < _hauteur; i++)
            {
                _matrice.Add(new List<int>());

                for (int j = 0; j < _largeur; j++)
                {
                    _matrice[i].Add(M[i, j]);
                }
            }
        }

        /// <summary>
        /// Indexeur de la Matrice permet un acces au données en lecture et en ecriture.
        /// </summary>
        /// <param name="i"> Correspond a la ligne N°i </param>
        /// <param name="j"> Correspond a la colonne N°j </param>
        /// <returns> La valeur que la matrice contiens à la ligne i et la colonne j </returns>
        public int this[int i, int j]
        {
            get
            {
                if (i < 0 || i >= _hauteur || j < 0 || j >= _largeur)
                {
                    throw new IndexOutOfRangeException();
                }
                else
                {
                    return _matrice[i][j];
                }
            }
            set
            {
                if (i < 0 || i >= _hauteur || j < 0 || j >= _largeur)
                {
                    throw new IndexOutOfRangeException();
                }
                else
                {
                    _matrice[i][j] = value;
                }
            }
        }

        /// <summary>
        /// Accesseur en lecture des données.
        /// </summary>
        /// <param name="i"> Correspond à la ligne N°i </param>
        /// <param name="j"> Correspond à la colonne N°j </param>
        /// <returns> La valeur de la matrice à la ligne i et la colonne j. </returns>
        public int at(int i, int j)
        {
            return this[i, j];
        }

         /// <summary>
        /// Max
        /// </summary>
        /// <returns> Le Max </returns>
        public double max()
        {
            return Math.Round(Math.Sqrt((this.largeur() * this.largeur()) + (this.hauteur() * this.hauteur()))) + 2;
        }

        /// <summary>
        /// Accesseur en ecriture des données.
        /// </summary>
        /// <param name="i"> Correspond à la ligne N°i </param>
        /// <param name="j"> Correspond à la colonne N°j </param>
        /// <param name="val"> Correspond à la val à changer</param>
        public void setVal(int i, int j, int val)
        {
            this[i, j] = val;
        }
        /*
        /// <summary>
        /// Accesseur en lecture sur la hauteur.
        /// </summary>
        /// <returns> Hauteur de la TileMatrix. </returns>
        public int hauteur { get { return _hauteur; } }

        /// <summary>
        /// Accesseur en lecture sur la largeur.
        /// </summary>
        /// <returns> Largeur de la TileMatrix. </returns>
        public int largeur { get { return _largeur; } }
        */

        /// <summary>
        /// Accesseur en lecture.
        /// </summary>
        /// <returns> La hauteur de la matrice. </returns>
        public int hauteur()
        {
            return _hauteur;
        }

        /// <summary>
        /// Accesseur en lecture.
        /// </summary>
        /// <returns> La largeur de la matrice. </returns>
        public int largeur()
        {
            return _largeur;
        }

        /// <summary>
        /// Ajoute une Ligne à la Matrice.
        /// </summary>
        /// <param name="val"> valeur d'initialisation de la colonne </param>
        public void addLigne(int val = 0)
        {
            _matrice.Add(new List<int>());

            for (int j = 0; j < _largeur; j++)
            {
                _matrice[_hauteur].Add(val);
            }

            _hauteur++;
        }

        /// <summary>
        /// Ajoute une colonne a la matrice.
        /// </summary>
        /// <param name="val"> valeur d'initialisation de la colonne </param>
        public void addColonne(int val = 0)
        {
            for (int i = 0; i < _hauteur; i++)
            {
                _matrice[i].Add(val);
            }

            _largeur++;
        }

        /// <summary>
        /// Accesseur en ecriture, change toute les valeur d'une ligne.
        /// </summary>
        /// <param name="i"> Correspond a la ligne N°i </param>
        /// <param name="val"> Nouvelle valeur sur toute a la ligne i </param>
        public void setLigne(int i, int val)
        {
            int j = 0;
            while (j < largeur())
            {
                this[i, j] = val;
                j++;
            }
        }

        /// <summary>
        /// Accesseur en ecriture, change toute les valeur d'une colonne.
        /// </summary>
        /// <param name="j"> Correspond a la colonne N°j </param>
        /// <param name="val"> Nouvelle valeur sur toute a la colonne j </param>
        public void setColonne(int j, int val)
        {
            int i = 0;
            while (i < hauteur())
            {
                this[i, j] = val;
                i++;
            }
        }

        /// <summary>
        /// Remplace chaque valeur "a" de la matrice par la valeur "b".
        /// </summary>
        /// <param name="a"> Valeur à chercher et remplacer </param>
        /// <param name="b"> Valeur avec laquel sera remplacer "a" </param>
        public void replace(int a, int b)
        {
            for (int i = 0; i < hauteur(); i++)
            {
                for (int j = 0; j < largeur(); j++)
                {
                    if (this[i, j] == a)
                    {
                        this[i, j] = b;
                    }
                }
            }
        }

        /// <summary>
        /// Affiche le contenu de la matrice dans la console.
        /// </summary>
        public void print()
        {
            for (int i = 0; i < hauteur(); i++)
            {
                for (int j = 0; j < largeur(); j++)
                {
                    Console.Write(this[i, j] + " ");
                }
                Console.Write("\n");
            }
        }

        /// <summary>
        /// Sauvegarde toute la matrice dans un fichier.
        /// </summary>
        /// <param name="chemin"> Le chemin du fichier ou l'on veut sauvegarder </param>
        public void save(string chemin)
        {

            System.IO.StreamWriter fichier = System.IO.File.AppendText(chemin);

            string ligne = "";

            for (int i = 0; i < hauteur(); i++)
            {
                for (int j = 0; j < largeur(); j++)
                {
                    ligne += this[i, j] + " ";
                }
                fichier.WriteLine(ligne);
                ligne = "";
            }
            fichier.WriteLine("-");
            fichier.Close();
        }

        /// <summary>
        /// charge une matrice contenu dans un fichier.
        /// </summary>
        /// <param name="chemin"> Le chemin du fichier que l'on veut charger </param>
        /// <param name="num"> le numero de la matrice que l'on veut charger </param>                
        public void load(string chemin, int num = 0)
        {
            System.Collections.Generic.List<int> list = new System.Collections.Generic.List<int>();
            string ligne = "";
            string[] words;
            int hauteur = 0;
            int largeur = 0;
            int k = 0;

            System.IO.StreamReader fichier = new System.IO.StreamReader(chemin);

            for (int i = 0; i < num; i++)
            {
                while (fichier.ReadLine() != "-") { }
            }

            while ((ligne = fichier.ReadLine()) != null && ligne != "-")
            {
                largeur = 0;
                words = ligne.Split();

                for (int i = 0; i < words.Length - 1; i++)
                {
                    int val = 0;
                    int.TryParse(words[i], out val);
                    list.Add(val);
                    largeur++;
                }
                hauteur++;
            }

            _matrice = new List<List<int>>();
            _hauteur = hauteur;
            _largeur = largeur;

            for (int i = 0; i < this.hauteur(); i++)
            {
                _matrice.Add(new List<int>());
                for (int j = 0; j < this.largeur(); j++)
                {
                    _matrice[i].Add(list[k]);
                    k++;
                }
            }
            fichier.Close();
        }
    }