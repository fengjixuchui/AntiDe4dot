using dnlib.DotNet;
using dnlib.DotNet.Writer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntiDe4dot
{
    class Program
    {

        public static void Yeboy(Context context)
        {
            Random rnd = new Random();


            foreach (ModuleDef module in context.Assembly.Modules)
            {
                InterfaceImpl interfaceM = new InterfaceImplUser(module.GlobalType);


                for (int i = 100; i < 150; i++)
                {
                    TypeDef typeDef1 = new TypeDefUser("", $"Form{i.ToString()}", module.CorLibTypes.GetTypeRef("System", "Attribute"));
                    InterfaceImpl interface1 = new InterfaceImplUser(typeDef1);
                    module.Types.Add(typeDef1);
                    typeDef1.Interfaces.Add(interface1);
                    typeDef1.Interfaces.Add(interfaceM);

                }
            }
        }

        static void Main(string[] args)
        {

            if (args.Length < 1)
            {
                Console.WriteLine("You must drag your file to protect into the protector to use it.", ConsoleColor.Red);
                Console.ReadLine();
            }
            else
            {

                string filetoprotect = args[0];
                AssemblyDef assembly = AssemblyDef.Load(filetoprotect);
                Context ctx = new Context(assembly);
                Yeboy(ctx);
               
                
                string pathwithoutexe = filetoprotect.Replace(".exe", "");
                var Options = new ModuleWriterOptions(ctx.ManifestModule);
                Options.Logger = DummyLogger.NoThrowInstance;
                assembly.Write(pathwithoutexe + "-AntiDe4dot.exe", Options);
                Console.WriteLine($"Done. File saved as {pathwithoutexe}-AntiDe4dot.exe", ConsoleColor.Cyan);
                Console.ReadLine();

            }

        }
    }
}
