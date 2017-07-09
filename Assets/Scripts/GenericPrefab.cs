using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericPrefab : MonoBehaviour {
    
    public Coin coin;
    public Saw saw;
	public Spider spider;
	public PowerUp powerUp;
	public ToggleSwitch toggle_switch;
	public Laser laser;
    public Cupim cupim;

    public static int iterationTile;

	private GameObject genericObj;
    private int capacidadeTile;
    private Vector3 pos;
    private float alturaTile;
    private SpawnableObject curr_laser;
    private Vector3 curr_laser_pos;
    private SpawnableObject curr_toggle_switch;

    private void Start()
    {
        iterationTile = 2;
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


        //procura pelo laser
        foreach (TileData.linha linha in tile.matriz)
        {
            int pos_linha = tile.matriz.IndexOf(linha);
            for (int i = 0; i < linha.line.Count; i++)
            {
                int pos_coluna = i;
                SpawnableObject so = linha.line[pos_coluna];
                switch (so)
                {
                    case SpawnableObject.LASER:
                        curr_laser = so;
                        curr_laser_pos = calculaPos(pos_linha, pos_coluna);
                        break;
                }
            }
        }

        //procura pelo toggle_switch
        foreach (TileData.linha linha in tile.matriz)
        {
            int pos_linha = tile.matriz.IndexOf(linha);
            for (int i = 0; i < linha.line.Count; i++)
            {
                int pos_coluna = i;
                SpawnableObject so = linha.line[pos_coluna];
                switch (so)
                {
                    case SpawnableObject.TOGGLE_SWITCH:
                        pos = calculaPos(pos_linha, pos_coluna);
                        instantiateToggleSwitch(pos, curr_laser_pos);
                        break;
                }
            }
        }
        
        foreach (TileData.linha linha in tile.matriz) {
			int pos_linha = tile.matriz.IndexOf (linha);
			//Debug.Log("pos_linha: " + pos_linha);

			for (int i = 0; i < linha.line.Count; i++) {
				int pos_coluna = i;

				//geração de power up
				/*
				if (linha.hasPowerUp) {
					Debug.Log ("criei um power up");
					pos = calculaPos (pos_linha, pos_coluna);
					instantiatePowerUp (pos);
				}
				*/

				SpawnableObject so = linha.line [pos_coluna];
				//Debug.Log("* pos_coluna: " + pos_coluna);

				switch (so) {
				case SpawnableObject.NADA:
					break;
				case SpawnableObject.COIN:
					pos = calculaPos (pos_linha, pos_coluna);
					instantiateCoin (pos);
					break;
				case SpawnableObject.SAW:
					pos = calculaPos (pos_linha, pos_coluna);
					instantiateSaw (pos);
					break;
				case SpawnableObject.SPIDER:
					pos = calculaPos (pos_linha, pos_coluna);
					instantiateSpider (pos);
					break;
                case SpawnableObject.CUPIM:
                    pos = calculaPos(pos_linha, pos_coluna);
                    if (pos.x != 4) {
                        instantiateCupim(new Vector3 (pos.x + 0.8f, pos.y - 2, 0));
                    }
                    break;
				case SpawnableObject.POWER_UP:
					pos = calculaPos (pos_linha, pos_coluna);
					instantiatePowerUp (new Vector3 (pos.x, pos.y -3, 0));
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

	private void instantiatePowerUp(Vector3 pos) {
		GameObject childObj = Instantiate (powerUp.gameObject);
		childObj.transform.parent = genericObj.transform;
		childObj.transform.localPosition = pos;
	}

    private void instantiateCupim(Vector3 pos)
    {
        GameObject childObj = Instantiate(cupim.gameObject);
        childObj.transform.parent = genericObj.transform;
        childObj.transform.localPosition = pos;
    }

    private void instantiateToggleSwitch(Vector3 toggle_switch_pos, Vector3 laser_pos)
    {
        GameObject childObj = Instantiate(toggle_switch.gameObject);
        childObj.transform.parent = genericObj.transform;
        childObj.transform.localPosition = toggle_switch_pos;
        GameObject laserObj = instantiateLaser(laser_pos);
        childObj.gameObject.GetComponent<ToggleSwitch>().laser = laserObj.GetComponent<Laser>();
    }

    private GameObject instantiateLaser(Vector3 pos)
    {
        GameObject childObj = Instantiate(laser.gameObject);
        childObj.transform.parent = genericObj.transform;
        childObj.transform.localPosition = pos + Vector3.left*5f;//editado por mcPedrinho123
        return childObj;
    }
}
