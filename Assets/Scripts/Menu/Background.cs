using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {

	[SerializeField] private Transform A,B;
	public bool modeA = true;
	[SerializeField] Transform myCam;
	[SerializeField] private float speed;

	private IEnumerator ChangeToRight(){
		Vector3 initPos = myCam.position;
		float offset = B.position.x - myCam.position.x;

		while(myCam.position.x != B.position.x){
			myCam.position = Vector3.MoveTowards(myCam.position,
													 initPos+offset*Vector3.right,
													 speed);

			yield return new WaitForEndOfFrame();
		}
		Debug.Log("OI1");
	}
	private IEnumerator ChangeToLeft(){
		Vector3 initPos = myCam.position;
		float offset = A.position.x - myCam.position.x;
		
		while(myCam.position.x != A.position.x){
			myCam.position = Vector3.MoveTowards(myCam.position,
													 initPos+offset*Vector3.right,
													 speed);

			yield return new WaitForEndOfFrame();
		}
		Debug.Log("OI2");
	}
	public void Change(){
		if(!modeA){
			StartCoroutine( ChangeToRight() );
		} else{
			StartCoroutine( ChangeToLeft() );
		}
		modeA = !modeA;
	}
}
