using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
    public Coin coin;
    public Saw saw;

	void Update () {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            createCoin(new Vector3(0, 3, 0));
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            createSaw(new Vector3(3, 3, 0));
        }
    }

    void createCoin(Vector3 position)
    {
        Instantiate(coin, position, Quaternion.identity);
    }

    void createSaw(Vector3 position)
    {
        Instantiate(saw, position, Quaternion.identity);
    }

}
