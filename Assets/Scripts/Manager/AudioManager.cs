using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    private AudioSource _audioSource;
    private AudioClip _bgmClip;
    private AudioClip _attackClip;
    private AudioClip _jumpClip;
    private AudioClip _hurtClip;
    private AudioClip _moveClip;
    private AudioClip _deathClip;
    private AudioClip _chickenHurtClip;
    private AudioClip _portalClip;
    private AudioClip _kitchenClip;
    private AudioClip _restaurantClip;
    private AudioClip _clickClip;

    public float volume;
    public bool isBGMPlaying;
    protected override void Awake()
    {
        base.Awake();
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource = GetComponent<AudioSource>();
        }

        _audioSource.loop = true;
    }

    public void SetAudioClips(AudioClip bgm, AudioClip attack, AudioClip jump, AudioClip hurt, AudioClip move, AudioClip death,
        AudioClip chickenHurt, AudioClip portal, AudioClip kitchen, AudioClip restaurant, AudioClip click)
    {
        _bgmClip = bgm;
        _attackClip = attack;
        _jumpClip = jump;
        _hurtClip = hurt;       
        _moveClip = move;
        _deathClip = death;
        _chickenHurtClip = chickenHurt;
        _portalClip = portal;
        _kitchenClip = kitchen;
        _restaurantClip = restaurant;
        _clickClip = click;
    }

    public void PlayBGM()
    {
        if (_bgmClip == null)
        {
            Debug.Log("BGM is null");
            return;
        }
        if(isBGMPlaying) return;
        _audioSource.clip = _bgmClip;
        _audioSource.Play();
        isBGMPlaying = true;
    }

    public void PlayAttackSoundEffect()
    {
        if (_attackClip == null) return;
        _audioSource.PlayOneShot(_attackClip);
    }

    public void PlayJumpSoundEffect()
    {
        if (_jumpClip == null) return;
        _audioSource.PlayOneShot(_jumpClip);
    }

    public void PlayHurtSoundEffect()
    {
        if (_hurtClip == null) return;
        _audioSource.PlayOneShot(_hurtClip);       
    }
    public void PlayMoveSoundEffect()
    {
        Debug.Log("MoveSoundEffect");
        if (_moveClip == null) return;
        _audioSource.PlayOneShot(_moveClip);
    }

    public void PlayDeathSoundEffect()
    {
        if (_deathClip == null) return;
        _audioSource.PlayOneShot(_deathClip);
    }

    public void PlayChickenHurtSoundEffect()
    {
        if (_chickenHurtClip == null) return;
        _audioSource.PlayOneShot(_chickenHurtClip);
    }

    public void PlayPortalSoundEffect()
    {
        if (_portalClip == null) return;
        _audioSource.PlayOneShot(_portalClip);
    }

    public void PlayKitchenSoundEffect()
    {
        if (_kitchenClip == null) return;
        _audioSource.PlayOneShot(_kitchenClip);
    }

    public void PlayRestaurantSoundEffect()
    {
        if (_restaurantClip == null) return;
        _audioSource.PlayOneShot(_restaurantClip);
    }

    public void PlayClickSoundEffect()
    {
        if (_clickClip == null) return;
        _audioSource.PlayOneShot(_clickClip);
    }

    public void SetVolume(float vol)
    {
        volume = vol;
        _audioSource.volume = volume;
    }
}