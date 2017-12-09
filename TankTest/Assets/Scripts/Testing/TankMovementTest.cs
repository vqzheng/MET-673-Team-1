
using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;


public class TankMovementTest
{
	public int m_PlayerNumber = 1;         
	public float m_Speed = 12f;            
	public float m_TurnSpeed = 250f;  
	private Rigidbody m_Rigidbody;       
	private Vector3 position;
	private Quaternion quaternion;


	public void Move(float m_MovementInputValue, float delta_Time)
	{
		// Adjust the position of the tank based on the player's input.
		Vector3 movement =  Vector3.forward * m_MovementInputValue * m_Speed * delta_Time;
		position += movement;

	}

	public void Init() 
	{
		position = new Vector3 (0, 0, 0);
		quaternion = eularQuaternion (0f, 0f, 0f);
	}

	private void Turn(float m_TurnInputValue, float delta_time)
	{
		// Adjust the rotation of the tank based on the player's input.

		float turn = m_TurnInputValue * m_TurnSpeed * delta_time;
		Quaternion turnRotation = eularQuaternion (0f, turn, 0f);

		quaternion *= turnRotation;

	}

	private Quaternion eularQuaternion(double pitch, double roll, double yaw)
	{
		Quaternion q;
		// Abbreviations for the various angular functions
		float cy = Mathf.Cos((float)(yaw * 0.5));
		float sy = Mathf.Sin((float)(yaw * 0.5));
		float cr = Mathf.Cos((float)(roll * 0.5));
		float sr = Mathf.Sin((float)(roll * 0.5));
		float cp = Mathf.Cos((float)(pitch * 0.5));
		float sp = Mathf.Sin((float)(pitch * 0.5));

		q.w = cy * cr * cp + sy * sr * sp;
		q.x = cy * sr * cp - sy * cr * sp;
		q.y = cy * cr * sp + sy * sr * cp;
		q.z = sy * cr * cp - cy * sr * sp;
		return q;
	}

	private bool isEqualVector3(Vector3 v1, Vector3 v2) 
	{
		if (Mathf.Abs((float)v1.x - (float)v2.x) < 0.001 
			&& Mathf.Abs((float)v1.y - (float)v2.y) < 0.001
			&& Mathf.Abs((float)v1.z - (float)v2.z) < 0.001)
			return true;
		return false;
	}

	private bool isEqualQuaternion(Quaternion q1, Quaternion q2) 
	{
		if (Mathf.Abs(q1.w - q2.w) < 0.01
			&& Mathf.Abs(q1.x - q2.x) < 0.01
			&& Mathf.Abs(q1.y - q2.y) < 0.01
			&& Mathf.Abs(q1.z - q2.z) < 0.01)
			return true;
		return false;
	}


	[Test]
	public void MoveForwardTest() 
	{
		// if can forward correctly
		Init ();
		Move (1, 1.2f);
		Assert.IsTrue(isEqualVector3(position, new Vector3(0,0,(float)14.4)));
	}

	[Test]
	public void MoveBackwardTest() 
	{
		// if can backward correctly
		Init();
		Move (-1, 1.2f);
		Assert.IsTrue(isEqualVector3(position, new Vector3(0,0,(float)-14.4)));
	}

	[Test]
	public void CombineMoveTest()
	{
		Init ();
		Move (1, 1.0f);
		Move (-1, 1.0f);
		Assert.IsTrue(isEqualVector3(position, new Vector3(0, 0, 0)));
	}


	[Test]
	public void TurnLeftTest() 
	{
		// if can turn left correctly
		Init ();
		Turn (1, 5);
		Quaternion res = new Quaternion (0.176f, 0.0f, 0.0f, -0.98f);
		Assert.IsTrue(isEqualQuaternion(quaternion, res));
	}


	[Test]
	public void TurnRightTest() 
	{
		// if can turn left correctly
		Init ();
		Turn (-1, 5);
		Quaternion res = new Quaternion (-0.176f, 0.0f, 0.0f, -0.98f);
		Assert.IsTrue(isEqualQuaternion(quaternion, res));
	}

	[Test]
	public void CombineTurnTest()
	{
		Init ();
		Turn (1, 5);
		Turn (-1, 5);
		Assert.IsTrue(isEqualQuaternion(quaternion, new Quaternion(0f, 0f, 0f, 1.0f)));
	}

	[Test]
	public void CombineMovementTest() 
	{
		// combine forward, backward, and turn together
		Init();
		Move (1, 1);
		Move (-1, 1);
		Turn (1, 1);
		Turn (-1, 1);
		Assert.IsTrue (isEqualVector3 (position, new Vector3 (0f, 0f, 0f)));
	}

}

