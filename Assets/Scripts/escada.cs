using UnityEngine;
using System.Collections;

public class escada : MonoBehaviour
{

    public bool alive = true;
    public Vector3 posicao = new Vector3(0, 0, 0);
    private SpriteRenderer mySR;

    private GameObject player;

    private void Start(){
        mySR = GetComponent<SpriteRenderer>();
    }
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
        mySR.color = new Color (0.7f,0.7f,0.7f,1);
        yield return new WaitForSeconds(0.3f);
        mySR.color = new Color (0.4f,0.4f,0.4f,1);
        yield return new WaitForSeconds(0.3f);
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
