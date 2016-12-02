using UnityEngine;
using System;
using System.Collections.Generic;


public class Spawner : MonoBehaviour {

    private enum Spawnable {
        NONE, COIN, SPIDER, SAW
    };

    public Coin coin;
    public Saw saw;
    public Spider spider;
    public Player player;
    public int offsetPlayer = -1;
    public int offsetLinha = 21;
    public int par_primeiroPto = -4;
    public int par_segundoPto = 0;
    public int par_terceiroPto = 4;
    public int impar_primeiroPto = -2;
    public int impar_segundoPto = 2;


    private int[] Linhas;
    private int linhaAtual;
    private int linhaSpawn;
    private int alturaAtualPlayer;


    void Start()
    {
        Linhas = new int[50];
    }

	void Update () {

        spawnProximaLinha();

    }

    void createSpawnable(Spawnable spawnable, Vector3 position)
    {

        if (spawnable == Spawnable.NONE) { return; }
        else if (spawnable == Spawnable.COIN) { Instantiate(coin, position, Quaternion.identity); }
        else if (spawnable == Spawnable.SAW) { Instantiate(saw, position, Quaternion.identity); }
        else if (spawnable == Spawnable.SPIDER) { Instantiate(spider, position, Quaternion.identity); }
    }

    void calculaLinhaSpawn()
    {
        alturaAtualPlayer = (int) player.transform.position.y; //supondo que player ande por numeros inteiros (o que atualmente ocorre)
        linhaAtual = alturaAtualPlayer / 2;
        linhaSpawn = linhaAtual + offsetLinha;
    }

    void spawnProximaLinha()
    {
        calculaLinhaSpawn();

        if(linhaSpawn >= Linhas.Length ) //excedeu o tamanho limite
        {
            Array.Resize(ref Linhas, Linhas.Length * 2);
        }

        if (Linhas[linhaSpawn] != 1) //linha ainda não spawnada
        {
            if(linhaAtual %2 == 0) //3 pontos em x (par)
            {
                int pos_x = UnityEngine.Random.Range(0, 3);
                if (pos_x == 0) { pos_x = par_primeiroPto; }
                else if (pos_x == 1) { pos_x = par_segundoPto; }
                else { pos_x = par_terceiroPto; }

                List<Spawnable> possibleSpawns = new List<Spawnable>();
                possibleSpawns.Add(Spawnable.SPIDER);
                possibleSpawns.Add(Spawnable.NONE);
                possibleSpawns.Add(Spawnable.NONE);
                possibleSpawns.Add(Spawnable.NONE);
                possibleSpawns.Add(Spawnable.COIN);
                possibleSpawns.Add(Spawnable.COIN);
                possibleSpawns.Add(Spawnable.COIN);
                possibleSpawns.Add(Spawnable.COIN);
                possibleSpawns.Add(Spawnable.SAW);
                possibleSpawns.Add(Spawnable.SAW);
                possibleSpawns.Add(Spawnable.SAW);
                possibleSpawns.Add(Spawnable.SAW);

                int spawnable = UnityEngine.Random.Range(0, possibleSpawns.Count);

                createSpawnable((Spawnable)spawnable, new Vector3(pos_x, alturaAtualPlayer + offsetLinha -1, 0));

            }
            else //2 pontos em x (impar)
            {
                int pos_x = UnityEngine.Random.Range(0, 2);
                if(pos_x == 0) { pos_x = impar_primeiroPto; }
                else{ pos_x = impar_segundoPto; }

                List<Spawnable> possibleSpawns = new List<Spawnable>();
                possibleSpawns.Add(Spawnable.SPIDER);
                possibleSpawns.Add(Spawnable.NONE);
                possibleSpawns.Add(Spawnable.NONE);
                possibleSpawns.Add(Spawnable.NONE);
                possibleSpawns.Add(Spawnable.COIN);
                possibleSpawns.Add(Spawnable.COIN);
                possibleSpawns.Add(Spawnable.COIN);
                possibleSpawns.Add(Spawnable.COIN);
                possibleSpawns.Add(Spawnable.SAW);
                possibleSpawns.Add(Spawnable.SAW);
                possibleSpawns.Add(Spawnable.SAW);
                possibleSpawns.Add(Spawnable.SAW);

                int spawnable = UnityEngine.Random.Range(0, possibleSpawns.Count);

                createSpawnable((Spawnable)spawnable, new Vector3(pos_x, alturaAtualPlayer + offsetLinha -1, 0));

            }

            Linhas[linhaSpawn] = 1; //linha spawnada
        }
    }

}
