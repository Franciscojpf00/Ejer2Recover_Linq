using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LINQPractica
{
    class Program
    {
        public static void Main()
        {
            using (StreamReader r = new StreamReader(@"C:\Users\Javier\source\repos\LINQPractica\LINQPractica\Cars.json"))
            {
                string json = r.ReadToEnd();
                List<Coche> Coches = JsonConvert.DeserializeObject<List<Coche>>(json);
                //List<Coche> Coches = new List<Coche>();
                //Coches.Add(new Coche { id = 1234, Maker = "BMW", Model = "M4", Color = "negro", Year = 1992, Location = { Latitude = "12.56789", Longitude = "43.09876" } });
                //Mostrar Fabricante sin duplicar
                //metodos.Ej2(Coches);
                //metodos.Ej3(Coches);
                //metodos.Ej4(Coches);
                //metodos.Ej5(Coches);
                //metodos.Ej6(Coches);
                //metodos.Ej7(Coches);
                //metodos.Ej8(Coches);
                //metodos.Ej9(Coches);
                //metodos.Ej10(Coches);
                //metodos.Ej11(Coches);
                //metodos.Ej12(Coches);
            }
        }
        
            

        public class metodos
        {
            //Ejercicio2: Devuelve todos los fabricantes
            public static void Ej2(List<Coche> Coches)
            {
                var consulta = Coches.Select(o => o.Maker).Distinct().ToList();
                foreach (var fabricante in consulta)
                {
                    Console.WriteLine($"Fabricante : {fabricante}");
                }
            }

            //Ejercicio 3: mostrar colores distintos, marca y modelo
            public static void Ej3(List<Coche> Coches)
            {
                var consulta = Coches.Select(o => new { o.Color, o.Maker, o.Model }).Distinct().ToList();
                foreach (var coche in consulta)
                {
                    Console.WriteLine($"Color: {coche.Color} Marca: {coche.Maker} Modelo: {coche.Model}");
                }
            }

            //Ejercicio 4: mostrar fabricantes y modelos de color verde
            public static void Ej4(List<Coche> Coches)
            {
                var consulta = Coches.Select(o => o).Where(o => o.Color=="Green").ToList();
                foreach (var coche in consulta)
                {
                    Console.WriteLine($"Color: {coche.Color} Marca: {coche.Maker} Modelo: {coche.Model}");
                }
            }

            //Ejercicio 5: Permite al usuario introducir una latitud y una longitud. 
            //Indica al usuario si encuentra un coche del año 1992 dentro de esa latitud y longitud facilitada.
            public static void Ej5(List<Coche> Coches)
            {
                string lat;
                string lon;

                Console.Write("Introduce Latitud --> ");
                //lat = Double.Parse(Console.ReadLine());
                lat = Console.ReadLine();
                Console.Write("Introduce Longitud --> ");
                //lon = Double.Parse(Console.ReadLine());
                lon = Console.ReadLine();
                var consulta = Coches.Select(o => o).Where(o => o.Location.Latitude == lat && o.Location.Longitude== lon && o.Year==1992).ToList();
                if (consulta.Count() > 0)
                {
                    Console.WriteLine("Se han encontrado " + consulta.Count() + "coches");
                    foreach (var coche in consulta)
                    {
                        Console.WriteLine($"{coche.Maker} - {coche.Model}");
                    }
                    
                }else
                {
                    Console.WriteLine("No se han encontrado coches en esa Localizacion");
                }
            }

            //Ejercicio 6: Todos los coches del año superior al 2001
            public static void Ej6(List<Coche> Coches)
            {
                var consulta = Coches.Select(o => o).Where(o => o.Year > 2001).ToList();
                foreach (var coche in consulta)
                {
                    Console.WriteLine($"Marca: {coche.Maker} Modelo: {coche.Model} Año: {coche.Year}");
                }

            }

            //Ejercicio 7: Genera una nueva clase con modelo y fabricante. Muestra todos los 
            //coches que no tengan latitud, ni longitud Convierte en la búsqueda a esa clase.
            public static void Ej7(List<Coche> Coches)
            {
                var consulta = Coches.Select(o => o).Where(o => o.Location.Latitude==null && o.Location.Longitude==null).ToList();
                List<Ej7Coche> LostCars = new List<Ej7Coche>();
                foreach (var coche in consulta)
                {
                    LostCars.Add(new Ej7Coche { Maker=coche.Maker, Model=coche.Model});
                    Console.WriteLine($"Marca: {coche.Maker} Modelo: {coche.Model}");
                }
                Console.WriteLine("Se han encontrado " + LostCars.Count() + " coches perdidos");
            }

            //Ejercicio 8: Busca todos los coches de color Blue y que sean posteriores al año 2000.
            public static void Ej8(List<Coche> Coches)
            {
                var consulta = Coches.Select(o => o).Where(o => o.Year > 2000 && o.Color=="Blue").ToList();
                foreach (var coche in consulta)
                {
                    Console.WriteLine($"Marca: {coche.Maker} Modelo: {coche.Model} Año: {coche.Year} Color: {coche.Color}");
                }

            }

            //Ejercicio 9: Agrupa todos los coches por fabricante, muestralos por pantalla ordenados por año.
            public static void Ej9(List<Coche> Coches)
            {
                
                var consulta = Coches.GroupBy(g => new { g.Maker }).Select(o => o);

                foreach (var group in consulta)
                {
                    Console.WriteLine("**************************************");
                    Console.WriteLine($"Todos los {group.Key.Maker}" );
                    
                    foreach (var coche in group.OrderBy(o => o.Year))
                    {
                        Console.WriteLine($"- {coche.Model} {coche.Year}");
                    }
                }

            }

            //Ejercicio 10: Agrupa todos los coches por modelo, muestra los colores disponibles sin duplicar la muestra.

            public static void Ej10(List<Coche> Coches)
            {

                var result = Coches.GroupBy(o => o.Model).ToList();

                foreach (var coche in result)
                {
                    Console.WriteLine("Modelo: " + coche.Key);
                    var result1 = coche
                        .Select(o => new { o.Color, o.Model })
                        .Distinct()
                        .Where(
                            o => o.Model == coche.Key && o.Color != null)
                        .ToList();
                    foreach (var colores in result1)
                    {
                        Console.WriteLine(" - " + colores.Color);
                    }
                }
                                
            }

            //Ejercicio 11: Página de 10 en 10 pulsando una tecla y muestra todos los coches disponibles.
            //Interpreto que los que están disponibles, son los que no tienen atributos con valor nulo

            public static void Ej11(List<Coche> Coches)
            {
                var consulta = Coches.Select(o => new { o.Maker, o.Model, o.Color, o.Year, o.Location.Latitude}).Where(o => o.Latitude!=null).ToList();
                var puntero = 0;
                String tecla;

                do
                {
                    Console.WriteLine("Se han mostrado " + (puntero + 10) + " elementos de " + consulta.Count());
                    foreach (var coche in consulta.Skip(puntero).Take(10))
                    {
                        Console.WriteLine($"{coche.Maker}-{coche.Model}-{coche.Color}-{coche.Year}");

                    }
                    Console.WriteLine("Pulsa A para continuar");
                    tecla = Console.ReadLine();
                    puntero += 10;
                } while (tecla=="A");
            }

            //Ejercicio 12: Encuentra el primer coche posterior del año 2001 del fabricante Suzuki
            public static void Ej12(List<Coche> Coches)
            {
                var consulta = Coches.Select(o => new {o.Maker, o.Year, o.Model, o.Color}).Where(o => o.Maker =="Suzuki" && o.Year > 2001).First();
                Console.WriteLine($"{consulta.Maker}-{consulta.Model}-{consulta.Color}-{consulta.Year}");
            }

        }
    }
}
