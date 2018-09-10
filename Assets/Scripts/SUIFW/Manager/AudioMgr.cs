using SUIFW;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SUIFW {
    public class AudioMgr : DDOLUnitySingleton<AudioMgr> {
        private AudioSource music = null;
        private int max_effects = 10; // 当前允许最大播放10个;

        private AudioClip now_music_clip = null;
        private bool now_music_loop = true;

        // 存放我们每个音效的AudioSource;
        private Queue<AudioSource> effects = new Queue<AudioSource>();

        override protected void Awake() {
            base.Awake();

            this.music = this.gameObject.AddComponent<AudioSource>();

            for (int i = 0; i < max_effects; i++) {
                AudioSource source = this.gameObject.AddComponent<AudioSource>();
                this.effects.Enqueue(source);
            }
        }

        public void play_music(AudioClip clip, bool loop = true) {
            if (this.music == null || clip == null || (this.music.clip && this.music.clip.name == clip.name)) {
                return;
            }

            this.now_music_clip = clip;
            this.now_music_loop = loop;

            this.music.clip = clip;
            this.music.loop = loop;
            this.music.volume = 1.0f;
            this.music.Play();
        }

        public void stop_music() {
            if (this.music == null || this.music.clip == null) {
                return;
            }
            this.music.Stop();
        }

        public AudioSource play_effect(AudioClip clip) {
            AudioSource source = this.effects.Dequeue();

            source.clip = clip;
            source.volume = 1.0f;
            source.Play();

            this.effects.Enqueue(source);
            return source;
        }

        public void enable_music(bool enabled) {
            if (this.music == null || this.music.enabled == enabled) {
                return;
            }

            this.music.enabled = enabled;
            if (enabled) {
                this.music.clip = this.now_music_clip;
                this.music.loop = this.now_music_loop;
                this.music.Play();
            }
        }

        public void enable_effect(bool enabled) {
            AudioSource[] effect_set = this.effects.ToArray();
            for (int i = 0; i < this.effects.Count; i++) {
                if (effect_set[i].enabled == enabled) {
                    continue;
                }

                effect_set[i].enabled = enabled;
                if (enabled) {
                    effect_set[i].clip = null;
                }
            }
        }
    }
}
