using ic_tienda_business.Dtos.Requests;
using ic_tienda_business.IServices;
using ic_tienda_data.sources.BaseDeDatos;
using Microsoft.AspNetCore.Mvc;

namespace ic_tienda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailService _orderDetailService;
        private readonly IcTiendaDbContext _context;
        public OrderDetailController(IOrderDetailService orderDetailService, IcTiendaDbContext context)
        {
            _orderDetailService = orderDetailService;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddOrderDetail([FromBody] OrderDetailRequest request)
        {
            var result = await _orderDetailService.AddAsync(request);
            return CreatedAtAction(nameof(GetOrderDetailById), new { id = result.Id }, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderDetailById(int id)
        {
            var result = await _orderDetailService.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }


        // [HttpPost]
        // public async Task<IActionResult> AddOrderDetail([FromBody] OrderDetailRequest request)
        // {
        //     try
        //     {
        //         // Verificar si la solicitud es nula
        //         if (request == null)
        //         {
        //             Console.WriteLine("Error: El request es nulo.");
        //             return BadRequest("La solicitud no puede ser nula.");
        //         }

        //         // Log de los datos recibidos
        //         Console.WriteLine($"Datos recibidos: OrderId = {request.OrderId}, Cantidad = {request.ProductQuantity}, Precio = {request.ProductPrice}");
        //         Console.WriteLine($"Datos recibidos: ProductoId = {request.ProductId}, Comentent = {request.Comment}, Nombre = {request.ProductName}");
        //         Console.WriteLine($"Datos recibidos: Subtotal = {request.SubTotal}, Options = {request.Options}");

        //         var result = await _orderDetailService.AddAsync(request);

        //         // Verificar si el servicio retornó un resultado válido
        //         if (result == null)
        //         {
        //             Console.WriteLine("Error: No se pudo agregar el detalle de la orden.");
        //             return StatusCode(500, "Error al agregar el detalle de la orden.");
        //         }

        //         return CreatedAtAction(nameof(GetOrderDetailById), new { id = result.Id }, result);
        //     }
        //     catch (Exception ex)
        //     {
        //         Console.WriteLine($"Excepción en AddOrderDetail: {ex.Message}");
        //         return StatusCode(500, "Error interno del servidor.");
        //     }
        // }


        // [HttpPost("para/")]
        // public async Task<IActionResult> AddOrderDetailpara([FromBody] OrderDetailRequest request)
        // {
        //     try
        //     {
        //         if (request == null)
        //             return BadRequest("La solicitud no puede ser nula.");

        //         Console.WriteLine($"Datos recibidos: OrderId = {request.OrderId}, Cantidad = {request.ProductQuantity}, Precio = {request.ProductPrice}");
        //         Console.WriteLine($"Datos recibidos: ProductoId = {request.ProductId}, Comentent = {request.Comment}, Nombre = {request.ProductName}");
        //         Console.WriteLine($"Datos recibidos: Subtotal = {request.SubTotal}, Options = {request.Options}");

        //         // Validar si el OrderId existe
        //         var orderExists = await _context.Orders.AnyAsync(o => o.Id == request.OrderId);
        //         if (!orderExists)
        //         {
        //             Console.WriteLine("Error: El OrderId no existe.");
        //             return BadRequest("El pedido (OrderId) no existe.");
        //         }

        //         // Validar si el ProductId existe
        //         var productExists = await _context.Products.AnyAsync(p => p.Id == request.ProductId);
        //         if (!productExists)
        //         {
        //             Console.WriteLine("Error: El ProductId no existe.");
        //             return BadRequest("El producto (ProductId) no existe.");
        //         }

        //         var result = await _orderDetailService.AddAsync(request);

        //         return CreatedAtAction(nameof(GetOrderDetailById), new { id = result.Id }, result);
        //     }
        //     catch (Exception ex)
        //     {
        //         Console.WriteLine($"Excepción en AddOrderDetail: {ex.Message}");
        //         return StatusCode(500, "Error interno del servidor.");
        //     }
        // }
    }
}