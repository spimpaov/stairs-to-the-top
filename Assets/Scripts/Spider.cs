using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spider : GridObject {

    private float timeBetweenMovements = 1.0f;
    private Player player;
    private GameObject arrow;
    [SerializeField] private float arrowDistance;
    public bool atePlayer = false;
    public Spider spider;

	void Start () {
        arrow = transform.GetChild(1).gameObject;
        GetComponentInChildren<SpriteRenderer>().color = GetColor(Random.Range(1,6));
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        targetPOS = transform.position;
        StartCoroutine(randomBehaviour());
	}

    void Update()
    {
        if(!atePlayer) transform.position = Vector3.MoveTowards(transform.position, targetPOS, speed * Time.deltaTime);
    }
    private Color GetColor(int number){
        switch(number){
            case 1:
                return new Color32(175, 60, 60, 255);
            case 2:
                return new Color32(90, 180, 10, 255);
            case 3:
                return new Color32(230, 230, 85, 255);
            case 4:
                return new Color32(100, 200, 250, 255);
            case 5:
                return new Color32(240, 130, 250, 255);
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
/*
    void raycast()
    {
        Vector3 sightStart = this.transform.position;
        Vector3 sightEnd = sightStart + ray_dir;

        Debug.DrawLine(sightStart, sightEnd, Color.cyan);
    }
*/
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
        //if(target.gameObject.tag == "Saw") { destroySpiderByPlayer(true); } //chuva de aranha morta
        if (target.gameObject.tag == "Water") { destroySpiderByPlayer(true); }; //agua esta sempre abaixo do player, nao cai aranha do ceu assim
        //if (target.gameObject.tag == "Laser") { destroySpiderByPlayer(true); };

    }

    public void destroySpider()
    {
        Destroy(this.gameObject,4f);
    }
    public void destroySpiderByPlayer(bool right)
    {
        GetComponent<Collider2D>().enabled = false;
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
    public void SpiderAtePlayer()
    {
        atePlayer = true;
        StopCoroutine(randomBehaviour());
        GetComponentInChildren<Animator>().Play("spider-eating");
    }
}
