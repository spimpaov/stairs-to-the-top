using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundCreator : MonoBehaviour {

	[SerializeField] private List<GameObject> bgList;
	[SerializeField] private GameObject initialGround;

	private Camera myCamera;
	private GameObject myPlayer;
	private float bgSize;
	private float lastBgHeight;
	private float cameraHeight;

	private void Start(){
		GetBackgroundSize();
		GetPlayer();
		GetCamera();
		GetInitialBGHeight();
	}
	private void Update(){
		GetCameraHeight();
		CanCreateBackground();
	}
	private void GetPlayer(){
		myPlayer = GameObject.Find("Player");
	}
	private void GetCamera(){
		myCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
	}
	private void GetCameraHeight(){
		cameraHeight = myCamera.transform.position.y + myCamera.orthographicSize;
	}
	private void GetBackgroundSize(){
		bgSize = bgList[0].GetComponent<SpriteRenderer>().size.y*2 + 0.15f;
	}
	private void GetInitialBGHeight(){
		lastBgHeight = initialGround.transform.position.y;
	}
	private void CreateBackground(){
		Vector3 position = new Vector3(0, lastBgHeight+bgSize, 1);
		if(Random.Range(0,100)< 71){
			Instantiate(bgList[3], position, Quaternion.identity);
		}else{
			Instantiate(bgList[Random.Range(0,bgList.Count)], position, Quaternion.identity);
		}
		lastBgHeight = position.y;
	}
	private void CanCreateBackground(){
		if(cameraHeight > lastBgHeight){
			CreateBackground();
		}
	}


}
