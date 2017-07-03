using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transition : MonoBehaviour {
	[SerializeField] private float timeBetweenPlanks;

	private void Update(){
		if(Input.GetKeyDown(KeyCode.Space)){
			StartCoroutine(ATransition());
		}
		if(Input.GetKeyDown(KeyCode.A)){
			StartCoroutine(BTransition());
		}
	}
	private IEnumerator ATransition(){
		while(true){
			for(int i = 0; i<transform.childCount;i++){
				transform.GetChild(i).GetComponent<Image>().enabled = true;
				yield return new WaitForSeconds(timeBetweenPlanks);
			}
		}
	}
	private IEnumerator BTransition(){
		while(true){
			for(int i = 0; i<transform.childCount;i++){
				transform.GetChild(i).GetComponent<Rigidbody2D>().gravityScale = 20;
			}
		}
	}
}
