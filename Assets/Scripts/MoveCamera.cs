using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {

	public GameObject player;
	public float playerY = 0;
	public float playerPosYAnterior = 0;

	void Update (){
		if(player != null) Movimento ();
	}

	void Movimento(){
		playerY = player.transform.position.y - playerPosYAnterior;
		playerPosYAnterior = player.transform.position.y;

		if (player.transform.position.y >= 8) transform.position += new Vector3 (0, playerY, 0);
	}
}