using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
public class DialogueParser : MonoBehaviour {

	struct DialogueLine
	{
		public string mName;
		public string mContent;
		public int mPose;
		public string[] mOptions;

		public DialogueLine(string name, string content, int pose)
		{
			mName = name;
			mContent = content;
			mPose = pose;
			mOptions = new string[0];
		}
	};

	List<DialogueLine> mLines;
	// Use this for initialization
	void Start ()
	{
		string file = "Assets/Data/DialogueSet.txt";		
		mLines = new List<DialogueLine>();
		LoadDialogue(file);
	}

	void LoadDialogue(string filename)
	{
		string line;
		StreamReader r = new StreamReader(filename);

		using (r)
		{
			do
			{
				line = r.ReadLine();
				if (line != null)
				{
					string[] lineData = line.Split(';');
					if (lineData[0] == "Options")
					{
						DialogueLine lineEntry = new DialogueLine(lineData[0], "", 0);
						lineEntry.mOptions = new string[lineData.Length - 1];
						for (int i = 1; i < lineData.Length; i++)
						{
							lineEntry.mOptions[i - 1] = lineData[i];
						}
						mLines.Add(lineEntry);
					}
					else
					{
						DialogueLine lineEntry = new DialogueLine(lineData[0], lineData[1], int.Parse(lineData[2]));
						mLines.Add(lineEntry);
					}
				}
			}
			while (line != null);
			r.Close();
		}
	}

	public string GetName(int lineNumber)
	{
		if (lineNumber < mLines.Count)
		{
			return mLines[lineNumber].mName;
		}
		return "";
	}

	public string GetContent(int lineNumber)
	{
		if (lineNumber < mLines.Count)
		{
			return mLines[lineNumber].mContent;
		}
		return "";
	}

	public int GetPose(int lineNumber)
	{
		if (lineNumber < mLines.Count)
		{
			return mLines[lineNumber].mPose;
		}
		return 0;
	}

	public string[] GetOptions(int lineNumber)
	{
		if (lineNumber < mLines.Count)
		{
			return mLines[lineNumber].mOptions;
		}
		return new string[0];
	}

	// Update is called once per frame
	void Update ()
	{
		
	}
}
