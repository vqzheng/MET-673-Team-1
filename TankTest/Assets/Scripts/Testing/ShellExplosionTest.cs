using UnityEngine;
using NUnit.Framework;

public class ShellExplosionTest
{
public float m_MaxDamage = 100f;                    // The amount of damage done if the explosion is centred on a tank.
	public float m_ExplosionForce = 1000f;              // The amount of force added to a tank at the centre of the explosion.
	public float m_MaxLifeTime = 2f;                    // The time in seconds before the shell is removed.
	public float m_ExplosionRadius = 5f;                // The maximum distance away from the explosion tanks can be and are still affected.
	public Vector3 position;						// the position of tank

	private void init() {
		position = new Vector3 (0, 0, 0);
	}

	private float CalculateDistance(Vector3 v1, Vector3 v2) {
		return Mathf.Sqrt ((v1.x - v2.x) * (v1.x - v2.x)
			+ (v1.y - v2.y) * (v1.y - v2.y)
			+ (v1.z - v2.z) * (v1.z - v2.z));
	}

	private float CalculateDamage (Vector3 targetPosition)
	{
		// Calculate the distance from the shell to the target.
		float explosionDistance = CalculateDistance(position, targetPosition);

		// Calculate the proportion of the maximum distance (the explosionRadius) the target is away.
		float relativeDistance = (m_ExplosionRadius - explosionDistance) / m_ExplosionRadius;

		// Calculate damage as this proportion of the maximum possible damage.
		float damage = relativeDistance * m_MaxDamage;

		// Make sure that the minimum damage is always 0.
		damage = Mathf.Max (0f, damage);

		return damage;
	}

	[Test]
	public void CalculateDistanceTest() {
		Vector3 v1 = new Vector3 (0, 0, 0);
		Vector3 v2 = new Vector3 (1, 1, 1);
		Vector3 v3 = new Vector3 (0, 0, 1);
		Assert.AreEqual (Mathf.Sqrt (3), CalculateDistance(v1, v2));
		Assert.AreEqual (1, CalculateDistance (v1, v3));
	}

	[Test]
	public void CalculateDamageTest() {
		init ();
		Vector3 v1 = new Vector3 (2, 2, 2);
		Vector3 v2 = new Vector3 (1, 1, 1);
		Vector3 v3 = new Vector3 (5, 5, 5);
		Assert.AreEqual (30.717968f, CalculateDamage (v1));
		Assert.AreEqual (65.3589859f, CalculateDamage (v2));
		Assert.AreEqual (0, CalculateDamage (v3));
	}
}
