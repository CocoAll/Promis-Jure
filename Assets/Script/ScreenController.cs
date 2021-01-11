using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Script chargé de gérer le changement de taille de l'écran du menu

public class ScreenController : MonoBehaviour {

	//0 = ordinateur
	//1 = portable
	private int screen = 0;

	[SerializeField]
	private GameObject _panelOrdinateur;

	[SerializeField]
	private GameObject _panelPortable;



	// Use this for initialization
	void Start () {
		
		checkScreenSize ();
	}
	
	// Update is called once per frame
	void Update () {

		checkScreenSize ();
	}


	private void checkScreenSize(){


		switch (screen) {


		case 0:
			//Si le panel ordinateur est activé, que la largeur est inférieur ou égal à 800 et que la largeur est
			//inférieur à la hauteur, on passe au panel portable
			if (Screen.width <= 800 && Screen.width < Screen.height) {


				_panelOrdinateur.SetActive (false);
				_panelPortable.SetActive (true);
				screen = 1;


			}

			break;

		//Si le panel ordinateur est actif et que la largeur de l'écran est supérieur à 800
		case 1:

			if (Screen.width > 800) {

				_panelPortable.SetActive (false);
				_panelOrdinateur.SetActive (true);
				screen = 0;

			}

			break;

		}


	}
}


	
