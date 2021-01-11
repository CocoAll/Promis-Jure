using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour {

    [SerializeField]
    private Slider[] _sliders = null;

    [SerializeField]
    private AudioSource[] _audiosClips = null;

	// Use this for initialization
	void Start () {
		foreach(Slider sld in _sliders)
        {
            sld.onValueChanged.AddListener(delegate { ValueChangeCheck(sld.value); });
        }
	}


    private void ValueChangeCheck(float value)
    {
        foreach(AudioSource ac in _audiosClips)
        {
            ac.volume = value;
        }
    }
}
