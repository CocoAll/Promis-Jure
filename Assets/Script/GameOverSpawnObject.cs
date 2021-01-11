using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameOverSpawnObject : MonoBehaviour {

    [SerializeField]
    private string[] _listeMots;

    [SerializeField]
    private GameOverController _goc;

    private string _motChoisit = null;

	// Use this for initialization
	void Start () {
        _goc = GameObject.Find("GameOverController").GetComponent<GameOverController>();
        _motChoisit = _listeMots[Random.Range(0, _listeMots.Length)];
        this.GetComponent<Text>().text = _motChoisit;
    }
	
	// Update is called once per frame
	void Update () {
        if(_goc._timeLeft <= 0)
        {
            DestroyIt();
        }
        this.transform.Translate(-this.transform.up * Time.deltaTime * 135);
	}

    public void DestroyIt()
    {
        Destroy(this.gameObject);
    }
}
