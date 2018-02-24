using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportingAttacker : MonoBehaviour {

    [Tooltip("Maximum distance attacker can teleport")]
    public float MaxTeleportDistance;

	
	void Start () {
        SetSmugglerPosition();
	}
	
	
	void Update () {
		
	}

    private void SetSmugglerPosition()
    {
        transform.position += Vector3.left * Random.Range(1, MaxTeleportDistance);
    }
}
