using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericPrefab : MonoBehaviour {
    
    public Coin coin;
    public Saw saw;
    public Spider spider;

	private GameObject genericObj;
    private int capacidadeTile;
    private Vector3 pos;
    private float alturaTile;
    private static int iterationTile;

    private void Start()
    {
        iterationTile = 0;
    }

	static int currentTile = 0;

    //identifica quais sao os objetos no tile e spawna cada um
    public void generate(TileData tile)
    {
        //itera por cada posicao do tile
        //identifica o que há em cada posicao
        //instancia o objeto naquele ponto

        capacidadeTile = tile.matriz.Capacity;
        alturaTile = (float) capacidadeTile * 2;

		genericObj = new GameObject();
		genericObj.name = "Tile " + currentTile++;
		genericObj.transform.position = new Vector3 (0, calculaAlturaRelativaTile(), 0);
		iterationTile++;

        foreach (TileData.linha linha in tile.matriz) {
            int pos_linha = tile.matriz.IndexOf(linha);
			//Debug.Log("pos_linha: " + pos_linha);

			for (int i = 0; i < linha.line.Count; i++) {
                int pos_coluna = i;
				SpawnableObject so = linha.line [pos_coluna];
                //Debug.Log("* pos_coluna: " + pos_coluna);

                switch (so) {
                    case SpawnableObject.NADA:
                        break;
                    case SpawnableObject.COIN:
                        pos = calculaPos(pos_linha, pos_coluna);
                        instantiateCoin(pos);
                        break;
                    case SpawnableObject.SAW:
                        pos = calculaPos(pos_linha, pos_coluna);
                        instantiateSaw(pos);
                        break;
                    case SpawnableObject.SPIDER:
                        pos = calculaPos(pos_linha, pos_coluna);
                        instantiateSpider(pos);
                        break;
                }
            }
        }
    }

    //calcular a posição do objeto na grid de acordo com a posição dele dentro do tile 
    //calcular a posição de spawn do tile no mapa do jogo (ex. 20 m acima da posição atual do player)
    //tile é um objeto instanciável que contem objetos instanciaveis (Spawnables) ou é apenas uma posição relativa?

    private Vector3 calculaPos(int pos_linha, int pos_coluna)
    {
        float y;
        float x =-10;
        if (pos_linha%2 == 0) { //impar -> linha[3]
            switch (pos_coluna)
            {
                case 0:
                    x = -4;
                    break;
                case 1:
                    x = 0;
                    break;
                case 2:
                    x = +4;
                    break;
            }
        }
        else { //par -> linha[2]
            switch (pos_coluna)
            {
                case 0:
                    x = -2;
                    break;
                case 1:
                    x = +2;
                    break;
            }
        }
        y = (capacidadeTile - pos_linha ) * 2;
        return new Vector3(x, y, 0);
    }

    private float calculaAlturaRelativaTile()
    {
        float temp = alturaTile * iterationTile;
        return temp;
    }

    private void instantiateCoin(Vector3 pos)
    {
		GameObject childObj = Instantiate (coin.gameObject);
		childObj.transform.parent = genericObj.transform;
		childObj.transform.localPosition = pos;

        //Instantiate(coin, pos, Quaternion.identity);
    }

    private void instantiateSpider(Vector3 pos)
    {
		GameObject childObj = Instantiate (spider.gameObject);
		childObj.transform.parent = genericObj.transform;
		childObj.transform.localPosition = pos;

        //Instantiate(spider, pos, Quaternion.identity);
    }

    private void instantiateSaw(Vector3 pos)
    {
		GameObject childObj = Instantiate (saw.gameObject);
		childObj.transform.parent = genericObj.transform;
		childObj.transform.localPosition = pos;
        
		//Instantiate(saw, pos, Quaternion.identity);
    }
}
