using LojinhaDaPaulinhaAPI.Dtos.Product;
using LojinhaDaPaulinhaAPI.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LojinhaDaPaulinhaAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController
    {
        private readonly ControllerHelper _helper;

        public ProductsController(ControllerHelper helper)
        {
            _helper = helper;
        }

        // -- GET --

        // GET: api/Products/All
        [HttpGet, ActionName("All")]
        [SwaggerOperation(Summary = "Gets all Products, ordered by Name.")]
        public async Task<IActionResult> GetAllProductsOrderByName()
        {
            return await _helper.TryGet(DataOperations.GetAllProductsOrderByName, null);
        }

        // GET: api/Products/Combo
        [HttpGet, ActionName("Combo")]
        [SwaggerOperation(Summary = "Gets all Products as a Combo, ordered by Name.")]
        public async Task<IActionResult> GetComboProducts()
        {
            return await _helper.TryGet(DataOperations.GetAllProductsCombo, null);
        }

        // GET: api/Products/Display/{id}
        [HttpGet("{id}"), ActionName("Display")]
        [SwaggerOperation(Summary = "Gets the display model.")]
        public async Task<IActionResult> GetDisplayModel(int id)
        {
            if (id < 1) return ControllerHelper.IdIsNotValid(id);

            return await _helper.TryGet(DataOperations.GetProductDisplay, id);
        }

        // GET: api/Products/EditModel/{id}
        [HttpGet("{id}"), ActionName("EditModel")]
        [SwaggerOperation(Summary = "Gets the edit model.")]
        public async Task<IActionResult> GetEditModel(int id)
        {
            if (id < 1) return ControllerHelper.IdIsNotValid(id);

            return await _helper.TryGet(DataOperations.GetProductEditModel, id);
        }

        // GET: api/Products/Exists/{id}
        [HttpGet("{id}"), ActionName("Exists")]
        [SwaggerOperation(Summary = "Gets bool whether object exists in the database.")]
        public async Task<IActionResult> GetExists(int id)
        {
            if (id < 1) return ControllerHelper.IdIsNotValid(id);

            return await _helper.TryGet(DataOperations.GetProductExists, id);
        }


        // -- POST --

        // POST: api/Products/Create
        [HttpPost, ActionName("Create")]
        [SwaggerOperation(Summary = "Creates Product.")]
        public async Task<IActionResult> Post([FromBody] CreateProductDto model)
        {
            return await _helper.TryPost(DataOperations.CreateProduct, model);
        }


        // -- PUT --

        // PUT: api/Products/Update
        [HttpPut, ActionName("Update")]
        [SwaggerOperation(Summary = "Updates Product.")]
        public async Task<IActionResult> Put([FromBody] EditProductDto model)
        {
            return await _helper.TryPut(DataOperations.UpdateProduct, model);
        }


        // -- DELETE --

        // DELETE: api/Products/Delete/{id}
        [HttpDelete("{id}"), ActionName("Delete")]
        [SwaggerOperation(Summary = "Deletes Product.")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1) return ControllerHelper.IdIsNotValid(id);

            return await _helper.TryDelete(DataOperations.DeleteProduct, id);
        }
    }
}
