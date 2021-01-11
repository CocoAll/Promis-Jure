using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopDownAlphaMover : MonoBehaviour {

    private bool _isMaxAlpha = false;
    private Image _imageToChange = null;

    void Start()
    {
        _imageToChange = this.gameObject.GetComponent<Image>();
        _imageToChange.canvasRenderer.SetAlpha(0.0f);
    }

    // Update is called once per frame
    void Update () {
		if(_isMaxAlpha == false)
        {
            _imageToChange.canvasRenderer.SetAlpha(_imageToChange.canvasRenderer.GetAlpha() + 0.007f);
            if(_imageToChange.canvasRenderer.GetAlpha() >= 0.85f)
            {
                _imageToChange.canvasRenderer.SetAlpha(0.85f);
                _isMaxAlpha = true;
            }
        }
        else
        {
            _imageToChange.canvasRenderer.SetAlpha(_imageToChange.canvasRenderer.GetAlpha() - 0.007f);
            if (_imageToChange.canvasRenderer.GetAlpha() <= 0.0f)
            {
                _imageToChange.canvasRenderer.SetAlpha(0.0f);
                _isMaxAlpha = false;
            }
        }
	}
}
