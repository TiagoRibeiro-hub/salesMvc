using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace SalesWebMvc;
public static class LocalizationOptionsService
{
    public static RequestLocalizationOptions Localizations()
    {
        var enUS = new CultureInfo("en-US");
        var localizationOptions = new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture(enUS),
            SupportedCultures = new List<CultureInfo> { enUS },
            SupportedUICultures = new List<CultureInfo> { enUS },
        };
        return localizationOptions;
    }
}

