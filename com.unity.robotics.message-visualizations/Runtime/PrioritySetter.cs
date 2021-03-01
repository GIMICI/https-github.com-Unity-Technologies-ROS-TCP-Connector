﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.Robotics.MessageVisualizers
{
    public interface IPriority
    {
        int Priority { get; set; }
    }

    public class PrioritySetter : MonoBehaviour
    {
        [SerializeField]
        int m_Priority;

        private void Awake()
        {
            foreach (IPriority p in GetComponents<IPriority>())
                p.Priority = m_Priority;
        }
    }
}