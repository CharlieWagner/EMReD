using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoordinateSystem : MonoBehaviour
{

    Vector3 _coordinateVector;
    Transform _player;
    public Text CoordinateDisplay;
    public string[] _alphabet;
    bool xSet = false, zSet = false;
    public int gridSize = 10;


    public void FixedUpdate()
    {
        CoordinateDisplay.text = "SECTOR : " + Coordinates();
    }
    public string Coordinates()
    {
        
        string coordinates = null;
        string coordinatesraw = null;
        _player = GameObject.FindGameObjectWithTag("Player").transform;

        _coordinateVector = _player.position - transform.position;

        float[] coorValues = new float[2];
        coorValues[0] = Mathf.Abs(_coordinateVector.x);
        coorValues[1] = Mathf.Abs(_coordinateVector.z);

        foreach (float coordinate in coorValues)
        {
            coordinatesraw += coordinate + " ";
            Debug.Log(coordinatesraw);
        }
        if (!xSet || !zSet)
        {
            for (int i = 0; i < _alphabet.Length; i++)
            {
                if (coorValues[0] <= (i + 1) * gridSize && coorValues[0] >= i * gridSize)
                {
                    int coor = i + 1;
                    coordinates += CoordinatesSign(_coordinateVector.x) + coor;
                    xSet = true;
                }
                else
                    xSet = false;
                if (coorValues[1] <= (i + 1) * gridSize && coorValues[1] >= i * gridSize)
                {
                    //int coor = i + 1;
                    coordinates += CoordinatesSign(_coordinateVector.z) + _alphabet[i];
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
        return coordinates;
    }

    public string CoordinatesSign(float testedValue)
    {
        string sign = "";
        if (testedValue >= 0)
            sign = null;
        else
            sign = "-";
        return sign;
    }
}
