using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;

public class GameState : MonoBehaviour {

    /*Script qui va garder en mémoire les valeurs de la session en cours*/
    private static int _iDEcranActuel = 1001;
    private static int _emotionConviction = 0;

    /*Prefabs pour instancier les objets du journal*/
    [SerializeField]
    private GameObject _personnageObject = null;
    [SerializeField]
    private GameObject _lieuObject = null;
    [SerializeField]
    private GameObject _noteObject = null;


    //Journal Personnage
    private static List<GameObject> _journalPersonnage = null;
    private static XmlNodeList _personnageList = null;

    //journal lieu
    private static List<GameObject> _journalLieu = null;
    private static XmlNodeList _lieuList = null;

    //journal note
    private static List<GameObject> _journalNote = null;
    private static XmlNodeList _noteList = null;

    private static int _currentChapter = 1;
    private static string[] _chapterName = { "LA CONVOCATION", "UN PREMIER CHOC", "LE PREMIER JOUR - Matin", "LE PREMIER JOUR - Apres Midi", "LE DEUXIÈME JOUR", "LE TROISIÈME JOUR", "LE DÉLIBÉRÉ", "LE VERDICT / LE SOIR" };
    private static int[] _idStartChapter = { 1001, 2001, 2201, 3001, 4001, 5001, 6001, 7001, 8001 };
    private static int[] _idCheckpoints = { 1001, 1012, 1212, 2001, 2201, 3001, 3132, 4001, 6001, 7001 };

    /*************XML refs variables*************/
    private XmlDocument _xmlPerosDoc = null;
    private XmlDocument _xmlLieuxDoc = null;
    private XmlDocument _xmlNotesDoc = null;

    /*************Quizz value********************/

    public static int _quizzValue = 0;

    /************Attention value****************/

    public static int _attentionValue = 0;

    /*************Boolean Delibéré**************/

    public static bool _votedGuilty = false;

    /*****************
     *****************
     *Debut Fonctions*
     ***************** 
     *****************/

    private void Awake()
    {
        _xmlPerosDoc = new XmlDocument();
        _xmlPerosDoc.LoadXml(Resources.Load<TextAsset>("personnage").text);

        _xmlLieuxDoc = new XmlDocument();
        _xmlLieuxDoc.LoadXml(Resources.Load<TextAsset>("lieux").text);

        _xmlNotesDoc = new XmlDocument();
        _xmlNotesDoc.LoadXml(Resources.Load<TextAsset>("notes").text);

        LoadPersonnages();
        LoadLieux();
        LoadNotes();
    }

    /***************Start fonction***************/
    void Start()
    {
        
    }

    private void LoadPersonnages()
    {
        _personnageList = _xmlPerosDoc.SelectNodes("/personnages/personnage");
        _journalPersonnage = new List<GameObject>();
    }

    private void LoadLieux()
    {
        _lieuList = _xmlLieuxDoc.SelectNodes("/lieux/lieu");
        _journalLieu = new List<GameObject>();
    }

    private void LoadNotes()
    {
        _noteList = _xmlNotesDoc.SelectNodes("/notes/note");
        _journalNote= new List<GameObject>();
    }

    /****************Accesseurs******************/

    public static int GetIdEcranActuel()
    {
        return _iDEcranActuel;
    }
    public static void SetIdEcranActuel(int newId)
    {
        _iDEcranActuel = newId;
    }

    public static int GetEmotionConviction()
    {
        return _emotionConviction;
    }

    public static void UpdateEmotion(int add)
    {
        if(_emotionConviction + add > 50)
        {
            _emotionConviction = 50;
        }
        else
        {
            _emotionConviction += add;
        }
    }

    public static void UpdateConviction(int sub)
    {
        if(_emotionConviction - sub <= -50)
        {
            _emotionConviction = -50;
        }
        else
        {
            _emotionConviction -= sub;
        }
    }

    /*********Gestion journal Perosnnage*********/

    public static List<GameObject> GetJournalPersonnage()
    {
        return _journalPersonnage;
    }

    public static GameObject GetJournalPersonnageByPrenom(string prenom)
    {
        foreach(GameObject go in _journalPersonnage)
        {
            if(go.GetComponent<PersonnageObject>().GetPrenom() == prenom)
            {
                return go;
            }
        }

        return null;
    }

