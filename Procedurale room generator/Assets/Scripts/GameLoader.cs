using UnityEngine;

public class GameLoader : MonoBehaviour
{
	public GameObject gameManager = null;

	private void Awake()
	{
		if (GameManager.Instance == null)
			Instantiate(gameManager);
	}
}
