using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CarnetState
{
    None = 0,
    Note = 1,
    Personnage = 2,
    Lieux = 3,
    Tableau = 4
}

public class CarnetController : MonoBehaviour {

    private CarnetState _carnetState = 0;

    [SerializeField]
    private Sprite[] _images = null;

    [SerializeField]
    private Image _carnetImage = null;

    [SerializeField]
    private GameObject _buttonNote = null;
    [SerializeField]
    private GameObject _buttonPersonnageAvant = null;
    [SerializeField]
    private GameObject _buttonPersonnageArriere = null;
    [SerializeField]
    private GameObject _buttonLieuxAvant = null;
    [SerializeField]
    private GameObject _buttonLieuxArriere = null;
    [SerializeField]
    private GameObject _buttonTableau = null;

    [SerializeField]
    private GameObject _panelNote = null;
    [SerializeField]
    private GameObject _panelPersonnage = null;
    [SerializeField]
    private GameObject _panelLieux = null;
    [SerializeField]
    private GameObject _panelTableau = null;

    /*Varibables for Note*/
    [SerializeField]
    private int _notePageNumber = 1;
    private int _noteMaxPage = 1;

    /*Varibables for Personnage*/
    [SerializeField]
    private int _personnageNbOfItemsByPage = 10;
    private int _personnagePageNumber = 1;
    private int _personnageMaxPage = 1;

    [SerializeField]
    private Image[] _listOfImagesPersonnage;
    [SerializeField]
    private Image[] _listOfImagesLieux;

    /*Variables for Lieux*/
    [SerializeField]
    private int _lieuxNbOfItemsByPage = 10;
    private int _lieuxPageNumber = 1;
    private int _lieuxMaxPage = 1;

    /*Variables for tableau*/
    public Text _textDecharge = null;
    public Text _textCharge = null;
    public Text _textDoutes = null;

    /*variables for SubPanels*/

    [SerializeField]
    private GameObject _panelPersoInfo = null;
    [SerializeField]
    private Text _texteNom = null;
    [SerializeField]
    private Text _texteDescription = null;

    [SerializeField]
    private GameObject _panelLieuInfo = null;
    [SerializeField]
    private Text _textNomLieu = null;
    [SerializeField]
    private Text _texteDescriptionLieu = null;

    [SerializeField]
    private Text _dateNotes = null;
    [SerializeField]
    private Text _contenuNotes1 = null;
    [SerializeField]
    private Text _contenuNotes2 = null;

    [SerializeField]
    private GameObject _nextButton = null;
    [SerializeField]
    private GameObject _previousButton = null;

    // Use this for initialization
    void Start () {
        //On s'assure que tout soit désactiver avant de charger les pages notes
        foreach (Image img in _listOfImagesPersonnage)
        {
            img.transform.parent.gameObject.SetActive(false);
        }
        foreach (Image img in _listOfImagesLieux)
        {
            img.transform.parent.gameObject.SetActive(false);
        }
        MaxPageLieux();
        MaxPageNote();
        MaxPagePersonnage();
        _carnetState = CarnetState.Note;
        ChangeState(1);
	}

    private void OnEnable()
    {
        switch (_carnetState)
        {
            default:
                break;
            case CarnetState.Note:
                LoadPageNote(_notePageNumber);
                break;
            case CarnetState.Personnage:
                LoadPagePersonnage(_personnagePageNumber);
                break;
            case CarnetState.Lieux:
                LoadPageLieux(_lieuxPageNumber);
                break;
            case CarnetState.Tableau:
                break;
        }
    }

