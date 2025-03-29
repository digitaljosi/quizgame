using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RepositorioRespostas : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField]
    private List<string[]> initialListaRespostas = new List<string[]>();

    public List<string[]> runtimeListaRespostas;
    
    public void OnAfterDeserialize(){
        runtimeListaRespostas = initialListaRespostas;
    }

    public void OnBeforeSerialize(){

    }
}
