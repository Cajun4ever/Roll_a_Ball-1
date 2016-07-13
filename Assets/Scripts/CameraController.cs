using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
	[SerializeField]
	GameObject m_Following;

	Vector3 m_Offset;

	void Awake()
	{
		// If a followed object was not set in the inspector then try to set one based on tags
		if (m_Following == null)
			m_Following = GameObject.FindGameObjectWithTag ("Player");

		// If following is still null then exit the function
		if (m_Following == null)
			return;
		
		m_Offset = m_Following.transform.position - transform.position;
	}

	void Update()
	{
		if (m_Following == null)
			return;

		transform.position = m_Following.transform.position - m_Offset;
	}
}
