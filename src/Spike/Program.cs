using System;

namespace Spike
{
    class Program
    {
        static void Main(string[] args)
        {
            var s = "Person\n\tiHeightInInches\n\tdDateOfBirth\nAddress\n\tsCity\n\tsState";

            var lines = s.Split(new char[] {'\n'}, StringSplitOptions.None);
            var className="";
            var propertyName="";
            var result = "";

            foreach (var line in lines)
            {
                if(!line.StartsWith("\t"))
                {
                    if(!string.IsNullOrEmpty(line))
                        result+="}";

                    className=line.Trim();
                    result+=$"\n\npublic class {className} \n{{\n";
                    continue;
                }

                propertyName=line.Trim();
                var property = PropertyDefinition.Parse(propertyName);
                result+=$"\tpublic {property.Type} {property.Name} {{get;set;}}\n";


            }
            result+="}";

            // var result1 = "public class {className} \n\tpublic int Number {get;set;}\n}";
             Console.WriteLine(result);
             Console.ReadLine();
        }
    }
    public class PropertyDefinition {
        public string Name { get; set; }
        public string Type { get; set; }

        public static PropertyDefinition Parse(string property)
        {
            var result = new PropertyDefinition();
            switch(property[0].ToString().ToLower())
            {
                case "i":
                    result.Type="int";
                    break;

                case "d":
                    result.Type="DateTime";
                    break;
                default:
                    result.Type="string";
                    break;
            }
            result.Name=property.Substring(1);
            return result;
        }
    }
}
