using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{

	public Sprite[] characterPoses = null;
	public Animator mAnimation; 
	// Use this for initialization
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void setTrigger(int trigger)
	{
		switch (trigger)
		{
			case 0: mAnimation.SetTrigger("Idle");
				break;
			case 1: mAnimation.SetTrigger("Surprised");
				break;
			case 2: mAnimation.SetTrigger("Agitated");
				break;
			case 3:	mAnimation.SetTrigger("Calm");
				break;
		}
	}
}