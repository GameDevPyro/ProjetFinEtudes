using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GestionBoutonsDes : MonoBehaviour {
	GameObject gDes;
	Button btnRelancer;
	Button btnConfirmer;

	// Use this for initialization
	void Start () {
		gDes = GameObject.Find ("Des").transform.gameObject;
		btnRelancer = GameObject.Find("BoutonRELANCER").GetComponent<Button>();
		btnConfirmer = GameObject.Find("BoutonCONFIRMER").GetComponent<Button>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(gDes.GetComponent<GestionDes>().bSelectionne == true) {
			btnRelancer.interactable = true;
			btnConfirmer.interactable = false;
		}
		if(gDes.GetComponent<GestionDes>().bSelectionne == false) {
			btnRelancer.interactable = false;
			btnConfirmer.interactable = true;
		}
		if(gDes.GetComponent<GestionDes>().iLancer >= 3) {
			btnRelancer.gameObject.SetActive(false);
			btnConfirmer.gameObject.SetActive (false);
		}
	}
}
