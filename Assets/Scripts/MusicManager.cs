using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour {

    static float music_volume = 1;
    public Slider music_slider;

    void Start()
    {
        this.gameObject.GetComponent<AudioSource>().volume = music_volume;
        if (music_slider != null) music_slider.value = music_volume;
    }

    public void setMusicVolume()
    {
        music_volume = music_slider.value;
        Debug.Log("music_volume: " + music_volume);
        this.gameObject.GetComponent<AudioSource>().volume = music_volume;
    }
}
