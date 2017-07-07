using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public enum PlayerKill{
	WATER,SERRA,ARANHA,LASER
}
public class Player : GridObject {

	public Transform target;
    public Transform ladderA;
    public Transform ladderB;
	public bool martelo = false, boia = false;
    public bool moved;
    public float timeMartelo = 10f;
    public float timeLeftMartelo;
	public int num_momentos; //numero de momentos existentes
	public float alt_momento; //define a altura de um momento (único para todos os momentos)
    
    private bool paused = false;
    private Vector3 vetorDirecaoAtual = new Vector3(0,0,0);
	private GameObject scoreText;
	private TileSpawner tileSpawner;
	private MadeiraManager madeiraManager;
    private SoundManager soundManager;
	private Vector3 initPos;
    private Vector3 sightStart, sightEndRU, sightEndLU, sightEndRD, sightEndLD;
    public bool spottedRU, spottedLU, spottedRD, spottedLD;

    private void Start()
    {
		moved = false;
		initPos = this.transform.position;
        timeLeftMartelo = timeMartelo;
		scoreText = GameObject.FindGameObjectWithTag("ScoreText");
		tileSpawner = GameObject.FindGameObjectWithTag("TileSpawner").GetComponent<TileSpawner>();
        madeiraManager = GameObject.FindGameObjectWithTag("MadeiraManager").GetComponent<MadeiraManager>();
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
    }


    public void pauseGame()
    {
        paused = true;
        Debug.Log("pause");
    }


    public void resumeGame()
    {
        paused = false;
        Debug.Log("resume");
    }


    void Update(){

        if (!paused)
        {
            if (!moved && transform.position != initPos)
            {
                moved = true;
                GetComponent<ReadyScreen>().BeginGame();
            }
            InputTeclado();
            transform.position = Vector3.MoveTowards(transform.position, targetPOS, speed * Time.deltaTime);
            vetorDirecaoAtual = targetPOS - transform.position;
            playerAnimation();
            if (transform.position == targetPOS)
            {
                forceIdleAnim();
            }
            hasHammer();

            checkAltura();

            raycast();
        }

        
	}

    void raycast()
    {
        sightStart = this.transform.position + new Vector3 (0, -1, 0);
        sightEndRU = sightStart + new Vector3(1f, 1f, 0);
        sightEndLU = sightStart + new Vector3(-1f, 1f, 0);
        sightEndRD = sightStart + new Vector3(1f, -1f, 0);
        sightEndLD = sightStart + new Vector3(-1f, -1f, 0);

        Debug.DrawLine(sightStart, sightEndRU, Color.magenta);
        Debug.DrawLine(sightStart, sightEndLU, Color.red);
        Debug.DrawLine(sightStart, sightEndRD, Color.blue);
        Debug.DrawLine(sightStart, sightEndLD, Color.yellow);

        spottedRU = Physics2D.Linecast(new Vector2(sightStart.x, sightStart.y), new Vector2(sightEndRU.x, sightEndRU.y), 1 << LayerMask.NameToLayer("Escada"));
        spottedLU = Physics2D.Linecast(sightStart, sightEndLU, 1 << LayerMask.NameToLayer("Escada"));
        spottedRD = Physics2D.Linecast(sightStart, sightEndRD, 1 << LayerMask.NameToLayer("Escada"));
        spottedLD = Physics2D.Linecast(sightStart, sightEndLD, 1 << LayerMask.NameToLayer("Escada"));
    }

    public bool playerAdjEscada()
    {
        int count = 0;
        if (spottedRU) { count++; }
        if (spottedLU) { count++; }
        if (spottedRD) { count++; }
        if (spottedLD) { count++; }

        Debug.Log("count: " + count);

        if (count > 1)
        {
            return true;
        }
        else return false;
    }

	private void checkAltura() {
		int momento = 0;
		float y_player = this.transform.position.y % (alt_momento * num_momentos);
		for (int i = 1; i <= num_momentos; i++) {
			float limite_atual = alt_momento * i;
			if (y_player < limite_atual) {
				momento = i;
				break;
			}
		}
		//Debug.Log ("momento: " + momento);
		tileSpawner.setMomento(momento);
	}

	public void criaEscada(Direction direcao) {
		GameObject.Find ("ScoreManager").GetComponent<ScoreText> ().addOnStair ();
        
		switch (direcao) {
		case Direction.RIGHT_UP:
			if (existeEscada (Direction.RIGHT_UP)) {
                //Debug.Log ("AconteceuRU");
                soundManager.setSoundMovPlayer();
                return;
			}
            if (transform.position.x < 4 && transform.position == targetPOS)
            {
                Instantiate(ladderA, transform.position + new Vector3(1, 0, 0), Quaternion.identity);
                madeiraManager.delMadeira();
                soundManager.setSoundMovPlayer();
            }

                break;
		case Direction.RIGHT_DOWN:
			if (existeEscada (Direction.RIGHT_DOWN)) {
                //Debug.Log ("AconteceuRD");
                soundManager.setSoundMovPlayer();
                return;
			}
            if (transform.position.x < 4 && transform.position == targetPOS)
            {
                Instantiate(ladderB, transform.position - new Vector3(-1, 2, 0), Quaternion.identity);
                madeiraManager.delMadeira();
                soundManager.setSoundMovPlayer();
            }
			break;
		case Direction.LEFT_UP:
			if (existeEscada (Direction.LEFT_UP)) {
                //Debug.Log ("AconteceuLU");
                soundManager.setSoundMovPlayer();
                return;
			}
            if (transform.position.x > -4 && transform.position == targetPOS)
            {
                Instantiate(ladderB, transform.position - new Vector3(1, 0, 0), Quaternion.identity);
                madeiraManager.delMadeira();
                soundManager.setSoundMovPlayer();
            }
			break;
		case Direction.LEFT_DOWN:
			if (existeEscada (Direction.LEFT_DOWN)) {
                //Debug.Log ("AconteceuLD");
                soundManager.setSoundMovPlayer();
                return;
			}
            if (transform.position.x > -4 && transform.position == targetPOS)
            {
                Instantiate(ladderA, transform.position - new Vector3(1, 2, 0), Quaternion.identity);
                madeiraManager.delMadeira();
                soundManager.setSoundMovPlayer();
            }
			break;
		}
	}

