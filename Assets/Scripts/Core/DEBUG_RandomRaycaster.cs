using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RandomRaycaster : MonoBehaviour
{
    [SerializeField] private float timer = 0.0f;

    private void Update()
    {
        if (timer > 1.0f)
        {
            RaycastHit2D hit;
            hit = Physics2D.Raycast(transform.position, Input.mousePosition);
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject.name);
            }
            timer = 0.0f;
        }
        timer += Time.deltaTime;
    }
}
