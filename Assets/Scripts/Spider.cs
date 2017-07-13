using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spider : GridObject {

    private float timeBetweenMovements = 1.0f;
    private Player player;
    private GameObject arrow;
    [SerializeField] private float arrowDistance;

	void Start () {
        arrow = transform.GetChild(1).gameObject;
        GetComponentInChildren<SpriteRenderer>().color = GetColor(Random.Range(1,5));
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        targetPOS = transform.position;
        StartCoroutine(randomBehaviour());
	}

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPOS, speed * Time.deltaTime);
    }
    private Color GetColor(int number){
        switch(number){
            case 1:
                return Color.red;
            case 2:
                return Color.green;
            case 3:
                return Color.yellow;
            case 4:
                return Color.blue;
            default:
                return Color.white;
        }
    }
    IEnumerator randomBehaviour()
    {
        while (true)
        {
            Direction dir = getPlayerGeneralDirection();
            ChangeArrowDirection(dir);

            yield return new WaitForSeconds(timeBetweenMovements);

            move(dir);
        }
    }
    void ChangeArrowDirection(Direction dir){
        SpriteRenderer arrowSR = arrow.GetComponent<SpriteRenderer>();

        switch(dir){
            case Direction.LEFT_DOWN:
                arrowSR.flipX = false;
                arrowSR.flipY = false;
                arrow.transform.localPosition = (Vector3.down+Vector3.left).normalized*arrowDistance;
                break;
            case Direction.LEFT_UP:
                arrowSR.flipX = false;
                arrowSR.flipY = true;
                arrow.transform.localPosition = (Vector3.up+Vector3.left).normalized*arrowDistance;
                break;
            case Direction.RIGHT_DOWN:
                arrowSR.flipX = true;
                arrowSR.flipY = false;
                arrow.transform.localPosition = (Vector3.down+Vector3.right).normalized*arrowDistance;
                break;
            case Direction.RIGHT_UP:
                arrowSR.flipX = true;
                arrowSR.flipY = true;
                arrow.transform.localPosition = (Vector3.up+Vector3.right).normalized*arrowDistance;
                break;
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
