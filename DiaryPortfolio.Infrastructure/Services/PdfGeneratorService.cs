using DiaryPortfolio.Application.IServices;
using Microsoft.AspNetCore.Components;
using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Infrastructure.Services
{
    public class PdfGeneratorService : IPdfGeneratorService
    {
        public async Task<byte[]> GenerateFromHtmlAsync(string html)
        {
            // 1. Locate the physical output.css on your server
            var cssPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "css", "output.css");
            var tailwindCss = await File.ReadAllTextAsync(cssPath);

            // 2. Inject the CSS into the HTML string
            // This replaces the <link> tag with the actual CSS content
            var finalHtml = html.Replace(
                "<link rel=\"stylesheet\" href=\"/css/output.css\" />",
                $"<style>{tailwindCss}</style>"
            );


            await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true,
                Args = new[] { "--no-sandbox", "--disable-setuid-sandbox" } // needed in Linux/Docker
            });

            await using var page = await browser.NewPageAsync();

            await page.SetContentAsync(finalHtml, new PuppeteerSharp.NavigationOptions
            {
                WaitUntil = new[] { WaitUntilNavigation.Networkidle0 } // waits for all resources
            });

            var pdf = await page.PdfDataAsync(new PdfOptions
            {
                Format = PuppeteerSharp.Media.PaperFormat.A4,
                PrintBackground = true,
                MarginOptions = new PuppeteerSharp.Media.MarginOptions
                {
                    Top = "40px",
                    Bottom = "40px",
                    Left = "40px",
                    Right = "40px"
                }
            });

            return pdf;
        }
    }
}
