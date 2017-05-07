using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    public float timeToNextSpawn;
    public GenericPrefab aux;

    public List<TileData> tiles;

    void Start()
    {       

        StartCoroutine(spawn());
        
        
    }

    IEnumerator spawn()
    {
        while (true)
        {
            TileData randomTile = tiles[Random.Range(0, tiles.Count)];
           // Debug.Log("tile escolhido: " + randomTile);
            aux.generate(randomTile);
            yield return new WaitForSeconds(timeToNextSpawn);
        }
    }
}