    //permet de changer d'état, et ensuite de charger la page actuelle
    public void ChangeState(int newState)
    {
        switch (newState)
        {
            default:
                break;
            case 1:
                _carnetState = CarnetState.Note;
                _panelNote.SetActive(true);
                _panelPersonnage.SetActive(false);
                _panelLieux.SetActive(false);
                _panelTableau.SetActive(false);
                LoadPageNote(_notePageNumber);
                break;
            case 2:
                _carnetState = CarnetState.Personnage;
                _panelNote.SetActive(false);
                _panelPersonnage.SetActive(true);
                _panelLieux.SetActive(false);
                _panelTableau.SetActive(false);
                LoadPagePersonnage(_personnagePageNumber);
                break;
            case 3:
                _carnetState = CarnetState.Lieux;
                _panelNote.SetActive(false);
                _panelPersonnage.SetActive(false);
                _panelLieux.SetActive(true);
                _panelTableau.SetActive(false);
                LoadPageLieux(_lieuxPageNumber);
                break;
            case 4:
                _carnetState = CarnetState.Tableau;
                _panelNote.SetActive(false);
                _panelPersonnage.SetActive(false);
                _panelLieux.SetActive(false);
                _panelTableau.SetActive(true);
                UpdateButtonChangingPages();
                break;
        }
        UpdateButton();
        UpdateImage();
    }

    //Function used when changing state

    void UpdateButton()
    {
        if(_carnetState == CarnetState.Note)
        {
            _buttonNote.SetActive(false);
            _buttonPersonnageAvant.SetActive(true);
            _buttonPersonnageArriere.SetActive(false);
            _buttonLieuxAvant.SetActive(true);
            _buttonLieuxArriere.SetActive(false);
            _buttonTableau.SetActive(true);
        }
        else if (_carnetState == CarnetState.Personnage)
        {
            _buttonNote.SetActive(true);
            _buttonPersonnageAvant.SetActive(false);
            _buttonPersonnageArriere.SetActive(false);
            _buttonLieuxAvant.SetActive(true);
            _buttonLieuxArriere.SetActive(false);
            _buttonTableau.SetActive(true);
        }
        else if (_carnetState == CarnetState.Lieux)
        {
            _buttonNote.SetActive(true);
            _buttonPersonnageAvant.SetActive(false);
            _buttonPersonnageArriere.SetActive(true);
            _buttonLieuxAvant.SetActive(false);
            _buttonLieuxArriere.SetActive(false);
            _buttonTableau.SetActive(true);
        }
        else if (_carnetState == CarnetState.Tableau)
        {
            _buttonNote.SetActive(true);
            _buttonPersonnageAvant.SetActive(false);
            _buttonPersonnageArriere.SetActive(true);
            _buttonLieuxAvant.SetActive(false);
            _buttonLieuxArriere.SetActive(true);
            _buttonTableau.SetActive(false);
        }
        else
        {

        }
    }

    void UpdateImage()
    {
        switch (_carnetState)
        {
            default:
                break;

            case CarnetState.Note:
                _carnetImage.sprite = _images[0];
                break;

            case CarnetState.Personnage:
                _carnetImage.sprite = _images[1];
                break;

            case CarnetState.Lieux:
                _carnetImage.sprite = _images[2];
                break;

            case CarnetState.Tableau:
                _carnetImage.sprite = _images[3];
                break;
        }
    }

    void UpdatePanel()
    {
        if (_carnetState == CarnetState.Note)
        {
            _panelNote.SetActive(true);
            _panelPersonnage.SetActive(false);
            _panelLieux.SetActive(false);
        }
        else if (_carnetState == CarnetState.Personnage)
        {
            _panelNote.SetActive(false);
            _panelPersonnage.SetActive(true);
            _panelLieux.SetActive(false);

        }
        else if (_carnetState == CarnetState.Lieux)
        {
            _panelNote.SetActive(false);
            _panelPersonnage.SetActive(false);
            _panelLieux.SetActive(true);
        }
        else if (_carnetState == CarnetState.Tableau)
        {
            _panelNote.SetActive(false);
            _panelPersonnage.SetActive(false);
            _panelLieux.SetActive(false);
        }
        else
        {

        }
    }
    
    //Function to load one page

