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
    [HotReload("BeatSaberChat.Views.ChatView.bsml")]
    [ViewDefinition("BeatSaberChat.Views.ChatView.bsml")]
    internal class ChatUIViewController : BSMLResourceViewController
    {
        public override string ResourceName
        {
            get
            {
                return "BeatSaberChat.Views.ChatView.bsml";
            }
        }
        public static ChatUIViewController Instance { get; private set; }

        private RectTransform ChatUIRectTransform;

        [UIComponent("messageList")]
        private CustomListTableData messageList;

        [UIValue("customMessages")]
        private List<CustomListTableData.CustomCellInfo> messages = new List<CustomListTableData.CustomCellInfo>();

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
                ChatUIRectTransform.offsetMax = new Vector2(285.7f, 0f);
                ChatUIRectTransform.sizeDelta = new Vector2(ChatUIRectTransform.sizeDelta.x, 86.0f);
                Instance.gameObject.transform.position = new Vector3(Instance.gameObject.transform.position.x, 1.78f, Instance.gameObject.transform.position.z);

            }
        }

        private void InitializeColors()
        {
            backgroundColor = new Color(.558f, .558f, .558f, .2f);
        }

        private void ShowMessages()
        {
            this.messages.Add(new CustomListTableData.CustomCellInfo("Hello", null, null));
            this.messages.Add(new CustomListTableData.CustomCellInfo("Hello", null, null));
            this.messages.Add(new CustomListTableData.CustomCellInfo("Goodbye", null, null));
            this.messageList.tableView.ReloadData();
            this.messageList.tableView.ScrollToCellWithIdx(2, TableViewScroller.ScrollPositionType.Center, false);
        }
    }
}
    