    public static void SetPersonnageValueByPrenom(string prenom, int value)
    {
        foreach (GameObject go in _journalPersonnage)
        {
            if (go.GetComponent<PersonnageObject>().GetPrenom() == prenom && go.GetComponent<PersonnageObject>().GetValueRelation() != value)
            {
                go.GetComponent<PersonnageObject>().SetValueRelation(value);
                break;
            }
            else if (go.GetComponent<PersonnageObject>().GetPrenom() == prenom && go.GetComponent<PersonnageObject>().GetValueRelation() == value)
            {
                break;
            }
        }
    }

    public void AddJournalPersonnage(string prenom, int value)
    {
        foreach(XmlNode node in _personnageList)
        {
            if(node.Attributes["Prenom"].Value == prenom)
            {
                bool noExistance = true;
                foreach(GameObject go in _journalPersonnage)
                {
                    if(go.GetComponent<PersonnageObject>().GetPrenom() == prenom)
                    {
                        noExistance = false;
                        break;
                    }
                }
                if(noExistance == true)
                {
                    GameObject go = Instantiate(_personnageObject);
                    PersonnageObject data = go.GetComponent<PersonnageObject>();
                
                    data.SetPrenom(node.Attributes["Prenom"].Value);
                    data.SetNom(node.Attributes["Nom"].Value);
                    data.SetDescription(node.Attributes["Description"].Value);
                    data.SetValueRelation(value);
                    _journalPersonnage.Add(go);
                }
            }
        }
    }
    
    /*********Gestion journal lieux*********/

    public static List<GameObject> GetJournalLieux()
    {
        return _journalLieu;
    }

    public static GameObject GetJournalLieuxByNom(string nom)
    {
        foreach (GameObject go in _journalLieu)
        {
            if (go.GetComponent<LieuObject>().GetNom() == nom)
            {
                return go;
            }
        }

        return null;
    }

    public void AddJournalLieu(string nom)
    {
      //  Debug.Log(nom);
        foreach (XmlNode node in _lieuList)
        {
            if (node.Attributes["Nom"].Value == nom)
            {
                GameObject go = Instantiate(_lieuObject);
                LieuObject data = go.GetComponent<LieuObject>();
        //        Debug.Log(node.Attributes["Nom"].Value);
                data.SetNom(node.Attributes["Nom"].Value);
          //      Debug.Log(node.Attributes["Descriptions"].Value);
                data.SetDescription(node.Attributes["Descriptions"].Value);
                _journalLieu.Add(go);
            //    Debug.Log(data.ToStringLieu());
                break;
            }
        }
    }

    /*********Gestion journal note*********/

    public static List<GameObject> GetJournalNotes()
    {
        return _journalNote;
    }

    public static GameObject GetJournalNotesByNom(int id)
    {
        foreach (GameObject go in _journalNote)
        {
            if (go.GetComponent<NoteObject>().GetID() == id)
            {
                return go;
            }
        }

        return null;
    }

    public void AddJournalNote(int id)
    {
        foreach (XmlNode node in _noteList)
        {
            if (int.Parse(node.Attributes["ID"].Value) == id)
            {
                GameObject go = Instantiate(_noteObject);
                NoteObject data = go.GetComponent<NoteObject>();
                data.SetID(int.Parse(node.Attributes["ID"].Value));
                data.SetContenu(node.Attributes["Contenu"].Value);
                data.SetTitre(int.Parse(node.Attributes["Titre"].Value));
                int nbcaractere = data.GetContenu().Length;
                data.SetNombreLigne(nbcaractere / 34);
                _journalNote.Add(go);
            }
        }
    }

    /****************Autres accesseurs************/
    public static int GetCurrentChapter()
    {
        return _currentChapter;
    }
    public static void SetCurrentChapter(int newChapter)
    {
        _currentChapter = newChapter;
    }

    public static string[] GetChapterName()
    {
        return _chapterName;
    }
    public static string GetChapterNameByID(int index)
    {
        return _chapterName[index];
    }

    public static int[] GetIDStartChapter()
    {
        return _idStartChapter;
    }
    public static int GetIDStartChapterByID(int index)
    {
        return _idStartChapter[index];
    }

    public static int[] GetIDSCheckpoints()
    {
        return _idCheckpoints;
    }

    /***********Manipulation du cache**********/

