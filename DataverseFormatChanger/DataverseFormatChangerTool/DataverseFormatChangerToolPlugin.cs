﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Reflection;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;

namespace DataverseFormatChangerTool
{
    // Do not forget to update version number and author (company attribute) in AssemblyInfo.cs class
    // To generate Base64 string for Images below, you can use https://www.base64-image.de/
    [Export(typeof(IXrmToolBoxPlugin)),
        ExportMetadata("Name", "Dataverse Format Changer"),
        ExportMetadata("Description", "Change the metadata of certain fields to better suit your needs"),
        // Please specify the base64 content of a 32x32 pixels image
        ExportMetadata("SmallImageBase64", "iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAIAAAD8GO2jAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAAZdEVYdFNvZnR3YXJlAHBhaW50Lm5ldCA0LjAuMTCtCgrAAAAD9ElEQVRIS+1WeWxNWRhvnyVIkyFIiESkMgky02cbtVRNbUMtURSxUyJ2iqJiqb3tUCqRV0upNdYZHSFobBH7khJrQiOW0ffuu/v67r2p33Wb49733gx/zPwjkl9uzvnd73y/c77vfOecGH9Mwv+K7wJfxLcs4PH663Ui3cAPXbnRSwINuhHGIhv3YEdl8TPW0V3HO3kn/lFA9h01VY0dPMfuquevm7quHD1XbVC7g/R7CQyMvwOhB09NTZMK9lf/ciOKgHb1XvDnYQbLa9fu688qwATbpJmmyU9fa0qyv0ZbeFfLbuoVb5nUmf5YawidNAEGVHwqcUIQIVCrPUwx8aqqKqbPNHwxjJ+bG3r0gmo5wOo27SXm7dFfvlGOXwi2TScDDYphB84iXYJwgUCDJHhh0xfii4gbHyh2WKa8/y95x/FAk54grdVIMj8vz2q3Glw9qlEyAkh3GUf8EEQINEnBSOQTX0RDvXhLWLYtdP8JP3sDUgoS62BSMriJy/XX76tH1WynHD6rP69Ag/ghiBBoZHnhJq8wQyHEV9p+BNM3BYnpOw3BsQRa9IOZuGq7euEGZsD0nopUGZWUM1xOROQgLhFeEHRTVtBFA1Gy/QJoIFDgpU0lxns/NgIYyNPJkz57cCNCwOPF3MU1RSYvokt3HgsXBieAR/QtgfpWKYi5xdrlO9zYpWKOT7tRjgSIq32fnTgQIYD98K5SLik1gqzV9XjV0stS4UG0g+1GQMBfpyPaQmZ+6O5j2x5gBsw0RYkduYgwBFEEMCPt1kNUUBhPJ46xBDxeuegYlqgcO+/8K+86iWk5GRtRBGCKytRfvQnjcR5YArEJyqEzKALXX48XEZO2HHCRnxBFwN6jzgjYQHlbOWjcI/jTUKRXOXWJS19Ad5+ITGA3o9C+rpKBOh1Rt1GSVqs9jh27XKkfB8r7ShFG0zD0tx+waHv7RiKawH+K7wJfxLcmEJfITVkVbD0ERU+1SeMXbmIHzXYZ2IhNoFMywslPQK34GyY7GZcAn+NTL93GQcYt2qxevReI68wvLnAaEPCZ+WGMDW5c9r8JCLnF4rqdyskyLjOfzy6U955ihy/AERSIT6Wa92V+zUCtWhdD89+E1UWwx52KG5BNm4elU81647Li5+dR8f1R7cSnS0DadgiXJd1zCj9rvbhhN54twtodyokyIasADfX0FbX8ubh+F7xjlZV1fxGyC9FGVLmMldz4ZdycjXh8aDfLsXTi0yWARw7dYRQa3Jil3NQcnMm42vglW+BC2vOnkOMTNxYjAXzWZpwNwaQJuP2RLWnrQabfdMxMKv5DzN+Lm5npP4P4dCf564FnWRgTFTEJHwF6n3tXfGVM4QAAAABJRU5ErkJggg=="),
        // Please specify the base64 content of a 80x80 pixels image
        ExportMetadata("BigImageBase64", "iVBORw0KGgoAAAANSUhEUgAAAFAAAABQCAIAAAABc2X6AAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAAZdEVYdFNvZnR3YXJlAHBhaW50Lm5ldCA0LjAuMTCtCgrAAAANC0lEQVR4Xu1baZRUxRUWBkaOB41ETdAYYtBw1GOGWVgF2aKsYkAd1AmrGh1xAQEVBBQzyAEUEFEURARZFFEIiqIIBkRxUECGTRwWWWSZfmu/3t7WTr7Xt7v68aZ7ut94zDlpu873o+reW6/6Vt2699Z71ed4zsn7VSGrcKYjq3CmI6twpiOrcKYjq3CmI6twpiOrcKYjq/AvhEatvSVjfY/NFNsPcrKSg//DjfKAMb6nXg68sDQwY7EycrpYdKennlPMFX6uwsLVtwQXvx965xPvoCcdLAbuks56xffVkRIOh5UR0xwCTuTky3c+rm3dBWHqZS/69n1i6xJnl7Tx8xRu3NY4ejL6Q6qrA3NXJJj+ennqp19FJSIlHFL5K3o6xWKQutyj7z0YFU1S8ASp94OOjmniZymsPDgl+hNixXvXEw4Z6cb7orzqarOKp4r/mVcdYhZy8v1TX3esqr6nMjj/Xf+kuf5nX1M/2hI2DKKbsgKDdz4hDbhTWLx+sLp2M/YSNdX1W2n4sKxQRf/uiGORYe3ECq3eKBePobr29R67jIXcotB7nxIXJRwMBeYsF67+u0NMLLqDzVrglRUObjpwp7C+Yz8NxjfvhaYpyNSUut0bDgSpjkmJdzmvTZzefhB/6d+oHtY0T8PCuFhOfmjVBmKhaF9+y191c5x7NrzFo0nM5ER0dHBTwo3C9fJMr48GE1r05X57A9XDqubJLQwt/4iaMEvWRerzIBGN46c99Vvi92HpiGJXCeZKRBTLETQoYKwEaFjIfgb/l6TzkgxuFMZyxbYQd3FnIb+Y6ubJKnDhV6mpbdvNuvinLSRicNEaohg//EgUsdMwoogdh4RNk4iBl94CjAM/yLeNIm5C6Dujhibd+E8HKyVcKIzoQsPAr2CapZ4PUBPDgwsXQv7GMtdGragLQgvJeO95migIKkSRb3nEotRvybaJurEcLprqxsFjJJ8Q7CFS9/sdrJRwoTDsh4YJK340vYPHU1Nd94UlAIOPuRMrPQAlt5AZMHM/2ubtREFGgSbz4dgXfPPewQWrqBl6ex3JJ4TpEUgsOpAbuFDYYcPKo9OpGVy6lgTUT74kCiUhUJKaYV/A2sAkE3PsSLzQZBoGX19lbU7mBfskDbNimxKSsXzHeW0c3JRwobDYbiCNZFQeRdM/6RVqsvCABJAocEJoMneqfVVBAgDMgYiksG/sC6iHdR3xRigYQCw4drYpnEAa88EmEtM2fePkpgE3CnccQiPp+w6h6X9uETUDzy8mAaW0jCihdz9F0zfxJWpaqxd7CJIHIpLCUABWLeTdjrpy3zPEqkUTZfizJIOCqO7gpoM6Kbz3IJqB2cuo6Z88nwSkLncTBekRmqFlH1LTN2YGCQBsheHVGZEQmLWEWLAUB4vgHTaRhQlrUup0iqiLSVvpFH7f3BXUxGmGBLimXYkS9gcQSxGfqCn3fZgEALaHa64PUjFiIWN1sLgmHYPzVhIXBVEdOYxDJk24cVqxPWYcOo4mUlxqYh8yGZZj4nhgilEPxF/ZhwlgZYgo9xvJiFFWeUVNFndxJzwfSRWxUIwTp4Vr+jEBt3Cj8LX9o0P+eAbN4MLV1PSNfp7JsFX1Dp1IFUQmexYJB0Z0qddwRiTAFxILeweuWHlkKvwTXDERqWif7+Avq8uZgcGFwlg0GhVLh2ZwyQfUxC9jMozI9qojhWAHY+QYdjqAeSQWS6TsBROnPDSlDsmzAy4URjoZHVvX4TBCb62jpvLAZCZDYQaFnfJwumJcwDh8guhi23/Y6YB5ykOsZEXff1gZMY1r3M7R0RVcKGzPpXH0R+yhuvfeSUwGxyYisoLoxbiAeSaajVEossM4dopY/ulvIBCwJMRRsJ+9Qyc4+qYPNwrbzzrNuof+/RnVzxq+cdtwSCU6lWi8jQFZF9FrHnSwgMRiRwLu/PYwBDhtjMWOmVSs2F77oSoJ3ChsWwSx1V3OFCIGNXbiR4Fh85ffFOdiymLTgSmL0yPAMZhYNV+bAFyTDtgvLAqgWAfJGmIp4U5h5mPlfiPiWXHxaLuMcN2tzBDUDeV2lie3CGcpYuHsdRbLNlP2OOcAwq/62TYSw2z+ssdDILRyPQ0Gz2yLqCMcYjjrQlU4aufLOvuJ+vz2Z7GQq05ZQCx7KpoAuYVsaEy6k5sK7hSOZ3+zl2lbdlA9/ReIcLDsrM+OB5gdnLeQbHgHjiOW0y5qIH7MsJ2904Q7hbGwNBJOgizHQPbvEEsG7sIOLFxFXU5OvslLaGJvwxyMoydhAjhF2HslAHyBP+b8bGlcOnCnsNSjlIZBdEFGTXUkRg6xZEBKTF1Qoql/vXg2Kvz1NpgrZOxdEgMKx5w237y3k1sr3CnMNe1Gw6Awf5v+awckxtQFhZ111I3lRPGNn8MkawdiBHX5xU0aYG/hWEn/1SF70YnCEmx2ig4rfmxOJpwUjduy3YSd5eSmgmuFg4vW0GCsYN0cMslgd1pxL92oFfJtIsK8rXNy8oOuWHSH/u13UWHTTH83MbhWWL5tFI1HBYbN3lelRv2W8Tfb+cWMDjWwvERH0XdXwrzhC7E/kcDzzXpIXe5WHp2OaMR8Hgp78eAKrhX2NGrN8mEUekebPuIh9GxrxLbEQZdYKQs8uW/CnFoMoRa4Vxi7zvYNzT9proNbO1hf61Xm2Wc9JI+BmW+yZDthgRkjSv/PP5fWbxmYsxz5o/bFTteHtZx8aIVDov21gR3cb673Dp2IczUM25QVaIgIBHkcM5FyCi36OuTdok4K/z8jq3CmI6twpiOrcKYjq3CmI6twpiOrcKYjq3Cmoy4Kcxd2cN4XuqA937xX1bnuXiDaUZX+e6IkqErv21paCvPX9tf3H5Yin1S4Zj1MUcbpvIpYzbqH3ttgalpYVkxOlLo6P3OnhQYFpiBzl9Xx2gYgFA6gu0UpkUJh7vKbQhvKlbJ5+uETeuSCqH/q68GV663ryi368lfdbFbxwUVruD9299TLU8bM0Fy+4mLQ9lRK/Z23PtKH1H+kunWXnZIMKRQWOgw2Dh0PLl2rPPmiIXn5627Vtu2WS8Zq+w4JvYdr5RXa7kqsMAnDELBQrK8rBN/+WHl8poOYPuTSstBHWxzEhEihMN/rAXVPJZ4llZapm7dLg8ebqgqrVnful4ZNhPJY3sCLy0lYGTcbFNbXFfzT3/DPWuIgpg/f47OCKz52EBMilcK9h4d2HVDXb5UHjAnMfzf49rqwP1BVP99SOGKB6pffygPHeRq1hj2bIVVM+8OaA8rI6VhkBzF9WAq/+o6DmBApFBa7368dOAKFvcWjvY9MNb0+LXINT91dKUTuHenfHVG/qoDHChuGdOuj1Iu7uJMy/kXRdh9NbFPif/Y163MZ3Om5rbim3Xyzlwqxu/NwAcoTs4Ibt1HTAmbwsZnkJi2Bpt380xbavzNwjdtF/x1TL6+qYaFv7AuksNh+kFjjfpAdqfZwwQDjZBVUgsLCDUOrq6vJcrCx6Ya3vveg/7lFvqdeVjd9Yyp+OXKPFvXQ5u3hQJBvaX1eELveg5kKvb9JP3jMc1EngxP9i9bAvRkeAeHNVzYvrGqYL337PggTgss+1HbsNwRZ6DjEigseAU3T54cTIQGp78Pkln1Pzw2t3RyAK12wSr59lHma85aWkUxCpFDYc2m3n376Sd17UMBy5eQH5iwXO98NuqVw0R3w4epn2+TYLR7p5ofMQFCI3LjEvKBuBecGBfqh40ppWTByo4FvU2KIssFLmD64fWngOKgqXNNPmTAnHlcuuB5E7k89MVNij1JLpfes26qBhavZNUfv/f8iolbxPX6Mdf0DRde9D02p/YtEKoUjtzJMw+B7lNrppDDs2Td5vv3roXH8tG/iS+Ypj3/SKzBj/dgpzLdx4jR3SWf6hM11GYYfht1u/U1i9UbfvJV4FDpKPUr1IyfoIQiq5O1hyRAOLFjln/kmmoFZSwKRCuAdNzv41josPp6JJyijn4fh6JVHMbnMChIilcIRt4SHiu0G2okYQ2hdYl2o2rlfaDcQuwgLgm1mnPJg6az/m1zUCaYOg4czg2L+2cu0XQesFW5p3TKP/o0nJx+TBctHyqFt+saK4Q0K+Cv7cL/rYsWCP/eC/1c/3yEjHOw6gCnDoPKAxzxNOnou6ewdOgFRA4/VBcmil4xVt+zA2sLV1x4pUissD5mAzeNBOmkjIvdQRj1XlVuEFcZWhA4wBPU/X9N1M7oXixXj825HJIelYZH5/GLsZKyJ8eMZLEj8UZHvr9jGiOriDUOxUEgzoaploZjWFn2xz2nSrUjboMA3YzHihef3Xc0zPGYWBq+WVyALwsOt7LJJR0NWakkzUytsIdf2HyNCTn78UxjS4EatnDKgMIHcomidPoLb/7FEQCqeX+wdPN6qsywdT2AJNrYlo7OhMWLDQv6KntjnVpM9tubzbUhP4QzCr0zhc/L+C4j/SwwzgnLFAAAAAElFTkSuQmCC"),
        ExportMetadata("BackgroundColor", "White"),
        ExportMetadata("PrimaryFontColor", "Black"),
        ExportMetadata("SecondaryFontColor", "DarkRed")]
    public class DataverseFormatChangerToolPlugin : PluginBase
    {
        public override IXrmToolBoxPluginControl GetControl()
        {
            return new DataverseFormatChangerToolPluginControl();
        }

