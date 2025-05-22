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
    public AudioSource bgmSource;

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
}
