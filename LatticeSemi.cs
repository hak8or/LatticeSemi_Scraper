using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

using AngleSharp;

namespace Scraper {
    class LatticeSemi {
        // Keep track of pricing for each unit.
        public class Product {
            public string Name;
            public struct Price {
                public int Qty;
                public Decimal Cost;
            }
            public List<Price> Prices;
            public bool Stocked;
        }

        static async Task<List<Product>> Parse_Page(int page, string searchid){
            // Get the page itself.
            var page_fetch = await BrowsingContext
                    .New(Configuration.Default.WithDefaultLoader())
                    .OpenAsync(
                        "http://www.latticestore.com/products/tabid/417/pageindex/" + page + 
                        "/searchid/1/default.aspx?searchvalue=" + searchid
                    );

            // Get all the rows of products in the page.
            var rows = page_fetch.QuerySelectorAll(".StoreCategoryProductItem_Table");

            // Get pairs for each row saying the product name and price.
            return rows.Select(r =>
                new Product(){
                    Name = r.QuerySelectorAll(".productItemtd2 .StoreProductLinkTitle").Single().TextContent,
                    Stocked = r.QuerySelectorAll(".productItemtd4 .StoreProductStockQuantity").Single().TextContent.Contains("Yes"),
                    Prices = r.QuerySelectorAll(".productItemtd3 .PricingTable tr").Select(price_row =>
                        new Product.Price(){
                            Qty = int.Parse(price_row.QuerySelectorAll(".tdPricingLabel").Single().TextContent.Replace(":", "").Trim()),
                            Cost = Decimal.Parse(price_row.QuerySelectorAll(".tdPricingItem").Single().TextContent.Replace("$", "").Trim())
                        }
                    ).ToList()
                }
            ).ToList();
        }

        // Product listing goes here.
        public static async Task<List<Product>> Search(int Page_Count, bool verbose, string searchid) {
            // Fetch all the pages
            var Fetches =
                Enumerable.Range(1, Page_Count)
                .Select(page => Parse_Page(page, searchid));

            // Wait for the page fetches to finish.
            var Pages = await Task.WhenAll(Fetches);

            // Flatten the pages of products into just products.
            var Products = Pages.SelectMany(x => x);

            // Order the pages by the cheapest item we can find.
            return Products.OrderBy(p => p.Prices.OrderBy(v => v.Cost).First().Cost).ToList();
        }
    }
}