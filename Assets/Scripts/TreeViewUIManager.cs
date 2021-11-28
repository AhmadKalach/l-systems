using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TreeViewUIManager : MonoBehaviour
{
    public TextMeshProUGUI currentGenerationText;

    public void SetCurrentGeneration(LSystem system)
    {
        currentGenerationText.text = "Generation: " + (system.GetGenerationIndex() + 1);
    }
}
