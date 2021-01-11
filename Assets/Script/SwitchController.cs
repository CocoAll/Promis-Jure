using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Gère le mouvement du menu de la version téléphone portable du jeu, pour le faire apparaître et disparaitre

public class SwitchController : MonoBehaviour {

	[SerializeField]
	private GameObject _panelMenu;

	[SerializeField]
	private GameObject _panelDeroule;

	[SerializeField]
	private GameObject _menuPosition;

	private float xpanelMenu;
	private float ypanelMenu;
	private float ypanelDeroule;

	private float speed = 500.0f;
	private float startTime;


	private Vector3 start;
	private Vector3 end;

	private int menuIsActive = 1;

	// Use this for initialization
	void Start () {

		//On récupère la position initiale du menu et celle qu'il doit prendre lorsqu'on l'active
		//start est la position initiale, end est la position qu'il doit prendre lorsqu'il est actif
		start = _menuPosition.transform.position;
		end = _panelDeroule.transform.position;

	}
	
	// Update is called once per frame
	void Update () {

		end = _panelDeroule.transform.position;
		float step = speed * Time.deltaTime;

		//Si le menu est activé et qu'il n'a pas encore pris la bonne position
		//on continue de le bouger jusqu'à la position end
		if (menuIsActive == 0 && _panelMenu.transform.position != end) {

			_panelMenu.transform.position = Vector3.MoveTowards (_panelMenu.transform.position,end, step);

			//Si le menu est désactivé et qu'il n'a pas repris sa position initiale
			//On continue de le bouger jusqu'à la position start
		} else if (menuIsActive == 1 && _panelMenu.transform.position != start) {

			_panelMenu.transform.position = Vector3.MoveTowards (_panelMenu.transform.position,start, step);
		}

	}


	public void SwitchMenu(int menu){

		menuIsActive = menu;

	}

	public void StartChangeScreen(){

		start = _menuPosition.transform.position;

	}

}
