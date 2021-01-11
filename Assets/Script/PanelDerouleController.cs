using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;

public class PanelDerouleController : MonoBehaviour {

	private int idDerouleActuel;
	private int idDerouleNext;
	private int choixNext;
	private int choixActu;

	private DerouleObject derouleActu;
	private DerouleObject derouleNext;

	[SerializeField]
	private GameController _GameController;

	[SerializeField]
	private GameState _GameState;


	[SerializeField]
	private Image _dessin;

    [SerializeField]
    private Image _avatar;

    [SerializeField]
    private BalanceController _bc = null;

	//Texte simple lorsqu'il n'y a pas de choix
	[SerializeField]
	private Text _textLinéaire;

	//Texte lorsqu'il y a un choix
	[SerializeField]
	private Text _textChoix;

	[SerializeField]
	private GameObject _buttonChoix;

	[SerializeField]
	private GameObject _buttonChoixLong;

	[SerializeField]
	private GameObject _listeButtonChoix_1;
	[SerializeField]
	private GameObject _listeButtonChoix_2;
	[SerializeField]
	private GameObject _listeButtonChoix_3;

	[SerializeField]
	private GameObject _listeButtonChoixCourt;

	[SerializeField]
	private GameObject _listeButtonChoixLong;

	[SerializeField]
	private GameObject _panelDeroule;

	[SerializeField]
	private GameObject _panelDerouleTexte;

	[SerializeField]
	private GameObject _panelDialogue;

    [SerializeField]
    private GameObject _panelChapitre;

	[SerializeField]
	private GameObject _panelSwap;

    [SerializeField]
    private GameObject _panelTopDown;

	[SerializeField]
	private GameObject _panelSplitScreen;

	[SerializeField]
	private GameObject _swap1;

	[SerializeField]
	private GameObject _swap2;

    [SerializeField]
    private GameObject _swap3;

	[SerializeField]
	private GameObject _panelLettre;

	[SerializeField]
	private SwapController _swapController1;

	[SerializeField]
	private SwapController _swapController2;

    [SerializeField]
    private SwapController _swapController3;

    private Image _imageChapitre = null;

    private string[] _nomsimages = { "I101", "I201", "I301", "I346", "I401", "I401", "I501", "I701", "I801" };

    [SerializeField]
    private Text _chapterNumber;
    [SerializeField]
    private Text _chapterName;

	[SerializeField]
	private GameObject _fadeDessin;

	[SerializeField]
	private GameObject _fadePanelDeroule;

	[SerializeField]
	private GameObject _fadePanelDialogue;


	[SerializeField]
	private GameObject _panelFadeDessin;

	[SerializeField]
	private GameObject _panelFadeDeroule;

    [SerializeField]
    private AnimationsImagesController _aic = null;

    [SerializeField]
    private AudioController _audioSourceController = null;

    private Animator animFadeDessin;
	private Animator animFadeDeroule;

    //Fade entre 2 journées
    [SerializeField]
    private GameObject _panelFonduAuNoir = null;
    private int[] _idlancementPanel = {2054, 3030, 5018, 5035, 5055, 5116, 5170, 6041, 7007, 7020, 7033, 7246, 7058, 7074};

    [SerializeField]
    private GameObject _panelGameOver = null;
    [SerializeField]
    private Text _textGameOver = null;

    [SerializeField]
    private GameObject _panelMiniJeuNotes = null;

    [SerializeField]
    private GameObject _panelSEnSouviendra = null;
    private float _timerPanel = 3.0f;

    [SerializeField]
    private GameObject _panelDelibereCulpabilite = null;

    [SerializeField]
    private GameObject _panelAvisPeine = null;

    [SerializeField]
    private GameObject _panelDelibereAnnee = null;

    private bool _isNewGame = false;

    void Awake ()
	{
		animFadeDessin = _panelFadeDessin.GetComponent <Animator> ();

		animFadeDeroule = _panelDerouleTexte.GetComponent <Animator> ();
	}

    // Use this for initialization
    void Start () {



        _textChoix.supportRichText = true;
        _textLinéaire.supportRichText = true;
        _imageChapitre = _panelChapitre.GetComponent<Image>();

        int etatSauvegarde = PlayerPrefs.GetInt("PartieEnCours");
        if(etatSauvegarde == 0)
        {

		//	Debug.Log ("Nouvelle partie");
            _GameState.CreateDatas();
			_GameState.LoadDatas();
            _isNewGame = true;
        //	Debug.Log ("PlayerPref idActuel : "+PlayerPrefs.GetInt ("IDActuel"));
        //	Debug.Log ("GameState idActuel : " +GameState.GetIdEcranActuel());
        }
        else if (etatSauvegarde == 1)
        {
            _GameState.LoadDatas();
        }
        else
        {
         //   Debug.Log("ERROR");
        }


		DerouleConnexion(GameState.GetIdEcranActuel());

	}

    void Update()
    {
        if(_panelSEnSouviendra.activeInHierarchy == true)
        {
            _timerPanel -= Time.deltaTime;
            if(_timerPanel <= 0)
            {
                _panelSEnSouviendra.GetComponent<Image>().CrossFadeAlpha(0.0f, 0.4f, false);
                _panelSEnSouviendra.GetComponentInChildren<Text>().CrossFadeAlpha(0.0f, 0.4f, false);
                _timerPanel = 3.0f;
            }
        }
    }
    
