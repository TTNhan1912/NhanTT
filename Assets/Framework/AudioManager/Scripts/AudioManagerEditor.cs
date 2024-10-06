#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Framework
{
    [CustomEditor(typeof(AudioManager))]
    public class AudioManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            GUILayout.Space(20); // Add 20 pixels of space before the button
            EditorGUILayout.LabelField("EDITOR", EditorStyles.boldLabel); 
            AudioManager audioManager = (AudioManager)target;
            EditorGUILayout.HelpBox("Drag audio clips to Music Clip List and Sound Clip List then press reload to generate sound enum!", MessageType.Info);
            if (GUILayout.Button("Reload"))
            {
                GenerateMusicEnum(audioManager.MusicClips);
                GenerateSoundEnum(audioManager.SoundClips);
            }
        }
    
        private void GenerateMusicEnum(List<AudioClip> soundClips)
        {
            string enumName = "EMusic";
            string[] files = Directory.GetFiles(Application.dataPath, "EMusic.cs", SearchOption.AllDirectories);

            string enumContent = $"public enum {enumName}\n{{\n";

            enumContent += "    None,\n";
            foreach (AudioClip clip in soundClips)
            {
                enumContent += $"    {clip.name},\n";
            }

            enumContent += "}\n";

            try
            {
                if (files.Length > 0)
                {
                    // If there are existing ESound.cs files, overwrite the first found file
                    string filePath = files[0];
                    File.WriteAllText(filePath, enumContent);
                    Debug.Log($"{enumName}.cs updated successfully at: {filePath}");
                }
                else
                {
                    // If no existing ESound.cs file found, create a new one
                    string filePath = Path.Combine(Application.dataPath, "Scripts", $"{enumName}.cs");
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                    File.WriteAllText(filePath, enumContent);
                    Debug.Log($"{enumName}.cs created successfully at: {filePath}");
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to generate/update {enumName}.cs: {e}");
                return;
            }

            AssetDatabase.Refresh();
        }
    
        private void GenerateSoundEnum(List<AudioClip> soundClips)
        {
            string enumName = "ESound";
            string[] files = Directory.GetFiles(Application.dataPath, "ESound.cs", SearchOption.AllDirectories);

            string enumContent = $"public enum {enumName}\n{{\n";

            enumContent += "    None,\n";
            foreach (AudioClip clip in soundClips)
            {
                enumContent += $"    {clip.name},\n";
            }

            enumContent += "}\n";

            try
            {
                if (files.Length > 0)
                {
                    // If there are existing ESound.cs files, overwrite the first found file
                    string filePath = files[0];
                    File.WriteAllText(filePath, enumContent);
                    Debug.Log($"{enumName}.cs updated successfully at: {filePath}");
                }
                else
                {
                    // If no existing ESound.cs file found, create a new one
                    string filePath = Path.Combine(Application.dataPath, "Scripts", $"{enumName}.cs");
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                    File.WriteAllText(filePath, enumContent);
                    Debug.Log($"{enumName}.cs created successfully at: {filePath}");
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to generate/update {enumName}.cs: {e}");
                return;
            }

            AssetDatabase.Refresh();
        }
    }
}
#endif