    void LoadPageNote(int page)
    {
        if(_noteMaxPage != 0)
        {
            _notePageNumber = page;
            int i = 0;
            int j;
            List<GameObject> ln = GameState.GetJournalNotes();
            for( j = 0 ; j < ln.Count; j++)
            {
                NoteObject no =ln[j].GetComponent<NoteObject>();
                if(no.GetTitre() == 1)
                {
                    i++;
                    if(i == _notePageNumber)
                    {
                        _dateNotes.text = ln[j].GetComponent<NoteObject>().GetContenu();
                        i = j + 1;
                        break;
                    }
                }
            }
            int nbOfLineAvailable = 28;
            bool hasNext;
            if(ln[i].GetComponent<NoteObject>().GetTitre() == 0)
            {
                hasNext = true;
            }
            else
            {
                hasNext = false;
            }
            while (nbOfLineAvailable > 0 && hasNext == true && i < GameState.GetJournalNotes().Count)
            {
                NoteObject no = ln[i].GetComponent<NoteObject>();
                Debug.Log(no.GetNombreLigne());
                if(no.GetTitre() == 0)
                {
                    if(nbOfLineAvailable > 14 && nbOfLineAvailable - no.GetNombreLigne() > 14)
                    {
                        if(nbOfLineAvailable == 28)
                        {
                            _contenuNotes1.text = no.GetContenu();
                            nbOfLineAvailable -= no.GetNombreLigne();
                            if (i + 1 < GameState.GetJournalNotes().Count)
                            {
                                if (ln[i+1].GetComponent<NoteObject>().GetTitre() == 0)
                                {
                                    i++;
                                }
                                else
                                {
                                    hasNext = false;
                                }
                            }
                            else
                            {
                                hasNext = false;
                            }
                        }
                        else
                        {
                            _contenuNotes1.text += "\n" + no.GetContenu();
                            nbOfLineAvailable -= no.GetNombreLigne(); 
                            if(i + 1 < GameState.GetJournalNotes().Count)
                            {
                                if (ln[i + 1].GetComponent<NoteObject>().GetTitre() == 0)
                                {
                                    i++;
                                }
                                else
                                {
                                    hasNext = false;
                                }
                            }
                            else
                            {
                                hasNext = false;
                            }
                        }
                    }
                    else if ((nbOfLineAvailable > 14) && (nbOfLineAvailable - no.GetNombreLigne() < 15))
                    {
                        _contenuNotes2.text = no.GetContenu();
                        nbOfLineAvailable = 14 - no.GetNombreLigne();
                        if (i + 1 < GameState.GetJournalNotes().Count)
                        {
                            if (ln[i + 1].GetComponent<NoteObject>().GetTitre() == 0)
                            {
                                i++;
                            }
                            else
                            {
                                hasNext = false;
                            }
                        }
                        else
                        {
                            hasNext = false;
                        }
                    }
                    else if(nbOfLineAvailable - no.GetNombreLigne() > 0)
                    {
                        _contenuNotes2.text += "\n" + no.GetContenu();
                        nbOfLineAvailable -= no.GetNombreLigne();
                        if (i + 1 < GameState.GetJournalNotes().Count)
                        {
                            if (ln[i + 1].GetComponent<NoteObject>().GetTitre() == 0)
                            {
                                i++;
                            }
                            else
                            {
                                hasNext = false;
                            }
                        }
                        else
                        {
                            hasNext = false;
                        }
                    }
                    else
                    {
                        hasNext = false;
                    }
                }
                else
                {
                    i++;
                }
            }
        }
        UpdateButtonChangingPages();
    }

