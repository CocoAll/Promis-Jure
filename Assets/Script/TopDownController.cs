using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownController : MonoBehaviour {

    [SerializeField]
    private GameObject _joueur = null;
    [SerializeField]
    private GameObject _text1 = null;
    [SerializeField]
    private GameObject _text2 = null;

	[SerializeField]
	private PanelDerouleController _panelDerouleController;

    private Transform _initialPosJoueur = null;

    private bool _isGameWon = false;

    public bool _canWalk = false;

    // Use this for initialization
    void Start () {
        StartCoroutine(ShutDownText());
        _initialPosJoueur = _joueur.transform;
        PrepareMiniJeu();
	}
	
	// Update is called once per frame
	void Update () {
		if(_isGameWon == true)
        {
            StartCoroutine(End());
        }
	}

    void PrepareMiniJeu()
    {
        if(_initialPosJoueur != _joueur.transform)
        {
            _joueur.transform.position = _initialPosJoueur.position;
            _joueur.transform.rotation = _initialPosJoueur.rotation;
        }

        if(_isGameWon == true)
        {
            _isGameWon = false;
        }
    }

    private IEnumerator End()
    {
        yield return new WaitForSeconds(0.075f);
        PrepareMiniJeu();
		_panelDerouleController.OnClickLinéaire ();
    }

    private IEnumerator ShutDownText()
    {
        yield return new WaitForSeconds(5.0f);
        _text1.SetActive(false);
        _text2.SetActive(false);
        _canWalk = true;
    }

    public void SetGameWon(bool set)
    {
        _isGameWon = set;
    }

}
