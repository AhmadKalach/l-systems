using System.Collections.Generic;
using UnityEngine;

public class TreeGenerator : MonoBehaviour
{
    public List<LSystem> systems;
    public LSystem currentSystem;
    public TreeViewUIManager treeViewUiManager;

    DrawTree treeDrawer;

    void Start()
    {
        treeDrawer = GetComponent<DrawTree>();
        systems = new List<LSystem>();
    }

    void Update()
    {
        GoThroughCurrentSystem();
    }

    public void AddSystem(LSystem system)
    {
        systems.Add(system);
    }

    public void SetCurrentSystem(int id)
    {
        currentSystem = systems[id];
        currentSystem.SetToFirstGeneration();
        treeViewUiManager.SetCurrentGeneration(currentSystem);
        treeDrawer.Draw(currentSystem.GetCurrentGeneration(), currentSystem.GetAngle(), currentSystem.GetForwardChars());
    }

    void GoThroughCurrentSystem()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            GoToPreviousGeneration();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                GoToNextGeneration();
            }
        }
    }

    public void GoToPreviousGeneration()
    {
        string prevGen = currentSystem.GetPreviousGeneration();
        treeViewUiManager.SetCurrentGeneration(currentSystem);
        treeDrawer.Draw(prevGen, currentSystem.GetAngle(), currentSystem.GetForwardChars());
    }

    public void GoToNextGeneration()
    {
        string nextGen = currentSystem.GetNextGeneration();
        treeViewUiManager.SetCurrentGeneration(currentSystem);
        treeDrawer.Draw(nextGen, currentSystem.GetAngle(), currentSystem.GetForwardChars());
    }
}
