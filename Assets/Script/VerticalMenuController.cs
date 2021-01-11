using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VerticalMenuController : MonoBehaviour {

    [SerializeField]
    private GameObject _panelToActivate = null;

    //Fonction pour quand la souris passe sur l'objet
    public void PointerEnter()
    {
        if (_panelToActivate.activeInHierarchy != true)
        {
            gameObject.GetComponent<Image>().color -= new Color(0.2f, 0.2f, 0.2f, 0.0f);
            Debug.Log(gameObject.GetComponent<Image>().color.ToString());
        }
    }

    //Fonction pour quand la souris arrete de passer sur l'objet
    public void PointerExit()
    {
        if (_panelToActivate.activeInHierarchy != true)
        {
            gameObject.GetComponent<Image>().color += new Color(0.2f, 0.2f, 0.2f, 0.0f);
            Debug.Log(gameObject.GetComponent<Image>().color.ToString());
        }
    }

    //Fonction pour quand on clique sur l'objet
    public void PointerClick()
    {
        if(gameObject.GetComponent<Image>() != null)
        {
            gameObject.GetComponent<Image>().color += new Color(0.2f, 0.2f, 0.2f, 0.0f);
        }
        if (_panelToActivate.activeInHierarchy != true)
        {
            _panelToActivate.SetActive(true);
        }
        else
        {
            _panelToActivate.SetActive(false);
        }
    }
}
