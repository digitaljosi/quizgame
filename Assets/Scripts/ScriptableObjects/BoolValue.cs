using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BoolValue : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField]
    private bool initialValue;
    // Start is called before the first frame update

    public bool RuntimeValue;
     public void OnAfterDeserialize(){
        RuntimeValue = initialValue;
    }

    public void OnBeforeSerialize(){
       
    }
}
