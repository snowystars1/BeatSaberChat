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
using UnityEngine.UI;

namespace BeatSaberChat
{
    public class BeatSaberChatController : PersistentSingleton<BeatSaberChatController>
    {
        public static BeatSaberChatController Instance { get; private set; }

        private ChatUIViewController chatUIViewController;
        private FlowCoordinator soloFreePlayCoordinator;
        private HMUI.Screen chosenScreen;

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
            Button button = Resources.FindObjectsOfTypeAll<Button>().First((Button x) => x.name == "SoloButton");
            if (button is null)
            {
                return;
            }

            //Register a listener for the button so that when the player clicks the button, Initialize() will be called.
            button.onClick.AddListener(delegate ()
            {
                this.Initialize();
            });
        }

        private void Initialize()
        {
            if(this.chatUIViewController is null)
            {
                this.chatUIViewController = BeatSaberUI.CreateViewController<ChatUIViewController>();
                base.StopAllCoroutines();
                base.StartCoroutine(this.PresentView());
            }
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
            foreach (FlowCoordinator flow in Resources.FindObjectsOfTypeAll<FlowCoordinator>())
            {
                if (flow.name == "SoloFreePlayFlowCoordinator")
                {
                    soloFreePlayCoordinator = flow;
                }
            }
            if (soloFreePlayCoordinator != null)
            {
                //BeatSaberUI.MainFlowCoordinator.InvokeMethod("SetTopScreenViewController", new object[]
                //{
                //this.chatUIViewController,
                //ViewController.AnimationType.None
                //});

                soloFreePlayCoordinator.InvokeMethod("SetTopScreenViewController", new object[]
                {
                      this.chatUIViewController,
                      ViewController.AnimationType.None
                });

                chatUIViewController.Setup();
            }
        }


            //foreach (GameObject obj in FindObjectsOfType<GameObject>())
            //{
            //    if (obj.name == "RightScreen")
            //    {
            //        chosenScreen = obj.GetComponent<HMUI.Screen>();
            //    }
            //}


            //AddViewToFlowCoordinator(BeatSaberUI.MainFlowCoordinator, this.chatUIViewController, chosenScreen);
            //}

            //internal void AddViewToFlowCoordinator(FlowCoordinator flowCoordinator, ViewController viewController, HMUI.Screen screen)
            //{
            //    if (viewController is null)
            //    {
            //        return;
            //    }
            //    if (!viewController.gameObject.activeSelf)
            //    {
            //        viewController.gameObject.SetActive(true);
            //    }
            //    viewController.__Init(screen, null, null);
            //    viewController.__Activate(true, false);
            //    viewController.transform.localPosition = Vector3.zero;
            //    viewController.canvasGroup.alpha = 0f;
            //}
        private void OnDestroy()
        {
            Plugin.Log?.Debug($"{name}: OnDestroy()");
            if (Instance == this)
                Instance = null; // This MonoBehaviour is being destroyed, so set the static instance property to null.
        }
    }
}
