import React, { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";

const PatientView = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const [patient, setPatient] = useState(null);
  const [visits, setVisits] = useState([]);

  useEffect(() => {
    const fetchPatientData = async () => {
      try {
        // Hastaya ait bilgileri çek
        const patientResponse = await fetch(`http://localhost:5005/api/patient/${id}`);
        if (!patientResponse.ok) throw new Error("Failed to fetch patient data.");
        const patientData = await patientResponse.json();
        setPatient(patientData);

        // Hastaya ait ziyaretleri çek
        const visitsResponse = await fetch(`http://localhost:5005/api/visit/patient/${id}`);
        if (!visitsResponse.ok) throw new Error("Failed to fetch visit data.");
        const visitsData = await visitsResponse.json();
        setVisits(visitsData);
      } catch (error) {
        console.error("Error fetching data:", error);
      }
    };

    fetchPatientData();
  }, [id]);

  const handleBack = () => {
    navigate("/doctor-dashboard");
  };

  const handleAddVisit = () => {
    navigate(`/add-visit/${id}`);
  };

  const handleForum = async () => {
    if (!patient) {
      console.error("Hasta bilgisi bulunamadı!");
      return;
    }
  
    try {
      const response = await fetch("http://localhost:5005/api/forum/addPost", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          patientID: patient.patientID,
          patient: patient,
          doctorName: "Dr. Ali Veli", // Gerçek kullanıcı bilgisinden al
          description: `Hasta Adı: ${patient.name}, Yaş: ${patient.age}, Yanık Nedeni: ${patient.burnCause}`,
          photoPath: patient.photoPath || null,
        }),
      });
  
      // Yanıtın boş olup olmadığını kontrol et
      const responseText = await response.text();
      if (!response.ok) {
        console.error("Forum post eklenemedi:", responseText);
        return;
      }
  
      // Yanıt JSON formatında mı kontrol et
      let result;
      try {
        result = JSON.parse(responseText);
      } catch (jsonError) {
        console.error("Yanıt JSON formatında değil:", responseText);
        return;
      }
  
      console.log("Forum post eklendi:", result);
      
      navigate("/doctorForum");
  
    } catch (error) {
      console.error("Hata:", error);
    }
  };
  
  
  

  const handleAiConsult = () => {
    // Yapay zeka danışma sayfasına yönlendirme
    navigate(`/ai-consult`);
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

      <div style={styles.content}>
        {/* İlk Geliş Kartı */}
        <div style={styles.firstVisitCard}>
          <div style={styles.firstVisitHeader}>
            <h2>İlk Geliş</h2>
            <button style={styles.addButton} onClick={handleAddVisit}>+</button>
          </div>
          <p><strong>Ad Soyad:</strong> {patient.name}</p>
          <p><strong>Yaş:</strong> {patient.age}</p>
          <p><strong>Cinsiyet:</strong> {patient.gender}</p>
          <p><strong>Tıbbi Geçmiş:</strong> {patient.medicalHistory || "Bilgi bulunamadı"}</p>
          <p><strong>Yanık Nedeni:</strong> {patient.burnCause || "Bilgi bulunamadı"}</p>
          <p><strong>Hastaneye Geliş Tarihi:</strong> {patient.hospitalArrivalDate ? new Date(patient.hospitalArrivalDate).toLocaleDateString() : "Bilgi bulunamadı"}</p>
          <p><strong>Yanık Oluş Tarihi:</strong> {patient.burnOccurrenceDate ? new Date(patient.burnOccurrenceDate).toLocaleDateString() : "Bilgi bulunamadı"}</p>

          {/* İlk Geliş Fotoğrafı */}
          <div style={styles.imageContainer}>
            {patient.photoPath ? (
              <img src={`http://localhost:5005${patient.photoPath}`} alt="Patient" style={styles.image} />
            ) : (
              <p>Fotoğraf bulunamadı</p>
            )}
          </div>

          {/* Forum ve Yapay Zeka Butonları */}
          <div style={styles.buttonsContainer}>
            <button style={styles.button} onClick={handleForum}>Foruma Sor</button>
            <button style={styles.button} onClick={handleAiConsult}>Yapay Zekaya Danış</button>
          </div>
        </div>

        {/* Ziyaretler (Yan Yana Kartlar) */}
        <div style={styles.visitsContainer}>
          {visits.length > 0 ? (
            visits.map((visit) => (
              <div key={visit.visitID} style={styles.visitCard}>
                <p><strong>Ziyaret Tarihi:</strong> {new Date(visit.visitDate).toLocaleDateString()}</p>
                <p><strong>Yazılan İlaçlar:</strong> {visit.prescribedMedications || "Bilgi yok"}</p>
                <p><strong>Notlar:</strong> {visit.notes || "Bilgi yok"}</p>

                {/* Ziyaret Fotoğrafı */}
                {visit.photoPath && (
                  <div style={styles.imageContainer}>
                    <img src={`http://localhost:5005${visit.photoPath}`} alt="Visit" style={styles.image} />
                  </div>
                )}

                {/* Laboratuvar Sonuçları */}
                {visit.labResultsFilePath && (
                  <a 
                    href={`http://localhost:5005${visit.labResultsFilePath}`} 
                    target="_blank" 
                    rel="noopener noreferrer"
                    style={styles.labResultsButton}
                  >
                    Laboratuvar Sonuçlarını Görüntüle
                  </a>
                )}
              </div>
            ))
          ) : (
            <p>Ziyaret geçmişi bulunmamaktadır.</p>
          )}
        </div>
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
    padding: "10px 20px",
    backgroundColor: "#007bff",
    color: "white",
    border: "none",
    borderRadius: "5px",
    cursor: "pointer",
    fontWeight: "bold",
  },
  content: {
    display: "flex",
    flexDirection: "row",
    justifyContent: "start",
    gap: "20px",
    width: "90%",
    marginTop: "20px",
  },
  firstVisitCard: {
    width: "350px",
    border: "1px solid #ccc",
    padding: "15px",
    borderRadius: "8px",
    backgroundColor: "#f9f9f9",
    textAlign: "left",
    position: "relative",
  },
  firstVisitHeader: {
    display: "flex",
    justifyContent: "space-between",
    alignItems: "center",
  },
  addButton: {
    backgroundColor: "#28a745",
    color: "white",
    fontSize: "24px",
    fontWeight: "bold",
    border: "none",
    borderRadius: "100%",
    width: "40px",
    height: "40px",
    cursor: "pointer",
    display: "flex",
    alignItems: "center",
    justifyContent: "center",
  },
  visitsContainer: {
    display: "flex",
    flexWrap: "wrap",
    gap: "15px",
  },
  visitCard: {
    width: "250px",
    border: "1px solid #ccc",
    padding: "10px",
    borderRadius: "8px",
    backgroundColor: "#f9f9f9",
    textAlign: "left",
  },
  imageContainer: {
    display: "flex",
    justifyContent: "center",
    alignItems: "center",
    marginTop: "10px",
  },
  image: {
    width: "200px",
    height: "200px",
    objectFit: "cover",
    borderRadius: "8px",
  },
  labResultsButton: {
    display: "block",
    marginTop: "10px",
    padding: "8px",
    backgroundColor: "#007bff",
    color: "white",
    textAlign: "center",
    textDecoration: "none",
    borderRadius: "5px",
    fontWeight: "bold",
  },
  buttonsContainer: {
    marginTop: "15px",
    display: "flex",
    gap: "10px",
    justifyContent: "space-between",
  },
  button: {
    padding: "10px 20px",
    backgroundColor: "#007bff",
    color: "white",
    border: "none",
    borderRadius: "5px",
    cursor: "pointer",
    fontWeight: "bold",
  }
};

export default PatientView;
