using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoordinateSystem : MonoBehaviour
{

    Vector3 _coordinateVector;
    Transform _player;
    public Text CoordinatesDisplay;
    public string[] _alphabet; //Array of Letters for Z axis coordinates
    bool xSet = false, zSet = false;
    public int gridSize = 10; //Grid unit


    public void FixedUpdate() // UPDATE COORDINATES
    {
        CoordinatesDisplay.text = "SECTOR : " + Coordinates();
    }
    public string Coordinates() // Defines coordinates
    {
        
        string coordinates = null;
        string zcoordinates = "";
        string xcoordinates = "";
        //string coordinatesraw = null; // DebugRaw Coordinates
        _player = GameObject.FindGameObjectWithTag("Player").transform;

        _coordinateVector = _player.position - transform.position; // calculates coordinates

        float[] coorValues = new float[2]; // Array stocking raw coordinates values
        coorValues[0] = Mathf.Abs(_coordinateVector.x); //Absolute x values
        coorValues[1] = Mathf.Abs(_coordinateVector.z); // Absolute z values
        //------------------------------------------------------------------------------------------------------------------------------------\\
            /*foreach (float coordinate in coorValues) // Debug Raw Coordinatess
            {
                coordinatesraw += coordinate + " ";
                Debug.Log(coordinatesraw);
            }*/
        //----------------------------------------------------------------------------------------------------------------------------------\\
        if (!xSet || !zSet)
        {
            for (int i = 0; i < _alphabet.Length; i++) // Set Coordinates string
            {
                if (coorValues[0] <= (i + 1) * gridSize && coorValues[0] >= i * gridSize) //
                {
                    int coor = i + 1;
                    xcoordinates = CoordinatesSign(_coordinateVector.x) + coor;
                    xSet = true;
                }
                else
                    xSet = false;
                if (coorValues[1] <= (i + 1) * gridSize && coorValues[1] >= i * gridSize)
                {
                    zcoordinates = CoordinatesSign(_coordinateVector.z) + _alphabet[i];
                    zSet = true;
                }
                else
                    zSet = false;

                //if (coorValues[1] <= (i + 1) * gridSize && coorValues[1] >= i * gridSize && i > 26)
                //{
                //    //int coor = i + 1;
                //    int loops = Mathf.RoundToInt(1.0f* i / 26);
                //    coordinates += CoordinatesSign(_coordinateVector.z) + _alphabet[i];
                //    int todisplay = i - 26 * loops;
                //    coordinates += _alphabet[todisplay];
                //    ySet = true;
                //}
                //Debug.Log(coordinates);
            }
        
        }
        coordinates = xcoordinates + zcoordinates;
        return coordinates;
    }

    public string CoordinatesSign(float testedValue) // Test for negative/positive values
    {
        string sign = "";
        if (testedValue >= 0)
            sign = null;
        else
            sign = "-";
        return sign;
    }
}
