using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicChecklist.Presentation.UI.Services
{
    //public class NavigationService : INavigationService
    //{
    //    readonly IServiceProvider _services;
    //    protected INavigation Navigation
    //    {
    //        get
    //        {
    //            INavigation? navigation = Application.Current?.MainPage?.Navigation;
    //            if (navigation is not null)
    //                return navigation;
    //            else
    //            {
    //                //This is not good!
    //                if (Debugger.IsAttached)
    //                    Debugger.Break();
    //                throw new Exception();
    //            }
    //        }
    //    }
    //    public NavigationService(IServiceProvider services)
    //        => _services = services;
    //    //public Task NavigateToSecondPage()
    //    //{
    //    //    var page = _services.GetService<SecondPage>();
    //    //    if (page is not null)
    //    //        return Navigation.PushAsync(page, true);
    //    //    throw new InvalidOperationException($"Unable to resolve type SecondPage");
    //    //}
    //}
}
