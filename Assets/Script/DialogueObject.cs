using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueObject : MonoBehaviour {

    private string _iD = "-1";
    private string _texteDeBase = null;
    private string _relationOption = "-1";
    private int _idNextRO = -1;
    private int _idNext = -1;
    private int _modEmotion = 0;
    private int _modConviction = 0;
    private string _lieux = "-1";
    private int _note = -1;
    private string _journal = "-1";

    /*********************Accesseurs*************************/

    public string GetID()
    {
        return _iD;
    }
    public void SetID(string newID)
    {
        _iD = newID;
    }

    public string GetTexte()
    {
        return _texteDeBase;
    }
    public void SetTexte(string newTexte)
    {
        _texteDeBase = newTexte;
    }

    public string GetRelationOption()
    {
        return _relationOption;
    }
    public void SetRelationOption(string newRO)
    {
        _relationOption = newRO;
    }

    public int GetIDNextRO()
    {
        return _idNextRO;
    }
    public void SetIDNextRO(int newID)
    {
        _idNextRO = newID;
    }

    public int GetIDNext()
    {
        return _idNext;
    }
    public void SetIDNext(int newID)
    {
        _idNext = newID;
    }

    public int GetModEmotion()
    {
        return _modEmotion;
    }
    public void SetModEmotion(int newMod)
    {
        _modEmotion = newMod;
    }

    public int GetModConviction()
    {
        return _modConviction;
    }
    public void SetModConviction(int newMod)
    {
        _modConviction = newMod;
    }

    public string GetLieux()
    {
        return _lieux;
    }
    public void SetLieux(string lieux)
    {
        _lieux = lieux;
    }

    public int GetNote()
    {
        return _note;
    }
    public void SetNote(int note)
    {
        _note = note;
    }


    public string GetJournal()
    {
        return _journal;
    }
    public void SetJournal(string journal)
    {
        _journal = journal;
    }

}
