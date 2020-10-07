﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrototypeGame
{
    public class ShieldChargeAnimation : StateMachineBehaviour
    {
        public ShieldCharge shieldCharge;
        public CharacterStateManager stateManager;
        public TaticalMovement taticalMovement;
        int cellIndex;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            stateManager = animator.GetComponent<CharacterStateManager>();
            shieldCharge = animator.GetComponent<ShieldCharge>();
            taticalMovement = animator.GetComponent<TaticalMovement>();
            stateManager.characterAction = CharacterAction.ShieldCharge;
            stateManager.characterState = CharacterState.IsInteracting;
            shieldCharge.GetComponent<Collider>().isTrigger = true;
            cellIndex = 0;
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            shieldCharge.characterRigidBody.velocity = 5f * shieldCharge.targetDirection;
            Vector3 nextPos = shieldCharge.cells[cellIndex].transform.position;
            if (taticalMovement.ReachedPosition(taticalMovement.transform.position, nextPos))
            {
                cellIndex++;
                taticalMovement.PathCellInteractions();
                if (taticalMovement.ReachedPosition(taticalMovement.transform.position, shieldCharge.targetPos) && !shieldCharge.reachedTarget)
                {
                    if (!shieldCharge.targetPathBlocked)
                        shieldCharge.EndCast();
                }
            }
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!shieldCharge.targetPathBlocked)
            {
                stateManager.characterAction = CharacterAction.None;
                stateManager.characterState = CharacterState.Ready;
            }

            shieldCharge.reachedTarget = false;
            shieldCharge.GetComponent<Collider>().isTrigger = false;
        }

        // OnStateMove is called right after Animator.OnAnimatorMove()
        //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that processes and affects root motion
        //}

        // OnStateIK is called right after Animator.OnAnimatorIK()
        //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that sets up animation IK (inverse kinematics)
        //}
    }

}
