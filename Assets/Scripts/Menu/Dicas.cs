using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dicas : MonoBehaviour {

	[SerializeField]private List<string> dicas;

	private void Start(){
		GetComponent<Text>().text = "<b>DICA</b>\n" + dicas[ Random.Range(0,dicas.Count)];
	}
}
