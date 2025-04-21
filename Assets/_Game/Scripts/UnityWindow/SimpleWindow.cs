using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class SimpleWindow : EditorWindow
{
    private List<Sprite> sprites = new List<Sprite>();
    private List<string> prefabPaths = new List<string>(); // Danh sách đường dẫn Prefab
    private Vector2 scrollPosition;

    private const string PrefKey = "SavedPrefabs";

    [MenuItem("Window/Simple Window")]
    public static void ShowWindow()
    {
        GetWindow<SimpleWindow>("Simple Window").LoadSavedPrefabs();
    }

    private void OnGUI()
    {
        GUILayout.Label("Kéo và thả Sprite hoặc Prefab vào đây!", EditorStyles.boldLabel);

        // Khu vực kéo thả
        Rect dropArea = GUILayoutUtility.GetRect(0, 100, GUILayout.ExpandWidth(true));
        GUI.Box(dropArea, "Thả Sprite hoặc Prefab vào đây", EditorStyles.helpBox);

        HandleDragAndDrop(dropArea);

        // Hiển thị danh sách Sprite
        GUILayout.Space(10);
        GUILayout.Label("Danh sách Sprite:", EditorStyles.boldLabel);

        scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Height(200));
        for (int i = 0; i < sprites.Count; i++)
        {
            GUILayout.BeginHorizontal();

            if (sprites[i] != null)
            {
                GUILayout.Label(AssetPreview.GetAssetPreview(sprites[i]), GUILayout.Width(50), GUILayout.Height(50));
                GUILayout.Label(sprites[i].name);
            }

            if (GUILayout.Button("Xóa", GUILayout.Width(50)))
            {
                sprites.RemoveAt(i);
                SavePrefabs(); // Cập nhật lại danh sách đã lưu
            }

            GUILayout.EndHorizontal();
            GUILayout.Space(5);
        }
        GUILayout.EndScrollView();

        GUILayout.Space(10);

        // Nút xóa toàn bộ data
        if (GUILayout.Button("Xóa Data", GUILayout.Height(30)))
        {
            ClearSavedData();
        }
    }

    private void HandleDragAndDrop(Rect dropArea)
    {
        Event evt = Event.current;

        if (dropArea.Contains(evt.mousePosition))
        {
            switch (evt.type)
            {
                case EventType.DragUpdated:
                case EventType.DragPerform:
                    DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

                    if (evt.type == EventType.DragPerform)
                    {
                        DragAndDrop.AcceptDrag();

                        foreach (Object draggedObject in DragAndDrop.objectReferences)
                        {
                            if (draggedObject is Sprite sprite)
                            {
                                sprites.Add(sprite);
                            }
                            else if (draggedObject is GameObject prefab)
                            {
                                string path = AssetDatabase.GetAssetPath(prefab);
                                if (!string.IsNullOrEmpty(path) && !prefabPaths.Contains(path))
                                {
                                    prefabPaths.Add(path);
                                    AddSpritesFromPrefab(prefab);
                                }
                            }
                        }

                        SavePrefabs(); // Lưu lại Prefab khi có thay đổi
                    }
                    Event.current.Use();
                    break;
            }
        }
    }

    private void AddSpritesFromPrefab(GameObject prefab)
    {
        SpriteRenderer[] spriteRenderers = prefab.GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer sr in spriteRenderers)
        {
            if (sr.sprite != null && !sprites.Contains(sr.sprite))
            {
                sprites.Add(sr.sprite);
            }
        }
    }

    private void SavePrefabs()
    {
        EditorPrefs.SetString(PrefKey, string.Join(";", prefabPaths));
    }

    private void LoadSavedPrefabs()
    {
        sprites.Clear();
        prefabPaths = EditorPrefs.GetString(PrefKey, "").Split(';').ToList();

        foreach (string path in prefabPaths)
        {
            if (!string.IsNullOrEmpty(path))
            {
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                if (prefab != null)
                {
                    AddSpritesFromPrefab(prefab);
                }
            }
        }
    }

    private void ClearSavedData()
    {
        prefabPaths.Clear();
        sprites.Clear();
        EditorPrefs.DeleteKey(PrefKey);
    }
}
