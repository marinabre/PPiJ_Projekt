using UnityEngine;
using System.Collections;
using Model;

public class EarthController : MonoBehaviour 
{
    private const float RotationSpeed = 100.0f;

    private CorrectPinController _correctPin;

    public CorrectPinController CorrectPin
    {
        get { return _correctPin; }
    }

    public CorrectPinController CorrectPinPrefab;

    public Vector3 ActualRotation
    {
        get
        {
            return new Vector3
                   {
                       x = this.transform.rotation.eulerAngles.x - 270,
                       y = this.transform.rotation.eulerAngles.y - 180,
                       z = this.transform.rotation.eulerAngles.z
                   };
        }
    }

    public void CreateCorrectPin()
    {
        _correctPin = Instantiate(CorrectPinPrefab);
        _correctPin.Visible(false);
        _correctPin.transform.SetParent(this.transform);
    }

	// Use this for initialization
	private void Start () 
    {
        CreateCorrectPin();
        _correctPin.Visible(false);
	}
	
	// Update is called once per frame
	private void Update () 
    {
	    Rotate();
	}

    private void Rotate()
    {
        var horAxis = Input.GetAxis("Horizontal");
        var rotation = new Vector3
                       {
                           x = 0.0f,
                           y = 0.0f,
                           z = horAxis * RotationSpeed * Time.deltaTime
                       };

        this.transform.Rotate(rotation);
    }
}
