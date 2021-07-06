using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// The Matrix class allows you to modify matrices and load/save them to files.
/// </summary>
public class Matrix
{
    /// <summary> List list (contains data).</summary>
    protected List<List<int>> _matrix;
    /// <summary> Height of the table.</summary>
    protected int _height;
    /// <summary> Width of the table.</summary>
    protected int _width;

    /// <summary>
    /// Default constructor.
    /// </summary>
    public Matrix()
    {
        _matrix = null;
        _height = 0;
        _width = 0;
    }

    /// <summary>
    /// Parametric constructor.
    /// </summary>
    public Matrix(int height, int width, int init = 0)
    {
        _matrix = new List<List<int>>();
        _height = height;
        _width = width;

        for (int i = 0; i < height; i++)
        {
            _matrix.Add(new List<int>());

            for (int j = 0; j < width; j++)
            {
                _matrix[i].Add(init);
            }
        }
    }

    /// <summary>
    /// Constructor by copy.
    /// </summary>
    public Matrix(Matrix M)
    {
        copy(M);
    }

    /// <summary>
    /// Copies all the values and format of an M-matrix.
    /// </summary>
    /// <param name="M"> The matrix to be copied </param>
    public void copy(Matrix M)
    {
        _matrix = new List<List<int>>();
        _height = M.height();
        _width = M.width();

        for (int i = 0; i < _height; i++)
        {
            _matrix.Add(new List<int>());

            for (int j = 0; j < _width; j++)
            {
                _matrix[i].Add(M[i, j]);
            }
        }
    }

    /// <summary>
    /// Matrix Indexer allows read and write access to data.
    /// </summary>
    /// <param name="i"> Corresponds to line N°i </param>
    /// <param name="j"> Corresponds to column N°j </param>
    /// <returns> The value that the matrix contains in row i and column j </returns>
    public int this[int i, int j]
    {
        get
        {
            if (i < 0 || i >= _height || j < 0 || j >= _width)
            {
                throw new IndexOutOfRangeException();
            }
            else
            {
                return _matrix[i][j];
            }
        }
        set
        {
            if (i < 0 || i >= _height || j < 0 || j >= _width)
            {
                throw new IndexOutOfRangeException();
            }
            else
            {
                _matrix[i][j] = value;
            }
        }
    }

    /// <summary>
    /// Accessor reading data.
    /// </summary>
    /// <param name="i"> Corresponds to line N°i </param>
    /// <param name="j"> Corresponds to column N°j </param>
    /// <returns> The value of the matrix in row i and column j. </returns>
    public int at(int i, int j)
    {
        return this[i, j];
    }

    /// <summary>
    /// Max
    /// </summary>
    /// <returns> The Max </returns>
    public double max()
    {
        return Math.Round(Math.Sqrt((this.width() * this.width()) + (this.height() * this.height()))) + 2;
    }

    /// <summary>
    /// Write accessor to the data.
    /// </summary>
    /// <param name="i"> Corresponds to line N°i </param>
    /// <param name="j"> Corresponds to column N°j </param>
    /// <param name="val"> Corresponds to the value to be changed</param>
    public void setVal(int i, int j, int val)
    {
        this[i, j] = val;
    }
    /*
    /// <summary>
    /// Reading accessor on height.
    /// </summary>
    /// <returns> Height of the TileMatrix. </returns>
    public int height { get { return _height; } }

    /// <summary>
    /// Reading accessor on width.
    /// </summary>
    /// <returns> Width of the TileMatrix. </returns>
    public int width { get { return _width; } }
    */

    /// <summary>
    /// Reading accessor.
    /// </summary>
    /// <returns> Height of the matrix. </returns>
    public int height()
    {
        return _height;
    }

    /// <summary>
    /// Reading accessor.
    /// </summary>
    /// <returns> Width of the matrix. </returns>
    public int width()
    {
        return _width;
    }

    /// <summary>
    /// Adds a Line to the Matrix.
    /// </summary>
    /// <param name="val"> initialization value of the column </param>
    public void addLine(int val = 0)
    {
        _matrix.Add(new List<int>());

        for (int j = 0; j < _width; j++)
        {
            _matrix[_height].Add(val);
        }

        _height++;
    }

    /// <summary>
    /// Adds a column to the matrix.
    /// </summary>
    /// <param name="val"> initialization value of the column </param>
    public void addColumn(int val = 0)
    {
        for (int i = 0; i < _height; i++)
        {
            _matrix[i].Add(val);
        }

        _width++;
    }

    /// <summary>
    /// Write accessor, changes all the values of a line.
    /// </summary>
    /// <param name="i"> Corresponds to line N°i </param>
    /// <param name="val"> New value on the entire line i </param>
    public void setLigne(int i, int val)
    {
        int j = 0;
        while (j < width())
        {
            this[i, j] = val;
            j++;
        }
    }

    /// <summary>
    /// Write accessor, changes all values in a column.
    /// </summary>
    /// <param name="j"> Corresponds to column N°j </param>
    /// <param name="val"> New value on all a column j </param>
    public void setColumn(int j, int val)
    {
        int i = 0;
        while (i < height())
        {
            this[i, j] = val;
            i++;
        }
    }

    /// <summary>
    /// Replace each "a" value in the matrix with the "b" value.
    /// </summary>
    /// <param name="a"> Value to be searched and replaced </param>
    /// <param name="b"> Value with which to replace "a". </param>
    public void replace(int a, int b)
    {
        for (int i = 0; i < height(); i++)
        {
            for (int j = 0; j < width(); j++)
            {
                if (this[i, j] == a)
                {
                    this[i, j] = b;
                }
            }
        }
    }

    /// <summary>
    /// Displays the contents of the matrix in the console.
    /// </summary>
    public void print()
    {
        for (int i = 0; i < height(); i++)
        {
            for (int j = 0; j < width(); j++)
            {
                Console.Write(this[i, j] + " ");
            }
            Console.Write("\n");
        }
    }

    /// <summary>
    /// Saves the entire matrix to a file.
    /// </summary>
    /// <param name="path"> The path of the file to be saved </param>
    public void save(string path)
    {

        System.IO.StreamWriter fichier = System.IO.File.AppendText(path);

        string ligne = "";

        for (int i = 0; i < height(); i++)
        {
            for (int j = 0; j < width(); j++)
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
    /// loads a matrix contained in a file.
    /// </summary>
    /// <param name="path"> The path of the file to be loaded </param>
    /// <param name="num"> the number of the matrix to be loaded </param>                
    public void load(string path, int num = 0)
    {
        System.Collections.Generic.List<int> list = new System.Collections.Generic.List<int>();
        string line = "";
        string[] words;
        int height = 0;
        int width = 0;
        int k = 0;

        System.IO.StreamReader file = new System.IO.StreamReader(path);

        for (int i = 0; i < num; i++)
        {
            while (file.ReadLine() != "-") { }
        }

        while ((line = file.ReadLine()) != null && line != "-")
        {
            width = 0;
            words = line.Split();

            for (int i = 0; i < words.Length - 1; i++)
            {
                int val = 0;
                int.TryParse(words[i], out val);
                list.Add(val);
                width++;
            }
            height++;
        }

        _matrix = new List<List<int>>();
        _height = height;
        _width = width;

        for (int i = 0; i < this.height(); i++)
        {
            _matrix.Add(new List<int>());
            for (int j = 0; j < this.width(); j++)
            {
                _matrix[i].Add(list[k]);
                k++;
            }
        }
        file.Close();
    }
}