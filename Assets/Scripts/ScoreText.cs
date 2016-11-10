using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour {

	public Text text;
	public int score = 0, pontuacaoEscada = 10;
	public float tempo = 0.0f;

	void Update(){
		showScore ();
		addEachSecond ();
	}

	private void showScore(){
		text.GetComponent<Text> ().text = "Score " + score.ToString();
	}
	public void addEachSecond(){
		tempo = tempo + Time.deltaTime;
		if (tempo >= 1) {
			tempo = 0;
			score++;
		}
	}
	public void addOnStair(){
		score += pontuacaoEscada;
	}

	public void addEachCoin(){
		score += 15;
	}
}
