using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SmugglerAttacker : MonoBehaviour
{
    private const string BOXTAG = "SmuggleBox";

    private IEnumerable<Defender> potentialTargets;
    private Defender captureTarget;
    private bool smuggleTarget;
    private Vector3 smugglerBoxPosition;

    
    void Start()
    {
        if (!CheckLaneForDefenders())
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
        else
        {
            SelectTarget();
            SetSmugglerPosition();
        }
    }

    private bool CheckLaneForDefenders()
    {
        potentialTargets = FindObjectsOfType<Defender>().Where(defender => defender.transform.position.y == transform.position.y &&
                                                               !defender.Smuggler &&
                                                               !defender.GetComponent<CraftyBotDefender>());
        return potentialTargets.Any();
    }

    private void SelectTarget()
    {
        captureTarget = potentialTargets.ElementAt(Mathf.RoundToInt(UnityEngine.Random.Range(0, potentialTargets.Count())));
    }

    private void SetSmugglerPosition()
    {
        transform.position = captureTarget.transform.position + Vector3.right;
    }

    private void SetBoxPosition()
    {
        foreach (var child in transform)
        {
            var childObject = child as GameObject;
            if (childObject.tag == BOXTAG)
            {
                smugglerBoxPosition = childObject.transform.position;
                return;
            }
        }
    }

    private void Arrive()
    {
        captureTarget.Smuggle(gameObject);
    }

    private void SmuggleTarget()
    {
        SetBoxPosition();
        smuggleTarget = true;
        Destroy(GetComponent<Health>());
    }

    private void DestroyTarget()
    {
        //Destroy(captureTarget.gameObject);
        captureTarget.gameObject.SetActive(false);
    }

    private void Leave()
    {
        //Destroy(gameObject);
        gameObject.SetActive(false);
    }

    void FixedUpdate()
    {
        if(!captureTarget)
        {
            //Leave no box
        }

        if (smuggleTarget && captureTarget)
        {
            captureTarget.transform.localScale = new Vector3(Mathf.Lerp(captureTarget.transform.localScale.x,
                                                                        0.0f, 0.1f),
                                                             Mathf.Lerp(captureTarget.transform.localScale.y,
                                                                        0.0f, 0.1f),
                                                             1);
            captureTarget.transform.position = new Vector3(Mathf.Lerp(captureTarget.transform.position.x,
                                                                      smugglerBoxPosition.x, 0.1f),
                                                             Mathf.Lerp(captureTarget.transform.position.y,
                                                                        smugglerBoxPosition.y, 0.1f),
                                                             1);
        }
    }
}
