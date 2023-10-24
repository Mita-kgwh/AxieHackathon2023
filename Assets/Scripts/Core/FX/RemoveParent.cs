using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveParent : MonoBehaviour
{
	void OnDestroy()
    {
        if (transform.parent != null)
            Destroy(transform.parent.gameObject);
    }
}
