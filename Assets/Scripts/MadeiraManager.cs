using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MadeiraManager : MonoBehaviour {

    public int qntd_madeira_inicio;
    public int qntd_madeira_add;
    public int qntd_madeira_del;
    public Text madeira_text;

    private int qntd_madeira_atual;
    
    void Start()
    {
        qntd_madeira_atual = qntd_madeira_inicio;
    }

    void Update()
    {
        showMadeira();
    }

    public void addMadeira()
    {
        qntd_madeira_atual += qntd_madeira_add;
        //Debug.Log("addicionou mad: +" + qntd_madeira_add);
    }

    public void delMadeira()
    {
        qntd_madeira_atual -= qntd_madeira_del;
        //Debug.Log("perdeu mad: -" + qntd_madeira_del);
        if (qntd_madeira_atual <= 0 )
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    private void showMadeira()
    {
        madeira_text.text = qntd_madeira_atual.ToString();
    }

}
