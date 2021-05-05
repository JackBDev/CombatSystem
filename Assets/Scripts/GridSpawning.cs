using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpawning : MonoBehaviour
{
    public int gridHeight = 4;
    public int gridWidth = 4;
    public GameObject tilePrefab;

    [HideInInspector]
    public List<GameObject> friendlyTiles = new List<GameObject>();
    [HideInInspector]
    public List<GameObject> enemyTiles = new List<GameObject>();

    private SharedFunctions functions;
    private float tileWidth;

    void Awake()
    {
        //ADD ENEMY GRID SPAWNING
        tileWidth = functions.GetObjectSize(tilePrefab).x;

        for(int a = 1; a < gridWidth; a++)
        {
            Vector3 spawnPosition = transform.position + new Vector3(tileWidth * -a, 0f, 0f);

            GameObject newTile =  Instantiate(tilePrefab, spawnPosition, Quaternion.identity);

            friendlyTiles.Add(newTile);

            for (int b = 1; b < gridHeight; b++)
            {
                spawnPosition = spawnPosition + new Vector3(0f, 0f, tileWidth * b);

                newTile = Instantiate(tilePrefab, spawnPosition, Quaternion.identity);

                friendlyTiles.Add(newTile);
            }
        }
    }

    void Update()
    {
        
    }
}
