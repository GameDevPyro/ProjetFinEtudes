using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class De : MonoBehaviour {
	Button btn;

	[HideInInspector]
	public bool bEtatBouton;
	[HideInInspector]
	public int iValeur;
	[HideInInspector]
	public bool bRelancer;

	public Image imgContour;
	public Faces soTypeDe;

	// Use this for initialization
	void Start () {
		btn = this.GetComponent<Button> ();
		btn.onClick.AddListener (Actif);
		bEtatBouton = false;
		bRelancer = false;
	}

	public void Actif() {
		bEtatBouton = !bEtatBouton;
		bRelancer = bEtatBouton;
		imgContour.gameObject.SetActive (bEtatBouton);
	}
}
