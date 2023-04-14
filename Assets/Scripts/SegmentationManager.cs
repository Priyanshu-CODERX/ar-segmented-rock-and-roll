using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Niantic.ARDK;
using Niantic.ARDK.AR;
using Niantic.ARDK.Extensions;
using Niantic.ARDK.AR.ARSessionEventArgs;
using Niantic.ARDK.AR.Configuration;
using Niantic.ARDK.AR.Awareness;
using Niantic.ARDK.AR.Awareness.Semantics;
public enum SegmentationType
{
    person,
    foliage,
    ground,
    artificial_ground,
    natural_ground,
    water,
    building,
    grass
}

public class SegmentationManager : MonoBehaviour
{
    public ARSemanticSegmentationManager semanticManager;
    public Material _shaderMaterial;
    public SegmentationType segmentationType;

    private Texture2D _semanticTexture;


    void Start()
    {
        semanticManager.SemanticBufferUpdated += OnSemanticsBufferUpdated;
        ARSessionFactory.SessionInitialized += OnSessionInitialized;
    }

    private void OnSessionInitialized(AnyARSessionInitializedArgs args)
    {
        Resolution resolution = new Resolution();
        resolution.width = Screen.width;
        resolution.height = Screen.height;
        ARSessionFactory.SessionInitialized -= OnSessionInitialized;
    }

    private void OnSemanticsBufferUpdated(ContextAwarenessStreamUpdatedArgs<ISemanticBuffer> args)
    {
        ISemanticBuffer semanticBuffer = args.Sender.AwarenessBuffer;
        int channel = semanticBuffer.GetChannelIndex(segmentationType.ToString().ToLower());

        semanticBuffer.CreateOrUpdateTextureARGB32(
                ref _semanticTexture, channel
            );
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        _shaderMaterial.SetTexture("_SemanticTex", _semanticTexture);

        _shaderMaterial.SetMatrix("_semanticTransform", semanticManager.SemanticBufferProcessor.SamplerTransform);
        Graphics.Blit(source, destination, _shaderMaterial);
    }
}
