using UnityEngine;
using UnityEngine.UI;
using NUnit.Framework;

public class TankShootingTest
{
	public int m_PlayerNumber = 1;              // Used to identify the different players.
	public float m_MinLaunchForce = 15;        // The force given to the shell if the fire button is not held.
	public float m_MaxLaunchForce = 30;        // The force given to the shell if the fire button is held for the max charge time.
	public float m_MaxChargeTime = 0.75f;       // How long the shell can charge for before it is fired at max force.


	private string m_FireButton;                // The input axis that is used for launching shells.
	private float m_CurrentLaunchForce;         // The force that will be given to the shell when the fire button is released.
	private float m_ChargeSpeed;                // How fast the launch force increases, based on the max charge time.
	private bool m_Fired;                       // Whether or not the shell has been launched with this button press.


	public void Init() 
	{
		m_CurrentLaunchForce = m_MinLaunchForce;
		m_ChargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime;
		m_Fired = false;
		
	}

	public void CalculateForce(float delta_time) 
	{
		m_CurrentLaunchForce += m_ChargeSpeed * delta_time;

		if (m_CurrentLaunchForce >= m_MaxLaunchForce && !m_Fired) {
			// ... use the max force and launch the shell.
			m_CurrentLaunchForce = m_MaxLaunchForce;
			m_Fired = true;
		}
		else if (m_Fired) {
			m_Fired = false;
			m_CurrentLaunchForce = m_MinLaunchForce;
		}

	}
		
	[Test]
	public void CalculateForceTest() {
		Init ();
		CalculateForce (0);
		Assert.AreEqual (m_MinLaunchForce, m_CurrentLaunchForce);
		Assert.IsFalse (m_Fired);
		CalculateForce (0.5f);
		Assert.AreEqual (25, m_CurrentLaunchForce);
		Assert.IsFalse (m_Fired);
		CalculateForce (1);
		Assert.AreEqual (30, m_CurrentLaunchForce);
		Assert.IsTrue (m_Fired);
		CalculateForce (0.1f);
		Assert.AreEqual (m_MinLaunchForce, m_CurrentLaunchForce);
		Assert.IsFalse (m_Fired);
	}
}
