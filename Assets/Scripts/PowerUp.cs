using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PowerUpType{
    MARTELO,
    CHAVE,
    INSETICIDA
}
public class PowerUp : MonoBehaviour {

    public Transform sightStart, sightEndXY, sightEnd_XY, sightEndX_Y, sightEnd_X_Y;

    public bool spottedXY = false, spotted_XY = false, spottedX_Y = false, spotted_X_Y = false;
    //private GameObject HUD;
    //private GameObject score;
    private GameObject player;
	private GameObject text_sucesso;
    private PowerUpType type;

    [SerializeField] float speed,time;
    private bool up;

    void Start()
    {
        type = GetRandomPowerUpType();
        GetBoxLabel();
        //HUD = GameObject.FindGameObjectWithTag("HUD");
        //score = GameObject.FindGameObjectWithTag("Score");
        player = GameObject.FindGameObjectWithTag("Player");
		text_sucesso = GameObject.FindGameObjectWithTag ("TextoPOColetado");

        StartCoroutine( UpAndDown() );
        StartCoroutine( UpState() );
    }

    void Update ()
    {
        raycast();
        collectPowerUp();
	}
    private PowerUpType GetRandomPowerUpType(){
        int randomNumber = Random.Range(0,3);
        switch(randomNumber){
            case 0:
                return PowerUpType.MARTELO;
            case 1:
                return PowerUpType.CHAVE;
            case 2:
                return PowerUpType.INSETICIDA;
        }
        return PowerUpType.INSETICIDA;
    }

    void destroyPowerUp()
    {
        Destroy(this.gameObject);
    }
    private void GetBoxLabel(){
        switch(type){
                case PowerUpType.CHAVE:
                    GameObject.Find("Chav").SetActive(true);
                    GameObject.Find("Mart").SetActive(false);
                    GameObject.Find("Inset").SetActive(false);
                    break;
                case PowerUpType.INSETICIDA:
                    GameObject.Find("Chav").SetActive(false);
                    GameObject.Find("Mart").SetActive(false);
                    GameObject.Find("Inset").SetActive(true);
                    break;
                case PowerUpType.MARTELO:
                    GameObject.Find("Chav").SetActive(false);
                    GameObject.Find("Mart").SetActive(true);
                    GameObject.Find("Inset").SetActive(false);
                    break;
            }
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
            switch(type){
                case PowerUpType.CHAVE:
                    player.GetComponent<Player>().setHammer();
                    break;
                case PowerUpType.INSETICIDA:
                    player.GetComponent<Player>().setHammer();
                    break;
                case PowerUpType.MARTELO:
                    player.GetComponent<Player>().setHammer();
                    break;
            }
        }
    }

	IEnumerator POColetado() {
        GetComponent<Animator>().Play("power-up_activate");
		yield return new WaitForSeconds (1f);
        StartCoroutine( Smaller() );

    }

    private IEnumerator Smaller(){
        while(transform.localScale.x > 0.05f){
            transform.localScale = transform.localScale*0.9f;
            yield return new WaitForEndOfFrame();
        }
        Destroy(this.gameObject);
    }
    private IEnumerator UpAndDown(){
        float direction = 1;

        while(true){
            if(up){direction = 1;}
            else{direction = -1;}
            transform.position = transform.position + Vector3.up*direction*speed*Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        
    }
    private IEnumerator UpState(){
        while(true){
            yield return new WaitForSeconds(time);
            up = !up;
        }
    }
}
