using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using AngleSharp;
using AngleSharp.XPath;

namespace AngleSharpXPathReadSubTreeTests
{
    class Program
    {
        async static Task Main(string[] args)
        {
            var html = @"<!doctype html>
<head>
  <title>Test</title>
  <style>
  .p { font-size: 1.1em;}
  </style>
</head>
<body>
<h1 id=intro>Test</h1>
<p>Paragraph 1.
<p>Paragraph 2.
</body>
</html>";
            var config = Configuration.Default.WithXPath();

            var context = BrowsingContext.New(config);

            var angleSharpDoc = await context.OpenAsync(req => req.Content(html));

            using (XmlReader xr = angleSharpDoc.CreateNavigator(false).ReadSubtree())
            {
                while (xr.Read())
                {
                    Console.WriteLine("NodeType: {0}; Name: {1}; LocalName: {2}; Prefix: {3}, NamespaceURI: {4}", xr.NodeType, xr.Name, xr.LocalName, xr.Prefix, xr.NamespaceURI);
                }
            }
        }
    }
}
