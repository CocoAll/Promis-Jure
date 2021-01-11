using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DelibereCulpabiliteController : MonoBehaviour {

    [SerializeField]
    private GameObject _button = null;
    [SerializeField]
    private Text _inputText = null;
    [SerializeField]
    private GameObject _panelFonduAuNoir = null;
    [SerializeField]
    private PanelDerouleController _pdc = null;

    public void CheckStringCulpabilite(string str)
    {
        if(str == "oui" || str == "Oui" || str == "OUI")
        {
            _button.SetActive(true);
        }
        else if(str == "Non" || str == "non" || str == "NON")
        {
            _button.SetActive(true);
        }
        else
        {
            _button.SetActive(false);
        }
    }

    public void ValidateString()
    {
        string str = _inputText.text;
        if (str == "oui" || str == "Oui" || str == "OUI")
        {
            GameState.SetPersonnageValueByPrenom("Nicolas",0);
        }
        else if (str == "Non" || str == "non" || str == "NON")
        {
            GameState.SetPersonnageValueByPrenom("Nicolas", 1);
        }
        ResetValues();
        _pdc.LinearFromDelibere();
        this.transform.parent.gameObject.SetActive(false);
    }

    public void ResetValues()
    {
        _inputText.text = "";
        _button.SetActive(false);
    }

}
