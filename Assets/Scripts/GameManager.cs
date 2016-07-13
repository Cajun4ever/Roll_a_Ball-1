using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	[SerializeField]
	ObstacleController m_ObstaclePrefab;
	[SerializeField]
	int m_ObstacleCount;

	[SerializeField]
	Vector3 m_ObstacleMinPosition;
	[SerializeField]
	Vector3 m_ObstacleMaxPosition;

	[SerializeField]
	GameObject m_CoinPrefab;
	[SerializeField]
	int m_CoinCount;

	[SerializeField]
	Vector3 m_CoinMinPosition;
	[SerializeField]
	Vector3 m_CoinMaxPosition;

	[SerializeField]
	Text m_WinText;

	public static GameManager self;

	UnityEvent m_ScoreCallback;

	int m_Score = 0;

	bool m_GameOver;

	public int score
	{
		get{ return m_Score; }
		set{ m_Score = value; m_ScoreCallback.Invoke (); }
	}
	public UnityEvent scoreCallback
	{
		get{ return m_ScoreCallback; }	
	}

	public bool gameOver
	{
		get{ return m_GameOver; }
		set{ m_GameOver = value; }
	}

	void Awake()
	{
		if (self == null)
			self = this;
		else
			Destroy (gameObject);

		m_ScoreCallback = new UnityEvent ();

		for (int i = 0; i < m_CoinCount; ++i) 
		{
			Instantiate (
				m_CoinPrefab, 
				new Vector3(
					Random.Range(m_CoinMinPosition.x, m_CoinMaxPosition.x),
					Random.Range(m_CoinMinPosition.y, m_CoinMaxPosition.y),
					Random.Range(m_CoinMinPosition.z, m_CoinMaxPosition.z)),
				Quaternion.identity);
		}

		for (int i = 0; i < m_ObstacleCount; ++i) 
		{
			ObstacleController newObstacle = Instantiate (
				m_ObstaclePrefab,
				new Vector3 (
					Random.Range (m_ObstacleMinPosition.x, m_ObstacleMaxPosition.x),
					Random.Range (m_ObstacleMinPosition.y, m_ObstacleMaxPosition.y),
					Random.Range (m_ObstacleMinPosition.z, m_ObstacleMaxPosition.z)),
				Quaternion.identity) as ObstacleController;

			int randomDirection = Random.Range (1, 4);

			newObstacle.minPosition = m_ObstacleMinPosition;
			newObstacle.maxPosition = m_ObstacleMaxPosition;

			switch (randomDirection) 
			{
			case 1:
				newObstacle.speed = new Vector3(Random.Range(1, newObstacle.speed.x), 0f, 0f);
				break;
			case 2:
				newObstacle.speed = new Vector3(0f, Random.Range(1, newObstacle.speed.y), 0f);
				break;
			case 3:
				newObstacle.speed = new Vector3(0f, 0f, Random.Range(1, newObstacle.speed.z));
				break;
			}
		}

		m_WinText.gameObject.SetActive(false);
	}

	public void PlayerFallen()
	{
		m_WinText.gameObject.SetActive(true);
		m_WinText.text = "Failure...";

		m_GameOver = true;
	}

	public void RemoveCoin(GameObject coin)
	{
		if (coin.tag != "Coin")
			return;

		Destroy (coin);

		if (GameObject.FindGameObjectsWithTag ("Coin").Length <= 1) 
		{
			m_WinText.gameObject.SetActive(true);
			m_WinText.text = "You've Won!";

			m_GameOver = true;
		}
	}
}
