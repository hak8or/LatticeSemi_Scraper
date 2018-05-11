using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.IO;

using Newtonsoft.Json;
using CommandLine;

namespace Scraper {
    class Program {
        public class Options {
            [Option('q', "query", Required = true, HelpText = "Query string used to finding IC's.")]
            public string Query { get; set; }

            [Option('p', "pages", Required = false, Default = int.MinValue, HelpText = "Pages to use for searching of IC's.")]
            public int Page_Count { get; set; }

            [Option('f', "file", Required = false, Default = "", HelpText = "File to save output to instead of stdout.")]
            public string JSON_File { get; set; }
        }

        static void Main(string[] args) {
            CommandLine.Parser.Default.ParseArguments<Options>(args)
                .WithNotParsed<Options>((errs) => Console.WriteLine("Failed to parse flags!"))
                .WithParsed<Options>(opts => {
                    // Get all the products based on the search query.
                    var Products = LatticeSemi.Search(opts.Query, opts.Page_Count).Result;

                    // Make a dense yet still pretty print representation of the JSON data.
                    string dump = "[\n\t" + String.Join(",\n\t", Products.Select(p=>JsonConvert.SerializeObject(p))) + "\n]";

                    // Save JSON output to file or stdout.
                    if (!string.IsNullOrEmpty(opts.JSON_File))
                        File.WriteAllText(opts.JSON_File, dump);
                    else
                        Console.Write(dump);
                });
        }
    }
}
