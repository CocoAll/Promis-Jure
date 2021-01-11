using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenURL : MonoBehaviour {

    public void OpenURLMinistere()
    {
        Application.ExternalEval("window.open(\"http://www.mediatheque.justice.gouv.fr/direct/3466-4c8b98d8ee33c99f17d753874e62c74a626fc61b-1499242277-direct\",\"_blank\")");
    }
}
