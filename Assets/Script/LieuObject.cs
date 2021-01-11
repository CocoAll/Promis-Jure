using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LieuObject : MonoBehaviour {

    private string _nom = "";
    private string _description = "";


    /*********************Accesseurs*************************/

    public string GetNom()
    {
        return _nom;
    }
    public void SetNom(string nom)
    {
        _nom = nom;
    }

    public string GetDescription()
    {
        return _description;
    }
    public void SetDescription(string description)
    {
        _description = description;
    }

    public string ToStringLieu()
    {
        string ret = _nom + "/" + _description;
        return ret;
    }
}
