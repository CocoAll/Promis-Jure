using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {


	[SerializeField]
	private GameObject _panelNouvellePartie;

	[SerializeField] 
	private GameObject _panelContinuerPartie;

	// Use this for initialization
	void Start () {

		LancementMenu ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void NouvellePartie(){

		PlayerPrefs.SetInt("PartieEnCours", 0);
		LoadByIndex (1);

	}

	public void ContinuerPartie(){

        PlayerPrefs.SetInt("PartieEnCours", 1);
        LoadByIndex (1);
	}

	public void LoadByIndex(int sceneIndex){

		SceneManager.LoadScene (sceneIndex);
	}


	private void LancementMenu(){

		int enCours = PlayerPrefs.GetInt ("PartieEnCours");
		Debug.Log ("Partie en cours : " + enCours.ToString ());
		if (enCours == 1) {

			_panelNouvellePartie.SetActive (false);
			_panelContinuerPartie.SetActive (true);
		}


	}

}
