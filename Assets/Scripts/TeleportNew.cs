/*
 * Copyright (c) Meta Platforms, Inc. and affiliates.
 * All rights reserved.
 *
 * Licensed under the Oculus SDK License Agreement (the "License");
 * you may not use the Oculus SDK except in compliance with the License,
 * which is provided at the time of installation or download, or which
 * otherwise accompanies this software in either electronic or hard copy form.
 *
 * You may obtain a copy of the License at
 *
 * https://developer.oculus.com/licenses/oculussdk/
 *
 * Unless required by applicable law or agreed to in writing, the Oculus SDK
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

namespace Oculus.Interaction.Locomotion
{
    /// <summary>
    /// Moves a player when receiving events from ILocomotionEventBroadcasters.
    /// The movement can be a combination of translations and rotations
    /// and it happens at the very end of the frame (after rendering).
    /// </summary>
    public class PlayerLocomotorNew : MonoBehaviour,
        ILocomotionEventHandler
    {
        [SerializeField]
        private Transform _playerOrigin;

        private Rigidbody _playerRB;

        [SerializeField]
        private Transform _playerHead;
        private Rigidbody _playerHeadRB;

        private Action<LocomotionEvent, Pose> _whenLocomotionEventHandled = delegate { };
        public event Action<LocomotionEvent, Pose> WhenLocomotionEventHandled
        {
            add
            {
                _whenLocomotionEventHandled += value;
            }
            remove
            {
                _whenLocomotionEventHandled -= value;
            }
        }

        protected bool _started;

        private Queue<LocomotionEvent> _deferredEvent = new Queue<LocomotionEvent>();

        protected virtual void Start()
        {
            _playerHeadRB = _playerHead.GetComponent<Rigidbody>();
            _playerRB = _playerOrigin.GetComponent<Rigidbody>();
            this.BeginStart(ref _started);
            this.AssertField(_playerOrigin, nameof(_playerOrigin));
            this.AssertField(_playerHead, nameof(_playerHead));

            this.EndStart(ref _started);
        }

        private void OnEnable()
        {
            if (_started)
            {
                this.RegisterEndOfFrameCallback(MovePlayer);
            }
        }

        private void OnDisable()
        {
            if (_started)
            {
                _deferredEvent.Clear();
                this.UnregisterEndOfFrameCallback();
                this.UnregisterEndOfFrameCallback();
            }
        }

        public void HandleLocomotionEvent(LocomotionEvent locomotionEvent)
        {
            _deferredEvent.Enqueue(locomotionEvent);
        }

        private void MovePlayer()
        {
            while (_deferredEvent.Count > 0)
            {
                LocomotionEvent locomotionEvent = _deferredEvent.Dequeue();
                Pose originalPose = _playerOrigin.GetPose();
                MovePlayer(locomotionEvent.Pose.position, locomotionEvent.Translation);
                RotatePlayer(locomotionEvent.Pose.rotation, locomotionEvent.Rotation);
                Pose delta = PoseUtils.Delta(originalPose, _playerOrigin.GetPose());
                _whenLocomotionEventHandled.Invoke(locomotionEvent, delta);
            }
        }

        private void MovePlayer(Vector3 targetPosition, LocomotionEvent.TranslationType translationMode)
        {
            if (translationMode == LocomotionEvent.TranslationType.None)
            {
                return;
            }
            if (translationMode == LocomotionEvent.TranslationType.Absolute)
            {
                Vector3 positionOffset = _playerOrigin.position - _playerHead.position;
                positionOffset.y = 0f;
                _playerOrigin.position = targetPosition + positionOffset;
                _playerRB.MovePosition(targetPosition + positionOffset);
            }
            else if (translationMode == LocomotionEvent.TranslationType.AbsoluteEyeLevel)
            {
                Vector3 positionOffset = _playerOrigin.position - _playerHead.position;
                // _playerOrigin.position = targetPosition + positionOffset;
                _playerRB.MovePosition(targetPosition + positionOffset);
            }
            else if (translationMode == LocomotionEvent.TranslationType.Relative)
            {
               // _playerOrigin.position = _playerOrigin.position + targetPosition;
                _playerRB.MovePosition(targetPosition);
            }
            else if (translationMode == LocomotionEvent.TranslationType.Velocity)
            {
                //_playerOrigin.position = _playerOrigin.position + targetPosition * Time.deltaTime;
                _playerRB.MovePosition(_playerOrigin.position + targetPosition * Time.deltaTime);
            }
        }

        private void RotatePlayer(Quaternion targetRotation, LocomotionEvent.RotationType rotationMode)
        {
            if (rotationMode == LocomotionEvent.RotationType.None)
            {
                return;
            }
            Vector3 originalHeadPosition = _playerHead.position;
            if (rotationMode == LocomotionEvent.RotationType.Absolute)
            {
                Vector3 headForward = Vector3.ProjectOnPlane(_playerHead.forward, _playerOrigin.up).normalized;
                Quaternion headFlatRotation = Quaternion.LookRotation(headForward, _playerOrigin.up);
                Quaternion rotationOffset = Quaternion.Inverse(_playerRB.rotation) * headFlatRotation;
                //_playerOrigin.rotation = Quaternion.Inverse(rotationOffset) * targetRotation;
                _playerRB.MoveRotation(Quaternion.Inverse(rotationOffset) * targetRotation);
            }
            else if (rotationMode == LocomotionEvent.RotationType.Relative)
            {
                //_playerOrigin.rotation = targetRotation * _playerOrigin.rotation;
                _playerRB.MoveRotation(targetRotation * _playerRB.rotation);
            }
            else if (rotationMode == LocomotionEvent.RotationType.Velocity)
            {
                targetRotation.ToAngleAxis(out float angle, out Vector3 axis);
                angle *= Time.deltaTime;

                //_playerOrigin.rotation = Quaternion.AngleAxis(angle, axis) * _playerOrigin.rotation;
                _playerRB.MoveRotation(_playerRB.rotation* Quaternion.AngleAxis(angle, axis));
            }
           // _playerOrigin.position = _playerOrigin.position + originalHeadPosition - _playerHead.position;
           _playerRB.MovePosition(_playerRB.position + originalHeadPosition - _playerHead.position);
        }

        #region Inject
        public void InjectAllPlayerLocomotor(Transform playerOrigin, Transform playerHead)
        {
            InjectPlayerOrigin(playerOrigin);
            InjectPlayerHead(playerHead);
        }

        public void InjectPlayerOrigin(Transform playerOrigin)
        {
            _playerOrigin = playerOrigin;
        }

        public void InjectPlayerHead(Transform playerHead)
        {
            _playerHead = playerHead;
        }

        #endregion
    }
}

