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
        return RemoveNullsList(levelsGenerator.liPacmen);
    }

    public List<GameObject> GetGhosts()
    {
        return RemoveNullsList(levelsGenerator.fantomes);
    }

    public List<GameObject> GetPellets()
    {
        return RemoveNullsList(levelsGenerator.liPacgomme);

    }

    public List<GameObject> GetLargePellets()
    {
        return RemoveNullsList(levelsGenerator.liSpPacgomme); 
    }

    public List<Transform> GetWalls()
    {
        var walls = new List<Transform>(levelsGenerator.wallParent.GetComponentsInChildren<Transform>());
        
        foreach (Transform p in walls)
            if (p == null)
                walls.Remove(p);

        return walls;
    }

    private List<GameObject> RemoveNullsList(List<GameObject> lst)
    {
        int count = lst.Count;

        for (int i = count - 1; i >= 0; i--)
        {
            if (lst[i] == null)
            {
                lst.RemoveAt(i);
            }
        }
        return lst;
    }

}