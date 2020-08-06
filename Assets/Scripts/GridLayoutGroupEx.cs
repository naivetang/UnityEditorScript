using UnityEngine;
using UnityEngine.UI;

namespace Naive
{
    /// <summary>
    /// GridLayoutGroup 的扩展，未填充完的行（列），此行（列）中元素居中显示
    /// </summary>
    public class GridLayoutGroupEx : GridLayoutGroup
    {
        /// <summary>
        ///   <para>Called by the layout system.</para>
        /// </summary>
        public override void SetLayoutHorizontal()
        {
            this.SetCellsAlongAxis(0);
        }

        /// <summary>
        ///   <para>Called by the layout system.</para>
        /// </summary>
        public override void SetLayoutVertical()
        {
            this.SetCellsAlongAxis(1);
        }


        private void SetCellsAlongAxis(int axis)
        {
            if (axis == 0)
            {
                for (int index = 0; index < this.rectChildren.Count; ++index)
                {
                    RectTransform rectChild = this.rectChildren[index];
                    this.m_Tracker.Add((Object)this, rectChild, DrivenTransformProperties.Anchors | DrivenTransformProperties.AnchoredPosition | DrivenTransformProperties.SizeDelta);
                    rectChild.anchorMin = Vector2.up;
                    rectChild.anchorMax = Vector2.up;
                    rectChild.sizeDelta = this.cellSize;
                }
            }
            else
            {
                float x = this.rectTransform.rect.size.x;
                float y = this.rectTransform.rect.size.y;


                int num1;
                int num2;

                // 列
                if (this.m_Constraint == GridLayoutGroup.Constraint.FixedColumnCount)
                {
                    num1 = this.m_ConstraintCount;
                    num2 = Mathf.CeilToInt((float)((double)this.rectChildren.Count / (double)num1 - 1.0 / 1000.0));
                }

                // 行
                else if (this.m_Constraint == GridLayoutGroup.Constraint.FixedRowCount)
                {
                    num2 = this.m_ConstraintCount;
                    num1 = Mathf.CeilToInt((float)((double)this.rectChildren.Count / (double)num2 - 1.0 / 1000.0));
                }
                else
                {
                    num1 = (double)this.cellSize.x + (double)this.spacing.x > 0.0 ? Mathf.Max(1, Mathf.FloorToInt((float)(((double)x - (double)this.padding.horizontal + (double)this.spacing.x + 1.0 / 1000.0) / ((double)this.cellSize.x + (double)this.spacing.x)))) : int.MaxValue;
                    num2 = (double)this.cellSize.y + (double)this.spacing.y > 0.0 ? Mathf.Max(1, Mathf.FloorToInt((float)(((double)y - (double)this.padding.vertical + (double)this.spacing.y + 1.0 / 1000.0) / ((double)this.cellSize.y + (double)this.spacing.y)))) : int.MaxValue;
                }

                // 0 => 左方   1 => 右方
                int num3 = (int)this.startCorner % 2;

                // 0 => 上方   1 => 下方
                int num4 = (int)this.startCorner / 2;

                int num5;
                int num6;
                int num7;
                int lastHangNum = 0;
                int lastLieNum = 0;
                if (this.startAxis == GridLayoutGroup.Axis.Horizontal)
                {
                    num5 = num1;
                    num6 = Mathf.Clamp(num1, 1, this.rectChildren.Count);
                    num7 = Mathf.Clamp(num2, 1, Mathf.CeilToInt((float)this.rectChildren.Count / (float)num5));
                    if (num6 != 0)
                        lastHangNum = this.rectChildren.Count % num6 == 0 ? num6 : this.rectChildren.Count % num6;
                    lastLieNum = num7;
                }
                else
                {
                    num5 = num2;
                    num7 = Mathf.Clamp(num2, 1, this.rectChildren.Count);
                    num6 = Mathf.Clamp(num1, 1, Mathf.CeilToInt((float)this.rectChildren.Count / (float)num5));

                    lastHangNum = num6;
                    if (num7 != 0)
                        lastLieNum = this.rectChildren.Count % num7 == 0 ? num7 : this.rectChildren.Count % num7;

                }
                Vector2 vector2_1 = new Vector2((float)((double)num6 * (double)this.cellSize.x + (double)(num6 - 1) * (double)this.spacing.x), (float)((double)num7 * (double)this.cellSize.y + (double)(num7 - 1) * (double)this.spacing.y));
                Vector2 vector2_2 = new Vector2(this.GetStartOffset(0, vector2_1.x), this.GetStartOffset(1, vector2_1.y));

                Vector2 vector3_1 = new Vector2((float)((double)lastHangNum * (double)this.cellSize.x + (double)(lastHangNum - 1) * (double)this.spacing.x), (float)((double)lastLieNum * (double)this.cellSize.y + (double)(lastLieNum - 1) * (double)this.spacing.y));
                Vector2 vector3_2 = new Vector2(this.GetStartOffset(0, vector3_1.x), this.GetStartOffset(1, vector3_1.y));

                for (int index = 0; index < this.rectChildren.Count; ++index)
                {
                    int num8;
                    int num9;
                    int num10 = 0;
                    int num11 = 0;

                    Vector2 v = vector2_2;

                    if (this.startAxis == GridLayoutGroup.Axis.Horizontal)
                    {
                        num8 = index % num5; // 列
                        num9 = index / num5; // 行

                        num10 = num8;
                        num11 = num9;
                        if (num2 - 1 == num9) //最后一行
                        {
                            v = vector3_2;
                            if (num3 == 1)
                                num10 = lastHangNum - 1 - num8;
                        }
                        else if (num3 == 1)
                            num10 = num6 - 1 - num8;

                        if (num4 == 1)
                            num11 = num7 - 1 - num9;
                    }
                    else  //优先列
                    {
                        num8 = index / num5; // 列
                        num9 = index % num5; // 行

                        num10 = num8;
                        num11 = num9;

                        if (num1 - 1 == num8)
                        {
                            v = vector3_2;
                            if (num4 == 1)
                                num11 = lastLieNum - 1 - num9;
                        }
                        else if (num4 == 1)
                            num11 = num7 - 1 - num9;

                        if (num3 == 1)
                            num10 = num6 - 1 - num8;
                    }


                    // 水平
                    this.SetChildAlongAxis(this.rectChildren[index], 0, v.x + (this.cellSize[0] + this.spacing[0]) * (float)num10, this.cellSize[0]);

                    // 垂直
                    this.SetChildAlongAxis(this.rectChildren[index], 1, v.y + (this.cellSize[1] + this.spacing[1]) * (float)num11, this.cellSize[1]);

                }
            }
        }
    }
}
