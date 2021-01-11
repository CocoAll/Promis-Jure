using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JulienController : MonoBehaviour {

    //Inspector Variables
    float playerSpeed = 75.0f; //speed player moves
    bool isWalking = false;
    bool isAnimationRunning = false;

    [SerializeField]
    private TopDownController _tdc = null;

    [SerializeField]
    private Sprite[] _julienAnimationImage = null;
    
    private float _julienTimeBetweenTwo = 0.175f;
    [SerializeField]
    private Image _imageToAnimate = null;


    private IEnumerator coroutine = null;

    void Update()
    {
        if (_tdc._canWalk == true)
        {
            MoveForward(); // Player Movement 
            if (isWalking == true)
            {
                if(isAnimationRunning == false)
                {
                    coroutine = Animate();
                    StartCoroutine(coroutine);
                    isAnimationRunning = true;
                }
            }
            else
            {
                if (isAnimationRunning == true)
                {
                    StopCoroutine(coroutine);
                    isAnimationRunning = false;
                    _imageToAnimate.sprite = _julienAnimationImage[0];
                }
            }
        }
    }

    private void MoveForward()
    {

        if (Input.GetKey("up"))//Press up arrow key to move forward on the Y AXIS
        {
            transform.rotation = Quaternion.Euler(0,0,0);
            transform.Translate(0, playerSpeed * Time.deltaTime, 0);
            if(isWalking == false)
            {
                isWalking = true;
            }
        }
        else if (Input.GetKey("down"))//Press up arrow key to move forward on the Y AXIS
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);
            transform.Translate(0, playerSpeed * Time.deltaTime, 0);
            if (isWalking == false)
            {
                isWalking = true;
            }
        }
        else if (Input.GetKey("left"))//Press up arrow key to move forward on the Y AXIS
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
            transform.Translate(0, playerSpeed * Time.deltaTime, 0);
            if (isWalking == false)
            {
                isWalking = true;
            }
        }
        else if (Input.GetKey("right"))//Press up arrow key to move forward on the Y AXIS
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);
            transform.Translate(0, playerSpeed * Time.deltaTime, 0);
            if (isWalking == false)
            {
                isWalking = true;
            }
        }
        else
        {
            if (isWalking == true)
            {
                isWalking = false;
            }
        }
    }
    
    private IEnumerator Animate()
    {
        int i = 0;
        while (isWalking == true)
        {
            yield return new WaitForEndOfFrame();
            _imageToAnimate.sprite = _julienAnimationImage[i];
            if (i == _julienAnimationImage.Length - 1)
            {
                i = 0;
            }
            else
            {
                i++;
            }

            yield return new WaitForSeconds(_julienTimeBetweenTwo);
        }

    }
    
}