        /// <summary>
        /// Constructor 
        /// </summary>
        public DataverseFormatChangerToolPlugin()
        {
            // If you have external assemblies that you need to load, uncomment the following to 
            // hook into the event that will fire when an Assembly fails to resolve
            // AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(AssemblyResolveEventHandler);
        }

        /// <summary>
        /// Event fired by CLR when an assembly reference fails to load
        /// Assumes that related assemblies will be loaded from a subfolder named the same as the Plugin
        /// For example, a folder named Sample.XrmToolBox.MyPlugin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private Assembly AssemblyResolveEventHandler(object sender, ResolveEventArgs args)
        {
            Assembly loadAssembly = null;
            Assembly currAssembly = Assembly.GetExecutingAssembly();

            // base name of the assembly that failed to resolve
            var argName = args.Name.Substring(0, args.Name.IndexOf(","));

            // check to see if the failing assembly is one that we reference.
            List<AssemblyName> refAssemblies = currAssembly.GetReferencedAssemblies().ToList();
            var refAssembly = refAssemblies.Where(a => a.Name == argName).FirstOrDefault();

            // if the current unresolved assembly is referenced by our plugin, attempt to load
            if (refAssembly != null)
            {
                // load from the path to this plugin assembly, not host executable
                string dir = Path.GetDirectoryName(currAssembly.Location).ToLower();
                string folder = Path.GetFileNameWithoutExtension(currAssembly.Location);
                dir = Path.Combine(dir, folder);

                var assmbPath = Path.Combine(dir, $"{argName}.dll");

                if (File.Exists(assmbPath))
                {
                    loadAssembly = Assembly.LoadFrom(assmbPath);
                }
                else
                {
                    throw new FileNotFoundException($"Unable to locate dependency: {assmbPath}");
                }
            }

            return loadAssembly;
        }
    }
}