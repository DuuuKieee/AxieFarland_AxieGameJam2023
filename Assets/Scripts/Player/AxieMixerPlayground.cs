using System.Collections;
using System.Collections.Generic;
using AxieCore.AxieMixer;
using AxieMixer.Unity;
using Newtonsoft.Json.Linq;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Game
{
    public class AxieMixerPlayground : MonoBehaviour
    {
        [SerializeField] RectTransform rootTF;
        [SerializeField] SkeletonAnimation charracter;
        [SerializeField] Character stats;
        private SkeletonAnimation skeletonAnimation;


        Axie2dBuilder builder => Mixer.Builder;

        const bool USE_GRAPHIC = true;
        // int accessoryIdx = 1;

        static string[] ACCESSORY_SLOTS = new[]
          {
                "accessory-air",
                "accessory-cheek",
                "accessory-ground",
                "accessory-hip",
                "accessory-neck",
            };

        private void OnEnable()
        {
        }

        private void OnDisable()
        {
        }

        void Start()
        {
            Mixer.Init();
            if (isFetchingGenes) return;
            StartCoroutine(GetAxiesGenes(Random.Range(1, 10000).ToString()));
            // skeletonAnimation = charracter.GetComponent<SkeletonAnimation>();
            //ProcessMixer("", "0x90000000000001000080e020c40c00000001001028a084080001000008404408000003000800440c0000039408a0450600000300300041020000048008004104", USE_GRAPHIC);
        }


        void ProcessMixer(string axieId, string genesStr, bool isGraphic)
        {

            if (string.IsNullOrEmpty(genesStr))
            {
                Debug.LogError($"[{axieId}] genes not found!!!");
                StartCoroutine(GetAxiesGenes(Random.Range(1, 10000).ToString()));
                return;
            }
            float scale = 0.0021f;

            var meta = new Dictionary<string, string>();
            //foreach (var accessorySlot in ACCESSORY_SLOTS)
            //{
            //    meta.Add(accessorySlot, $"{accessorySlot}1{System.Char.ConvertFromUtf32((int)('a') + accessoryIdx - 1)}");
            //}
            var builderResult = builder.BuildSpineFromGene(axieId, genesStr, meta, scale, isGraphic);

            //Test
            charracter.skeletonDataAsset = builderResult.skeletonDataAsset;
            charracter.Initialize(true);
            stats.RandomStats();
            GameController.instance.isChoosen = true;

        }

        void ClearAll()
        {
            var skeletonAnimations = FindObjectsOfType<SkeletonAnimation>();
            foreach (var p in skeletonAnimations)
            {
                Destroy(p.transform.parent.gameObject);
            }
            var skeletonGraphics = FindObjectsOfType<SkeletonGraphic>();
            foreach (var p in skeletonGraphics)
            {
                Destroy(p.transform.gameObject);
            }
        }

        void SpawnSkeletonAnimation(Axie2dBuilderResult builderResult)
        {
            // ClearAll();
            GameObject go = new GameObject("DemoAxie");
            go.transform.localPosition = new Vector3(0f, -2.4f, 0f);
            SkeletonAnimation runtimeSkeletonAnimation = SkeletonAnimation.NewSkeletonAnimationGameObject(builderResult.skeletonDataAsset);
            runtimeSkeletonAnimation.gameObject.layer = LayerMask.NameToLayer("Player");
            runtimeSkeletonAnimation.transform.SetParent(go.transform, false);
            runtimeSkeletonAnimation.transform.localScale = Vector3.one;

            runtimeSkeletonAnimation.gameObject.AddComponent<AutoBlendAnimController>();
            runtimeSkeletonAnimation.state.SetAnimation(0, "action/idle/normal", true);

            if (builderResult.adultCombo.ContainsKey("body") &&
                builderResult.adultCombo["body"].Contains("mystic") &&
                builderResult.adultCombo.TryGetValue("body-class", out var bodyClass) &&
                builderResult.adultCombo.TryGetValue("body-id", out var bodyId))
            {
                runtimeSkeletonAnimation.gameObject.AddComponent<MysticIdController>().Init(bodyClass, bodyId);
            }
            runtimeSkeletonAnimation.skeleton.FindSlot("shadow").Attachment = null;
        }

        void SpawnSkeletonGraphic(Axie2dBuilderResult builderResult)
        {
            ClearAll();

            var skeletonGraphic = SkeletonGraphic.NewSkeletonGraphicGameObject(builderResult.skeletonDataAsset, rootTF, builderResult.sharedGraphicMaterial);
            skeletonGraphic.rectTransform.sizeDelta = new Vector2(1, 1);
            skeletonGraphic.rectTransform.localScale = Vector3.one;
            skeletonGraphic.rectTransform.anchoredPosition = new Vector2(0f, -260f);
            skeletonGraphic.Initialize(true);
            skeletonGraphic.Skeleton.SetSkin("default");
            skeletonGraphic.Skeleton.SetSlotsToSetupPose();

            skeletonGraphic.gameObject.AddComponent<AutoBlendAnimGraphicController>();
            skeletonGraphic.AnimationState.SetAnimation(0, "action/idle/normal", true);

            if (builderResult.adultCombo.ContainsKey("body") &&
             builderResult.adultCombo["body"].Contains("mystic") &&
             builderResult.adultCombo.TryGetValue("body-class", out var bodyClass) &&
             builderResult.adultCombo.TryGetValue("body-id", out var bodyId))
            {
                skeletonGraphic.gameObject.AddComponent<MysticIdGraphicController>().Init(bodyClass, bodyId);
            }
            skeletonAnimation.skeletonDataAsset = builderResult.skeletonDataAsset;
        }

        bool isFetchingGenes = false;
        // public void OnMixButtonClicked()
        // {
        //     if (isFetchingGenes) return;
        //     StartCoroutine(GetAxiesGenes(Random.Range(1, 10000).ToString()));

        // }

        public IEnumerator GetAxiesGenes(string axieId)
        {
            isFetchingGenes = true;
            string searchString = "{ axie (axieId: \"" + axieId + "\") { id, genes, newGenes}}";
            JObject jPayload = new JObject();
            jPayload.Add(new JProperty("query", searchString));

            var wr = new UnityWebRequest("https://graphql-gateway.axieinfinity.com/graphql", "POST");
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jPayload.ToString().ToCharArray());
            wr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            wr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            wr.SetRequestHeader("Content-Type", "application/json");
            wr.timeout = 10;
            yield return wr.SendWebRequest();
            if (wr.error == null)
            {
                var result = wr.downloadHandler != null ? wr.downloadHandler.text : null;
                if (!string.IsNullOrEmpty(result))
                {
                    JObject jResult = JObject.Parse(result);
                    string genesStr = (string)jResult["data"]["axie"]["newGenes"];
                    ProcessMixer(axieId, genesStr, USE_GRAPHIC);
                }
            }
            else
            {
                ProcessMixer("1546", "0x8000000000003000000c050420c000000010004086083040001000808208304000100041060c502000100100840c50200010004184045060001000c18608302", USE_GRAPHIC);
            }
            isFetchingGenes = false;
        }

        // Update is called once per frame
    }
}
