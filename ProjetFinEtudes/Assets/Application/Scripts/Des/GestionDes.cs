using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class GestionDes : MonoBehaviour {
	[HideInInspector]
	public List<int> resultats = new List<int> ();
	[HideInInspector]
	public List<GameObject> des;
	int nbDes;
	[HideInInspector]
	public List<GameObject> listeRelancer;

	[HideInInspector]
	public bool selectionne;

	//Valeurs pour compter
	[HideInInspector]
	public int lancer;
	int nb1;
	int nb2;
	int nb3;
	int nbCoeurs;
	int nbEclairs;
	int nbGriffes;

	//Valeurs finales des résultats
	[HideInInspector]
	public int VP;
	[HideInInspector]
	public int LP;
	[HideInInspector]
	public int Energie;
	[HideInInspector]
	public int Griffes;

	// Use this for initialization
	void Start () {
		foreach(GameObject g in GameObject.FindGameObjectsWithTag("De")) {
			nbDes += 1;
		}
		for(int i=0; i<nbDes; i++) {
			//print(transform.GetChild(i).transform.GetChild(1).name);
			des.Add (transform.GetChild(i).transform.GetChild(1).gameObject);
		}
		selectionne = false;
		Calcul ();
		PremierResultat ();
		lancer += 1;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		for (int i = 0; i < nbDes; i++) {
			if (des [i].GetComponent<De> ().relancer == true) {
				selectionne = true;
				break;
			} else {
				selectionne = false;
			}
		}
		//print (lancer);
	}

	#region Dés
	void Calcul() {
		for(int i=0; i<nbDes; i++) {
			int r = Mathf.RoundToInt(Random.Range(1.0f,6.0f));
			resultats.Add (r);
		}
	}

	void PremierResultat() {
		for(int i=0; i<nbDes; i++) {
			//print (resultats [i]);
			des [i].GetComponent<De> ().valeur = resultats [i];
		}
		AfficherFaces ();
	}

	void AfficherFaces() {
		for(int i=0; i<nbDes; i++) {
			//print (resultats [i]);
			if(des [i].GetComponent<De> ().valeur == 1) {
				des[i].GetComponent<Image>().sprite = des [i].GetComponent<De> ().TypeDe.face1;
			}
			if(des [i].GetComponent<De> ().valeur == 2) {
				des[i].GetComponent<Image>().sprite = des [i].GetComponent<De> ().TypeDe.face2;
			}
			if(des [i].GetComponent<De> ().valeur == 3) {
				des[i].GetComponent<Image>().sprite = des [i].GetComponent<De> ().TypeDe.face3;
			}
			if(des [i].GetComponent<De> ().valeur == 4) {
				des[i].GetComponent<Image>().sprite = des [i].GetComponent<De> ().TypeDe.face4;
			}
			if(des [i].GetComponent<De> ().valeur == 5) {
				des[i].GetComponent<Image>().sprite = des [i].GetComponent<De> ().TypeDe.face5;
			}
			if(des [i].GetComponent<De> ().valeur == 6) {
				des[i].GetComponent<Image>().sprite = des [i].GetComponent<De> ().TypeDe.face6;
			}
		}
		DesactiverContours ();
	}
	#endregion

	#region Relancer
	public void VerifierSiRelancer() {
		if(lancer<3) {
			for (int i = 0; i < nbDes; i++) {
				if (des [i].GetComponent<De> ().relancer == true) {
					listeRelancer.Add (des [i].gameObject);
					des [i].GetComponent<De> ().valeur = Mathf.RoundToInt (Random.Range (1.0f, 6.0f));
				}
			}
			lancer += 1;
			AfficherFaces ();
		}
		if(lancer>=3) {
			CompterFaces ();
		}
	}

	void DesactiverContours() {
		listeRelancer.Clear ();
		for(int i=0; i<nbDes; i++) {
			transform.GetChild(i).transform.GetChild(0).gameObject.SetActive(false);
			transform.GetChild (i).transform.GetChild (1).GetComponent<De> ().relancer = false;
			transform.GetChild (i).transform.GetChild (1).GetComponent<De> ().etatBouton = false;
		}
	}
	#endregion

	#region Résultat Final
	public void CompterFaces() {
		for(int i=0; i<nbDes; i++) {
			resultats [i] = des [i].GetComponent<De> ().valeur;
			//print (resultats [i]);

			if(resultats[i] == 1) {
				nb1 += 1;
			}
			if(resultats[i] == 2) {
				nb2 += 1;
			}
			if(resultats[i] == 3) {
				nb3 += 1;
			}
			if(resultats[i] == 4) {
				nbCoeurs += 1;
			}
			if(resultats[i] == 5) {
				nbEclairs += 1;
			}
			if(resultats[i] == 6) {
				nbGriffes += 1;
			}
		}
		lancer = 3;
		CalculPointsEtVies ();
	}

	void CalculPointsEtVies() {
		if(nb1==3) {
			VP += 1;
		}
		if(nb1>3) {
			VP += (nb1 - 3) + 1;
		}
		if(nb2==3) {
			VP += 2;
		}
		if(nb2>3) {
			VP += (nb2 - 3) + 2;
		}
		if(nb3==3) {
			VP += 3;
		}
		if(nb3>3) {
			VP += (nb3 - 3) + 3;
		}
		if(nbCoeurs>=1) {
			LP += nbCoeurs;
		}
		if(nbEclairs>=1) {
			Energie += nbEclairs;
		}
		if(nbGriffes>=1) {
			Griffes += nbGriffes;
		}
	}
	#endregion
}
