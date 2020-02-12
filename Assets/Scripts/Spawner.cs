using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject[] fruits;
	public GameObject bomb;

	public float xBounds, yBound;

	// Use this for initialization
	void Start () {
		StartCoroutine (SpawnRandomGameObject ());
	}
	
	IEnumerator SpawnRandomGameObject(){

		yield return new WaitForSeconds (Random.Range (1, 2));

		int randomFruit = Random.Range (0, 2);

		if (Random.value <= .6f) {
			Instantiate (fruits [randomFruit], new Vector2 (Random.Range (-xBounds, xBounds), yBound), Quaternion.identity);
		} else {
			Instantiate (bomb, new Vector2 (Random.Range (-xBounds, xBounds), yBound), Quaternion.identity);
		}

		StartCoroutine (SpawnRandomGameObject ());
	}
}
