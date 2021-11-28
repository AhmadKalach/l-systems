using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LSystemsManager : MonoBehaviour
{
    [Header("Input")]
    public TMP_InputField nameText;
    public TMP_InputField ruleFrom;
    public TMP_InputField ruleTo;
    public TextMeshProUGUI rulesNumber;
    public TMP_InputField startString;
    public TMP_InputField angle;
    public TMP_InputField forwardChar;

    [Header("Output")]
    public GameObject buttonPrefab;
    public RectTransform buttonsParent;

    [HideInInspector] public List<Rule> currentRules;
    [HideInInspector] public List<char> forwardChars;

    TreeGenerator treeGenerator;
    int systemId;
    bool addedExamples;

    // Start is called before the first frame update
    void Start()
    {
        systemId = 0;
        treeGenerator = GameObject.FindGameObjectWithTag("TreeGenerator").GetComponent<TreeGenerator>();
        currentRules = new List<Rule>();
        forwardChars = new List<char>();
    }

    void Update()
    {
        if (!addedExamples)
        {
            AddExampleSystems();
            addedExamples = true;
        }
    }

    void AddExampleSystems()
    {
        //Example 1
        Rule ruleE1 = new Rule('F', "F[+F]F[-F]F");
        LSystem example1 = new LSystem(0, "Edge 1", "F", new List<Rule>() { ruleE1 }, 25.7f, new List<char>() { 'F' });
        treeGenerator.AddSystem(example1);
        AddLSystemToUI(example1);

        //Example 2
        Rule ruleE2 = new Rule('F', "F[+F]F[-F][F]");
        LSystem example2 = new LSystem(0, "Edge 2", "F", new List<Rule>() { ruleE2 }, 20f, new List<char>() { 'F' });
        treeGenerator.AddSystem(example2);
        AddLSystemToUI(example2);

        //Example 3
        Rule ruleE3 = new Rule('F', "FF-[-F+F+F]+[+F-F-F]");
        LSystem example3 = new LSystem(0, "Edge 3", "F", new List<Rule>() { ruleE3 }, 22.5f, new List<char>() { 'F' });
        treeGenerator.AddSystem(example3);
        AddLSystemToUI(example3);

        //Example 4
        Rule ruleE41 = new Rule('K', "F[+K]F[-K]+K");
        Rule ruleE42 = new Rule('F', "FF");
        LSystem example4 = new LSystem(0, "Node 4", "K", new List<Rule>() { ruleE41, ruleE42 }, 20f, new List<char>() { 'F' });
        treeGenerator.AddSystem(example4);
        AddLSystemToUI(example4);

        //Example 5
        Rule ruleE51 = new Rule('K', "F[+K][-K]FK");
        Rule ruleE52 = new Rule('F', "FF");
        LSystem example5 = new LSystem(0, "Node 5", "K", new List<Rule>() { ruleE51, ruleE52 }, 25.7f, new List<char>() { 'F' });
        treeGenerator.AddSystem(example5);
        AddLSystemToUI(example5);

        //Example 6
        Rule ruleE61 = new Rule('K', "F-[[K]+K]+F[+FK]-K");
        Rule ruleE62 = new Rule('F', "FF");
        LSystem example6 = new LSystem(0, "Node 6", "K", new List<Rule>() { ruleE61, ruleE62 }, 22.5f, new List<char>() { 'F' });
        treeGenerator.AddSystem(example6);
        AddLSystemToUI(example6);

    }

    // Update is called once per frame
    public void AddRule()
    {
        currentRules.Add(new Rule(ruleFrom.text.ToCharArray()[0], ruleTo.text));
        ruleFrom.text = "";
        ruleTo.text = "";
        rulesNumber.text = "Rules: " + currentRules.Count;
    }

    public void AddForwardChar()
    {
        forwardChars.Add(forwardChar.text.ToCharArray()[0]);
        forwardChar.text = "";
    }

    public void AddLSystem()
    {
        LSystem lSystem = CreateLSystemFromUI();
        treeGenerator.AddSystem(lSystem);
        AddLSystemToUI(lSystem);
    }

    void AddLSystemToUI(LSystem lSystem)
    {
        GameObject newButton = Instantiate(buttonPrefab, buttonsParent);
        SystemButtonBehavior buttonBehavior = newButton.GetComponent<SystemButtonBehavior>();
        buttonBehavior.systemId = systemId;
        buttonBehavior.system = lSystem;
        buttonBehavior.systemText.text = lSystem.GetName();
        rulesNumber.text = "Rules: " + currentRules.Count;
        nameText.text = "";

        systemId++;
    }

    LSystem CreateLSystemFromUI()
    {
        //add to code 
        float angleValue = 0;
        float.TryParse(angle.text, out angleValue);
        LSystem lSystem = new LSystem(systemId, nameText.text, startString.text, currentRules, angleValue, forwardChars);
        startString.text = "";
        angle.text = "";
        currentRules = new List<Rule>();
        forwardChars = new List<char>();

        return lSystem;
    }
}
