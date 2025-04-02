using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadianView : BaseView
{
    public override void Start()
    {
        // 计算每个弧形的fillAmount
        float fillAmount = 1f / Num - Spacing;
        for (int i = 0; i < Num; i++)
        {
            // 创建新的Image实例
            var newRadian = Instantiate(m_RadianPrefab, transform);
            newRadian.SetActive(true);
            var img = newRadian.GetComponent<Image>();
            // 设置旋转角度，使每个弧形均匀分布在圆环上，并调整初始角度以对齐手部位置
            img.transform.localRotation = Quaternion.Euler(0, 0, i * (360f / Num) + 203);
            // 设置fillAmount
            img.fillAmount = fillAmount;
            radianList.Add(img);
        }
    }

    public override void Update()
    {
        if (isFinish) return;
        float segmentAngle = 360f / Num;

        // 计算手到眼睛的射线方向
        Vector3 handToEyeDirection = (m_Hand.position - Eye.position).normalized;
        // 创建圆盘平面，法线为圆盘的forward方向
        Plane diskPlane = new Plane(-transform.forward, transform.position);
        // 计算手眼射线与圆盘平面的交点
        float enter;
        if (diskPlane.Raycast(new Ray(m_Hand.position, handToEyeDirection), out enter))
        {
            Vector3 intersectionPoint = m_Hand.position + handToEyeDirection * enter;
            // 计算交点到圆盘中心的方向
            Vector3 centerToIntersection = (intersectionPoint - transform.position).normalized;

            // 计算交点到圆盘中心的方向与圆盘右方向（0度位置）之间的夹角
            float angle = Vector3.SignedAngle(transform.up, centerToIntersection, transform.forward);
            if (angle < 0) angle += 360;

            // 计算每个弧形的角度范围
            for (int i = 0; i < Num; i++)
            {
                float startAngle = (i) * segmentAngle;
                float endAngle = (i + 1) * segmentAngle;

                // 判断射线是否在当前弧形的角度范围内
                if (angle >= startAngle && angle < endAngle)
                {
                    // 高亮当前弧形
                    radianList[i].color = Color.yellow;
                    radianList[i].transform.localScale = SelectScale;
                    SelectImg = radianList[i].transform;
                    SelectIndex = i + 1;
                }
                else
                {
                    // 恢复默认颜色
                    radianList[i].color = Color.white;
                    radianList[i].transform.localScale = Vector3.one;
                }
            }
        }
    }
    
    public override void Finish(bool fade)
    {
        base.Finish(fade);
        if (fade)
        {
            GetComponent<CanvasGroup>().alpha = 0.2f;
        }
    }
}