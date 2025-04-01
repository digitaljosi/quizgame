using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManeger : MonoBehaviour
{
    public void ReiniciarJogo(){
        //SceneManager.LoadScene("Menu");
        Application.Quit();
    }
}
