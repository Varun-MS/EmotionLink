using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChoiceButton : MonoBehaviour
{

	public string mOption;
	public DialogueManager mBox;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void SetText(string newText)
	{
		this.GetComponentInChildren<Text>().text = newText;
	}

	public void SetOption(string newOption)
	{
		this.mOption = newOption;
	}

	public void ParseOption()
	{		
		mBox.mIsPlayerTalking = false;
		mBox.mCurrentLine = int.Parse(mOption);
		mBox.ShowDialogue();		
	}
}