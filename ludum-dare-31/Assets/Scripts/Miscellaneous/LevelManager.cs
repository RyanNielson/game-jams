using UnityEngine;

public static class LevelManager
{
    private static Transform[] allObjects;
    
    public static void Reload()
    {
        //Find & destroy all objects in scene
        
        allObjects = GameObject.FindObjectsOfType<Transform>() as Transform[];
        
        foreach (Transform t in allObjects)
        {
            if (t.tag != "BackgroundMusicPlayer")
            {
                GameObject.Destroy(t.gameObject);
            }
        }

        Application.LoadLevelAdditive(Application.loadedLevel);    
    }
}