using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


[RequireComponent(typeof(Button))]
public class TextColor : Button
{
    public void Update()
    {
        Button button = GetComponent<Button>();
        Text text = (Text)button.GetComponentInChildren<Text>();

        ColorBlock colorBlock = this.colors;

        if (this.currentSelectionState == SelectionState.Normal)
        {
            text.color = Color.white;
        }
        else if (this.currentSelectionState == SelectionState.Highlighted)
        {
            text.color = Color.blue;
        }
        else if (this.currentSelectionState == SelectionState.Pressed)
        {
            text.color = Color.white;
        }
        else if (this.currentSelectionState == SelectionState.Disabled)
        {
            text.color = Color.white;
        }
        else if (this.currentSelectionState == SelectionState.Selected)
        {
            text.color = Color.blue;
        }
    }
}
