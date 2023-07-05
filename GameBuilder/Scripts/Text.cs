using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBuilder.Scripts
{
    internal class Text : Script
    {
        public string text = "Sallamander Man";

        public Text(string text)
        {
            this.text = text.Replace('_', ' ');
        }
    }
}
