using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


[Serializable]
[PostProcess(typeof(SplitScreenEffect), PostProcessEvent.AfterStack, "Custom/SplitScreen")]
public sealed class SplitScreen : PostProcessEffectSettings
{
    public TextureParameter _Tex = new TextureParameter { value = null };
    public TextureParameter _MaskTex = new TextureParameter { value = null };
}

[Serializable]
[PostProcess(typeof(SplitScreenEffect), PostProcessEvent.AfterStack, "Custom/SplitScreen")]
public sealed class SplitScreenEffect : PostProcessEffectRenderer<SplitScreen>
{

    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Hidden/Custom/SplitScreen"));

        var imageTexture = settings._Tex.value == null
                            ? RuntimeUtilities.transparentTexture
                            : settings._Tex.value;

        sheet.properties.SetTexture("_OtherTex", imageTexture);

        var maskTexture = settings._MaskTex.value == null
                            ? RuntimeUtilities.transparentTexture
                            : settings._MaskTex.value;

        sheet.properties.SetTexture("_MaskTex", maskTexture);

        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}
