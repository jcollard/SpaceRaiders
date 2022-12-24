using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnScreenController : MonoBehaviour
{
    public UnityEvent OnDestroyed;
    void Update()
    {
        if (transform.position.y < -7 || transform.position.y > 7)
        {
            Destroy(this.gameObject);
            OnDestroyed.Invoke();
        }
    }
}
