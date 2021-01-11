using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverJulienCollision : MonoBehaviour {

    [SerializeField]
    private GameOverController _goc = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Get Touched");
        if(collision.gameObject.tag == "SpawnObject")
        {
            _goc.TakeDamage();
        }
    }

}
