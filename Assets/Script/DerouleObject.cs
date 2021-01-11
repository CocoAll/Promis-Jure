using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DerouleObject : MonoBehaviour {
    [SerializeField]
    private int _iD = -1;
    [SerializeField]
    private string _nomImage = null;
    [SerializeField]
    private string _texteDeBase = null;
    [SerializeField]
    private string _personnage = "-1";
    [SerializeField]
    private string _audio = "-1";
    [SerializeField]
    private string _video = "-1";
    [SerializeField]
    private int _minijeu = -1;
    [SerializeField]
    private int _choix = 0;
    [SerializeField]
    private string _relationOption = "-1";
    [SerializeField]
    private int _idNextRO = -1;
    [SerializeField]
    private int _idNext = -1;
    [SerializeField]
    private string _iDDialogues = "-1";
    [SerializeField]
    private int _modEmotion = 0;
    [SerializeField]
    private int _modConviction = 0;
    [SerializeField]
    private string _lieux = "-1";
    [SerializeField]
    private int _note = -1;
    [SerializeField]
    private string _journal = "-1";

    /*********************Accesseurs*************************/

    public int GetID()
    {
        return _iD;
    }
    public void SetID(int newID)
    {
        _iD = newID;
    }

    public string GetNomImage()
    {
        return _nomImage;
    }
    public void SetNomImage(string newImage)
    {
        _nomImage = newImage;
    }

    public string GetTexte()
    {
        return _texteDeBase;
    }
    public void SetTexte(string newTexte)
    {
        _texteDeBase = newTexte;
    }

    public string GetPersonnage()
    {
        return _personnage;
    }
    public void SetPersonnage(string newPerso)
    {
        _personnage = newPerso;
    }

    public string GetAudio()
    {
        return _audio;
    }
    public void SetAudio(string newAudio)
    {
        _audio = newAudio;
    }

    public string GetVideo()
    {
        return _video;
    }
    public void SetVideo(string newVideo)
    {
        _video = newVideo;
    }

    public int GetMiniJeu()
    {
        return _minijeu;
    }
    public void SetMiniJeu(int newMiniJeu)
    {
        _minijeu = newMiniJeu;
    }

    public int GetChoix()
    {
        return _choix;
    }
    public void SetChoix(int newChoix)
    {
        _choix = newChoix;
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

    public string GetIDDialogues()
    {
        return _iDDialogues;
    }
    public void SetIDDialogues(string newID)
    {
        _iDDialogues = newID;
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
