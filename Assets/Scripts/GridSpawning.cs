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
    public List<GameObject[]> friendlyTiles = new List<GameObject[]>();
    [HideInInspector]
    public List<GameObject[]> enemyTiles = new List<GameObject[]>();

    private GameObject[] row;
    private UsefulMethods functions;
    private float tileWidth;

    void Awake()
    {
        tileWidth = UsefulMethods.GetObjectSize(tilePrefab.transform.GetChild(0).gameObject).x;
        row = new GameObject[gridWidth];

        SpawnBattlefieldGrid();
    }

    public void SpawnBattlefieldGrid()
    {
        Vector3 spawnPosition = transform.position;
        GameObject newTile;

        //Player's Grid
        for (int a = 1; a <= gridWidth; a++)
        {
            newTile = Instantiate(tilePrefab, spawnPosition, Quaternion.identity);
            newTile.transform.parent = transform;
            row[0] = newTile;

            for (int b = 1; b <= gridHeight; b++)
            {
                spawnPosition = spawnPosition + new Vector3(-tileWidth, 0f, 0f);
                newTile = Instantiate(tilePrefab, spawnPosition, Quaternion.identity);
                newTile.transform.parent = transform;
                row[b] = newTile;
            }

            friendlyTiles.Add(row);
            spawnPosition = transform.position + new Vector3(0f, 0f, tileWidth * -a);
        }

        spawnPosition = transform.position + new Vector3(tileWidth, 0f, 0f);

        //Enemy's Grid
        for (int a = 1; a <= gridWidth; a++)
        {
            newTile = Instantiate(tilePrefab, spawnPosition, Quaternion.identity);
            newTile.transform.parent = transform;
            row[0] = newTile;

            for (int b = 1; b <= gridHeight; b++)
            {
                spawnPosition = spawnPosition + new Vector3(tileWidth, 0f, 0f);
                newTile = Instantiate(tilePrefab, spawnPosition, Quaternion.identity);
                newTile.transform.parent = transform;
                row[b] = newTile;
            }
            enemyTiles.Add(row);
            spawnPosition = transform.position + new Vector3(tileWidth, 0f, tileWidth * -a);
        }

        StartCoroutine(SpawnAnimationDelay());
    }

    private IEnumerator SpawnAnimationDelay()
    {
        for(int i = 1; i <= friendlyTiles.Count; i++)
        {

        }

        yield return new WaitForSeconds(tileSpawnDelay);
    }
}
