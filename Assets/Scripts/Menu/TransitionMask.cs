using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionMask : MonoBehaviour {
	[SerializeField]private float targetSize = 10,speed = 1.2f;
	private RectTransform myRT;

	private void Start(){
		myRT = GetComponent<RectTransform>();
		Smaller_transition();
	}
	private IEnumerator Bigger(){
		myRT.localScale = new Vector3(0.01f,0.01f, 1);
		while(myRT.localScale.x < targetSize){
			myRT.localScale = new Vector3(myRT.localScale.x,myRT.localScale.y, 1)*speed;
			yield return new WaitForEndOfFrame();
		}
	}
	private IEnumerator Smaller(){
		myRT.localScale = new Vector3(targetSize,targetSize, 1);
		while(myRT.localScale.x > 0.01){
			myRT.localScale = new Vector3(myRT.localScale.x,myRT.localScale.y, 1)*(2-speed);
			yield return new WaitForEndOfFrame();
		}
	}
	public void Smaller_transition(){
		StartCoroutine(Smaller());
	}
	public void Bigger_transition(){
            StartCoroutine(Bigger());
    }

}
