using System.Reflection;

namespace P01_HarvestingFields
{
    public static class FieldInfoExtension
    {
       public static string GetAccessModifier(this FieldInfo finfo)
        {
            if (finfo.IsFamily)
            {
                return "protected";
            }
            else if (finfo.IsAssembly)
            {
                return "internal";
            }
          //  else if (finfo.IsStatic)
          //  {
          //      return "static";
          //  }
            else if (finfo.IsPublic)
            {
                return "public";
            }
            else 
            {
                return "private";
            }
        }
    }
}