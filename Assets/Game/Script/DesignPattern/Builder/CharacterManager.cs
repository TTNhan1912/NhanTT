using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] private Button _logCharacter1;
    [SerializeField] private Button _logCharacter2;

    [SerializeField] private CharacterData _characterData;

    private Character _character;

    private void Start()
    {
        // SpawnCharacter(_characterData);

        _logCharacter1.onClick.AddListener(OnClick);

    }

    private void OnClick()
    {
        _character = new CharacterBuilder()
             .SetData(1, _characterData)
             .Build();

        _character.DisplayInfo();
    }

    /* public Character SpawnCharacter(CharacterData data)
     {
         Character character = Instantiate(_characterPrefab);
         character.Init(data);
         return character;
     }

     private void Character2Log()
     {

     }

     private void Character1Log()
     {

     }*/
}
