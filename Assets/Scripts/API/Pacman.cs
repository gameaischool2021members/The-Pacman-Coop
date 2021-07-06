using UnityEngine;
using System.Collections.Generic;

public class Pacman : MonoBehaviour
{
    public LevelsGenerator levelsGenerator;

    public Pacman()
    {
        
    }

    public List<GameObject> GetPacmen()
    {
        var pacmen = levelsGenerator.liPacmen;
        
        foreach (GameObject p in pacmen)
            if (p == null)
                pacmen.Remove(p);

        return pacmen;
    }

    public List<GameObject> GetGhosts()
    {
        var ghosts = levelsGenerator.fantomes;
        
        foreach (GameObject g in ghosts)
            if (g == null)
                ghosts.Remove(g);

        return ghosts;
    }

    public List<GameObject> GetPellets()
    {
        var pellets = levelsGenerator.liPacgomme;

        foreach (GameObject p in pellets)
            if (p == null)
                pellets.Remove(p);

        return pellets;
    }

    public List<GameObject> GetLargePellets()
    {
        var big_pellets = levelsGenerator.liSpPacgomme;
        
        foreach (GameObject p in big_pellets)
            if (p == null)
                big_pellets.Remove(p);

        return big_pellets;
    }

    public List<Transform> GetWalls()
    {
        var big_pellets = new List<Transform>(levelsGenerator.wallParent.GetComponentsInChildren<Transform>());
        
        foreach (Transform p in big_pellets)
            if (p == null)
                big_pellets.Remove(p);

        return big_pellets;
    }
}