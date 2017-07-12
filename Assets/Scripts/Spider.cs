using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spider : GridObject {

    float timeBetweenMovements = 1.0f;
    Player player;

	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        targetPOS = transform.position;
        StartCoroutine(randomBehaviour());
	}

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPOS, speed * Time.deltaTime);
    }

    IEnumerator randomBehaviour()
    {
        while (true)
        {
            move(getPlayerGeneralDirection());
            yield return new WaitForSeconds(timeBetweenMovements);
        }
    }

    Direction getPlayerGeneralDirection()
    {
        List<Direction> possibleDirections = new List<Direction>();

        //player below spider
        if (player.transform.position.y < this.transform.position.y)
        {
            possibleDirections.Add(Direction.LEFT_DOWN);
            possibleDirections.Add(Direction.RIGHT_DOWN);
        }
        //player above spider
        else 
        {
            possibleDirections.Add(Direction.RIGHT_UP);
            possibleDirections.Add(Direction.LEFT_UP);
        }
        return possibleDirections[Random.Range(0, possibleDirections.Count)];
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.gameObject.tag == "Saw") { destroySpider(); }
    }

    public void destroySpider()
    {
        Destroy(this.gameObject,4f);
    }
    public void destroySpiderByPlayer(bool right)
    {
        GetComponent<Rigidbody2D>().gravityScale = 5;
        if(!right){
            GetComponent<Rigidbody2D>().AddForce(Vector3.up*600f+Vector3.right*200f);
        }else{
            GetComponent<Rigidbody2D>().AddForce(Vector3.up*600f+Vector3.left*200f);
        }
        
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponentInChildren<Animator>().Play("spider-dead");
        GetComponentInChildren<SpriteRenderer>().flipY = true;
        Destroy(this.gameObject,4f);
        GetComponent<Spider>().enabled = false;
    }
}
