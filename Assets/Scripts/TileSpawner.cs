using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    public float timeToNextSpawn;
    public int spiderLimitPerRandomTile;
    public GenericPrefab aux;
    public List<TileData> tiles;
	public int tamSpawn; //tamanho do Tile *2
	public int intervaloTilesProntos; //numero limite de tiles aleatorios que podem ser gerados em sequencia ate que seja requisitada a geraçao de um novo tile já pronto
									  //caso seja setado em 0, apenas tiles já prontos serão spawnados

	private GameObject player;
	private int aux_intervalo;
	private float lastPlayerPosY;
    private TileData lastTile;
    private int spiderCount;


	void Start() {
		player = GameObject.FindGameObjectWithTag ("Player");
		aux_intervalo = intervaloTilesProntos;
		lastPlayerPosY = player.transform.position.y;
        spiderCount = spiderLimitPerRandomTile;
	}

	//chama uma função a cada x METROS DE ALTURA DO PLAYER (update? corroutine?) que decide se o proximo tile será aleatório ou será um dos já criados
    void Update()
    {   
		if (player.transform.position.y % tamSpawn == 0 && player.transform.position.y != lastPlayerPosY) {
			spawn ();
			lastPlayerPosY = player.transform.position.y;
		}
    }

	//decide qual tile deverá ser spawnado : um aleatorio ou um já pronto
    void spawn()
    {
		if (aux_intervalo > 0) {
            lastTile = geraTileNovoAleatoriamente();
			aux_intervalo--;
		} else {
            lastTile = geraTilePronto();
			aux_intervalo = intervaloTilesProntos;

        }
        aux.generate(lastTile);
    }
    
	TileData geraTileNovoAleatoriamente() {
        Debug.Log("TILE RANDOM");
		TileData tile = tiles[0];
		for(int pos_linha = 0; pos_linha < tile.matriz.Count; pos_linha++){
			TileData.linha linha = tile.matriz[pos_linha];
			for (int pos_coluna = 0; pos_coluna < linha.line.Count; pos_coluna++) {
				SpawnableObject temp = (SpawnableObject)Random.Range(0, System.Enum.GetValues(typeof(SpawnableObject)).Length);
                tile.matriz[pos_linha].line[pos_coluna] = temp;

                


                while (!bomSpawn(pos_linha, pos_coluna, tile, tile.matriz[pos_linha].line[pos_coluna])) {
					tile.matriz[pos_linha].line [pos_coluna] = 
					(SpawnableObject) Random.Range (0, System.Enum.GetValues(typeof(SpawnableObject)).Length);
				}

			}
		}
        return tile;

	}

    List<SpawnableObject> getAdjacent(int pos_linha, int pos_coluna, TileData tile)
    {
        List<SpawnableObject> adj = new List<SpawnableObject>();
        if(tile.matriz[pos_linha].line.Count == 2) {

            adj.Add(tile.matriz[pos_linha].line[(pos_coluna) % 2]);

            if (pos_coluna == 0) {

                if (pos_linha > 0) {
                    adj.Add(tile.matriz[pos_linha - 1].line[0]);

                    adj.Add(tile.matriz[pos_linha - 1].line[1]);

                }
                if (pos_linha < tile.matriz.Count -1) {
                    adj.Add(tile.matriz[pos_linha + 1].line[0]);
                    adj.Add(tile.matriz[pos_linha + 1].line[1]);
                }
                if (pos_linha == tile.matriz.Count && lastTile != null) {
                    adj.Add(lastTile.matriz[0].line[0]);
                    adj.Add(lastTile.matriz[0].line[1]);
                }
            }
            
            else if (pos_coluna == 1) {
                if (pos_linha > 0) {
                    adj.Add(tile.matriz[pos_linha - 1].line[1]);
                    adj.Add(tile.matriz[pos_linha - 1].line[2]);
                }
                if (pos_linha < tile.matriz.Count-1) {
                    adj.Add(tile.matriz[pos_linha + 1].line[1]);
                    adj.Add(tile.matriz[pos_linha + 1].line[2]);
                }
                if (pos_linha == tile.matriz.Count && lastTile != null) {
                    adj.Add(lastTile.matriz[0].line[1]);
                    adj.Add(lastTile.matriz[0].line[2]);
                }
            }

        } else if (tile.matriz[pos_linha].line.Count == 3) {

            adj.Add(tile.matriz[pos_linha].line[(pos_coluna + 1) % 3]);
            adj.Add(tile.matriz[pos_linha].line[(pos_coluna + 2) % 3]);

            if (pos_coluna == 0) {

                if (pos_linha > 0) {

                    adj.Add(tile.matriz[pos_linha -1].line[0]);
                }
                if (pos_linha < tile.matriz.Count-1) {

                    adj.Add(tile.matriz[pos_linha + 1].line[0]);
                }
                if (pos_linha == tile.matriz.Count && lastTile != null) {

                    adj.Add(lastTile.matriz[0].line[0]);
                }
            } else if (pos_coluna == 1){

                if (pos_linha > 0) {

                    adj.Add(tile.matriz[pos_linha - 1].line[0]);
                    adj.Add(tile.matriz[pos_linha - 1].line[1]);
                }
                if (pos_linha < tile.matriz.Count-1) {

                    adj.Add(tile.matriz[pos_linha + 1].line[0]);
                    adj.Add(tile.matriz[pos_linha + 1].line[1]);
                }
                if (pos_linha == tile.matriz.Count && lastTile != null) {

                    adj.Add(lastTile.matriz[0].line[0]);
                    adj.Add(lastTile.matriz[0].line[1]);
                }
            } else if (pos_coluna == 2) {

                if (pos_linha > 0) {

                    adj.Add(tile.matriz[pos_linha - 1].line[1]);
                }
                if (pos_linha < tile.matriz.Count-1) {

                    adj.Add(tile.matriz[pos_linha + 1].line[1]);
                }
                if (pos_linha == tile.matriz.Count && lastTile != null) {

                    adj.Add(lastTile.matriz[0].line[1]);
                }
            }
        }
        return adj;
    }
    

	bool bomSpawn(int pos_linha, int pos_coluna, TileData tile, SpawnableObject so) {

        
        List<SpawnableObject> adj = getAdjacent(pos_linha, pos_coluna, tile);
		//checar se há + de 1 serra adjacente à atual
		if(tile.matriz[pos_linha].line[pos_coluna] == SpawnableObject.SAW && adj.Contains(SpawnableObject.SAW)){

            return false;
		}
		//checar se há mais de uma serra numa linha de 2
		if (pos_linha%2 == 1 
			&& pos_coluna == 1 
			&& tile.matriz[pos_linha].line[pos_coluna-1] == SpawnableObject.SAW 
			&& tile.matriz[pos_linha].line[pos_coluna] == SpawnableObject.SAW) {

            return false;
		}
        //checar se tem muita aranha
         if (so == SpawnableObject.SPIDER)
         {
             spiderCount--;
             if(spiderCount <= 0) return false;
         }


        return true;
	}

	TileData geraTilePronto() {
        Debug.Log("TILE PRONTO");
        TileData randomTile = tiles[Random.Range(0, tiles.Count)];
        Debug.Log(" * tile escolhido: " + randomTile);
        return randomTile;
	}
}
