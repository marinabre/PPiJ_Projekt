using UnityEngine;
using System.Collections;
using System;
using Model;

public class UserPinController : MonoBehaviour 
{
    [SerializeField]
    private GameObject _earth;
    private bool _isPinned = false;

    public Vector3 ActualRotation
    {
        get
        {
            var rotation = this.transform.eulerAngles;
            return new Vector3
                   {
                       x = rotation.x,
                       y = rotation.y - 180,
                       z = rotation.z
                   };
        }
    }

    public GPSLocation GPS
    {
        get
        {
            var earthRot = _earth.GetComponent<EarthController>()
                                 .ActualRotation;
            return GPSLocation.RotationToGPS(this.ActualRotation,
                                             earthRot);
        }
    }

    public GameObject Earth
    {
        set { _earth = value; }
    }

    public void PinToEarth()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray))
        {
            _isPinned = true;
            this.transform.parent = _earth.transform;
        }
    }

    public void UnpinFromEarth()
    {
        _isPinned = false;
    }

    public void SetPin(Vector3 point)
    {
        this.transform.position = point;
        this.transform.LookAt(Vector3.zero);
        this.transform.Rotate(180.0f, 0.0f, 0.0f);
    }

	// Use this for initialization
	private void Start () 
    {
	}
	
	// Update is called once per frame
	private void Update ()
    {
        MoveOverEarth();
	}

    private void MoveOverEarth()
    {
        RaycastHit hit;
        var ray = Camera.main
                        .ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit) &&
            !_isPinned)
        {
            SetPin(hit.point);
        }
    }
}
