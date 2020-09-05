﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrototypeGame
{
    public class Move : SkillAbstract
    {
        public override SkillAbstract AttachSkill(CharacterStats _characterStats, AnimationHandler _animationHandler,
            TaticalMovement _taticalMovement,Skill _skill)
        {
            Move move = _characterStats.gameObject.AddComponent<Move> ();
            move.characterStats = _characterStats;
            move.animationHandler = _animationHandler;
            move.taticalMovement = _taticalMovement;
            move.skill = _skill;
            return move;
        }

        public override void Activate(float delta)
        {
            taticalMovement.ExcuteMovement(delta);
        }

        public override void Excute(float delta, GridCell targetCell)
        {
        }
    }
}