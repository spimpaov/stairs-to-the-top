using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameoverMenu : MonoBehaviour {

	[SerializeField] private Transform rightPosition;
	[SerializeField] private float speed;
	[SerializeField] private List<GameObject> uiObjects;

	private void Start(){
		StartCoroutine( Up() );
		DesactivateUI();
	}
	private IEnumerator Up(){
		while(this.transform.position.y < rightPosition.position.y){
			transform.position += Vector3.up*Time.deltaTime*speed;
			yield return new WaitForEndOfFrame();
		}
		ActivateUI();
	}
	private void  ActivateUI(){
		foreach(GameObject go in uiObjects){
			go.SetActive(true);
		}
	}
	private void  DesactivateUI(){
		foreach(GameObject go in uiObjects){
			go.SetActive(false);
		}
	}
}
