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
		switch (direcao) {
		case Direction.RIGHT_UP:
			if(transform.position.x < 4 && transform.position == targetPOS) Instantiate(ladderA,transform.position + new Vector3(1,0,0), Quaternion.identity);
			break;
		case Direction.RIGHT_DOWN:
			if(transform.position.x < 4 && transform.position == targetPOS) Instantiate(ladderB,transform.position - new Vector3(-1,2,0), Quaternion.identity);
			break;
		case Direction.LEFT_UP:
			if(transform.position.x > -4 && transform.position == targetPOS) Instantiate(ladderB,transform.position - new Vector3(1,0,0), Quaternion.identity);
			break;
		case Direction.LEFT_DOWN:
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
}
