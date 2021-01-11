using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownObjectif : MonoBehaviour {

    [SerializeField]
    private TopDownController _tdc = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<JulienController>() != null)
        {
            _tdc.SetGameWon(true);
        }
    }
}
