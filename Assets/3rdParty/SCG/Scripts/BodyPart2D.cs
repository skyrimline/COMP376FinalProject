using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
[System.Serializable]
public class BodyPart2D : MonoBehaviour {

    public enum Scaler {
        Renderer,
        Transform
    }
    public enum Type {
        Leg,
        Arm,
        Center,
        Fat,
        Face
    }
    public enum PartSide {
        Right = -1, Left = 1, Center = 0
    }

    public enum ProportionRelation {
        WidthAndHeight = 2,
        WidthOnly = 1,
        None = 0
    }

    [HideInInspector] public PartSide side;

    public virtual void SetSide (int i) {

        transform.localScale = new Vector3 (i, transform.localScale.y, transform.localScale.z);

    }

    [HideInInspector] public Type type;
    [HideInInspector] public Scaler scaleBy;
    [HideInInspector] public ProportionRelation relyOnParent = ProportionRelation.WidthAndHeight;
    [HideInInspector] public Sleeve2D sleeve;

    [HideInInspector] public BodyPart2D buddy;

    [HideInInspector][SerializeField] protected float width = 1;

    public float Width {
        get { return width; }
    }

    public void SetWidth (float width) {

        this.width = Mathf.Clamp (width, GetMinWidth (), GetMaxWidth ());
        Validate ();

    }

    [HideInInspector][SerializeField] protected float height = 1;

    public float Height {
        get { return height; }
    }
    public void SetHeight (float height) {
        this.height = Mathf.Clamp (height, GetMinHeight (), GetMaxHeight ());
        Validate ();

    }

    [HideInInspector] public float minWidthScale = 0;

    [HideInInspector] public float maxWidthScale = 1f;

    [HideInInspector] public float minHeightScale = 0;

    [HideInInspector] public float maxHeightScale = 1f;

    [HideInInspector] public float circleProportion = 1f;
    protected float jumpOffset = 0.01f;
    protected Vector2 controlOffset;

    [HideInInspector] public bool IK = false;

    [HideInInspector] public float minRotation = -360f;

    [HideInInspector] public float maxRotation = 360f;

    [HideInInspector] public BodyPart2D circleColorRef;
    [HideInInspector] public BodyPart2D colorRef;
    [HideInInspector][SerializeField] protected Color color = UnityEngine.Color.white;

    public Color Color {
        get { return color; }
    }

    public void SetColor (Color color) {
        SetColor (color, true);
    }

    public void SetColor (Color color, bool mirroring) {
        this.color = color;

        RefreshColor ();
        if (mirroring && buddy) buddy.SetColor (color, false);

    }

    [HideInInspector] public string sortingLayerName;
    [HideInInspector] public int sortingOrder;

    [HideInInspector] public SpriteRenderer renderer;

    [HideInInspector][SerializeField] protected Vector2 pivot = new Vector2 (0, 1f);

    public Vector2 Pivot {
        get { return pivot; }
    }

    public void SetPivot (Vector2 pivot) {
        this.pivot = pivot;
        RefreshRenderer ();
    }

    [HideInInspector] public SpriteRenderer ball;

    [HideInInspector][SerializeField] protected Vector2 ballPivot = new Vector2 (0, 0);

    public Vector2 BallPivot {
        get { return ballPivot; }
    }

    public void SetBallPivot (Vector2 pivot) {
        this.ballPivot = pivot;
        RefreshRenderer ();
    }

    [HideInInspector] public BodyPart2D childPart;
    [HideInInspector] public BodyPart2D parentPart;

    protected Vector3 originLocalPos;

    protected float groundLevel;
    [HideInInspector] public Transform floorTransform;

    Vector2 socketOffset;

    [HideInInspector] public CharacterBody2D body;

    [HideInInspector] public BodyPartHandle2D handle;

    public Vector2 widthScale () {
        return new Vector2 (minWidthScale, maxWidthScale);
    }

