using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using System.Text;
using System.IO;
using Framework;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class LoadingControl : MonoBehaviour
{
    [SerializeField] private float _loadingDuration;
    [SerializeField] private SceneNames _targetScene;
    [SerializeField] private TextMeshProUGUI _loadingText;
    [SerializeField] private Image _loadingFillImage;


    private AsyncOperation async;


    private void Start()
    {
        LoadScene(_targetScene.ToString(), _loadingDuration);
    }


    public void LoadScene(string id, float time)
    {
        StartCoroutine(IELoadScene(id, time));
    }


    private IEnumerator IELoadScene(string id, float time)
    {
        yield return new WaitForSeconds(0.1f);
        async = SceneManager.LoadSceneAsync(id);
        async.allowSceneActivation = false;

        DOVirtual.Int(0, 100, time, (value) => _loadingText.SetText($"Loading {value}%"));
        _loadingFillImage.DOFillAmount(1, time).SetEase(Ease.Linear);

        yield return new WaitUntil(() => async.progress == 0.9f);
        yield return new WaitForSeconds(time);

        async.allowSceneActivation = true;
    }

#if UNITY_EDITOR
    private const string enumName = "SceneNames";
    private const string filePath = "/SceneNames.cs";
    [ButtonMethod]
    public void GenScreenNameInBuildSetting()
    {
        
        var scenes = EditorBuildSettings.scenes;

        var enumContent = new StringBuilder();
        enumContent.AppendLine("public enum " + enumName);
        enumContent.AppendLine("{");

        foreach (var scene in scenes)
        {
            if (scene.enabled)
            {
                var sceneName = Path.GetFileNameWithoutExtension(scene.path);
                enumContent.AppendLine("    " + sceneName + ",");
            }
        }

        enumContent.AppendLine("}");
        File.WriteAllText($"{Application.dataPath}"+ filePath, enumContent.ToString());
        AssetDatabase.Refresh();

        Debug.Log("Scene Names enum generated at: " + $"{Application.dataPath}" + filePath);
    }
    
#endif

}
