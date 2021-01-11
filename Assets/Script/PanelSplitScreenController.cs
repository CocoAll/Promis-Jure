using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelSplitScreenController : MonoBehaviour {


	[SerializeField]
	private PanelDerouleController _PanelDerouleController;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void ChoixSplitScreen(int choix){


		switch (choix) {

		case 0:

			_PanelDerouleController.ChoixBouton(3120,"3119A");

			break;

		case 1:

			_PanelDerouleController.ChoixBouton(3121,"3119B");

			break;

		case 2:

			_PanelDerouleController.ChoixBouton(3122,"3119C");

			break;

		}

	}

}
