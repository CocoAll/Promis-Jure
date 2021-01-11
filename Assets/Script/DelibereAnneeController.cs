using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DelibereAnneeController : MonoBehaviour {

    [SerializeField]
    private GameObject _button = null;
    [SerializeField]
    private Text _inputText = null;
    [SerializeField]
    private GameObject _panelFonduAuNoir = null;
    [SerializeField]
    private PanelDerouleController _pdc = null;
    private static int _nbAnnée = 0;

    public void CheckStringAnnee(string integer)
    {
        if (int.Parse(integer) > 0 && int.Parse(integer) < 100)
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
        _nbAnnée = int.Parse(_inputText.text);
        ResetValues();
        _pdc.NextDerouleForDelibere(7087);
        this.transform.parent.gameObject.SetActive(false);
    }

    public void ResetValues()
    {
        _inputText.text = "";
        _button.SetActive(false);
    }

    public static int GetNbAnnée()
    {
        return _nbAnnée;
    }
}
