using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Frontend;
using CommandLine;


namespace MiniCCli
{
    internal class Options
    {
        [Value(0)] public string SourceFile { get; set; }

        [Option('e', "emit-ir", Default = false, HelpText = "Stop at Intermediate Representation.")]
        public bool EmitIr { get; set; }

        [Option('o', "output", HelpText = "Output File Path")]
        public string OutputFile { get; set; }
    }

    static class Program
    {
        static int Run(Options options)
        {
            AntlrFileStream stream = new AntlrFileStream(options.SourceFile);
            ITokenSource lexer = new ProgramLexer(stream);
            ITokenStream tokens = new CommonTokenStream(lexer);
            ProgramParser parser = new ProgramParser(tokens)
            {
                BuildParseTree = true,
                ErrorHandler = new FrontEndErrorStrategy()
            };

            try
            {
                IParseTree tree = parser.program();
                var walker = new ParseTreeWalker();
                var frontEndListener = new FrontEndListener();
                walker.Walk(frontEndListener, tree);

                if (options.EmitIr)
                {
                    if (options.OutputFile != null)
                    {
                        File.WriteAllText(options.OutputFile, frontEndListener.Result);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Print result to screen due to no output file was specified.");
                        Console.ResetColor();
                        Console.WriteLine(frontEndListener.Result);
                    }
                }
                else
                {
                    var rlt = MiddleWares.MergeLabel.Merge(frontEndListener.Result);
                    ICharStream bstream = CharStreams.fromString(rlt);
                    ITokenSource blexer = new Backend.ProgramLexer(bstream);
                    ITokenStream btokens = new CommonTokenStream(blexer);
                    Backend.ProgramParser bparser = new Backend.ProgramParser(btokens)
                    {
                        BuildParseTree = true,
                        ErrorHandler = new FrontEndErrorStrategy()
                    };

                    IParseTree bTree = bparser.program();
                    var bWalker = new ParseTreeWalker();
                    var backendListener = new Backend.BackendListener();
                    bWalker.Walk(backendListener, bTree);
                    if (options.OutputFile != null)
                    {
                        File.WriteAllText(options.OutputFile, backendListener.Result, Encoding.ASCII);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Print result to screen due to no output file was specified.");
                        Console.ResetColor();
                        Console.WriteLine(backendListener.Result);
                    }
                }
            }
            catch (FrontEndException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: {0}", e.Message);
                return 1;
            }
            finally
            {
                Console.ResetColor();
            }

            return 0;
        }

        static int HandleError(IEnumerable<Error> errs)
        {
            var result = -2;
            Console.WriteLine("errors {0}", errs.Count());
            if (errs.Any(x => x is HelpRequestedError || x is VersionRequestedError))
                result = -1;
            Console.WriteLine("Exit code {0}", result);
            return result;
        }

        static void Main(string[] args)
        {
            var result = CommandLine.Parser.Default.ParseArguments<Options>(args).MapResult(Run, HandleError);
            Environment.Exit(result);
        }
    }
}