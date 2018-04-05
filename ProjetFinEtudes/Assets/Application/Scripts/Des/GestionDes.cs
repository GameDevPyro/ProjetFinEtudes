using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class GestionDes : MonoBehaviour {
	[HideInInspector]
	public List<int> lResultats = new List<int> ();
	[HideInInspector]
	public List<GameObject> lDes;
	int nbDes;
	[HideInInspector]
	public List<GameObject> lRelancer;
	[HideInInspector]
	public bool bSelectionne;

	//Valeurs pour compter
	public int iLancer;
	int iNbChiffre1;
	int iNbChiffre2;
	int iNbChiffre3;
	int iNbCoeurs;
	int iNbEclairs;
	public int iNbGriffes;

	public GameObject gInterfaceDes;
	public GameObject gJoueurs;
	public List<GameObject> lJoueurs = new List<GameObject>();
	public List<GameObject> lAnimationsGriffes = new List<GameObject>();
	public List<GameObject> lAnimationsLP = new List<GameObject>();
	public List<GameObject> lAnimationsEnergie = new List<GameObject>();
	public List<GameObject> lAnimationsPV = new List<GameObject>();
	public List<GameObject> lAnimationsPVTokyo = new List<GameObject>();
	GameObject copie1;
	GameObject copie2;
	GameObject copie3;
	GameObject copieC;
	GameObject copieE;
	GameObject copieP;
	public TextMeshProUGUI tmTitre;
	public TextMeshProUGUI tmRessources;
	public GameObject gBoutonTERMINE;
	public GameObject gM;

	void Start () {
		foreach(GameObject g in GameObject.FindGameObjectsWithTag("De")) {
			nbDes += 1;
		}
		for(int i=0; i<nbDes; i++) {
			//print(transform.GetChild(i).transform.GetChild(1).name);
			lDes.Add (transform.GetChild(i).transform.GetChild(1).gameObject);
		}
		bSelectionne = false;
		Calcul ();
		PremierResultat ();
	}

	void FixedUpdate () {
		for (int i = 0; i < nbDes; i++) {
			if (lDes [i].GetComponent<De> ().bRelancer == true) {
				bSelectionne = true;
				break;
			} else {
				bSelectionne = false;
			}
		}

		tmTitre.text = "joueur " + gJoueurs.GetComponent<GestionJoueurs>().iTour;

		tmRessources.text = lJoueurs[gJoueurs.GetComponent<GestionJoueurs>().iTour - 1].GetComponent<Joueur>().iPointsVie + "<sprite=6>" 
			+ lJoueurs[gJoueurs.GetComponent<GestionJoueurs>().iTour - 1].GetComponent<Joueur>().iEnergie + "<sprite=7>" 
			+ lJoueurs[gJoueurs.GetComponent<GestionJoueurs>().iTour - 1].GetComponent<Joueur>().iPointsVictoire + "<sprite=8>";

		if (iLancer == 3) {
			for (int i = 0; i < nbDes; i++) {
				lDes [i].GetComponent<Button> ().interactable = false;
			}
		} else {
			for (int i = 0; i < nbDes; i++) {
				lDes [i].GetComponent<Button> ().interactable = true;
			}
		}
	}

	#region Dés
	void Calcul() {
		for(int i=0; i<nbDes; i++) {
			int r = Mathf.RoundToInt(Random.Range(1.0f,6.0f));
			lResultats.Add (r);
		}
	}

	void PremierResultat() {
		iLancer += 1;
		for(int i=0; i<nbDes; i++) {
			//print (lResultats [i]);
			lDes [i].GetComponent<De> ().iValeur = lResultats [i];
		}
		AfficherFaces ();
	}

	void AfficherFaces() {
		for(int i=0; i<nbDes; i++) {
			//print (lResultats [i]);
			if(lDes [i].GetComponent<De> ().iValeur == 1) {
				lDes[i].GetComponent<Image>().sprite = lDes [i].GetComponent<De> ().soTypeDe.sFace1;
			}
			if(lDes [i].GetComponent<De> ().iValeur == 2) {
				lDes[i].GetComponent<Image>().sprite = lDes [i].GetComponent<De> ().soTypeDe.sFace2;
			}
			if(lDes [i].GetComponent<De> ().iValeur == 3) {
				lDes[i].GetComponent<Image>().sprite = lDes [i].GetComponent<De> ().soTypeDe.sFace3;
			}
			if(lDes [i].GetComponent<De> ().iValeur == 4) {
				lDes[i].GetComponent<Image>().sprite = lDes [i].GetComponent<De> ().soTypeDe.sFace4;
			}
			if(lDes [i].GetComponent<De> ().iValeur == 5) {
				lDes[i].GetComponent<Image>().sprite = lDes [i].GetComponent<De> ().soTypeDe.sFace5;
			}
			if(lDes [i].GetComponent<De> ().iValeur == 6) {
				lDes[i].GetComponent<Image>().sprite = lDes [i].GetComponent<De> ().soTypeDe.sFace6;
			}
		}
		lDesactiverContours ();
	}
	#endregion

	#region Relancer
	public void VerifierSiRelancer() {
		if(iLancer<3) {
			for (int i = 0; i < nbDes; i++) {
				if (lDes [i].GetComponent<De> ().bRelancer == true) {
					lRelancer.Add (lDes [i].gameObject);
					lDes [i].GetComponent<De> ().iValeur = Mathf.RoundToInt (Random.Range (1.0f, 6.0f));
				}
			}
			iLancer += 1;
			AfficherFaces ();
		}
		if(iLancer>=3) {
			CompterFaces ();
		}
	}

	void lDesactiverContours() {
		lRelancer.Clear ();
		for(int i=0; i<nbDes; i++) {
			transform.GetChild(i).transform.GetChild(0).gameObject.SetActive(false);
			transform.GetChild (i).transform.GetChild (1).GetComponent<De> ().bRelancer = false;
			transform.GetChild (i).transform.GetChild (1).GetComponent<De> ().bEtatBouton = false;
		}
	}

	public void RelancerInitial() {
		iLancer = 1;
		iNbChiffre1 = 0;
		iNbChiffre2 = 0;
		iNbChiffre3 = 0;
		iNbCoeurs = 0;
		iNbEclairs = 0;
		iNbGriffes = 0;
		foreach(GameObject g in lDes) {
			g.GetComponent<De> ().iValeur = Mathf.RoundToInt (Random.Range (1.0f, 6.0f));
		}
		AfficherFaces ();
	}
	#endregion

	#region Résultat Final
	public void CompterFaces() {
		for(int i=0; i<nbDes; i++) {
			lResultats [i] = lDes [i].GetComponent<De> ().iValeur;
			//print (lResultats [i]);

			if(lResultats[i] == 1) {
				iNbChiffre1 += 1;
			}
			if(lResultats[i] == 2) {
				iNbChiffre2 += 1;
			}
			if(lResultats[i] == 3) {
				iNbChiffre3 += 1;
			}
			if(lResultats[i] == 4) {
				iNbCoeurs += 1;
			}
			if(lResultats[i] == 5) {
				iNbEclairs += 1;
			}
			if(lResultats[i] == 6) {
				iNbGriffes += 1;
			}
		}
		iLancer = 3;
		CalculPointsEtVies ();
	}

	void CalculPointsEtVies() {
		if(iNbChiffre1==3) {
			lJoueurs [gJoueurs.GetComponent<GestionJoueurs>().iTour - 1].GetComponent<Joueur> ().iPointsVictoire += 1;
			//lAnimationsPV [gJoueurs.GetComponent<GestionJoueurs> ().iTour - 1].SetActive (true);

			//Animation Notification
			copie1 = Instantiate (lAnimationsPV [gJoueurs.GetComponent<GestionJoueurs> ().iTour - 1]);
			copie1.transform.SetParent(lJoueurs [gJoueurs.GetComponent<GestionJoueurs> ().iTour - 1].transform);
			copie1.transform.localScale = new Vector3 (1, 1, 1);
			copie1.GetComponent<TextMeshProUGUI> ().text = "+" + 1 + "<sprite=8>";
			gJoueurs.GetComponent<GestionJoueurs> ().FileAttente (copie1);
		}
		if(iNbChiffre1>3) {
			lJoueurs [gJoueurs.GetComponent<GestionJoueurs>().iTour - 1].GetComponent<Joueur> ().iPointsVictoire += (iNbChiffre1 - 3) + 1;

			//Animation Notification
			copie1 = Instantiate (lAnimationsPV [gJoueurs.GetComponent<GestionJoueurs> ().iTour - 1]);
			copie1.transform.SetParent(lJoueurs [gJoueurs.GetComponent<GestionJoueurs> ().iTour - 1].transform);
			copie1.transform.localScale = new Vector3 (1, 1, 1);
			copie1.GetComponent<TextMeshProUGUI> ().text = "+" + ((iNbChiffre1 - 3) + 1) + "<sprite=8>";
			gJoueurs.GetComponent<GestionJoueurs> ().FileAttente (copie1);
		}
		if(iNbChiffre2==3) {
			lJoueurs [gJoueurs.GetComponent<GestionJoueurs>().iTour - 1].GetComponent<Joueur> ().iPointsVictoire += 2;

			//Animation Notification
			copie2 = Instantiate (lAnimationsPV [gJoueurs.GetComponent<GestionJoueurs> ().iTour - 1]);
			copie2.transform.SetParent(lJoueurs [gJoueurs.GetComponent<GestionJoueurs> ().iTour - 1].transform);
			copie2.transform.localScale = new Vector3 (1, 1, 1);
			copie2.GetComponent<TextMeshProUGUI> ().text = "+" + 2 + "<sprite=8>";
			gJoueurs.GetComponent<GestionJoueurs> ().FileAttente (copie2);
		}
		if(iNbChiffre2>3) {
			lJoueurs [gJoueurs.GetComponent<GestionJoueurs>().iTour - 1].GetComponent<Joueur> ().iPointsVictoire += (iNbChiffre2 - 3) + 2;

			//Animation Notification
			copie2 = Instantiate (lAnimationsPV [gJoueurs.GetComponent<GestionJoueurs> ().iTour - 1]);
			copie2.transform.SetParent(lJoueurs [gJoueurs.GetComponent<GestionJoueurs> ().iTour - 1].transform);
			copie2.transform.localScale = new Vector3 (1, 1, 1);
			copie2.GetComponent<TextMeshProUGUI> ().text = "+" + ((iNbChiffre2 - 3) + 2) + "<sprite=8>";
			gJoueurs.GetComponent<GestionJoueurs> ().FileAttente (copie2);
		}
		if(iNbChiffre3==3) {
			lJoueurs [gJoueurs.GetComponent<GestionJoueurs>().iTour - 1].GetComponent<Joueur> ().iPointsVictoire += 3;

			//Animation Notification
			copie3 = Instantiate (lAnimationsPV [gJoueurs.GetComponent<GestionJoueurs> ().iTour - 1]);
			copie3.transform.SetParent(lJoueurs [gJoueurs.GetComponent<GestionJoueurs> ().iTour - 1].transform);
			copie3.transform.localScale = new Vector3 (1, 1, 1);
			copie3.GetComponent<TextMeshProUGUI> ().text = "+" + 3 + "<sprite=8>";
			gJoueurs.GetComponent<GestionJoueurs> ().FileAttente (copie3);
		}
		if(iNbChiffre3>3) {
			lJoueurs [gJoueurs.GetComponent<GestionJoueurs>().iTour - 1].GetComponent<Joueur> ().iPointsVictoire += (iNbChiffre3 - 3) + 3;

			//Animation Notification
			copie3 = Instantiate (lAnimationsPV [gJoueurs.GetComponent<GestionJoueurs> ().iTour - 1]);
			copie3.transform.SetParent(lJoueurs [gJoueurs.GetComponent<GestionJoueurs> ().iTour - 1].transform);
			copie3.transform.localScale = new Vector3 (1, 1, 1);
			copie3.GetComponent<TextMeshProUGUI> ().text = "+" + ((iNbChiffre3 - 3) + 3) + "<sprite=8>";
			gJoueurs.GetComponent<GestionJoueurs> ().FileAttente (copie3);
		}
		if(iNbCoeurs>=1) {
			if (gJoueurs.GetComponent<GestionJoueurs> ().gJoueurDansTokyo != lJoueurs [gJoueurs.GetComponent<GestionJoueurs> ().iTour - 1].transform.gameObject 
				|| gJoueurs.GetComponent<GestionJoueurs>().gJoueurDansTokyo == null) {

				if(lJoueurs [gJoueurs.GetComponent<GestionJoueurs>().iTour - 1].GetComponent<Joueur> ().iPointsVie + iNbCoeurs > 10) {
					lJoueurs [gJoueurs.GetComponent<GestionJoueurs> ().iTour - 1].GetComponent<Joueur> ().iPointsVie = 10;
					iNbCoeurs = 10 - lJoueurs [gJoueurs.GetComponent<GestionJoueurs> ().iTour - 1].GetComponent<Joueur> ().iPointsVie;
				} else {
					lJoueurs [gJoueurs.GetComponent<GestionJoueurs>().iTour - 1].GetComponent<Joueur> ().iPointsVie += iNbCoeurs;
				}

				//Animation Notification
				if(iNbCoeurs!=0) {
					copieC = Instantiate (lAnimationsLP [gJoueurs.GetComponent<GestionJoueurs> ().iTour - 1]);
					copieC.transform.SetParent(lJoueurs [gJoueurs.GetComponent<GestionJoueurs> ().iTour - 1].transform);
					copieC.transform.localScale = new Vector3 (1, 1, 1);
					copieC.GetComponent<TextMeshProUGUI> ().text = "+" + iNbCoeurs + "<sprite=6>";
					gJoueurs.GetComponent<GestionJoueurs> ().FileAttente (copieC);
				}
			} 
		}
		if(iNbEclairs>=1) {
			lJoueurs [gJoueurs.GetComponent<GestionJoueurs>().iTour - 1].GetComponent<Joueur> ().iEnergie += iNbEclairs;

			//Animation Notification
			copieE = Instantiate (lAnimationsEnergie [gJoueurs.GetComponent<GestionJoueurs> ().iTour - 1]);
			copieE.transform.SetParent(lJoueurs [gJoueurs.GetComponent<GestionJoueurs> ().iTour - 1].transform);
			copieE.transform.localScale = new Vector3 (1, 1, 1);
			copieE.GetComponent<TextMeshProUGUI> ().text = "+" + iNbEclairs + "<sprite=7>";
			gJoueurs.GetComponent<GestionJoueurs> ().FileAttente (copieE);
		}
		if(iNbGriffes>=1) {
			//Si Tokyo est vide, le joueur entre dans Tokyo
			if(gJoueurs.GetComponent<GestionJoueurs>().gJoueurDansTokyo == null) {
				print (lJoueurs [gJoueurs.GetComponent<GestionJoueurs> ().iTour] + " entre dans Tokyo");
				gJoueurs.GetComponent<GestionJoueurs> ().gJoueurDansTokyo = lJoueurs [gJoueurs.GetComponent<GestionJoueurs> ().iTour - 1].transform.gameObject;
				lJoueurs [gJoueurs.GetComponent<GestionJoueurs> ().iTour - 1].GetComponent<Joueur> ().iPointsVictoire += 1;
				gJoueurs.GetComponent<GestionJoueurs> ().bJoueurVientDeRentrerDansTokyo = true;

				//Animation Notification
				copieP = Instantiate (lAnimationsPVTokyo [gJoueurs.GetComponent<GestionJoueurs> ().iTour - 1]);
				copieP.transform.SetParent(lJoueurs [gJoueurs.GetComponent<GestionJoueurs> ().iTour - 1].transform);
				copieP.transform.localScale = new Vector3 (1, 1, 1);
				copieP.GetComponent<TextMeshProUGUI> ().text = "+" + 1 + "<sprite=8>";
				gJoueurs.GetComponent<GestionJoueurs> ().FileAttente (copieP);
			} else {
				//Si le joueur est dans Tokyo
				if (gJoueurs.GetComponent<GestionJoueurs> ().gJoueurDansTokyo == lJoueurs [gJoueurs.GetComponent<GestionJoueurs> ().iTour - 1].transform.gameObject) {
					print ("Le joueur dans Tokyo attaque tous les autres joueurs");
					for (int i = 0; i<gM.GetComponent<ChoixMonstres> ().iNbJoueurs; i++) {
						if (lJoueurs [i].transform.gameObject != gJoueurs.GetComponent<GestionJoueurs> ().gJoueurDansTokyo) {
							
							lAnimationsGriffes [i].GetComponent<Animator> ().SetTrigger ("tAttaque");

							if (lJoueurs [i].GetComponent<Joueur> ().iPointsVie - iNbGriffes < 0) {
								lJoueurs [i].GetComponent<Joueur> ().iPointsVie = 0;

								if(gJoueurs.GetComponent<GestionJoueurs> ().lJoueursMort.Count == 0) {
									gJoueurs.GetComponent<GestionJoueurs> ().lJoueursMort.Add (lJoueurs [i].transform.gameObject);
								}
								if (gJoueurs.GetComponent<GestionJoueurs> ().lJoueursMort.Count == 1 && gM.GetComponent<ChoixMonstres>().iNbJoueurs >= 3) {
									if (gJoueurs.GetComponent<GestionJoueurs> ().lJoueursMort [0] != lJoueurs [i].transform.gameObject) {
										gJoueurs.GetComponent<GestionJoueurs> ().lJoueursMort.Add (lJoueurs [i].transform.gameObject);
									}
								} 
								if (gJoueurs.GetComponent<GestionJoueurs> ().lJoueursMort.Count == 2 && gM.GetComponent<ChoixMonstres>().iNbJoueurs == 4) {
									if (gJoueurs.GetComponent<GestionJoueurs> ().lJoueursMort [0] != lJoueurs [i].transform.gameObject && gJoueurs.GetComponent<GestionJoueurs> ().lJoueursMort [1] != lJoueurs [i].transform.gameObject) {
										gJoueurs.GetComponent<GestionJoueurs> ().lJoueursMort.Add (lJoueurs [i].transform.gameObject);
									}
								} 
							}

							if (lJoueurs [i].GetComponent<Joueur> ().iPointsVie - iNbGriffes > 0) {
								lJoueurs [i].GetComponent<Joueur> ().iPointsVie -= iNbGriffes;
							}

							//Animation Notification
							copieC = Instantiate (lAnimationsLP [i]);
							copieC.transform.SetParent (lJoueurs [i].transform);
							copieC.transform.localScale = new Vector3 (1, 1, 1);
							copieC.GetComponent<TextMeshProUGUI> ().text = "-" + iNbGriffes + "<sprite=6>";
							gJoueurs.GetComponent<GestionJoueurs> ().FileAttente (copieC);
						}
					}
				}

				//Si le joueur n'est pas dans Tokyo
				else if (gJoueurs.GetComponent<GestionJoueurs> ().gJoueurDansTokyo != lJoueurs [gJoueurs.GetComponent<GestionJoueurs> ().iTour - 1].transform.gameObject) {
					gJoueurs.GetComponent<GestionJoueurs> ().bJoueurDansTokyoEstAttaque = true;
					for(int i=0; i<gM.GetComponent<ChoixMonstres>().iNbJoueurs; i++) {
						if(lJoueurs[i].transform.gameObject == gJoueurs.GetComponent<GestionJoueurs> ().gJoueurDansTokyo) {
							lAnimationsGriffes[i].GetComponent<Animator>().SetTrigger("tAttaque");

							if(lJoueurs[i].GetComponent<Joueur> ().iPointsVie - iNbGriffes <= 0) {
								print ("Joueur dans Tokyo est mort");
								gJoueurs.GetComponent<GestionJoueurs> ().lJoueursMort.Add(gJoueurs.GetComponent<GestionJoueurs> ().gJoueurDansTokyo);
								lJoueurs [i].GetComponent<Joueur> ().iPointsVie = 0;
								gJoueurs.GetComponent<GestionJoueurs> ().gBoutonRester.SetActive (false);
								gJoueurs.GetComponent<GestionJoueurs> ().gBoutonQuitter.transform.localPosition = new Vector3(0, -100, 0);
							} else if(lJoueurs[i].GetComponent<Joueur> ().iPointsVie - iNbGriffes > 0) {
								print ("Joueur dans Tokyo est attaqué");
								lJoueurs[i].GetComponent<Joueur> ().iPointsVie -= iNbGriffes;
								gJoueurs.GetComponent<GestionJoueurs> ().gBoutonRester.SetActive (true);
								gJoueurs.GetComponent<GestionJoueurs> ().gBoutonRester.transform.localPosition = new Vector3(-150, -100, 0);
								gJoueurs.GetComponent<GestionJoueurs> ().gBoutonQuitter.transform.localPosition = new Vector3(150, -100, 0);
							}

							//Attaque
							//Animation Notification
							copieP = Instantiate (lAnimationsPVTokyo [gJoueurs.GetComponent<GestionJoueurs> ().iTour - 1]);
							copieP.transform.SetParent(lJoueurs [gJoueurs.GetComponent<GestionJoueurs> ().iTour - 1].transform);
							copieP.transform.localScale = new Vector3 (1, 1, 1);
							copieP.GetComponent<TextMeshProUGUI> ().text = "+" + 1 + "<sprite=8>";
							gJoueurs.GetComponent<GestionJoueurs> ().Attaque (lJoueurs [gJoueurs.GetComponent<GestionJoueurs> ().iTour - 1].transform.gameObject, copieP);

							//Animation Notification
							copieC = Instantiate (lAnimationsLP [i]);
							copieC.transform.SetParent(lJoueurs [i].transform);
							copieC.transform.localScale = new Vector3 (1, 1, 1);
							copieC.GetComponent<TextMeshProUGUI> ().text = "-" + iNbGriffes + "<sprite=6>";
							gJoueurs.GetComponent<GestionJoueurs> ().FileAttente (copieC);
						}
					}
				}
			}	
		}

		if(gJoueurs.GetComponent<GestionJoueurs> ().gJoueurDansTokyo == lJoueurs [gJoueurs.GetComponent<GestionJoueurs> ().iTour - 1].transform.gameObject
			&& !gJoueurs.GetComponent<GestionJoueurs> ().bJoueurVientDeRentrerDansTokyo) {
			print ("Le joueur a commencé son tour dans Tokyo");
			lJoueurs [gJoueurs.GetComponent<GestionJoueurs> ().iTour - 1].GetComponent<Joueur> ().iPointsVictoire += 2;

			//Animation Notification
			copieP = Instantiate (lAnimationsPVTokyo [gJoueurs.GetComponent<GestionJoueurs> ().iTour - 1]);
			copieP.transform.SetParent(lJoueurs [gJoueurs.GetComponent<GestionJoueurs> ().iTour - 1].transform);
			copieP.transform.localScale = new Vector3 (1, 1, 1);
			copieP.GetComponent<TextMeshProUGUI> ().text = "+" + 2 + "<sprite=8>";
			gJoueurs.GetComponent<GestionJoueurs> ().FileAttente (copieP);
		}

		gBoutonTERMINE.SetActive (true);
	}
	#endregion
}
