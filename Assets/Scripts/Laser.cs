using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {
	[SerializeField] private bool off;
	private SpriteRenderer mySR;
	[SerializeField] float turningOffVelocity = 0.5f;

	// Use this for initialization
	void Start () {
		off = false;
		mySR = transform.GetChild(0).GetComponent<SpriteRenderer>();
	}
	public void Desativar(){
		if(!off){
			Debug.Log("OIPORRA");
			transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = false;
			StartCoroutine(TurnOffLaserRay());
		}
	}
	private IEnumerator TurnOffLaserRay(){
		while(mySR.size.x > 0){
			Debug.Log(mySR.size.x);
			mySR.size = new Vector2(mySR.size.x*turningOffVelocity, mySR.size.y);
			yield return new WaitForEndOfFrame();
		}
	}
}
