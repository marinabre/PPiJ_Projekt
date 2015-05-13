using System;
using UnityEngine;

public class CameraController : MonoBehaviour 
{
    private const float CameraSpeed = 10.0f;
    private const float ScrollingSpeed = 35.0f;
    [SerializeField]
    private GameObject _earth;

    public GameObject Earth
    {
        set { _earth = value; }
    }

    public void ResetPosition()
    {
        this.transform.position = new Vector3(0.0f, 0.0f, -2.0f);
        this.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 1.0f);
    }

	// Use this for initialization
	private void Start() 
    {
	}
	
	// Update is called once per frame
	private void Update() 
    {
        Zoom();
        MoveVertical();    
	}

    private void Zoom()
    {
        var scrollAxis = Input.GetAxis("Mouse ScrollWheel");
        var distance = Vector3.Distance(_earth.transform.position, this.transform.position);

        if ((distance < 1.5f && scrollAxis >= 0.0f) ||
            (distance > 4.0f && scrollAxis <= 0.0f))
        {
            return;
        }

        this.transform.Translate(new Vector3(0.0f,
                                             0.0f,
                                             scrollAxis * ScrollingSpeed * Time.deltaTime));
    }

    private void MoveVertical()
    {
        var vertAxis = Input.GetAxis("Vertical");
        var r = Vector3.Distance(_earth.transform.position, this.transform.position);
        var y = this.transform.position.y + 
                vertAxis * CameraSpeed * Time.deltaTime;
        var z = (float)Math.Sqrt(Math.Abs((r * r) - (y * y)));

        if ((y * y) < (r * r) * 0.8f)
        {
            this.transform.position = new Vector3(this.transform.position.x,
                                                  y,
                                                  -z);
            this.transform.LookAt(_earth.transform);
        }
    }
}