    public Vector2 heightScale () {
        return new Vector2 (minHeightScale, maxHeightScale);
    }

    public Vector2 rotationLimit () {
        return new Vector2 (minRotation, maxRotation);

    }

    [HideInInspector] public Skirt2D mySkirt;
    [HideInInspector] public Skirt2D myCoat;

    [HideInInspector] public SpriteRenderer wrapper;

    [HideInInspector] public float wrapperHeightScale = 1f;

    [HideInInspector] public bool isPerspectivePart;

    public virtual bool isFoot () {
        return false;
    }

    public void SetControlOffset (Vector2 offset) {
        controlOffset = offset;
    }

    public virtual bool isDirectlyConnected () {
        return true;
    }

    public void SetRendererSprite (Sprite sprite) {
        if (renderer) {
            renderer.sprite = sprite;
            if (wrapper) wrapper.sprite = sprite;
        }
    }

    public Sprite GetRendererSprite () {
        if (renderer) return renderer.sprite;
        else return null;

    }

    public void SetBallSprite (Sprite sprite) {
        if (ball) ball.sprite = sprite;
    }

    public Sprite GetBallSprite () {
        if (ball) return ball.sprite;
        else return null;

    }

    public void Load (BodyPartData part) {
        Load (part, true);
    }

    public virtual void Load (BodyPartData part, bool mirroing) {
        width = part.width;
        height = part.height;
        SetRendererSprite (SCGSaveSystem.LoadSprite (part.rendererSprite));
        SetBallSprite (SCGSaveSystem.LoadSprite (part.ballSprite));

        if (buddy && mirroing) buddy.Load (part, false);

        SetThisDirty ();

    }

    void Awake () {

#if UNITY_EDITOR
        Undo.undoRedoPerformed += RefreshCircle;
#endif

        Setup ();
        Validate ();

    }

    public BodyPart2D[] GetRenderers () {
        BodyPart2D[] renderers = new BodyPart2D[] { this };

        if (buddy) renderers = new BodyPart2D[] { this, buddy };
        return renderers;
    }

    protected void Setup () {

        if (SCGCore.isEditor ()) {

            if (transform && transform.parent) {
                parentPart = transform.parent.GetComponent<BodyPart2D> ();
            }
            FetchChild ();

            sleeve = GetComponent<Sleeve2D> ();
            FetchSleeve ();
        }
    }

    protected void FetchSleeve () {

        if (sleeve && childPart) {
            childPart.sleeve = sleeve;
            childPart.FetchSleeve ();
        }

    }

    public void SetFloor (Transform floor) {

        floorTransform = floor;
        GetGroundLevel ();

    }

    protected virtual void FetchChild () {
        for (int i = 0; i <= transform.childCount - 1; i++) {
            childPart = transform.GetChild (i).GetComponent<BodyPart2D> ();
            if (childPart) break;
        }
        if (childPart) childPart.SetFloor (floorTransform);
    }

    public virtual void ValidatePerspective () {

        RefreshRenderer ();
        RefreshCircle ();
        RefreshWrapper ();
        ControlSelf ();

        if (buddy && buddy.isPerspectivePart) buddy.ValidatePerspective ();

        if (childPart) childPart.ValidatePerspective ();
    }

    public virtual void Validate () {

        Validate (true);

    }

    public virtual void ValidateUndo () {

        Validate (true, true, false);

    }

