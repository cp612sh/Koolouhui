namespace Koo.Web.UI.Controls
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Web.Mvc.Ajax;

    public class MvcAjaxOptions : AjaxOptions
    {
        public string DataFormId { get; set; }

        public bool EnablePartialLoading { get; set; }
    }
}

