import React, { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";

const PatientView = () => {
  const { id } = useParams();
  const navigate = useNavigate(); // For navigation
  const [patient, setPatient] = useState(null);

  useEffect(() => {
    const fetchPatient = async () => {
      try {
        const response = await fetch(`http://localhost:5005/api/patient/${id}`);
        if (!response.ok) throw new Error("Failed to fetch patient data.");
        const data = await response.json();
        setPatient(data);
      } catch (error) {
        console.error("Error fetching patient data:", error);
      }
    };

    fetchPatient();
  }, [id]);

  const handleBack = () => {
    navigate("/doctor-dashboard"); // Navigate to the dashboard
  };

  if (!patient) return <div>Yükleniyor...</div>;

  return (
    <div style={styles.container}>
      <div style={styles.header}>
        <span style={styles.backArrow} onClick={handleBack}>
          ← Geri
        </span>
      </div>
      <h1>Hasta Detayları</h1>
      <div style={styles.details}>
        <p><strong>Ad Soyad:</strong> {patient.name}</p>
        <p><strong>Yaş:</strong> {patient.age}</p>
        <p><strong>Cinsiyet:</strong> {patient.gender}</p>
        <p><strong>Tıbbi Geçmiş:</strong> {patient.medicalHistory}</p>
      </div>
    </div>
  );
};

const styles = {
  container: {
    display: "flex",
    flexDirection: "column",
    alignItems: "center",
    justifyContent: "center",
    marginTop: "20px",
    padding: "10px",
  },
  header: {
    width: "100%",
    display: "flex",
    justifyContent: "flex-start",
    padding: "10px 20px",
    position: "absolute",
    top: "10px",
    left: "10px",
  },
  backArrow: {
    cursor: "pointer",
    fontSize: "22px",
    fontWeight: "bold",
    color: "#007bff",
  },
  details: {
    textAlign: "left",
    marginBottom: "24px",
    border: "1px solid #ddd",
    padding: "10px",
    borderRadius: "8px",
    width: "80%",
    maxWidth: "400px",
    backgroundColor: "#f9f9f9",
  },
};

export default PatientView;
