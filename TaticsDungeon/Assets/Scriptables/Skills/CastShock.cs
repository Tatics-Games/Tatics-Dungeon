﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrototypeGame
{
    public abstract class CastShock : SkillAbstract
    {
        public float intScaleValue;

        public override void Cast(float delta, IntVector2 targetIndex)
        {
            List<GridCell> cells = CastableShapes.GetCastableCells(skill, targetIndex);

            characterStats.transform.LookAt(cells[0].transform);
            animationHandler.PlayTargetAnimation("SpellCastHand");
            characterStats.UseAP(skill.APcost);

            GridManager.Instance.RemoveAllHighlights();
            GameObject effect = Instantiate(skill.effectPrefab,
                taticalMovement.transform.position + Vector3.up * 1.5f + taticalMovement.transform.forward * 1f,
                Quaternion.identity);
            effect.GetComponent<VFXSpawns>().Initialize(cells, this, taticalMovement.currentIndex);
        }

        public override void Excute(float delta, GridCell targetCell)
        {
            AlchemyManager.Instance.ApplyShock(targetCell);
        }
    }
}