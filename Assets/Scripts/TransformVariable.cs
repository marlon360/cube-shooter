using System;
using UnityEngine;

[CreateAssetMenu]
public class TransformVariable : ScriptableObject, ISerializationCallbackReceiver {

    [NonSerialized]
    public Transform Value;
    
    public Transform InitialValue;
    
    public void OnAfterDeserialize () {
        Reset();
    }

    public void OnBeforeSerialize () { }

    public void Reset() {
        Value = InitialValue;
    }

    public void SetValue(Transform transform) {
        this.Value = transform;
    }

}