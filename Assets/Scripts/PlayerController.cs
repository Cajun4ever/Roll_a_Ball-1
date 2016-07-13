using UnityEngine;
using System.Collections;
using UnityEngine.Events;

// Explicitly Require the Rigidbody Component
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour 
{
	[SerializeField]
	Rigidbody m_Rigidbody;
	// Coefficient for x and z speed
	[SerializeField]
	float m_Speed;
	// Coefficient for y speed
	[SerializeField]
	float m_JumpPower;

	bool m_IsGrounded;

	void Awake()
	{
		// If the rigidbody was not set in the inspector, set it here
		if (m_Rigidbody == null)
			m_Rigidbody = GetComponent<Rigidbody> ();
	}
	// Called everytime physics callculations are done in Unity's Engine
	void FixedUpdate()
	{
		// If a Rigidbody does not exist or the game is over, stop all movement and return
		if (m_Rigidbody == null || GameManager.self.gameOver) 
		{
			m_Rigidbody.velocity =
				new Vector3 (
					0f,
					m_Rigidbody.velocity.y,
					0f);
			return;
		}

		// Jumps if possible
		if (Input.GetAxis ("Jump") > 0f && m_IsGrounded)
			m_Rigidbody.velocity = 
				new Vector3(
					m_Rigidbody.velocity.x,
					m_JumpPower,
					m_Rigidbody.velocity.z);

		// Moves based on user input
		m_Rigidbody.velocity = 
			new Vector3 (
				Input.GetAxis("Horizontal") * m_Speed,
				m_Rigidbody.velocity.y,
				Input.GetAxis("Vertical") * m_Speed);
	}

	void LateUpdate()
	{
		// If the player's position is less than a certain value, then the player should be considered 'dead'
		if (transform.position.y <= -3f)
			GameManager.self.PlayerFallen ();
	}

	void OnTriggerEnter(Collider other)
	{
		// If the object that is colliding with this one is tagged with "Coin"
		if (other.tag == "Coin") 
		{
			
			GameManager.self.score += 1;
			GameManager.self.RemoveCoin (other.gameObject);
		}
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Ground")
			m_IsGrounded = true;
	}
	void OnCollisionExit(Collision other)
	{
		if (other.gameObject.tag == "Ground")
			m_IsGrounded = false;
	}
}
