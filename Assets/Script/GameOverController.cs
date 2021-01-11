using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameOverController : MonoBehaviour {
    
    /*****Variables for Timers*****/
    [SerializeField]
    private Image _img = null;
    [SerializeField]
    private Text _txt = null;
    private float _timer = 12.5f;
    public float _timeLeft;

    /******Panel Explications and end of Mini jeu*****/
    [SerializeField]
    private GameObject _panelExplications = null;
    [SerializeField]
    private GameObject _panelGameWon = null;
    [SerializeField]
    private GameObject _panelGameLost = null;

    /***Variable to Check Game End***/
    private int _nbOfLife = 5;
    private bool _isGameOver = false;
    private bool _isGameWon = false;
    public bool _isGameRunning = false;

    /*PanelDerouleController reference*/
    [SerializeField]
    private PanelDerouleController _pdc = null;

    // Use this for initialization
    void Start () {
        _timeLeft = _timer;
        if(_panelExplications.activeInHierarchy == false)
        {
            _panelExplications.SetActive(true);
        }
	}
	
	// Update is called once per frame
	void Update () {
		if(_isGameRunning == true)
        {
            _timeLeft -= Time.deltaTime;
            _img.fillAmount = _timeLeft / _timer;
            if (_timeLeft >= 0)
            {
                _txt.text = _timeLeft.ToString("F");
            }
            else
            {
                _txt.text = "0.00";
            }

            if (_timeLeft <= 0.0f && _nbOfLife > 0)
            {
                _isGameRunning = false;
                _isGameOver = true;
                _isGameWon = true;
                EndGame();
            }
        }
	}

    public void TakeDamage()
    {
        _nbOfLife--;
        if(_nbOfLife <= 0)
        {
            _isGameRunning = false;
            _isGameOver = true;
            EndGame();
        }
    }

    public void StartGame()
    {
        _isGameRunning = true;
        _panelExplications.SetActive(false);
    }

    public void EndGame()
    {
        if(_isGameOver == true && _isGameWon == true)
        {
            _panelGameWon.SetActive(true);
        }
        else if (_isGameOver == true && _isGameWon == false)
        {
            _panelGameLost.SetActive(true);
        }
        else
        {
            //How could you possibily arrived here ???
        }
    }

    private void ResetValues()
    {
        _timeLeft = _timer;
        _nbOfLife = 5;
        _isGameOver = false;
        _isGameWon = false;
        _isGameRunning = false;
        _panelExplications.SetActive(true);
    }

    public void GameWon()
    {
        this.gameObject.SetActive(false);
    }

    public void GameLost()
    {
        _pdc.LoadLastCheckpoint();
    }

}
