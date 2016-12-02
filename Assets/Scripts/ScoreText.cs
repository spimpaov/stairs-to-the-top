using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour {

	public Text text;
	public int score = 0, pontuacaoEscada = 10;
	public float tempo = 0.0f;
    public bool inGame;    

	void Update() {
		showScore ();
        if (inGame) {
            addEachSecond();
            if (score > PlayerPrefs.GetInt("Highscore")) { PlayerPrefs.SetInt("Highscore", score); }
        }
	}

	private void showScore() {
        if (inGame) { text.GetComponent<Text>().text = "Score: " + score.ToString(); }
        else { text.GetComponent<Text>().text = "Score: " + PlayerPrefs.GetInt("Score") + "\nHighscore: " + PlayerPrefs.GetInt("Highscore"); }
    }

	public void addEachSecond() {
		tempo = tempo + Time.deltaTime;
		if (tempo >= 1) {
			tempo = 0;
			score++;
            PlayerPrefs.SetInt("Score", score);
        }
    }
	public void addOnStair() {
		score += pontuacaoEscada;
	}

	public void addEachCoin() {
		score += 15;
	}
}
