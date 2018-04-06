using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;


public class InputParser : MonoBehaviour {

    InputField input;
    InputField.SubmitEvent se;
    GameObject laptop;

    string[] keyWords = new string[] { "angst", "apprehension", "doubt", "disquiet", "dread", "tense", "nervous", "scared", "worried", "nobody", "lonely", "alone", "stressed", "afraid", "not", "never", "sad", "hate", "no", "fear" };
    bool[] keyWordsPresent = new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
    int matchCount = 0;

    public Text output;

    static bool ExactMatch(string input, string match)
    {
        return Regex.IsMatch(input, string.Format(@"\b{0}\b", Regex.Escape(match)));
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Start ()
    {
        input = gameObject.GetComponent<InputField>();
        se = new InputField.SubmitEvent();
        se.AddListener(SubmitInput);
        input.onEndEdit = se;
        laptop = GameObject.Find("Pivot");
	}

    private void SubmitInput(string arg0)
    {
        //string currentText = output.text;
        //string newText = currentText + "\n" + arg0;

        string newText = arg0;

        for (int i = 0; i < keyWords.Length; i++)
        {
            if (ExactMatch(newText.ToLower(), keyWords[i].ToLower()))
            {
                keyWordsPresent[i] = true;
                matchCount++;
            }
        }

        if (matchCount >= 2)
        {
            output.text = "\nYou don't seem to be doing too well! \n\nLet's talk about it some more. . .";
            StartCoroutine(Transition());
        }

        else
        {
            output.text = "\nYou seem to be ready to tackle tomorrow! Good luck!\n\n";
            Invoke("Restart", 5);
        }

        input.text = " ";
        input.ActivateInputField();
    }

    IEnumerator Transition()
    {
        yield return new WaitForSeconds(2);
        laptop.GetComponent<Animator>().SetBool("close", true);
        yield return new WaitForSeconds(1);
        output.text = "";
        Initiate.Fade("Chapter1", Color.black, 0.4f);

    }

}
