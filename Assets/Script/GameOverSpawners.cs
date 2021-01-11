using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverSpawners : MonoBehaviour {

    [SerializeField]
    GameOverController _goc = null;
    [SerializeField]
    private List<GameObject> _listSpawners = null;
    [SerializeField]
    private GameObject _spawnObject = null;

    private bool _shouldSpawn = true;

    /*Varibales for TimeControl*/
    private float _timeBetween2 = 0.6f;
    private float _timerBetween2 = 0.0f;

	// Use this for initialization
	void Start () {
        _timerBetween2 = _timeBetween2;
	}
	
	// Update is called once per frame
	void Update () {
		if(_goc._isGameRunning == true && _shouldSpawn == true)
        {
            _timerBetween2 -= Time.deltaTime;
            if(_timerBetween2 <= 0.0f)
            {
                SpawnObjectAtRandomSpawner();
                if(_goc._timeLeft > 3.0)
                {
                    _timerBetween2 = _timeBetween2;
                }
                else
                {
                    _shouldSpawn = false;
                }
            }
        }
	}

    void SpawnObjectAtRandomSpawner()
    {
        int spawner = Random.Range(0, 5);
        Instantiate(_spawnObject, _listSpawners[spawner].transform);
    }
}
