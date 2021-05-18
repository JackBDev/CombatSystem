using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpawning : MonoBehaviour
{
    

    [Tooltip("Height should be 1 less than the width if you want each side to be square.")]
    public int gridHeight = 3;
    [Tooltip("This should be one more than the height because the first block in a row is not counted when spawning.")]
    public int gridWidth = 4;
    [Tooltip("Length of time between triggering each spawn animation. Try to keep under half a second.")]
    public float tileSpawnDelay;
    [Tooltip("The prefap for the blocks that make up the battlefield.")]
    public GameObject tilePrefab;

    private List<GameObject> friendlyTiles = new List<GameObject>();
    private List<GameObject> enemyTiles = new List<GameObject>();

    //private GameObject[] row;
    //private int[] rowTileTracker;
    private float tileWidth;

    void Awake()
    {
        tileWidth = UsefulMethods.GetObjectSize(tilePrefab.transform.GetChild(0).gameObject).x;
        //row = new GameObject[gridWidth];
        //rowTileTracker = new int[gridWidth];

        //for(int i = rowTileTracker.Length - 1; i >= 0; i--)
        //{
        //    rowTileTracker[i] = 0;
        //}

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
            newTile.transform.name = a.ToString() + 0.ToString();
            friendlyTiles.Add(newTile);

            for (int b = 1; b <= gridHeight; b++)
            {
                spawnPosition = spawnPosition + new Vector3(-tileWidth, 0f, 0f);
                newTile = Instantiate(tilePrefab, spawnPosition, Quaternion.identity);
                newTile.transform.name = a.ToString() + b.ToString();
                newTile.transform.parent = transform;
                friendlyTiles.Add(newTile);
            }
            spawnPosition = transform.position + new Vector3(0f, 0f, tileWidth * -a);
        }

        spawnPosition = transform.position + new Vector3(tileWidth, 0f, 0f);

        //Enemy's Grid
        for (int a = 1; a <= gridWidth; a++)
        {
            newTile = Instantiate(tilePrefab, spawnPosition, Quaternion.identity);
            newTile.transform.parent = transform;
            newTile.transform.name = a.ToString() + 0.ToString();
            enemyTiles.Add(newTile);

            for (int b = 1; b <= gridHeight; b++)
            {
                spawnPosition = spawnPosition + new Vector3(tileWidth, 0f, 0f);
                newTile = Instantiate(tilePrefab, spawnPosition, Quaternion.identity);
                newTile.transform.name = a.ToString() + b.ToString();
                newTile.transform.parent = transform;
                enemyTiles.Add(newTile);
            }
            spawnPosition = transform.position + new Vector3(tileWidth, 0f, tileWidth * -a);
        }

        StartCoroutine(SpawnAnimationDelay());
    }

    private IEnumerator SpawnAnimationDelay()
    {
        Debug.Log("Count: " + friendlyTiles.Count);

        for (int i = 0; i <= friendlyTiles.Count - 1; i++)
        {
            Debug.Log("I: " + i);

            

            Transform block = friendlyTiles[j].transform.GetChild(0);

            block.GetComponent<Animator>().SetBool("Spawn", true);

            yield return new WaitForSeconds(tileSpawnDelay);
        }  
    }
}
