using UnityEngine;

public class GameManager : MonoBehaviour
{
	private static GameManager instance = null;

	private void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);
		DontDestroyOnLoad(gameObject);
	}

	public static GameManager Instance
	{
		get { return instance; }
	}
}
