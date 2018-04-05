using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class ChoixMonstres : MonoBehaviour {
	//Pour la gestion des listes
	UnityEngine.Object[] uoChoixMonstres;
	int index;
	[HideInInspector]
	public GameObject gChoixMonstre;
	int iTailleCM;
	[HideInInspector]
	public int iNbJoueurs;

	//Textes
	TextMeshProUGUI tmIndicateurCM;
	TextMeshProUGUI tmTitre;

	//Listes
	[HideInInspector]
	public List<Sprite> lMonstres = new List<Sprite> ();
	[HideInInspector]
	public List<Sprite> lChoixMonstres = new List<Sprite>();
	[HideInInspector]
	public List<int> lIndex = new List<int>();

	[Header("Listes pour contrôler l'affichage des éléments du jeu")]
	public List<GameObject> lMonstresJeu = new List<GameObject>();
	public List<GameObject> lJoueursJeu = new List<GameObject>();
	public List<GameObject> lJoueursInterfaceJeu = new List<GameObject>();

	void Start () {
		uoChoixMonstres = Resources.LoadAll ("Monstres", typeof(Sprite));
		iTailleCM = uoChoixMonstres.Length;

		for(int i=0; i<iTailleCM; i++) {
			lMonstres.Add (uoChoixMonstres [i] as Sprite);
		}

		uoChoixMonstres = null;
		tmIndicateurCM = GameObject.Find ("IndicateurCM").GetComponent<TextMeshProUGUI> ();
		tmTitre = GameObject.Find ("Titre").GetComponent<TextMeshProUGUI> ();
		tmTitre.text = "Choisissez votre monstre";
		tmTitre.fontSize = 35;

		if(iNbJoueurs == 2) {
			lMonstresJeu [2].gameObject.SetActive (false);
			lMonstresJeu [3].gameObject.SetActive (false);
			lJoueursJeu [2].gameObject.SetActive (false);
			lJoueursJeu [3].gameObject.SetActive (false);
			lJoueursInterfaceJeu [2].gameObject.SetActive (false);
			lJoueursInterfaceJeu [3].gameObject.SetActive (false);
		}
		if(iNbJoueurs == 3) {
			lMonstresJeu [3].gameObject.SetActive (false);
			lJoueursJeu [3].gameObject.SetActive (false);
			lJoueursInterfaceJeu [3].gameObject.SetActive (false);
		}
	}

	void FixedUpdate () {
		gChoixMonstre.GetComponent<Image>().sprite = lMonstres[index] as Sprite;

		if(lChoixMonstres.Count==1) {
			lMonstresJeu [0].GetComponent<Image> ().sprite = lChoixMonstres[0] as Sprite;
			if(index == lIndex[0]) {
				tmIndicateurCM.text = "1";
			} else {
				tmIndicateurCM.text = "";
			}
		}
		if(lChoixMonstres.Count==2) {
			lMonstresJeu [1].GetComponent<Image> ().sprite = lChoixMonstres[1] as Sprite;
			if(index == lIndex[0]) {
				tmIndicateurCM.text = "1";
			} else if(index == lIndex[1]) {
				tmIndicateurCM.text = "2";
			} else {
				tmIndicateurCM.text = "";
			}
		}
		if(lChoixMonstres.Count==3) {
			lMonstresJeu [2].GetComponent<Image> ().sprite = lChoixMonstres[2] as Sprite;
			if(index == lIndex[0]) {
				tmIndicateurCM.text = "1";
			} else if(index == lIndex[1]) {
				tmIndicateurCM.text = "2";
			} else if(index == lIndex[2]) {
				tmIndicateurCM.text = "3";
			} else {
				tmIndicateurCM.text = "";
			}
		}
		if (lChoixMonstres.Count == 4) {
			lMonstresJeu [3].GetComponent<Image> ().sprite = lChoixMonstres[3] as Sprite;
			if (index == lIndex [0]) {
				tmIndicateurCM.text = "1";
			} else if (index == lIndex [1]) {
				tmIndicateurCM.text = "2";
			} else if (index == lIndex [2]) {
				tmIndicateurCM.text = "3";
			} else if (index == lIndex [3]) {
				tmIndicateurCM.text = "4";
			} else {
				tmIndicateurCM.text = "";
			}
		}

		if(lChoixMonstres.Count == iNbJoueurs) {
			this.transform.parent.transform.parent.transform.gameObject.SetActive (false);

			lMonstresJeu [0].GetComponent<Animator> ().SetTrigger ("tPret");
			lMonstresJeu [1].GetComponent<Animator> ().SetTrigger ("tPret");
			if(lChoixMonstres.Count > 2) {
				lMonstresJeu [2].GetComponent<Animator> ().SetTrigger ("tPret");
			}
			if(lChoixMonstres.Count > 3) {
				lMonstresJeu [3].GetComponent<Animator> ().SetTrigger ("tPret");
			}
		}
	}

	public void Suivant() {
		if (index == iTailleCM-1) {
			index = 0;
		} else {
			index += 1;
		}
	}

	public void Precedent() {
		if (index <= 0) {
			index = iTailleCM - 1;
		} else {
			index -= 1;
		}
	}

	public void Confirmer() {
		if (lChoixMonstres.Count > 0) {
			if(lChoixMonstres.Count == 1) {
				if(index != lIndex[0]) {
					lChoixMonstres.Add (lMonstres [index] as Sprite);
					lIndex.Add (index);
				}
			}
			if(lChoixMonstres.Count == 2 && iNbJoueurs >= 3) {
				if(index != lIndex[0] && index != lIndex[1]) {
					lChoixMonstres.Add (lMonstres [index] as Sprite);
					lIndex.Add (index);
				}
			}
			if(lChoixMonstres.Count == 3 && iNbJoueurs == 4) {
				if(index != lIndex[0] && index != lIndex[1] && index != lIndex[2]) {
					lChoixMonstres.Add (lMonstres [index] as Sprite);
					lIndex.Add (index);
				}
			}
		} else {
			lChoixMonstres.Add (lMonstres [index] as Sprite);
			lIndex.Add (index);
		}
	}
}