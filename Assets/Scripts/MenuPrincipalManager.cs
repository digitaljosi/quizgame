using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour

{
  [SerializeField] private string nomeDoLevelDeJogo;
  [SerializeField] private GameObject painelMenuInicial;
  [SerializeField] private GameObject painelOpcoes;
  
  
  public void Jogar()
  {
      SceneManager.LoadScene("Scenes/Fase");
  }

  public void AbrirOpcoes()
  {
  painelMenuInicial.SetActive(false);
  painelOpcoes.SetActive(true);

  }

  public void FecharOpcoes()
  {
  painelOpcoes.SetActive(false);
  painelMenuInicial.SetActive(true);
  }

  public void SairJogo()
  {
      Debug.Log("Sair do Jogo");
      Application.Quit();
  }
}
