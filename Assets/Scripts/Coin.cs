using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

    public void destroyCoin()
    {
        Destroy(this.gameObject);
    }
}
