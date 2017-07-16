using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour {

    public bool pressionado = false;
    private Player player;

    private GameObject pauseBg;

    void Start()
    {
        Time.timeScale = 1;
        pressionado = false;
    }

    public void pausar()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        pauseBg = GameObject.FindGameObjectWithTag("PauseBackground");

        if(!pressionado)
        {
            Debug.Log("entrada na pausa");
            pressionado = true;
            player.pauseGame();
            pauseBg.GetComponent<Image>().enabled = true;
            pauseBg.transform.GetChild(0).GetComponent<Text>().enabled = true;
            setMenuButton(true);
            Time.timeScale = 0;
        } else
        {
            Debug.Log("saida da pausa");
            pressionado = false;
            player.resumeGame();
            pauseBg.GetComponent<Image>().enabled = false;
            pauseBg.transform.GetChild(0).GetComponent<Text>().enabled = false;
            setMenuButton(false);
            Time.timeScale = 1;
        }
    }

    void setMenuButton(bool valor)
    {
        GameObject botao = GameObject.FindGameObjectWithTag("MenuButton");
        botao.GetComponent<Image>().enabled = valor;
        botao.GetComponent<Button>().enabled = valor;
        botao.transform.GetChild(0).GetComponent<Image>().enabled = valor;
    }
}
