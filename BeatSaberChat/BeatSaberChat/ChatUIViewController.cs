using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;
using UnityEngine;

namespace BeatSaberChat
{
    [ViewDefinition("BeatSaberChat.Views.ChatView.bsml")]
    internal class ChatUIViewController : BSMLAutomaticViewController
    {
        public static ChatUIViewController Instance { get; private set; }

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

    }
}