    public bool CreateDatas()
    {
        bool ret = false;
        try
        {
            //Parsing personnage
            string[] allInOne = new string[_journalPersonnage.Count];
            for (int i = 0; i < allInOne.Length; i++)
            {
                string value = _journalPersonnage[i].GetComponent<PersonnageObject>().ToStringPersonnage();
                allInOne[i] = value;
            }
            string[] result = System.Array.ConvertAll(allInOne, x => x.ToString());
            string parsedjournal = System.String.Join("|", result);

            //Parsing Lieu
            string[] allInOne2 = new string[_journalLieu.Count];
            for (int i = 0; i < allInOne2.Length; i++)
            {
                string value = _journalLieu[i].GetComponent<LieuObject>().ToStringLieu();
                allInOne2[i] = value;
            }
            string[] result2 = System.Array.ConvertAll(allInOne2, x => x.ToString());
            string parsedlieu = System.String.Join("|", result2);

            //Parsing note
            string[] allInOne3 = new string[_journalNote.Count];
            for (int i = 0; i < allInOne3.Length; i++)
            {
                string value = _journalNote[i].GetComponent<NoteObject>().ToStringNote();
                allInOne3[i] = value;
            }
            string[] result3 = System.Array.ConvertAll(allInOne3, x => x.ToString());
            string parsednote = System.String.Join("|", result3);

            //Remplir les Playerprefs
            PlayerPrefs.SetInt("PartieEnCours", 1);
            PlayerPrefs.SetInt("IDActuel", 1001);
            PlayerPrefs.SetInt("EmotionConviction", 0);
            PlayerPrefs.SetString("Journal", parsedjournal);
            PlayerPrefs.SetString("Lieu", parsedlieu);
            PlayerPrefs.SetString("Note", parsednote);
            PlayerPrefs.SetInt("IDCheckpoint", 1001);
            PlayerPrefs.SetInt("EmotionCheckpoint", 0);
            PlayerPrefs.SetInt("ConvictionCheckpoint", 0);
            PlayerPrefs.SetString("JournalCheckpoint", parsedjournal);
            PlayerPrefs.SetString("LieuCheckpoint", parsedlieu);
            PlayerPrefs.SetString("NoteCheckpoint", parsednote);
            PlayerPrefs.SetInt("Quizz",0);
            PlayerPrefs.SetInt("Attention", 0);
            ret = true;
        }
        catch(UnityException e)
        {
            Debug.Log(e);
        }
        return ret;
    }

    public static bool UpdateDatas()
    {
        bool ret = false;
        try
        {
            //Parsing personnage
            string[] allInOne = new string[_journalPersonnage.Count];
            for (int i = 0; i < allInOne.Length; i++)
            {
                string value = _journalPersonnage[i].GetComponent<PersonnageObject>().ToStringPersonnage();
                allInOne[i] = value;
            }
            string[] result = System.Array.ConvertAll(allInOne, x => x.ToString());
            string parsedjournal = System.String.Join("|", result);

            //Parsing Lieu
            string[] allInOne2 = new string[_journalLieu.Count];
            for (int i = 0; i < allInOne2.Length; i++)
            {
                string value = _journalLieu[i].GetComponent<LieuObject>().ToStringLieu();
                allInOne2[i] = value;
            }
            string[] result2 = System.Array.ConvertAll(allInOne2, x => x.ToString());
            string parsedlieu = System.String.Join("|", result2);

            //Parsing note
            string[] allInOne3 = new string[_journalNote.Count];
            for (int i = 0; i < allInOne3.Length; i++)
            {
                string value = _journalNote[i].GetComponent<NoteObject>().ToStringNote();
                allInOne3[i] = value;
            }
            string[] result3 = System.Array.ConvertAll(allInOne3, x => x.ToString());
            string parsednote = System.String.Join("|", result3);

            if (PlayerPrefs.GetInt("IDActuel") != _iDEcranActuel)
            {
				PlayerPrefs.SetInt("IDActuel", _iDEcranActuel);
            }
            if (PlayerPrefs.GetInt("EmotionConviction") != _emotionConviction)
            {
                PlayerPrefs.SetInt("EmotionConviction", _emotionConviction);
            }
            if(PlayerPrefs.GetString("Journal") != parsedjournal)
            {
                PlayerPrefs.SetString("Journal", parsedjournal);
            }
            if(PlayerPrefs.GetString("Lieu") != parsedlieu)
            {
                PlayerPrefs.SetString("Lieu", parsedlieu);
            }
            if(PlayerPrefs.GetString("Note") != parsednote)
            {
                PlayerPrefs.SetString("Note", parsednote);
            }
            if(PlayerPrefs.GetInt("Quizz") != _quizzValue)
            {
                PlayerPrefs.SetInt("Quizz", _quizzValue);
            }
            if (PlayerPrefs.GetInt("Attention") != _attentionValue)
            {
                PlayerPrefs.SetInt("Attention", _attentionValue);
            }
            ret = true;
        }
        catch (UnityException e)
        {
            Debug.Log(e);
        }
        return ret;
    }

