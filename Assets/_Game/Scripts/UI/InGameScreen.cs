using Framework;

public class InGameScreen : ScreenBase
{
    public void TestPlaySound()
    {
        AudioManager.Instance.PlaySound(ESound.piano);
    }
}
