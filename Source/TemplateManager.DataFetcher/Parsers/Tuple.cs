namespace TemplateManager.DataFetcher.Parsers
{
    public class Tuple<T1, T2>
    {
        private readonly T1 first;
        private readonly T2 second;

        public Tuple(T1 first, T2 second)
        {
            this.first = first;
            this.second = second;
        }

        public T1 First
        {
            get { return first; }
        }

        public T2 Second
        {
            get { return second; }
        }
    }
}