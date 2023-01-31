using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Emily Chavez
public class RecipeMenu : MonoBehaviour
{
    public static bool _recipeMenu = false;
    public GameObject UIrecipeMenu;
   
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (_recipeMenu)
            {
                Resume();
            }
            else
            {
                OpenRecipeMenu();
            }
        }
    }

    public void Resume()
    {
        UIrecipeMenu.SetActive(false);
        Time.timeScale = 1f;
        _recipeMenu = false;
    }

    private void OpenRecipeMenu()
    {
        UIrecipeMenu.SetActive(true);
        Time.timeScale = 0f;
        _recipeMenu = true;
    }

    public void ToggleRecipeMenu()
    {
        if (UIrecipeMenu.activeInHierarchy)
            Resume();
            
        else
           {OpenRecipeMenu();
           }
    }

    
}
