﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ServiceHealthStatus.ViewModel.Model

{
    public class ServiceItemBase : IResultPatternHolderModel
    {
        public string ResultPattern { get; set; }
        public string Name { get; set; }
    }
}
