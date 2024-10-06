using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Framework;
using UnityEngine;
[CreateAssetMenu(fileName = "CommentAsset", menuName = "Game/CommentAsset")]
public class CommentAssetSO : ScriptableObject
{
     public List<string> Contents;
     public List<Sprite> Avatars; 
     public string PickRandomContent() => Contents.PickRandom();
     public Sprite PickRandomAvatar() => Avatars.PickRandom();

     #region editor

     //public string comment;


     //[ButtonMethod]
     //public void Cut()
     //{
     //    var cuts = comment.Split(";");
     //    Contents.AddRange(cuts);
     //}

     #endregion



}

public enum ECommentType
{
    None,
    Donate
}



[System.Serializable] 
public class CommentContent
{
    public string Content;

    public CommentContent(string content)
    {
        Content = content;
    }
}
