using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Text))]
public class ScoreController : MonoBehaviour 
{
	[SerializeField]
	Text m_Text;

	void Awake()
	{
		if (m_Text == null)
			m_Text = GetComponent<Text> ();
	}
	void Start()
	{		
		GameManager.self.scoreCallback.AddListener (
			delegate 
			{
				UpdateScore();
			});

		UpdateScore ();
	}

	void UpdateScore()
	{
		m_Text.text = "Score: " + GameManager.self.score.ToString();
	}
}