	public void InputTeclado(){
		
		if(Input.GetKeyDown("f")){
			criaEscada (Direction.RIGHT_UP);
            move(Direction.RIGHT_UP);
		}
		if (Input.GetKeyDown ("c")) {
			criaEscada (Direction.RIGHT_DOWN);
			move (Direction.RIGHT_DOWN);
		}
		if (Input.GetKeyDown ("s")) {
			criaEscada (Direction.LEFT_UP);
			move (Direction.LEFT_UP);
		}
		if (Input.GetKeyDown ("x")) {
			criaEscada (Direction.LEFT_DOWN);
			move (Direction.LEFT_DOWN);
		}
	}

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag == "Madeira")
        {
            madeiraManager.addMadeira();
            target.GetComponent<Coin>().destroyCoin();
        }

        if (target.gameObject.tag == "Saw")
        {
            if (martelo) target.GetComponent<Saw>().destroySaw();
            else {
				KillPlayer(PlayerKill.SERRA);
				StartCoroutine(destroyPlayer());
				}
        }

        if (target.gameObject.tag == "Laser")
        {
            StartCoroutine(destroyPlayer());
        }

        if (target.gameObject.tag == "ToggleSwitch")
        {
            target.GetComponent<ToggleSwitch>().activateToggleSwitch();
        }

        if (target.gameObject.tag == "Water")
        {
            if (boia) { ; }
            else StartCoroutine(destroyPlayer());
        }

        if (target.gameObject.tag == "Spider")
        {
            if (martelo) target.GetComponent<Spider>().destroySpiderByPlayer(transform.position.x>target.transform.position.x);
            else StartCoroutine(destroyPlayer());
        }
    }

    public void playerDed()
    {
        StartCoroutine(destroyPlayer());
    }

	//Pedro: Alguem chamava essa destroy player? Pq ela era publica? 
    public IEnumerator destroyPlayer()
    {
        scoreText.GetComponent<ScoreText>().setHighscore();
		GameObject.Find("Transition_Mask").GetComponent<TransitionMask>().Bigger_transition();
		yield return new WaitForSeconds(1);
        SceneManager.LoadScene("GameOver");
    }
	private void KillPlayer(PlayerKill modo){
		switch(modo){
			case PlayerKill.ARANHA:

			case PlayerKill.LASER:
				//Fumaça
			case PlayerKill.SERRA:
			case PlayerKill.WATER:
				//Afunda na agua
			default:
				break;
		}
	}
	bool existeEscada(Direction direcao){
		RaycastHit2D hit;
		Vector3 alvo = new Vector3(0,0,0);

		switch (direcao) {
		case Direction.RIGHT_UP:
			alvo = new Vector3 (1, 1, 0);
			break;
		case Direction.RIGHT_DOWN:
			alvo = new Vector3 (1, -1, 0);
			break;
		case Direction.LEFT_UP:
			alvo = new Vector3 (-1, 1, 0);
			break;
		case Direction.LEFT_DOWN:
			alvo = new Vector3 (-1, -1, 0);
			break;
		}
		Debug.DrawRay (transform.position - new Vector3(0,1,0),alvo,Color.blue,2f);
		hit = Physics2D.Raycast (transform.position - new Vector3(0,1,0), alvo, 1f,1<<LayerMask.NameToLayer("Escada"));
		if (hit.collider != null) {
			//Debug.Log (hit.collider);
			return true;
		}	
		return false;
	}

	void playerAnimation(){
		if(martelo){
			this.gameObject.GetComponent<Animator> ().SetBool ("hammer",true);
		}else{
			this.gameObject.GetComponent<Animator> ().SetBool ("hammer",false);
		}
		if (vetorDirecaoAtual.x < 0) {
			forceIdleAnim ();
			this.gameObject.GetComponent<Animator> ().SetBool ("move",true);
			this.gameObject.GetComponent<Animator> ().SetBool ("right",false);
		}else if (vetorDirecaoAtual.x > 0) {
			forceIdleAnim ();
			this.gameObject.GetComponent<Animator> ().SetBool ("move",true);
			this.gameObject.GetComponent<Animator> ().SetBool ("right",true);
		}else if(vetorDirecaoAtual.magnitude == 0){
			forceIdleAnim ();
		}
	}

	void forceIdleAnim(){
		transform.gameObject.GetComponent<Animator> ().SetBool ("move", false);
	}	

    public void setHammer()
    {
        martelo = true;
        timeLeftMartelo = timeMartelo;
    }
 		
    void hasHammer()
    {
        if(martelo)
        {
            timeLeftMartelo -= Time.deltaTime; //-1 por segundo e nao por frame
            if (timeLeftMartelo < 0) {
                martelo = false; timeLeftMartelo = timeMartelo;
            }
        }
    }
}
