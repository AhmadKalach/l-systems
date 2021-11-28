using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SystemButtonBehavior : MonoBehaviour
{
    public TextMeshProUGUI systemText;
    public LSystem system;
    public int systemId;

    public void SetCurrentSystem()
    {
        GameObject.FindGameObjectWithTag("TreeGenerator").GetComponent<TreeGenerator>().SetCurrentSystem(systemId);
    }
}
