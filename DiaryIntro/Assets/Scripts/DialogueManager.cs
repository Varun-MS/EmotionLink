using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
	DialogueParser mParser;

	public string mCharacterDialogue;
	public string mCharacterName;
	public int mCharacterPose;

	public int mCurrentLine;
	
	string[] mPlayerOptions;
	public bool mIsPlayerTalking;
	List<Button> buttons = new List<Button>();

	public Text mDialogueBox;
	public GameObject mPlayerChoiceBox;
    FadeText fadeProcess;
    public bool sent = false;

	// Use this for initialization
	void Start()
	{
		mCharacterDialogue = "";
		mCharacterName = "";
		mCharacterPose = 0;
		mIsPlayerTalking = false;
		mParser = GameObject.Find("DialogueParser").GetComponent<DialogueParser>();
		mCurrentLine = 0;
        fadeProcess = GetComponentInChildren<FadeText>();
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetMouseButtonDown(0) && mIsPlayerTalking == false)
		{   
			if (mCurrentLine == 110)
				Application.Quit();
			ShowDialogue();			
		}
		UpdateUI();
	}
	
	void UpdateUI()
	{
		if (!mIsPlayerTalking)
		{
			ClearButtons();
		}
        mDialogueBox.text = mCharacterDialogue;
        if (mDialogueBox.text != "" && !sent)
        {
            mDialogueBox.enabled = false;
            fadeProcess.Start();
            sent = true;
        }
	}

	void ClearButtons()
	{
		for (int i = 0; i < buttons.Count; i++)
		{
			print("Clearing buttons");
			Button b = buttons[i];
			buttons.Remove(b);
			Destroy(b.gameObject);
		}
	}

	public void ShowDialogue()
	{		
		ParseLine();

		if (mCurrentLine != 40 && mCurrentLine != 57)
			mCurrentLine++;
		else
			mCurrentLine = 110;
	}

	void ParseLine()
	{
		if (mParser.GetName(mCurrentLine) != "Options")
		{
			mIsPlayerTalking = false;
			mCharacterName = mParser.GetName(mCurrentLine);
			mCharacterDialogue = mParser.GetContent(mCurrentLine);
			mCharacterPose = mParser.GetPose(mCurrentLine);

			GameObject character = GameObject.Find("Anxiety");
			character.GetComponent<Character>().setTrigger(mCharacterPose);
		}
		else
		{
			mIsPlayerTalking = true;
			mCharacterName = "";
			mCharacterDialogue = "";
			mCharacterPose = 0;
			mPlayerOptions = mParser.GetOptions(mCurrentLine);
			CreateButtons();
		}
	}

	void CreateButtons()
	{
        sent = false;
		for (int i = 0; i < mPlayerOptions.Length; i++)
		{
			GameObject button = (GameObject)Instantiate(mPlayerChoiceBox);
			Button b = button.GetComponent<Button>();
			ChoiceButton cb = button.GetComponent<ChoiceButton>();
			cb.SetText(mPlayerOptions[i].Split(':')[0]);
			cb.mOption = mPlayerOptions[i].Split(':')[1];
			cb.mBox = this;
			b.transform.SetParent(this.transform);
			b.transform.localPosition = new Vector3(0, -25 + (i * 50));
			b.transform.localScale = new Vector3(1, 1, 1);
			buttons.Add(b);
		}
	}
}

