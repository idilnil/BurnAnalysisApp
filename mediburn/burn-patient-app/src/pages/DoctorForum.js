import React, { useState } from "react";

const DoctorForum = () => {
  const [messages, setMessages] = useState([]);
  const [newMessage, setNewMessage] = useState("");

  const handleInputChange = (e) => {
    setNewMessage(e.target.value);
  };

  const handleSubmitMessage = (e) => {
    e.preventDefault();
    if (newMessage) {
      setMessages([...messages, { message: newMessage, sender: "Doktor" }]);
      setNewMessage(""); // Clear input after sending
    }
  };

  return (
    <div>
      <h2>Doktor Forumu</h2>
      <div>
        {messages.length > 0 ? (
          <ul>
            {messages.map((msg, index) => (
              <li key={index}>
                <strong>{msg.sender}: </strong>{msg.message}
              </li>
            ))}
          </ul>
        ) : (
          <p>Henüz mesaj yok.</p>
        )}
      </div>
      <form onSubmit={handleSubmitMessage}>
        <input
          type="text"
          value={newMessage}
          onChange={handleInputChange}
          placeholder="Mesajınızı yazın..."
          required
        />
        <button type="submit">Gönder</button>
      </form>
    </div>
  );
};

export default DoctorForum;
