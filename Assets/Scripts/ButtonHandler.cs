using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    public Button Earth;
    public Button Cloud;
    public Button Tree;
    public char LastClicked;

    // Start is called before the first frame update
    void Start()
    {
        Earth.onClick.AddListener(LastButtonClicked('E'));
        Cloud.onClick.AddListener(LastButtonClicked('C'));
        Tree.onClick.AddListener(LastButtonClicked('T'));
    }

    void LastButtonClicked(char _buttonName)
    {
        LastClicked = _buttonName;
    }
}
