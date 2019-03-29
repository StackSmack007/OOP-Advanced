using System;
using System.Collections.Generic;

namespace _07InfernoInfinity.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MyCustomAttribute : Attribute
    {
        private string[] reviewers;

        public MyCustomAttribute( string author, int revision, string description,params string[] reviewers)
        {
            Author = author;
            Revision = revision;
            Description = description;
            this.reviewers = reviewers;
        }
        public string Author { get; }
        public int Revision { get; }
        public string Description { get; }
        public IReadOnlyCollection<string> Reviewers => Array.AsReadOnly(reviewers);
    }
}