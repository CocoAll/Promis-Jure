using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

    [SerializeField]
    private AudioSource _audioSourceAmbiance = null;
    [SerializeField]
    private AudioSource _audioSourceBruitage = null;

    public float _currentVolume = 0.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(_audioSourceBruitage.volume != _currentVolume)
        {
            _currentVolume = _audioSourceAmbiance.volume;
        }
	}

    public IEnumerator ExploitAudioString(string audio)
    {
        string[] parsed = audio.Split('|');
        //Debug.Log(parsed.Length);
        int int1 = int.Parse(parsed[0]);
        string str1 = parsed[1];
        int int2 = int.Parse(parsed[2]);
        string str2 = parsed[3];
        switch (int1)
        {
            default:
                break;
            case 1:
                _audioSourceAmbiance.Stop();
                _audioSourceAmbiance.clip = Resources.Load<AudioClip>("Audio/" + str1);
                PlayerPrefs.SetString("Musique", str1);
                _audioSourceAmbiance.Play();
                break;
            case 2:
                Coroutine fade = StartCoroutine(FadeOutAmbiance());
                yield return fade;
                _audioSourceAmbiance.Stop();
                _audioSourceAmbiance.clip = Resources.Load<AudioClip>("Audio/" + str1);
                PlayerPrefs.SetString("Musique", str1);
                _audioSourceAmbiance.Play();
                fade = StartCoroutine(FadeInAmbiance());
                yield return fade;
                break;
            case 3:
                _audioSourceAmbiance.Stop();
                PlayerPrefs.SetString("Musique", null);
                break;
        }

        switch (int2)
        {
            default:
                break;
            case 1:
                _audioSourceBruitage.Stop();
                if (_audioSourceBruitage.loop == true )
                {
                    _audioSourceBruitage.loop = false;
                }
                _audioSourceBruitage.clip = Resources.Load<AudioClip>("Audio/" + str2);
                PlayerPrefs.SetString("Bruitage", str2);
                _audioSourceBruitage.Play();
                break;
            case 2:
                _audioSourceBruitage.Stop();
                if (_audioSourceBruitage.loop == false)
                {
                    _audioSourceBruitage.loop = true;
                }
                _audioSourceBruitage.clip = Resources.Load<AudioClip>("Audio/" + str2);
                PlayerPrefs.SetString("Bruitage", str2);
                _audioSourceBruitage.Play();
                break;
            case 3:
                _audioSourceBruitage.Stop();
                PlayerPrefs.SetString("Bruitage", null);
                break;
        }

        yield return null;
    }

    public IEnumerator ConnexionExploitAudioString(string audio)
    {

        if (PlayerPrefs.GetString("Musique") != null)
        {
            _audioSourceAmbiance.clip = Resources.Load<AudioClip>("Audio/" + PlayerPrefs.GetString("Musique"));
        }
        if (PlayerPrefs.GetString("Bruitage") != null)
        {
            _audioSourceBruitage.clip = Resources.Load<AudioClip>("Audio/" + PlayerPrefs.GetString("Bruitage"));
        }
        yield return null;
    }

    private IEnumerator FadeOutAmbiance()
    {
        while(_audioSourceBruitage.volume > 0)
        {
            _audioSourceBruitage.volume -= Time.deltaTime;
        }
        yield return null;
    }

    private IEnumerator FadeInAmbiance()
    {
        while (_audioSourceBruitage.volume < _currentVolume)
        {
            _audioSourceBruitage.volume -= Time.deltaTime;
        }
        if(_audioSourceBruitage.volume > _currentVolume)
        {
            _audioSourceBruitage.volume = _currentVolume;
        }
        yield return null;
    }

}
