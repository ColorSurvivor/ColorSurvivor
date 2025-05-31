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

    public GameObject mainMenuCanvas;
    private AudioSource lastPlayedBGM = null;

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

    void Update()
    {
        if (mainMenuCanvas == null)
        {
            mainMenuCanvas = GameObject.Find("MainMenuCanvas");
            if (mainMenuCanvas == null)
                return;
        }

        if (mainMenuCanvas.activeInHierarchy)
        {
            if (lastPlayedBGM != MainMenuBgm)
            {
                if (bgmSource.isPlaying)
                    bgmSource.Stop();

                if (!MainMenuBgm.isPlaying)
                {
                    MainMenuBgm.loop = true;
                    MainMenuBgm.Play();
                }

                lastPlayedBGM = MainMenuBgm;
            }
        }
        else
        {
            if (lastPlayedBGM != bgmSource)
            {
                if (MainMenuBgm.isPlaying)
                    MainMenuBgm.Stop();

                if (!bgmSource.isPlaying)
                {
                    bgmSource.loop = true;
                    bgmSource.Play();
                }

                lastPlayedBGM = bgmSource;
            }
        }
    }

    public void ReplayBGM()
    {
        if (bgmSource != null)
        {
            bgmSource.Stop();
            bgmSource.time = 0f;
            bgmSource.Play();
            bgmSource.loop = true;
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
