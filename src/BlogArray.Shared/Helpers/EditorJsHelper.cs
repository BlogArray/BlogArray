using System.Text;
using System.Text.Json;

namespace BlogArray.Shared.Helpers;

public static class EditorJsHelper
{
    //https://github.com/rizki4106/editor-js-parser/blob/master/table/index.js
    public static string BuildHtml(string editorJsJson)
    {
        try
        {
            dynamic? blocks = JsonSerializer.Deserialize<dynamic?>(editorJsJson);

            if (blocks?.blocks == null)
            {
                return editorJsJson;
            }

            StringBuilder buildHtml = new();

            foreach (dynamic block in blocks.blocks)
            {
                buildHtml.Append(GetHtmlForBlock(block));
            }

            return buildHtml.ToString();
        }
        catch (Exception)
        {
            return editorJsJson;
        }
    }

    private static string GetHtmlForBlock(dynamic block)
    {
        //string html = string.Empty;
        //switch (block.type.ToString())
        //{
        //    case "header":
        //        html = ParseHeader(block);
        //        break;
        //    case "raw":
        //        html = ParseRaw(block);
        //        break;
        //    case "paragraph":
        //        html = ParseParagraph(block);
        //        break;
        //    case "delimiter":
        //        html = ParseDivider();
        //        break;
        //    case "embed":
        //        html = ParseEmbed(block);
        //        break;
        //    case "image":
        //        html = ParseImage(block);
        //        break;
        //    case "list":
        //        html += ParseList(block);
        //        break;
        //    case "codeblock":
        //        html = ParseCode(block);
        //        break;
        //    case "quote":
        //        html = ParseQuote(block);
        //        break;
        //    case "linkTool":
        //        html = ParseLink(block);
        //        break;
        //    case "table":
        //        html = ParseTable(block);
        //        break;
        //    default:
        //        break;
        //}

        //return html;

        return block.type.ToString() switch
        {
            "header" => ParseHeader(block),
            "raw" => ParseRaw(block),
            "paragraph" => ParseParagraph(block),
            "delimiter" => ParseDivider(),
            "embed" => ParseEmbed(block),
            "image" => ParseImage(block),
            "list" => ParseList(block),
            "codeblock" => ParseCode(block),
            "quote" => ParseQuote(block),
            "linkTool" => ParseLink(block),
            "table" => ParseTable(block),
            _ => string.Empty,
        };
    }

    private static string ParseTable(dynamic block)
    {
        var tableHtml = new StringBuilder();
        var hadHeaders = Converter.ToBoolean(block.data.withHeadings);

        if (hadHeaders)
        {
            var headerRow = block.data.content[0];
            tableHtml.Append("<thead><tr>");
            foreach (var head in headerRow)
            {
                tableHtml.Append($"<th>{Converter.ToString(head)}</th>");
            }
            tableHtml.Append("</tr></thead>");
        }

        tableHtml.Append("<tbody>");
        var startRow = hadHeaders ? 1 : 0;
        for (int i = startRow; i < block.data.content.Count; i++)
        {
            var row = block.data.content[i];
            tableHtml.Append("<tr>");
            foreach (var cell in row)
            {
                tableHtml.Append($"<td>{Converter.ToString(cell)}</td>");
            }
            tableHtml.Append("</tr>");
        }
        tableHtml.Append("</tbody>");

        return $"<div class='table-responsive'><table class='table table-bordered'>{tableHtml}</table></div>";
    }

    private static string ParseHeader(dynamic block)
    {
        var level = Converter.ToString(block.data.level);
        var alignment = block.data.alignment.ToString() switch
        {
            "center" => "center",
            "right" => "end",
            _ => "start"
        };
        var text = Converter.ToString(block.data.text);

        return $"<h{level} class='text-{alignment}'>{text}</h{level}>";
    }

    private static string ParseParagraph(dynamic block)
    {
        return $"<p>{Converter.ToString(block.data.text)}</p>";
    }

    private static string ParseRaw(dynamic block)
    {
        return Converter.ToString(block.data.html);
    }

    private static string ParseEmbed(dynamic block)
    {
        var embedUrl = Converter.ToString(block.data.embed);
        var caption = Converter.ToString(block.data.caption);

        return $"<figure class='figure'>" +
            $"<iframe width='560' height='420' class='figure-img img-fluid' src='{embedUrl}' frameborder='0' allow='autoplay; encrypted-media' allowfullscreen></iframe>" +
            $"<figcaption class='figure-caption'>{caption}</figcaption>" +
            $"</figure>";
    }

    private static string ParseImage(dynamic block)
    {
        var imageUrl = Converter.ToString(block.data.url);
        var caption = Converter.ToString(block.data.caption);

        return $"<figure class='figure'>" +
            $"<img src='{imageUrl}' class='figure-img img-fluid' alt='{caption}'>" +
            $"<figcaption class='figure-caption'>{caption}</figcaption>" +
            $"</figure>";
    }

    private static string ParseList(dynamic block)
    {
        var listType = Converter.ToString(block.data?.style) == "unordered" ? "ul" : "ol";
        return BuildListItems(block.data.items, listType);
    }

    private static string BuildListItems(dynamic items, string listType)
    {
        var listHtml = new StringBuilder();
        listHtml.Append($"<{listType}>");

        foreach (var item in items)
        {
            var content = Converter.ToString(item.content);
            listHtml.Append($"<li>{content}</li>");

            if (item.items?.Count > 0)
            {
                listHtml.Append(BuildListItems(item.items, listType));
            }
        }

        listHtml.Append($"</{listType}>");
        return listHtml.ToString();
    }

    private static string ParseCode(dynamic block)
    {
        var code = Converter.ToString(block.data.code).Replace("<", "&lt;");
        var language = Converter.ToString(block.data.language);

        return $"<pre class='language-{language}' lang='{language}'>" +
               $"<code class='scrollbar language-{language}'>{code}</code></pre>";
    }

    private static string ParseQuote(dynamic block)
    {
        var text = Converter.ToString(block.data?.text);
        var caption = Converter.ToString(block.data?.caption);

        return $"<blockquote><p>{text}</p><cite>{caption}</cite></blockquote>";
    }

    private static string ParseLink(dynamic block)
    {
        var link = Converter.ToString(block.data?.link);
        var title = Converter.ToString(block.data?.meta?.title) ?? link;

        return $"<a href='{link}' target='_blank'>{title}</a>";
    }

    private static string ParseDivider()
    {
        return $"<hr />";
    }
}
