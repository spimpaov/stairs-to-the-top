using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSwitch : MonoBehaviour {

    public Laser laser;

    [SerializeField] private bool activated = false;

    public void activateToggleSwitch()
    {
        activated = true;
        Debug.Log("LASER: " + laser);
        laser.Desativar();
        //Debug.Log("switch ativado: " + activated);
    }


}
