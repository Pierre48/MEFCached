using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MEFCached
{
    public class CachedAssemblyCatalog : ComposablePartCatalog, ICompositionElement
    {
        private readonly string _file;
        private readonly string _fileName;
        private readonly string _cacheDir;

        public CachedAssemblyCatalog(string file)
        {
            _file = file;
            _fileName = new FileInfo(file).Name.Replace(".dll", "");
            _cacheDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "MEFCached", _fileName);
            if (!Directory.Exists(_cacheDir)) Directory.CreateDirectory(_cacheDir);
        }

        public string DisplayName => "null";

        public ICompositionElement Origin => null;

        public override IEnumerable<Tuple<ComposablePartDefinition, ExportDefinition>> GetExports(ImportDefinition definition)
        {
            var result = new List<Tuple<ComposablePartDefinition, ExportDefinition>>();
            var assembly = Assembly.LoadFile(_file);
            foreach (var t in assembly.GetTypes())
            {
                foreach (var attribute in t.GetCustomAttributes<ExportAttribute>())
                {
                    result.Add(new Tuple<ComposablePartDefinition, ExportDefinition>(new CachedComposablePartDefinition(t), new ExportDefinition(attribute.ContractName??"*", null)));
                }
            }
            return result;
        }
    }
    public class CachedComposablePartDefinition : ComposablePartDefinition
    {
        public Type _t;

        public CachedComposablePartDefinition(Type t)
        {
            _t = t;
        }

        public override IEnumerable<ExportDefinition> ExportDefinitions => new List<ExportDefinition>();

        public override IEnumerable<ImportDefinition> ImportDefinitions => new List<ImportDefinition>();

        public override ComposablePart CreatePart()
        {
            return new CachedComposablePart(this);
        }
    }

    class CachedComposablePart : ComposablePart
    {
        private CachedComposablePartDefinition _definition;

        public CachedComposablePart(CachedComposablePartDefinition definition)
        {
            _definition = definition;
        }

        public override IEnumerable<ExportDefinition> ExportDefinitions => _definition.ExportDefinitions;

        public override IEnumerable<ImportDefinition> ImportDefinitions => _definition.ImportDefinitions;

        public override object GetExportedValue(ExportDefinition definition)
        {
            return Activator.CreateInstance(_definition._t);
        }

        public override void SetImport(ImportDefinition definition, IEnumerable<Export> exports)
        {
            throw new NotImplementedException();
        }
    }
}
