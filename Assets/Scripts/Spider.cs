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
        
        /*
        possibleDirections.Add(Direction.RIGHT_DOWN);
        possibleDirections.Add(Direction.LEFT_DOWN);
        possibleDirections.Add(Direction.RIGHT_UP);
        possibleDirections.Add(Direction.LEFT_UP);

        if (player.transform.position.y < this.transform.position.y)
        {
            for (int i = 0; i < 3; i++)
            {
                possibleDirections.Add(Direction.LEFT_DOWN);
                possibleDirections.Add(Direction.RIGHT_DOWN);
            }
        }
        //player above spider
        else
        {
            for (int i = 0; i < 3; i++)
            {
                possibleDirections.Add(Direction.RIGHT_UP);
                possibleDirections.Add(Direction.LEFT_UP);
            }
        }
        */
        return possibleDirections[Random.Range(0, possibleDirections.Count)];
    }
}