    //Lancée lorsqu'on appuie sur le bouton "suivant" d'un déroulé linéaire,
    //permettant de passer au déroulé suivant
    public void OnClickLinéaire(){
        Debug.Log("OnClickLinear");
		idDerouleActuel = GameState.GetIdEcranActuel();

        UpdateJournalLinear(idDerouleActuel);
        UpdateLieuxLinear(idDerouleActuel);
        UpdateNotesLinear(idDerouleActuel);

        GameObject deroule = _GameController.GetDerouleObjectByID (idDerouleActuel);


		derouleActu = deroule.GetComponent<DerouleObject> ();

        if (derouleActu.GetRelationOption() != "-1")
        {
            if(derouleActu.GetRelationOption().Contains("Attention") == false)
            {
                int i;
                string[] parsed = derouleActu.GetRelationOption().Split(',');
                List<GameObject> journalNom = GameState.GetJournalPersonnage();
                for (i =0; i < journalNom.Count ; i++)
                {
                    if(parsed[0] == journalNom[i].GetComponent<PersonnageObject>().GetPrenom())
                    {
                        if (CheckRelationvalue(parsed[0], int.Parse(parsed[1])) == true)
                        {
                            StartCoroutine(NextDeroule(derouleActu.GetIDNextRO()));
                        }
                        else
                        {
                            StartCoroutine(NextDeroule(derouleActu.GetIDNext()));
                        }
                        break;
                    }
                }
            }
            else
            {
                string[] parsed = derouleActu.GetRelationOption().Split(',');
                if (GameState._attentionValue >= int.Parse(parsed[1]))
                {
                    StartCoroutine(NextDeroule(derouleActu.GetIDNextRO()));
                }
                else
                {
                    StartCoroutine(NextDeroule(derouleActu.GetIDNext()));
                }
            }

        }
        else
        {
            StartCoroutine(NextDeroule(derouleActu.GetIDNext()));
        }

	}

    /*Variables temporaires (A supprimer après la démo)*/
    public GameObject panelFinDemo = null;

    //sauf toi pour le moment
    public GameObject buttonURL = null;

