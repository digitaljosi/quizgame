using UnityEngine;
using UnityEngine.UI;

public class StartDialogButton : MonoBehaviour
{
    [SerializeField] Dialog dialog; // Referência ao Dialog que queremos mostrar

    private void Start()
    {
        // Adiciona o listener ao botão
        GetComponent<Button>().onClick.AddListener(StartDialog);
    }

    void StartDialog()
    {
        StartCoroutine(DialogManager.Instance.ShowDialog(dialog));
    }
}
