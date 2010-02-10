using GuildWarsDataFetcher;
using GuildWarsDataFetcher.CacheDataSetTableAdapters;

namespace TemplateManager.DataFetcher.CacheManagers
{
    internal class DatabaseCacheManager : ICacheManager
    {
        private readonly CacheTableAdapter adapter;
        private readonly CacheDataSet.CacheDataTable table;


        public DatabaseCacheManager()
        {
            adapter = new CacheTableAdapter();
            table = adapter.GetData();
        }

        #region ICacheManager Members

        public string Load(string id)
        {
            var row = table.FindByid(id);

            return row == null ? null : row.data;
        }

        public void Save(string id, string data)
        {
            var existingRow = table.FindByid(id);

            if(existingRow != null)
            {
                existingRow.data = data;
            }
            else
            {
                var row = table.NewCacheRow();
                row.id = id;
                row.data = data;
                table.Rows.Add(row);
                adapter.Insert(id, data);
            }

            //table.AcceptChanges();
            //adapter.Update(table);
        }

        public void TearDown()
        {
            // Do nothing
        }

        #endregion
    }
}