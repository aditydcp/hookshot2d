using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
	public float SlowdownFactor = 0.05f;

	private float _initialFixedDeltaTime = 0f;

    void Start()
    {
		_initialFixedDeltaTime = Time.fixedDeltaTime;   
    }

	public void EnableSlowmotion()
	{
		Time.timeScale = SlowdownFactor;
		Time.fixedDeltaTime = Time.timeScale * .01f;
	}

	public void DisableSlowmotion()
	{
		Time.timeScale = 1;
		Time.fixedDeltaTime = _initialFixedDeltaTime;
	}
}
