#pragma checksum "C:\Users\emrec\Desktop\web\swiftwheels\Views\Shared\AccessDenied.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "76efcc85b9373f36467deacacc85a64615b011614e179513101d55a846b9a931"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(SwiftWheels.Views.Shared.Views_Shared_AccessDenied), @"mvc.1.0.view", @"/Views/Shared/AccessDenied.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Shared/AccessDenied.cshtml", typeof(SwiftWheels.Views.Shared.Views_Shared_AccessDenied))]
namespace SwiftWheels.Views.Shared
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 2 "C:\Users\emrec\Desktop\web\swiftwheels\Views\_ViewImports.cshtml"
using SwiftWheels.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"76efcc85b9373f36467deacacc85a64615b011614e179513101d55a846b9a931", @"/Views/Shared/AccessDenied.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"cec5f6d40cb5969e40c82e349c7ed051561067453e943925f31de92298e9463e", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_AccessDenied : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "C:\Users\emrec\Desktop\web\swiftwheels\Views\Shared\AccessDenied.cshtml"
  
    ViewData["Title"] = "Access Denied";

#line default
#line hidden
            BeginContext(49, 114, true);
            WriteLiteral("\r\n<h2>Access Denied</h2>\r\n<p>You do not have permission to access this page.</p>\r\n<a href=\"/\">Return to Home</a>\r\n");
            EndContext();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
