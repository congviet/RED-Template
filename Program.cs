using de4dot.code;
using dnlib.DotNet;
using dnlib.DotNet.Writer;
using System;
using System.IO;

namespace RED_Template
{
    class Program
    {
        static ModuleDefMD module;
        static void Main(string[] args)
        {
            Logger.n("RED template");
            Logger.n("Created by CONGVIET");
            Logger.n("");
            Logger.n("RED v{0}", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version);
            Logger.n("");
            string path = "";
            try
            {
                if (args != null && args.Length > 0)
                {
                    path = args[0];
                    module = ModuleDefMD.Load(path);
                }
                else
                {
                    Logger.n("Please select file");
                    Console.ReadKey();
                    return;
                }
            }
            catch(Exception ex)
            { 
                Console.ForegroundColor = ConsoleColor.Red; 
                Logger.n(ex.Message); 
            }
            #region Save module to file
            Console.ForegroundColor = ConsoleColor.Green;
            Logger.n("Finished!");
            Logger.n("");

            string outPath = "";
            int i = 1;
            while (File.Exists(outPath))
            {
                outPath = Path.GetFileNameWithoutExtension(path) + $"-Cleaned{i}" + Path.GetExtension(path);
                i++;
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Logger.n("Saving...");
            if (module.IsILOnly) 
            {
                var writer = new ModuleWriterOptions(module);
                writer.MetadataOptions.Flags |= MetadataFlags.PreserveAll;
                writer.Logger = DummyLogger.NoThrowInstance;
                module.Write(outPath, writer); 
            } 
            else 
            {

                var nativeWriter = new NativeModuleWriterOptions(module, true);
                nativeWriter.MetadataOptions.Flags |= MetadataFlags.PreserveAll;
                nativeWriter.Logger = DummyLogger.NoThrowInstance;
                module.NativeWrite(outPath, nativeWriter);
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Logger.n("File saved: "+outPath);
            #endregion

            Console.ResetColor();
            Logger.n("");
            Logger.n("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
