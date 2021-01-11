using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonnageObject : MonoBehaviour {

    private string _prenom = "";
    private string _nom = "";
    private string _description = "";
    private int _valueRelation = 0;

    /*********************Accesseurs*************************/

    public string GetPrenom()
    {
        return _prenom;
    }
    public void SetPrenom(string newPrenom)
    {
        _prenom = newPrenom;
    }

    public string GetNom()
    {
        return _nom;
    }
    public void SetNom(string newNom)
    {
        _nom = newNom;
    }

    public string GetDescription()
    {
        return _description;
    }
    public void SetDescription(string newDescription)
    {
        _description = newDescription;
    }

    public int GetValueRelation()
    {
        return _valueRelation;
    }
    public void SetValueRelation(int newValue)
    {
        _valueRelation = newValue;
    }

    public string ToStringPersonnage()
    {
        string ret = _prenom + "/" + _nom + "/" + _description + "/" + _valueRelation;
        return ret;
    }
}
