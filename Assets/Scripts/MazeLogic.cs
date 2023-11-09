using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeLogic : MonoBehaviour
{
    public int width = 30;
    public int depth = 30;
    public List<GameObject> Cube;
    public byte[,] map;
    // Start is called before the first frame update
    void Start()
    {
        InitialiseMap();
        GenerateMaps();
        DrawMaps();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitialiseMap()
    {
        map = new byte[width, depth];
        for (int z = 0; z < depth; z++)
        {
            for (int x = 0; x < width; x++)
            {
            map[x, z] = 1;
            }
        }
    }

    public virtual void GenerateMaps()
    {
        for (int z = 0; z < depth; z++)
        {
            for (int x = 0; x < width; x++)
            {
                if (Random.Range(0, 100) < 50)
                {
                    map[x, z] = 0;
                }
            }
        }
    }

    void DrawMaps()
    {
        for (int z = 0; z < depth; z++)
        {
            for (int x = 0; x < width; x++)
            {
                if (map[x, z] == 1)
                {
                    Vector3 pos = new Vector3(x, 0, z);
                    GameObject wall = Instantiate(Cube[Random.Range(0, Cube.Count)], pos, Quaternion.identity);
                }
            }
        }
    }
}
