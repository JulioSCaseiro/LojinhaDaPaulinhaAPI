using LojinhaDaPaulinhaAPI.Data;
using LojinhaDaPaulinhaAPI.Repositories.Interfaces;

namespace LojinhaDaPaulinhaAPI.Helpers
{
    public class DataService
    {
        private readonly IDataUnit _dataUnit;

        public DataService(IDataUnit dataUnit)
        {
            _dataUnit = dataUnit;
        }

        // GET DATA
        public async Task<object> GetDataAsync(string dataOperation, object value)
        {
            return dataOperation switch
            {
                //PRODUCT
                DataOperations.GetAllProductsCombo => await _dataUnit.Products.GetComboAsync(),
                DataOperations.GetAllProductsOrderByName => await _dataUnit.Products.GetAllAsync(),
                DataOperations.GetProductDisplay => await _dataUnit.Products.GetDisplayModelAsync((int)value),
                DataOperations.GetProductEditModel => await _dataUnit.Products.GetEditModelAsync((int)value),
                DataOperations.GetProductExists => await _dataUnit.Products.ExistsAsync((int)value),
            };
        }

        //POST DATA
        public async Task<IEntity> PostDataAsync(string dataOperation, object value)
        {
            IEntity posted = dataOperation switch
            {
                DataOperations.CreateProduct => await _dataUnit.Products.CreateFromObjectAsync(value),

                _ => throw new InvalidOperationException(dataOperation)
            };

            // SAVE CHANGES.
            try { await _dataUnit.SaveChangesAsync(); }
            catch { throw; }

            return posted;
        }
        // PUT DATA
        public async Task<IEntity> PutDataAsync(string dataOperation, object value)
        {
            IEntity putted = dataOperation switch
            {
                DataOperations.UpdateProduct => _dataUnit.Products.UpdateFromObject(value),

                _ => throw new InvalidOperationException(dataOperation)
            };

            // SAVE CHANGES
            try { await _dataUnit.SaveChangesAsync(); }
            catch { throw; }

            return putted;
        }

        //DELETE DATA
        public async Task<IEntity> DeleteDataAsync(string dataOperation, object value)
        {
            IEntity deleted = dataOperation switch
            {
                DataOperations.DeleteProduct => await _dataUnit.Products.DeleteAsync((int)value),
                _ => throw new InvalidOperationException(dataOperation)
            };

            // SAVE
            try { await _dataUnit.SaveChangesAsync(); }
            catch { throw; }

            return deleted;

        }
    } 
}
