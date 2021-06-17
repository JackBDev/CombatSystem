using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpawning : MonoBehaviour
{
    [Tooltip("Height should be 1 less than the width if you want each side to be square.")]
    public byte gridHeight = 3;
    [Tooltip("This should be one more than the height because the first block in a row is not counted when spawning.")]
    public byte gridWidth = 5;
    [Tooltip("Length of time between triggering each spawn animation. Try to keep under half a second.")]
    public float tileSpawnDelay;
    [Tooltip("The prefap for the blocks that make up the battlefield.")]
    public GameObject tilePrefab;

    private List<GameObject[]> friendlyTiles = new List<GameObject[]>();
    private List<GameObject[]> enemyTiles = new List<GameObject[]>();
    private float tileWidth;

    void Awake()
    {
        tileWidth = UsefulMethods.GetObjectSize(tilePrefab.transform.GetChild(0).gameObject).x;

        for (byte i = 0; i < gridWidth; i++)
        {
            friendlyTiles.Add(new GameObject[gridWidth]);
            enemyTiles.Add(new GameObject[gridWidth]);
        }

        SpawnBattlefieldGrid();
    }

    public void SpawnBattlefieldGrid()
    {
        Vector3 spawnPosition = transform.position;
        GameObject newTile;

        StartCoroutine(SpawnAnimationDelay());
    }

    IEnumerator SpawnAnimationDelay()
    {
        Vector3 spawnPositionPlayerTiles = transform.position;
        Vector3 spawnPositionEnemyTiles = transform.position;
        
        WaitForSecondsRealtime wait = new WaitForSecondsRealtime(tileSpawnDelay);

        //Player's Grid
        for (byte a = 0; a < gridWidth; a++)
        {
            spawnPositionPlayerTiles = transform.position + new Vector3(0f, 0f, tileWidth * -a);

            SpawnTile(spawnPositionPlayerTiles, friendlyTiles, a, 0);

            spawnPositionEnemyTiles = transform.position + new Vector3(tileWidth, 0f, tileWidth * -a);

            SpawnTile(spawnPositionEnemyTiles, enemyTiles, a, 0);

            for (byte b = 1; b <= gridHeight; b++)
            {
                yield return wait;

                spawnPositionPlayerTiles = spawnPositionPlayerTiles + new Vector3(-tileWidth, 0f, 0f);

                SpawnTile(spawnPositionPlayerTiles, friendlyTiles, a, b);

                spawnPositionEnemyTiles = spawnPositionEnemyTiles + new Vector3(tileWidth, 0f, 0f);

                SpawnTile(spawnPositionEnemyTiles, enemyTiles, a, b);
            } 
        }
    }

    public void SpawnTile(Vector3 spawnPosition, List<GameObject[]> list, int indexX, int indexY)
    {
        GameObject newTile;

        newTile = Instantiate(tilePrefab, spawnPosition, Quaternion.identity);
        newTile.transform.parent = transform;
        newTile.transform.name = indexX.ToString() + indexY.ToString();
        newTile.transform.GetChild(0).GetComponent<Animator>().SetBool("Spawn", true);

        friendlyTiles[indexX][indexY] = newTile;
    }
}
