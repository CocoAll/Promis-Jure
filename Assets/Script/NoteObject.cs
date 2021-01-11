using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour {

    private int _iD = 0;
    private string _contenu = "";
    private int _titre = 0;
    private int _nombreLigne = 0;

    /*********************Accesseurs*************************/

    public int GetID()
    {
        return _iD;
    }
    public void SetID(int ID)
    {
        _iD = ID;
    }

    public string GetContenu()
    {
        return _contenu;
    }
    public void SetContenu(string contenu)
    {
        _contenu = contenu;
    }

    public int GetTitre()
    {
        return _titre;
    }
    public void SetTitre(int titre)
    {
        _titre = titre;
    }

    public int GetNombreLigne()
    {
        return _nombreLigne;
    }

    public void SetNombreLigne(int nb)
    {
        _nombreLigne = nb;
    }

    public string ToStringNote()
    {
        string ret = _iD + "/" + _contenu + "/" + _titre + "/" + _nombreLigne;
        return ret;
    }
}
