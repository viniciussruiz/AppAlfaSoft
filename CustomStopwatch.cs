﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAlfaSoft
{
    public class CustomStopwatch : Stopwatch
    {

        public DateTime? StartAt { get; private set; }
        public DateTime? EndAt { get; private set; }


        public new void Start()
        {
            StartAt = DateTime.Now;

            base.Start();
        }

        public new void Stop()
        {
            EndAt = DateTime.Now;

            base.Stop();
        }

        public new void Reset()
        {
            StartAt = null;
            EndAt = null;

            base.Reset();
        }

        public new void Restart()
        {
            StartAt = DateTime.Now;
            EndAt = null;

            base.Restart();
        }

    }
}
