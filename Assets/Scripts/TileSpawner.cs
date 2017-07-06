using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
	public int intervaloTilesProntos; //numero limite de tiles aleatorios que podem ser gerados em sequencia 
									  //ate que seja requisitada a geraçao de um novo tile já pronto
    public int spiderLimitPerRandomTile;
    public GenericPrefab gp;
	public TileData tile_random;
    public List<TileData> tiles_momento1;
	public List<TileData> tiles_momento2;
	public List<TileData> tiles_momento3;
	public List<TileData> tiles_momento4;

	public int tamSpawn; //tamanho do Tile *2
						 //caso seja setado em 0, apenas tiles já prontos serão spawnados

	private GameObject player;
	private int gp_intervalo;
	private float lastPlayerPosY;
    private TileData lastTile;
    private int spiderCount;
	private int momento;
    private int last_momento;

    private List<SpawnableObject> SOpermitidos;



    void Start() {
        SOpermitidos = new List<SpawnableObject>();
		player = GameObject.FindGameObjectWithTag ("Player");
		gp_intervalo = intervaloTilesProntos;
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
		if (gp_intervalo > 0) {
            lastTile = geraTileNovoAleatoriamente();
			gp_intervalo--;
		} else {
            lastTile = geraTilePronto();
			gp_intervalo = intervaloTilesProntos;
        }
        gp.generate(lastTile);
    }
    
	TileData geraTileNovoAleatoriamente() {
        Debug.Log("TILE RANDOM");
		TileData tile = tile_random;
		for(int pos_linha = 0; pos_linha < tile.matriz.Count; pos_linha++){
			TileData.linha linha = tile.matriz[pos_linha];
			for (int pos_coluna = 0; pos_coluna < linha.line.Count; pos_coluna++) {

                SpawnableObject temp = SOpermitidos[Random.Range(0, SOpermitidos.Count)];
                
                tile.matriz[pos_linha].line[pos_coluna] = temp;
                while (!bomSpawn(pos_linha, pos_coluna, tile, tile.matriz[pos_linha].line[pos_coluna])) {
					tile.matriz[pos_linha].line[pos_coluna] = SOpermitidos[Random.Range(0, SOpermitidos.Count)];
                }
            }
		}
        spiderCount = spiderLimitPerRandomTile;
        return tile;
	}

    public void setMomento(int valor)
    {
        last_momento = momento;
        momento = 4;
        if (last_momento != momento) { ativa_momento(momento); }
    }

    private void ativa_momento(int num)
    {
        switch (num) {
            case 1:
                SOpermitidos.Clear();
                SOpermitidos.Add(SpawnableObject.COIN);
                SOpermitidos.Add(SpawnableObject.SAW);
                SOpermitidos.Add(SpawnableObject.NADA);
                break;

            case 2:
                SOpermitidos.Clear();
                SOpermitidos.Add(SpawnableObject.SAW);
                SOpermitidos.Add(SpawnableObject.COIN);
                SOpermitidos.Add(SpawnableObject.SPIDER);
                SOpermitidos.Add(SpawnableObject.NADA);
                break;
            case 3:
                SOpermitidos.Clear();
                SOpermitidos.Add(SpawnableObject.SAW);
                SOpermitidos.Add(SpawnableObject.COIN);
                SOpermitidos.Add(SpawnableObject.SPIDER);
                SOpermitidos.Add(SpawnableObject.NADA);
                break;
            case 4:
                SOpermitidos.Clear();
                //SOpermitidos.Add(SpawnableObject.SAW);
                SOpermitidos.Add(SpawnableObject.COIN);
                SOpermitidos.Add(SpawnableObject.SPIDER);
                SOpermitidos.Add(SpawnableObject.CUPIM);
                SOpermitidos.Add(SpawnableObject.NADA);
                break;
                
        }
    }

    bool bomSpawn(int pos_linha, int pos_coluna, TileData tile, SpawnableObject so) {
        List<SpawnableObject> adj = getAdjacent(pos_linha, pos_coluna, tile);
        //checar se há serra adjacente à atual

        //TODO: trocar pra checar se há mais de 1 serra na adj

        if (tile.matriz[pos_linha].line[pos_coluna] == SpawnableObject.SAW && adj.Contains(SpawnableObject.SAW)) {
            return false;
		}
        //checar se há serra nas adjacências de um cupim
        if (tile.matriz[pos_linha].line[pos_coluna] == SpawnableObject.CUPIM && adj.Contains(SpawnableObject.SAW)) {
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
		TileData randomTile = null;
        Debug.Log("TILE PRONTO");
		switch (momento){
		case 1:
			randomTile = tiles_momento1 [Random.Range (0, tiles_momento1.Count)];
			break;
		case 2:
			randomTile = tiles_momento2[Random.Range(0, tiles_momento2.Count)];
			break;
		case 3:
			randomTile = tiles_momento3[Random.Range(0, tiles_momento3.Count)];
			break;
		case 4:
			randomTile = tiles_momento4[Random.Range(0, tiles_momento4.Count)];
			break;
		}
        Debug.Log(" * tile escolhido: " + randomTile);
        return randomTile;
	}

    //calcula as adjacencias entre os campos da matriz de um tile
    List<SpawnableObject> getAdjacent(int pos_linha, int pos_coluna, TileData tile)
    {
        List<SpawnableObject> adj = new List<SpawnableObject>();
        if (tile.matriz[pos_linha].line.Count == 2)
        {

            adj.Add(tile.matriz[pos_linha].line[(pos_coluna) % 2]);

            if (pos_coluna == 0)
            {
                if (pos_linha > 0)
                {
                    adj.Add(tile.matriz[pos_linha - 1].line[0]);
                    adj.Add(tile.matriz[pos_linha - 1].line[1]);
                }
                if (pos_linha < tile.matriz.Count - 1)
                {
                    adj.Add(tile.matriz[pos_linha + 1].line[0]);
                    adj.Add(tile.matriz[pos_linha + 1].line[1]);
                }
                if (pos_linha == tile.matriz.Count && lastTile != null)
                {
                    adj.Add(lastTile.matriz[0].line[0]);
                    adj.Add(lastTile.matriz[0].line[1]);
                }
            }
            else if (pos_coluna == 1)
            {
                if (pos_linha > 0)
                {
                    adj.Add(tile.matriz[pos_linha - 1].line[1]);
                    adj.Add(tile.matriz[pos_linha - 1].line[2]);
                }
                if (pos_linha < tile.matriz.Count - 1)
                {
                    adj.Add(tile.matriz[pos_linha + 1].line[1]);
                    adj.Add(tile.matriz[pos_linha + 1].line[2]);
                }
                if (pos_linha == tile.matriz.Count && lastTile != null)
                {
                    adj.Add(lastTile.matriz[0].line[1]);
                    adj.Add(lastTile.matriz[0].line[2]);
                }
            }
        }
        else if (tile.matriz[pos_linha].line.Count == 3)
        {
            adj.Add(tile.matriz[pos_linha].line[(pos_coluna + 1) % 3]);
            adj.Add(tile.matriz[pos_linha].line[(pos_coluna + 2) % 3]);
            if (pos_coluna == 0)
            {
                if (pos_linha > 0)
                {
                    adj.Add(tile.matriz[pos_linha - 1].line[0]);
                }
                if (pos_linha < tile.matriz.Count - 1)
                {
                    adj.Add(tile.matriz[pos_linha + 1].line[0]);
                }
                if (pos_linha == tile.matriz.Count && lastTile != null)
                {
                    adj.Add(lastTile.matriz[0].line[0]);
                }
            }
            else if (pos_coluna == 1)
            {
                if (pos_linha > 0)
                {
                    adj.Add(tile.matriz[pos_linha - 1].line[0]);
                    adj.Add(tile.matriz[pos_linha - 1].line[1]);
                }
                if (pos_linha < tile.matriz.Count - 1)
                {
                    adj.Add(tile.matriz[pos_linha + 1].line[0]);
                    adj.Add(tile.matriz[pos_linha + 1].line[1]);
                }
                if (pos_linha == tile.matriz.Count && lastTile != null)
                {
                    adj.Add(lastTile.matriz[0].line[0]);
                    adj.Add(lastTile.matriz[0].line[1]);
                }
            }
            else if (pos_coluna == 2)
            {
                if (pos_linha > 0)
                {
                    adj.Add(tile.matriz[pos_linha - 1].line[1]);
                }
                if (pos_linha < tile.matriz.Count - 1)
                {
                    adj.Add(tile.matriz[pos_linha + 1].line[1]);
                }
                if (pos_linha == tile.matriz.Count && lastTile != null)
                {
                    adj.Add(lastTile.matriz[0].line[1]);
                }
            }
        }
        return adj;
    }
}
