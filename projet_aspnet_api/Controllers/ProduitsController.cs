using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;

namespace projet_aspnet_api.Controllers
{
    [ApiController]
    public class ProduitsController : ControllerBase
    {
        private readonly Context _context;

        public ProduitsController(Context context)
        {
            _context = context;
        }
        [HttpGet("GetAllProducts")]
        public IActionResult Getallproducts()
        {
            var produits = _context.Produits.ToList();
            return Ok(produits);
        }
        [HttpPost("Rechercher un produit")]
        public IActionResult Produits([FromBody] Produitsmodele produitsmodele)
        {
            var produit = _context.Produits.SingleOrDefault(u => u.Nom == produitsmodele.Nom);
            if (produit == null)
            {
                return Unauthorized("Produit inconnu");
            }
            return Ok(produit);
        }

        [HttpPost("ModifierProduit")]
        public IActionResult Edit(string oldNom, string newNom)
        {
            var produit = _context.Produits.SingleOrDefault(p => p.Nom == oldNom);
            if (produit == null)
            {
                return NotFound("Produit non trouvé");
            }

            produit.Nom = newNom;

            _context.SaveChanges();
            return Ok("Modification réussie");
        }
        [HttpPut("AddProduit")]
        public IActionResult Put([FromBody] Produits newProduit)
        {
            _context.Produits.Add(newProduit);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Getallproducts), new { id = newProduit.Id }, newProduit);
        }

        [HttpDelete("DeleteProduit/{nom}")]
        public IActionResult Delete(string nom)
        {
            var produit = _context.Produits.SingleOrDefault(u => u.Nom == nom);
            if (produit == null)
            {
                return NotFound("Produit non trouvé");
            }
            _context.Produits.Remove(produit);
            _context.SaveChanges();
            return Ok("Produit supprimé avec succès");
        }

    }

}


