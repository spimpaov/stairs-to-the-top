using UnityEngine;
using System.Collections;

public class Saw : MonoBehaviour {

    [SerializeField] private GameObject brokenSawPrefab;

    public void destroySaw()
    {
        GameObject go;
        Destroy(this.gameObject);
        go = Instantiate(brokenSawPrefab,transform.position,Quaternion.identity);
        go.GetComponent<Rigidbody2D>().AddForce(Vector2.up*500);
        go.transform.Rotate(0,0,Random.Range(0,90));
    }
}
