using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawTree : MonoBehaviour
{
    public GameObject linePrefab;
    public GameObject parentPrefab;
    public GameObject leafPrefab;
    public float startY = -2.1f;

    public Vector2 startPosition;
    public float minCameraSize = 3;

    Vector2 currPosition;
    float currRotation;
    GameObject currParent;
    Stack<Vector2> positionsStack;
    Stack<float> rotationsStack;
    Camera mainCamera;
    float maxX;
    float maxY;
    float minX;
    float minY;

    private void Start()
    {
        currParent = Instantiate(parentPrefab);
        currPosition = startPosition;
        positionsStack = new Stack<Vector2>();
        rotationsStack = new Stack<float>();
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    public void Draw(string input, float angle, List<char> drawChars)
    {
        ResetDrawing();
        for (int i = 0; i < input.Length; i++)
        {
            switch (input.ToCharArray()[i])
            {
                case '+':
                    currRotation += angle;
                    break;
                case '-':
                    currRotation -= angle;
                    break;
                case '[':
                    positionsStack.Push(currPosition);
                    rotationsStack.Push(currRotation);
                    break;
                case ']':
                    GameObject leaf = Instantiate(leafPrefab, currPosition, Quaternion.Euler(0, 0, currRotation), currParent.transform);
                    currPosition = positionsStack.Pop();
                    currRotation = rotationsStack.Pop();
                    break;

                default:
                    foreach (char c in drawChars)
                    {
                        if (input.ToCharArray()[i] == c)
                        {
                            GameObject currLine = Instantiate(linePrefab, currPosition, Quaternion.Euler(0, 0, currRotation), currParent.transform);
                            currPosition = currLine.transform.position + currLine.transform.up * currLine.transform.localScale.y * 2;

                            maxX = Mathf.Max(maxX, currPosition.x);
                            maxY = Mathf.Max(maxY, currPosition.y);
                            minX = Mathf.Min(minX, currPosition.x);
                            minY = Mathf.Min(minY, currPosition.y);
                        }
                    }

                    break;
            }
        }

        //compute diffs
        float yDiff = maxY - minY;
        float xDiff = maxX - minX;

        if (yDiff / 2 > minCameraSize)
        {
            mainCamera.orthographicSize = yDiff / 2 + 1;
            mainCamera.transform.position = new Vector3((maxX + minX) / 2, (maxY + minY) / 2, mainCamera.transform.position.z);
        }
        else
        {
            mainCamera.orthographicSize = minCameraSize;
            mainCamera.transform.position = new Vector3((maxX + minX) / 2, (maxY + minY) / 2, mainCamera.transform.position.z);
        }

    }

    void ResetDrawing()
    {
        Destroy(currParent);
        currParent = Instantiate(parentPrefab);
        currPosition = startPosition;
        currRotation = 0f;
        positionsStack = new Stack<Vector2>();
        rotationsStack = new Stack<float>();
        minX = startPosition.x;
        maxX = startPosition.x;
        minY = startPosition.y;
        maxY = startPosition.y;
    }
}
