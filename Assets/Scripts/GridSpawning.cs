using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpawning : MonoBehaviour
{
    public int gridHeight = 3;
    public int gridWidth = 4;
    public float tileSpawnDelay;
    public GameObject tilePrefab;

    [HideInInspector]
    public List<GameObject> friendlyTiles = new List<GameObject>();
    [HideInInspector]
    public List<GameObject> enemyTiles = new List<GameObject>();

    private UsefulMethods functions;
    private float tileWidth;

    void Awake()
    {
        tileWidth = UsefulMethods.GetObjectSize(tilePrefab.transform.GetChild(0).gameObject).x;

        StartCoroutine(SpawnTiles());
    }

    private IEnumerator SpawnTiles()
    {
        Vector3 spawnPosition = transform.position;
        GameObject newTile;

        //Player's Grid
        for (int a = 1; a <= gridWidth; a++)
        {
            newTile = Instantiate(tilePrefab, spawnPosition, Quaternion.identity);
            newTile.transform.parent = transform;
            friendlyTiles.Add(newTile);

            for (int b = 1; b <= gridHeight; b++)
            {
                spawnPosition = spawnPosition + new Vector3(-tileWidth, 0f, 0f);
                newTile = Instantiate(tilePrefab, spawnPosition, Quaternion.identity);
                newTile.transform.parent = transform;
                friendlyTiles.Add(newTile);
                yield return new WaitForSeconds(tileSpawnDelay);
            }

            spawnPosition = transform.position + new Vector3(0f, 0f, tileWidth * -a);
        }

        spawnPosition = transform.position + new Vector3(tileWidth, 0f, 0f);

        //Enemy's Grid
        for (int a = 1; a <= gridWidth; a++)
        {
            newTile = Instantiate(tilePrefab, spawnPosition, Quaternion.identity);
            newTile.transform.parent = transform;
            enemyTiles.Add(newTile);

            for (int b = 1; b <= gridHeight; b++)
            {
                spawnPosition = spawnPosition + new Vector3(tileWidth, 0f, 0f);
                newTile = Instantiate(tilePrefab, spawnPosition, Quaternion.identity);
                newTile.transform.parent = transform;
                enemyTiles.Add(newTile);
                yield return new WaitForSeconds(tileSpawnDelay);
            }

            spawnPosition = transform.position + new Vector3(tileWidth, 0f, tileWidth * -a);
        }
    }
}