    protected void KeepUnderParent () {

        if (parentPart && (!parentPart.isDirectlyConnected () && type != Type.Center) && type != Type.Fat) {

            if (type == Type.Leg) {

                if (Mathf.Abs (transform.position.y - parentPart.transform.position.y) > 0) {
                    socketOffset = new Vector3 ((int) side * controlOffset.x, controlOffset.y, transform.localPosition.z);

                } else {
                    socketOffset = new Vector3 ((int) side * controlOffset.x, transform.localPosition.y, transform.localPosition.z);

                }
            } else {

                socketOffset = new Vector3 ((int) side * controlOffset.x, controlOffset.y, transform.localPosition.z);

            }

            if (parentPart.body) {

                if (parentPart.body.GetPerspectiveFactorForDualParts () > 0) {

                    transform.localPosition = new Vector3 (socketOffset.x, socketOffset.y, transform.localPosition.z);
                } else {

                    transform.localPosition = new Vector3 (-socketOffset.x, socketOffset.y, transform.localPosition.z);
                }

            } else {

                transform.localPosition = new Vector3 (socketOffset.x, socketOffset.y, transform.localPosition.z);
            }

        }
    }

    public virtual void ReceiveSkirt (Skirt2D sprite) {
        mySkirt = sprite;

    }
    public virtual void ReceiveCoat (Skirt2D sprite) {
        myCoat = sprite;

    }

    protected void Validate (bool mirror) {
        Validate (mirror, true, true);
    }

    protected virtual void Validate (bool mirror, bool child, bool setup) {

        if (this) {

            if (setup) Setup ();
            ValidateRotation ();
            GetGroundLevel ();
            ValidateParentSize ();
            RefreshDisplay ();

            if (setup) ControlSelf ();

            if (mirror && buddy) {
                buddy.Sync (this);
            }

            if (sleeve) sleeve.Validate ();
            if (parentPart && parentPart.wrapper) parentPart.RefreshWrapper ();
            if (childPart && childPart.sleeve) childPart.sleeve.Validate ();
            if (childPart && child) childPart.Validate ();
        }

    }

    public void Sync () {
        if (buddy) { buddy.Sync (this); }
    }

    public virtual void Sync (BodyPart2D origin) {

        if (origin) {
            width = origin.width;
            height = origin.height;

            minHeightScale = origin.minHeightScale;
            maxHeightScale = origin.maxHeightScale;
            minWidthScale = origin.minWidthScale;
            maxWidthScale = origin.maxWidthScale;

            color = origin.color;

            if (type != Type.Fat) {
                pivot = origin.pivot;
                ballPivot = origin.ballPivot;
            }

            if (renderer && origin.renderer) renderer.sprite = origin.renderer.sprite;
            if (ball && origin.ball) ball.sprite = origin.ball.sprite;
            circleProportion = origin.circleProportion;
            relyOnParent = origin.relyOnParent;
            wrapperHeightScale = origin.wrapperHeightScale;

            if (renderer && origin.renderer) renderer.sprite = origin.renderer.sprite;

            if (wrapper && childPart && origin.wrapper) wrapper.color = origin.wrapper.color;

            if (sleeve && origin.sleeve && origin.sleeve.myRenderer) {
                sleeve.SetColor (origin.sleeve.GetColor);
                sleeve.SetHeight (origin.sleeve.Height);
                sleeve.Validate ();
            }

            SetThisDirty (false);
            Validate (false, false, true);
        }

    }

    public virtual void Sort () {
        Sort (sortingLayerName, sortingOrder);
    }

    public virtual int Sort (string layer, int order) {

        sortingLayerName = layer;
        sortingOrder = order;

        SortByOrder (order);

        if (isDirectlyConnected () && childPart && (childPart.isDirectlyConnected () || childPart.type == Type.Arm)) {
            return childPart.Sort (sortingLayerName, sortingOrder + 1);
        } else {
            return sortingOrder + 1;
        }

    }

    public virtual void SortByOrder (int order) {

        ChangeOrder (renderer, order);
        ChangeOrder (ball, order + 1);
        if (wrapper && type == Type.Leg) {
            ChangeOrder (wrapper, order + 1);
        }
        if (sleeve && sleeve.myRenderer) ChangeOrder (sleeve.myRenderer, order + 3);

    }

