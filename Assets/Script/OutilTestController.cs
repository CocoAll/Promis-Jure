using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutilTestController : MonoBehaviour {

	[SerializeField]
	private PanelDerouleController _panelDerouleController;

	[SerializeField]
	private GameObject _panelOutilTest;

	[SerializeField]
	private InputField _id;

	[SerializeField]
	private InputField _victor;

	[SerializeField]
	private InputField _emma;

	[SerializeField]
	private GameState _GameState;

	private int active;

	// Use this for initialization
	void Start () {

		active = 0;

	}
	
	// Update is called once per frame
	void Update () {

		//Lorqu'on appuie sur la touche t, on active ou désactive
		//l'outil de test
		if (Input.GetKeyDown ("t")) {

			if (active == 0) {

				_panelOutilTest.SetActive (true);
				active = 1;

			} else if (active == 1) {

				_panelOutilTest.SetActive (false);
				active = 0;
			}

		}

	}


	public void LancerTest(){

		string id = _id.text;
		string victor = _victor.text;
		string emma = _emma.text;

		GameState.SetIdEcranActuel (int.Parse (id));

		_GameState.AddJournalPersonnage ("Victor", 0);

		_GameState.AddJournalPersonnage("Emma", 0);

		/*
		GameState.GetJournalPersonnageByPrenom ("Victor").GetComponent<PersonnageObject> ().SetValueRelation (int.Parse(victor));

		GameState.GetJournalPersonnageByPrenom ("Emma").GetComponent<PersonnageObject> ().SetValueRelation (int.Parse(emma));
		*/

		GameState.SetPersonnageValueByPrenom ("Victor", int.Parse (victor));

		GameState.SetPersonnageValueByPrenom("Emma",int.Parse(emma));

		_panelDerouleController.DerouleConnexion(GameState.GetIdEcranActuel());



	}



}
