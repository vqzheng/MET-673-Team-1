using UnityEngine;
using UnityEngine.UI;
using NUnit.Framework;

public class TankHealthTest
{
	public float m_StartingHealth = 100f;
	private float m_CurrentHealth;  
	private bool m_Dead;     
	public Color m_HealthColor;
	public Color m_FullHealthColor = Color.green;  
	public Color m_ZeroHealthColor = Color.red;

	public void init() {
		m_CurrentHealth = m_StartingHealth;
		m_Dead = false;
		m_HealthColor = m_FullHealthColor;
	}

	public void TakeDamage(float amount)
	{
		// Adjust the tank's current health, update the UI based on the new health and check whether or not the tank is dead.
		m_CurrentHealth -= amount;

		if (m_CurrentHealth <= 0f && !m_Dead) {
			OnDeath();
		}
	}

	private void OnDeath()
	{
		// Play the effects for the death of the tank and deactivate it.
		m_Dead = true;
		m_CurrentHealth = 0;
	}

	[Test]
	public void TakeDamageTest() {
		init ();
		TakeDamage (5);
		Assert.AreEqual (95f, m_CurrentHealth);
		TakeDamage (200);
		Assert.AreEqual (0f, m_CurrentHealth);
	}

	[Test]
	public void DeathTest() {
		init ();
		TakeDamage (5);
		Assert.IsFalse (m_Dead);
		TakeDamage (200);
		Assert.IsTrue (m_Dead);
	}
}