﻿using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repository.Models
{
    public class Transformer
    {
        public ObjectId Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
