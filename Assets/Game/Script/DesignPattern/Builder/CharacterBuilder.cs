public class CharacterBuilder : ICharacterBuilder
{
    private Character _character = new Character();

    public ICharacterBuilder SetData(int index, CharacterData characterData)
    {
        _character.Name = characterData.DataCharacter[index].Name;
        _character.Attack = characterData.DataCharacter[index].Attack;
        _character.Hp = characterData.DataCharacter[index].Hp;
        _character.Mp = characterData.DataCharacter[index].Mp;

        return this;
    }

    public Character Build()
    {
        return _character;
    }


}

public interface ICharacterBuilder
{
    public ICharacterBuilder SetData(int index, CharacterData characterData);

    Character Build();

}
