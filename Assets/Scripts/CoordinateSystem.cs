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

    public int _numberOfGizmosX;
    public int _numberOfGizmosY;


    public void FixedUpdate() // UPDATE COORDINATES
    {
        CoordinatesDisplay.text = "SECTOR : " + Coordinates();
    }
    public string Coordinates() // Defines coordinates
    {
        string coordinates = null;
        string zcoordinates = "";
        string xcoordinates = "";
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _coordinateVector = _player.position - transform.position; // calculates coordinates
        float[] coorValues = new float[2]; // Array stocking raw coordinates values
        coorValues[0] = Mathf.Abs(_coordinateVector.x); //Absolute x values
        coorValues[1] = Mathf.Abs(_coordinateVector.z); // Absolute z values                                                     
        if (!xSet || !zSet)
        {
           for (int i = 0; i < _alphabet.Length; i++) // Set Coordinates string
            {
                if (coorValues[0] <= (i + 1) * gridSize && coorValues[0] >= i * gridSize)
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

    [ExecuteInEditMode]
    private void OnDrawGizmos()
    {
        for (int i = 0; i <= _numberOfGizmosX; i++)
        {
            for (int j = 0; j <= _numberOfGizmosY; j++)
            {
                DrawRectGizmo(i, j);
            }
        }
    }

    void DrawRectGizmo( int X, int Y)
    {
        Vector3 point1 = transform.position;
        Vector3 point2 = transform.position;
        Vector3 point3 = transform.position;
        Vector3 point4 = transform.position;

        Vector3 Center = transform.position;

        point1 += new Vector3(gridSize / 2 + (X * gridSize) - (.95f * gridSize / 2), 0, gridSize / 2 + (Y * gridSize) - (.95f * gridSize / 2));
        point2 += new Vector3(gridSize / 2 + (X * gridSize) - (.95f * gridSize / 2), 0, gridSize / 2 + (Y * gridSize) + (.95f * gridSize / 2));
        point3 += new Vector3(gridSize / 2 + (X * gridSize) + (.95f * gridSize / 2), 0, gridSize / 2 + (Y * gridSize) - (.95f * gridSize / 2));
        point4 += new Vector3(gridSize / 2 + (X * gridSize) + (.95f * gridSize / 2), 0, gridSize / 2 + (Y * gridSize) + (.95f * gridSize / 2));

        Center += new Vector3(gridSize / 2 + (X * gridSize), 0, gridSize / 2 + (Y * gridSize));

        Debug.DrawLine(point1, point2, Color.yellow);
        Debug.DrawLine(point2, point4, Color.yellow);
        Debug.DrawLine(point3, point4, Color.yellow);
        Debug.DrawLine(point3, point1, Color.yellow);
        
        UnityEditor.Handles.Label(Center, "Cell : " + X + " - " + Y);
    }


    //------------------------------------------------------------------------------------------------------------------------------------\\
    /*foreach (float coordinate in coorValues) // Debug Raw Coordinatess
    {
        coordinatesraw += coordinate + " ";
        Debug.Log(coordinatesraw);
    }*/
    //----------------------------------------------------------------------------------------------------------------------------------\\

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
