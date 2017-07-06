using UnityEngine;
using System.Collections;

public class escada : MonoBehaviour
{

    public bool alive = true;
    public Vector3 posicao = new Vector3(0, 0, 0);

    private GameObject player;

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag == "Cupim")
        {
            Debug.Log("escada vs cupim");
            StartCoroutine(destroyStair());
        }
    }

    IEnumerator destroyStair()
    {
        yield return new WaitForSeconds(1.5f);
        checaPlayerNaEscada();
        Destroy(this.gameObject);
    }

    void checaPlayerNaEscada()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Vector3 player_pos = player.GetComponent<Player>().transform.position;
        float x = Mathf.Abs(player_pos.x - this.transform.position.x);
        float y = Mathf.Abs(player_pos.y - this.transform.position.y);
            Debug.Log("x: " + x + ", y: " + y);
        if (x <= 1 && y <= 2 && !player.GetComponent<Player>().playerAdjEscada())
        {
            Debug.Log("ded");
            player.GetComponent<Player>().playerDed();

        }
    }


}
