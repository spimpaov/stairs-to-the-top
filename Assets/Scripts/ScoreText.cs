using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour {

	public Text text;
    public int pontuacaoEscada, pontuacaoPowerUp, pontuacaoMadeira;
	public float tempo = 0.0f;
    public bool inGame;

    private Player player;
    private int score;

    void Start()
    {
        if (inGame) player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        score = 0;
    }

    void Update() {
		showScore ();
        if (inGame && player.moved) {
            //addEachSecond();
        }
	}

	private void showScore() {
        if (inGame) { text.GetComponent<Text>().text = score.ToString(); }
        else { text.GetComponent<Text>().text = "Score: " + PlayerPrefs.GetInt("Score") + "\nHighscore: " + PlayerPrefs.GetInt("Highscore"); }
    }

	public void addEachSecond() {
		tempo = tempo + Time.deltaTime;
		if (tempo >= 1) {
			tempo = 0;
			score++;
        }
    }

    public void setHighscore()
    {
        PlayerPrefs.SetInt("Score", score);
        if (score > PlayerPrefs.GetInt("Highscore")) { PlayerPrefs.SetInt("Highscore", score); }
    }

	public void addOnStair() {
		score += pontuacaoEscada;
	}

	public void addOnPowerUp() {
		score += pontuacaoPowerUp;
	}

    public void addOnMadeira()
    {
        score += pontuacaoMadeira;
    }
}
