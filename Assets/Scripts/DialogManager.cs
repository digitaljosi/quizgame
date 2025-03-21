using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [SerializeField] GameObject dialogBox;
    [SerializeField] Text dialogText;
    [SerializeField] int lettersPerSecond = 5;
    public static DialogManager Instance { get; private set; }

    public event Action OnShowDialog;
    public event Action OnHideDialog;
    GameManager gameManager;

    private void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        Instance = this;
    }
    Dialog dialog;
    int currentLine =  0;
    bool isTyping;

    public IEnumerator ShowDialog(Dialog dialog)
    {
        yield return new WaitForEndOfFrame();
        OnShowDialog?.Invoke();
        this.dialog = dialog;
        dialogBox.SetActive(true);
        StartCoroutine(TypeDialog(dialog.Lines[0]));
    }
    public void HandleUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Z) && !isTyping)
        {
            ++currentLine;
            if(currentLine < dialog.Lines.Count)
            {
                StartCoroutine(TypeDialog(dialog.Lines[currentLine]));
            }
            else
            {
                currentLine = 0;
                dialogBox.SetActive(false);
                OnHideDialog?.Invoke();
            }
        }
        if (Input.GetKeyUp(KeyCode.X))
        {
            currentLine = 0;
            dialogBox.SetActive(false);
            OnHideDialog?.Invoke();
            gameManager.OnStartQuiz();
        }
    }

    public IEnumerator TypeDialog(string Line)
    {
        isTyping = true;
        dialogText.text = "";
        foreach ( var letter in Line.ToCharArray() )
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f/ lettersPerSecond);
        }
        isTyping = false;
    }

}
