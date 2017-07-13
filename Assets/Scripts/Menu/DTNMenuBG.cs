using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DTNMenuBG : MonoBehaviour {

    public Sprite diaBG;
    public Sprite tardeBG;
    public Sprite noiteBG;

    void Start () {
        chooseBG();
	}
    private void chooseBG()
    {
        int sysHour = System.DateTime.Now.Hour;
        if (sysHour >= 18)
        {
            //seta o fundo da noite
            this.gameObject.GetComponent<SpriteRenderer>().sprite = noiteBG;
            Debug.Log("OI");

        }
        else if (sysHour >= 12)
        {
            //seta fundo da tarde
            this.gameObject.GetComponent<SpriteRenderer>().sprite = tardeBG;

        }
        else // sysHour >= 0
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = diaBG;
        }
    }
}
