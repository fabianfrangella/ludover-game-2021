using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : MonoBehaviour
{

    private void Update()
    {
        StartCoroutine(nameof(EndCast));
    }

    IEnumerator EndCast()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
