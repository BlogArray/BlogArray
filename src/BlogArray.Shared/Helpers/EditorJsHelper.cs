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

            if (blocks == null)
            {
                return editorJsJson;
            }

            string buildHtml = string.Empty;

            foreach (dynamic block in blocks.blocks)
            {
                buildHtml += GetHtmlForBlock(block);
            }

            return buildHtml;
        }
        catch (Exception)
        {
            return editorJsJson;
        }
    }

    private static string GetHtmlForBlock(dynamic block)
    {
        string html = string.Empty;
        switch (block.type.ToString())
        {
            case "header":
                html = ParseHeader(block);
                break;
            case "raw":
                html = ParseRaw(block);
                break;
            case "paragraph":
                html = ParseParagraph(block);
                break;
            case "delimiter":
                html = ParseDivider();
                break;
            case "embed":
                html = ParseEmbed(block);
                break;
            case "image":
                html = ParseImage(block);
                break;
            case "list":
                html += ParseList(block);
                break;
            case "codeblock":
                html = ParseCode(block);
                break;
            case "quote":
                html = ParseQuote(block);
                break;
            case "linkTool":
                html = ParseLink(block);
                break;
            case "table":
                html = ParseTable(block);
                break;
            default:
                break;
        }

        return html;
    }

    private static string ParseTable(dynamic block)
    {
        string header = string.Empty, body = string.Empty;
        bool hadHeaders = Converter.ToBoolean(block.data.withHeadings);

        if (hadHeaders)
        {
            dynamic hdrArr = block.data.content[0];
            header += "<tr>";
            foreach (dynamic head in hdrArr)
            {
                header += $"<th>{Converter.ToString(head)}</th>";
            }
            header += "<tr>";
        }

        int index = hadHeaders ? 1 : 0;

        for (int i = index; i < block.data.content.Count; i++)
        {
            dynamic cblock = block.data.content[i];
            body += "<tr>";
            foreach (dynamic v in cblock)
            {
                body += $"<td>{Converter.ToString(v)}</td>";
            }
            body += "</tr>";
        }

        return $"<div class='table-responsive'><table class='table table-bordered'><thead>{header}</thead><tbody>{body}</tbody></table></div>";
    }

    private static string ParseHeader(dynamic block)
    {
        string alignment = "start";
        if (block.data.alignment == "center")
        {
            alignment = "center";
        }
        else if (block.data.alignment == "right")
        {
            alignment = "end";
        }

        return $"<h{Converter.ToString(block.data.level)} class='text-{alignment}'>{Converter.ToString(block.data.text)}</h{Converter.ToString(block.data.level)}>";
    }

    private static string ParseParagraph(dynamic block)
    {
        return $"<p>{Converter.ToString(block.data.text)}</p>";
    }

    private static string ParseRaw(dynamic block)
    {
        return $"{Converter.ToString(block.data.html)}";
    }


    private static string ParseEmbed(dynamic block)
    {
        return $"<figure class='figure'>" +
            $"<iframe width='560' height='420' class='figure-img img-fluid' src='{Converter.ToString(block.data.embed)}' frameborder='0' allow='autoplay; encrypted-media' allowfullscreen></iframe>" +
            $"<figcaption class='figure-caption'>{Converter.ToString(block.data.caption)}</figcaption>" +
            $"</figure>";
    }

    private static string ParseImage(dynamic block)
    {
        return $"<figure class='figure'>" +
            $"<img src='{Converter.ToString(block.data.url)}' class='figure-img img-fluid' alt='{Converter.ToString(block.data.caption)}'>" +
            $"<figcaption class='figure-caption'>{Converter.ToString(block.data.caption)}</figcaption>" +
            $"</figure>";
    }

    private static string ParseList(dynamic block)
    {
        string listStyle = Converter.ToString(block.data?.style) == "unordered" ? "ul" : "ol";
        //var html = "<" + listStyle + ">";
        //foreach (var li in block.data.items)
        //{
        //    html += $"<li>{Converter.ToString(li)}</li>";
        //};
        //html += "</" + listStyle + ">";

        return ListMaker(block.data.items, listStyle);
    }

    private static string ListMaker(dynamic items, string style)
    {
        string element = "";

        foreach (dynamic item in items)
        {
            if (string.IsNullOrEmpty(Converter.ToString(item.content)) && item.items.Count > 0)
            {
                element += $"<li>{Converter.ToString(item.content)}</li>";
            }

            string nested = string.Empty;
            if (item.items.Count > 0)
            {
                nested = ListMaker(item.items, style);
            }

            if (!string.IsNullOrEmpty(item.content.ToString()))
            {
                element += $"<li>{Converter.ToString(item.content)}</li>" + nested;
            }
        }

        return $"<{style}>{element}</{style}>";
    }

    private static string ParseCode(dynamic block)
    {
        string code = Converter.ToString(block.data.code);
        return $"<pre class='language-{Converter.ToString(block.data.language)}' lang='{Converter.ToString(block.data.language)}'>" +
            $"<code class='scrollbar language-{Converter.ToString(block.data.language)}'>" +
            $"{code.Replace("<", "&lt;")}" +
            $"</code></pre>";
    }

    private static string ParseQuote(dynamic block)
    {
        return $"<blockquote>" +
            $"<p>{Converter.ToString(block.data?.text)}</p>" +
            $"<cite>{Converter.ToString(block.data?.caption)}</cite>" +
            $"</blockquote>";
    }

    private static string ParseLink(dynamic block)
    {
        return $"<a href='{Converter.ToString(block.data?.link)}' target='_blank'>" +
            $"{Converter.ToString(block.data?.meta?.title) ?? Converter.ToString(block.data?.link)}" +
            $"</a>";
    }

    private static string ParseDivider()
    {
        return $"<hr />";
    }
}