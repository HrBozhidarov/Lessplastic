﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Lessplastic.Models
{
    public class Answer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Voters { get; set; }

        public int PollId { get; set; }

        public Poll Poll { get; set; }
    }
}