    void LoadPagePersonnage(int page)
    {
        _personnagePageNumber = page;
        //premier item page = item par page * (numero page - 1)
        if(page > 0 && page <= _personnageMaxPage)
        {
            List<GameObject> go = GameState.GetJournalPersonnage();
            int nbOfPerso = go.Count;
            if (page == _personnageMaxPage && nbOfPerso % _personnageNbOfItemsByPage != 0)
            {
                int nbOfImages = nbOfPerso % _personnageNbOfItemsByPage;
                for(int i =0; i < nbOfPerso % _personnageNbOfItemsByPage; i++)
                {
                    if(i < nbOfImages)
                    {
                        _listOfImagesPersonnage[i].transform.parent.gameObject.SetActive(true);
                        _listOfImagesPersonnage[i].sprite = Resources.Load<Sprite>("Images/Avatars/" + go[_personnageNbOfItemsByPage * (_personnagePageNumber - 1) + i].GetComponent<PersonnageObject>().GetPrenom());
                    }
                    else
                    {
                        _listOfImagesPersonnage[i].transform.parent.gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                for (int i = 0; i < _personnageNbOfItemsByPage; i++)
                {
                    _listOfImagesPersonnage[i].transform.parent.gameObject.SetActive(true);
                    _listOfImagesPersonnage[i].sprite = Resources.Load<Sprite>("Images/Avatars/" + go[_personnageNbOfItemsByPage*(_personnagePageNumber-1)+i].GetComponent<PersonnageObject>().GetPrenom());
                }
            }
        }

        UpdateButtonChangingPages();
    }

    void LoadPageLieux(int page)
    {
        _lieuxPageNumber = page;
        if (page <= _lieuxMaxPage && page > 0)
        {
            List<GameObject> go = GameState.GetJournalLieux();
            int nbOfLieux = go.Count;
            if (page == _lieuxMaxPage && nbOfLieux % _lieuxNbOfItemsByPage != 0)
            {
                int nbOfImages = nbOfLieux % _lieuxNbOfItemsByPage;
                for (int i = 0; i < _listOfImagesLieux.Length; i++)
                {
                    if (i < nbOfImages)
                    {
                        _listOfImagesLieux[i].transform.parent.gameObject.SetActive(true);
                        _listOfImagesLieux[i].sprite = Resources.Load<Sprite>("Images/Lieux/" + go[i].GetComponent<LieuObject>().GetNom());
                    }
                    else
                    {
                        _listOfImagesLieux[i].transform.parent.gameObject.SetActive(false);
                    }
                }
            }
            else
            {

            }
        }

        UpdateButtonChangingPages();
    }

    //Funtion to load Next or previous page

    public void LoadNextPage()
    {
        switch (_carnetState)
        {
            default:
                break;
            case CarnetState.Lieux:
                LoadPageLieux(_lieuxPageNumber +1);
                break;
            case CarnetState.Note:
                LoadPageNote(_notePageNumber + 1);
                break;
            case CarnetState.Personnage:
                LoadPagePersonnage(_personnagePageNumber + 1);
                break;
        }
    }

    public void LoadPreviousPage()
    {
        switch (_carnetState)
        {
            default:
                break;
            case CarnetState.Lieux:
                LoadPageLieux(_lieuxPageNumber - 1);
                break;
            case CarnetState.Note:
                LoadPageNote(_notePageNumber - 1);
                break;
            case CarnetState.Personnage:
                LoadPagePersonnage(_personnagePageNumber - 1);
                break;
        }
    }

    void UpdateButtonChangingPages()
    {
        switch (_carnetState)
        {
            default:
                break;
            case CarnetState.Lieux:
                if(_lieuxPageNumber + 1 > _lieuxMaxPage)
                {
                    if (_nextButton.activeInHierarchy == true)
                    {
                        _nextButton.SetActive(false);
                    }
                }
                else
                {
                    if (_nextButton.activeInHierarchy == false)
                    {
                        _nextButton.SetActive(true);
                    }
                }

                if(_lieuxPageNumber - 1 < 1)
                {
                    if (_previousButton.activeInHierarchy == true)
                    {
                        _previousButton.SetActive(false);
                    }
                }
                else
                {
                    if (_previousButton.activeInHierarchy == false)
                    {
                        _previousButton.SetActive(true);
                    }
                }
                break;
            case CarnetState.Note:
                if (_notePageNumber + 1 > _noteMaxPage)
                {
                    if (_nextButton.activeInHierarchy == true)
                    {
                        _nextButton.SetActive(false);
                    }
                }
                else
                {
                    if (_nextButton.activeInHierarchy == false)
                    {
                        _nextButton.SetActive(true);
                    }
                }

                if (_notePageNumber - 1 < 1)
                {
                    if (_previousButton.activeInHierarchy == true)
                    {
                        _previousButton.SetActive(false);
                    }
                }
                else
                {
                    if (_previousButton.activeInHierarchy == false)
                    {
                        _previousButton.SetActive(true);
                    }
                }
                break;
            case CarnetState.Personnage:
                if (_personnagePageNumber + 1 > _personnageMaxPage)
                {
                    if (_nextButton.activeInHierarchy == true)
                    {
                        _nextButton.SetActive(false);
                    }
                }
                else
                {
                    if (_nextButton.activeInHierarchy == false)
                    {
                        _nextButton.SetActive(true);
                    }
                }

                if (_personnagePageNumber - 1 < 1)
                {
                    if (_previousButton.activeInHierarchy == true)
                    {
                        _previousButton.SetActive(false);
                    }
                }
                else
                {
                    if (_previousButton.activeInHierarchy == false)
                    {
                        _previousButton.SetActive(true);
                    }
                }

                break;
            case CarnetState.Tableau:
                _nextButton.SetActive(false);
                _previousButton.SetActive(false);
                break;
        }
    }

    //Function to calcul the number max of page

    public void MaxPageNote()
    {
        int nbPage = 0;
        foreach(GameObject go in GameState.GetJournalNotes())
        {
            NoteObject no = go.GetComponent<NoteObject>();
            if(no.GetTitre() == 1)
            {
                nbPage ++;
            }
        }
        _noteMaxPage = nbPage;
    }

    public void MaxPagePersonnage()
    {
        int nbOfPerso = GameState.GetJournalPersonnage().Count;
        if(nbOfPerso / _personnageNbOfItemsByPage != 0)
        {
            if(nbOfPerso % _personnageNbOfItemsByPage == 0)
            {
                _personnageMaxPage = nbOfPerso / _personnageNbOfItemsByPage;
            }
            else if (nbOfPerso % _personnageNbOfItemsByPage != 0)
            {
                _personnageMaxPage = nbOfPerso / _personnageNbOfItemsByPage + 1;
            }
        }
        else if (nbOfPerso / _personnageNbOfItemsByPage == 0)
        {
            _personnageMaxPage = 1;
        }
    }

    public void MaxPageLieux()
    {
        _lieuxMaxPage = 1;
    }

    //Function to display informations

    public void DisplayPersonnageDescription(int place)
    {
        _texteNom.text = GameState.GetJournalPersonnage()[(_personnageNbOfItemsByPage*(_personnagePageNumber-1))+place].GetComponent<PersonnageObject>().GetPrenom() + " " + GameState.GetJournalPersonnage()[(_personnageNbOfItemsByPage * (_personnagePageNumber - 1)) + place].GetComponent<PersonnageObject>().GetNom();
        _texteDescription.text = GameState.GetJournalPersonnage()[(_personnageNbOfItemsByPage * (_personnagePageNumber - 1)) + place].GetComponent<PersonnageObject>().GetDescription();
        _panelPersoInfo.SetActive(true);
    }

    public void DisplayLieuDescription(int place)
    {
        _textNomLieu.text = GameState.GetJournalLieux()[(_lieuxNbOfItemsByPage * (_lieuxPageNumber - 1)) + place].GetComponent<LieuObject>().GetNom();
        _texteDescriptionLieu.text = GameState.GetJournalLieux()[(_lieuxNbOfItemsByPage * (_lieuxPageNumber - 1)) + place].GetComponent<LieuObject>().GetDescription();
        _panelLieuInfo.SetActive(true);
    }

    public int GetCurrentState()
    {

        if (_carnetState == CarnetState.Note)
        {
            return 1;
        }
        else if (_carnetState == CarnetState.Personnage)
        {
            return 2;
        }
        else if (_carnetState == CarnetState.Lieux)
        {
            return 3;
        }
        else if (_carnetState == CarnetState.Tableau)
        {
            return 4;
        }

        return 0;

    }
}
