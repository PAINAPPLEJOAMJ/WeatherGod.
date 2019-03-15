using UnityEngine;
using System.Collections;

public abstract class FSM_Base : MonoBehaviour
{
	protected abstract string GetMethodNameFromCurrentState();

	public virtual bool Init() {
		return true;
	}

	protected void NextState() {
        System.Reflection.MethodInfo info = GetType().GetMethod(
			GetMethodNameFromCurrentState(), 
			System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance
			);
        
		StartCoroutine((IEnumerator)info.Invoke(this, null));
    }

}

