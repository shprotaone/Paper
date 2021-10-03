using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerUpRotation : MonoBehaviour
{

	#region Settings
	[SerializeField] private float rotationSpeed = 99.0f;
	[SerializeField] private bool reverse = false;
	#endregion

	void Update ()
	{
		if(this.reverse)
		    transform.Rotate(new Vector3(0f,1f,0f) * Time.deltaTime * this.rotationSpeed);
		else			
			transform.Rotate(new Vector3(0f,1f,0f) * Time.deltaTime * this.rotationSpeed);
	}

	public void SetRotationSpeed(float speed)
	{
		this.rotationSpeed = speed;
	}

	public void SetReverse(bool reverse)
	{
		this.reverse = reverse;
	}
}
