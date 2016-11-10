using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Player : GridObject {

	public Transform target;
    public Transform ladderA;
	public Transform ladderB;

	void Update(){
		InputTeclado ();
        transform.position = Vector3.MoveTowards(transform.position, targetPOS, speed * Time.deltaTime);
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
        if (target.gameObject.tag == "Saw" || target.gameObject.tag == "Water"|| target.gameObject.tag == "Spider")
        {
            destroyPlayer();
        }
    }

    void destroyPlayer()
    {
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
		//Debug.DrawRay (transform.position - new Vector3(0,1,0),alvo,Color.blue,2f);
		hit = Physics2D.Raycast (transform.position - new Vector3(0,1,0), alvo, 1f,1<<LayerMask.NameToLayer("Escada"));
		if (hit.collider != null) {
			//Debug.Log (hit.collider);
			return true;
		}	
		return false;
	}
	
		
}
