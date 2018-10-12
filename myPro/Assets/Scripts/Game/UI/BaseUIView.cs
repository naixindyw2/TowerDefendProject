using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public abstract class BaseUIView
    {
        private string viewIdentifying;
        public string ViewIdentifying
        {
            get { return viewIdentifying; }
        }

        private bool isVisible = false;
        public bool IsVisible
        {
            get { return this.isVisible; }
        }

        public virtual bool IsAlpha
        {
            get { return false; }
        }
        
        public virtual bool IsUI
        {
            get { return true; }
        }

        //private ResourceHandle resourceHandle = null;
        //private ResourceHandle musicResHandle = null;

        private GameObject gameObject = null;

        private AudioClip backgroundMusic = null;

        protected float backgroundAudioVolum = 0.3f;

        public GameObject GameObject
        {
            get { return this.gameObject; }
        }

        private Dictionary<string, Coroutine> coroutines = null;

        public BaseUIView()
        {
        }

        public abstract string GetPrefabPath();

        public AudioClip GetBackgroundMusic()
        {
            return backgroundMusic;
        }

        public IEnumerator AsyncLoadPrefab()
        {
            //if (this.resourceHandle == null) {
            //    bool done = false;
            //    this.resourceHandle = ResourceManager.Instance.LoadAsync(GetPrefabPath(), (ResourceHandle handle) => {
            //        done = true;
            //    });
            //    while (!done) {
            //        yield return null;
            //    }
            //}
            yield return null;
        }

        public virtual bool Init()
        {
            // load game object
            //if (this.resourceHandle == null) {
            //    this.resourceHandle =
            //        ResourceManager.Instance.Load(GetPrefabPath());
            //}
            //if (this.resourceHandle.ResourceObject == null) {
            //    ALog.Error("load ui prefab({0}) failed: {1}",
            //        GetPrefabPath(), this.resourceHandle.ErrorInfo);
            //    return false;
            //}
            //this.gameObject = GameObject.Instantiate(
            //    (GameObject)this.resourceHandle.ResourceObject);

            //if (!string.IsNullOrEmpty(this.GetBackgroundMusicPath())) {
            //    this.musicResHandle =
            //        ResourceManager.Instance.Load(GetBackgroundMusicPath());
            //    if (this.musicResHandle.ResourceObject == null) {
            //        ALog.Error("load ui background music({0}) failed: {1}",
            //            GetBackgroundMusicPath(), this.musicResHandle.ErrorInfo);
            //        return false;
            //    }
            //    this.backgroundMusic =
            //        (AudioClip)this.musicResHandle.ResourceObject;
            //}

            this.coroutines = new Dictionary<string, Coroutine>();

            return true;
        }

        public virtual void Dispose()
        {
            if (this.coroutines != null) {
                StopAllCoroutines();
                this.coroutines = null;
            }
            if (this.backgroundMusic != null) {
                this.backgroundMusic = null;
            }
            if (this.gameObject != null) {
                GameObject.Destroy(this.gameObject);
                this.gameObject = null;
            }
        }

        public virtual void SetVisible(bool isVisible)
        {
            this.isVisible = isVisible;
            this.GameObject.SetActive(isVisible);
        }

        public GameObject FindChild(string path)
        {
            if (this.gameObject == null) {
                return null;
            }

            Transform child = this.gameObject.transform.Find(path);
            if (child == null) {
                return null;
            }

            return child.gameObject;
        }

        public T FindChild<T>(string path) where T : MonoBehaviour
        {
            GameObject child = FindChild(path);
            if (child == null) {
                return null;
            }

            return child.GetComponent<T>();
        }

        public void StartCoroutine(string coroutineName, IEnumerator routine)
        {
            StopCoroutine(coroutineName);
            //this.coroutines[coroutineName] =
            //    AppMain.Instance.StartCoroutine(routine);
        }

        public void StopCoroutine(string coroutineName)
        {
            Coroutine coroutine = null;
            if (this.coroutines.TryGetValue(
                    coroutineName, out coroutine) == false) {
                return;
            }
         //   AppMain.Instance.StopCoroutine(coroutine);
            this.coroutines.Remove(coroutineName);
        }

        public void StopAllCoroutines()
        {
            Dictionary<string, Coroutine>.Enumerator iter =
                this.coroutines.GetEnumerator();
            while (iter.MoveNext()) {
                Coroutine coroutine = iter.Current.Value;
           //     AppMain.Instance.StopCoroutine(coroutine);
            }
            this.coroutines.Clear();
        }

        public virtual void OnShow()
        {
        }

        public virtual void OnHide()
        {
            StopAllCoroutines();
        }
    }
}