using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvisPeineController : MonoBehaviour {

    [SerializeField]
    private PanelDerouleController _pdc = null;

    public void OnClickPeineLourde()
    {
        int previousChoice = GameState.GetJournalPersonnageByPrenom("Charles-Henri").GetComponent<PersonnageObject>().GetValueRelation();
        if (/*Lourde*/ previousChoice == 1)
        {
            _pdc.NextDerouleForDelibere(7076);
        }
        else if (/*Juste*/ previousChoice == 2)
        {
            _pdc.NextDerouleForDelibere(7077);
        }
        else if (/*Legere*/previousChoice == 3)
        {
            _pdc.NextDerouleForDelibere(7078);
        }
        else
        {
            //We are fucked
        }
    }

    public void OnClickPeineJuste()
    {
        int previousChoice = GameState.GetJournalPersonnageByPrenom("Charles-Henri").GetComponent<PersonnageObject>().GetValueRelation();
        if (/*Lourde*/previousChoice == 1)
        {
            _pdc.NextDerouleForDelibere(7079);
        }
        else if (/*Juste*/previousChoice == 2)
        {
            _pdc.NextDerouleForDelibere(7080);
        }
        else if (/*Legere*/previousChoice == 3)
        {
            _pdc.NextDerouleForDelibere(7081);
        }
        else
        {
            //We are fucked
        }
    }

    public void OnClickPeineLegere()
    {
        int previousChoice = GameState.GetJournalPersonnageByPrenom("Charles-Henri").GetComponent<PersonnageObject>().GetValueRelation();
        if (/*Lourde*/previousChoice == 1)
        {
            _pdc.NextDerouleForDelibere(7082);
        }
        else if (/*Juste*/previousChoice == 2)
        {
            _pdc.NextDerouleForDelibere(7083);
        }
        else if (/*Legere*/previousChoice == 3)
        {
            _pdc.NextDerouleForDelibere(7084);
        }
        else
        {
            //We are fucked
        }
    }


}
