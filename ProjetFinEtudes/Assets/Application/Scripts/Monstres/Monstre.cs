using UnityEngine;

[CreateAssetMenu(fileName = "Nouveau Monstre", menuName = "Monstre")]
public class Monstre : ScriptableObject {
	public Sprite artwork;

	[Header("Game setup values")]
	[Range(0,20)]
	public int setupVictoryPoints = 0;
	[Range(0,12)]
	public int setupLifePoints = 10;
}