#pragma checksum "C:\Users\RaviPrakas\Documents\dotNETCore\VS Studio\Empty\ASPwebappEmpty\ASPwebappEmpty\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c23c2be09757bc6cc00d5f4559972cc10f5b131b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
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
#line 1 "C:\Users\RaviPrakas\Documents\dotNETCore\VS Studio\Empty\ASPwebappEmpty\ASPwebappEmpty\Views\_ViewImports.cshtml"
using ASPwebappEmpty.Model;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\RaviPrakas\Documents\dotNETCore\VS Studio\Empty\ASPwebappEmpty\ASPwebappEmpty\Views\_ViewImports.cshtml"
using ASPwebappEmpty.ViewData;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c23c2be09757bc6cc00d5f4559972cc10f5b131b", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ae47bd1d8f004178780703550624ec3d42a3eac4", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ViewModelDTO>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 5 "C:\Users\RaviPrakas\Documents\dotNETCore\VS Studio\Empty\ASPwebappEmpty\ASPwebappEmpty\Views\Home\Index.cshtml"
  
    ViewBag.Title = Model.PageTitle;

#line default
#line hidden
#nullable disable
            WriteLiteral("    <h1>");
#nullable restore
#line 8 "C:\Users\RaviPrakas\Documents\dotNETCore\VS Studio\Empty\ASPwebappEmpty\ASPwebappEmpty\Views\Home\Index.cshtml"
   Write(Model.PageTitle);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\r\n    <table>\r\n        <thead>\r\n            <tr>\r\n                <td>ID</td>\r\n                <td>Name</td>\r\n                <td>Department</td>\r\n                <td>File</td>\r\n            </tr>\r\n        </thead>\r\n        <tbody>\r\n");
#nullable restore
#line 19 "C:\Users\RaviPrakas\Documents\dotNETCore\VS Studio\Empty\ASPwebappEmpty\ASPwebappEmpty\Views\Home\Index.cshtml"
             foreach (var Employee in Model.empDetails)
            {
                //var FilePath = Model.wwwpath + Employee.FileName;

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td>");
#nullable restore
#line 23 "C:\Users\RaviPrakas\Documents\dotNETCore\VS Studio\Empty\ASPwebappEmpty\ASPwebappEmpty\Views\Home\Index.cshtml"
               Write(Employee.ID);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 24 "C:\Users\RaviPrakas\Documents\dotNETCore\VS Studio\Empty\ASPwebappEmpty\ASPwebappEmpty\Views\Home\Index.cshtml"
               Write(Employee.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 25 "C:\Users\RaviPrakas\Documents\dotNETCore\VS Studio\Empty\ASPwebappEmpty\ASPwebappEmpty\Views\Home\Index.cshtml"
               Write(Employee.Department);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n");
#nullable restore
#line 26 "C:\Users\RaviPrakas\Documents\dotNETCore\VS Studio\Empty\ASPwebappEmpty\ASPwebappEmpty\Views\Home\Index.cshtml"
                 if (Employee.FileName == null)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <td>No File Found</td>\r\n");
#nullable restore
#line 29 "C:\Users\RaviPrakas\Documents\dotNETCore\VS Studio\Empty\ASPwebappEmpty\ASPwebappEmpty\Views\Home\Index.cshtml"
                }
                else
                {

                    System.Text.StringBuilder htmlbuilder = new System.Text.StringBuilder();
                    //string htmlBuilder2 = "";
                    string[] filenames = Employee.FileName.Split(';').ToArray();

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <td>\r\n");
#nullable restore
#line 37 "C:\Users\RaviPrakas\Documents\dotNETCore\VS Studio\Empty\ASPwebappEmpty\ASPwebappEmpty\Views\Home\Index.cshtml"
                         foreach (var fileName in filenames)
                        {
                            string filePath = Model.wwwpath + fileName.Trim();
                            ////htmlbuilder.Append(@<a target='_blank' href='"+ filePath + "'>"+ fileName.Trim() +"</a>);
                            //// htmlbuilder.Append(@Html.ActionLink("Download", "Download", new { link = filePath }));
                            //htmlBuilder2 += @Html.ActionLink("Download", "Download", new { link = filePath });
                            

#line default
#line hidden
#nullable disable
#nullable restore
#line 43 "C:\Users\RaviPrakas\Documents\dotNETCore\VS Studio\Empty\ASPwebappEmpty\ASPwebappEmpty\Views\Home\Index.cshtml"
                       Write(Html.ActionLink("Download", "Download", new { link = filePath }));

#line default
#line hidden
#nullable disable
#nullable restore
#line 43 "C:\Users\RaviPrakas\Documents\dotNETCore\VS Studio\Empty\ASPwebappEmpty\ASPwebappEmpty\Views\Home\Index.cshtml"
                                                                                             
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                    </td>\r\n");
#nullable restore
#line 46 "C:\Users\RaviPrakas\Documents\dotNETCore\VS Studio\Empty\ASPwebappEmpty\ASPwebappEmpty\Views\Home\Index.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                <td>\r\n                    ");
#nullable restore
#line 48 "C:\Users\RaviPrakas\Documents\dotNETCore\VS Studio\Empty\ASPwebappEmpty\ASPwebappEmpty\Views\Home\Index.cshtml"
               Write(Html.ActionLink("Delete", "Delete", new { id = Employee.ID }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            WriteLiteral("                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 53 "C:\Users\RaviPrakas\Documents\dotNETCore\VS Studio\Empty\ASPwebappEmpty\ASPwebappEmpty\Views\Home\Index.cshtml"
               Write(Html.ActionLink("Edit", "EditForm", new { id = Employee.ID }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 56 "C:\Users\RaviPrakas\Documents\dotNETCore\VS Studio\Empty\ASPwebappEmpty\ASPwebappEmpty\Views\Home\Index.cshtml"
               Write(Html.ActionLink("Details", "Details", new { id = Employee.ID }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n            </tr>\r\n");
#nullable restore
#line 59 "C:\Users\RaviPrakas\Documents\dotNETCore\VS Studio\Empty\ASPwebappEmpty\ASPwebappEmpty\Views\Home\Index.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </tbody>\r\n    </table>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ViewModelDTO> Html { get; private set; }
    }
}
#pragma warning restore 1591
