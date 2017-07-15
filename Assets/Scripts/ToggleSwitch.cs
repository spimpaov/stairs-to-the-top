using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSwitch : MonoBehaviour {

    public Laser laser;
    private Animator myAnimator;

    private void Start(){
        myAnimator = GetComponent<Animator>();
    }
    public void activateToggleSwitch()
    {
        myAnimator.Play("switch_activate");
        laser.Desativar();
    }
}
