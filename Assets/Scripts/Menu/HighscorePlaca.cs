using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscorePlaca : MonoBehaviour {

	void Start () {
        this.GetComponent<Text>().text = PlayerPrefs.GetInt("Highscore").ToString();
	}
}