	//Lance le prochain déroulé
	//idDerouleNext : id du prochain déroulé à afficher
	IEnumerator NextDeroule(int idDerouleNext){

        if (idDerouleNext < 9000)
        {

            //if temporaire
            if(idDerouleNext == 1030)
            {
                buttonURL.SetActive(true);
            }
            else if (buttonURL.activeInHierarchy == true)
            {
                buttonURL.SetActive(false);
            }

            if(idDerouleActuel == 5156)
            {
                GameState._attentionValue += GameState._quizzValue;
                switch (GameState._quizzValue)
                {
                    default:
                        break;
                    case 0:
                        idDerouleNext = 5463;
                        break;
                    case 1:
                        idDerouleNext = 5464;
                        break;
                    case 2:
                        idDerouleNext = 5465;
                        break;
                    case 3:
                        idDerouleNext = 5466;
                        break;
                    case 4:
                        idDerouleNext = 5467;
                        break;
                    case 5:
                        idDerouleNext = 5468;
                        break;

                }
            }
            else if(idDerouleActuel == 5162)
            {
                GameState._attentionValue += GameState._quizzValue;
                switch (GameState._quizzValue)
                {
                    default:
                        break;
                    case 0:
                        idDerouleNext = 5363;
                        break;
                    case 1:
                        idDerouleNext = 5364;
                        break;
                    case 2:
                        idDerouleNext = 5365;
                        break;
                    case 3:
                        idDerouleNext = 5366;
                        break;
                    case 4:
                        idDerouleNext = 5367;
                        break;
                    case 5:
                        idDerouleNext = 5368;
                        break;

                }
            }

		    CheckChapter(idDerouleNext);

            CheckPoint();

            StartCoroutine(FadeEndOfDay(idDerouleNext));
            
		    //On récupère l'ID du déroulé où l'on se trouve actuellement, pour en récupérer l'objet
		    idDerouleActuel = GameState.GetIdEcranActuel();

		    //Debug.Log ("idEcranActuel = "+idDerouleActuel.ToString ());


		    GameObject deroule = _GameController.GetDerouleObjectByID (idDerouleActuel);
		    derouleActu = deroule.GetComponent<DerouleObject> ();

            //On récupère l'objet du prochain déroulé
		    deroule= _GameController.GetDerouleObjectByID (idDerouleNext);

		    derouleNext = deroule.GetComponent<DerouleObject> ();


		    int fade = CheckFade(derouleActu,derouleNext,0);

		 //   Debug.Log ("fade : "+fade.ToString ()+ " id : "+idDerouleActuel.ToString());

		    //switch du fade première étape

		    switch (fade) {

		    case 0:

			    //On active le panel de l'image du fade
			    _panelFadeDessin.SetActive (true);

			    //On place à "vraie" le booléen de l'animation du fade pour la lancer
			    animFadeDessin.SetBool ("fade",true);
			    break;

		    case 1:

			    //On active le panel de l'image du fade, pour empêcher les joueurs de toucher le déroulé avant que l'animation ne soit finie
			    _panelFadeDeroule.SetActive (true);

			    //On place à "vraie" le booléen de l'animation du fade pour la lancer
			    animFadeDeroule.SetBool ("fade",true);
			    break;

		    case 2:

			    break;
		    }
		    //fin du switch du fade première étape



		    yield return new WaitForSeconds(0.3f);

            
            CheckAudio(derouleNext.GetAudio());

            //switch du fade seconde étape

            switch (fade) {

		    case 0:

			    //On place à "faux" le booléen de l'animation du fade pour éviter qu'il ne se relance en boucle
			    animFadeDessin.SetBool ("fade",false);
                break;

		    case 1:

			    //On place à "faux" le booléen de l'animation du fade pour éviter qu'il ne se relance en boucle
			    animFadeDeroule.SetBool ("fade",false);
			    break;

		    case 2:

			    break;
		    }
		    //fin du switch du fade seconde étape

	    //	Debug.Log ("idEcranNext = "+idDerouleNext.ToString ());

		    int minijeuActu = derouleActu.GetMiniJeu ();
		    int minijeuNext = derouleNext.GetMiniJeu();

		    //On récupère les choix du déroulé actuel et du prochain déroulé
		    //pour savoir quel type d'affichage retirer et afficher
		    choixNext = derouleNext.GetChoix ();
		    choixActu = derouleActu.GetChoix ();

		    //On change l'image du dessin s'il est différent
		    ChangeImage(derouleActu, derouleNext,0);
		    //On change l'image de l'avatar s'il est différent
            ChangeAvatar(derouleActu, derouleNext, 0);

//		    Debug.Log ("minijeu :" + minijeuNext.ToString ());
		    //On change le panel d'affichage s'il est différent
            ChangePanel (choixActu, choixNext,0, minijeuActu,minijeuNext);


		    //On vérifie s'il y a un choix ou non
			string iddDialogue;

		    switch (choixNext) {

		    case 0:
			
			    //On vérifie s'il faut modifier les valeurs d'émotion ou de conviction
			    CheckEmoConviDeroule (idDerouleNext);
                break;

		    case 1:
			
			    iddDialogue = derouleNext.GetIDDialogues ();
				CreateButtonChoix (iddDialogue,choixNext);

			    break;


			case 2:

				 iddDialogue = derouleNext.GetIDDialogues ();
				CreateButtonChoix (iddDialogue,choixNext);

				break;

		    }

		    //On change le texte à afficher
		    ChangeText (derouleNext, choixNext);
            
            _textLinéaire.CrossFadeAlpha(1.0f, 0.4f, false);
            _textLinéaire.color = new Color(_textLinéaire.color.r, _textLinéaire.color.g, _textLinéaire.color.b, 1.0f);

            _aic.CheckAnimation(derouleNext.GetID());
            //On met à jour les informations à sauvegarder
            GameState.SetIdEcranActuel (idDerouleNext);
		    GameState.UpdateDatas ();

    //		Debug.Log("Conviction : "+GameState.GetConviction ());



		    yield return new WaitForSeconds(0.4f);


		    //switch du fade troisième étape

		    switch (fade) {

		    case 0:

			    //On désactive le panel du fade
			    _panelFadeDessin.SetActive (false);
			    break;

		    case 1:

			    //On désactive le panel du fade
			    _panelFadeDeroule.SetActive (false);
			    break;

		    case 2:

			    break;
		    }
		    //fin du switch du fade troisième étape



			if (minijeuNext == 2) {

				StartCoroutine(NextDerouleTimer(1));

			}


		    yield return null;
        }
        else
        {
            StartCoroutine(GameOver(idDerouleNext));
        }

        
	}

	private void ChangeImage(DerouleObject DerouleActuel,DerouleObject DerouleNext, int start){

		string ImageActu = DerouleActuel.GetNomImage ();
		string ImageNext = DerouleNext.GetNomImage ();

		if (ImageActu != ImageNext || start == 1) {

		//	Debug.Log ("ImageNext : " + ImageNext);
			Sprite dessin = Resources.Load <Sprite> (ImageNext);
			_dessin.sprite = dessin;
				
		}

	}

	private void ChangeText(DerouleObject DerouleNext, int choix){

        string textNext = DerouleNext.GetTexte();

        //Cas ou on est après le Quizz
        if (DerouleNext.GetID() == 5162 || DerouleNext.GetID() == 5156)
        {
            textNext = "Le test est terminé. Vous avez donné " + GameState._quizzValue + " bonnes réponses sur cinq questions";
        }
        if(DerouleNext.GetID() == 7087/* || DerouleNext.GetID() == 7090 || DerouleNext.GetID() == 7093 || DerouleNext.GetID() == 7096*/)
        {
            textNext = "20. 20. 20. 15. 25. 20. 30. "+ DelibereAnneeController.GetNbAnnée() +". 20.";
        }

		if (choix == 0) {

            string textName = DerouleNext.GetPersonnage();
            if (textName == "Michel")
            {
                textName = "Président";
            }
            else if (textName == "Jules")
            {
                textName = "Avocat général";
            }
            else if (textName == "Valérie")
            {
                textName = "Greffière";
            }
            else if (textName == "Sonia")
            {
                textName = "Assesseur";
            }
            else if (textName == "Marie")
            {
                textName = "Avocate de la partie civile";
            }
            else if (textName == "Louis")
            {
                textName = "Avocat de la défense";
            }
            else if (textName == "Loïc")
            {
                textName = "Journaliste";
            }
            else if (textName == "Eloïse")
            {
                textName = "Enquêtrice";
            }
            else if (textName == "Jean-Pierre")
            {
                textName = "Médecin";
            }
            else if (textName == "Arthur")
            {
                textName = "Psychiatre";
            }
            else if (textName == "Elliot")
            {
                textName = "Psychologue";
            }
            else if (textName == "Julienpensif")
            {
                textName = "Julien";
            }
            if (DerouleNext.GetPersonnage() != "")
            {
			    _textLinéaire.text = "<color=#e74615>" + textName + " : </color>" + textNext;
            }
            else
            {
                _textLinéaire.text = textNext;
            }
		} else {
            _textChoix.text = textNext;
		}

	}

