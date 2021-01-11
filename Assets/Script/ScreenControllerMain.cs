using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenControllerMain : MonoBehaviour {


	//0 = ordinateur
	//1 = demiOrdinateur
	//2 = téléphone portable;

	private int screen = 0;

	[SerializeField]
	private GameObject _panelO;
	[SerializeField]
	private GameObject _panelDO;
	[SerializeField]
	private GameObject _panelTP;


	[SerializeField]
	private PanelDerouleController _panelDerouleControllerO;

	[SerializeField]
	private PanelDerouleController _panelDerouleControllerDO;

	[SerializeField]
	private PanelDerouleController _panelDerouleControllerTP;

	[SerializeField]
	private GameObject _panelChapitreO;

	[SerializeField]
	private  SwitchController _switchControllerDO;

	[SerializeField]
	private  SwitchController _switchControllerTP;

	private float width;

	private float height;

    [SerializeField]
    private GameObject _collidersTopDown = null;

    // Use this for initialization
    void Start () {
		width = Screen.width;
		height = Screen.height;

        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            ChangeScreen(0, 2);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Application.platform != RuntimePlatform.Android || Application.platform != RuntimePlatform.IPhonePlayer)
        {
            checkChangeSize ();
		    checkScreenSize ();
        }
}


	private void checkScreenSize(){


		switch (screen) {


		case 0:
            /*
			//On vérifie s'il faut passer en écran téléphone portable
			if (Screen.width <= 400 || Screen.height <= 400) {

				ChangeScreen (0,2);
			}
            */
			//On vérifie s'il faut passer en écran demi-ordinateur
			if (Screen.width < Screen.height) {

				ChangeScreen (0,1);

			}

			break;


		case 1:
                /*
			//On vérifie s'il faut passer en écran téléphone portable
			if (Screen.width <= 400 || Screen.height <= 400) {

				ChangeScreen (1, 2);

			}
            */
			//Sinon on vérifie s'il faut passer en écran ordinateur
			if (Screen.width > Screen.height) {

				ChangeScreen (1, 0);

			}

			break;

		/*case 2:

			//On vérifie si on retire l'écran de téléphone portable
			if (Screen.width >= 400 && Screen.height >= 400) {

				//On vérifie s'il faut mettre l'écran d'ordinateur
				if (Screen.width > Screen.height) {

					ChangeScreen (2, 0);

				//Sinon on met l'écran de demi-ordinateur
				} else {

					ChangeScreen (2, 1);
				}


			}
				
			break;
        */
		}


	}

	private void checkChangeSize(){

		if (Screen.width != width || Screen.height != height) {

            ChangeColliderTopDown();

            width = Screen.width;
			height = Screen.height;

			_switchControllerDO.StartChangeScreen ();
			//_switchControllerTP.StartChangeScreen ();
		}
	}
	//Cette fonction effectue les modifications de panel entre chaque changement d'écran
	//actu = écran actuel à changer, nouv = panel à mettre à la place
	private void ChangeScreen(int actu, int nouv){



		switch (actu) {

		//Cas où l'écran à changer est l'ordinateur
		case 0:

			_panelO.SetActive (false);
			_panelChapitreO.SetActive (false);

			//On vérifie quel écran mettre à la place
			switch (nouv) {

			//Si l'écran à placer est demi-ordinateur
			case 1 :
				
				_panelDerouleControllerDO.DerouleConnexion (GameState.GetIdEcranActuel ());
				_panelDO.SetActive (true);


				break;
			/*
			//Si l'écran à placer est téléphone portable
			case 2:

				_panelDerouleControllerTP.DerouleConnexion (GameState.GetIdEcranActuel ());
				_panelTP.SetActive (true);


				break;
			*/	
			}


				break;

		//Fin du cas où il faut changer l'écran ordinateur
		
		//Cas où l'écran à changer est demi-ordinateur
		case 1:

			_panelDO.SetActive (false);

			//On vérifie quel écran mettre à la place
			switch (nouv) {

			//Si l'écran à placer est ordinateur
			case 0:

				_panelDerouleControllerO.DerouleConnexion (GameState.GetIdEcranActuel ());
				_panelO.SetActive (true);

				break;
			/*
			//Si l'écran à placer est téléphone portable
			case 2:

				_panelDerouleControllerTP.DerouleConnexion (GameState.GetIdEcranActuel ());
				_panelTP.SetActive (true);

				break;
            */


			}


			break;

		//Fin du cas où il faut changer l'écran demi-ordinateur
		
		//Cas où il faut changer l'écran téléphone portable
		/*case 2:


			_panelTP.SetActive (false);

			//On vérifie quel écran mettre à la place
			switch (nouv) {

			//Si l'écran à placer est l'ordinateur
			case 0:

				_panelDerouleControllerO.DerouleConnexion (GameState.GetIdEcranActuel ());
				_panelO.SetActive (true);

				break;
			
			//Si l'écran à placer est demi-ordinateur
			case 1:

				_panelDerouleControllerDO.DerouleConnexion (GameState.GetIdEcranActuel ());
				_panelDO.SetActive (true);

				break;



			}


			break;
            */
		}

		//Fin du cas où il faut changer l'écran téléphone portable


		screen = nouv;
		Debug.Log ("ChangeScreen : actu[" + actu.ToString () + "] / nouv[" + nouv.ToString () + "]");

	}

    private void ChangeColliderTopDown()
    {
        
        foreach(Transform child in _collidersTopDown.transform)
        {
            BoxCollider2D c2d = child.gameObject.GetComponent<BoxCollider2D>();
            c2d.size = new Vector2(child.gameObject.GetComponent<RectTransform>().rect.size.x - (2*c2d.edgeRadius), child.gameObject.GetComponent<RectTransform>().rect.size.y - (2*c2d.edgeRadius));
        }

    }
}
