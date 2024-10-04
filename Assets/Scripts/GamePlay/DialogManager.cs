using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogManager : MonoBehaviour
{

    int currentLine;
    Dialog dialog2;
    [SerializeField] GameObject dialogBox;
    [SerializeField] Text dialogText;
    [SerializeField] int lettersPerSecond;

    [SerializeField] ChoiceManager choiceBox;

    public event System.Action OnShowDialog;
    public event System.Action OnCloseDialog;

    public bool isShowing { get; private set; }

    // [SerializeField] AudioClip typeSound;
    // [SerializeField] AudioClip openDialogSound;
    // [SerializeField] AudioClip closeDialogSound;

    public static DialogManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

    }

    public IEnumerator ShowDialog(Dialog dialog, List<string> choices = null)
    {

        yield return new WaitForEndOfFrame();

        dialog2 = dialog;
        isShowing = true;
        OnShowDialog?.Invoke();
        dialogBox.SetActive(true);
        AudioManager.Instance.PlaySoundEffect(CommonAudios.Select);
        StartCoroutine(TypeDialog(dialog.Lines[0]));

        if (choices == null)
        {
            choices = new List<string>();
            choices.Add("Option 1");
            choices.Add("Option 2");
        }

        if (choices != null)
        {


            for (int i = 0; i < choices.Count; i++)
            {
                Debug.Log(choices[i]);
            }

            if (choices.Count > 1)
            {
                choiceBox.ShowChoices(choices);
            }
        }
    }

    public void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            currentLine = currentLine + 1;
            AudioManager.Instance.PlaySoundEffect(CommonAudios.Select);
            if (currentLine == dialog2.Lines.Count)
            {
                currentLine = 0;
                isShowing = false;
                dialogBox.SetActive(false);
                OnCloseDialog?.Invoke();
                // StartCoroutine(choiceBox.HideChoices());
                return;
            }
            StartCoroutine(TypeDialog(dialog2.Lines[currentLine]));
        }
    }

    public IEnumerator TypeDialog(string line)
    {
        dialogText.text = "";
        foreach (var letter in line.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
    }
}