    private void ChangeAvatar(DerouleObject DerouleActuel, DerouleObject DerouleNext, int start)
    {
        string avatarActu = "";
        if (DerouleActuel.GetPersonnage() != "")
        {
            avatarActu = DerouleActuel.GetPersonnage();
        }
        string avatarNext = "";
        if (DerouleNext.GetPersonnage() != "")
        {
            avatarNext = DerouleNext.GetPersonnage();
        }

        if (avatarActu != avatarNext || start == 1)
        {
           // Debug.Log("ça marche");
            if (DerouleNext.GetPersonnage() != "")
            {
                Sprite dessin = Resources.Load<Sprite>("Images/Avatars/" + avatarNext);
                _avatar.sprite = dessin;  
            }
        }

    }

	private void CreateButtonChoix(string iddDialogue,int choix){


			//Ces trois boucles sont présentes pour détruire les boutons crées précédemment
			foreach (Transform child in _listeButtonChoix_1.transform) {

				GameObject.Destroy (child.gameObject);
			}


			foreach (Transform child in _listeButtonChoix_2.transform) {

				GameObject.Destroy (child.gameObject);
			}




			foreach (Transform child in _listeButtonChoix_3.transform) {

				GameObject.Destroy (child.gameObject);
			}



		GameObject buttonChoix;
		 
		var listeDialogue = iddDialogue.Split (',');
		int nbButton = listeDialogue.Length;
		GameObject dialogue;

		GameObject buttonChoixModèle;

		//On détermine quel type de bouton est utilisé : choix court ou long
		if (choix == 1) {

			buttonChoixModèle = _buttonChoix;


		} else {

			buttonChoixModèle = _buttonChoixLong;
		}

		for (int i = 0; i < nbButton; i++) {

			string idDialogue = listeDialogue[i];
//			Debug.Log ("idDialogue : " + idDialogue);
			dialogue = _GameController.GetDialogueObjectByID (idDialogue);

			DialogueObject dialogueObject = dialogue.GetComponent<DialogueObject> ();

			string textDialogue =  dialogueObject.GetTexte();


			buttonChoix = Instantiate (buttonChoixModèle, new Vector3 (0.0f, 0.0f, 0.0f), Quaternion.identity) as GameObject;

			buttonChoix.GetComponentInChildren<Text> ().text = textDialogue; 

			if (choix == 1) {

				if (i % 2 == 0) {
				
					buttonChoix.transform.SetParent (_listeButtonChoix_1.transform, false);	
				} else {

					buttonChoix.transform.SetParent (_listeButtonChoix_2.transform, false);	
				}

			} else {

				buttonChoix.transform.SetParent (_listeButtonChoix_3.transform, false);
			}


			Button b = buttonChoix.GetComponent<Button> ();

			int idNext = dialogueObject.GetIDNext ();

		//	Debug.Log ("Bouton " + i.ToString () + " : " + idNext.ToString ());

			AddListener (b, idNext,idDialogue);
		}

	}

	public void DerouleConnexion(int idDerouleActuel){

        CheckChapter(idDerouleActuel);

       // Debug.Log ("idEcranActuel = "+idDerouleActuel.ToString ());

		GameObject deroule = _GameController.GetDerouleObjectByID (idDerouleActuel);
		derouleActu = deroule.GetComponent<DerouleObject> ();

		choixActu = derouleActu.GetChoix ();


		int minijeu = derouleActu.GetMiniJeu ();

		ChangeImage(derouleActu, derouleActu,1);
        ChangeAvatar(derouleActu, derouleActu, 1);
        ChangePanel (choixActu, choixActu,1, -1,minijeu);


		//On vérifie s'il y a un choix ou non
		string iddDialogue;
		switch (choixActu) {

		case 0:

			break;

		case 1:

			iddDialogue = derouleActu.GetIDDialogues ();
			CreateButtonChoix (iddDialogue, choixActu);

			break;


		case 2:

			iddDialogue = derouleActu.GetIDDialogues ();
			CreateButtonChoix (iddDialogue,choixActu);

			break;
		}

        _aic.CheckAnimation(derouleActu.GetID());

        ChangeText (derouleActu, choixActu);
        if(_isNewGame == true)
        {
            StartCoroutine(_audioSourceController.ExploitAudioString(derouleActu.GetAudio()));
        }
        else if(_isNewGame == false)
        {
            StartCoroutine(_audioSourceController.ConnexionExploitAudioString(derouleActu.GetAudio()));
        }

        //Debug.Log ("Mini jeu :" + minijeu.ToString ());
        if (minijeu == 2) {
			Debug.Log ("test condition");
			StartCoroutine(NextDerouleTimer(1));
		}
	
	}

