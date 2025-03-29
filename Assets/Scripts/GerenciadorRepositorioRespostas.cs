using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class GerenciadorRepositorioRespostas : MonoBehaviour
{
    [SerializeField]
    private RepositorioRespostas repositorioRespostas;

    public void AddResposta(string[] respota){
        repositorioRespostas.runtimeListaRespostas.Add(respota);
    }

    public List<string[]> GetRespostas(){
        return repositorioRespostas.runtimeListaRespostas;
    }
}
