using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSwitch : MonoBehaviour {

    public Laser laser;

    private bool activated = false;

    public void activateToggleSwitch()
    {
        activated = true;
        Debug.Log("LASER: " + laser);
        laser.GetComponent<BoxCollider2D>().enabled = false;
        //Debug.Log("switch ativado: " + activated);
    }


}
