using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidbodyMonoLink : MonoLink<RigidbodyLink>
{
#if UNITY_EDITOR
	private void OnValidate()
	{
		if (Value.Value == null)
		{
			Value = new RigidbodyLink
			{
				Value = GetComponent<Rigidbody>()
			};
		}
	}
#endif
}