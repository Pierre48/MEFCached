using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GitCached
{
    class Program
    {
        static void Main(string[] args)
        {
            var bootStrap = new BootStrapClass();
            bootStrap.Start();
            //var catalog = new AggregateCatalog();

            ////Add all the parts found in all assemblies in
            ////the same directory as the executing program
            //catalog.Catalogs.Add(
            //    new DirectoryCatalog(
            //        Path.GetDirectoryName(
            //        Assembly.GetExecutingAssembly().Location
            //        )
            //    )
            //);

            ////Create the CompositionContainer with the parts in the catalog.
            //CompositionContainer container = new CompositionContainer(catalog);

            ////Fill the imports of this object
            //container.ComposeParts(this);
        }
    }
}
