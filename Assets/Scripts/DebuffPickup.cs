using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffPickup : MonoBehaviour
{
    private void Start()
    {
        RandomizePosition();
    }

    void RandomizePosition()
    {

        float x = Random.Range(-31.0f, 31.0f);
        float y = Random.Range(-21.0f, 21.0f);

        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
