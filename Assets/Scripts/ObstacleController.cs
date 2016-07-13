using UnityEngine;
using System.Collections;

[System.Serializable]
public class Moving
{
	public int x = 1;
	public int y = 1;
	public int z = 1;
}

[RequireComponent(typeof(Rigidbody))]
public class ObstacleController : MonoBehaviour 
{
	[SerializeField]
	Rigidbody m_Rigidbody;

	Vector3 m_MinPosition;
	Vector3 m_MaxPosition;

	[SerializeField]
	Vector3 m_Speed;

	Moving m_Forward;

	public Vector3 minPosition
	{
		get{ return m_MinPosition; }
		set{ m_MinPosition = value; }
	}
	public Vector3 maxPosition
	{
		get{ return m_MaxPosition; }
		set{ m_MaxPosition = value; }
	}

	public Vector3 speed
	{
		get{ return m_Speed; }
		set{ m_Speed = value; }
	}

	void Awake()
	{
		m_Forward = new Moving ();

		if (m_Rigidbody == null)
			m_Rigidbody = GetComponent<Rigidbody> ();

		if (m_Rigidbody == null)
			return;
	}

	void Start()
	{
		if (m_Speed.x == 0)
			m_Rigidbody.constraints |= RigidbodyConstraints.FreezePositionX;
		if (m_Speed.y == 0)
			m_Rigidbody.constraints |= RigidbodyConstraints.FreezePositionY;
		if (m_Speed.z == 0)
			m_Rigidbody.constraints |= RigidbodyConstraints.FreezePositionZ;
	}

	// Update is called once per frame
	void Update () 
	{
		if (m_Rigidbody == null)
			return;

		if (transform.position.x >= m_MaxPosition.x) 
			m_Forward.x = -1;
		else if (transform.position.x <= m_MinPosition.x)
			m_Forward.x = 1;

		if (transform.position.y >= m_MaxPosition.y) 
			m_Forward.y = -1; 
		else if (transform.position.y <= m_MinPosition.y)
			m_Forward.y = 1;

		if (transform.position.z >= m_MaxPosition.z) 
			m_Forward.z = -1; 
		else if (transform.position.z <= m_MinPosition.z)
			m_Forward.z = 1;

		m_Rigidbody.velocity = 
			new Vector3 (
				m_Speed.x * m_Forward.x,
				m_Speed.y * m_Forward.y,
				m_Speed.z * m_Forward.z);
	}
}
