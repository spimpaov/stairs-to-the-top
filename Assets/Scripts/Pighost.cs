using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pighost : MonoBehaviour {

	[SerializeField] private float speed;

	void Update () {
		transform.position += Vector3.up*speed*Time.deltaTime;
	}
}
