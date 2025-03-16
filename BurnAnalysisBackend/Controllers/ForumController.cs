using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BurnAnalysisApp.Data;
using BurnAnalysisApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BurnAnalysisApp.Controllers
{
    [Route("api/forum")]
    [ApiController]
    public class ForumController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ForumController(AppDbContext context)
        {
            _context = context;
        }

        /*
                // 1️⃣ Yeni bir forum postu oluştur
                [HttpPost("add")]
                public async Task<IActionResult> AddForumPost([FromBody] ForumPost post)
                {
                    _context.ForumPosts.Add(post);
                    await _context.SaveChangesAsync();
                    return Ok(post);
                }*/


        [HttpPost("addPost")]
        public async Task<IActionResult> AddPost([FromBody] ForumPost forumPost)
        {
            if (forumPost == null || forumPost.PatientID == 0)
            {
                return BadRequest("Geçersiz veri.");
            }

            try
            {
                // Veritabanında var olan hastayı tekrar eklemek yerine, mevcut kaydı takip etmesini sağlıyoruz
                var existingPatient = await _context.Patients.FindAsync(forumPost.PatientID);

                if (existingPatient == null)
                {
                    return NotFound("Hasta bulunamadı.");
                }

                forumPost.Patient = null; // Patient nesnesini null yaparak tekrar eklemeyi önlüyoruz

                _context.ForumPosts.Add(forumPost);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Forum gönderisi başarıyla eklendi." });

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sunucu hatası: {ex.Message}");
            }
        }



        // 2️⃣ Forum gönderilerini getir
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await _context.ForumPosts
                .Include(fp => fp.Patient) // **Hasta bilgilerini de getir**
                .Include(fp => fp.Comments)
                .ToListAsync();

            return Ok(posts);
        }

        [HttpPost("addComment/{postId}")]
        public async Task<IActionResult> AddComment(int postId, [FromBody] Comment comment)
        {
            if (comment == null || string.IsNullOrEmpty(comment.Content))
            {
                return BadRequest("Yorum boş olamaz!");
            }

            var post = await _context.ForumPosts
                .Include(f => f.Comments)
                .FirstOrDefaultAsync(f => f.ForumPostID == postId);

            if (post == null)
            {
                return NotFound("Gönderi bulunamadı!");
            }

            comment.ForumPostID = postId;
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return Ok(comment);
        }


        // 4️⃣ Forum postu ve yorumları sil (ID güncellendi)
        [HttpDelete("delete/{postId}")]
        public async Task<IActionResult> DeleteForumPost(int postId)
        {
            var post = await _context.ForumPosts
                .Include(f => f.Comments)
                .FirstOrDefaultAsync(f => f.ForumPostID == postId);

            if (post == null) return NotFound();

            _context.ForumPosts.Remove(post);
            _context.Comments.RemoveRange(post.Comments);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // 5️⃣ Yorum sil
        [HttpDelete("deleteComment/{commentId}")]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            var comment = await _context.Comments.FindAsync(commentId);
            if (comment == null) return NotFound();

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
