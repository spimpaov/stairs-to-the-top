using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public AudioClip mov_player;
    public AudioClip coleta_mad;
    public AudioClip ativa_switch;
    public AudioClip morte_player;
    public AudioClip pega_powerup;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<AudioSource>();
    }
    public void setSoundMovPlayer()
    {
        audioSource.PlayOneShot(mov_player);
    }
    public void setSoundColetaMadeira()
    {
        audioSource.PlayOneShot(coleta_mad);
    }
    public void setSoundAtivaSwitch()
    {
        audioSource.PlayOneShot(ativa_switch);
    }
    public void setSoundMortePlayer()
    {
        audioSource.PlayOneShot(morte_player);
    }
    public void setSoundPegaPowerUp()
    {
        audioSource.PlayOneShot(pega_powerup);
    }
}
