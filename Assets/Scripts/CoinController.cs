using UnityEngine;
using System.Collections;

public class CoinController : MonoBehaviour 
{	
	[SerializeField]
	Vector3 m_Speed;

	// Update is called once per frame
	void Update () 
	{
		transform.Rotate (m_Speed);
	}
}
