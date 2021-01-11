using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//Script chargé de gérer le mini-jeu du swap : bouger un cercle sur une ligne

public class SwapController : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IDragHandler {

	[SerializeField]
	private GameObject _swapBegin;
	[SerializeField]
	private GameObject _swapEnd;
	[SerializeField]
	private PanelDerouleController _panelDerouleController;

	private float xBegin;
	private float xEnd;
	private float yBegin;
	private float yEnd;

	private bool end;

	[SerializeField]
	private int _numSwap;

    [SerializeField]
    private Image img = null;
    [SerializeField]
    private Text txt = null;
    [SerializeField]
    private GameObject panelText = null;

    private float timer = 6.0f;
	private float timeLeft;

	// Use this for initialization
	void Start () {

		timeLeft = timer;

		end = false;

		xBegin = _swapBegin.transform.position.x;
		xEnd = _swapEnd.transform.position.x;
		yBegin = _swapBegin.transform.position.y;
		yEnd = _swapEnd.transform.position.y;

	}
	
	// Update is called once per frame
	void Update () {

		//Condition pour gérer le temps du mini jeu swap
		//Si le mini-jeu n'est pas terminé et qu'il s'agit du mini-jeu avec le timer
		if (end == false && _numSwap == 1) {

			//On retire du temps à chaque update
			timeLeft -= Time.deltaTime;
            img.fillAmount = timeLeft / timer;
            if(timeLeft >= 0)
            {
                txt.text = timeLeft.ToString("F");
            }
            else
            {
                txt.text = "0.00";
            }
			//S'il ne reste plus de temps, on lance le choix "ne pas aller à la prison"
			if (timeLeft < 0) {

                end = true;
				_panelDerouleController.ChoixBouton(2052,"2035B");
			}
		}

		//Condition qui vérifie si on vient de réussir le mini-jeu swap 1
		if (_numSwap == 0) {
			//Si le joueur a emmené le cercle à la fin de la ligne et que le mini-jeu n'est pas encore terminé
			if (_swapBegin.transform.position.x == xEnd && end == false) {

				end = true;
				_panelDerouleController.OnClickLinéaire ();
				Debug.Log ("Arrivé à la fin");
			}
		
		//Condition qui vérifie si on vient de réussir le mini-jeu swap 2
		} else if (_numSwap == 1) {

			//Si le joueur a emmené le cercle à la fin de la ligne et que le mini-jeu n'est pas encore terminé
			if (_swapBegin.transform.position.y == yEnd && end == false) {

				end = true;
				_panelDerouleController.ChoixBouton(2036,"2035A");
				Debug.Log ("Arrivé à la fin");
			}
        //Condition qui vérifie si on vient de réussir le mini-jeu swap 3
        } else if (_numSwap == 2) {

			//Si le joueur a emmené le cercle à la fin de la ligne et que le mini-jeu n'est pas encore terminé
			if (_swapBegin.transform.position.y == yEnd && end == false) {

				end = true;
                _panelDerouleController.OnClickLinéaire ();
                Debug.Log("Arrivé à la fin");
			}

		}
	}

	public void OnPointerEnter(PointerEventData eventData){
		
		Debug.Log ("test pointer enter");
		//Lorsque l'on clique sur l'élément de la ville
		if(Input.GetMouseButtonDown(0)){

			Debug.Log ("test");


		}
	}

	public void OnPointerDown(PointerEventData eventData){

		Debug.Log ("test pointer down");

	}

	public void OnDrag(PointerEventData data){

		if (_numSwap == 0) {

			transform.position = new Vector3 (Mathf.Clamp (Input.mousePosition.x, xBegin, xEnd), Mathf.Clamp (Input.mousePosition.y, yBegin, yBegin), 0);

		} else if (_numSwap == 1) {

			transform.position = new Vector3 (Mathf.Clamp (Input.mousePosition.x, xBegin, xBegin), Mathf.Clamp (Input.mousePosition.y, yBegin, yEnd), 0);
		}
        else if (_numSwap == 2)
        {

            transform.position = new Vector3(Mathf.Clamp(Input.mousePosition.x, xBegin, xBegin), Mathf.Clamp(Input.mousePosition.y, yBegin, yEnd), 0);
        }

    }

	void OnTriggerEnter2D(Collider2D other){

		Debug.Log ("Test collider : " + other.ToString ());
		if (other.gameObject == _swapEnd) {

			Debug.Log ("Test de la collision entre le début et la fin");
		}

	}

	public void ResetSwap(){

		end = false;
		Debug.Log ("Reset Swap");
		transform.position = new Vector3 (xBegin,yBegin,0);
	}

}
