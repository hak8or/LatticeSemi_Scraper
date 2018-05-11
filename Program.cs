using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

using Newtonsoft.Json;
using CommandLine;

namespace Scraper {
    class Program {
        public class Options {
            [Option('p', "pages", Required = true, HelpText = "Pages to use for searching of IC's.")]
            public int Page_Count { get; set; }

            [Option('q', "query", Required = true, HelpText = "Query string used to finding IC's.")]
            public string Query { get; set; }
        }

        static void Main(string[] args) {
            CommandLine.Parser.Default.ParseArguments<Options>(args)
                .WithNotParsed<Options>((errs) => Console.WriteLine("Failed to parse flags!"))
                .WithParsed<Options>(opts => {
                    // Get all the products based on the search query.
                    var Products = LatticeSemi.Search(opts.Page_Count, false, opts.Query).Result;

                    // Dump them to the terminal.
                    Console.Write(String.Join("\n", Products.Select(p=>JsonConvert.SerializeObject(p))));
                });
        }
    }
}