    protected void ChangeOrder (SpriteRenderer target, int i) {
        if (target) {
            target.sortingLayerName = sortingLayerName;
            target.sortingOrder = i;
        }

    }

    protected virtual void ValidateRotation () {
        if (minRotation > maxRotation) {
            minRotation = maxRotation;
        }
        if (maxRotation < minRotation) {
            maxRotation = minRotation;
        }

    }

    public virtual void GetGroundLevel () {
        if (type == Type.Leg) {

            groundLevel = GetChildHeights ();

            if (parentPart) {
                parentPart.GetGroundLevel ();
            }
        }

    }

    public virtual float GetChildHeights () {

        if (childPart) {
            return GetFloor () + childPart.GetChildHeights ();
        } else {
            return GetFloor ();
        }

    }

    float GetFloor () {

        return height;

    }

    void Update () {
        EditorUpdate ();
    }

    float GetRefSize () {
        if (!isFoot ()) {
            return width;
        } else {
            return height;
        }
    }

    public float GetCircleWidth () {
        if (ball) {
            return ball.size.x;
        }
        return width;
    }

    public float GetMinWidth () {
        float min = -Mathf.Infinity;
        if ((int) relyOnParent > 0 && parentPart && parentPart.isFoot () == isFoot () && parentPart.scaleBy == scaleBy) {
            min = parentPart.GetRefSize () * minWidthScale;
        } else {
            min = 10f * minWidthScale;
        }
        return min;

    }
    public float GetMaxWidth () {

        float max = Mathf.Infinity;
        if ((int) relyOnParent > 0 && parentPart && parentPart.isFoot () == isFoot () && parentPart.scaleBy == scaleBy) {
            max = parentPart.GetRefSize () * maxWidthScale;
        } else {
            max = 10f * maxWidthScale;
        }
        return max;

    }

    public float GetMinHeight () {

        float min = -Mathf.Infinity;
        if ((int) relyOnParent > 1 && parentPart && parentPart.isFoot () == isFoot () && parentPart.isDirectlyConnected () && parentPart.scaleBy == scaleBy) {
            min = parentPart.height * minHeightScale;
        } else {
            min = 10f * minHeightScale;
        }
        return min;

    }
    public float GetMaxHeight () {

        float max = -Mathf.Infinity;
        if ((int) relyOnParent > 1 && parentPart && parentPart.isFoot () == isFoot () && parentPart.isDirectlyConnected () && parentPart.scaleBy == scaleBy) {
            max = parentPart.height * maxHeightScale;
        } else {
            max = 10f * maxHeightScale;
        }
        return max;

    }

    public virtual void ValidateParentSize () {
        if (parentPart && !parentPart.isDirectlyConnected () && parentPart.type == Type.Leg) {
            parentPart.GetComponent<BodyPartSocket2D> ().RefreshGround ();
        }

    }

    public virtual void EditorUpdate () {

        if (SCGCore.isEditor ()) {

            if (IK) {
                Folding ();
            }

            ControlSelf ();
        } else {
            enabled = false;
        }

    }

    public virtual void RefreshColor () {

        if (renderer) {

            if (colorRef) {
                renderer.color = colorRef.color;

            } else {
                renderer.color = color;

            }
        }
        if (ball) {
            if (circleColorRef && (!parentPart || !parentPart.wrapper || parentPart.wrapperHeightScale <= 0)) {

                ball.color = circleColorRef.color;

            } else {

                ball.color = renderer.color;

            }
        }

    }

    protected float GetBallWidth () {
        return GetBallWidth (true);
    }

    protected float GetBallWidth (bool isX) {
        if (!isFoot ()) {

            if (body && side == PartSide.Center && isX) {
                return (width * circleProportion) * body.GetPerspectiveFactor ();

            } else {
                return (width * circleProportion);

            }

        } else if (parentPart) {

            if (body && side == PartSide.Center && isX) {
                return (parentPart.width * circleProportion) * body.GetPerspectiveFactor ();

            } else {
                return (parentPart.width * circleProportion);

            }

        }

        if (body && side == PartSide.Center && isX) {
            return width * body.GetPerspectiveFactor ();

        } else {
            return width;

        }

    }

