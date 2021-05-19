using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpawning : MonoBehaviour
{
    [Tooltip("Height should be 1 less than the width if you want each side to be square.")]
    public byte gridHeight = 3;
    [Tooltip("This should be one more than the height because the first block in a row is not counted when spawning.")]
    public byte gridWidth = 4;
    [Tooltip("Length of time between triggering each spawn animation. Try to keep under half a second.")]
    public float tileSpawnDelay;
    [Tooltip("The prefap for the blocks that make up the battlefield.")]
    public GameObject tilePrefab;

    private List<GameObject[]> friendlyTiles = new List<GameObject[]>();
    private List<GameObject> enemyTiles = new List<GameObject>();
    private float tileWidth;

    void Awake()
    {
        tileWidth = UsefulMethods.GetObjectSize(tilePrefab.transform.GetChild(0).gameObject).x;

        for(byte i = 0; i < gridWidth; i++)
        {
            friendlyTiles.Add(new GameObject[gridWidth]);
        }

        SpawnBattlefieldGrid();
    }

    public void SpawnBattlefieldGrid()
    {
        Vector3 spawnPosition = transform.position;
        GameObject newTile;

        //Player's Grid
        for (byte a = 0; a < gridWidth; a++)
        {
            newTile = Instantiate(tilePrefab, spawnPosition, Quaternion.identity);
            newTile.transform.parent = transform;
            newTile.transform.name = a.ToString() + 0.ToString();
            friendlyTiles[a][0] = newTile;

            for (byte b = 1; b <= gridHeight; b++)
            {
                spawnPosition = spawnPosition + new Vector3(-tileWidth, 0f, 0f);
                newTile = Instantiate(tilePrefab, spawnPosition, Quaternion.identity);
                newTile.transform.name = a.ToString() + b.ToString();
                newTile.transform.parent = transform;
                friendlyTiles[a][b] = newTile;
            }

            spawnPosition = transform.position + new Vector3(0f, 0f, tileWidth * -a);
        }

        spawnPosition = transform.position + new Vector3(tileWidth, 0f, 0f);

        //Enemy's Grid
        for (byte a = 1; a <= gridWidth; a++)
        {
            newTile = Instantiate(tilePrefab, spawnPosition, Quaternion.identity);
            newTile.transform.parent = transform;
            newTile.transform.name = a.ToString() + 0.ToString();
            enemyTiles.Add(newTile);

            for (byte b = 1; b <= gridHeight; b++)
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
        WaitForSeconds wait = new WaitForSeconds(tileSpawnDelay);
        
        for (byte i = 0; i <= gridWidth * 2; i++)
        {
            byte row = i;

            for(byte j = 0; j < i; j++)
            {
                if (j >= gridWidth || row >= gridWidth)
                {
                    if (row > 0)
                    {
                        row--;
                    }
                }
                else
                {
                    Transform block = friendlyTiles[row][j].transform.GetChild(0);

                    block.GetComponent<Animator>().SetBool("Spawn", true);

                    if (row > 0)
                    {
                        row--;
                    }
                }
            }

            yield return wait; 
        }  
    }
}