	void AddListener(Button b,int idNext,string idDialogue){

		b.onClick.AddListener (() => ChoixBouton(idNext,idDialogue));

	}

	public void ChoixBouton(int idNext,string idDialogue){

        UpdateJournalChoix(idDialogue);
        UpdateLieuxChoix(idDialogue);
        UpdateNotesChoix(idDialogue);
        if(idDialogue == "5151C" || idDialogue == "5152B" || idDialogue == "5153D" || idDialogue == "5154C" || idDialogue == "5155A" || idDialogue == "5157C" || idDialogue == "5158B" || idDialogue == "5159D" || idDialogue == "5160C" || idDialogue == "5161A")
        {
            GameState._quizzValue++;
        }
        DialogueObject dc = _GameController.GetDialogueObjectByID(idDialogue).GetComponent<DialogueObject>();

        CheckEmoConviDialogue (idDialogue);

        if(dc.GetRelationOption() != "-1")
        {
            if(dc.GetRelationOption().Contains("Attention") == false)
            {
                int i;
                string[] parsed = dc.GetRelationOption().Split(',');
                bool foundedCharaceter = false;
                List<GameObject> journalNom = GameState.GetJournalPersonnage();
                for (i = 0; i < journalNom.Count; i++)
                {
                    if (parsed[0] == journalNom[i].GetComponent<PersonnageObject>().GetPrenom())
                    {
                        foundedCharaceter = true;
                        if (CheckRelationvalue(parsed[0], int.Parse(parsed[1])) == true)
                        {
                            StartCoroutine(NextDeroule(dc.GetIDNextRO()));
                        }
                        else
                        {
                            StartCoroutine(NextDeroule(idNext));
                        }
                        break;
                    }
                }

                if(foundedCharaceter == false)
                {
                    StartCoroutine(NextDeroule(idNext));
                }
            }
            else
            {
                string[] parsed = dc.GetRelationOption().Split(',');
                if(GameState._attentionValue >= int.Parse(parsed[1]))
                {
                    StartCoroutine(NextDeroule(dc.GetIDNextRO()));
                }
                else
                {
                    StartCoroutine(NextDeroule(idNext));
                }
            }


        }
        else
        {
		    StartCoroutine(NextDeroule (idNext));
        }

	}

	private void ChangePanel(int choixActu, int choixNext, int start, int minijeuAct,int minijeuNext){

		//Début de la série de conditions qui vérifie si le déroulé à mettre en place est un mini-jeu

		//Mini jeu du swap de la lettre
		if (minijeuNext == 0) {
			_panelDeroule.SetActive (false);
			_panelSwap.SetActive (true);
			_swap1.SetActive (true);
		} 

		//Pas un mini jeu mais il fallait le caser quelque part, lettre du tribunal
		else if (minijeuNext == 1) {
			_panelDeroule.SetActive (false);
			_panelLettre.SetActive (true);
		} 

		//Ecran d'attente d'ouverture de la lettre
		else if (minijeuNext == 2) {
			_swap1.SetActive (false);
			_panelDeroule.SetActive (false);
			_panelSwap.SetActive (false);
			_swapController1.ResetSwap ();
		} 

		//Mini jeu du swap de bras levé pour aller à la prison
		else if (minijeuNext == 3) {
			_panelDeroule.SetActive (false);
			_panelSwap.SetActive (true);
			_swap2.SetActive (true);
		}
        //Mini jeu Top Down
        else if (minijeuNext == 4) {
			_panelDeroule.SetActive (false);
			_panelTopDown.SetActive (true);
		} else if (minijeuNext == 6) {
			_panelDeroule.SetActive (false);
			_panelSplitScreen.SetActive (true);
		}
        //Mini Jeu Notes
        else if(minijeuNext == 7)
        {
            _panelDeroule.SetActive(false);
            _panelMiniJeuNotes.SetActive(true);
        }
        //Mini jeu du swap de la lettre
        else if (minijeuNext == 8)
        {
            _panelDeroule.SetActive(false);
            _panelSwap.SetActive(true);
            _swap3.SetActive(true);
        }
        //Ecran délibéré Oui/Non
        else if (minijeuNext == 9)
        {
            _panelDeroule.SetActive(false);
            _panelDelibereCulpabilite.SetActive(true);
        }
        //Ecran Avis Délibéré
        else if (minijeuNext == 10)
        {
            _panelDeroule.SetActive(false);
            _panelAvisPeine.SetActive(true);
        }
        //Ecran Délibéré nombre d'année
        else if (minijeuNext == 11)
        {
            _panelDeroule.SetActive(false);
            _panelDelibereAnnee.SetActive(true);
        }

        //Fin de la série de condition qui vérifie si le déroulé à mettre en place est un mini-jeu

        //Dans le cas où le déroulé à mettre en place n'est pas un mini-jeu, on vérifie 
        //si la variable choix du déroulé actuel et de celui à mettre en place sont différents,
        //si on est au lancement du jeu ou si le déroulé actuel est un mini-jeu : si l'un de ces
        //cas est vrai, il va falloir effectuer un changement sur les panels

        else if(choixActu != choixNext || start == 1 || minijeuAct != -1){

			//Si le prochain déroulé à mettre en place est un déroulé linéaire
			if (choixNext == 0) {

                //Série de condition qui vérifie si le déroulé actuel est un mini-jeu qu'il faut désactiver
                if (minijeuAct == 0)
                {
                    _swap1.SetActive(false);
                    _panelSwap.SetActive(false);

                    _swapController1.ResetSwap();
                }
                else if (minijeuAct == 1)
                {
                    _panelLettre.SetActive(false);
                }
                else if (minijeuAct == 3)
                {
                    _swap2.SetActive(false);
                    _panelSwap.SetActive(false);

                    _swapController2.ResetSwap();
                }
                else if (minijeuAct == 4)
                {
                    _panelTopDown.SetActive(false);

                }
                else if (minijeuAct == 6)
                {
                    _panelSplitScreen.SetActive(false);
                }
                else if (minijeuNext == 7)
                {
                    _panelMiniJeuNotes.SetActive(false);
                }
                else if (minijeuAct == 8)
                {
                    _swap3.SetActive(false);
                    _panelSwap.SetActive(false);

                    _swapController3.ResetSwap();
                }
                else if (minijeuNext == 9)
                {
                    _panelDelibereCulpabilite.SetActive(false);
                }
                else if (minijeuNext == 10)
                {
                    _panelAvisPeine.SetActive(false);
                }
                else if (minijeuNext == 11)
                {
                    _panelDelibereAnnee.SetActive(false);
                }
                //Fin de la série de condition qui vérifie si le déroulé actuel est un mini-jeu qu'il faut désactiver

                //Si ce n'est pas actuellement un mini-jeu, on considère que c'est un dialogue, qu'il faut désactiver
                else
                {

                    _panelDialogue.SetActive(false);

                    if (choixActu == 1)
                    {

                        _listeButtonChoixCourt.SetActive(false);
                    }
                    else
                    {

                        _listeButtonChoixLong.SetActive(false);
                    }

                }

				//Après avoir tout désactivé, on active le déroulé linéaire
			    _panelDeroule.SetActive (true);

			//Si le prochain déroulé à mettre en place n'est pas un déroulé linéaire, ni un mini-jeu, alors c'est un dialogue, qu'il faut ré-activer
			} else {
				
				_panelDeroule.SetActive (false);
				_panelDialogue.SetActive (true);

				if (choixNext == 1) {

					_listeButtonChoixCourt.SetActive (true);
				} else {

					_listeButtonChoixLong.SetActive (true);
				}


			}

		}


	}

