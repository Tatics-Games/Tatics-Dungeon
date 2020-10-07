﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrototypeGame
{
    public class FireStorm : CastAlchemical
    {
        public override SkillAbstract AttachSkill(CharacterStats _characterStats, AnimationHandler _animationHandler, TaticalMovement _taticalMovement, CombatUtils _combatUtils, Skill _skill)
        {
            FireStorm fireStorm = _characterStats.gameObject.AddComponent<FireStorm>();
            fireStorm.characterStats = _characterStats;
            fireStorm.animationHandler = _animationHandler;
            fireStorm.taticalMovement = _taticalMovement;
            fireStorm.skill = _skill;
            fireStorm.combatUtils = _combatUtils;

            fireStorm.alchemicalDamage = new CombatStat(_skill.damage, CombatStatType.fireDamage);
            fireStorm.intScaleValue = skill._scaleValue * _characterStats.Intelligence.Value;
            StatModifier intScaling = new StatModifier(fireStorm.intScaleValue, StatModType.Flat);
            fireStorm.alchemicalDamage.AddModifier(intScaling);

            return fireStorm;
        }
        /*
        public static void Activate(CharacterStats characterStats, AnimationHandler animationHandler, TaticalMovement taticalMovement, Skill skill, float delta)
        {
            IntVector2 index = taticalMovement.GetMouseIndex();
            GridManager.Instance.HighlightCastableRange(taticalMovement.currentIndex, index, skill);

            if (index.x >= 0 && characterStats.currentAP >= skill.APcost)
            {
                if (Input.GetMouseButtonDown(0) || InputHandler.instance.tacticsXInput)
                {
                    InputHandler.instance.tacticsXInput = false;

                    animationHandler.PlayTargetAnimation("Attack");
                    characterStats.UseAP(skill.APcost);
                    GridManager.Instance.RemoveAllHighlights();
                    List<GridCell> cells = CastableShapes.GetCastableCells(skill, index);
                    GameObject effect = SpellManager.Instance.BuildSpellPrefab(skill.effectPrefab, GridManager.Instance.GetCellByIndex(index).transform.position);
                    effect.GetComponent<DangerTriangleScript>().Initalize(cells, index);
                }
            }
        }
        */
    }
}