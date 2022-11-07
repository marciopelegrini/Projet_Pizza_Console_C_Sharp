using System;
using System.Linq;
using System.Text;

namespace projet_pizza
{
    class Pizza
    {
        string nom;
        public float prix { get; private set; }
        public bool vegetarienne { get; private set; }
        public List<string> ingredients { get; private set; }

        public Pizza(string nom, float prix, bool vegetarienne, List<string> ingredients)
        {
            this.nom = nom;
            this.prix = prix;
            this.vegetarienne = vegetarienne;
            this.ingredients = ingredients;
        }

        public void Afficher()
        {

            string badgeVegetarienne = vegetarienne ? " (V)" : ""; //if Ternarie
            string nomAfficher = FormatPremLettreMaju(nom);

            /*var ingredientsAfficher=new List<string>();
            foreach(var ingredient in ingredients)
            {
                ingredientsAfficher.Add(FormatPremLettreMaju(ingredient));
            }*/

            var ingredientsAfficher = ingredients.Select(i => FormatPremLettreMaju(i)).ToList();

            Console.WriteLine(nomAfficher + badgeVegetarienne + " - " + prix + "$");
            Console.WriteLine(string.Join(", ", ingredientsAfficher));
            Console.WriteLine();
        }

        private static string FormatPremLettreMaju(string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            string minuscules = s.ToLower();
            string majuscules = s.ToUpper();

            string result = majuscules[0] + minuscules[1..];

            return result;
        }

        public bool ContientIngredient(string ingredient)
        {
            return ingredients.Where(i => i.ToLower().Contains(ingredient)).ToList().Count > 0;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            //var pizza1 = new Pizza("4 fromages", 11.5f, false);
            //pizza1.Afficher();

            var listePizza = new List<Pizza>()
            {
                new Pizza("4 fromages", 11.5f, true, new List<string> {"cantal", "mozza", "suisse", "parmesan"}),
                new Pizza("margherita", 9f, true, new List<string> {"sauce tomate", "oignon", "poivron"}),
                new Pizza("mexicainne", 14f, false, new List<string>{"sauce", "piment", "mozza" }),
                new Pizza("Portugaise", 12.4f, false, new List<string>{"sauce", "olive", "jambom", "tomates"}),
                new Pizza("Californie", 8f, true, new List<string>{"sauce", "ananas", "avocat", "tomates"}),
            };

            // listePizza = listePizza.OrderBy(p => p.prix).ToList();

            float prixMin, prixMax;
            Pizza pizzaPrixMin = null;
            Pizza pizzaPrixMax = null;

            prixMin = listePizza[0].prix;
            prixMax = listePizza[0].prix;
            pizzaPrixMin = listePizza[0];
            pizzaPrixMax = listePizza[0];

            foreach (var pizza in listePizza)
            {
                if (pizza.prix < pizzaPrixMin.prix)
                {
                    pizzaPrixMin = pizza;
                }
                if (pizza.prix > pizzaPrixMax.prix)
                {
                    pizzaPrixMax = pizza;
                }
            }

            foreach (var pizza in listePizza)
            {
                pizza.Afficher();
            }

            // La pizza la moins chere est : ...
            // La pizza la plus chere est : ...
            Console.WriteLine();
            Console.WriteLine("La pizza la moins chere est :");
            pizzaPrixMin.Afficher();
            Console.WriteLine();
            Console.WriteLine("La pizza la plus chere est :");
            pizzaPrixMax.Afficher();

            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("pizzas -> vegetariennes");

            // 1 seule ligne where
            listePizza = listePizza.Where(p => p.vegetarienne).ToList();
            foreach (var pizza in listePizza)
            {
                pizza.Afficher();
            }

            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("pizza qui contient du tomate");

            listePizza = listePizza.Where(p => p.ContientIngredient("tomate")).ToList();
            foreach (var pizza in listePizza)
            {
                pizza.Afficher();
            }
        }
    }
}