    IEnumerator LaunchStartChapter()
    {

        //Mise en place des éléments pour le Panel Chapitre
        _chapterNumber.text = "Chapitre " + GameState.GetCurrentChapter();
        _chapterName.text = GameState.GetChapterNameByID(GameState.GetCurrentChapter() - 1);
        _imageChapitre.sprite = Resources.Load <Sprite> ("Images/Histoire/" + _nomsimages[GameState.GetCurrentChapter() - 1]);

        //On s'assure que l'alpha soit bien regler
        _panelChapitre.GetComponent<Image>().color += new Color(0.0f, 0.0f, 0.0f, 1.0f);
        _chapterNumber.color += new Color(0.0f, 0.0f, 0.0f, 1.0f);
        _chapterName.color += new Color(0.0f, 0.0f, 0.0f, 1.0f);

        
        _panelChapitre.transform.SetAsLastSibling();
        _panelFonduAuNoir.GetComponent<Image>().canvasRenderer.SetAlpha(1.0f);
        _panelFonduAuNoir.SetActive(true);
        _panelFonduAuNoir.transform.SetAsLastSibling();

        yield return new WaitForSeconds(2.0f);

        _panelChapitre.SetActive(true);
        _panelFonduAuNoir.GetComponent<Image>().CrossFadeAlpha(0.0f, 0.4f, false);

        yield return new WaitForSeconds(0.5f);

        _panelFonduAuNoir.SetActive(false);

        yield return new WaitForSeconds(2.0f);

        _panelChapitre.GetComponent<Image>().CrossFadeAlpha(0.0f,1.0f,false);
        _chapterNumber.CrossFadeAlpha(0.0f, 1.01f, false);
        _chapterName.CrossFadeAlpha(0.0f, 1.01f, false);

        yield return new WaitForSeconds(1.1f);

        _panelChapitre.SetActive(false);

        _panelChapitre.GetComponent<Image>().color += new Color(0.0f, 0.0f, 0.0f, 1.0f);
        _chapterNumber.color += new Color(0.0f, 0.0f, 0.0f, 1.0f);
        _chapterName.color += new Color(0.0f, 0.0f, 0.0f, 1.0f);

        yield return null;
    }

	void CheckChapter(int id)
	{
		int[] idChapter = GameState.GetIDStartChapter();
        bool hasChapterChange = false;
		for (int i = 0; i < idChapter.Length; i++)
		{
            if(hasChapterChange == false)
            {
			    if (idChapter[i] == id)
			    {
                    if (i < 2)
                    {
                            GameState.SetCurrentChapter(i + 1);
                            hasChapterChange = true;
                    }
                    else if (i >= 2)
                    {
                            GameState.SetCurrentChapter(i);
                            hasChapterChange = true;
                    }
				
			    }
            }
            else
            {
                break;
            }
		}
        if(hasChapterChange == true)
        {
            StartCoroutine(LaunchStartChapter());
        }
	}

