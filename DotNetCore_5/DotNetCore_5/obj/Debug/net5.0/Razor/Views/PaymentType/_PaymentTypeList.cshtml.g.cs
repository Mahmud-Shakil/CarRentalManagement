#pragma checksum "D:\IDB-IT\MVC_Core\DotNetCore_5\DotNetCore_5\Views\PaymentType\_PaymentTypeList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "461d8741f3519d646aee87b5d8ad418c695dfdf6"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_PaymentType__PaymentTypeList), @"mvc.1.0.view", @"/Views/PaymentType/_PaymentTypeList.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\IDB-IT\MVC_Core\DotNetCore_5\DotNetCore_5\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\IDB-IT\MVC_Core\DotNetCore_5\DotNetCore_5\Views\_ViewImports.cshtml"
using DotNetCore_5;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\IDB-IT\MVC_Core\DotNetCore_5\DotNetCore_5\Views\_ViewImports.cshtml"
using DotNetCore_5.Data;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\IDB-IT\MVC_Core\DotNetCore_5\DotNetCore_5\Views\_ViewImports.cshtml"
using DotNetCore_5.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\IDB-IT\MVC_Core\DotNetCore_5\DotNetCore_5\Views\_ViewImports.cshtml"
using DotNetCore_5.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\IDB-IT\MVC_Core\DotNetCore_5\DotNetCore_5\Views\_ViewImports.cshtml"
using X.PagedList.Mvc.Core;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "D:\IDB-IT\MVC_Core\DotNetCore_5\DotNetCore_5\Views\_ViewImports.cshtml"
using X.PagedList.Web.Common;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "D:\IDB-IT\MVC_Core\DotNetCore_5\DotNetCore_5\Views\_ViewImports.cshtml"
using X.PagedList;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"461d8741f3519d646aee87b5d8ad418c695dfdf6", @"/Views/PaymentType/_PaymentTypeList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e1b30d4bc122c4320bbfa4d3217edd978bb658e0", @"/Views/_ViewImports.cshtml")]
    public class Views_PaymentType__PaymentTypeList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<PaymentTypeViewModel>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<h2>Payment Type List</h2>\r\n<div>\r\n    <table class=\"table table-bordered table-striped text-center\">\r\n\r\n        <tr>\r\n            <th>Id</th>\r\n            <th>Payment Type</th>\r\n            <th>Operation</th>\r\n        </tr>\r\n");
#nullable restore
#line 11 "D:\IDB-IT\MVC_Core\DotNetCore_5\DotNetCore_5\Views\PaymentType\_PaymentTypeList.cshtml"
         foreach (var item in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td>");
#nullable restore
#line 14 "D:\IDB-IT\MVC_Core\DotNetCore_5\DotNetCore_5\Views\PaymentType\_PaymentTypeList.cshtml"
               Write(item.PaymentTypeCode);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 15 "D:\IDB-IT\MVC_Core\DotNetCore_5\DotNetCore_5\Views\PaymentType\_PaymentTypeList.cshtml"
               Write(item.PaymentDiscription);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>\r\n                    <a href=\"#\" class=\"btn btn-warning\" name=\"btnEditModal\"");
            BeginWriteAttribute("onclick", " onclick=\'", 531, "\'", 574, 3);
            WriteAttributeValue("", 541, "Update(\"+", 541, 9, true);
#nullable restore
#line 17 "D:\IDB-IT\MVC_Core\DotNetCore_5\DotNetCore_5\Views\PaymentType\_PaymentTypeList.cshtml"
WriteAttributeValue("", 550, item.PaymentTypeCode, 550, 21, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 571, "+\")", 571, 3, true);
            EndWriteAttribute();
            WriteLiteral(" data-bs-toggle=\"modal\" data-bs-target=\"#EditModal\">Edit</a>\r\n                    <a href=\"#\" class=\"btn  btn-danger\" name=\"btnDeleteModal\"");
            BeginWriteAttribute("onclick", " onclick=\'", 714, "\'", 757, 3);
            WriteAttributeValue("", 724, "Delete(\"+", 724, 9, true);
#nullable restore
#line 18 "D:\IDB-IT\MVC_Core\DotNetCore_5\DotNetCore_5\Views\PaymentType\_PaymentTypeList.cshtml"
WriteAttributeValue("", 733, item.PaymentTypeCode, 733, 21, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 754, "+\")", 754, 3, true);
            EndWriteAttribute();
            WriteLiteral(" data-bs-toggle=\"modal\" data-bs-target=\"#DeleteModal\">Delete</a>\r\n                </td>\r\n            </tr>\r\n");
#nullable restore
#line 21 "D:\IDB-IT\MVC_Core\DotNetCore_5\DotNetCore_5\Views\PaymentType\_PaymentTypeList.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </table>\r\n</div>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<PaymentTypeViewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591