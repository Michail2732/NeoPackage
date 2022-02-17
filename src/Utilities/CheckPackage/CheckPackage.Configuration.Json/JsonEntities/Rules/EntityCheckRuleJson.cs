﻿using CheckPackage.Core.Checks;
using CheckPackage.Core.Conditions;
using Newtonsoft.Json;
using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Configuration.Json.Entities
{
    public class EntityCheckRuleJson
    {
        [JsonProperty("state")]
        [JsonRequired]
        public Critical State { get; set; }
        [JsonProperty("conditions")]
        [JsonRequired]
        public List<EntityConditionJson> Conditions { get; set; }
        [JsonProperty("checks")]
        [JsonRequired]
        public List<EntityCheckJson> Checks { get; set; }

    }
}