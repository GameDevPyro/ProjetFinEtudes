using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GestionBoutonsDes : MonoBehaviour {
	GameObject refGestionDes;
	Button boutonRelancer;
	Button boutonConfirmer;
	Button boutonRedemarrer;

	// Use this for initialization
	void Start () {
		refGestionDes = GameObject.Find ("Des").transform.gameObject;
		boutonRelancer = GameObject.Find("BoutonRELANCER").GetComponent<Button>();
		boutonConfirmer = GameObject.Find("BoutonOK").GetComponent<Button>();
		boutonRedemarrer = GameObject.Find ("BoutonRedemarrer").GetComponent<Button> ();
		boutonRedemarrer.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(refGestionDes.GetComponent<GestionDes>().selectionne == true) {
			boutonRelancer.interactable = true;
			boutonConfirmer.interactable = false;
		}
		if(refGestionDes.GetComponent<GestionDes>().selectionne == false) {
			boutonRelancer.interactable = false;
			boutonConfirmer.interactable = true;
		}
		if(refGestionDes.GetComponent<GestionDes>().lancer >= 3) {
			boutonRelancer.gameObject.SetActive(false);
			boutonConfirmer.gameObject.SetActive (false);
			boutonRedemarrer.gameObject.SetActive (true);
		}
	}

	public void Redemarrer() {
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
	}
}
