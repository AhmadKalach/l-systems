using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SceneManager : MonoBehaviour
{
    public RectTransform systemCreationMenu;
    public RectTransform treeViewMenu;
    public float transitionMove = 1000f;
    public float transitionSpeed;

    Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    public void SwitchToSystemCreation()
    {
        mainCamera.transform.DOLocalRotate(new Vector3(0, -179, 0), transitionSpeed);
        systemCreationMenu.anchoredPosition = new Vector2(-transitionMove, 0);
        systemCreationMenu.DOAnchorPos(Vector2.zero, transitionSpeed);
        treeViewMenu.anchoredPosition = Vector2.zero;
        treeViewMenu.DOAnchorPos(new Vector2(transitionMove, 0), transitionSpeed);
    }

    public void SwitchToTreeView()
    {
        mainCamera.transform.DOLocalRotate(Vector3.zero, transitionSpeed);
        systemCreationMenu.anchoredPosition = Vector2.zero;
        systemCreationMenu.DOAnchorPos(new Vector2(-transitionMove, 0), transitionSpeed);
        treeViewMenu.anchoredPosition = new Vector2(transitionMove, 0);
        treeViewMenu.DOAnchorPos(Vector2.zero, transitionSpeed);
    }
}
