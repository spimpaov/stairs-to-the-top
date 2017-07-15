using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{

    static float sfx_volume = 1;

    public Slider sfx_slider;
    public AudioClip mov_player;
    public AudioClip coleta_mad;
    public AudioClip ativa_switch;
    public AudioClip morte_player;
    public AudioClip pega_powerup;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
        this.gameObject.GetComponent<AudioSource>().volume = sfx_volume;
        if (sfx_slider != null) sfx_slider.value = sfx_volume;
    }

    public void setSfxVolume()
    {
        sfx_volume = sfx_slider.value;
        Debug.Log("sfx_volume: " + sfx_volume);
        audioSource.volume = sfx_volume;
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
