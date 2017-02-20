using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace Silk
{
    public abstract class SilkTag
    {
        string tagName;
        List<string> tagArgs = new List<string>();

        public virtual void SilkTagDefinition()
        {
            
        }

        protected void RunTag(List<string> args)
        {
            
        }

        protected void DefineArgument(string arg)
        {
            tagArgs.Add(arg);
        }

        protected void DefineArguments(string[] args)
        {
            tagArgs = args.ToList<string>();
        }

    }
}
