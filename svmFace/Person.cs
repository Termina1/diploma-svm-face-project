using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SVM;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace svmFace
{
    public class Person
    {
        [BsonId(IdGenerator = typeof(CombGuidGenerator))]
        public Guid Id { get; set; }

        public Model model { get; set; }

        public String name { get; set; }
    }
}
