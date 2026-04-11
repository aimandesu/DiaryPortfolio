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
            await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true,
                Args = new[] { "--no-sandbox", "--disable-setuid-sandbox" } // needed in Linux/Docker
            });

            await using var page = await browser.NewPageAsync();

            await page.SetContentAsync(html, new PuppeteerSharp.NavigationOptions
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
