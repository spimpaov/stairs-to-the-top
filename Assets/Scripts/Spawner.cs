using UnityEngine;
using System;
using System.Collections.Generic;


public class Spawner : MonoBehaviour {

    private enum Spawnable {
        NONE = 1, COIN, SPIDER, SAW
    };

    public Coin coin;
    public Saw saw;
    public Spider spider;
    public Player player;
    public PowerUp powerUp;
    public int OffsetEmYItem = 16; //tem que ser numero multiplo de 4 (altura de um losango de escadas)
    public int par_primeiroPto = -4;
    public int par_segundoPto = 0;
    public int par_terceiroPto = 4;
    public int impar_primeiroPto = -2;
    public int impar_segundoPto = 2;
    public int rangePowerUp = 4; //quantas linhas apos o offset entram no random de quem vai spawnar power up

    private int offsetEmYPowerUp; //quantas linhas serão puladas em relaçao a linha atual em que o player está pra começar a spawnar mais power up (tb tem que ser multiplo de 4)
    private int[] Linhas;
    private int linhaSpawnItem;
    private int linhaSpawnPowerUp;
    private int alturaAtualPlayer;
    private int ultimaLinhaSpawnPowerUp = 0;
    private int spawnable;
    private int linhaSpawnItemComOffset;
    private int linhaSpawnPowerUpComOffset;
    private Spawnable[] arrayItens;


    void Start()
    {
        Linhas = new int[50];
        offsetEmYPowerUp = OffsetEmYItem/2;
        arrayItens = new Spawnable[13] { Spawnable.NONE, Spawnable.NONE, Spawnable.NONE,
                                        Spawnable.COIN, Spawnable.COIN, Spawnable.COIN, Spawnable.COIN, Spawnable.COIN,
                                        Spawnable.SPIDER,
                                        Spawnable.SAW, Spawnable.SAW, Spawnable.SAW, Spawnable.SAW };
    }

	void Update () {

        spawnProximaLinha();

    }

    void createSpawnable(Vector3 position)
    {
        Spawnable item = arrayItens[UnityEngine.Random.Range(0, arrayItens.Length)];
        if (item == Spawnable.COIN) {
            Instantiate(coin, position, Quaternion.identity);
            spawnable = 2;
        }
        else if (item == Spawnable.SPIDER) {
            Instantiate(spider, position, Quaternion.identity);
            spawnable = 3;
        }
        else if (item == Spawnable.SAW) { 
            if (linhaSpawnItem != 0 && linhaSpawnItem != 1 && Linhas[linhaSpawnItem - 2] != 4) {
                Instantiate(saw, position, Quaternion.identity);
                spawnable = 4; ;
            }
        }
        else /* Spawnable.NONE */ spawnable = 1;
    }

    void calculaLinhaSpawn()
    {
        alturaAtualPlayer = (int) player.transform.position.y; //supondo que player ande por numeros inteiros (o que atualmente ocorre)
        linhaSpawnItem = alturaAtualPlayer /2; //transformando a contagem das linhas em 0, 1, 2, 3 ... (player anda de 2 em 2 em y)
        linhaSpawnItemComOffset = alturaAtualPlayer + OffsetEmYItem - 1;
    }

    void spawnProximaLinha()
    {
        //calcula proxima linha de spawn pros itens
        calculaLinhaSpawn();

        //se excedeu o tamanho limite do array das linhas
        if (linhaSpawnItem >= Linhas.Length) { Array.Resize(ref Linhas, Linhas.Length * 2); }

        if (Linhas[linhaSpawnItem] == 0) //linha ainda não spawnada
        {
            if(linhaSpawnItem % 2 == 0) //3 pontos em x (linha par em Linhas[])
            {
                int pos_x = UnityEngine.Random.Range(0, 3);
                if (pos_x == 0) { pos_x = par_primeiroPto; }
                else if (pos_x == 1) { pos_x = par_segundoPto; }
                else { pos_x = par_terceiroPto; }
                createSpawnable(new Vector3(pos_x, linhaSpawnItemComOffset, 0));
            }
            else //2 pontos em x (linha impar em Linhas[])
            {
                int pos_x = UnityEngine.Random.Range(0, 2);
                if(pos_x == 0) { pos_x = impar_primeiroPto; }
                else{ pos_x = impar_segundoPto; }
                createSpawnable(new Vector3(pos_x, linhaSpawnItemComOffset, 0));
            }
            Linhas[linhaSpawnItem] = spawnable;
        }

        //spawna power up
        if (linhaSpawnItem % offsetEmYPowerUp == 0 && linhaSpawnItem != ultimaLinhaSpawnPowerUp) { spawnPowerUp(); }
    }

    void spawnPowerUp()
    {
        //linha onde o proximo power up será spawnado
        linhaSpawnPowerUp = UnityEngine.Random.Range(linhaSpawnItem + offsetEmYPowerUp + 1, linhaSpawnItem + offsetEmYPowerUp + rangePowerUp + 1); //+1 pq range(1,3) retorna 1 ou 2

        int ajusteMisterioso;
        if (linhaSpawnPowerUp%2 == 0) { ajusteMisterioso = linhaSpawnItem - offsetEmYPowerUp / 2 + 1; }
        else { ajusteMisterioso = linhaSpawnItem - offsetEmYPowerUp / 2; }
        

        if (Linhas[ajusteMisterioso] != 4 && Linhas[ajusteMisterioso + 1] != 4 && Linhas[ajusteMisterioso - 1] != 4)
        {
            if (linhaSpawnPowerUp % 2 == 0) //3 pontos em x (par)
            {
                int pos_x = 0; //power up nao pode spawnar no canto da tela
                Instantiate(powerUp, new Vector3(pos_x, alturaAtualPlayer + offsetEmYPowerUp + 1, 0), Quaternion.identity);
            }
            else //2 pontos em x (impar)
            {
                int pos_x = UnityEngine.Random.Range(0, 2);
                if (pos_x == 0) { pos_x = impar_primeiroPto; }
                else { pos_x = impar_segundoPto; }
                Instantiate(powerUp, new Vector3(pos_x, alturaAtualPlayer + offsetEmYPowerUp - 1, 0), Quaternion.identity);
            }
        }
        ultimaLinhaSpawnPowerUp = linhaSpawnItem;
        
    }

}
