using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace Silk
{
    public abstract class SilkTagBase
    {
		protected string _rawTag;
        protected string _tagName;
		//protected string _value;
		protected int priority;

        List<string> _silkTagArgs = new List<string>();

		public string Value{ get; set; }
        public string TagName
        {
            get
            {
                return _tagName;
            }
        }

		public string RawTag{
			get{
				return _rawTag;
			}
			set{
				_rawTag = value;
			}
		}

        public virtual string ReplaceWith() {

            return null;
        }

        public virtual void SilkTagDefinition()
        {

        }

        protected void RunTag(List<string> args)
        {

        }

        protected void SetName(string name)
        {
            _tagName = name;
        }

        protected void DefineArgument(string arg)
        {
            _silkTagArgs.Add(arg);
        }

        protected void DefineArguments(string[] args)
        {
            _silkTagArgs = args.ToList<string>();
        }

        protected void DefineArguments(List<string> args) {
            _silkTagArgs = args;
        }

    }
}
