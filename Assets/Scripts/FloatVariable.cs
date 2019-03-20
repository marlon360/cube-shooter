using System;
using UnityEngine;

[CreateAssetMenu]
public class FloatVariable : ScriptableObject, ISerializationCallbackReceiver {

    [NonSerialized]
    public float Value;
    
    public float InitialValue;
    
    public void OnAfterDeserialize () {
        Reset();
    }

    public void OnBeforeSerialize () { }

    public void Reset() {
        Value = InitialValue;
    }

}