using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyScreen : MonoBehaviour {
	[SerializeField]private GameObject woodHud;
	[SerializeField]private GameObject water;

	private void Start(){
		//woodHud.SetActive(false);
		water.SetActive(false);
	}
	private void ShowHUD(){
		woodHud.SetActive(true);
	}
	private void InitializeWater(){
		water.SetActive(true);
	}
	public void BeginGame(){
		InitializeWater();
		//ShowHUD();
	}
}
