using Problem1EventImplementation.Contracts;
using System;

namespace Problem1EventImplementation
{
    public class Dispatcher:INameChangeable
    {
        public event EventHandler<NameChangeEventArgs> NameChangeEvent;

        private string name;

        public Dispatcher(string name)
        {
            this.name = name;
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (value != name)
                {
                    NameChangeEvent(this,new NameChangeEventArgs(value));
                  //  OnNameChange(new NameChangeEventArgs(value));
                }
                name = value;
            }
        }

              //  private void OnNameChange(NameChangeEventArgs args)
      //  {
      //      if (NameChangeEvent != null)
      //      {
      //          NameChangeEvent(this,args);
      //      }
      //  }
    }
}