
using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI inputScore;
    [SerializeField] private TMP_InputField inputName;
    [SerializeField] private TextMeshProUGUI extraText;

    public UnityEvent<string, int> submitScoreEvent;

    public void SubmitScore()
    {
        if (inputName.text.Length > 10)
        {
            extraText.text = "username must have 10 or less characters";
            return;
        }
        submitScoreEvent.Invoke(inputName.text, int.Parse(inputScore.text));
        extraText.text = "score submitted!";
    }
}