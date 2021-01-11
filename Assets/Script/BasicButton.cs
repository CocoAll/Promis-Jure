using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicButton : MonoBehaviour {

    //Fonction pour fermer le panel dans lequelle est le bouton
    public void ClosePanel()
    {
        this.transform.parent.gameObject.SetActive(false);
    }

    //Mise en plein ecran du jeu ou inversement
    public void FullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
}
