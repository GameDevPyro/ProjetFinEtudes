using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Sprint_1 : MonoBehaviour {
	GameObject RefGestionDes;
	TextMeshProUGUI resultats;

	void Start() {
		RefGestionDes = GameObject.Find ("Des").gameObject;
		resultats = this.GetComponent<TextMeshProUGUI> ();
	}

	// Update is called once per frame
	void FixedUpdate () {
		resultats.text = RefGestionDes.GetComponent<GestionDes> ().Energie.ToString ()+"<sprite=7>"
			+RefGestionDes.GetComponent<GestionDes> ().VP.ToString()+"<sprite=8>"
			+RefGestionDes.GetComponent<GestionDes> ().LP.ToString()+"<sprite=6>"
			+RefGestionDes.GetComponent<GestionDes> ().Griffes.ToString()+"<sprite=5>";
	}
}
