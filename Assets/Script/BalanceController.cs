using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BalanceController : MonoBehaviour {
    
    //Morceaux de la balnces qui bougent (images)
    [SerializeField]
    private Image _balancoire = null;
    [SerializeField]
    private Image _poidsDroite = null;
    [SerializeField]
    private Image _poidsGauche = null;

    private bool _firstTimeBalance = true;
    [SerializeField]
    private GameObject _popUpBalance = null;

    [SerializeField]
    private float _maxAngle = 10f;
    [SerializeField]
    private int _maxValueBalance = 50;
    [SerializeField]
    private float _maxAddingHigh = 5.5f;
    [SerializeField]
    private float _startingHigh = -1.8f;
    private float _currentAngle = 0.0f;

    [SerializeField]
    private GameObject _gameOverPanel = null;
	
	//Fonction appelé quand il y a un changement de valeur
    //pour faire varier la position de la balance
	public void UpdateBalance () {
        Debug.LogError("UpdateBalance Call");

        if(_firstTimeBalance == true)
        {
            _popUpBalance.SetActive(true);
            _firstTimeBalance = false;
        }


        int emotConvi = GameState.GetEmotionConviction();


        _currentAngle = _maxAngle * emotConvi / _maxValueBalance;
        float highDestination = _maxAddingHigh * emotConvi / _maxValueBalance;

        Debug.LogError("Should move of about : " + highDestination);
        _balancoire.gameObject.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, _currentAngle);
        _poidsDroite.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(-30.7f, _startingHigh - highDestination, 0);
        _poidsGauche.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(29.9f, _startingHigh + highDestination, 0);

        if(emotConvi >= _maxValueBalance || emotConvi <= -1 * _maxValueBalance)
        {
            _gameOverPanel.SetActive(true);
            _gameOverPanel.transform.SetAsLastSibling();
        }

	}
}
