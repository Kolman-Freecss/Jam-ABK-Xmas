using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Wires : MonoBehaviour
{
    [SerializeField] private SpriteRenderer wireEnd;
    private Vector3 startPosition;
    [SerializeField] private InputActionReference move;
    [SerializeField] private InputActionReference pick;

    private void Start()
    {
        startPosition = transform.parent.position;
    }

    private void OnMouseDrag()
    {
        Vector2 positionValue = move.action.ReadValue<Vector2>();
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(positionValue);
        newPosition.z = 0;

        transform.position = newPosition;

        Vector3 direction = newPosition - startPosition;
        transform.right = direction * transform.lossyScale.x;

        float dist = Vector2.Distance(startPosition, newPosition);
        wireEnd.size = new Vector2(dist, wireEnd.size.y);
    }
}
