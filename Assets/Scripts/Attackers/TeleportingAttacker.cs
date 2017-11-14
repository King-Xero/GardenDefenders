using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportingAttacker : MonoBehaviour {

    [Tooltip("Maximum distance attacker can teleport")]
    public float MaxTeleportDistance;

	// Use this for initialization
	void Start () {
        SetSmugglerPosition();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void SetSmugglerPosition()
    {
        transform.position += Vector3.left * Random.Range(1, MaxTeleportDistance);
    }
}
