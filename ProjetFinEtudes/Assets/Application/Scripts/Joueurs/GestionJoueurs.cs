using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GestionJoueurs : MonoBehaviour {
	public List<GameObject> lMonstres = new List<GameObject> ();
	public List<GameObject> lJoueurs = new List<GameObject> ();

	[Header("Tokyo")]
	public GameObject gJoueurDansTokyo;
	public string sJoueurDejaDansTokyo;
	public bool bJoueurVientDeRentrerDansTokyo;
	public bool bJoueurDansTokyoEstAttaque;
	public GameObject gInterfaceChoixTokyo;
	public GameObject gAttaquant;
	public GameObject gAnimationPVTokyo;
	public GameObject gBoutonRester;
	public GameObject gBoutonQuitter;
	public TextMeshProUGUI tmChoixTokyoTitre;

	[Space]
	public GameObject gMonstres;
	public GameObject gInterface;

	[Header("Gestion des tours")]
	public List<TextMeshProUGUI> lTmJoueurs = new List<TextMeshProUGUI>();
	public GameObject gM;
	public GameObject gInterfaceDes;
	public GameObject gInterfaceTour;
	public GameObject gBoutonCONFIRMER;
	public GameObject gBoutonRELANCER;
	public GameObject gBoutonTERMINE;
	public int iTour;
	bool bJeuCommence;
	public TextMeshProUGUI tmTourTitre;
	public List<GameObject> lAnimationsNotifications = new List<GameObject> ();

	[Header("Gestion de la victoire")]
	public GameObject gInterfaceVictoire;
	public GameObject gMonstreGagnant;
	public TextMeshProUGUI tmVictoireTitre;
	public TextMeshProUGUI tmVictoireTexte;
	public TextMeshProUGUI tmIndicateurJoueurGagnant;
	public List<GameObject> lJoueursMort = new List<GameObject> ();
	public List<GameObject> lIndicateursMorts = new List<GameObject> ();

	void FixedUpdate () {
		if(lMonstres[0].transform.localPosition == new Vector3(-240, 130, 0) && !bJeuCommence) {
			gInterface.SetActive (true);
			bJeuCommence = true;
			Invoke ("GestionTour", 1);
		}

		for(int i=0; i<gM.GetComponent<ChoixMonstres>().iNbJoueurs; i++) {
			if(lJoueurs[i].GetComponent<Joueur>().iPointsVie <= 0) {
				lIndicateursMorts [i].SetActive (true);
			}
		}
	}

	#region Gestion Tour
	public void GestionTour() {
		iTour += 1;
		if(iTour > gM.GetComponent<ChoixMonstres>().iNbJoueurs) {
			iTour = 1;
		}

		if(lJoueurs[iTour-1].GetComponent<Joueur>().iPointsVie <= 0) {
			GestionTour ();
		} 

		else if (bJoueurDansTokyoEstAttaque) {
			InterfaceChoixTokyo ();
		}

		else {
			InterfaceTour ();
		}
	}

	public void InterfaceTour() {
		gInterfaceTour.SetActive (true);

		tmTourTitre.text = "Tour du joueur " + iTour;
		//print (iTour);

		lAnimationsNotifications.Clear ();


	}

	public void ProchainJoueur() {
		gInterfaceTour.SetActive (false);
		gInterfaceDes.SetActive (true);
		gBoutonCONFIRMER.SetActive (true);
		gBoutonRELANCER.SetActive (true);
		gBoutonTERMINE.SetActive (false);
		bJoueurVientDeRentrerDansTokyo = false;
		bJoueurDansTokyoEstAttaque = false;
	}

	public void TourTermine() {
		gInterfaceDes.SetActive (false);

		Invoke ("GestionTour", 4);

		JouerAnimations ();
		Invoke ("GererValeursJoueurs", 1);
	}

	public void GererValeursJoueurs() {
		lTmJoueurs [0].text = lJoueurs [0].GetComponent<Joueur> ().iPointsVie + "<sprite=6>" + lJoueurs [0].GetComponent<Joueur> ().iEnergie + "<sprite=7>"
			+ lJoueurs [0].GetComponent<Joueur> ().iPointsVictoire + "<sprite=8>";
		if(lJoueurs [0].GetComponent<Joueur> ().iPointsVictoire >= 20) {
			gInterfaceVictoire.SetActive (true);
			tmVictoireTitre.text = "Victoire du joueur 1!";
			tmVictoireTexte.text = lTmJoueurs [0].text;
			gMonstreGagnant.GetComponent<Image>().sprite = lMonstres[0].GetComponent<Image>().sprite;
			tmIndicateurJoueurGagnant.text = "1";
		}

		lTmJoueurs [1].text = lJoueurs [1].GetComponent<Joueur> ().iPointsVie + "<sprite=6>" + lJoueurs [1].GetComponent<Joueur> ().iEnergie + "<sprite=7>"
			+ lJoueurs [1].GetComponent<Joueur> ().iPointsVictoire + "<sprite=8>";
		if(lJoueurs [1].GetComponent<Joueur> ().iPointsVictoire >= 20) {
			gInterfaceVictoire.SetActive (true);
			tmVictoireTitre.text = "Victoire du joueur 2!";
			tmVictoireTexte.text = lTmJoueurs [1].text;
			gMonstreGagnant.GetComponent<Image>().sprite = lMonstres[1].GetComponent<Image>().sprite;
			tmIndicateurJoueurGagnant.text = "2";
		}

		if(gM.GetComponent<ChoixMonstres>().iNbJoueurs > 2) {
			lTmJoueurs [2].text = lJoueurs [2].GetComponent<Joueur> ().iPointsVie + "<sprite=6>" + lJoueurs [2].GetComponent<Joueur> ().iEnergie + "<sprite=7>"
				+ lJoueurs [2].GetComponent<Joueur> ().iPointsVictoire + "<sprite=8>";
			if (lJoueurs [2].GetComponent<Joueur> ().iPointsVictoire >= 20) {
				gInterfaceVictoire.SetActive (true);
				tmVictoireTitre.text = "Victoire du joueur 3!";
				tmVictoireTexte.text = lTmJoueurs [2].text;
				gMonstreGagnant.GetComponent<Image>().sprite = lMonstres[2].GetComponent<Image>().sprite;
				tmIndicateurJoueurGagnant.text = "3";
			}
		}
		if(gM.GetComponent<ChoixMonstres>().iNbJoueurs > 3) {
			lTmJoueurs [3].text = lJoueurs [3].GetComponent<Joueur> ().iPointsVie + "<sprite=6>" + lJoueurs [3].GetComponent<Joueur> ().iEnergie + "<sprite=7>"
				+ lJoueurs [3].GetComponent<Joueur> ().iPointsVictoire + "<sprite=8>";
			if (lJoueurs [3].GetComponent<Joueur> ().iPointsVictoire >= 20) {
				gInterfaceVictoire.SetActive (true);
				tmVictoireTitre.text = "Victoire du joueur 4!";
				tmVictoireTexte.text = lTmJoueurs [3].text;
				gMonstreGagnant.GetComponent<Image>().sprite = lMonstres[3].GetComponent<Image>().sprite;
				tmIndicateurJoueurGagnant.text = "4";
			}
		}

		if(gM.GetComponent<ChoixMonstres>().iNbJoueurs - lJoueursMort.Count == 1) {
			for(int i=0; i<gM.GetComponent<ChoixMonstres>().iNbJoueurs; i++) {
				if(gM.GetComponent<ChoixMonstres>().iNbJoueurs == 3) {
					if(lJoueurs[i] != lJoueursMort[0] && lJoueurs[i] != lJoueursMort[1]) {
						gInterfaceVictoire.SetActive (true);
						tmVictoireTitre.text = "Victoire du joueur " + (i+1) + "!";
						tmVictoireTexte.text = lTmJoueurs [i].text;
						gMonstreGagnant.GetComponent<Image>().sprite = lMonstres[0].GetComponent<Image>().sprite;
						tmIndicateurJoueurGagnant.text = (i + 1).ToString();
					}
				}
				if(gM.GetComponent<ChoixMonstres>().iNbJoueurs == 4) {
					if(lJoueurs[i] != lJoueursMort[0] && lJoueurs[i] != lJoueursMort[1] && lJoueurs[i] != lJoueursMort[2]) {
						gInterfaceVictoire.SetActive (true);
						tmVictoireTitre.text = "Victoire du joueur " + (i+1) + "!";
						tmVictoireTexte.text = lTmJoueurs [i].text;
						gMonstreGagnant.GetComponent<Image>().sprite = lMonstres[0].GetComponent<Image>().sprite;
						tmIndicateurJoueurGagnant.text = (i + 1).ToString();
					}
				}
				else {
					if(lJoueurs[i] != lJoueursMort[0]) {
						gInterfaceVictoire.SetActive (true);
						tmVictoireTitre.text = "Victoire du joueur " + (i+1) + "!";
						tmVictoireTexte.text = lTmJoueurs [i].text;
						gMonstreGagnant.GetComponent<Image>().sprite = lMonstres[0].GetComponent<Image>().sprite;
						tmIndicateurJoueurGagnant.text = (i + 1).ToString();
					}
				}
			}
		}
	}
	#endregion

	#region Tokyo
	public void GestionTokyo() {
		if(gJoueurDansTokyo != null) {
			if (gJoueurDansTokyo.name == "J1") {
				//print ("J1");
				PlacerJoueurDansTokyo ("J1");
			}

			if (gJoueurDansTokyo.name == "J2") {
				//print ("J2");
				PlacerJoueurDansTokyo ("J2");
			}

			if (gJoueurDansTokyo.name == "J3") {
				//print ("J3");
				PlacerJoueurDansTokyo ("J3");
			}

			if (gJoueurDansTokyo.name == "J4") {
				//print ("J4");
				PlacerJoueurDansTokyo ("J4");
			}
		}
	}

	void PlacerJoueurDansTokyo(string joueur) {
		if(joueur == "J1") {
			//print ("J1 placé");
			lMonstres[0].GetComponent<Animator>().SetBool("bTokyo", true);
			if(sJoueurDejaDansTokyo == "") {
				sJoueurDejaDansTokyo = "JT1";
			}
			if(sJoueurDejaDansTokyo != "") {
				if (sJoueurDejaDansTokyo == "JT2") {
					lMonstres[1].GetComponent<Animator>().SetBool("bTokyo", false);
					sJoueurDejaDansTokyo = "JT1";
				}
				if (sJoueurDejaDansTokyo == "JT3") {
					lMonstres[2].GetComponent<Animator>().SetBool("bTokyo", false);
					sJoueurDejaDansTokyo = "JT1";
				}
				if (sJoueurDejaDansTokyo == "JT4") {
					lMonstres[3].GetComponent<Animator>().SetBool("bTokyo", false);
					sJoueurDejaDansTokyo = "JT1";
				}
			}
		}

		if(joueur == "J2") {
			//print ("J2 placé");
			lMonstres[1].GetComponent<Animator>().SetBool("bTokyo", true);
			if(sJoueurDejaDansTokyo == "") {
				sJoueurDejaDansTokyo = "JT2";
			}
			if(sJoueurDejaDansTokyo != "") {
				if (sJoueurDejaDansTokyo == "JT1") {
					lMonstres[0].GetComponent<Animator>().SetBool("bTokyo", false);
					sJoueurDejaDansTokyo = "JT2";
				}
				if (sJoueurDejaDansTokyo == "JT3") {
					lMonstres[2].GetComponent<Animator>().SetBool("bTokyo", false);
					sJoueurDejaDansTokyo = "JT2";
				}
				if (sJoueurDejaDansTokyo == "JT4") {
					lMonstres[3].GetComponent<Animator>().SetBool("bTokyo", false);
					sJoueurDejaDansTokyo = "JT2";
				}
			}
		}

		if(joueur == "J3") {
			//print ("J3 placé");
			lMonstres[2].GetComponent<Animator>().SetBool("bTokyo", true);
			if(sJoueurDejaDansTokyo == "") {
				sJoueurDejaDansTokyo = "JT3";
			}
			if(sJoueurDejaDansTokyo != "") {
				if (sJoueurDejaDansTokyo == "JT1") {
					lMonstres[0].GetComponent<Animator>().SetBool("bTokyo", false);
					sJoueurDejaDansTokyo = "JT3";
				}
				if (sJoueurDejaDansTokyo == "JT2") {
					lMonstres[1].GetComponent<Animator>().SetBool("bTokyo", false);
					sJoueurDejaDansTokyo = "JT3";
				}
				if (sJoueurDejaDansTokyo == "JT4") {
					lMonstres[3].GetComponent<Animator>().SetBool("bTokyo", false);
					sJoueurDejaDansTokyo = "JT3";
				}
			}
		}

		if(joueur == "J4") {
			//print ("J4 placé");
			lMonstres[3].GetComponent<Animator>().SetBool("bTokyo", true);
			if(sJoueurDejaDansTokyo == "") {
				sJoueurDejaDansTokyo = "JT4";
			}
			if(sJoueurDejaDansTokyo != "") {
				if (sJoueurDejaDansTokyo == "JT1") {
					lMonstres[0].GetComponent<Animator>().SetBool("bTokyo", false);
					sJoueurDejaDansTokyo = "JT4";
				}
				if (sJoueurDejaDansTokyo == "JT2") {
					lMonstres[1].GetComponent<Animator>().SetBool("bTokyo", false);
					sJoueurDejaDansTokyo = "JT4";
				}
				if (sJoueurDejaDansTokyo == "JT3") {
					lMonstres[2].GetComponent<Animator>().SetBool("bTokyo", false);
					sJoueurDejaDansTokyo = "JT4";
				}
			}
		}
	}

	void InterfaceChoixTokyo() {
		gInterfaceChoixTokyo.SetActive (true);

		if(sJoueurDejaDansTokyo == "JT1") {
			tmChoixTokyoTitre.text = "Choix du joueur 1";
		}
		if(sJoueurDejaDansTokyo == "JT2") {
			tmChoixTokyoTitre.text = "Choix du joueur 2";
		}
		if(sJoueurDejaDansTokyo == "JT3") {
			tmChoixTokyoTitre.text = "Choix du joueur 3";
		}
		if(sJoueurDejaDansTokyo == "JT4") {
			tmChoixTokyoTitre.text = "Choix du joueur 4";
		}
	}

	public void ResterDansTokyo() {
		gInterfaceChoixTokyo.SetActive (false);
		bJoueurDansTokyoEstAttaque = false;
		Invoke ("InterfaceTour", 2);
	}

	public void Attaque(GameObject attaquant, GameObject animation) {
		gAttaquant = attaquant;
		gAnimationPVTokyo = animation;
	}

	public void QuitterTokyo() {
		gInterfaceChoixTokyo.SetActive (false);
		gJoueurDansTokyo = gAttaquant;
		lAnimationsNotifications.Clear ();
		FileAttente (gAnimationPVTokyo);
		if(gAttaquant == lJoueurs[0]) {
			lJoueurs [0].GetComponent<Joueur> ().iPointsVictoire += 1;
		}
		if(gAttaquant == lJoueurs[1]) {
			lJoueurs [1].GetComponent<Joueur> ().iPointsVictoire += 1;
		}
		if(gAttaquant == lJoueurs[2]) {
			lJoueurs [2].GetComponent<Joueur> ().iPointsVictoire += 1;
		}
		if(gAttaquant == lJoueurs[3]) {
			lJoueurs [3].GetComponent<Joueur> ().iPointsVictoire += 1;
		}
		JouerAnimations ();
		Invoke ("GererValeursJoueurs", 1);


		bJoueurVientDeRentrerDansTokyo = true;
		GestionTokyo ();

		Invoke ("InterfaceTour", 2);
	}
	#endregion


	#region Gestion Animations Notifications
	public void FileAttente(GameObject anim) {
		lAnimationsNotifications.Add (anim);
	}

	public void JouerAnimations() {
		for(int i=0; i<lAnimationsNotifications.Count; i++) {
			lAnimationsNotifications [i].SetActive (true);
			lAnimationsNotifications [i].GetComponent<Animator> ().SetBool ("bPret", true);
		}
	}
	#endregion
}
