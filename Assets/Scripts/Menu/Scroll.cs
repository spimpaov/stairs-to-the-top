using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour {

	[SerializeField] private float speed = 10f;
	private Material myMaterial;

	void Start(){
		myMaterial = GetComponent<MeshRenderer>().material;
	}
	void Update(){
		myMaterial.mainTextureOffset = Vector2.right*speed*Time.time*0.2f;
	}
}
