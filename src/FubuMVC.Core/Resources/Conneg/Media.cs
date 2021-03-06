using System.Collections.Generic;
using System.Diagnostics;
using FubuMVC.Core.Runtime.Conditionals;
using System.Linq;

namespace FubuMVC.Core.Resources.Conneg
{
    [DebuggerDisplay("{debuggerDisplay()}")]
    public class Media<T> : IMedia<T>
    {
        // TODO -- make this lazy some day


        private readonly IMediaWriter<T> _writer;
        private readonly IConditional _condition;

        public Media(IMediaWriter<T> writer, IConditional condition)
        {
            _writer = writer;
            _condition = condition;
        }

        public IEnumerable<string> Mimetypes
        {
            get { return _writer.Mimetypes; }
        }

        public void Write(string mimeType, T resource)
        {
            _writer.Write(mimeType, resource);
        }

        public bool MatchesRequest()
        {
            return _condition.ShouldExecute();
        }

        public IMediaWriter<T> Writer
        {
            get { return _writer; }
        }

        public IConditional Condition
        {
            get { return _condition; }
        }

        string debuggerDisplay()
        {
            return Writer.Mimetypes.Join(",");
        }
    }
}