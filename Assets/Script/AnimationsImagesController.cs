using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class AnimationsImagesController : MonoBehaviour {

    //General variable
    private bool _isAnimationRunning = false;
    [SerializeField]
    private Image _imgToAnimate = null;

    //Variable pour l'animation de pluie
    [SerializeField]
    private Sprite[] _pluieAnimationImage = null;
    [SerializeField]
    private float _pluieTimeBetweenTwo = 0.0f;

    //Variable pour l'animation de l'ordinateur
    [SerializeField]
    private Sprite[] _ordiAnimationImage = null;
    [SerializeField]
    private float _ordiTimeBetweenTwo = 0.0f;

    //Variable pour l'animation de l'ID 3144
    [SerializeField]
    private Sprite[] _chap3AnimationImage = null;
    [SerializeField]
    private float _chap3TimeBetweenTwo = 0.0f;

    //Variable pour l'animation de l'ID 4206
    [SerializeField]
    private Sprite[] _4206AnimationImage = null;
    [SerializeField]
    private float _4206TimeBetweenTwo = 0.0f;

    //Variable pour l'animation de l'ID 4211
    [SerializeField]
    private Sprite[] _4211AnimationImage = null;
    [SerializeField]
    private float _4211TimeBetweenTwo = 0.0f;

    //Variable pour l'animation de l'ID 4219
    [SerializeField]
    private Sprite[] _4219AnimationImage = null;
    [SerializeField]
    private float _4219TimeBetweenTwo = 0.0f;


    //Variable pour l'animation de l'ID 5488
    [SerializeField]
    private Sprite[] _5488AnimationImage = null;
    [SerializeField]
    private float _5488TimeBetweenTwo = 0.0f;

    //Variable pour l'animation de l'ID 8009
    [SerializeField]
    private Sprite[] _8009AnimationImage = null;
    [SerializeField]
    private float _8009TimeBetweenTwo = 0.0f;

    //Variable pour l'animation de l'ID 8011
    [SerializeField]
    private Sprite[] _8011AnimationImage = null;
    [SerializeField]
    private float _8011TimeBetweenTwo = 0.0f;

    //Varibale pour gérer la coroutine d'animation
    private IEnumerator coroutine = null;

    [SerializeField]
    private PanelDerouleController _pdc = null;

    //Fonction pour verifier si il y a une animation a lancé
    public void CheckAnimation(int ID)
    {
        switch (ID)
        {
            case 1001:
                if(_isAnimationRunning == true)
                {
                    StopCoroutine(coroutine);
                }
                coroutine = RunningAnimation(_pluieAnimationImage, _pluieTimeBetweenTwo);
                StartCoroutine(coroutine);
                _isAnimationRunning = true;
                break;
            case 1014:
                if (_isAnimationRunning == true)
                {
                    StopCoroutine(coroutine);
                }
                coroutine = RunningAnimation(_ordiAnimationImage, _ordiTimeBetweenTwo);
                StartCoroutine(coroutine);
                _isAnimationRunning = true;
                break;
            case 4007:
                if (_isAnimationRunning == true)
                {
                    StopCoroutine(coroutine);
                }
                coroutine = OneTimeAnimation(_chap3AnimationImage, _chap3TimeBetweenTwo);
                StartCoroutine(coroutine);
                _isAnimationRunning = true;
                break;
            case 5206:
                if (_isAnimationRunning == true)
                {
                    StopCoroutine(coroutine);
                }
                coroutine = OneTimeAnimation(_4206AnimationImage, _4206TimeBetweenTwo);
                StartCoroutine(coroutine);
                _isAnimationRunning = true;
                break;
            case 5211:
                if (_isAnimationRunning == true)
                {
                    StopCoroutine(coroutine);
                }
                coroutine = OneTimeAnimation(_4211AnimationImage, _4211TimeBetweenTwo);
                StartCoroutine(coroutine);
                _isAnimationRunning = true;
                break;
            case 5219:
                if (_isAnimationRunning == true)
                {
                    StopCoroutine(coroutine);
                }
                coroutine = OneTimeAnimation(_4219AnimationImage, _4219TimeBetweenTwo);
                StartCoroutine(coroutine);
                _isAnimationRunning = true;
                break;
            case 5488:
                if (_isAnimationRunning == true)
                {
                    StopCoroutine(coroutine);
                }
                coroutine = OneTimeAnimation(_5488AnimationImage, _5488TimeBetweenTwo);
                StartCoroutine(coroutine);
                _isAnimationRunning = true;
                break;
            case 8009:
                if (_isAnimationRunning == true)
                {
                    StopCoroutine(coroutine);
                }
                coroutine = OneTimeAnimation(_8009AnimationImage, _8009TimeBetweenTwo);
                StartCoroutine(coroutine);
                _isAnimationRunning = true;
                break;
            case 8011:
                if (_isAnimationRunning == true)
                {
                    StopCoroutine(coroutine);
                }
                coroutine = OneTimeAnimation(_8011AnimationImage, _8011TimeBetweenTwo);
                StartCoroutine(coroutine);
                _isAnimationRunning = true;
                break;
            default:
                if (_isAnimationRunning == true)
                {
                    StopCoroutine(coroutine);
                    _isAnimationRunning = false;
                }
                break;
        }
    }

    //Coroutine qui gere le changement d'image pour une animation
    private IEnumerator RunningAnimation(Sprite[] tabOgImg, float time)
    {
        int i = 0;

        while (true)
        {
            yield return new WaitForEndOfFrame();
            _imgToAnimate.sprite = tabOgImg[i];
            
            if(i == tabOgImg.Length - 1)
            {
                i = 0;
            }
            else
            {
                i++;
            }

            yield return new WaitForSeconds(time);
        }
    }

    //Coroutine qui gere les animations qui une ofis terminer amene sur un nouveau deroule
    private IEnumerator OneTimeAnimation(Sprite[] tabOgImg, float time)
    {
        _pdc.DeactivatePanelDerouleAndDialogue();
        for (int i = 0; i < tabOgImg.Length; i++)
        {
            yield return new WaitForEndOfFrame();
            _imgToAnimate.sprite = tabOgImg[i];
            yield return new WaitForSeconds(time);
        }
        _pdc.LaunchingNextDerouleFromAnimationController();
    }

}
