using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class De : MonoBehaviour {
	Button bouton;

	[HideInInspector]
	public bool etatBouton;
	[HideInInspector]
	public int valeur;
	[HideInInspector]
	public bool relancer;

	public Image Contour;
	public Faces TypeDe;

	// Use this for initialization
	void Start () {
		bouton = this.GetComponent<Button> ();
		bouton.onClick.AddListener (Actif);
		etatBouton = false;
		relancer = false;
	}

	public void Actif() {
		etatBouton = !etatBouton;
		relancer = etatBouton;
		Contour.gameObject.SetActive (etatBouton);
	}
}
