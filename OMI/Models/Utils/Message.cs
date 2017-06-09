using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace OMI.Models.Utils
{
    public enum MessageToIndexType
    {
        Information,
        Error,
        Success,
        Warning
    }

    public static class MessageToIndexManager
    {
        public static void SetMessage(TempDataDictionary tempData, string text)
        {
            tempData["MessageToIndex"] = new MessageToIndex(text);
        }
        public static void SetMessage(TempDataDictionary tempData, string text, MessageToIndexType type)
        {
            tempData["MessageToIndex"] = new MessageToIndex(text, type);
        }
    }

    public class MessageToIndex
    {
        public readonly string Text;
        public readonly MessageToIndexType Type;
        public readonly string CssClass;

        public MessageToIndex(string text)
        {
            Text = StripHtmlTags(text);
            Type = MessageToIndexType.Information;
            CssClass = ConvertTypeToCssClass();
        }

        public MessageToIndex(string text, MessageToIndexType type)
        {
            Text = StripHtmlTags(text);
            Type = type;
            CssClass = ConvertTypeToCssClass();
        }

        private static string StripHtmlTags(string text)
        {
            return Regex.Replace(text, @"<[^(\/?(strong|b|i|u))]{1}[^>]*>", string.Empty);
        }

        private string ConvertTypeToCssClass()
        {
            string cssClass;
            switch (Type)
            {
                case MessageToIndexType.Information:
                    cssClass = "alert-info";
                    break;
                case MessageToIndexType.Error:
                    cssClass = "alert-danger";
                    break;
                case MessageToIndexType.Success:
                    cssClass = "alert-success";
                    break;
                case MessageToIndexType.Warning:
                    cssClass = "alert-warning";
                    break;
                default:
                    cssClass = "";
                    break;
            }
            return cssClass;
        }

    }
}