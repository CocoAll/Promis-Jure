using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelMenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void LoadByIndex(int sceneIndex){

		SceneManager.LoadScene (sceneIndex);
	}


	public void RetourEcranTitre(){


		LoadByIndex (0);

	}

}
