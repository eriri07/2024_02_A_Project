using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftingUIManager : MonoBehaviour
{
    public static CraftingUIManager Instance { get; private set; }

    [Header("UI References")]
    public GameObject craftingPanel;
    public TextMeshProUGUI buildingNameText;
    public Transform recipeContainer;
    public Button closeButton;
    public GameObject recipeButtonPrefabs;

    private BuildingCrafter currentCrafter;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        craftingPanel.SetActive(false);
    }

    private void RefreshRecipeList()
    {
        foreach (Transform child in recipeContainer)
        {
            Destroy(child.gameObject);
        }

        if (currentCrafter != null && currentCrafter.recipes != null)
        {
            foreach (CraftingRecipe recipe in currentCrafter.recipes)
            {
                GameObject buttonObj = Instantiate(recipeButtonPrefabs, recipeContainer);
                RecipeButton recipeButton = buttonObj.GetComponent<RecipeButton>();
                recipeButton.Setup(recipe, currentCrafter);
            }
        }
    }

    public void ShowUI(BuildingCrafter crafter)
    {
        currentCrafter = crafter;
        craftingPanel.SetActive (true);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if (crafter != null)
        {
            buildingNameText.text = crafter.GetComponent<ConstructibleBuilding>().buildingName;
            RefreshRecipeList();
        }
    }

    public void HideUI()
    {
        craftingPanel.SetActive(false);
        currentCrafter = null;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked; 
    }

    private void Start()
    {
        closeButton.onClick.AddListener(() => HideUI());
    }
}
