using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChoixNbJoueurs : MonoBehaviour {
	public int iNbJoueurs;
	public GameObject gM;
	public GameObject gMonstres;
	TextMeshProUGUI tmTitre;

	void Start() {
		gM.SetActive (false);
		tmTitre = GameObject.Find ("Titre").GetComponent<TextMeshProUGUI> ();
		tmTitre.text = "Nombre de joueurs";
	}

	public void Choix() {
		gM.GetComponent<ChoixMonstres> ().iNbJoueurs = this.iNbJoueurs;
		this.transform.parent.gameObject.SetActive (false);
		gM.SetActive (true);
	}
}
