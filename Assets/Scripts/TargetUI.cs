using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class TargetUI : MonoBehaviour
{
	public RectTransform labelTr;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		labelTr.LookAt(FindObjectOfType<RigidbodyFirstPersonController>().transform, Vector3.up);

	}
}
