﻿using System.Collections.Generic;

namespace midleLevel.Models
{
    public class Translation
    {
        public string translatedText { get; set; }
    }

    public class Data
    {
        public List<Translation> translations { get; set; }
    }

    public class TransatorApiResult
    {
        public Data data { get; set; }
    }
}
