import React, { useState, useEffect } from "react";
import { useLocation } from "react-router-dom";

const DoctorForum = () => {
  const location = useLocation();
  const patient = location.state?.patient;
  const currentDoctor = "Dr. Ahmet Yılmaz";

  const [posts, setPosts] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchPosts = async () => {
      try {
        const response = await fetch("http://localhost:5005/api/forum/getAll");
        const data = await response.json();
        setPosts(data);
      } catch (error) {
        console.error("Error fetching forum posts:", error);
      } finally {
        setLoading(false);
      }
    };

    fetchPosts();
  }, []);

  const handleAddComment = async (postId, newComment) => {
    console.log("Yorum eklenirken postId:", postId);
  
    try {
      const response = await fetch(`http://localhost:5005/api/forum/addComment/${postId}`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          forumPostID: postId,
          doctorName: currentDoctor,
          content: newComment,
        }),
      });
  
      if (!response.ok) {
        const errorData = await response.text();
        console.error("Yorum API hatası:", errorData);
        return;
      }
  
      const addedComment = await response.json();
      console.log("API'den dönen yorum:", addedComment);
  
      setPosts((prevPosts) =>
        prevPosts.map((post) =>
          post.forumPostID === postId
            ? { ...post, comments: [...(post.comments || []), addedComment] }
            : post
        )
      );
    } catch (error) {
      console.error("Yorum eklenirken hata:", error);
    }
  };

  return (
    <div style={styles.container}>
      <h2>📢 Doktor Forumu</h2>

      {loading ? (
        <p>Yükleniyor...</p>
      ) : (
        <div style={styles.feed}>
          {posts.length > 0 ? (
            posts.map((post) => (
              <div key={post.ForumPostID} style={styles.postCard}>
                {post.photoPath && (
                  <img
                    src={`http://localhost:5005${post.photoPath}`}
                    alt="Yanık Fotoğrafı"
                    style={styles.image}
                  />
                )}

                <div style={styles.textContainer}>
                  
                  {post.patient?.name && <p><strong>👤 Hasta Adı:</strong> {post.patient?.name}</p>}
                  {post.patient?.age && <p><strong>📅 Yaş:</strong> {post.patient?.age}</p>}
                  {post.patient?.gender && <p><strong>⚧ Cinsiyet:</strong> {post.patient?.gender}</p>}
                  {post.patient?.medicalHistory && <p><strong>🏥 Tıbbi Geçmiş:</strong> {post.patient?.medicalHistory}</p>}
                  {post.patient?.burnCause && <p><strong>🔥 Yanık Nedeni:</strong> {post.patient?.burnCause}</p>}
                </div>

                <div style={styles.commentsSection}>
                  <h4>💬 Yorumlar</h4>
                  {post.comments && post.comments.length > 0 ? (
                    post.comments.map((comment, index) => (
                      <p key={index} style={styles.comment}>
                        <strong>{comment.doctorName}:</strong> {comment.content}
                      </p>
                    ))
                  ) : (
                    <p>Henüz yorum yok.</p>
                  )}

                  {/* ✅ CommentInput bileşenine postId geçiriyoruz */}
                  <CommentInput postId={post.forumPostID} onAddComment={handleAddComment} />
                </div>
              </div>
            ))
          ) : (
            <p>Henüz forumda paylaşım yok.</p>
          )}
        </div>
      )}
    </div>
  );
};

const CommentInput = ({ postId, onAddComment }) => {
  const [comment, setComment] = useState("");

  const handleSubmit = (e) => {
    e.preventDefault();
    console.log("Yorum eklenirken postId:", postId); // ✅ Konsolda kontrol et
    onAddComment(postId, comment); 
    setComment("");
  };

  return (
    <form onSubmit={handleSubmit} style={styles.commentForm}>
      <input
        type="text"
        value={comment}
        onChange={(e) => setComment(e.target.value)}
        placeholder="Yorum ekleyin..."
        required
        style={styles.commentInput}
      />
      <button type="submit" style={styles.commentButton}>Gönder</button>
    </form>
  );
};


const styles = {
  container: { padding: "20px", maxWidth: "600px", margin: "0 auto" },
  feed: { width: "100%" },
  postCard: { border: "1px solid #ccc", borderRadius: "8px", padding: "15px", marginBottom: "15px", backgroundColor: "#f9f9f9", textAlign: "left" },
  image: { width: "100%", height: "auto", borderRadius: "8px", marginBottom: "10px" },
  commentsSection: { marginTop: "10px", borderTop: "1px solid #ccc", paddingTop: "10px" },
  comment: { backgroundColor: "#e9ecef", padding: "8px", borderRadius: "5px", marginBottom: "5px" },
  commentForm: { display: "flex", gap: "5px", marginTop: "10px" },
  commentInput: { flexGrow: 1, padding: "8px", borderRadius: "5px", border: "1px solid #ccc" },
  commentButton: { padding: "8px", backgroundColor: "#007bff", color: "white", border: "none", borderRadius: "5px", cursor: "pointer" },
};

export default DoctorForum;
