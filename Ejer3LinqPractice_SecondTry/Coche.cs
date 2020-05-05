using System;
using System.Collections.Generic;
using System.Text;

namespace LINQPractica
{
    public class Coche
    { 
        public int? id;
        public String Maker;
        public String Model;
        public int? Year;
        public String Color;
        public Localizacion Location = new Localizacion();
    
    }
}
