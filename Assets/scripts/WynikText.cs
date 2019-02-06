using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class WynikText : MonoBehaviour
{
    Text wynik;

    void OnEnable()
    {
        wynik = GetComponent<Text>();
        wynik.text = "Wynik:" +PlayerPrefs.GetInt("Wynik").ToString();
    }
}
