using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSwitch : MonoBehaviour {

    public Laser laser;

    [SerializeField] private bool activated = false;
    private Animator myAnimator;

    private void Start(){
        myAnimator = GetComponent<Animator>();
    }
    public void activateToggleSwitch()
    {
        myAnimator.Play("switch_activate");
        activated = true;
        Debug.Log("LASER: " + laser);
        laser.Desativar();
    }


}
