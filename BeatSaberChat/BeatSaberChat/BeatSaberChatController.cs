using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Parser;
using HMUI;
using IPA.Utilities;
using BS_Utils.Utilities;

namespace BeatSaberChat
{
    public class BeatSaberChatController : PersistentSingleton<BeatSaberChatController>
    {
        public static BeatSaberChatController Instance { get; private set; }

        private ChatUIViewController chatUIViewController;
        private GameObject leftScreen;

        private void Awake()
        {
            // For this particular MonoBehaviour, we only want one instance to exist at any time, so store a reference to it in a static property
            //   and destroy any that are created while one already exists.
            if (Instance != null)
            {
                GameObject.DestroyImmediate(this);
                return;
            }
            GameObject.DontDestroyOnLoad(this); // Don't destroy this object on scene changes
            Instance = this;
        }

        internal void Setup()
        {
            this.chatUIViewController = BeatSaberUI.CreateViewController<ChatUIViewController>();
            base.StopAllCoroutines();
            base.StartCoroutine(this.PresentView());
        }

        private IEnumerator PresentView()
        {
            yield return new WaitForSeconds(0.2f);
            yield return new WaitWhile(() => BeatSaberUI.MainFlowCoordinator == null);
            this.ShowView(false, false, false);
            Resources.FindObjectsOfTypeAll<MainMenuViewController>().First<MainMenuViewController>().didActivateEvent += this.ShowView;
            yield break;
        }

		internal void ShowView(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
		{
            BeatSaberUI.MainFlowCoordinator.InvokeMethod("SetTopScreenViewController", new object[]
            {
                this.chatUIViewController,
                ViewController.AnimationType.None
            });
            //foreach (GameObject obj in FindObjectsOfType<GameObject>())
            //{
            //    if(obj.name == "LeftScreen")
            //    {
            //        leftScreen = obj;
            //    }
            //}

            chatUIViewController.Setup();
		}

        private void OnDestroy()
        {
            Plugin.Log?.Debug($"{name}: OnDestroy()");
            if (Instance == this)
                Instance = null; // This MonoBehaviour is being destroyed, so set the static instance property to null.

        }
    }
}
