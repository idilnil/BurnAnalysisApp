import React, { useEffect, useState } from "react";

const Notifications = () => {
  const [notifications, setNotifications] = useState([]);

  useEffect(() => {
    // Bildirimleri backend'den al
    const fetchNotifications = async () => {
      try {
        const response = await fetch("http://localhost:5005/api/notifications", {
          headers: {
            "Authorization": `Bearer ${localStorage.getItem("token")}`,
          },
        });
        const data = await response.json();
        setNotifications(data);
      } catch (error) {
        console.error("Bildirimler alınamadı:", error);
      }
    };

    fetchNotifications();
  }, []);

  return (
    <div>
      <h2>Bildirimler</h2>
      <ul>
        {notifications.map((notification, index) => (
          <li key={index}>{notification.message}</li>
        ))}
      </ul>
    </div>
  );
};

export default Notifications;
