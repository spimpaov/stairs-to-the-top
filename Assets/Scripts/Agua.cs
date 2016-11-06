using UnityEngine;
using System.Collections;

public class Agua : MonoBehaviour {
	
	public float velocidadeSubida = 0.5f;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		Sobe();
	}
	void Sobe(){
		transform.position += new Vector3 (0, velocidadeSubida * Time.deltaTime, 0);
	}
}
