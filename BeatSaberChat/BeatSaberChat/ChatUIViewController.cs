using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;
using BeatSaberMarkupLanguage.Components;
using HMUI;
using UnityEngine;

namespace BeatSaberChat
{
    [HotReload(@"C:\Users\Chris\Documents\GitHub\BeatSaberChat\BeatSaberChat\BeatSaberChat\Views\ChatView.bsml")]
    [ViewDefinition("BeatSaberChat.Views.ChatView.bsml")]
    internal class ChatUIViewController : BSMLAutomaticViewController
    {
        public static ChatUIViewController Instance { get; private set; }

        private RectTransform ChatUIRectTransform;

        [UIComponent("message-list")]
        public CustomCellListTableData messageList;

        [UIValue("custom-messages")]
        public List<object> messages = new List<object>();

        [UIValue("bg-color")]
        public Color backgroundColor;
        //METHODS ------------------------------------------------------------------------------------

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

        public void Setup()
        {
            SetWindowPosition();
            InitializeColors();
            ShowMessages();
        }

        private void SetWindowPosition()
        {
            ChatUIRectTransform = Instance.gameObject.GetComponent<RectTransform>();
            if(ChatUIRectTransform is RectTransform)
            {
                //ChatUIRectTransform.offsetMax = new Vector2(380.9f, 0f);
                ChatUIRectTransform.sizeDelta = new Vector2(ChatUIRectTransform.sizeDelta.x, 86.0f);
                Instance.gameObject.transform.position = new Vector3(Instance.gameObject.transform.position.x, 1.78f, Instance.gameObject.transform.position.z);
                Instance.gameObject.transform.position = new Vector3(Instance.gameObject.transform.position.x, 3.21f, Instance.gameObject.transform.position.z);
            }
        }

        private void InitializeColors()
        {
            backgroundColor = new Color(.558f, .558f, .558f, .2f);
        }

        private void ShowMessages()
        {

            if(this.messages is null)
            {
                Plugin.Log.Info("Messages is null");
                return;
            }
            if(this.messageList is null)
            {
                Plugin.Log.Info("MessageList is null");
                return;
            }
            this.messages.Add(new customMessageObject("hello"));
            this.messages.Add(new customMessageObject("hello"));
            this.messages.Add(new customMessageObject("goodbye1"));
            this.messages.Add(new customMessageObject("goodbye2"));
            this.messages.Add(new customMessageObject("goodbye3"));
            this.messages.Add(new customMessageObject("goodbye4"));
            this.messages.Add(new customMessageObject("goodbye5"));
            this.messageList.tableView.ReloadData();
            //this.messageList.tableView.ScrollToCellWithIdx(2, TableViewScroller.ScrollPositionType.Center, false);
        }
    }

    internal class customMessageObject
    {
        [UIValue("somethingMessage")]
        public string message;

        public customMessageObject(string message)
        {
            this.message = message; 
        }
    }
}
    