    public bool DeleteDatas()
    {
        bool ret = false;
        try
        {
            PlayerPrefs.DeleteAll();
            ret = true;
        }
        catch (UnityException e)
        {
            Debug.Log(e);
        }
        return ret;
    }

    public bool LoadDatas()
    {
        bool ret = false;
        try
        {
            _iDEcranActuel =  PlayerPrefs.GetInt("IDActuel");
            _emotionConviction = PlayerPrefs.GetInt("EmotionConviction");
            _quizzValue = PlayerPrefs.GetInt("Quizz");
            _attentionValue = PlayerPrefs.GetInt("Attention");
            LoadJournalValues();
        }
        catch (UnityException e)
        {
            Debug.Log(e);
        }
        return ret;
    }

    private void LoadJournalValues()
    {
        string[] sa = PlayerPrefs.GetString("Journal").Split('|');
        
        if(sa[0] != "")
        {
            for (int i = 0; i < sa.Length; i++)
            {
                GameObject go = Instantiate(_personnageObject);
                PersonnageObject data = go.GetComponent<PersonnageObject>();
                string[] sb = sa[i].Split('/');
                data.SetPrenom(sb[0]);
                data.SetNom(sb[1]);
                data.SetDescription(sb[2]);
                data.SetValueRelation(int.Parse(sb[3]));
                _journalPersonnage.Add(go);
            }
        }
        else
        {
            _journalPersonnage = new List<GameObject>();
        }

        sa = PlayerPrefs.GetString("Lieu").Split('|');
        if (sa[0] != "")
        {
            for (int i = 0; i < sa.Length; i++)
            {
                GameObject go = Instantiate(_lieuObject);
                LieuObject data = go.GetComponent<LieuObject>();
                string[] sb = sa[i].Split('/');
                data.SetNom(sb[0]);
                data.SetDescription(sb[1]);
                _journalLieu.Add(go);
            }
        }
        else
        {
            _journalLieu = new List<GameObject>();
        }

        sa = PlayerPrefs.GetString("Note").Split('|');
        if (sa[0] != "")
        {
            for (int i = 0; i < sa.Length; i++)
            {
                GameObject go = Instantiate(_noteObject);
                NoteObject data = go.GetComponent<NoteObject>();
                string[] sb = sa[i].Split('/');
                data.SetID(int.Parse(sb[0]));
                data.SetContenu(sb[1]);
                data.SetTitre(int.Parse(sb[2]));
                data.SetNombreLigne(int.Parse(sb[3]));
                _journalNote.Add(go);
            }
        }
        else
        {
            _journalNote = new List<GameObject>();
        }
    }

