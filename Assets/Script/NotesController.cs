using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class NotesController : MonoBehaviour {

    [SerializeField]
    private List<GameObject> _listeChoix ;

    private List<GameObject> _currentList;

    private GameObject _selectedChoix = null;
    private int _selectedIndex = 0;

    [SerializeField]
    private Text _texteACharge = null;
    [SerializeField]
    private Text _texteADecharge = null;
    [SerializeField]
    private Text _texteDoutes = null;

    [SerializeField]
    private PanelDerouleController _pdc;

    [SerializeField]
    private CarnetController _cc = null;

    private void Start()
    {
        _currentList = _listeChoix;
        _texteACharge.text = "";
        _texteADecharge.text = "";
        _texteDoutes.text = "";
    }

    public void SelectOne(int index)
    {
        Debug.Log(index + " is Selected");
        _selectedChoix = _listeChoix[index];
        _selectedIndex = index;
    }

    public void BoutonAddCharge()
    {
        StartCoroutine(AddCharge());
    }

    public IEnumerator AddCharge()
    {
        if(_selectedChoix != null)
        {
            _texteACharge.CrossFadeAlpha(0.0f, 0.1f, false);
            _currentList[_selectedIndex].GetComponent<Text>().CrossFadeAlpha(0.0f, 0.2f, false);
            yield return new WaitForSeconds(0.1f);

            _texteACharge.text += _currentList[_selectedIndex].GetComponent<Text>().text + "\n";
            Debug.Log("Added to Charge");
            _texteACharge.CrossFadeAlpha(1.0f, 0.5f, false);

            _currentList[_selectedIndex].SetActive(false);
            _currentList[_selectedIndex].GetComponent<Text>().CrossFadeAlpha(1.0f, 0.01f, false);
            _currentList[_selectedIndex] = null;
            _selectedChoix = null;
            CheckListeChoix();
            yield return null;
        }
    }

    public void BoutonAddDoute()
    {
        StartCoroutine(AddDoute());
    }

    private IEnumerator AddDoute()
    {
        if (_selectedChoix != null)
        {
            _texteDoutes.CrossFadeAlpha(0.0f, 0.1f, false);
            _currentList[_selectedIndex].GetComponent<Text>().CrossFadeAlpha(0.0f, 0.2f, false);
            yield return new WaitForSeconds(0.1f);

            _texteDoutes.text += _currentList[_selectedIndex].GetComponent<Text>().text + "\n";
            Debug.Log("Added to Doute");
            _texteDoutes.CrossFadeAlpha(1.0f, 0.5f, false);

            _currentList[_selectedIndex].SetActive(false);
            _currentList[_selectedIndex] = null;
            _selectedChoix = null;
            CheckListeChoix();
            yield return null;
        }
    }

    public void BoutonAddDecharge()
    {
        StartCoroutine(AddDecharge());
    }

    private IEnumerator AddDecharge()
    {
        if (_selectedChoix != null)
        {
            _texteADecharge.CrossFadeAlpha(0.0f, 0.1f, false);
            _currentList[_selectedIndex].GetComponent<Text>().CrossFadeAlpha(0.0f, 0.2f, false);
            yield return new WaitForSeconds(0.1f);

            _texteADecharge.text += _currentList[_selectedIndex].GetComponent<Text>().text + "\n";
            Debug.Log("Added to Decharge");
            _texteADecharge.CrossFadeAlpha(1.0f, 0.5f, false);

            _currentList[_selectedIndex].SetActive(false);
            _currentList[_selectedIndex] = null;
            _selectedChoix = null;
            CheckListeChoix();
            yield return null;
        }
    }

    private void CheckListeChoix()
    {
        bool empty = true;
        foreach(GameObject go in _currentList)
        {
            if(go != null)
            {
                Debug.Log("Liste isn't empty");
                empty = false;
                break;
            }
        }

        if(empty == true)
        {
            Debug.Log("Liste is empty");
            _cc._textCharge.text = _texteACharge.text;
            _cc._textDecharge.text = _texteADecharge.text;
            _cc._textDoutes.text = _texteDoutes.text;
            _pdc.OnClickLinéaire();
            this.gameObject.SetActive(false);
            //Fin du mini jeu
        }
    }

    public void ResetMiniGame()
    {
        _currentList = _listeChoix;
    }

}
