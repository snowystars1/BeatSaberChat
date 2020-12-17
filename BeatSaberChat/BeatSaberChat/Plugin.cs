using IPA;
using IPA.Config;
using IPA.Config.Stores;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using IPALogger = IPA.Logging.Logger;
using BS_Utils.Utilities;

namespace BeatSaberChat
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        internal static Plugin Instance { get; private set; }
        internal static IPALogger Log { get; private set; }

        [Init]
        /// <summary>
        /// Called when the plugin is first loaded by IPA (either when the game starts or when the plugin is enabled if it starts disabled).
        /// [Init] methods that use a Constructor or called before regular methods like InitWithConfig.
        /// Only use [Init] with one Constructor.
        /// </summary>
        public void Init(IPALogger logger)
        {
            Instance = this;
            Log = logger;
            Log.Info("BeatSaberChat initialized.");
            //SceneManager.activeSceneChanged += this.OnActiveSceneChanged;
        }

        #region BSIPA Config
        //Uncomment to use BSIPA's config
        /*
        [Init]
        public void InitWithConfig(Config conf)
        {
            Configuration.PluginConfig.Instance = conf.Generated<Configuration.PluginConfig>();
            Log.Debug("Config loaded");
        }
        */
        #endregion

        [OnStart]
        public void OnApplicationStart()
        {
            //BSEvents.OnLoad();
            BSEvents.lateMenuSceneLoadedFresh += this.OnMenuSceneLoadedFresh;
        }

        [OnExit]
        public void OnApplicationQuit()
        {
            //BSEvents.lateMenuSceneLoadedFresh -= this.OnMenuSceneLoadedFresh;
        }
        //private void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
        //{
        //    if (prevScene.name == "PCInit")
        //    {
        //        Plugin.hasInited = true;
        //    }
        //    if (Plugin.hasInited && nextScene.name.Contains("Menu") && prevScene.name == "EmptyTransition")
        //    {
        //        Plugin.hasInited = false;
        //        PersistentSingleton<BSMLParser>.instance.MenuSceneLoaded();
        //        if (this.gameScenesManager == null)
        //        {
        //            this.gameScenesManager = Resources.FindObjectsOfTypeAll<GameScenesManager>().FirstOrDefault<GameScenesManager>();
        //        }
        //        this.gameScenesManager.transitionDidFinishEvent += this.MenuLoadFresh;
        //    }
        //}

        private void OnMenuSceneLoadedFresh(ScenesTransitionSetupDataSO data)
        {
            PersistentSingleton<BeatSaberChatController>.instance.Setup();
        }

        private static bool hasInited;

        private GameScenesManager gameScenesManager;
    }
}
