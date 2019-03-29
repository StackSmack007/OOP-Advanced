using System;
using System.Collections.Generic;
using System.Text;
using tiral_1.Contracts;

namespace tiral_1
{
  class Person<T> : Interface2<T>
        where T:new()
    {
        public Person(string lastName, string firstName)
        {
            LastName = lastName;
            this.firstName = firstName;
            Legacy = "sukrovishte";
        }

        protected string Legacy;
        public string LastName { get; }

        public string firstName { get; }

        public List<T> CreateList()
        {
            return new List<T> () { default(T), default(T), default(T)};
        }



    }


    
}