	private void CheckEmoConviDeroule(int id){

		GameObject gameObj;


		gameObj = _GameController.GetDerouleObjectByID (id);
		DerouleObject obj = gameObj.GetComponent<DerouleObject> ();


		int emotion = obj.GetModEmotion ();

		if (emotion != 0) {

			GameState.UpdateEmotion (emotion);

		}

		int conviction = obj.GetModConviction ();

		if (conviction != 0) {

			GameState.UpdateConviction (conviction);

		}

        if (emotion != 0 || emotion != 0)
        {
            _bc.UpdateBalance();
        }

    }

	private void CheckEmoConviDialogue(string id){

		GameObject gameObj = _GameController.GetDialogueObjectByID (id);

		DialogueObject obj = gameObj.GetComponent<DialogueObject> ();


		int emotion = obj.GetModEmotion ();
		//Debug.Log ("emotion modif : " + emotion);

		if (emotion != 0) {
				

			GameState.UpdateEmotion (emotion);

		}

		int conviction = obj.GetModConviction ();
		//Debug.Log ("Conviction modif : " + conviction);

		if (conviction != 0) {
				

			GameState.UpdateConviction (conviction);


		}

        if(emotion != 0 || conviction != 0)
        {
            _bc.UpdateBalance();
        }

	}

    private void UpdateJournalLinear(int id)
    {
        GameObject go = _GameController.GetDerouleObjectByID(id);
        if(go.GetComponent<DerouleObject>().GetJournal() != "-1")
        {
            if(go.GetComponent<DerouleObject>().GetJournal() != "Attention")
            {
                bool exist = false;
                string[] parsed = go.GetComponent<DerouleObject>().GetJournal().Split(',');
               // Debug.Log("prenom : " + parsed[0]);
               // Debug.Log("Valeur : " + parsed[1]);
                int i;
                List<GameObject> journalNom = GameState.GetJournalPersonnage();
                for (i = 0; i < journalNom.Count; i++)
                {
                    if (parsed[0] == journalNom[i].GetComponent<PersonnageObject>().GetPrenom())
                    {
                        GameState.SetPersonnageValueByPrenom(parsed[0], int.Parse(parsed[1]));
                        exist = true;
                        break;
                    }
                }
                if (exist == false)
                {
                    _GameState.AddJournalPersonnage(parsed[0], int.Parse(parsed[1]));
                }
                if(int.Parse(parsed[1]) > 1 && int.Parse(parsed[1]) <= 3)
                {
                    if(parsed[0] != "Julien" && parsed[0] != "Sonia" && parsed[0] != "Sophie" && parsed[0] != "Charles-Henri")
                    {
                        _panelSEnSouviendra.GetComponent<Image>().canvasRenderer.SetAlpha(0.0f);
                        _panelSEnSouviendra.GetComponentInChildren<Text>().canvasRenderer.SetAlpha(0.0f);
                        _panelSEnSouviendra.GetComponentInChildren<Text>().text = parsed[0] + " s'en souviendra";
                        _panelSEnSouviendra.SetActive(true);
                        _panelSEnSouviendra.GetComponent<Image>().CrossFadeAlpha(0.5f,0.4f,false);
                        _panelSEnSouviendra.GetComponentInChildren<Text>().CrossFadeAlpha(1.0f, 0.4f, false);

                    }
                }
            }
            else
            {
                GameState._attentionValue++;
            }
        }
    }

    private void UpdateJournalChoix(string id)
    {
        GameObject go = _GameController.GetDialogueObjectByID(id);
        if (go.GetComponent<DialogueObject>().GetJournal() != "-1")
        {
            if (go.GetComponent<DialogueObject>().GetJournal() != "Attention")
            {
                bool exist = false;
                string[] parsed = go.GetComponent<DialogueObject>().GetJournal().Split(',');
                Debug.Log("prenom : " + parsed[0]);
                Debug.Log("Valeur : " + parsed[1]);
                int i;
                List<GameObject> journalNom = GameState.GetJournalPersonnage();
                for (i = 0; i < journalNom.Count; i++)
                {
                    if (parsed[0] == journalNom[i].GetComponent<PersonnageObject>().GetPrenom())
                    {
                        GameState.SetPersonnageValueByPrenom(parsed[0], int.Parse(parsed[1]));
                        exist = true;
                        break;
                    }
                }
                if (exist == false)
                {
                    _GameState.AddJournalPersonnage(parsed[0], int.Parse(parsed[1]));
                }
            }
            else
            {
                GameState._attentionValue++;
            }
        }
    }

    private void UpdateLieuxLinear(int id)
    {
        GameObject go = _GameController.GetDerouleObjectByID(id);
        if (go.GetComponent<DerouleObject>().GetLieux() != "-1")
        {
            _GameState.AddJournalLieu(go.GetComponent<DerouleObject>().GetLieux());
        }
    }

    private void UpdateLieuxChoix(string id)
    {
        GameObject go = _GameController.GetDialogueObjectByID(id);
        if (go.GetComponent<DialogueObject>().GetLieux() != "-1")
        {
            _GameState.AddJournalLieu(go.GetComponent<DialogueObject>().GetLieux());
        }
    }

