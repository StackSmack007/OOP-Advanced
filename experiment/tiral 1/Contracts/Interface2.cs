using System;
using System.Collections.Generic;
using System.Text;

namespace tiral_1.Contracts
{
  public  interface Interface2<T> :Interface1
    {
        string LastName { get; }
    }
}
