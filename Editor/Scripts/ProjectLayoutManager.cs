using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace Matthias.Utilities
{
    public class ProjectLayoutManager : EditorWindow
    {
        private static string _projectName = "ProjectName";
        private static bool _generateDevFolders = false;
        private static string _devNames = "";
        
        private const string KeepFilename = ".keep";
        
        [MenuItem("Assets/Project Layout/1. Create Default Folders")]
        private static void SetUpFolders()
        {
            ProjectLayoutManager layoutManager = ScriptableObject.CreateInstance<ProjectLayoutManager>();
            layoutManager.position = new Rect(Screen.width / 2, Screen.height / 2, 400, 150);
            layoutManager.ShowPopup();
        }
        
        [MenuItem("Assets/Project Layout/2. Keep Empty Folders")]
        // Add a "keep" file to any empty folders.
        private static void KeepEmptyFolders()
        {
            KeepFolders("Assets/");
        }
        
        private static void KeepFolders(string path)
        {
            foreach (string folder in Directory.GetDirectories(path))
            {
                KeepFolders(folder);
                if (Directory.GetFiles(folder).Length <= 1 && 
                    Directory.GetDirectories(folder).Length == 0)
                {
                    string filePath = Path.Join(folder, KeepFilename);
                    File.Create(filePath);
                    Debug.Log(".keep file created: " + filePath);
                }
            }
        }
        
        [MenuItem("Assets/Project Layout/3. Clean Kept Folders")]
        // Remove any "keep" files from folders that already have something in them.
        private static void CleanFolders()
        {
            foreach (string filePath in Directory.GetFiles("Assets/", KeepFilename+"*", SearchOption.AllDirectories))
            {
                File.Delete(filePath);
                Debug.Log(".keep file removed: " + filePath);
            }
        }

        private static void CreateAllFolders()
        {
            List<string> folders = new List<string>
            {
                "Animations",
                "Editor",
                "Fonts",
                "Materials",
                "Meshes",
                "Prefabs",
                "Scripts",
                "Scenes",
                "Shaders",
                "Textures"
            };

            foreach (string folder in folders)
            {
                if (!Directory.Exists("Assets/" + folder))  // Existing folders can be dragged into new root hierarchy
                {
                    string path = "Assets/" + _projectName + "/" + folder;
                    Directory.CreateDirectory(path);
                    Debug.Log(path + " folder created.");
                }

                if (_generateDevFolders)
                {
                    foreach (string name in _devNames.Split(", "))
                    {
                        string pathToFolder = "Assets/Developers/" + name;
                        if (!Directory.Exists(pathToFolder))
                        {
                            Directory.CreateDirectory(pathToFolder);
                            Debug.Log(pathToFolder + " folder created.");
                        }
                    }
                }
                
                AssetDatabase.Refresh();
            }
        }

        private void OnGUI()
        {
            // Global GUI Settings
            EditorGUIUtility.labelWidth = 250;
            
            // Header
            EditorGUILayout.LabelField("Insert the Project name used as the root folder");
            
            // Indent
            EditorGUI.indentLevel++;
            
            // Content
            _projectName = EditorGUILayout.TextField("Project Name: ", _projectName);
            _generateDevFolders = EditorGUILayout.Toggle("Generate Dev Folders: ", _generateDevFolders);
            if (_generateDevFolders)
            {
                EditorGUI.indentLevel++;
                _devNames = EditorGUILayout.TextField("Dev Names (Seperated by \", \")", _devNames);
                GUILayout.Space(-20);
                EditorGUI.indentLevel--;
            }
            this.Repaint();
            
            // Buttons
            GUILayout.Space(60);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Generate!"))
            {
                CreateAllFolders();
                this.Close();
            }
            if (GUILayout.Button("Close"))
                this.Close();
            GUILayout.EndHorizontal();
            
            EditorGUI.indentLevel--;
        }
    }
}
#endif
