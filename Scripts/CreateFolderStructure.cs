using System.Collections;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class CreateFolderStructure : MonoBehaviour
{
    private const string BASE_PATH = "Assets";

    [MenuItem("GameObject/Create Folder Hierarchy")]
    static void CreateFolder()
    {
        var assets = AssetDatabase.FindAssets("CFG_Configuration");
        var currentConfig = AssetDatabase.GUIDToAssetPath(assets.First());
        var config = AssetDatabase.LoadAssetAtPath<PathConfig>(currentConfig);
        
        var rootPath = $"{BASE_PATH}/{config.Root.name}";
        
        var exists = AssetDatabase.IsValidFolder(rootPath);
        
        var path = string.Empty;
        
        if (!exists)
            path = CreateFolderAndGetPath(BASE_PATH, config.Root.name);
        else
        {
            var guid = AssetDatabase.AssetPathToGUID(rootPath);
            path = AssetDatabase.GUIDToAssetPath(guid); 
        }
        
        Create(config.Root, path);
    }

    private static void Create(PathScriptable path, string parentPath)
    {
        foreach (var currentPath in path.Childrens)
        {
            var guid = CreateFolderAndGetPath(parentPath, currentPath.name);

            if (currentPath.Childrens.Count > 0)
                Create(currentPath, guid);
        }
    }

    private static string CreateFolderAndGetPath(string path, string name)
    {
        var guid = AssetDatabase.CreateFolder(path, name);
        return AssetDatabase.GUIDToAssetPath(guid);
    }
}