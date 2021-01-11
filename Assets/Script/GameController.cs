using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

    /***************Variables liées aux fichiers xml************/
    //docs
    private XmlDocument _xmlDocDialogues = null;
    private XmlDocument _xmlDocDeroule= null;
    private XmlDocument _xmlDocGameOver = null;

    private XmlNodeList _gameoverList = null;
    /*******************prefabs to Instanciate******************/
    [SerializeField]
    private GameObject _derouleObject = null;
    [SerializeField]
    private GameObject _dialogueObject = null;

    /****************Tableaux des prefabs**********************/
    private List<GameObject> _listDerouleObjects = null;
    private List<GameObject> _listDialogueObjects = null;

    /********************Start Fonction************************/
    void Start () {       

       

        _listDerouleObjects = new List<GameObject>();
        _listDialogueObjects = new List<GameObject>();

        //Loading xml Docs
        _xmlDocDeroule = new XmlDocument();
        _xmlDocDeroule.LoadXml(Resources.Load<TextAsset>("deroule").text);

        _xmlDocDialogues = new XmlDocument();
        _xmlDocDialogues.LoadXml(Resources.Load<TextAsset>("dialogues").text);

        _xmlDocGameOver = new XmlDocument();
        _xmlDocGameOver.LoadXml(Resources.Load<TextAsset>("gameover").text);

        _gameoverList = _xmlDocGameOver.SelectNodes("/gameovers/gameover");

        LoadDeroule();
        LoadDialogues();

    }

    /*********Fonctions qui chargent les fichiers Xml*********/
    private void LoadDeroule()
    {
        XmlNodeList list = _xmlDocDeroule.SelectNodes("/deroule/ecran");
       
        foreach (XmlNode node in list)
        {
            //Creating the object
            GameObject deroule = Instantiate(_derouleObject);
            DerouleObject datas = deroule.GetComponent<DerouleObject>();

            //Setting his values
            if (int.Parse(node.Attributes["ID"].Value) != -1)
            {
                datas.SetID(int.Parse(node.Attributes["ID"].Value));
            }

            //Debug.Log("Debut ID : " + datas.GetID());

            datas.SetNomImage("Images/Histoire/" + node.Attributes["NomImage"].Value);

            datas.SetTexte(node.Attributes["TexteDeBase"].Value);

            datas.SetPersonnage(node.Attributes["Personnage"].Value);

            datas.SetAudio(node.Attributes["Audio"].Value);

            datas.SetVideo(node.Attributes["Video"].Value);

            datas.SetMiniJeu(int.Parse(node.Attributes["MiniJeu"].Value));

            if (int.Parse(node.Attributes["Choix"].Value) == 0){
                datas.SetChoix(0);
            }
            else if (int.Parse(node.Attributes["Choix"].Value) == 1)
            {
                datas.SetChoix(1);
            }

			else if (int.Parse(node.Attributes["Choix"].Value) == 2)
			{
				datas.SetChoix(2);
			}
            else
            {
                Debug.LogError("Erreur dans la saisie du Choix");
            }

            if (node.Attributes["RelationOption"].Value != "-1")
            {
                datas.SetRelationOption(node.Attributes["RelationOption"].Value);
            }

            if(int.Parse(node.Attributes["IDNextRO"].Value) != -1)
            {
                datas.SetIDNextRO(int.Parse(node.Attributes["IDNextRO"].Value));
            }

            if (int.Parse(node.Attributes["IDNext"].Value) != -1)
            {
                datas.SetIDNext(int.Parse(node.Attributes["IDNext"].Value));
            }

            if (node.Attributes["IDDialogues"].Value != "-1")
            {
				datas.SetIDDialogues(node.Attributes["IDDialogues"].Value);
            }

            if(int.Parse(node.Attributes["ModEmotion"].Value) != 0){
                datas.SetModEmotion(int.Parse(node.Attributes["ModEmotion"].Value));
            }

            if (int.Parse(node.Attributes["ModEmotion"].Value) != 0)
            {
                datas.SetModConviction(int.Parse(node.Attributes["ModConviction"].Value));
            }

            if (node.Attributes["Lieu"].Value != "-1")
            {
                datas.SetLieux(node.Attributes["Lieu"].Value);
            }

            if (node.Attributes["Note"].Value != "-1")
            {
                datas.SetNote(int.Parse(node.Attributes["Note"].Value));
            }

            if (node.Attributes["Journal"].Value != "-1")
            {
                datas.SetJournal(node.Attributes["Journal"].Value);
            }

            //Debug.Log("Fin ID : " + datas.GetID());

            _listDerouleObjects.Add(deroule);
        }



    }

    private void LoadDialogues()
    {
        XmlNodeList list = _xmlDocDialogues.SelectNodes("/dialogues/dialogue");
        
        foreach (XmlNode node in list)
        {
            GameObject dialogue = Instantiate(_dialogueObject);
            DialogueObject datas = dialogue.GetComponent<DialogueObject>();
            //Setting his values
            if (node.Attributes["ID"].Value != "-1")
            {
                datas.SetID(node.Attributes["ID"].Value);
            }

            datas.SetTexte(node.Attributes["TexteDeBase"].Value);

            if (node.Attributes["RelationOption"].Value != "-1")
            {
                datas.SetRelationOption(node.Attributes["RelationOption"].Value);
            }
			
            if (int.Parse(node.Attributes["IDNextRO"].Value) != -1)
            {
                datas.SetIDNextRO(int.Parse(node.Attributes["IDNextRO"].Value));
            }
			
            if (int.Parse(node.Attributes["IDNext"].Value) != -1)
            {
                datas.SetIDNext(int.Parse(node.Attributes["IDNext"].Value));
            }
            if (int.Parse(node.Attributes["ModEmotion"].Value) != 0)
            {
                datas.SetModEmotion(int.Parse(node.Attributes["ModEmotion"].Value));
            }

            if (int.Parse(node.Attributes["ModConviction"].Value) != 0)
            {
                datas.SetModConviction(int.Parse(node.Attributes["ModConviction"].Value));
            }

            if (node.Attributes["Lieu"].Value != "-1")
            {
                datas.SetLieux(node.Attributes["Lieu"].Value);
            }

            if (node.Attributes["Note"].Value != "-1")
            {
                datas.SetNote(int.Parse(node.Attributes["Note"].Value));
            }

            if (node.Attributes["Journal"].Value != "-1")
            {
                datas.SetJournal(node.Attributes["Journal"].Value);
            }
            _listDialogueObjects.Add(dialogue);
        }

        

    }

    /*********Accesserus pour les objets dans les list********/
    public GameObject GetDerouleObjectByID(int id)
    {

	//	Debug.Log ("id : " + id.ToString ());
	//	Debug.Log (_listDerouleObjects);
        GameObject ret = null;
        foreach (GameObject obj in _listDerouleObjects)
        {
            if(obj.GetComponent<DerouleObject>().GetID() == id)
            {
                ret = obj;
                return ret;
            }
        }
        return ret;
    }

    public GameObject GetDialogueObjectByID(string id)
    {
        GameObject ret = null;
		foreach (GameObject obj in  _listDialogueObjects)
        {
            if (obj.GetComponent<DialogueObject>().GetID() == id)
            {
                ret = obj;
				return ret;
            }
        }
        return ret;
    }

    public XmlNodeList GetGameOverList()
    {
        return _gameoverList;
    }
}
