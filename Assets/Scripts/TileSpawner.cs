using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    public float timeToNextSpawn;
    public GenericPrefab aux;
    public List<TileData> tiles;
	public int tamSpawn; //tamanho do Tile *2
	public int intervaloTilesProntos; //numero limite de tiles aleatorios que podem ser gerados em sequencia ate que seja requisitada a geraçao de um novo tile já pronto
									  //caso seja setado em 0, apenas tiles já prontos serão spawnados

	private GameObject player;
	private int aux_intervalo;
	private float lastPlayerPosY;


	void Start() {
		player = GameObject.FindGameObjectWithTag ("Player");
		aux_intervalo = intervaloTilesProntos;
		lastPlayerPosY= player.transform.position.y;
	}

	//chama uma função a cada x METROS DE ALTURA DO PLAYER (update? corroutine?) que decide se o proximo tile será aleatório ou será um dos já criados
    void Update()
    {   
		if (player.transform.position.y % tamSpawn == 0 && player.transform.position.y != lastPlayerPosY) {
			//Debug.Log ("Spawnei. Player.position = " + player.transform.position.y);
			spawn ();
			lastPlayerPosY = player.transform.position.y;
		}
    }

	//decide qual tile deverá ser spawnado : um aleatorio ou um já pronto
    void spawn()
    {
		if (aux_intervalo > 0) {
			geraTileNovoAleatoriamente ();
			aux_intervalo--;
		} else {
			geraTilePronto ();
			aux_intervalo = intervaloTilesProntos;
		}
    }



	void geraTileNovoAleatoriamente() {
		Debug.Log ("TILE RANDOM");
		//opção1: preenche as lacunas de um TileData (vazio?) com spawnable objects aleatorios
		//opção2: gera objetos aleatoriamente tipo como o Spawner antigo funcionava, mas spawnando em blocos (tiles) e não em linhas
		//caso 1, os objetos ja sao spawnados aqui mesmo o que eu particularmente acho feio
		//caso 2, geramos um tile e podemos passar ele direto pro Generic Prefab

		//vou fazer o 2

		TileData tile = tiles[0];
		foreach (TileData.linha linha in tile.matriz) {

			for (int i = 0; i < linha.line.Count; i++) {
				linha.line [i] = (SpawnableObject) Random.Range (0, System.Enum.GetValues (typeof(SpawnableObject)).Length);
				//Debug.Log("obj: " + linha.line[pos_coluna]);

				//necessario tratar casos especificos: 
				// - serras em 3 linhas seguidas
				// - aranhas frequentes
				// - futuros "momentos" deverão ser implementados

			}
		}
		aux.generate (tile);

	}

	void geraTilePronto() {
		Debug.Log ("TILE PRONTO");
		TileData randomTile = tiles[Random.Range(0, tiles.Count)];
		//Debug.Log("tile escolhido: " + randomTile);
		aux.generate(randomTile);
	}
}
