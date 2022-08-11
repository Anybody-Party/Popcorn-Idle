using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickMonoLink : MonoLink<JoystickLink>
{
#if UNITY_EDITOR
	private void OnValidate()
	{
		//if (Value.Value == null)
		//{
		//	Value = new JoystickLink
		//	{
		//		Value = GetComponent<FloatingJoystick>()
		//	};
		//}
	}
#endif
}
