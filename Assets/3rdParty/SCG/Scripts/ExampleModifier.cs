using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExampleModifier : MonoBehaviour {
    public CharacterBody2D targetBody;

    public CharacterGenerator2D generator;

    public Slider scaleSlider;
    public Slider sizeSlider;
    public Slider bodyWidthSlider;
    public Slider bodyHeightSlider;
    public Slider faceWidthSlider;
    public Slider faceHeightSlider;
    public Slider fatnessSlider;
    public Slider shoulderSlider;
    public Slider bellySlider;
    public Slider breastsSlider;
    public Slider hipSlider;

    public Slider perspectiveSlider;

    bool overridePerspective;

    void Awake () {
        FetchBodyData ();
    }

    public void FetchBodyData () {
        if (targetBody == null) return;

        scaleSlider.value = targetBody.Scale;
        sizeSlider.value = targetBody.Size;
        bodyWidthSlider.value = targetBody.Width;
        bodyHeightSlider.value = targetBody.Height;
        faceWidthSlider.value = targetBody.FaceWidth;
        faceHeightSlider.value = targetBody.FaceHeight;
        fatnessSlider.value = targetBody.Fatness;
        shoulderSlider.value = targetBody.ShoulderHeight;
        bellySlider.value = targetBody.BellyHeight;
        breastsSlider.value = targetBody.BreastsHeight;
        hipSlider.value = targetBody.HipWidth;

    }

    public void Male () {
        targetBody.SetSex (CharacterBody2D.Sex.Male);
    }

    public void Female () {
        targetBody.SetSex (CharacterBody2D.Sex.Female);
    }

    public void Teen () {
        targetBody.SetAge (CharacterBody2D.Age.Teen);
    }
    public void Adult () {
        targetBody.SetAge (CharacterBody2D.Age.Adult);
    }
    public void MiddleAge () {
        targetBody.SetAge (CharacterBody2D.Age.MiddleAge);
    }
    public void Old () {
        targetBody.SetAge (CharacterBody2D.Age.Old);
    }

    public void SetScale (Slider slider) {
        targetBody.SetScale (slider.value);
    }

    public void SetSize (Slider slider) {
        targetBody.SetSize (slider.value);
    }

    public void SetWidth (Slider slider) {
        targetBody.SetWidth (slider.value);
    }
    public void SetHeight (Slider slider) {
        targetBody.SetHeight (slider.value);
    }
    public void SetFaceWidth (Slider slider) {
        targetBody.SetFaceWidth (slider.value);
    }
    public void SetFaceHeight (Slider slider) {
        targetBody.SetFaceHeight (slider.value);
    }
    public void SetFatness (Slider slider) {
        targetBody.SetFatness (slider.value);
    }
    public void SetShoulderHeight (Slider slider) {
        targetBody.SetShoulderHeight (slider.value);
    }
    public void SetBellyHeight (Slider slider) {
        targetBody.SetBellyHeight (slider.value);
    }
    public void SetBreastsHeight (Slider slider) {
        targetBody.SetBreastsHeight (slider.value);
    }
    public void SetHipWidth (Slider slider) {
        targetBody.SetHipWidth (slider.value);
    }

    public void Random () {
        generator.Generate (targetBody);
        FetchBodyData ();

    }

    public void Save (InputField field) {
        targetBody.Save ("SCG/Profiles/" + field.text);
    }
    public void Load (InputField field) {
        targetBody.Load ("SCG/Profiles/" + field.text);
        FetchBodyData ();
    }

    public void SetHairStyle (Sprite sprite) {
        targetBody.SetHairStyle (sprite);
    }

    public void SetHairColor (Image image) {
        targetBody.SetHairColor (image.color);
    }
    public void SetSkinColor (Image image) {
        targetBody.SetSkinColor (image.color);
    }
    public void SetShirtColor (Image image) {
        targetBody.SetShirtColor (image.color);
    }
    public void SetPantColor (Image image) {
        targetBody.SetPantColor (image.color);
    }

    public void ToggleOverridePerspective () {
        overridePerspective = !overridePerspective;
        perspectiveSlider.gameObject.SetActive (overridePerspective);
        if (!overridePerspective) targetBody.AutoPerspective ();
        else targetBody.SetPerspective (perspectiveSlider.value);
    }

    public void SetPerspective (Slider slider) {
        if (overridePerspective) {
            targetBody.SetPerspective (slider.value);
        }
    }
}