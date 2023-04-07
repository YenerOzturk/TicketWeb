#pragma checksum "/Users/yener.ozturk/Projects/TicketWeb/Ticket.Presentation/Views/Version/VersionList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0b5fa21192ceb418a51a42b25a1e6a60808a3be3"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Version_VersionList), @"mvc.1.0.view", @"/Views/Version/VersionList.cshtml")]
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
#line 1 "/Users/yener.ozturk/Projects/TicketWeb/Ticket.Presentation/Views/_ViewImports.cshtml"
using Ticket.Presentation;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/yener.ozturk/Projects/TicketWeb/Ticket.Presentation/Views/_ViewImports.cshtml"
using Ticket.Presentation.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0b5fa21192ceb418a51a42b25a1e6a60808a3be3", @"/Views/Version/VersionList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b5b80179090756b5b3afa7471abe24d7f2ecc9ed", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Version_VersionList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
<script>

    function Delete(id) {

        var callback = function () {

            var data = { Id: id };

            $.ajax({
                type: 'POST',
                url: '../../Version/DeleteVersion',
                data: data,
                success: function () {

                    table.ajax.reload();
                }
            });
        }

        ShowMessageBox(""Emin misiniz?"", ""Kayıt Silinecektir"", ""question"", callback)

    }
</script>

<div class=""card card-custom"">
    <div class=""card-header flex-wrap border-0 pt-6 pb-0"">
        <div class=""card-title"">
            <h3 class=""card-label"">
                Versiyon Listesi
            </h3>
        </div>
        <div class=""card-toolbar"">
            <!--begin::Dropdown-->
            <div class=""dropdown dropdown-inline mr-2"">

            </div>
            <!--end::Dropdown-->
            <!--begin::Button-->
            <a href=""../../Version/Index"" class=""btn btn-primary font-weight-bolder"">
                <i class=""la l");
            WriteLiteral(@"a-plus""></i>Yeni Kayıt
            </a>
            <!--end::Button-->
        </div>
    </div>
    <div class=""card-body"">
        <!--begin: Datatable-->
        <table class=""table table-bordered table-hover table-checkable"" id=""versionList"">
            <thead>
                <tr>
                    <th>Cari İsim</th>
                    <th>Ürün</th>
                    <th>Başlangıç Tarihi</th>
                    <th>Bitiş Tarihi</th>
                    <th>Para Birimi</th>
                    <th>Periyod</th>
                    <th>Taksit</th>
                    <th>#</th>
                    <th>#</th>
                </tr>
            </thead>
        </table>
        <!--end: Datatable-->
    </div>
</div>

<script>


    var table;


    $(document).ready(function () {

        $(""#menuUser"").removeClass(""menu-item-open menu-item-here"");
        $(""#menuTicket"").removeClass(""menu-item-open menu-item-here"");
        $(""#menuCalendar"").removeClass(""menu-item-open menu-item-here"");
        $(""#");
            WriteLiteral(@"menuProject"").removeClass(""menu-item-open menu-item-here"");
        $(""#menuVersion"").removeClass(""menu-item-open menu-item-here"").addClass(""menu-item-open menu-item-here"");
        $(""#menuCustomer"").removeClass(""menu-item-open menu-item-here"");
        $(""#menuReport"").removeClass(""menu-item-open menu-item-here"");


        table = $('#versionList').DataTable({
            ""processing"": true,
            ""searching"": false,
            ""info"": false,
            ""paging"": false,
            ""serverSide"": true,
            ""ordering"": false,
            ""ajax"": {
                ""url"": ""../../Version/GetVersions"",
                ""dataType"": ""JSON"",
                ""data"": function (d) {

                }
            },
            ""columns"": [
                {
                    ""data"": ""cardName"",
                    ""autoWidth"": true,
                },
                {
                    ""data"": ""productName"",
                    ""autoWidth"": true,
                },
                {
              ");
            WriteLiteral(@"      ""data"": ""startDateAsString"",
                    ""autoWidth"": true,
                },
                {
                    ""data"": ""endDateAsString"",
                    ""autoWidth"": true,
                },
                {
                    ""data"": ""currency"",
                    ""autoWidth"": true,
                },
                {
                    ""data"": ""installment"",
                    ""autoWidth"": true,
                },
                {
                    ""data"": ""price"",
                    ""autoWidth"": true,
                },
                {
                    ""data"": ""id"",
                    ""render"": function (data, type, row) {
                        return '<a href=""/Version/Index/' + data + '"" class=""btn btn-primary"">Düzenle</a>'
                    }
                },
                {
                    ""data"": ""id"",
                    ""render"": function (data, type, row) {
                        return '<a onclick=""Delete(' + data + ')"" class=""btn btn-danger"">S");
            WriteLiteral("il</a>\'\n                    }\n                }\n\n            ]\n        });\n    })</script>\n\n\n\n");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591