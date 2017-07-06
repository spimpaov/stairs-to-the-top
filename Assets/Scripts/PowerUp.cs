using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour {

    public Transform sightStart, sightEndXY, sightEnd_XY, sightEndX_Y, sightEnd_X_Y;

    private bool spottedXY = false, spotted_XY = false, spottedX_Y = false, spotted_X_Y = false;
    //private GameObject HUD;
    //private GameObject score;
    private GameObject player;
	private GameObject text_sucesso;


    void Start()
    {
        //HUD = GameObject.FindGameObjectWithTag("HUD");
        //score = GameObject.FindGameObjectWithTag("Score");
        player = GameObject.FindGameObjectWithTag("Player");
		text_sucesso = GameObject.FindGameObjectWithTag ("TextoPOColetado");
    }

    void Update ()
    {
        raycast();
        collectPowerUp();
	}

    void destroyPowerUp()
    {
        Destroy(this.gameObject);
    }

    void raycast()
    {
        Debug.DrawLine(sightStart.position, sightEndXY.position, Color.magenta);
        Debug.DrawLine(sightStart.position, sightEnd_XY.position, Color.red);
        Debug.DrawLine(sightStart.position, sightEndX_Y.position, Color.blue);
        Debug.DrawLine(sightStart.position, sightEnd_X_Y.position, Color.yellow);

        spottedXY = Physics2D.Linecast(sightStart.position, sightEndXY.position, 1 << LayerMask.NameToLayer("Escada"));
        spotted_XY = Physics2D.Linecast(sightStart.position, sightEnd_XY.position, 1 << LayerMask.NameToLayer("Escada"));
        spottedX_Y = Physics2D.Linecast(sightStart.position, sightEndX_Y.position, 1 << LayerMask.NameToLayer("Escada"));
        spotted_X_Y = Physics2D.Linecast(sightStart.position, sightEnd_X_Y.position, 1 << LayerMask.NameToLayer("Escada"));
    }

    void collectPowerUp()
    {
        if(spottedXY && spottedX_Y && spotted_XY && spotted_X_Y)
        {
			StartCoroutine (POColetado());
            /*
            GameObject temp = Instantiate(powerUpColetado.gameObject);
            temp.transform.localPosition = new Vector3(0, score.transform.position.y- relacaoScoreToPowerUpText, 0);
            temp.transform.SetParent(HUD.transform, false);
            */
            player.GetComponent<Player>().setHammer();
            
        }
    }

	IEnumerator POColetado() {
			text_sucesso.GetComponent<Text> ().enabled = true;
			yield return new WaitForSeconds (1f);
			text_sucesso.GetComponent<Text>().enabled = false;

		destroyPowerUp();
	
	}
}