    private void UpdateNotesLinear(int id)
    {
        GameObject go = _GameController.GetDerouleObjectByID(id);
        if (go.GetComponent<DerouleObject>().GetNote() != -1)
        {
            _GameState.AddJournalNote(go.GetComponent<DerouleObject>().GetNote());
        }
    }

    private void UpdateNotesChoix(string id)
    {
        GameObject go = _GameController.GetDialogueObjectByID(id);
        if (go.GetComponent<DialogueObject>().GetNote() != -1)
        {
            _GameState.AddJournalNote(go.GetComponent<DialogueObject>().GetNote());
        }
    }

    private bool CheckRelationvalue(string prenom, int valueTotest)
    {
        PersonnageObject po = GameState.GetJournalPersonnageByPrenom(prenom).GetComponent<PersonnageObject>();

        if(po != null)
        {
            if (valueTotest == 1)
            {
                if(po.GetValueRelation() == 1 || po.GetValueRelation() == 2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if(valueTotest == 2)
            {
                if (po.GetValueRelation() == 2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (valueTotest == 3)
            {
                if (po.GetValueRelation() == 3)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        return false;
    }

    private void CheckPoint()
    {
        foreach(int i in GameState.GetIDSCheckpoints())
        {
            if(i == GameState.GetIdEcranActuel())
            {
                GameState.UpdateCheckPoint();
                break;
            }
        }
    }

    private void CheckAudio(string audio)
    {
        if(audio != "-1")
        {
            StartCoroutine(_audioSourceController.ExploitAudioString(audio));
        }

    }

	//Renvoie quel type de fade il faut appliquer :
	//0 : Fade tout le dessin
	//1 : fade le déroulé
	//2 : fade le dialogue
	private int CheckFade(DerouleObject DerouleActuel,DerouleObject DerouleNext, int start){

		int fade;
		int choixActu;
		int choixNext;

		string ImageActu = DerouleActuel.GetNomImage ();
		string ImageNext = DerouleNext.GetNomImage ();

		if (ImageActu != ImageNext || start == 1) {

			fade = 0;

		} else {

			choixActu = DerouleActuel.GetChoix ();
			choixNext = DerouleNext.GetChoix ();

			if (choixActu == 0 && choixNext == 0) {
				fade = 1;

			} else {

				fade = 0;
			}
		}


		return fade;

	}

    public void NewGame()
    {
        _GameState.ResetList();
        _GameState.CreateDatas();
        _GameState.LoadDatas();
        _bc.UpdateBalance();
        panelFinDemo.SetActive(false);
        _panelGameOver.SetActive(false);
        DerouleConnexion(GameState.GetIdEcranActuel());
    }

    public void LoadLastCheckpoint()
    {
        _GameState.ResetList();
        _GameState.LoadCheckPointDatas();
        _bc.UpdateBalance();
        _panelGameOver.SetActive(false);
        DerouleConnexion(GameState.GetIdEcranActuel());
    }

	public IEnumerator NextDerouleTimer(int timer){

		Debug.Log ("test avant");

		yield return new WaitForSeconds(timer);

		Debug.Log ("test après");
		OnClickLinéaire ();

		yield return null;
	}

    //Fade utilisé pour marqué la séparation entre 2 journée (voir chap 2 ID 2053-2054)
    IEnumerator FadeEndOfDay(int id)
    {
        bool shouldFade = false;

        foreach (int numb in _idlancementPanel)
        {
            if(id == numb)
            {
                shouldFade = true;
                break;
            }
        }

        if(shouldFade == true)
        {
            _panelFonduAuNoir.SetActive(true);
            _panelFonduAuNoir.GetComponent<Image>().canvasRenderer.SetAlpha(0.0f);
            _panelFonduAuNoir.GetComponent<Image>().CrossFadeAlpha(1.0f, 0.4f, false);
            yield return new WaitForSeconds(2.0f);
            _panelFonduAuNoir.GetComponent<Image>().CrossFadeAlpha(0.0f, 0.4f, false);
            yield return new WaitForSeconds(0.5f);
            _panelFonduAuNoir.SetActive(false);
        }

        yield return null;
    }

    //Permet de mettre en place le panel de gameOver, avec le bon texte,  
    //sauvegardé dans le xml gameover.
    //En utilisant des IDNext supérieur a 9000 (non utilisé par l'histoire)
    IEnumerator GameOver(int id)
    {
        XmlNode goodNode = null;
        foreach(XmlNode node in _GameController.GetGameOverList())
        {
            if(int.Parse(node.Attributes["ID"].Value) == id)
            {
                goodNode = node;
                break;
            }
        }

        if(goodNode != null)
        {
            yield return new WaitForSeconds(0.5f);
            _panelGameOver.SetActive(true);
            _textGameOver.text = goodNode.Attributes["Text"].Value;
        }

        yield return null;
    }

    public void LinearFromDelibere()
    {
        OnClickLinéaire();
    }

    public void DeactivatePanelDerouleAndDialogue()
    {
        _panelDeroule.SetActive(false);
        _panelDialogue.SetActive(false);
    }

    public void LaunchingNextDerouleFromAnimationController()
    {
        OnClickLinéaire();
    }

    //Used on AvisPeineController.cs
    public void NextDerouleForDelibere(int IDNext)
    {
        StartCoroutine(NextDeroule(IDNext));
    }
}
