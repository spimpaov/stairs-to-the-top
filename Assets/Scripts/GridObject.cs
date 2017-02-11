using UnityEngine;
using System.Collections;

public enum Direction {NONE, RIGHT_UP, LEFT_UP, RIGHT_DOWN, LEFT_DOWN};

//classe pai de todos os objetos que tem de se movimentar na grade
public class GridObject : MonoBehaviour {

    protected Vector3 targetPOS;
    protected int ladderSize = 2;
    protected float speed = 18f;

    //todos os gameObjects que se movem na grid tem que ter a linha abaixo no update pq ta bagunçado
    //transform.position = Vector3.MoveTowards(transform.position, targetPOS, speed * Time.deltaTime);

    public void move(Direction direcao)
    {
        switch (direcao)
        {
            case Direction.RIGHT_UP:
                if (transform.position.x < 4 && transform.position == targetPOS)
                {
                    targetPOS += new Vector3(ladderSize, ladderSize, 0);
                }
                break;
            case Direction.RIGHT_DOWN:
                if (transform.position.x < 4 && transform.position == targetPOS)
                {
                    targetPOS += new Vector3(ladderSize, -ladderSize, 0);
                }
                break;
            case Direction.LEFT_UP:
                if (transform.position.x > -4 && transform.position == targetPOS)
                {
                    targetPOS += new Vector3(-ladderSize, ladderSize, 0);
                }
                break;
            case Direction.LEFT_DOWN:
                if (transform.position.x > -4 && transform.position == targetPOS)
                {
                    targetPOS += new Vector3(-ladderSize, -ladderSize, 0);
                }
                break;
            default:
                break;
        }
    }
}
