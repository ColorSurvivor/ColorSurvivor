using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource PlayerHurt;
    public AudioSource PlayerDead;
    public AudioSource ExpCollect;
    public AudioSource PlayerLevelUp;
    public AudioSource PlayerAttack;
    public AudioSource MonsterHurt;
    public AudioSource MonsterDead;
    public AudioSource MainMenuBgm;
    public AudioSource bgmSource;
    public AudioSource ClickSound;
    public AudioSource HoverSound;
    public AudioSource ChangingColor;
    public AudioSource SkillUse;
    public AudioSource SkillReady;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayPlayerHurt()
    {
        if (PlayerHurt != null)
        {
            PlayerHurt.Play();
        }
    }

    public void PlayPlayerDead()
    {
        if (PlayerDead != null)
        {
            PlayerDead.Play();
        }
    }

    public void PlayExpCollect()
    {
        if (ExpCollect != null)
        {
            ExpCollect.Play();
        }
    }

    public void PlayPlayerAttack()
    {
        if (PlayerAttack != null)
        {
            PlayerAttack.Play();
        }
    }

    public void PlayPlayerLevelUp()
    {
        if (PlayerLevelUp != null)
        {
            PlayerLevelUp.Play();
        }
    }

    public void PlayMonsterHurt()
    {
        if (MonsterHurt != null)
        {
            MonsterHurt.Play();
        }
    }

    public void PlayMonsterDead()
    {
        if (MonsterDead != null)
        {
            MonsterDead.Play();
        }
    }

    public void PlayBGM()
    {
        if (bgmSource != null && !bgmSource.isPlaying)
        {
            bgmSource.loop = true;
            bgmSource.Play();
        }
    }

    public void StopBGM()
    {
        if (bgmSource != null && bgmSource.isPlaying)
        {
            bgmSource.Stop();
        }
    }

    public void PlayMenuBGM()
    {
        if (MainMenuBgm != null && !MainMenuBgm.isPlaying)
        {
            MainMenuBgm.loop = true;
            MainMenuBgm.Play();
        }
    }

    public void StopMenuBGM()
    {
        if (MainMenuBgm != null && MainMenuBgm.isPlaying)
        {
            MainMenuBgm.Stop();
        }
    }

    public void PlayClickSound()
    {
        if (ClickSound != null)
        {
            ClickSound.Play();
        }
    }

    public void PlayHoverSound()
    {
        if (HoverSound != null)
        {
            HoverSound.Play();
        }
    }

    public void PlayChangingColor()
    {
        if (ChangingColor != null)
        {
            ChangingColor.Play();
        }
    }

    public void PlaySkillUse()
    {
        if (SkillUse != null)
        {
            SkillUse.Play();
        }
    }

    public void PlaySkillReady()
    {
        if (SkillReady != null)
        {
            SkillReady.Play();
        }
    }
}