    protected virtual void RefreshRenderer () {
        if (renderer) {

            if (scaleBy == Scaler.Renderer) {

                renderer.transform.localScale = new Vector3 (1f, 1f, 1f);
                if (type == Type.Fat) {

                    transform.localScale = new Vector3 (1f, 1f, 1f);
                } else {
                    transform.localScale = new Vector3 (Mathf.Clamp (transform.localScale.x, -1, 1f), 1f, 1f);

                }

                float rendererWidth = SCGCore.DebugSize (GetWidth ());
                float rendererHeight = SCGCore.DebugSize (GetHeight ());

                if (!float.IsNaN (rendererWidth) && !float.IsNaN (rendererHeight)) {

                    renderer.size = new Vector2 (rendererWidth, rendererHeight);
                    renderer.transform.localPosition = new Vector3 (-pivot.x * renderer.size.x * 0.5f, -pivot.y * renderer.size.y * 0.5f, renderer.transform.localPosition.z);
                }

            } else if (scaleBy == Scaler.Transform) {

                renderer.transform.localScale = new Vector3 ((width / 10f), height / 10f, 1);
                renderer.transform.localPosition = new Vector3 (-pivot.x * width / 10f * 0.5f, -pivot.y * height / 10f * 0.5f, renderer.transform.localPosition.z);

            }

        }
    }

    protected virtual void RefreshCircle () {
        if (ball) {

            ball.transform.localScale = Vector3.one;
            float ballSize = SCGCore.DebugSize (GetBallWidth (false));
            ball.size = new Vector2 (ballSize, ballSize);

            ball.transform.localPosition = new Vector3 (ballPivot.x, ballPivot.y, ball.transform.localPosition.z);

        }
    }

    public float GetWidth () {
        if (type == Type.Fat && parentPart) {

            if (body && side == PartSide.Center) {

                return Mathf.Clamp (width * parentPart.height / 2f, GetMinWidth (), GetMaxWidth ());

            } else {
                return Mathf.Clamp (width * parentPart.height / 2f, GetMinWidth (), GetMaxWidth ());

            }

        } else {

            if (body && side == PartSide.Center) {
                return (Mathf.Clamp (width, GetMinWidth (), GetMaxWidth ())) * body.GetPerspectiveFactor ();

            } else {
                return (Mathf.Clamp (width, GetMinWidth (), GetMaxWidth ()));

            }

        }

    }

    public void SwitchSide () {
        SetSide (-1 * Mathf.RoundToInt (transform.localScale.x));
    }

    public float GetHeight () {
        if (type == Type.Fat && parentPart && !float.IsNaN (parentPart.width)) {

            if (body && side == PartSide.Center) {

                if (isPerspectivePart) {
                    if (body.Perspective < 10f) return 0;

                    return (height * parentPart.width * 0.5f) * Mathf.Clamp (Mathf.Abs (body.GetPerspectiveFactor ()), 0.75f, 1f) * Mathf.Clamp (body.Perspective / 90f, 0.125f, 1f);

                }

                return (height * parentPart.width * 0.5f) * Mathf.Clamp (Mathf.Abs (body.GetPerspectiveFactor ()), 0.75f, 1f);

            } else {
                return (height * parentPart.width * 0.5f);

            }

        } else {

            return (Mathf.Clamp (height, GetMinHeight (), GetMaxHeight ()));

        }
    }

    protected virtual void RefreshDisplay () {

        RefreshRenderer ();
        RefreshCircle ();
        RefreshWrapper ();
        RefreshColor ();
    }

