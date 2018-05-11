# LatticeSemi Scraper

## Description

The Lattice Semi company has a few cool IC's but not all distributers stock all their products. Naturally most would go to the vendors website and use their product selector, but sadly Lattice has a **horible** website for this. You can't even change what the products are ordered by! So I wrote a tool overnight that scrapes the website for these IC's and exports them as a JSON file, automatically sorted by ascending price per chip.

## Getting started

You need to download this repo with a ```git clone```, after which a simple ```dotnet build``` should suffice.

To actually run the scraper you can use ```dotnet run --```. The extra `--` at the end tell the dotnet utility to pass everything after the double minus signs as arguments for the scraper itself.

```bash
c:\code\Brainy\scraper (master -> origin)
λ dotnet run -- --help
scraper 1.0.0
Copyright (C) 2018 scraper

  -q, --query    Required. Query string used to finding IC's.

  -p, --pages    Pages to use for searching of IC's.

  -f, --file     File to save output to instead of stdout.

  --help         Display this help screen.

  --version      Display version information.

Failed to parse flags!
```

## Examples

First is a simple run, search only two pages worth of results for all IC's starting with "lcmxo3" (MaxhXO3 family). Notice the output of this is JSON with some very minor pretty printing.

```json
c:\code\Brainy\scraper (master -> origin)
λ dotnet run -- -p 2 -q lcmxo3*
[
        {"Name":"LCMXO3LF-4300E-5UWG81CTR50","Prices":[{"Qty":1,"Cost":5.14},{"Qty":25,"Cost":4.48},{"Qty":50,"Cost":4.48},{"Qty":100,"Cost":4.11}],"Stocked":true},
        {"Name":"LCMXO3LF-4300E-5UWG81ITR50","Prices":[{"Qty":1,"Cost":5.71},{"Qty":25,"Cost":4.98},{"Qty":50,"Cost":4.98},{"Qty":100,"Cost":4.76}],"Stocked":true},
        {"Name":"LCMXO3L-1300E-5MG256I","Prices":[{"Qty":1,"Cost":6.20},{"Qty":25,"Cost":5.41},{"Qty":100,"Cost":4.96}],"Stocked":true},
        {"Name":"LCMXO3L-2100E-5MG256I","Prices":[{"Qty":1,"Cost":7.43},{"Qty":25,"Cost":6.48},{"Qty":100,"Cost":5.95}],"Stocked":true},
        {"Name":"LCMXO3LF-4300E-5MG121C","Prices":[{"Qty":1,"Cost":7.90},{"Qty":25,"Cost":6.89},{"Qty":100,"Cost":6.32}],"Stocked":true},
        {"Name":"LCMXO3L-2100E-6MG256I","Prices":[{"Qty":1,"Cost":8.17},{"Qty":25,"Cost":7.13},{"Qty":100,"Cost":6.54}],"Stocked":true},
        {"Name":"LCMXO3L-4300E-6MG324I","Prices":[{"Qty":1,"Cost":10.22},{"Qty":25,"Cost":8.91},{"Qty":100,"Cost":8.18}],"Stocked":true},
        {"Name":"LCMXO3LF-4300C-5BG256C","Prices":[{"Qty":1,"Cost":11.42},{"Qty":25,"Cost":9.96},{"Qty":100,"Cost":9.50}],"Stocked":true},
        {"Name":"LCMXO3LF-2100C-6BG324C","Prices":[{"Qty":1,"Cost":13.02},{"Qty":25,"Cost":11.34},{"Qty":100,"Cost":10.43}],"Stocked":true},
        {"Name":"LCMXO3L-6900C-6BG324C","Prices":[{"Qty":1,"Cost":13.29},{"Qty":25,"Cost":11.59},{"Qty":100,"Cost":10.64}],"Stocked":true},
        {"Name":"LCMXO3LF-6900E-6MG324C","Prices":[{"Qty":1,"Cost":13.37},{"Qty":25,"Cost":11.66},{"Qty":100,"Cost":10.71}],"Stocked":true},
        {"Name":"LCMXO3LF-6900E-6MG256I","Prices":[{"Qty":1,"Cost":14.12},{"Qty":25,"Cost":12.31},{"Qty":100,"Cost":11.31}],"Stocked":true},
        {"Name":"LCMXO3LF-6900C-5BG256I","Prices":[{"Qty":1,"Cost":15.61},{"Qty":25,"Cost":13.61},{"Qty":100,"Cost":12.51}],"Stocked":true},
        {"Name":"LCMXO3LF-6900C-6BG324C","Prices":[{"Qty":1,"Cost":15.95},{"Qty":25,"Cost":13.91},{"Qty":100,"Cost":13.27}],"Stocked":true},
        {"Name":"LCMXO3LF-6900C-6BG400I","Prices":[{"Qty":1,"Cost":18.55},{"Qty":25,"Cost":16.17},{"Qty":100,"Cost":14.86}],"Stocked":true},
        {"Name":"LCMXO3LF-6900C-S-EVN","Prices":[{"Qty":1,"Cost":24.95},{"Qty":5,"Cost":23.27}],"Stocked":true},
        {"Name":"LCMXO3L-6900C-S-EVN","Prices":[{"Qty":1,"Cost":24.95}],"Stocked":true},
        {"Name":"LCMXO3LF-9400C-ASC-B-EVN","Prices":[{"Qty":1,"Cost":39.00}],"Stocked":true},
        {"Name":"LCMXO3L-DSI-EVN","Prices":[{"Qty":1,"Cost":149.00}],"Stocked":true},
        {"Name":"LCMXO3L-SMA-EVN","Prices":[{"Qty":1,"Cost":218.75}],"Stocked":true}
]
```

Search for all MaxhXO2 series of chips which are **not** in stock.

```json
c:\code\Brainy\scraper (master -> origin)
λ dotnet run -- -q lcmxo2* | grep "false"
[
        {"Name":"LCMXO2-256HC-4SG32C","Prices":[{"Qty":1,"Cost":2.80},{"Qty":25,"Cost":2.45},{"Qty":100,"Cost":2.28}],"Stocked":false},
        {"Name":"LCMXO256C-4TN100I","Prices":[{"Qty":90,"Cost":2.94},{"Qty":180,"Cost":2.79}],"Stocked":false},
                            ...
        {"Name":"LCMXO2-256HC-5UMG64I","Prices":[{"Qty":490,"Cost":3.48}],"Stocked":false},
        {"Name":"LCMXO2-256ZE-2UMG64I","Prices":[{"Qty":490,"Cost":3.48}],"Stocked":false},
        {"Name":"LCMXO2-640HC-4SG48I","Prices":[{"Qty":1,"Cost":4.35},{"Qty":25,"Cost":3.80},{"Qty":100,"Cost":3.50}],"Stocked":false}
]
```

## Warning

Note this scrapes a website, and may put you in legal hot water depending on the laws where you live. Also, don't hammer their website for no reason, there is a feature here which shows how to save to a file.

## License

This is released under an MIT license. Enjoy!
