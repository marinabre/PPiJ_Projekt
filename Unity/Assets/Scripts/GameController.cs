using UnityEngine;
using System.Collections;
using Model;
using System;

public class GameController : MonoBehaviour 
{
    private Vector3 _earthStartingRotation;
    private bool _enabled = false;
    [SerializeField]
    private EarthController _earth;
    [SerializeField]
    private UserPinController _userPin;
    [SerializeField]
    private CameraController _camera;

    public EarthController Earth
    {
        get { return _earth; }
        set { _earth = value; }
    }

    public UserPinController UserPin 
    { 
        get { return _userPin; }
        set { _userPin = value; } 
    }

    public CameraController Camera
    {
        get { return _camera; }
        set { _camera = value; }
    }

	// Use this for initialization
	private void Start ()
    {
        _earthStartingRotation  = _earth.transform
                                        .rotation
                                        .eulerAngles;
	}
	
	// Update is called once per frame
	private void Update ()
    {
        if (!_enabled) { return; }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            InsertPin();
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            NewImage(15.32f + "|" + -30.2f);
        }
	}

    private void EnableGame()
    {
        _enabled = true;
    }

    // coords is a string in form of: lat + "|" + long
    private void NewImage(string coords)
    {
        ResetGame();
        var temp = coords.Split('|');
        var gps = new GPSLocation();

        try
        {
            gps.Latitude = float.Parse(temp[0]);
            gps.Longitude = float.Parse(temp[1]);
            _earth.CorrectPin
                  .PinToGpsLocation(gps);
        }
        catch (FormatException ex)
        {
            print(ex.ToString());
        }
    }

    private void InsertPin()
    {
        _userPin.PinToEarth();
        var userPinPosition = _userPin.transform
                                      .position;
        var correctPinPosition = _earth.CorrectPin
                                       .transform
                                       .position;
        var result = Vector3.Distance(
                                userPinPosition,
                                correctPinPosition);
        
        Application.ExternalCall("GetResult",
                                 new object[] 
                                 { 
                                     result.ToString() 
                                 });
        _earth.CorrectPin.Visible(true);
    }

    private void ResetGame()
    {
        _camera.ResetPosition();
        _earth.transform.rotation = Quaternion.Euler(_earthStartingRotation);
        _earth.CorrectPin.Visible(false);
        _userPin.UnpinFromEarth();
    }

    private Vector3 CameraStartingPosition()
    {
        return new Vector3(0.0f, 0.0f, -2.0f);
    }
}
