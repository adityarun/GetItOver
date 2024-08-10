using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockToPoint : MonoBehaviour
{
    [SerializeField] private List<GameObject> _allChildListToLock;
    [SerializeField] private Vector3 _lockPoint;
    // Start is called before the first frame update

    private void LateUpdate()
    {
        foreach(GameObject item in _allChildListToLock)
        {
            transform.position = transform.parent.position;
        }
    }
}