    void RefreshWrapper () {
        if (wrapper && renderer && childPart) {
            wrapper.color = childPart.color;
            wrapper.transform.localScale = Vector3.one;
            wrapper.size = new Vector2 (SCGCore.DebugSize (width + (0.2f)), SCGCore.DebugSize (Mathf.Clamp (height * wrapperHeightScale, 0.0001f, height)));
            wrapper.transform.localPosition = new Vector3 (-pivot.x * wrapper.size.x * 0.5f, childPart.transform.localPosition.y + (wrapper.size.y / 2f) - 0.1f, wrapper.transform.localPosition.z);
        }
    }
    public virtual void ControlSelf () {

        if (scaleBy == Scaler.Renderer) {
            if (type == Type.Arm || (type == Type.Face)) {
                transform.localScale = new Vector3 (transform.localScale.x, 1f, 1f);

            } else {
                transform.localScale = new Vector3 (1f, 1f, 1f);

            }
        }

        LimitMovement ();
        LimitRotation ();

    }

    protected virtual void LimitRotation () {
        if (SCGCore.isEditor ()) {

            if ((!isDirectlyConnected () && type == Type.Arm) && parentPart) {
                minRotation = -(parentPart.width - Mathf.Clamp (width, -Mathf.Infinity, parentPart.width)) * 40f;
                maxRotation = (parentPart.width - Mathf.Clamp (width, -Mathf.Infinity, parentPart.width)) * 40f;

            } else if ((type == Type.Center) && parentPart && parentPart.type == Type.Leg) {
                minRotation = -(parentPart.width) * 2.5f;
                maxRotation = (parentPart.width) * 2.5f;

            }

            if (myCoat && myCoat.gameObject.activeSelf) {
                LimitRotation (Mathf.Clamp (-myCoat.maxRadius, minRotation, maxRotation), Mathf.Clamp (myCoat.maxRadius, minRotation, maxRotation));

            } else if (mySkirt && mySkirt.gameObject.activeSelf) {

                LimitRotation (Mathf.Clamp (-mySkirt.maxRadius, minRotation, maxRotation), Mathf.Clamp (mySkirt.maxRadius, minRotation, maxRotation));

            } else {
                LimitRotation (minRotation, maxRotation);

            }
        }
    }

    void LimitRotation (float min, float max) {

        LimitRotation (min, max, transform);

    }

    protected void LimitRotation (float min, float max, Transform target) {

#if UNITY_EDITOR
        float currentZ = UnityEditor.TransformUtils.GetInspectorRotation (target).z;

        if (max == min) {
            currentZ = max;

        } else {

            if (currentZ < min) {
                currentZ = min;

            } else if (currentZ > max) {
                currentZ = max;
            }

        }
        if (float.IsNaN (currentZ) == false) {

            UnityEditor.TransformUtils.SetInspectorRotation (target, new Vector3 (0, 0, currentZ));
        }

#else

        float currentZ = target.transform.localEulerAngles.z;
        DebugRotation (ref currentZ);

        if (max == min) {
            currentZ = max;

        } else {

            if (currentZ < min) {
                currentZ = min;

            } else if (currentZ > max) {
                currentZ = max;
            }

        }
        if (float.IsNaN (currentZ) == false) {

            target.transform.localEulerAngles = new Vector3 (0, 0, currentZ);
        }

#endif
    }

    protected void DebugRotation (ref float angle) {
        angle = (angle > 180) ? angle - 360 : angle;
    }

    protected float DebugRotation (float angle) {
        angle = (angle > 180) ? angle - 360 : angle;
        return angle;
    }

