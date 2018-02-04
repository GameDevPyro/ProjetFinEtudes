using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Gestion_Tutoriel_Sprint_1 : MonoBehaviour {
	int page;
	TextMeshProUGUI titre;
	TextMeshProUGUI texte;
	Button boutonPagePrecedente;
	Button boutonPageSuivante;
	Button boutonDemarrer;

	// Use this for initialization
	void Start () {
		print (Screen.currentResolution);
		titre = GameObject.Find ("Titre").GetComponent<TextMeshProUGUI> ();
		texte = GameObject.Find ("Texte").GetComponent<TextMeshProUGUI> ();
		boutonPagePrecedente = GameObject.Find ("BoutonPagePrecedente").GetComponent<Button> ();
		boutonPageSuivante = GameObject.Find ("BoutonPageSuivante").GetComponent<Button> ();
		boutonDemarrer = GameObject.Find ("BoutonDemarrer").GetComponent<Button> ();
		boutonDemarrer.gameObject.SetActive (false);
		AfficherPage ();
	}
		
	void AfficherPage() {
		if(page==0) {
			boutonPagePrecedente.gameObject.SetActive (false);
			titre.text = "Tutoriel";
			texte.text = "Bienvenue.\n\nCe tutoriel explique le fonctionnement des dés dans le jeu.";
		}
		if(page==1) {
			boutonPagePrecedente.gameObject.SetActive (true);
			titre.text = "1. Lancer les dés";
			texte.text = "À son tour, le joueur a droit a droit à un maximum de trois lancers. " +
				"\n\nAu premier lancer, le joueur lance les dés." +
				"\n\nPour les lancers suivantes, il peut relancer tous les dés ou seulement ceux de son choix " +
				"(même parmi ceux qu’il aurait conservé lors d’un lancer précédent).";
		}
		if (page == 2) {
			titre.text = "2. Résoudre les dés";
			texte.text = "Les symboles de vos dés représentent vos actions du tour." +
				"\n\n<b>Description des dés:</b>" +
				"\n\n<sprite=0>  /<sprite=1>  /<sprite=2>  : Points de Victoire" +
				"\n\n<sprite=3>  : Coeur<sprite=6>" +
				"\n\n<sprite=4>  : Éclair Énergie<sprite=7>" +
				"\n\n<sprite=5>  : Griffe";
			texte.fontSize = 45;
		}
		if (page == 3) {
			titre.text = "3. Effets des dés";
			texte.text = "<b>Points de Victoire</b>" +
				"\nSi vous obtenez au moins un triple <sprite=0>   ,<sprite=1>   ou<sprite=2>" +
				"\n(3 dés identiques), vous gagnez autant de Points de Victoire (<sprite=8>) que le chiffre indiqué sur le dé." +
				"\n\nChaque dé obtenu en plus des trois premiers avec le même chiffre vous fait gagner 1<sprite=8> supplémentaire."
				+"\n\nDans le jeu, un joueur peut gagner en amassant 20 Points de Victoire (<sprite=8>).";
		}
		if(page == 4) {
			texte.text = "<b>Coeurs</b>" +
				"\nChaque Coeur<sprite=3>  obtenu permet de récupérer 1 Points de Vie (<sprite=6>)." +
				"\n\nLe joueur ne peut avoir plus que 10<sprite=6>." +
				"\n\nLorsque les Points de Vie d'un joueur atteignent 0<sprite=6>, il est hors de la partie.";
			texte.fontSize = 50;
		}
		if(page == 5) {
			texte.text = "<b>Éclairs Énergie</b>" +
				"\nVous obtenez 1 Éclair Énergie (<sprite=7>) pour chaque <sprite=4>  ." +
				"\n\nLes Éclairs Énergie(<sprite=7>) du joueur se conserve, jusqu'à ce qu'ils soient utilisés." +
				"\n\nDans le jeu, les Éclairs Énergie (<sprite=7>) sont utilisés pour acheter des cartes, qui donnent un avantage au joueur qui les achètent.";
		}
		if(page == 6) {
			texte.text = "<b>Griffes</b>" +
				"\nChaque Griffe<sprite=5>  obtenu entraîne la perte d'un Point de Vie <sprite=6> aux autres joueurs." +
				"\n\nSi un joueur n'a plus de Point de Vie<sprite=6>, il est éliminé.";
			if(boutonDemarrer.gameObject.activeSelf) {
				boutonDemarrer.gameObject.SetActive (false);
			}
			if(!boutonPageSuivante.gameObject.activeSelf) {
				boutonPageSuivante.gameObject.SetActive (true);
			}
			if(texte.fontSize != 50) {
				texte.fontSize = 50;
			}
			if (texte.alignment == TextAlignmentOptions.Center) {
				texte.alignment = TextAlignmentOptions.TopLeft;
			}
		}
		if(page == 7) {
			titre.text = "Fin du tutoriel";
			texte.text = "Cliquez sur le bouton «Démarrer» pour jouer.";
			texte.alignment = TextAlignmentOptions.Center;
			texte.fontSize = 70;
			boutonPageSuivante.gameObject.SetActive (false);
			boutonDemarrer.gameObject.SetActive (true);
		}
	}

	public void PagePrecedente() {
		page -= 1;
		AfficherPage ();
	}

	public void PageSuivante() {
		page += 1;
		AfficherPage ();
	}

	public void Demarrer() {
		SceneManager.LoadScene ("Sprint_1");
	}
}