    public static bool UpdateCheckPoint()
    {
        bool ret = false;
        try
        {
            //Parsing personnage
            string[] allInOne = new string[_journalPersonnage.Count];
            for (int i = 0; i < allInOne.Length; i++)
            {
                string value = _journalPersonnage[i].GetComponent<PersonnageObject>().ToStringPersonnage();
                allInOne[i] = value;
            }
            string[] result = System.Array.ConvertAll(allInOne, x => x.ToString());
            string parsedjournal = System.String.Join("|", result);

            //Parsing Lieu
            string[] allInOne2 = new string[_journalLieu.Count];
            for (int i = 0; i < allInOne2.Length; i++)
            {
                string value = _journalLieu[i].GetComponent<LieuObject>().ToStringLieu();
                allInOne2[i] = value;
            }
            string[] result2 = System.Array.ConvertAll(allInOne2, x => x.ToString());
            string parsedlieu = System.String.Join("|", result2);

            //Parsing note
            string[] allInOne3 = new string[_journalNote.Count];
            for (int i = 0; i < allInOne3.Length; i++)
            {
                string value = _journalNote[i].GetComponent<NoteObject>().ToStringNote();
                allInOne3[i] = value;
            }
            string[] result3 = System.Array.ConvertAll(allInOne3, x => x.ToString());
            string parsednote = System.String.Join("|", result3);

            if (PlayerPrefs.GetInt("IDCheckpoint") != _iDEcranActuel)
            {
                PlayerPrefs.SetInt("IDCheckpoint", _iDEcranActuel);
            }
            if (PlayerPrefs.GetInt("EmotionConvictionCheckpoint") != _emotionConviction)
            {
                PlayerPrefs.SetInt("EmotionConvictionCheckpoint", _emotionConviction);
            }
            if (PlayerPrefs.GetString("JournalCheckpoint") != parsedjournal)
            {
                PlayerPrefs.SetString("JournalCheckpoint", parsedjournal);
            }
            if (PlayerPrefs.GetString("LieuCheckpoint") != parsedlieu)
            {
                PlayerPrefs.SetString("LieuCheckpoint", parsedlieu);
            }
            if (PlayerPrefs.GetString("NoteCheckpoint") != parsednote)
            {
                PlayerPrefs.SetString("NoteCheckpoint", parsednote);
            }
            if (PlayerPrefs.GetInt("QuizzCheckpoint") != _quizzValue)
            {
                PlayerPrefs.SetInt("QuizzCheckpoint", _quizzValue);
            }
            if (PlayerPrefs.GetInt("AttentionCheckpoint") != _attentionValue)
            {
                PlayerPrefs.SetInt("AttentionCheckpoint", _attentionValue);
            }
            ret = true;
        }
        catch (UnityException e)
        {
            Debug.Log(e);
        }
        return ret;
    }

    public bool LoadCheckPointDatas()
    {
        bool ret = false;
        try
        {
            _iDEcranActuel = PlayerPrefs.GetInt("IDCheckpoint");
            _emotionConviction = PlayerPrefs.GetInt("EmotionConvictionCheckpoint");
            _quizzValue = PlayerPrefs.GetInt("QuizzCheckpoint");
            _attentionValue = PlayerPrefs.GetInt("AttentionCheckpoint");
            LoadJournalValuesCheckpoint();
        }
        catch (UnityException e)
        {
            Debug.Log(e);
        }
        return ret;
    }

    private void LoadJournalValuesCheckpoint()
    {
        string[] sa = PlayerPrefs.GetString("JournalCheckpoint").Split('|');

        if (sa[0] != "")
        {
            for (int i = 0; i < sa.Length; i++)
            {
                GameObject go = Instantiate(_personnageObject);
                PersonnageObject data = go.GetComponent<PersonnageObject>();
                string[] sb = sa[i].Split('/');
                data.SetPrenom(sb[0]);
                data.SetNom(sb[1]);
                data.SetDescription(sb[2]);
                data.SetValueRelation(int.Parse(sb[3]));
                _journalPersonnage.Add(go);
            }
        }
        else
        {
            _journalPersonnage = new List<GameObject>();
        }

        sa = PlayerPrefs.GetString("LieuCheckpoint").Split('|');
        if (sa[0] != "")
        {
            for (int i = 0; i < sa.Length; i++)
            {
                GameObject go = Instantiate(_lieuObject);
                LieuObject data = go.GetComponent<LieuObject>();
                string[] sb = sa[i].Split('/');
                data.SetNom(sb[0]);
                data.SetDescription(sb[1]);
                _journalLieu.Add(go);
            }
        }
        else
        {
            _journalLieu = new List<GameObject>();
        }

        sa = PlayerPrefs.GetString("NoteCheckpoint").Split('|');
        if (sa[0] != "")
        {
            for (int i = 0; i < sa.Length; i++)
            {
                GameObject go = Instantiate(_noteObject);
                NoteObject data = go.GetComponent<NoteObject>();
                string[] sb = sa[i].Split('/');
                data.SetID(int.Parse(sb[0]));
                data.SetContenu(sb[1]);
                data.SetTitre(int.Parse(sb[2]));
                data.SetNombreLigne(int.Parse(sb[3]));
                _journalNote.Add(go);
            }
        }
        else
        {
            _journalNote = new List<GameObject>();
        }
    }

    public void ResetList()
    {
        _journalPersonnage = new List<GameObject>();
        _journalLieu = new List<GameObject>();
        _journalNote = new List<GameObject>();
    }
}
