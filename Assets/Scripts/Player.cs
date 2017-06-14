using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Player : GridObject {

	public Transform target;
    public Transform ladderA;
    public Transform ladderB;
	public bool martelo = false, boia = false;
    public float timeMartelo = 10f;
    public float timeLeftMartelo;
	public int num_momentos; //numero de momentos existentes
	public float alt_momento; //define a altura de um momento (único para todos os momentos)
    
    private Vector3 vetorDirecaoAtual = new Vector3(0,0,0);
	private GameObject scoreText;
	private TileSpawner tileSpawner;

    private void Start()
    {
        timeLeftMartelo = timeMartelo;
		scoreText = GameObject.FindGameObjectWithTag("ScoreText");
		tileSpawner = GameObject.FindGameObjectWithTag("TileSpawner").GetComponent<TileSpawner>();
    }

    void Update(){
        InputTeclado();
        transform.position = Vector3.MoveTowards(transform.position, targetPOS, speed * Time.deltaTime);
		vetorDirecaoAtual = targetPOS - transform.position;
		playerAnimation ();
		if (transform.position == targetPOS) {
			forceIdleAnim ();
		}
        hasHammer();

		checkAltura();
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
				return;
			}
			if(transform.position.x < 4 && transform.position == targetPOS) Instantiate(ladderA,transform.position + new Vector3(1,0,0), Quaternion.identity);
			break;
		case Direction.RIGHT_DOWN:
			if (existeEscada (Direction.RIGHT_DOWN)) {
				//Debug.Log ("AconteceuRD");
				return;
			}
			if(transform.position.x < 4 && transform.position == targetPOS) Instantiate(ladderB,transform.position - new Vector3(-1,2,0), Quaternion.identity);
			break;
		case Direction.LEFT_UP:
			if (existeEscada (Direction.LEFT_UP)) {
				//Debug.Log ("AconteceuLU");
				return;
			}
			if(transform.position.x > -4 && transform.position == targetPOS) Instantiate(ladderB,transform.position - new Vector3(1,0,0), Quaternion.identity);
			break;
		case Direction.LEFT_DOWN:
			if (existeEscada (Direction.LEFT_DOWN)) {
				//Debug.Log ("AconteceuLD");
				return;
			}
			if(transform.position.x > -4 && transform.position == targetPOS) Instantiate(ladderA,transform.position - new Vector3(1,2,0), Quaternion.identity);
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
        if (target.gameObject.tag == "Coin")
        {
            target.GetComponent<Coin>().destroyCoin();
        }

        if (target.gameObject.tag == "Saw")
        {
            if (martelo) target.GetComponent<Saw>().destroySaw();
            else destroyPlayer();
        }

        if (target.gameObject.tag == "Laser")
        {
            destroyPlayer();
        }

        if (target.gameObject.tag == "Water")
        {
            if (boia) { ; }
            else destroyPlayer();
        }

        if (target.gameObject.tag == "Spider")
        {
            if (martelo) target.GetComponent<Spider>().destroySpider();
            else destroyPlayer();
        }
    }

    public void destroyPlayer()
    {
        scoreText.GetComponent<ScoreText>().setHighscore();
        SceneManager.LoadScene("GameOver");
        //Destroy(this.gameObject);
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
