using UnityEngine;
using System.Collections;
using Model;

public class CorrectPinController : MonoBehaviour {

	// Use this for initialization
	private void Start () 
    {
	}
	
	// Update is called once per frame
	private void Update () 
    {
	}

    public void PinToGpsLocation(GPSLocation gps)
    {
        this.transform.position = GPSLocation.GPSToCartesian(gps);
        this.transform.LookAt(Vector3.zero);
        this.transform.Rotate(180.0f, 0.0f, 0.0f);
        this.transform.SetParent(this.transform);
    }

    public void Visible(bool visible)
    {
        var rend = GetComponent<Renderer>();
        rend.enabled = visible;
    }
}
