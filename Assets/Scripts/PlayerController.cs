using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _distanceBetweenSegments = 0.1f;
    [SerializeField] private GameObject _ropeSegmentPrefab;
    [SerializeField] private Transform _parentPos;
    [SerializeField] private Transform _start;
    [SerializeField] private Transform _end;

    private int _segmentCount;
    // Start is called before the first frame update
    void Start()
    {
        //generateRope(_start,_end);
    }

    /// <summary>
    /// Draw rope between two transform points
    /// </summary>
    /// <param name="startPoint"></param>
    /// <param name="endPoint"></param>
    private void generateRope(Transform startPoint, Transform endPoint)
    {
        Vector3 _segmentDirection = (endPoint.position - startPoint.position).normalized;
        float _distance = Vector3.Distance(startPoint.position, endPoint.position);
        _segmentCount = (int)(_distance / _distanceBetweenSegments);

        for (int i = 0; i < _segmentCount; i++)
        {
            GameObject _segment = Instantiate(_ropeSegmentPrefab, startPoint.position + _segmentDirection * _distanceBetweenSegments * i, Quaternion.identity);
            _segment.transform.parent = _parentPos;
            _segment.transform.eulerAngles = new Vector3(0, 0, 90);

            // Setting Wire Color
            //_segment.gameObject.GetComponent<Renderer>().material.color = setWireColor();

            // Setting Rope Features
            if (i == 0)
            {
                GameObject anchor = new GameObject();
                anchor.transform.SetParent(_parentPos);
                anchor.AddComponent<HingeJoint2D>();
                anchor.AddComponent<Rigidbody2D>();

                //Destroy(_segment.GetComponent<HingeJoint2D>());
                //_segment.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

                _segment.GetComponent<HingeJoint2D>().connectedBody = anchor.GetComponent<Rigidbody2D>();
                _segment.GetComponent<HingeJoint2D>().autoConfigureConnectedAnchor = false;
            }
            else
            {
                int p = _parentPos.transform.childCount - 2;
                _segment.GetComponent<HingeJoint2D>().connectedBody = _parentPos.transform.GetChild(p).GetComponent<Rigidbody2D>();
            }

            // This is special case for End Node
            if (i == _segmentCount - 1)
            {
                //_segment.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
            }
        }
    }
}