    protected virtual void ConnectToParent () {
        if (type == Type.Fat) {

            if (isPerspectivePart) {
                if (body.Perspective <= 90) {

                    originLocalPos = new Vector3 (-((body.GetInnerPerFlankWidth ()) * 0.5f) + (GetHeight () / 2f), 0, 0);
                } else {

                    originLocalPos = buddy.transform.localPosition;
                }

            } else {

                originLocalPos = new Vector3 (0, 0, 0);
            }

        } else {
            if (type != Type.Center) {
                originLocalPos = new Vector3 (-parentPart.pivot.x * (parentPart.width), -parentPart.pivot.y * (parentPart.height));

            } else {
                originLocalPos = new Vector3 (-parentPart.pivot.x * (parentPart.width), (parentPart.height / 2f));

            }

        }

        transform.localPosition = new Vector3 (originLocalPos.x, originLocalPos.y, transform.localPosition.z);
    }

    public float GetLocalHeight () {
        return -pivot.y * (height);
    }

    protected virtual float ActualGround () {

        if (floorTransform == null && parentPart) floorTransform = parentPart.floorTransform;
        if (floorTransform) {

            return ((groundLevel * floorTransform.localScale.y) + floorTransform.transform.position.y);

        }
        return groundLevel;

    }

    public virtual void LimitMovement () {

        if (parentPart && (parentPart.isDirectlyConnected () || type == Type.Center)) {

            ConnectToParent ();

        }

        if (side == PartSide.Center) {
            KeepInCenter ();
        }

        KeepUnderParent ();

    }

    protected virtual void KeepInCenter () {

        if (parentPart && parentPart.isDirectlyConnected () == false && parentPart.type == Type.Arm) {
            transform.localPosition = new Vector3 (1 - Mathf.Abs (parentPart.body.GetPerspectiveFactor ()), transform.localPosition.y, transform.localPosition.z);

        } else if (!isPerspectivePart) {

            transform.localPosition = new Vector3 (0, transform.localPosition.y, transform.localPosition.z);
        }
    }

    void Folding () {

#if UNITY_EDITOR
        if (parentPart && Selection.activeGameObject == gameObject && transform.localPosition != originLocalPos) {
            float angle = Mathf.Rad2Deg * Mathf.Atan2 ((transform.position - parentPart.transform.position).y, (transform.position - parentPart.transform.position).x);
            transform.localEulerAngles = new Vector3 (0, 0, angle * 2f);
            transform.localPosition = new Vector3 (originLocalPos.x, originLocalPos.y, transform.localPosition.z);
            parentPart.transform.localEulerAngles = new Vector3 (0, 0, -angle);
            parentPart.LimitRotation (0, 90f);

        }
#endif
    }

    public void LerpHeight (float scale) {

        float min = GetMinHeight ();
        float max = GetMaxHeight ();

        height = (min + ((max - min) * scale));

        SetThisDirty ();

    }

    public void LerpWidth (float scale) {
        float min = GetMinWidth ();
        float max = GetMaxWidth ();
        width = (min + ((max - min) * scale));
        SetThisDirty ();
    }

    public void UndoThisAndBuddy () {
        Sync ();
        Validate ();
    }

    public virtual void SetThisDirty () {
        SetThisDirty (true);
    }

    public virtual void SetThisDirty (bool mirroring) {

#if UNITY_EDITOR

        if (SCGCore.isEditor () && this) {
            EditorUtility.SetDirty (this);
            if (renderer) EditorUtility.SetDirty (renderer);
            if (ball) EditorUtility.SetDirty (ball);
            if (wrapper) EditorUtility.SetDirty (wrapper);
            Validate (mirroring);

        }

#endif

    }

    public void Straighten () {
        Straighten (true);
    }

    public virtual void Straighten (bool mirroring) {

        StraightenSelf ();
        if (buddy && mirroring) buddy.Straighten (false);
        if (childPart) childPart.Straighten (true);
        Validate ();

    }

    public void StraightenSelf () {
        if (minRotation * maxRotation <= 0) {
            transform.localEulerAngles = Vector3.zero;
        } else {
            transform.localEulerAngles = new Vector3 (0, 0, (minRotation + maxRotation) / 2f);

        }

    }

    public virtual bool isHand () {
        return false;
    }

}