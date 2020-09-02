﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace PrototypeGame
{
    public class AggressiveState : FSMState
    {
        public CharacterStats characterStats;
        public AnimationHandler animationHandler;
        public CharacterStateManager stateManager;
        public TaticalMovement taticalMovement;
        public bool DestinationReached = false;

        public PlayerManager target;
        Dictionary<SkillType, Skill> skillDict;

        public AggressiveState(CharacterStats stats,
            CharacterStateManager states, TaticalMovement tatical, AnimationHandler animation,
            Dictionary<SkillType, Skill> skills)
        {
            stateID = FSMStateID.Aggressive;
            characterStats = stats;
            stateManager = states;
            taticalMovement = tatical;
            animationHandler = animation;
            skillDict = skills;
        }

        public override void HandleTransitions()
        {
            
        }

        public override void Act(float delta)
        {
            if (target == null)
            {                
                List<(PlayerManager, int)> playersHealthList = new List<(PlayerManager, int)>();
                    
                foreach (PlayerManager player in GameManager.instance.playersDict.Values.ToArray())
                {                    
                    List<IntVector2> targetsPath = NavigationHandler.instance.GetPath(taticalMovement.currentTargetsNavDict,
                        player.taticalMovement.currentIndex, taticalMovement.currentIndex);

                    int distance = taticalMovement.GetRequiredMoves(player.taticalMovement.currentIndex, targetsPath);
                    
                    if (distance <= characterStats.currentAP && distance!=-1)
                    {                        
                        playersHealthList.Add((player, player.characterStats.currentHealth));
                    }
                }

                playersHealthList.Sort((c1, c2) => c1.Item2.CompareTo(c2.Item2));

                //set the target destination
                target = playersHealthList[0].Item1;
                IntVector2 targetIndex = target.taticalMovement.currentIndex;
                List<IntVector2> targetPath = NavigationHandler.instance.GetPath(taticalMovement.currentTargetsNavDict,
                        targetIndex, taticalMovement.currentIndex);
                targetPath.RemoveAt(targetPath.Count - 1);
                taticalMovement.path = targetPath;
                int targetDistance = taticalMovement.GetRequiredMoves(targetIndex, taticalMovement.path);
                taticalMovement.SetTargetDestination(targetPath[targetPath.Count - 1], targetDistance);
            }

            else
            {
                if (taticalMovement.transform.position == taticalMovement.moveLocation)
                    DestinationReached = true;

                if (DestinationReached)
                {
                    if (characterStats.currentAP > 0 && GameManager.instance.gameState == GameState.Ready)
                    {
                        MeleeAttack.Activate(characterStats, animationHandler,
                            taticalMovement, skillDict[SkillType.MeleeAttack], target, 20, delta);
                    }
                }

                else
                {
                    taticalMovement.TraverseToDestination(delta);
                }
            }            
        }
    }
}

